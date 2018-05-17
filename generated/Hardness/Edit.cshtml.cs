using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Hardness
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public HardTablLanguage HardTablLanguage { get; set; }
        [BindProperty] public HardTablInvariant HardTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            HardTablLanguage = await _context.HardTablLanguage
                .Include(h => h.HardTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            HardTablInvariant = HardTablLanguage.HardTabl;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var HardTablLanguageToUpdate = await _context.HardTablLanguage
                .Include(m => m.HardTabl)
                .FirstAsync(m => m.Id == id);

            var HardTablInvariantToUpdate = HardTablLanguageToUpdate.HardTabl;

            await TryUpdateModelAsync(
                HardTablLanguageToUpdate,
                "HardTablLanguage",
                    m => m.MethodH            );

            await TryUpdateModelAsync(
                HardTablInvariantToUpdate,
                "HardTablInvariant",
m => m.Hard1 ,m => m.Hard2 ,m => m.ErrHard ,m => m.Mohs ,m => m.ErrMohs , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}