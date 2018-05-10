using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Piezoelectric_Coupling
{
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public MechTablLanguage MechTablLanguage { get; set; }
        [BindProperty] public MechTablInvariant MechTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            MechTablLanguage = await _context.MechTablLanguage
                .Include(h => h.MechTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            MechTablInvariant = MechTablLanguage.MechTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var MechTablLanguageToUpdate = await _context.MechTablLanguage
                .Include(m => m.MechTabl)
                .FirstAsync(m => m.Id == id);

            var MechTablInvariantToUpdate = MechTablLanguageToUpdate.MechTabl;

            await TryUpdateModelAsync(
                MechTablLanguageToUpdate,
                "MechTablLanguage",
                    m => m.MethodK,                    m => m.ZnK            );

            await TryUpdateModelAsync(
                MechTablInvariantToUpdate,
                "MechTablInvariant",
m => m.FreqCons ,m => m.Temper ,m => m.K ,m => m.ErrK             );

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}