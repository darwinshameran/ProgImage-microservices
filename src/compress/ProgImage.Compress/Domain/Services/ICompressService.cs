namespace ProgImage.Compress.Domain.Services
{
    public interface ICompressService
    {
        byte[] CompressImage(byte[] image, int quality);
    }
}