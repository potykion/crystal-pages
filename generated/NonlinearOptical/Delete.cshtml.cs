using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.NonlinearOptical
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

        [BindProperty] public NlOpTablLanguage NlOpTablLanguage { get; set; }
        [BindProperty] public NlOpTablInvariant NlOpTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            NlOpTablLanguage = await _context.NlOpTablLanguage
                .Include(h => h.NlOpTabl)
                .FirstAsync(m => m.Id == id);

            NlOpTablInvariant = NlOpTablLanguage.NlOpTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            NlOpTablLanguage = await _context.NlOpTablLanguage
                .Include(h => h.NlOpTabl)
                .FirstAsync(m => m.Id == id);

            NlOpTablInvariant = NlOpTablLanguage.NlOpTabl;

            _context.NlOpTablLanguage.Remove(NlOpTablLanguage);
            _context.NlOpTablInvariant.Remove(NlOpTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "NonlinearOptical",
                system: _contextUtils.GetSystemUrlByHeadClue(NlOpTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}