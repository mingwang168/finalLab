using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace finalLab
{
    class Models
    {
        public class FoodCartContext : DbContext
        {
            public DbSet<FoodCart> FoodCarts { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                options.UseSqlServer("Data Source=MINGWIN10\\SQLEXPRESS; Database=myDB;Trusted_Connection=True;");
            }
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
}
