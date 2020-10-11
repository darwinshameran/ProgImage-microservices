namespace ProgImage.Rotate.Domain.Services
{
    public interface IRotateService
    { 
        byte[] RotateImage(byte[] image, int degrees);
    }
}