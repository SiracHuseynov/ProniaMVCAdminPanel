using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaMVCProject.Core.Models
{
    public class Feature : BaseEntity
    {
        [Required]
        public string Icon { get; set; } = null!;
        [StringLength(50)]
        public string Title { get; set; }
        public string Description { get; set; }

    }
}


