using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.ElectroOptical
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public ElOpTablLanguage ElOpTablLanguage { get; set; }
        [BindProperty] public ElOpTablInvariant ElOpTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ElOpTablLanguage = await _context.ElOpTablLanguage
                .Include(h => h.ElOpTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            ElOpTablInvariant = ElOpTablLanguage.ElOpTabl;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ElOpTablLanguageToUpdate = await _context.ElOpTablLanguage
                .Include(m => m.ElOpTabl)
                .FirstAsync(m => m.Id == id);

            var ElOpTablInvariantToUpdate = ElOpTablLanguageToUpdate.ElOpTabl;

            await TryUpdateModelAsync(
                ElOpTablLanguageToUpdate,
                "ElOpTablLanguage",
                    m => m.MethodR,                    m => m.ZnN1            );

            await TryUpdateModelAsync(
                ElOpTablInvariantToUpdate,
                "ElOpTablInvariant",
m => m.WvLeng ,m => m.R ,m => m.ErrR , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}