using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.ElastoOptical
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public EsOpTablLanguage EsOpTablLanguage { get; set; }
        [BindProperty] public EsOpTablInvariant EsOpTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EsOpTablLanguage = await _context.EsOpTablLanguage
                .Include(h => h.EsOpTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            EsOpTablInvariant = EsOpTablLanguage.EsOpTabl;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var EsOpTablLanguageToUpdate = await _context.EsOpTablLanguage
                .Include(m => m.EsOpTabl)
                .FirstAsync(m => m.Id == id);

            var EsOpTablInvariantToUpdate = EsOpTablLanguageToUpdate.EsOpTabl;

            await TryUpdateModelAsync(
                EsOpTablLanguageToUpdate,
                "EsOpTablLanguage",
                    m => m.MethodP,                    m => m.ZnP            );

            await TryUpdateModelAsync(
                EsOpTablInvariantToUpdate,
                "EsOpTablInvariant",
m => m.LengWave ,m => m.P ,m => m.ErrP , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}