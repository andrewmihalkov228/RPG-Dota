using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RPG_Dota.Models;

namespace RPG_Dota.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var characters = from c in _context.Characters.Include(c => c.CharacterClass)
                             select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                characters = characters.Where(s => s.Name.Contains(searchString));
            }

            var viewModel = new CharacterListViewModel
            {
                Characters = await characters.Select(c => new CharacterViewModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.CharacterClass)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (character == null)
            {
                return NotFound();
            }

            var viewModel = new CharacterDetailsViewModel
            {
                Id = character.Id,
                Name = character.Name,
                Health = character.Health,
                Level = character.Level,
                CharacterClassName = character.CharacterClass.Name
            };

            return View(viewModel);
        }
        public IActionResult Create()
        {
            ViewBag.CharacterClasses = new SelectList(_context.CharacterClasses, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CharacterCreateEditViewModel viewModel)
        {
            ViewBag.CharacterClasses = new SelectList(_context.CharacterClasses, "Id", "Name", viewModel.CharacterClassId);

            if (ModelState.IsValid)
            {
                var character = new Character
                {
                    Name = viewModel.Name,
                    Health = viewModel.Health,
                    Level = viewModel.Level,
                    CharacterClassId = viewModel.CharacterClassId
                };
                _context.Add(character);
                await _context.SaveChangesAsync();

                _context.CharacterHistories.Add(new CharacterHistory
                {
                    CharacterId = character.Id,
                    Name = character.Name,
                    Health = character.Health,
                    Level = character.Level,
                    CharacterClassId = character.CharacterClassId,
                    ChangedDate = DateTime.Now,
                    OperationType = OperationType.Create
                });
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            var viewModel = new CharacterCreateEditViewModel
            {
                Id = character.Id,
                Name = character.Name,
                Health = character.Health,
                Level = character.Level,
                CharacterClassId = character.CharacterClassId,
            };

            ViewBag.CharacterClasses = new SelectList(_context.CharacterClasses, "Id", "Name", character.CharacterClassId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CharacterCreateEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            ViewBag.CharacterClasses = new SelectList(_context.CharacterClasses, "Id", "Name", viewModel.CharacterClassId);

            if (ModelState.IsValid)
            {
                var character = await _context.Characters.FindAsync(id);
                if (character == null)
                {
                    return NotFound();
                }

                character.Name = viewModel.Name;
                character.Health = viewModel.Health;
                character.Level = viewModel.Level;
                character.CharacterClassId = viewModel.CharacterClassId;

                _context.CharacterHistories.Add(new CharacterHistory
                {
                    CharacterId = character.Id,
                    Name = character.Name,
                    Health = character.Health,
                    Level = character.Level,
                    CharacterClassId = character.CharacterClassId,
                    ChangedDate = DateTime.Now,
                    OperationType = OperationType.Edit
                });

                try
                {
                    _context.Update(character);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterExists(character.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters
                .Include(c => c.CharacterClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (character == null)
            {
                return NotFound();
            }

            var viewModel = new CharacterDetailsViewModel
            {
                Id = character.Id,
                Name = character.Name,
                Health = character.Health,
                Level = character.Level,
                CharacterClassName = character.CharacterClass.Name
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var character = await _context.Characters.FindAsync(id);
            if (character != null)
            {
                _context.CharacterHistories.Add(new CharacterHistory
                {
                    CharacterId = character.Id,
                    Name = character.Name,
                    Health = character.Health,
                    Level = character.Level,
                    CharacterClassId = character.CharacterClassId,
                    ChangedDate = DateTime.Now,
                    OperationType = OperationType.Delete
                });

                _context.Characters.Remove(character);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterExists(int id)
        {
            return _context.Characters.Any(e => e.Id == id);
        }
    }
}
