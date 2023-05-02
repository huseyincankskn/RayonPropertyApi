using AutoMapper;
using Business.Abstract;
using Business.Abstract.Project;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;
using Entities.Dtos;
using Entities.VMs;
using Helper.AppSetting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection.Metadata;
using System.Security.Principal;

namespace Business.Concrete
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectFilesRepository _projectFilesRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectFeaturesRepository _projectFeaturesRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ITranslateService _translateService;
        private readonly ITranslateRepository _translateRepository;

        public ProjectService(
            IMapper mapper,
            IProjectRepository projectRepository,
            IHttpContextAccessor httpContextAccessor,
            IProjectFilesRepository projectFilesRepository,
            IProjectFeaturesRepository projectFeaturesRepository,
            IFeatureRepository featureRepository,
            IWebHostEnvironment webHostEnvironment,
            ITranslateService translateService,
            ITranslateRepository translateRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
            _projectFilesRepository = projectFilesRepository;
            _projectFeaturesRepository = projectFeaturesRepository;
            _featureRepository = featureRepository;
            _webHostEnvironment = webHostEnvironment;
            _translateService = translateService;
            _translateRepository = translateRepository;
        }
        public IDataResult<IQueryable<ProjectVm>> GetListQueryableOdata()
        {
            var entityList = _projectRepository.GetAllForOdataWithPassive()
                .Include(x => x.Town)
                .Include(x => x.City)
                .Include(x => x.District)
                .Include(x => x.Features)
                .Include(x => x.Street)
                .OrderByDescending(x => x.AddDate);
            var vmList = _mapper.ProjectTo<ProjectVm>(entityList);
            return new SuccessDataResult<IQueryable<ProjectVm>>(vmList);
        }

        public IDataResult<ProjectVm> GetById(Guid id)
        {
            var entity = _projectRepository.GetAllForOdata().Include(x => x.Town).Include(x => x.City).Include(x => x.District).Include(x => x.Street).FirstOrDefault(x => x.Id == id);
            var features = GetFeatureList(id);
            if (entity == null)
            {
                return new ErrorDataResult<ProjectVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<ProjectVm>(entity);
            var files = _projectFilesRepository.GetAllForOdata().Where(x => x.ProjectId == id).ToList();
            vm.PhotoUrls = files.Select(x => AppSettings.ImgUrl + x.FileName).ToList();
            vm.CheckBoxField = features.Data.Select(x => x.ProjectFeatureId).ToList();
            return new SuccessDataResult<ProjectVm>(vm);
        }
        public IDataResult<ProjectDto> AddProject(ProjectDto project)
        {
            project.CurrencyId = 1;
            var decimalPrice = Convert.ToDecimal(project.Price);
            var addEntity = _mapper.Map<Project>(project);
            var lastNumber = _projectRepository.GetAll().OrderByDescending(x => x.AddDate)?.FirstOrDefault()?.ProjectNumber;

            #region Translate
            var jsonDescription = JsonConvert.SerializeObject(addEntity.Description);
            var jsonDescriptionDe = JsonConvert.SerializeObject(addEntity.DescriptionDe);
            var jsonDescriptionRu = JsonConvert.SerializeObject(addEntity.DescriptionRu);


            var addTranslateList = new List<Translate>()
            {
                new Translate()
                {
                    Key = addEntity.Title,
                    KeyDe = addEntity.TitleDe,
                    KeyRu = addEntity.TitleRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                },
                new Translate()
                {
                    Key = jsonDescription,
                    KeyDe = jsonDescriptionDe,
                    KeyRu = jsonDescriptionRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                }
            };

            _translateRepository.AddRange(addTranslateList);

            addEntity.TitleTranslateKey = addTranslateList.First().TranslateKey;
            addEntity.DescriptionTranslateKey = addTranslateList.Last().TranslateKey;
            #endregion
            addEntity.StreetId = 1;
            var response = _projectRepository.Add(addEntity);
            project.Id = response.Data.Id;
            if (response.Success)
            {
                foreach (var item in project.CheckBoxField)
                {
                    Feature feature = new Feature()
                    {
                        ProjectId = project.Id,
                        ProjectFeatureId = item
                    };
                    _featureRepository.Add(feature);
                }
            }
            return new SuccessDataResult<ProjectDto>(project);
        }
        public Core.Utilities.Results.IResult Update(ProjectDto dto)
        {
            dto.CurrencyId = 1;
            dto.StreetId = 1;
            var project = _projectRepository.GetById(dto.Id);
            dto.TrimAllProps();
            project = _mapper.Map(dto, project);

            #region Translate
            var addTranslateList = new List<Translate>();

            var translateTitle = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == project.Title
                                                                                           && x.KeyDe == project.TitleDe
                                                                                           && x.KeyRu == project.TitleRu);
            if (translateTitle == null)
            {

                var TranslateEntity = new Translate()
                {
                    Key = project.Title,
                    KeyDe = project.TitleDe,
                    KeyRu = project.TitleRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                };
                addTranslateList.Add(TranslateEntity);
                project.TitleTranslateKey = TranslateEntity.TranslateKey;
            }

            var translateDescription = _translateRepository.GetAllForOdata().FirstOrDefault(x => x.Key == project.Description
                                                                                                 && x.KeyDe == project.DescriptionDe
                                                                                                 && x.KeyRu == project.DescriptionRu);
            if (translateDescription == null)
            {
                var jsonDescription = JsonConvert.SerializeObject(project.Description);
                var jsonDescriptionDe = JsonConvert.SerializeObject(project.DescriptionDe);
                var jsonDescriptionRu = JsonConvert.SerializeObject(project.DescriptionRu);
                var TranslateEntity = new Translate()
                {
                    Key = jsonDescription,
                    KeyDe = jsonDescriptionDe,
                    KeyRu = jsonDescriptionRu,
                    TranslateKey = _translateService.GenerateUniqueTranslateKey()
                };
                addTranslateList.Add(TranslateEntity);
                project.DescriptionTranslateKey = TranslateEntity.TranslateKey;
            }

            if (addTranslateList.Any())
            {
                _translateRepository.AddRange(addTranslateList);
            }

            #endregion

            var result = _projectRepository.Update(project);
            if (result.Success)
            {
                var features = _featureRepository.GetAll().Where(x => x.ProjectId == dto.Id).ToList();
                _featureRepository.HardDeleteRange(features);
                List<Feature> featuresList = new List<Feature>();
                foreach (var item in dto.CheckBoxField)
                {
                    Feature featuresFeature = new Feature()
                    {
                        ProjectFeatureId = item,
                        ProjectId = dto.Id
                    };
                    featuresList.Add(featuresFeature);
                }
                _featureRepository.AddRange(featuresList);
            }
            return new SuccessResult(Messages.EntityUpdated);
        }
        public IDataResult<bool> SaveImages(List<IFormFile> images, string productId)
        {
            var productIderId = JsonConvert.DeserializeObject<string>(productId);
            // Watermark fotoğrafını yükle
            var watermark = Image.FromFile(_webHostEnvironment.WebRootPath + "/Logo/Rayon_Logo.png");
            //Watermark transparancy ayarları
            var imageAttributes = new ImageAttributes();
            var colorMatrix = new ColorMatrix { Matrix33 = 0.3f };
            imageAttributes.SetColorMatrix(colorMatrix);
            List<ProjectFiles> fileList = new List<ProjectFiles>();
            // Her dosyayı işleyin
            foreach (var formFile in images)
            {
                if (formFile.Length > 0)
                {
                    var guid = Guid.NewGuid();
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath + "/Content/", guid.ToString().Substring(guid.ToString().Length - 8) + formFile.FileName);
                    if (formFile.ContentType.Contains("mp4") || formFile.ContentType.Contains("video"))
                    {
                        // Videoyu kaydetmek için dosya akışını açın
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            // Dosya akışından videoyu okuyun ve dosyaya yazın
                            formFile.CopyToAsync(stream);
                        }

                        ProjectFiles projectFile = new ProjectFiles()
                        {
                            FileName = guid.ToString().Substring(guid.ToString().Length - 8) + formFile.FileName,
                            ProjectId = Guid.Parse(productIderId)
                        };
                        fileList.Add(projectFile);
                    }
                    else
                    {
                        // Dosyayı yükle
                        var image = Image.FromStream(formFile.OpenReadStream());

                        // Watermark fotoğrafını ekle
                        using (var graphics = Graphics.FromImage(image))
                        {
                            graphics.DrawImage(watermark, new Rectangle(0, 0, image.Width, image.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
                        }

                        // İşlenmiş fotoğrafı kaydet
                        image.Save(filePath);

                        ProjectFiles projectFile = new ProjectFiles()
                        {
                            FileName = guid.ToString().Substring(guid.ToString().Length - 8) + formFile.FileName,
                            ProjectId = Guid.Parse(productIderId)
                        };
                        fileList.Add(projectFile);
                    }
                   
                }
            }
            _projectFilesRepository.AddRange(fileList);
            return new SuccessDataResult<bool>(true);
        }
        public Core.Utilities.Results.IResult DeletePhoto(string fileName)
        {
            var file = _projectFilesRepository.GetAll().Where(x => x.FileName == fileName).FirstOrDefault();
            _projectFilesRepository.Delete(file);
            return new SuccessResult(Messages.EntityDeleted);
        }
        public IDataResult<IQueryable<ProjectFeaturesVm>> GetProjectFeatureList()
        {
            var entityList = _projectFeaturesRepository.GetAllForOdata();
            var vmList = _mapper.ProjectTo<ProjectFeaturesVm>(entityList);
            return new SuccessDataResult<IQueryable<ProjectFeaturesVm>>(vmList);
        }
        private IDataResult<IQueryable<FeatureVm>> GetFeatureList(Guid id)
        {
            var entityList = _featureRepository.GetAllForOdata().Where(x => x.ProjectId == id);
            var vmList = _mapper.ProjectTo<FeatureVm>(entityList);
            return new SuccessDataResult<IQueryable<FeatureVm>>(vmList);
        }
        public Core.Utilities.Results.IResult Delete(Guid id)
        {
            var entity = _projectRepository.GetByIdWithPassive(id);
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var modelActive = !entity.IsActive;
            entity.IsActive = modelActive;
            _projectRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }

        public Core.Utilities.Results.IResult SellOrNot(IsSoldDto dto)
        {
            var entity = _projectRepository.GetByIdWithPassive(dto.Id);
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var modelIsSold = !entity.IsSold;
            entity.IsSold = modelIsSold;
            _projectRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }
        public Core.Utilities.Results.IResult IsFavourite(IsSoldDto dto)
        {
            var entity = _projectRepository.GetByIdWithPassive(dto.Id);
            if (entity == null)
            {
                return new ErrorResult(Messages.EntityNotFound);
            }
            var modelIsFavourite = !entity.IsFavourite;
            entity.IsFavourite = modelIsFavourite;
            _projectRepository.Update(entity);
            return new SuccessResult(Messages.EntityUpdated);
        }
    }
}
