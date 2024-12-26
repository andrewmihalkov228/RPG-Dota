using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_Dota.Models;

namespace RPG_Dota.Controllers
{
    public class CharacterHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharacterHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? characterId)
        {
            IQueryable<CharacterHistory> historiesQuery = _context.CharacterHistories;

            if (characterId.HasValue)
            {
                historiesQuery = historiesQuery.Where(h => h.CharacterId == characterId.Value);
            }

            var histories = await historiesQuery.ToListAsync();

            var viewModel = new CharacterHistoryListViewModel
            {
                Histories = histories.Select(h => new CharacterHistoryViewModel
                {
                    Id = h.Id,
                    CharacterId = h.CharacterId,
                    Name = h.Name,
                    Health = h.Health,
                    Level = h.Level,
                    CharacterClassId = h.CharacterClassId,
                    ChangedDate = h.ChangedDate,
                    OperationType = h.OperationType
                }).ToList(),
                CharacterId = characterId
            };

            return View(viewModel);
        }
    }
}
