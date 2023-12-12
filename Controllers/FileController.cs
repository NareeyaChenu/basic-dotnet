using Microsoft.AspNetCore.Mvc;
using TodoApi.Interfaces;
using TodoApi.DTOs.file;
namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/v1/file")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage([FromForm] FileModel file)
        {
            Console.WriteLine(file.File!.FileName);
            return await _fileService.UploadFile(file.File);
        }
        [HttpGet]
        [Route("{name}")]
        public ActionResult GetFiles([FromRoute] string name)
        {
            return _fileService.GetFileName(name);
        }

        [HttpGet]
        [Route("html")]
        public IActionResult Index()
        {
            string htmlContent = "<center><h1>This is some HTML content.</h1>/center>";
            return Content(htmlContent, "text/html");
        }

    }
}