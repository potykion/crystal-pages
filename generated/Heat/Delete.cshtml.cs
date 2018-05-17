using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Heat
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

        [BindProperty] public HeatTablLanguage HeatTablLanguage { get; set; }
        [BindProperty] public HeatTablInvariant HeatTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            HeatTablLanguage = await _context.HeatTablLanguage
                .Include(h => h.HeatTabl)
                .FirstAsync(m => m.Id == id);

            HeatTablInvariant = HeatTablLanguage.HeatTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            HeatTablLanguage = await _context.HeatTablLanguage
                .Include(h => h.HeatTabl)
                .FirstAsync(m => m.Id == id);

            HeatTablInvariant = HeatTablLanguage.HeatTabl;

            _context.HeatTablLanguage.Remove(HeatTablLanguage);
            _context.HeatTablInvariant.Remove(HeatTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Heat",
                system: _contextUtils.GetSystemUrlByHeadClue(HeatTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}