using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Heat
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public HeatTablLanguage HeatTablLanguage { get; set; }
        [BindProperty] public HeatTablInvariant HeatTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            HeatTablLanguage = await _context.HeatTablLanguage
                .Include(h => h.HeatTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            HeatTablInvariant = HeatTablLanguage.HeatTabl;


            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var HeatTablLanguageToUpdate = await _context.HeatTablLanguage
                .Include(m => m.HeatTabl)
                .FirstAsync(m => m.Id == id);

            var HeatTablInvariantToUpdate = HeatTablLanguageToUpdate.HeatTabl;

            await TryUpdateModelAsync(
                HeatTablLanguageToUpdate,
                "HeatTablLanguage",
                    m => m.MethodC            );

            await TryUpdateModelAsync(
                HeatTablInvariantToUpdate,
                "HeatTablInvariant",
m => m.Temperat ,m => m.ZnC ,m => m.C ,m => m.ErrC , m => m.Bknumber             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}