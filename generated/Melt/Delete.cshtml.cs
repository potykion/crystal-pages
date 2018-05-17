using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Melt
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

        [BindProperty] public PlavTablLanguage PlavTablLanguage { get; set; }
        [BindProperty] public PlavTablInvariant PlavTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PlavTablLanguage = await _context.PlavTablLanguage
                .Include(h => h.PlavTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            PlavTablInvariant = PlavTablLanguage.PlavTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            PlavTablLanguage = await _context.PlavTablLanguage
                .Include(h => h.PlavTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            PlavTablInvariant = PlavTablLanguage.PlavTabl;

            _context.PlavTablLanguage.Remove(PlavTablLanguage);
            _context.PlavTablInvariant.Remove(PlavTablInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Melt",
                system: _contextUtils.GetSystemUrlByHeadClue(PlavTablInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}