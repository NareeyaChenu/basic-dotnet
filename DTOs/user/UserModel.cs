namespace TodoApi.DTOs
{
    public class UserModel
    {
        public UserModel()
        {
        }

        public int Id {get; set;}

        public string? Name {get; set;}
    }


    public static class UserStore
    {
        private static List<UserModel> users = new List<UserModel>
        {
            new UserModel
            {
                Id = 1,
                Name = "Nareeya"
            }
        };

        public static List<UserModel> Users { get => users; set => users = value; }
    }
}