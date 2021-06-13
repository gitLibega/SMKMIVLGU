using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMKMIVLGU.Models

{
   
    public class User :IdentityUser    {      
        
        public string FIO { get; set; }

        public string Login{ get; set; }
        public string Password { get; set; }

        public string Department { get; set; }
    }
}