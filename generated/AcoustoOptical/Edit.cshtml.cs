using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.AcoustoOptical
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public AcOpTablLanguage AcOpTablLanguage { get; set; }
        [BindProperty] public AcOpTablInvariant AcOpTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AcOpTablLanguage = await _context.AcOpTablLanguage
                .Include(h => h.AcOpTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            AcOpTablInvariant = AcOpTablLanguage.AcOpTabl;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var AcOpTablLanguageToUpdate = await _context.AcOpTablLanguage
                .Include(m => m.AcOpTabl)
                .FirstAsync(m => m.Id == id);

            var AcOpTablInvariantToUpdate = AcOpTablLanguageToUpdate.AcOpTabl;

            await TryUpdateModelAsync(
                AcOpTablLanguageToUpdate,
                "AcOpTablLanguage",
                    m => m.E,                    m => m.Nsv,                    m => m.Uzv            );

            await TryUpdateModelAsync(
                AcOpTablInvariantToUpdate,
                "AcOpTablInvariant",
m => m.WaveLeng ,m => m.Nzv ,m => m.M1 ,m => m.M2 ,m => m.M3 , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}