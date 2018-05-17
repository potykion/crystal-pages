using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Cury
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public CuryTablLanguage CuryTablLanguage { get; set; }
        [BindProperty] public CuryTablInvariant CuryTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            CuryTablLanguage = await _context.CuryTablLanguage
                .Include(h => h.CuryTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            CuryTablInvariant = CuryTablLanguage.CuryTabl;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var CuryTablLanguageToUpdate = await _context.CuryTablLanguage
                .Include(m => m.CuryTabl)
                .FirstAsync(m => m.Id == id);

            var CuryTablInvariantToUpdate = CuryTablLanguageToUpdate.CuryTabl;

            await TryUpdateModelAsync(
                CuryTablLanguageToUpdate,
                "CuryTablLanguage",
                    m => m.Oboztran            );

            await TryUpdateModelAsync(
                CuryTablInvariantToUpdate,
                "CuryTablInvariant",
m => m.CuryTemp ,m => m.ErrCury , m => m.Bknumber             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}