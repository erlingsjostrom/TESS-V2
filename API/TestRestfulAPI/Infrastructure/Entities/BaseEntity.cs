using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestRestfulAPI.Infrastructure.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Column("Id", Order = 1)]
        public int Id { get; set; }
        [ReadOnly(true)]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
