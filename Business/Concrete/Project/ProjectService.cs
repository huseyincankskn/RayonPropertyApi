﻿using AutoMapper;
using Business.Abstract;
using Business.Abstract.Project;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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

        public ProjectService(IMapper mapper, IProjectRepository projectRepository)
        {
            _mapper = mapper;
            _projectRepository = projectRepository;
        }

        public IDataResult<ProjectDto> AddProject(ProjectDto project)
        {
            var addEntity = _mapper.Map<Project>(project);
            var lastNumber = _projectRepository.GetAll().OrderByDescending(x => x.AddDate)?.FirstOrDefault()?.ProjectNumber;
            var projectNumber = string.IsNullOrEmpty(lastNumber) ? "0002148512" : (Convert.ToInt32(lastNumber) + 1).ToString();
            addEntity.ProjectNumber = projectNumber;
            _projectRepository.Add(addEntity);
            return new SuccessDataResult<ProjectDto>(project);
        }
        public IDataResult<bool> SaveImages(List<IFormFile> images)
        {
            // Watermark fotoğrafını yükle
            var watermark = Image.FromFile("gslogo.png");
            //Watermark transparancy ayarları
            var imageAttributes = new ImageAttributes();
            var colorMatrix = new ColorMatrix { Matrix33 = 0.3f };
            imageAttributes.SetColorMatrix(colorMatrix);

            // Her dosyayı işleyin
            foreach (var formFile in images)
            {
                if (formFile.Length > 0)
                {
                    // Dosyayı yükle
                    var image = Image.FromStream(formFile.OpenReadStream());

                    // Watermark fotoğrafını ekle
                    using (var graphics = Graphics.FromImage(image))
                    {
                        graphics.DrawImage(watermark, new Rectangle(0, 0, 130, 170), 0, 0, watermark.Width, watermark.Height, GraphicsUnit.Pixel, imageAttributes);
                    }

                    // İşlenmiş fotoğrafı kaydet
                    var filePath = Path.Combine("Files", formFile.FileName);
                    image.Save(filePath);
                }
            }
            return new SuccessDataResult<bool>(true);
        }
    }
}