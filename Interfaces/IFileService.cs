using Microsoft.AspNetCore.Mvc;
namespace TodoApi.Interfaces
{
    public interface IFileService
    {
        public Task<ActionResult>  UploadFile (IFormFile file);

        public ActionResult GetFileName (string directoryName);
    }
}