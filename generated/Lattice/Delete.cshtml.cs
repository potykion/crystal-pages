using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Lattice
{
    [Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        private readonly CrystalContext _context;
        private readonly UrlBuilder _urlBuilder = new UrlBuilder();
        private readonly ContextUtils _contextUtils;

        public DeleteModel(CrystalContext context)
        {
            _context = context;
            _contextUtils = new ContextUtils(context);
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
            ElemTablLanguage = await _context.ElemTablLanguage
                .Include(h => h.ElemTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            ElemTablInvariant = ElemTablLanguage.ElemTabl;

            _context.ElemTablLanguage.Remove(ElemTablLanguage);
            _context.ElemTablInvariant.Remove(ElemTablInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Lattice",
                system: _contextUtils.GetSystemUrlByHeadClue(ElemTablInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}