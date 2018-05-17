using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Solubility
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public SuspTablLanguage SuspTablLanguage { get; set; }
        [BindProperty] public SuspTablInvariant SuspTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SuspTablLanguage = await _context.SuspTablLanguage
                .Include(h => h.SuspTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            SuspTablInvariant = SuspTablLanguage.SuspTabl;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var SuspTablLanguageToUpdate = await _context.SuspTablLanguage
                .Include(m => m.SuspTabl)
                .FirstAsync(m => m.Id == id);

            var SuspTablInvariantToUpdate = SuspTablLanguageToUpdate.SuspTabl;

            await TryUpdateModelAsync(
                SuspTablLanguageToUpdate,
                "SuspTablLanguage",
                    m => m.MethodS,                    m => m.SuspName            );

            await TryUpdateModelAsync(
                SuspTablInvariantToUpdate,
                "SuspTablInvariant",
m => m.Temper ,m => m.Suspense ,m => m.ErrSusp , m => m.Bknumber             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}