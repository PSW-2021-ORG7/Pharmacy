using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Model
{
    [Table("User")]
    public class User
    {
        public enum UserRole
        {
            Client,
            Pharmacist
        }

        [Key]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public String FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public String LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public String Username { get; set; }

        [Required]
        public String Password { get; set; }
        public UserRole Role { get; set; }

        public bool IsLogicalDeleted { get; set; }
        public bool IsBlocked { get; set; }

        public User()
        {
            UserId = new Guid();
            IsLogicalDeleted = false;
            IsBlocked = false;
        }
    }
}
