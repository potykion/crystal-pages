using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Wave
{
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public DecrTablLanguage DecrTablLanguage { get; set; }
        [BindProperty] public DecrTablInvariant DecrTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DecrTablLanguage = await _context.DecrTablLanguage
                .Include(h => h.DecrTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            DecrTablInvariant = DecrTablLanguage.DecrTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var DecrTablLanguageToUpdate = await _context.DecrTablLanguage
                .Include(m => m.DecrTabl)
                .FirstAsync(m => m.Id == id);

            var DecrTablInvariantToUpdate = DecrTablLanguageToUpdate.DecrTabl;

            await TryUpdateModelAsync(
                DecrTablLanguageToUpdate,
                "DecrTablLanguage",
                    m => m.Nzv,                    m => m.Uzv,                    m => m.WaveType            );

            await TryUpdateModelAsync(
                DecrTablInvariantToUpdate,
                "DecrTablInvariant",
m => m.WaveSpeed ,m => m.Decrement ,m => m.DecrFreq             );

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}