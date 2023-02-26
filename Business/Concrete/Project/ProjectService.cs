using AutoMapper;
using Business.Abstract.Project;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using DataAccess.Abstract;
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
        public ProjectService(
            IMapper mapper, 
            IProjectRepository projectRepository, 
            IHttpContextAccessor httpContextAccessor, 
            IProjectFilesRepository projectFilesRepository, 
            IProjectFeaturesRepository projectFeaturesRepository,
            IFeatureRepository featureRepository,
            IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
            _projectFilesRepository = projectFilesRepository;
            _projectFeaturesRepository = projectFeaturesRepository;
            _featureRepository = featureRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IDataResult<IQueryable<ProjectVm>> GetListQueryableOdata()
        {
            var entityList = _projectRepository.GetAllForOdataWithPassive().Include(x=> x.Town).Include(x => x.City).Include(x => x.District).Include(x => x.Street).OrderByDescending(x=> x.AddDate);
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
            var projectNumber = string.IsNullOrEmpty(lastNumber) ? "0002148512" : (Convert.ToInt32(lastNumber) + 1).ToString();
            addEntity.ProjectNumber = projectNumber;
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
            var project = _projectRepository.GetById(dto.Id);
            dto.TrimAllProps();
            project = _mapper.Map(dto, project);
            var result = _projectRepository.Update(project);
            if(result.Success)
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
            var watermark = Image.FromFile(_webHostEnvironment.WebRootPath + "/Logo/Rayon_Property_Logo_EN@4x.png");
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

                    // Dosyayı yükle
                    var image = Image.FromStream(formFile.OpenReadStream());

                    // Watermark fotoğrafını ekle
                    using (var graphics = Graphics.FromImage(image))
                    {
                        graphics.DrawImage(watermark, new Rectangle(0, 0, image.Width, image.Height), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
                    }

                    // İşlenmiş fotoğrafı kaydet
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath + "/Content/", guid.ToString().Substring(guid.ToString().Length - 8) + formFile.FileName);
                    image.Save(filePath);

                    ProjectFiles projectFile = new ProjectFiles()
                    {
                        FileName = guid.ToString().Substring(guid.ToString().Length - 8) + formFile.FileName,
                        ProjectId = Guid.Parse(productIderId)
                    };
                    fileList.Add(projectFile);
                }
            }
            _projectFilesRepository.AddRange(fileList);
            return new SuccessDataResult<bool>(true);
        }
        public Core.Utilities.Results.IResult DeletePhoto (string fileName)
        {
            var file = _projectFilesRepository.GetAll().Where(x=> x.FileName== fileName).FirstOrDefault();
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
            var entityList = _featureRepository.GetAllForOdata().Where(x=> x.ProjectId == id);
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
    }
}
