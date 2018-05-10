using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Lattice
{
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
        }

        [BindProperty] public ElemTablLanguage ElemTablLanguage { get; set; }
        [BindProperty] public ElemTablInvariant ElemTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ElemTablLanguage = await _context.ElemTablLanguage
                .Include(h => h.ElemTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            ElemTablInvariant = ElemTablLanguage.ElemTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var ElemTablLanguageToUpdate = await _context.ElemTablLanguage
                .Include(m => m.ElemTabl)
                .FirstAsync(m => m.Id == id);

            var ElemTablInvariantToUpdate = ElemTablLanguageToUpdate.ElemTabl;

            await TryUpdateModelAsync(
                ElemTablLanguageToUpdate,
                "ElemTablLanguage",
                    m => m.MethodP,                    m => m.Nazbparam            );

            await TryUpdateModelAsync(
                ElemTablInvariantToUpdate,
                "ElemTablInvariant",
m => m.Znparam ,m => m.Errparam ,m => m.NazvAngl ,m => m.ZnAngle ,m => m.ErrAngl             );

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}