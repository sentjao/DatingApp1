using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int Id{get; set;}
    }
}