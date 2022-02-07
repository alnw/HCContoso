// © Microsoft Corporation. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GQL.Database.Entities
{
    [Table("TeamRole")]
    public class TeamRole
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Team")]
        [Column(Order = 2)]
        public int TeamId { get; set; }

        public Team Team { get; set; }

        [ForeignKey("Contributor")]
        [Column(Order = 3)]
        public int MemberId { get; set; }

        public Contributor Member { get; set; }
    }
}
