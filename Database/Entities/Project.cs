using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GQL.Database.Entities
{
    [Table("Project")]
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [ForeignKey("Team")]
        [Column(Order = 1)]
        public int TeamId { get; set; }

        public Team Team { get; set; } = null;
    }
}