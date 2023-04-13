using System.ComponentModel.DataAnnotations.Schema;

namespace VetApp_BE.Feature.Pets.Models
{

    [Table("Pets")]
    public class PetModels
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public string PreferredContactDetails { get; set; }
    }
}
