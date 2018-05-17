using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Refractive
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public RefrcIndLanguage RefrcIndLanguage { get; set; }
        [BindProperty] public RefrcIndInvariant RefrcIndInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            RefrcIndLanguage = await _context.RefrcIndLanguage
                .Include(h => h.RefrcInd)
                .FirstOrDefaultAsync(m => m.Id == id);

            RefrcIndInvariant = RefrcIndLanguage.RefrcInd;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var RefrcIndLanguageToUpdate = await _context.RefrcIndLanguage
                .Include(m => m.RefrcInd)
                .FirstAsync(m => m.Id == id);

            var RefrcIndInvariantToUpdate = RefrcIndLanguageToUpdate.RefrcInd;

            await TryUpdateModelAsync(
                RefrcIndLanguageToUpdate,
                "RefrcIndLanguage",
                    m => m.MethodIn            );

            await TryUpdateModelAsync(
                RefrcIndInvariantToUpdate,
                "RefrcIndInvariant",
m => m.Temper ,m => m.WaveLeng ,m => m.NazbIndx ,m => m.ZnachInd ,m => m.ErrIndex , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}