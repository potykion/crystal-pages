using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Dielectric_Loss
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

        [BindProperty] public DielDissLanguage DielDissLanguage { get; set; }
        [BindProperty] public DielDissInvariant DielDissInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DielDissLanguage = await _context.DielDissLanguage
                .Include(h => h.DielDiss)
                .FirstAsync(m => m.Id == id);

            DielDissInvariant = DielDissLanguage.DielDiss;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            DielDissLanguage = await _context.DielDissLanguage
                .Include(h => h.DielDiss)
                .FirstAsync(m => m.Id == id);

            DielDissInvariant = DielDissLanguage.DielDiss;

            _context.DielDissLanguage.Remove(DielDissLanguage);
            _context.DielDissInvariant.Remove(DielDissInvariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Dielectric_Loss",
                system: _contextUtils.GetSystemUrlByHeadClue(DielDissInvariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}