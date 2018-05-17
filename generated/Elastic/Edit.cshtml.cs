using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Elastic
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public Elastic1Language Elastic1Language { get; set; }
        [BindProperty] public Elastic1Invariant Elastic1Invariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Elastic1Language = await _context.Elastic1Language
                .Include(h => h.Elastic1)
                .FirstOrDefaultAsync(m => m.Id == id);

            Elastic1Invariant = Elastic1Language.Elastic1;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var Elastic1LanguageToUpdate = await _context.Elastic1Language
                .Include(m => m.Elastic1)
                .FirstAsync(m => m.Id == id);

            var Elastic1InvariantToUpdate = Elastic1LanguageToUpdate.Elastic1;

            await TryUpdateModelAsync(
                Elastic1LanguageToUpdate,
                "Elastic1Language",
                    m => m.MethodE,                    m => m.ZnE            );

            await TryUpdateModelAsync(
                Elastic1InvariantToUpdate,
                "Elastic1Invariant",
m => m.TemperEl ,m => m.CondClu1 ,m => m.E1 ,m => m.ErrE , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}