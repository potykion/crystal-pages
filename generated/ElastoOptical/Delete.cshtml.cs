using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.ElastoOptical
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

        [BindProperty] public EsOpTablLanguage EsOpTablLanguage { get; set; }
        [BindProperty] public EsOpTablInvariant EsOpTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EsOpTablLanguage = await _context.EsOpTablLanguage
                .Include(h => h.EsOpTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            EsOpTablInvariant = EsOpTablLanguage.EsOpTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            EsOpTablLanguage = await _context.EsOpTablLanguage
                .Include(h => h.EsOpTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            EsOpTablInvariant = EsOpTablLanguage.EsOpTabl;

            _context.EsOpTablLanguage.Remove(EsOpTablLanguage);
            _context.EsOpTablInvariant.Remove(EsOpTablInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "ElastoOptical",
                system: _contextUtils.GetSystemUrlByHeadClue(EsOpTablInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}