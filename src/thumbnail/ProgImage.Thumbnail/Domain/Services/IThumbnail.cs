namespace ProgImage.Resize.Domain.Services
{
    public interface IThumbnail
    {
        byte[] Resize(byte[] image, int width, int height);
    }
}