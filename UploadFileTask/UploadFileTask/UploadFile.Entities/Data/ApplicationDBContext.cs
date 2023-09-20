using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UploadFile.Entities.Models;

namespace UploadFile.Entities.Data
{
    public class ApplicationDBContext: IdentityDbContext<IdentityUser>

    {
        private IConfiguration Configuration;

 

        public ApplicationDBContext(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }



        public ApplicationDBContext(DbContextOptions<DbContext> options)
            : base(options)
        {



        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connString = this.Configuration.GetConnectionString("DBConnection");
                
                optionsBuilder.UseSqlServer(connString);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }

       

    }
}
    

