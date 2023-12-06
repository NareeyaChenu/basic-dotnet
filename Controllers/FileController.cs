using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/v1/file")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public ActionResult CreateFile([FromForm] IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var fileContent = reader.ReadToEnd();

                // Set the content type based on your file type
                var contentType = file.ContentType;

                // Return the file in the response
                return File(Encoding.UTF8.GetBytes(fileContent), contentType, file.FileName);
            }

        }
    }
}