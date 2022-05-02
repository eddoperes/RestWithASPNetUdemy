using RestWithASPNetUdemy.Data.VO;

namespace RestWithASPNetUdemy.Business.Implementations
{
    public class FileBusinessImplementation : IFileBusiness
    {

        private readonly string _basePath;
        private readonly IHttpContextAccessor _context;

        public FileBusinessImplementation(IHttpContextAccessor context)
        {
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\"; 
        }

        public byte[] GetFile(string fileName)
        {
            var filePath = _basePath + fileName;
            return File.ReadAllBytes(filePath);  
        }

        public async Task<FileDetailVO> SaveFileToDisk(IFormFile file)
        {
            FileDetailVO fileDetail = new FileDetailVO();
            var fileType = Path.GetExtension(file.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if (fileType.ToLower() == ".pdf" ||
                fileType.ToLower() == ".jpg" ||
                fileType.ToLower() == ".png" ||
                fileType.ToLower() == ".jpeg") 
            {
                var docName = Path.GetFileName(file.FileName);
                if (file != null && file.Length > 0) 
                { 
                    var destination = Path.Combine (_basePath, docName);
                    fileDetail.DocumentName = docName;
                    fileDetail.DocType = fileType;
                    fileDetail.DocUrl = Path.Combine(baseUrl + "/api/file/v1/" + fileDetail.DocumentName);

                    using var stream = new FileStream(destination, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            return fileDetail;
        }

        public async Task<List<FileDetailVO>> SaveFilesToDisk(IList<IFormFile> files)
        {
            var filesDetail = new List<FileDetailVO>();
            for (int i = 0; i < files.Count; i++) 
            {
                filesDetail.Add(await SaveFileToDisk(files[i]));
            }
            return filesDetail;
        }

    }
}
