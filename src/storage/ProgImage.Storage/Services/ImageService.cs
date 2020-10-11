using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProgImage.Storage.Domain.Models;
using ProgImage.Storage.Domain.Repositories;
using ProgImage.Storage.Domain.Services;
using ProgImage.Storage.Domain.Services.RO;
using ProgImage.Storage.Helpers;
using Serilog;

namespace ProgImage.Storage.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IStorageService _storageService;
        private readonly Utils _utils;

        public ImageService(IImageRepository imageRepository, IStorageService storageService)
        {
            _imageRepository = imageRepository;
            _storageService = storageService;
            _utils = new Utils();
        }

        public async Task<ImageUploadResponse> AddImageAsync(IFormFile uploadedImage)
        {
            Guid imageId = Guid.NewGuid();
            var fileExtension = await _utils.GetImageExtensionAsync(uploadedImage);
            var imageFilePath = _utils.GenerateFileName(imageId.ToString(), fileExtension);

            try
            {
                await _storageService.SaveFile(uploadedImage, imageFilePath, FileMode.Create);
            }
            catch (ArgumentNullException)
            {
                Log.Debug($"Failed saving file to disk. (Image id: {imageId})");
                return new ImageUploadResponse(false, "Error: output path is invalid.", null);
            }

            Image image = new Image
            {
                ImageId = imageId,
                ImageFilePath = imageFilePath
            };

            try
            {
                await _imageRepository.AddAsync(image);
            }
            catch (DbUpdateException)
            {
                Log.Debug($"Failed saving image to database. (Image id: {imageId})");
                return new ImageUploadResponse(false, "Error: Unable to add image.", image);
            }

            Log.Debug($"Saved a new image. (Image id: {imageId})");

            return new ImageUploadResponse(true, null, image);
        }

        public async Task<ImageFindResponse> FindByImageIdAsync(Guid imageId)
        {
            Image image = await _imageRepository.FindByImageIdAsync(imageId);

            if (image != null)
            {
                return new ImageFindResponse(true, null!, image);
            }

            Log.Debug($"Unable to find image. (Image id: {imageId})");
            return new ImageFindResponse(false, "Error: Unable to fetch image by id.", null);
        }

        public async Task<ImageResponse> GetImageBytesAsync(string filePath, string extension)
        {
            byte[] image;
            var openedImageFile = await _storageService.OpenFile(filePath);

            if (extension == null)
            {
                return new ImageResponse(true, null, openedImageFile);
            }

            try
            {
                image = _utils.ConvertImage(openedImageFile, extension);
            }
            catch (SystemException)
            {
                Log.Debug($"Failed converting image. (Path: {filePath})");
                return new ImageResponse(false, $"Invalid image format: {extension}", null);
            }

            return new ImageResponse(true, null, image);
        }
    }
}