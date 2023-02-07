using AutoMapper;
using Business.Abstract;
using Business.Abstract.Project;
using Business.Constants;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Repositories;
using Entities.Concrete;
using Entities.Dto;
using Entities.Dtos;
using Entities.VMs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ProjectService(
            IMapper mapper, 
            IProjectRepository projectRepository, 
            IHttpContextAccessor httpContextAccessor, 
            IProjectFilesRepository projectFilesRepository, 
            IProjectFeaturesRepository projectFeaturesRepository,
            IFeatureRepository featureRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
            _httpContextAccessor = httpContextAccessor;
            _projectFilesRepository = projectFilesRepository;
            _projectFeaturesRepository = projectFeaturesRepository;
            _featureRepository = featureRepository;
        }
        public IDataResult<IQueryable<ProjectVm>> GetListQueryableOdata()
        {
            var entityList = _projectRepository.GetAllForOdata();
            var vmList = _mapper.ProjectTo<ProjectVm>(entityList);
            return new SuccessDataResult<IQueryable<ProjectVm>>(vmList);
        }
        public IDataResult<ProjectVm> GetById(Guid id)
        {
            var entity = _projectRepository.GetAllForOdata().FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return new ErrorDataResult<ProjectVm>(Messages.EntityNotFound);
            }
            var vm = _mapper.Map<ProjectVm>(entity);
            return new SuccessDataResult<ProjectVm>(vm);
        }
        public IDataResult<ProjectDto> AddProject(ProjectDto project)
        {
            project.CurrencyId = 1;
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
        public IDataResult<bool> SaveImages(List<IFormFile> images)
        {
            var productIderId = _httpContextAccessor.HttpContext.Request.Headers["productId"].ToString();
            // Watermark fotoğrafını yükle
            var watermark = Image.FromFile("gslogo.png");
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
                        graphics.DrawImage(watermark, new Rectangle(0, 0, 130, 170), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
                    }

                    // İşlenmiş fotoğrafı kaydet
                    var filePath = Path.Combine("Files", guid.ToString().Substring(guid.ToString().Length - 8) + formFile.FileName);
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

        public IDataResult<IQueryable<ProjectFeaturesVm>> GetProjectFeatureList()
        {
            var entityList = _projectFeaturesRepository.GetAllForOdata();
            var vmList = _mapper.ProjectTo<ProjectFeaturesVm>(entityList);
            return new SuccessDataResult<IQueryable<ProjectFeaturesVm>>(vmList);
        }
    }
}
