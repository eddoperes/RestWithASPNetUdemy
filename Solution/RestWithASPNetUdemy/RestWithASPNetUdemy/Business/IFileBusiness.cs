using RestWithASPNetUdemy.Data.VO;

namespace RestWithASPNetUdemy.Business
{
    public interface IFileBusiness
    {
        public byte[] GetFile (string fileName);
        public Task<FileDetailVO> SaveFileToDisk(IFormFile file);
        public Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files);
    }
}
