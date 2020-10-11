namespace ProgImage.Mask.Domain.Services
{
    public interface IMaskService
    {
        byte[] MaskImage(byte[] image);
    }
}