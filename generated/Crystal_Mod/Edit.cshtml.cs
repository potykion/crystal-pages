using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Crystal_Mod
{
    [Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public ModfTablLanguage ModfTablLanguage { get; set; }
        [BindProperty] public ModfTablInvariant ModfTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ModfTablLanguage = await _context.ModfTablLanguage
                .Include(h => h.ModfTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            ModfTablInvariant = ModfTablLanguage.ModfTabl;

            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ModfTablLanguageToUpdate = await _context.ModfTablLanguage
                .Include(m => m.ModfTabl)
                .FirstAsync(m => m.Id == id);

            var ModfTablInvariantToUpdate = ModfTablLanguageToUpdate.ModfTabl;

            await TryUpdateModelAsync(
                ModfTablLanguageToUpdate,
                "ModfTablLanguage",
                    m => m.SpaceGrp            );

            await TryUpdateModelAsync(
                ModfTablInvariantToUpdate,
                "ModfTablInvariant",
m => m.SuprTemp ,m => m.SP2 ,m => m.ErrSupr ,m => m.PointGrp ,m => m.Z , m => m.Bknumber , m => m.SingCode             );

            await _context.SaveChangesAsync();

            return Page();
        }
    }
}