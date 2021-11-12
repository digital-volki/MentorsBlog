using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentorsBlog.Core.DataAccess.Models
{
    public class DbComment : BaseEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [ForeignKey(nameof(PostId))]
        [Required]
        public Guid PostId { get; set; }
        [Required]
        public DbPost Post { get; set; }
        
        [ForeignKey(nameof(ParentId))]
        public Guid? ParentId { get; set; }
        public virtual DbComment Parent { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nickname { get; set; }
        
        [Required]
        [MaxLength(1000-7)]
        public string Message { get; set; }
    }
}