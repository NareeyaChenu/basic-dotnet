
using TodoApi.Interfaces;
using TodoApi.DTOs;
namespace TodoApi.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {

        }


        public IEnumerable<UserModel> GetUsers(UserParam param)
        {
            var users = UserStore.Users;


            if (!string.IsNullOrEmpty(param.Name))
            {
                var name = param.Name!.ToLower();
                users = users.Where(x => x.Name!.ToLower()!.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return users;
        }

        public string ImageProcessor()
        {
            string filePath = "";
            // byte[] byteArray = Convert.FromBase64String(model.Base64EncodedFile!);

            // var type = model.Type!.Split("/");
            // var outputfile = $"{model.Name}.{type[1]}";

            // if (!Directory.Exists("Images"))
            // {
            //     // Directory does not exist, so create it
            //     Directory.CreateDirectory("Images");
            //     Console.WriteLine($"Directory '{"Images"}' created.");
            // }

            // // Create a MemoryStream from the byte array
            // using (var memoryStream = new MemoryStream(byteArray))
            // {
            //     // Combine the folder path and file name to create the full file path
            //     filePath = Path.Combine("Images", outputfile);

            //     // Save the MemoryStream to a file
            //     File.WriteAllBytes(filePath, memoryStream.ToArray());
            //     Console.WriteLine($"Image saved to: {filePath}");

            // }

            return filePath;
        }

    }
}