using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using FluentValidation;

namespace Business.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;

        public RoleService(IRoleRepository roleRepository,
                           IMapper mapper,
                           IUserRoleRepository userRoleRepository,
                           IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
        }

        public IResult CheckUserRole(Guid userId, short roleId)
        {
            var userRoleEntity = _userRoleRepository.GetAllForWithoutLogin()
                                                    .FirstOrDefault(x => x.UserId == userId && x.RoleId == roleId);
            if (userRoleEntity != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<RoleVm> GetById(short id)
        {
            var entity = _roleRepository.GetById(id);
            var dtoList = _mapper.Map<RoleVm>(entity);
            return new SuccessDataResult<RoleVm>(dtoList);
        }

        public IDataResult<IQueryable<RoleVm>> GetListQueryable()
        {
            var entityList = _roleRepository.GetAllForOdata();
            var dtoList = _mapper.ProjectTo<RoleVm>(entityList);
            return new SuccessDataResult<IQueryable<RoleVm>>(dtoList);
        }

        public RoleVm GetRoleByPath(string path, string method)
        {
            var entityList = _roleRepository.GetAllForOdata()
                                 .Where(x => x.ControllerName == path);
            var dto = _mapper.ProjectTo<RoleVm>(entityList).FirstOrDefault();
            return dto;
        }

        public IDataResult<List<RoleVm>> RoleList(RoleVm role)
        {
            var userId = _userRepository.GetById(role.UserId)?.Id;

            if (userId == null) throw new ValidationException("");

            var userRoles = _userRoleRepository.GetAllForOdataWithPassive().Where(x => x.UserId == userId).Select(x => new { x.RoleId, x.IsActive }).ToList();
            var roles = _roleRepository.GetAll();
            var roleList = _mapper.ProjectTo<RoleVm>(roles).OrderBy(x => x.Id).ThenBy(x => x.Name).ToList();

            foreach (var item in roleList)
            {
                item.IsActive = userRoles.FirstOrDefault(x => x.RoleId == item.Id)?.IsActive == true;
            }
            return new SuccessDataResult<List<RoleVm>>(roleList);
        }

        public IResult UpdateUserRoles(UserRoleDto roleDto)
        {
            var user = _userRepository.GetById(roleDto.UserId);
            if (user is null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            var role = _roleRepository.GetById(roleDto.RoleId);
            if (role is null)
            {
                return new ErrorResult(Messages.RoleNotFound);
            }

            if (roleDto.IsActive)
            {
                var entity = _userRoleRepository.GetAllForOdataWithPassive().FirstOrDefault(x => x.UserId == roleDto.UserId && x.RoleId == roleDto.RoleId);
                if (entity != null)
                {
                    entity.IsActive = true;
                    _userRoleRepository.Update(entity);
                }
                else
                {
                    var userRole = new UserRole()
                    {
                        UserId = roleDto.UserId,
                        RoleId = roleDto.RoleId,

                    };
                    _userRoleRepository.Add(userRole);
                }

            }
            else
            {
                var userRole = _userRoleRepository.GetAllForOdataWithPassive().FirstOrDefault(x => x.UserId == roleDto.UserId && x.RoleId == roleDto.RoleId);
                if (userRole != null)
                {
                    userRole.IsActive = false;
                    _userRoleRepository.Update(userRole);
                }
            }
            return new SuccessResult();
        }
    }
}
