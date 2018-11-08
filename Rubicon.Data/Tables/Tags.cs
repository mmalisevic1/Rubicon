using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rubicon.Data.Tables
{
    public class Tags
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long BlogPostId { get; set; }

        [MaxLength(200)]
        [Required]
        public string Tag { get; set; }

        [ForeignKey(nameof(BlogPostId))]
        public BlogPosts BlogPost { get; set; }
    }
}
