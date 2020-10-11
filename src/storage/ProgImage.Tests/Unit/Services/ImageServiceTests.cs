using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Moq;
using ProgImage.Storage.Domain.Models;
using ProgImage.Storage.Domain.Repositories;
using ProgImage.Storage.Domain.Services;
using ProgImage.Storage.Domain.Services.RO;
using ProgImage.Storage.Helpers;
using ProgImage.Storage.Services;
using Xunit;
using Image = ProgImage.Storage.Domain.Models.Image;
using ImageFormat = ProgImage.Storage.Helpers.ImageFormat;

namespace Tests.Services
{
    public class ImageUploadServiceTests
    {
        private readonly Guid _dummyFileGuid = Guid.NewGuid();
        private readonly string _dummyFileName = "image.jpeg";
        private readonly string _dummyFilePath = "$HOME/super_secret_image.jpeg";
        private Mock<IImageRepository> _imageRepository;
        private readonly IImageService _imageService;
        private readonly Utils _utils;
        private Mock<IStorageService> _storageService;
        
        public ImageUploadServiceTests()
        {
            SetupMocks();
            _imageService = new ImageService(_imageRepository.Object, _storageService.Object);
            _utils = new Utils();
        }

        private void SetupMocks()
        {
            _imageRepository = new Mock<IImageRepository>();
            _imageRepository.Setup(r => r.FindByImageIdAsync(_dummyFileGuid))
                .ReturnsAsync(new Image {ImageId = _dummyFileGuid, ImageFilePath = _dummyFilePath});

            _storageService = new Mock<IStorageService>(); 
        }

        [Fact]
        public async Task Should_Add_Non_Existing_Image()
        {
            IFormFile dummyImageFile = new FormFile(new MemoryStream(
                Encoding.UTF8.GetBytes("image")), 0, 1024, "image", _dummyFileName);

            ImageUploadResponse response = await _imageService.AddImageAsync(dummyImageFile);

            Assert.True(response.Success);
            Assert.IsType<Guid>(response.Image.ImageId);
            Assert.NotNull(response.Image.ImageFilePath);
        }

        [Fact]
        public async Task Should_Find_Existing_Image_By_ImageId()
        {
            ImageFindResponse response = await _imageService.FindByImageIdAsync(_dummyFileGuid);

            Assert.NotNull(response);
            Assert.Equal(response.Image.ImageId, _dummyFileGuid);
            Assert.Equal(response.Image.ImageFilePath, _dummyFilePath);
        }

        [Fact]
        public async Task Should_Return_Null_When_Trying_To_Find_Image_By_Invalid_ImageId()
        {
            Guid guidOfInvalidImage = Guid.NewGuid();
            ImageFindResponse response = await _imageService.FindByImageIdAsync(guidOfInvalidImage);

            Assert.False(response.Success);
        }


        /* Test might fail as it seems like ImageMagick has some issues, or at least the version I am using.
         * Getting the error `no decode delegate for this image format `' @ error/blob.c/BlobToImage/458` quite
         * frequently, but works sometimes.
         */
        [Fact]
        public async Task Should_Convert_Image_Bmp_To_Png()
        {
            
            Bitmap bitmap = new Bitmap(80, 20);
            ImageConverter converter = new ImageConverter();
            byte[] bytes = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            byte[] convertedImage = _utils.ConvertImage(bytes, "png");
            ImageFormat imageFormat = _utils.GetImageFormat(convertedImage);
            
            Assert.Equal("png", imageFormat.ToString());
        }
    }
}