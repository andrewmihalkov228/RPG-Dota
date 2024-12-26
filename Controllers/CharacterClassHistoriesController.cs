using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_Dota.Models;

namespace RPG_Dota.Controllers
{
    public class CharacterClassHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharacterClassHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? classId)
        {
            IQueryable<CharacterClassHistory> historiesQuery = _context.CharacterClassHistories;

            if (classId.HasValue)
            {
                historiesQuery = historiesQuery.Where(h => h.CharacterClassId == classId.Value);
            }

            var histories = await historiesQuery.ToListAsync();

            var viewModel = new CharacterClassHistoryListViewModel
            {
                Histories = histories.Select(h => new CharacterClassHistoryViewModel
                {
                    Id = h.Id,
                    CharacterClassId = h.CharacterClassId,
                    Name = h.Name,
                    Description = h.Description,
                    Strength = h.Strength,
                    Agility = h.Agility,
                    ChangedDate = h.ChangedDate,
                    OperationType = h.OperationType
                }).ToList(),
                ClassId = classId
            };

            return View(viewModel);
        }
    }
}
