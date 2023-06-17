using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppSoftUni.Infrastructure.Data.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("Content")]
        public string MessageText { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Sender { get; set; } = null!;
    }
}
