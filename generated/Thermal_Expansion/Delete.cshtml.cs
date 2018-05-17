using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Thermal_Expansion
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

        [BindProperty] public HeatExpnLanguage HeatExpnLanguage { get; set; }
        [BindProperty] public HeatExpnInvariant HeatExpnInvariant { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            HeatExpnLanguage = await _context.HeatExpnLanguage
                .Include(h => h.HeatExpn)
                .FirstOrDefaultAsync(m => m.Id == id);

            HeatExpnInvariant = HeatExpnLanguage.HeatExpn;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            HeatExpnLanguage = await _context.HeatExpnLanguage
                .Include(h => h.HeatExpn)
                .FirstOrDefaultAsync(m => m.Id == id);

            HeatExpnInvariant = HeatExpnLanguage.HeatExpn;

            _context.HeatExpnLanguage.Remove(HeatExpnLanguage);
            _context.HeatExpnInvariant.Remove(HeatExpnInvariant);


            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Thermal_Expansion",
                system: _contextUtils.GetSystemUrlByHeadClue(HeatExpnInvariant.HeadClue)

            );
            return Redirect(url);

        }
    }
}