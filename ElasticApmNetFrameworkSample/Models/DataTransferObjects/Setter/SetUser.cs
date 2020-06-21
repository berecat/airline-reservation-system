namespace ElasticApmNetFrameworkSample.Models.DataTransferObjects.Setter
{
    public class SetUser : IMappable<DatabaseObjects.User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DatabaseObjects.User Convert()
        {
            DatabaseObjects.User user = new DatabaseObjects.User
            {
                Username = this.Username,
                Password = this.Password,
                Name = this.Name,
                Surname = this.Surname
            };
            return user;
        }
    }
}