using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElasticApmNetFrameworkSample.Models.DatabaseObjects
{
    public class User : IMappable<DataTransferObjects.Getter.GetUser>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public virtual List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();

        public DataTransferObjects.Getter.GetUser Convert()
        {
            DataTransferObjects.Getter.GetUser damUser = new DataTransferObjects.Getter.GetUser
            {
                Name = this.Name,
                Surname = this.Surname,
                Username = this.Username,
                Password = this.Password
            };
            return damUser;
        }
    }
}