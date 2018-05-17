using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Dielectric_Loss
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public DielDissLanguage DielDissLanguage { get; set; }
        [BindProperty] public DielDissInvariant DielDissInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DielDissLanguage = await _context.DielDissLanguage
                .Include(h => h.DielDiss)
                .FirstOrDefaultAsync(m => m.Id == id);

            DielDissInvariant = DielDissLanguage.DielDiss;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var DielDissLanguageToUpdate = await _context.DielDissLanguage
                .Include(m => m.DielDiss)
                .FirstAsync(m => m.Id == id);

            var DielDissInvariantToUpdate = DielDissLanguageToUpdate.DielDiss;

            await TryUpdateModelAsync(
                DielDissLanguageToUpdate,
                "DielDissLanguage",
                    m => m.MethodY,                    m => m.TangName            );

            await TryUpdateModelAsync(
                DielDissInvariantToUpdate,
                "DielDissInvariant",
m => m.FreqDiss ,m => m.Temper_3 ,m => m.TangentD ,m => m.ErrDiss , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}