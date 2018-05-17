using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.NonlinearOptical
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public NlOpTablLanguage NlOpTablLanguage { get; set; }
        [BindProperty] public NlOpTablInvariant NlOpTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            NlOpTablLanguage = await _context.NlOpTablLanguage
                .Include(h => h.NlOpTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            NlOpTablInvariant = NlOpTablLanguage.NlOpTabl;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var NlOpTablLanguageToUpdate = await _context.NlOpTablLanguage
                .Include(m => m.NlOpTabl)
                .FirstAsync(m => m.Id == id);

            var NlOpTablInvariantToUpdate = NlOpTablLanguageToUpdate.NlOpTabl;

            await TryUpdateModelAsync(
                NlOpTablLanguageToUpdate,
                "NlOpTablLanguage",
                    m => m.MethodR,                    m => m.ZnR            );

            await TryUpdateModelAsync(
                NlOpTablInvariantToUpdate,
                "NlOpTablInvariant",
m => m.Lyambda ,m => m.Rij ,m => m.ErrRij , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}