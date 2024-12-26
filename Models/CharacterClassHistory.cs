using System.ComponentModel.DataAnnotations;

namespace RPG_Dota.Models
{
    public class CharacterClassHistory
    {
        public int Id { get; set; }
        public int CharacterClassId { get; set; }

        [Required]
        [Display(Name = "Название класса")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Сила")]
        public int Strength { get; set; }

        [Display(Name = "Ловкость")]
        public int Agility { get; set; }

        public DateTime ChangedDate { get; set; }

        // Тип операции: 0 - создание, 1 - изменение, 2 - удаление
        public OperationType OperationType { get; set; }
    }

    public enum OperationType
    {
        Create = 0, Edit = 1, Delete = 2
    }
}
