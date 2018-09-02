using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace FIVIL.Litentity
{
    class LoginViewModel
    {
        [Required, StringLength(8, MinimumLength = 4)]
        public string UserName { get; set; }

        [StringLength(8, MinimumLength = 6), Required]
        public string PassWord { get; set; }

        internal byte[] PassWordHash(string pass)
        {
            byte[] res;
            using (var sha256=SHA256.Create())
            {
                res = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass));
            }
            return res;
        }
    } 
}
