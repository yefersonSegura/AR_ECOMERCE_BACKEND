
using AR.Common.Dto;

namespace AR.Common.InterfacesFirabase
{
    public interface IFirebaseFN
    {
        Task<ResponseDto<string>> UploadStorage(Stream stream, string folder, string name);
    }
}