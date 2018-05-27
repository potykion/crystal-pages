using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Lattice
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public ElemTablNewLanguage ElemTablNewLanguage { get; set; }
        [BindProperty] public ElemTablNewInvariant ElemTablNewInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ElemTablNewLanguage = await _context.ElemTablNewLanguage
                .Include(h => h.ElemTablNew)
                .FirstOrDefaultAsync(m => m.Id == id);

            ElemTablNewInvariant = ElemTablNewLanguage.ElemTablNew;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ElemTablNewLanguageToUpdate = await _context.ElemTablNewLanguage
                .Include(m => m.ElemTablNew)
                .FirstAsync(m => m.Id == id);

            var ElemTablNewInvariantToUpdate = ElemTablNewLanguageToUpdate.ElemTablNew;

            await TryUpdateModelAsync(
                ElemTablNewLanguageToUpdate,
                "ElemTablNewLanguage",
                    m => m.MethodP            );

            await TryUpdateModelAsync(
                ElemTablNewInvariantToUpdate,
                "ElemTablNewInvariant",
m => m.A ,m => m.B ,m => m.C ,m => m.Alpha ,m => m.Beta ,m => m.Gamma , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}