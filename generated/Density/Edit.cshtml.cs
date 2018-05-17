using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Density
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public DensTablLanguage DensTablLanguage { get; set; }
        [BindProperty] public DensTablInvariant DensTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DensTablLanguage = await _context.DensTablLanguage
                .Include(h => h.DensTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            DensTablInvariant = DensTablLanguage.DensTabl;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var DensTablLanguageToUpdate = await _context.DensTablLanguage
                .Include(m => m.DensTabl)
                .FirstAsync(m => m.Id == id);

            var DensTablInvariantToUpdate = DensTablLanguageToUpdate.DensTabl;

            await TryUpdateModelAsync(
                DensTablLanguageToUpdate,
                "DensTablLanguage",
                    m => m.MethodD            );

            await TryUpdateModelAsync(
                DensTablInvariantToUpdate,
                "DensTablInvariant",
m => m.Density ,m => m.ErrDens , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}