using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GQL.Database.Entities
{
    [Table("ProjectState")]
    public class ProjectState
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Project")]
        [Column(Order = 1)]
        public int ProjectId { get; set; }

        public Project Project { get; set; }

        [Required]
        [ForeignKey("Contributor")]
        [Column(Order = 2)]
        public int OwnerId { get; set; }

        public Contributor Owner { get; set; }
    }
}