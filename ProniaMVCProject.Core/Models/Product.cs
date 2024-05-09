using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Core.Models;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageURL { get; set; } = null!;
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Tag> Tags { get; set; }

        
}
