using AutoMapper;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Business.Concrete
{
    public class BlogFileService : IBlogFileService
    {
        private readonly IBlogFileRepository _blogFileRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public BlogFileService(IBlogFileRepository blogFileRepository,
                               IWebHostEnvironment webHostEnvironment,
                               IMapper mapper)
        {
            _blogFileRepository = blogFileRepository;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }


        public IDataResult<BlogFile> SaveImage(IFormFile image)
        {
            var guid = Guid.NewGuid().ToString();
            var setImage = System.Drawing.Image.FromStream(image.OpenReadStream());
            var name = guid.Substring(guid.Length - 8) + image.FileName;
            if (name.Length > 200)
            {
                name = name[..200];
            }
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath + "/BlogFile/", name);
            setImage.Save(filePath);
            setImage.Dispose();
            var blogFileDto = new BlogFileAddDto()
            {
                FileName = name
            };

            var addEntity = _mapper.Map<BlogFile>(blogFileDto);
            var entity = _blogFileRepository.Add(addEntity);
            return new SuccessDataResult<BlogFile>(entity.Data);
        }
    }
}
