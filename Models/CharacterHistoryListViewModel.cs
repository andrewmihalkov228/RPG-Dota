namespace RPG_Dota.Models
{
    public class CharacterHistoryListViewModel
    {
        public int? CharacterId { get; set; }
        public List<CharacterHistoryViewModel> Histories { get; set; }
    }
}
