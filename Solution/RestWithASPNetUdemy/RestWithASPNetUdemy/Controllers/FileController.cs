using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNetUdemy.Business;
using RestWithASPNetUdemy.Data.VO;

namespace RestWithASPNetUdemy.Controllers
{

    [ApiVersion("1")]
    [Authorize("Bearer")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController: Controller 
    {

        private readonly IFileBusiness _fileBusinnes;

        public FileController(IFileBusiness fileBusinnes)
        {
            _fileBusinnes = fileBusinnes;
        }

        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType(200, Type = typeof(FileDetailVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> GetFileAsync(string fileName)
        {
            byte[] buffer = _fileBusinnes.GetFile(fileName);
            if (buffer != null) 
            {
                HttpContext.Response.ContentType = $"application/{Path.GetExtension(fileName).Replace(".","")}";
                HttpContext.Response.Headers.Add("content-lenght", buffer.Length.ToString());
                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }
            return new ContentResult();
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType(200, Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileDetailVO detail = await _fileBusinnes.SaveFileToDisk(file);
            return new OkObjectResult(detail);
        }

        [HttpPost("uploadMultipleFile")]
        [ProducesResponseType(200, Type = typeof(FileDetailVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadMamyFiles([FromForm] List<IFormFile> files)
        {
            List<FileDetailVO> details = await _fileBusinnes.SaveFilesToDisk(files);
            return new OkObjectResult(details);
        }

    }

}
