using Microsoft.EntityFrameworkCore;
using ProniaMVCProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Data.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<Feature> Features { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {
             
        }
    }
}
