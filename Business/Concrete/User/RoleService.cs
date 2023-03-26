using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.VMs;

namespace Business.Concrete
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRoleRepository _userRoleRepository;

        public RoleService(IRoleRepository roleRepository,
                           IMapper mapper,
                           IUserRoleRepository userRoleRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _userRoleRepository = userRoleRepository;
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


    }
}
