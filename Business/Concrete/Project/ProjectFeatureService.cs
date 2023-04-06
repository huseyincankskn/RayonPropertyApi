using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class ProjectFeatureService : IProjectFeatureService
    {
        private readonly IProjectFeaturesRepository _projectFeaturesRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IMapper _mapper;
        private readonly ITranslateRepository _translateRepository;
        private readonly ITranslateService _translateService;

        public ProjectFeatureService(IProjectFeaturesRepository projectFeaturesRepository,
                                     IFeatureRepository featureRepository,
                                     IMapper mapper,
                                     ITranslateRepository translateRepository,
                                     ITranslateService translateService)
        {
            _projectFeaturesRepository = projectFeaturesRepository;
            _featureRepository = featureRepository;
            _mapper = mapper;
            _translateRepository = translateRepository;
            _translateService = translateService;
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
        public IDataResult<IQueryable<string>> GetFeaturesWithProjectId(Guid id)
        {
            var entityList = _featureRepository.GetAllForOdata().Include(x => x.Project).Include(x => x.ProjectFeature).Where(x => x.ProjectId == id).Select(x => x.ProjectFeature.Name);
            return new SuccessDataResult<IQueryable<string>>(entityList);
        }
        public IDataResult<ProjectFeatureDto> AddProject(ProjectFeatureDto project)
        {
            var addEntity = _mapper.Map<ProjectFeature>(project);
            var translate = new Translate()
            {
                Key = addEntity.Name,
                KeyDe = addEntity.NameDe,
                KeyRu = addEntity.NameRu,
                TranslateKey = _translateService.GenerateUniqueTranslateKey()
            };
            addEntity.NameTranslateKey = translate.TranslateKey;
            _translateRepository.Add(translate);
            var response = _projectFeaturesRepository.Add(addEntity);
            return new SuccessDataResult<ProjectFeatureDto>(project);
        }
        public Core.Utilities.Results.IResult Update(ProjectFeatureDto dto)
        {
            var project = _projectFeaturesRepository.GetById(dto.Id);
            dto.TrimAllProps();
            project = _mapper.Map(dto, project);

            var translateEntity = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == project.Name
                                                                                && x.KeyDe == project.NameDe
                                                                                && x.KeyRu == project.NameRu);
            if (translateEntity == null)
            {
                var translate = new Translate()
                {
                    Key = project.Name,
                    KeyDe = project.NameDe,
                    KeyRu = project.NameRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                };

                _translateRepository.Add(translate);
                project.NameTranslateKey = translate.TranslateKey;
            }

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
