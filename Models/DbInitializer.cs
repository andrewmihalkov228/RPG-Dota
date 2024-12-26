namespace RPG_Dota.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.CharacterClasses.Any())
            {
                return;
            }

            var classes = new CharacterClass[]
            {
                new CharacterClass { Name = "Cиловик", Description = "Силён, но недостаточно гибок", Strength = 80, Agility = 40 },
                new CharacterClass { Name = "Ловкач", Description = "Гибок и проворлив, но силёнок маловато", Strength = 30, Agility = 85 },
                new CharacterClass { Name = "Маг", Description = "Имеет баланс Вселенной", Strength = 50, Agility = 50 }
            };

            foreach (CharacterClass c in classes)
            {
                context.CharacterClasses.Add(c);
            }
            context.SaveChanges();

            if (context.Characters.Any())
            {
                return;
            }

            var characters = new Character[]
            {
                new Character { Name = "Пудж", Health = 100, Level = 7, CharacterClassId = 1 },
                new Character { Name = "Ассасин", Health = 60, Level = 15, CharacterClassId = 2 },
                new Character { Name = "Оракул", Health = 70, Level = 30, CharacterClassId = 3 }
            };

            foreach (Character p in characters)
            {
                context.Characters.Add(p);
            }
            context.SaveChanges();
        }
    }
}
