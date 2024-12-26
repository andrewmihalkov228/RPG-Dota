namespace RPG_Dota.Models
{
    public class CharacterClassHistoryViewModel
    {
        public int Id { get; set; }
        public int CharacterClassId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Strength { get; set; }
        public int Agility { get; set; }
        public DateTime ChangedDate { get; set; }
        public OperationType OperationType { get; set; }
    }
}
