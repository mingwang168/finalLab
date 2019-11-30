using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace finalLab
{
    public class FoodCartContext : DbContext
    {
        public DbSet<FoodCart> FoodCarts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=MINGWIN10\\SQLEXPRESS; Database= myDB;Trusted_Connection=True;");
    }
    public class FoodCart 
        {
            [Key]
            public string key { get; set; }
            public string status { get; set; }
            public string description { get; set; }
            public string geo_localarea { get; set; }
            public float longitude { get; set; }
            public float latitude { get; set; }

            public string location { get; set; }
            public string vendor_type { get; set; }
            public string business_name { get; set; }
        }

    }
//创建完此models class之后在命令行执行以下三行：
//Install-Package Microsoft.EntityFrameworkCore.Tools
//Add-Migration InitialCreate
//Update-Database
//有关说明见:https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=visual-studio
