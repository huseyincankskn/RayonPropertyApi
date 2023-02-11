using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract.EntityFramework.Repository;
using Entities.Concrete;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Business.Concrete
{
    public class BlogFileService : IBlogFileService
    {
        private readonly IBlogFileRepository _blogFileRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BlogFileService(IBlogFileRepository blogFileRepository, IWebHostEnvironment webHostEnvironment)
        {
            _blogFileRepository = blogFileRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        public IDataResult<BlogFile> SaveImage(IFormFile image)
        {
            var guid = new Guid();
            var setImage = Image.FromStream(image.OpenReadStream());
            var name = guid.ToString().Substring(guid.ToString().Length - 8) + image.FileName;
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath + "/Content/", name);
            setImage.Save(filePath);

            var BlogFile = new BlogFile()
            {
                FileName = name
            };

            var entity = _blogFileRepository.Add(BlogFile);
            return new SuccessDataResult<BlogFile>(entity.Data);
        }
    }
}
