using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Piezoelectric
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public PzElTablLanguage PzElTablLanguage { get; set; }
        [BindProperty] public PzElTablInvariant PzElTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PzElTablLanguage = await _context.PzElTablLanguage
                .Include(h => h.PzElTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            PzElTablInvariant = PzElTablLanguage.PzElTabl;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var PzElTablLanguageToUpdate = await _context.PzElTablLanguage
                .Include(m => m.PzElTabl)
                .FirstAsync(m => m.Id == id);

            var PzElTablInvariantToUpdate = PzElTablLanguageToUpdate.PzElTabl;

            await TryUpdateModelAsync(
                PzElTablLanguageToUpdate,
                "PzElTablLanguage",
                    m => m.MethodPz            );

            await TryUpdateModelAsync(
                PzElTablInvariantToUpdate,
                "PzElTablInvariant",
m => m.FreqPzEl ,m => m.ConstD ,m => m.D ,m => m.ErrD , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}