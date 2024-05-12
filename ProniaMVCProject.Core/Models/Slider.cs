using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Core.Models
{
    public class Slider : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(150)]
        public string Description { get; set; }
        public string? RedirectUrl { get; set; }
        [StringLength(100)]
        public string? ImageUrl { get; set; }
        [Required]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
