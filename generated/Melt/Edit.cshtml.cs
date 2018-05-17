using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Melt
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public PlavTablLanguage PlavTablLanguage { get; set; }
        [BindProperty] public PlavTablInvariant PlavTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PlavTablLanguage = await _context.PlavTablLanguage
                .Include(h => h.PlavTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            PlavTablInvariant = PlavTablLanguage.PlavTabl;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var PlavTablLanguageToUpdate = await _context.PlavTablLanguage
                .Include(m => m.PlavTabl)
                .FirstAsync(m => m.Id == id);

            var PlavTablInvariantToUpdate = PlavTablLanguageToUpdate.PlavTabl;

            await TryUpdateModelAsync(
                PlavTablLanguageToUpdate,
                "PlavTablLanguage",
                    m => m.PlavType            );

            await TryUpdateModelAsync(
                PlavTablInvariantToUpdate,
                "PlavTablInvariant",
m => m.PlavTemp ,m => m.ErrPlav , m => m.Bknumber             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}