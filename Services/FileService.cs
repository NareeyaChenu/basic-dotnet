using TodoApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace TodoApi.Services
{
    public class FileService : ControllerBase, IFileService
    {
        private readonly IWebHostEnvironment _environment;
        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            if (file.Length < 0)
            {
                return BadRequest("file is required");
            }
            // Generate a unique filename (you may want to add more logic here)
            var type = file.ContentType!.Split("/");
            Random random = new Random();
            int randomNumber = random.Next(); // Generates a random integer

            var outputfile = $"{file.FileName}-{randomNumber}.{type[1]}";

            if (!Directory.Exists("Images"))
            {
                // Directory does not exist, so create it
                Directory.CreateDirectory("Images");
                Console.WriteLine($"Directory '{"Images"}' created.");
            }


            var path = Path.Combine("Images", outputfile);

            // Save the file to the server
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(path);


            // Return information about the uploaded file
            return File(fileBytes, file.ContentType, Path.GetFileName(outputfile));
        }
        public ActionResult GetFileName(string directoryName)
        {
            if (!Directory.Exists(directoryName))
            {
                return NotFound($"directory with name {directoryName} does not exist");
            }

            List<string> fileNames = GetFileNames(directoryName);
            return Ok(fileNames);
        }
        static List<string> GetFileNames(string directoryPath)
        {
            // Create a list to store file names
            List<string> fileNames = new List<string>();

            // Get a list of file paths in the specified directory
            string[] files = Directory.GetFiles(directoryPath);

            // Extract file names from file paths
            foreach (var filePath in files)
            {
                // Use Path.GetFileName to get the file name without the full path
                string fileName = Path.GetFileName(filePath);

                // Add the file name to the list
                fileNames.Add(fileName);
            }

            return fileNames;
        }
    }
}