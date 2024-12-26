using Microsoft.AspNetCore.Mvc;
using RPG_Dota.Models;
using Microsoft.EntityFrameworkCore;

namespace RPG_Dota.Controllers
{
    public class CharacterClassesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CharacterClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var characterClasses = from c in _context.CharacterClasses
                                   select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                characterClasses = characterClasses.Where(s => s.Name.Contains(searchString));
            }

            var viewModel = new CharacterClassListViewModel
            {
                Classes = await characterClasses.Select(c => new CharacterClassViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                }).ToListAsync()
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CharacterClassCreateEditViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CharacterClassCreateEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var characterClass = new CharacterClass
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Strength = viewModel.Strength,
                    Agility = viewModel.Agility
                };
                _context.Add(characterClass);
                await _context.SaveChangesAsync();

                _context.CharacterClassHistories.Add(new CharacterClassHistory
                {
                    CharacterClassId = characterClass.Id,
                    Name = characterClass.Name,
                    Description = characterClass.Description,
                    Strength = characterClass.Strength,
                    Agility = characterClass.Agility,
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

            var characterClass = await _context.CharacterClasses.FindAsync(id);
            if (characterClass == null)
            {
                return NotFound();
            }

            var viewModel = new CharacterClassCreateEditViewModel
            {
                Id = characterClass.Id,
                Name = characterClass.Name,
                Description = characterClass.Description,
                Strength = characterClass.Strength,
                Agility = characterClass.Agility
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CharacterClassCreateEditViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var characterClass = await _context.CharacterClasses.FindAsync(id);
                if (characterClass == null)
                {
                    return NotFound();
                }

                _context.CharacterClassHistories.Add(new CharacterClassHistory
                {
                    CharacterClassId = characterClass.Id,
                    Name = characterClass.Name,
                    Description = characterClass.Description,
                    Strength = characterClass.Strength,
                    Agility = characterClass.Agility,
                    ChangedDate = DateTime.Now,
                    OperationType = OperationType.Edit
                });

                characterClass.Name = viewModel.Name;
                characterClass.Description = viewModel.Description;
                characterClass.Strength = viewModel.Strength;
                characterClass.Agility = viewModel.Agility;

                try
                {
                    _context.Update(characterClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterClassExists(characterClass.Id))
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

        private bool CharacterClassExists(int id)
        {
            return (_context.CharacterClasses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterClass = await _context.CharacterClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterClass == null)
            {
                return NotFound();
            }

            var viewModel = new CharacterClassDeleteViewModel
            {
                Id = characterClass.Id,
                Name = characterClass.Name
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var characterClass = await _context.CharacterClasses.FindAsync(id);
            if (characterClass != null)
            {
                _context.CharacterClassHistories.Add(new CharacterClassHistory
                {
                    CharacterClassId = characterClass.Id,
                    Name = characterClass.Name,
                    Description = characterClass.Description,
                    Strength = characterClass.Strength,
                    Agility = characterClass.Agility,
                    ChangedDate = DateTime.Now,
                    OperationType = OperationType.Delete
                });

                _context.CharacterClasses.Remove(characterClass);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characterClass = await _context.CharacterClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterClass == null)
            {
                return NotFound();
            }

            var viewModel = new CharacterClassDetailsViewModel
            {
                Id = characterClass.Id,
                Name = characterClass.Name,
                Description = characterClass.Description,
                Strength = characterClass.Strength,
                Agility = characterClass.Agility
            };

            return View(viewModel);
        }
    }
}
