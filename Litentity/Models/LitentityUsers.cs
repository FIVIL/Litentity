using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Litentity.Models
{
    public enum LitentityUsersTypes
    {
        Admin,
        TopUser,
        SecondaryUser,
        ThirdBaseUser,
        NormalUser
    }
    public class LitentityUsers
    {
        [Key]
        public Guid ID { get; set; }

        [Required, StringLength(8, MinimumLength = 4)]
        public string UserName { get; set; }

        [MaxLength(32), Required]
        public byte[] PassWordHash { get; set; }

        [EmailAddress, StringLength(50), Required]
        public string EmailAddress { get; set; }
        public bool EmailConfirmed { get; set; }

        [StringLength(20),Required]
        public string PhoneNumber { get; set; }
        public bool PhoneConfirmed { get; set; }

        public LitentityUsersTypes UserType { get; set; }

    }
}
