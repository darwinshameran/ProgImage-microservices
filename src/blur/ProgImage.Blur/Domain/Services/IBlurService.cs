namespace ProgImage.Blur.Domain.Services
{
    public interface IBlurService
    {
        byte[] BlurImage(byte[] image, double radius, double sigma);
    }
}