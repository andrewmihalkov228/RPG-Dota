using System.ComponentModel.DataAnnotations;

namespace RPG_Dota.Models
{
    public class CharacterClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название класса обязательно для заполнения")]
        [Display(Name = "Название класса")]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Описание должно быть от 3 до 50 символов")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Range(1, 100, ErrorMessage = "Сила должна быть в диапазоне от 1 до 100")]
        [Display(Name = "Сила")]
        public int Strength { get; set; }

        [Range(1, 100, ErrorMessage = "Ловкость должна быть в диапазоне от 1 до 100")]
        [Display(Name = "Ловкость")]
        public int Agility { get; set; }
    }
}
