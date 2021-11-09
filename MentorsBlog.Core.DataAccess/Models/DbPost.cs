using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorsBlog.Core.DataAccess.Models
{
    public class DbPost : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public ICollection<DbComment> Comments { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        
        [Required]
        [MaxLength(2000)]
        public string Preview { get; set; }
        
        [Required]
        public string Body { get; set; }
        
        [Required]
        public DateTime PublishDate { get; set; }
    }
}