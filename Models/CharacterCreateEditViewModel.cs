using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RPG_Dota.Models
{
    public class CharacterCreateEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя персонажа обязательно для заполнения")]
        [Display(Name = "Имя персонажа")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Здоровье должно быть в диапазоне от 1 до 100")]
        [Display(Name = "Здоровье")]
        public int Health { get; set; }

        [Range(1, 30, ErrorMessage = "Уровень должен быть от 1 до 30")]
        [Display(Name = "Уровень")]
        public int Level { get; set; }

        [Display(Name = "Класс")]
        public int CharacterClassId { get; set; }
    }
}
