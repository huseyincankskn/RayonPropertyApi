using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProjectFeatureService : IProjectFeatureService
    {
        private readonly IProjectFeaturesRepository _projectFeaturesRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IMapper _mapper;

        public ProjectFeatureService(IProjectFeaturesRepository projectFeaturesRepository, IFeatureRepository featureRepository, IMapper mapper)
        {
            _projectFeaturesRepository = projectFeaturesRepository;
            _featureRepository = featureRepository;
            _mapper = mapper;
        }

        public IDataResult<IQueryable<ProjectFeaturesVm>> GetListQueryableOdata()
        {
            var entityList = _projectFeaturesRepository.GetAllForOdata();
            var vmList = _mapper.ProjectTo<ProjectFeaturesVm>(entityList);
            return new SuccessDataResult<IQueryable<ProjectFeaturesVm>>(vmList);
        }
        public IDataResult<ProjectFeaturesVm> GetById(short id)
        {
            var entity = _projectFeaturesRepository.GetAllForOdata().FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return new ErrorDataResult<ProjectFeaturesVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<ProjectFeaturesVm>(entity);
            return new SuccessDataResult<ProjectFeaturesVm>(vm);
        }
        public IDataResult<ProjectFeatureDto> AddProject(ProjectFeatureDto project)
        {
            var addEntity = _mapper.Map<ProjectFeature>(project);
            var response = _projectFeaturesRepository.Add(addEntity);
            return new SuccessDataResult<ProjectFeatureDto>(project);
        }
        public Core.Utilities.Results.IResult Update(ProjectFeatureDto dto)
        {
            var project = _projectFeaturesRepository.GetById(dto.Id);
            dto.TrimAllProps();
            project = _mapper.Map(dto, project);
            _projectFeaturesRepository.Update(project);
            return new SuccessResult(Messages.EntityUpdated);
        }
        public IResult Delete(short id)
        {
            var entity = _projectFeaturesRepository.GetById(id);
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var projectList = _featureRepository.GetAll().Where(x => x.ProjectFeatureId == entity.Id).ToList();
            _featureRepository.HardDeleteRange(projectList);
            _projectFeaturesRepository.HardDelete(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}
