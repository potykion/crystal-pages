using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Piezoelectric
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

        [BindProperty] public PzElTablLanguage PzElTablLanguage { get; set; }
        [BindProperty] public PzElTablInvariant PzElTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            PzElTablLanguage = await _context.PzElTablLanguage
                .Include(h => h.PzElTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            PzElTablInvariant = PzElTablLanguage.PzElTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            PzElTablLanguage = await _context.PzElTablLanguage
                .Include(h => h.PzElTabl)
                .FirstOrDefaultAsync(m => m.Id == id);

            PzElTablInvariant = PzElTablLanguage.PzElTabl;

            _context.PzElTablLanguage.Remove(PzElTablLanguage);
            _context.PzElTablInvariant.Remove(PzElTablInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Piezoelectric",
                system: _contextUtils.GetSystemUrlByHeadClue(PzElTablInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}