using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Sellmeier
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public ConstSelLanguage ConstSelLanguage { get; set; }
        [BindProperty] public ConstSelInvariant ConstSelInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ConstSelLanguage = await _context.ConstSelLanguage
                .Include(h => h.ConstSel)
                .FirstOrDefaultAsync(m => m.Id == id);

            ConstSelInvariant = ConstSelLanguage.ConstSel;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ConstSelLanguageToUpdate = await _context.ConstSelLanguage
                .Include(m => m.ConstSel)
                .FirstAsync(m => m.Id == id);

            var ConstSelInvariantToUpdate = ConstSelLanguageToUpdate.ConstSel;

            await TryUpdateModelAsync(
                ConstSelLanguageToUpdate,
                "ConstSelLanguage",
                    m => m.Measure            );

            await TryUpdateModelAsync(
                ConstSelInvariantToUpdate,
                "ConstSelInvariant",
m => m.Equation ,m => m.NazvSel ,m => m.ZnachSel , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}