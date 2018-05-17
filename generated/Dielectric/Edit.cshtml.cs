using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Dielectric
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public DielectrLanguage DielectrLanguage { get; set; }
        [BindProperty] public DielectrInvariant DielectrInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DielectrLanguage = await _context.DielectrLanguage
                .Include(h => h.Dielectr)
                .FirstOrDefaultAsync(m => m.Id == id);

            DielectrInvariant = DielectrLanguage.Dielectr;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var DielectrLanguageToUpdate = await _context.DielectrLanguage
                .Include(m => m.Dielectr)
                .FirstAsync(m => m.Id == id);

            var DielectrInvariantToUpdate = DielectrLanguageToUpdate.Dielectr;

            await TryUpdateModelAsync(
                DielectrLanguageToUpdate,
                "DielectrLanguage",
                    m => m.MethodY,                    m => m.Znak            );

            await TryUpdateModelAsync(
                DielectrInvariantToUpdate,
                "DielectrInvariant",
m => m.FreqDiel ,m => m.Temper_2 ,m => m.Constant ,m => m.Diel ,m => m.ErrY , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}