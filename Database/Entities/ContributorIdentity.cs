using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GQL.Database.Entities
{
    [Table("ContributorIdentity")]
    public class ContributorIdentity
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(200)]
        public string Identity { get; set; }

        [ForeignKey("Contributor")]
        [Column(Order = 2)]
        public int ContributorId { get; set; }

        public Contributor Contributor { get; set; }
    }
}