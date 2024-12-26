using System.ComponentModel.DataAnnotations;

namespace RPG_Dota.Models
{
    public class CharacterHistory
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }

        [Required]
        [Display(Name = "Имя персонажа")]
        public string Name { get; set; }

        [Display(Name = "Здоровье")]
        public int Health { get; set; }

        [Display(Name = "Уровень")]
        public int Level { get; set; }

        [Display(Name = "Класс")]
        public int CharacterClassId { get; set; }

        public DateTime ChangedDate { get; set; }

        // Тип операции: 0 - создание, 1 - изменение, 2 - удаление.
        public OperationType OperationType { get; set; }
    }
}
