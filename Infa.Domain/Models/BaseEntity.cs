using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infa.Domain.Models
{
    public class BaseEntity
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string CreateAt { get; set; }

        public string? EditedAt { get; set; }

        public bool IsDeleted { get; set; }

        public long Code { get; set; }
    }
}
