using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Hardness
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

        [BindProperty] public HardTablLanguage HardTablLanguage { get; set; }
        [BindProperty] public HardTablInvariant HardTablInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            HardTablLanguage = await _context.HardTablLanguage
                .Include(h => h.HardTabl)
                .FirstAsync(m => m.Id == id);

            HardTablInvariant = HardTablLanguage.HardTabl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            HardTablLanguage = await _context.HardTablLanguage
                .Include(h => h.HardTabl)
                .FirstAsync(m => m.Id == id);

            HardTablInvariant = HardTablLanguage.HardTabl;

            _context.HardTablLanguage.Remove(HardTablLanguage);
            _context.HardTablInvariant.Remove(HardTablInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Hardness",
                system: _contextUtils.GetSystemUrlByHeadClue(HardTablInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}