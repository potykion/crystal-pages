using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Elastic
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

        [BindProperty] public Elastic1Language Elastic1Language { get; set; }
        [BindProperty] public Elastic1Invariant Elastic1Invariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Elastic1Language = await _context.Elastic1Language
                .Include(h => h.Elastic1)
                .FirstAsync(m => m.Id == id);

            Elastic1Invariant = Elastic1Language.Elastic1;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Elastic1Language = await _context.Elastic1Language
                .Include(h => h.Elastic1)
                .FirstAsync(m => m.Id == id);

            Elastic1Invariant = Elastic1Language.Elastic1;

            _context.Elastic1Language.Remove(Elastic1Language);
            _context.Elastic1Invariant.Remove(Elastic1Invariant);

            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Elastic",
                system: _contextUtils.GetSystemUrlByHeadClue(Elastic1Invariant.HeadClue)
            );
            return Redirect(url);

        }
    }
}