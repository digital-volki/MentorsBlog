using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorsBlog.Core.DataAccess.Models
{
    public class DbUser : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; }
        
        [Required]
        [MaxLength(64)]
        public string PasswordHash { get; set; }
    }
}