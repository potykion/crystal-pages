using System.Threading.Tasks;
using System.Linq;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Thermal_Conductivity
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly CrystalContext _context;
        private readonly ContextUtils _contextUtils;
        private readonly UrlBuilder _urlBuilder = new UrlBuilder();

        public CreateModel(CrystalContext context)
        {
            _context = context;
            _contextUtils = new ContextUtils(context);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var singCodes = await _context.SingTabl.Select(s => s.SingType).Distinct().ToListAsync();
            ViewData["SingCode"] = new SelectList(singCodes);

            return Page();
        }

        [BindProperty] public HeatExpnLanguage HeatExpnLanguage { get; set; }

        public async Task<IActionResult> OnPostAsync(string systemUrl)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HeatExpnLanguage.HeatExpn.HeadClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);
            HeatExpnLanguage.LanguageId = this.GetLanguageId();

            _context.HeatExpnLanguage.Add(HeatExpnLanguage);
            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Thermal_Conductivity",
                system: systemUrl
            );
            return Redirect(url);

        }
    }
}