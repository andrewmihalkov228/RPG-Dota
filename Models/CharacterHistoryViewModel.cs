namespace RPG_Dota.Models
{
    public class CharacterHistoryViewModel
    {
        public int Id { get; set; }
        public int CharacterId { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public int Level { get; set; }
        public int CharacterClassId { get; set; }
        public DateTime ChangedDate { get; set; }
        public OperationType OperationType { get; set; }
    }
}
