namespace RPG_Dota.Models
{
    public class CharacterClassHistoryListViewModel
    {
        public int? ClassId { get; set; }
        public List<CharacterClassHistoryViewModel> Histories { get; set; }
    }
}
