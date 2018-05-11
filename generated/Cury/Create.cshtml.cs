using System.Threading.Tasks;
using System.Linq;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Cury
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

            return Page();
        }

        [BindProperty] public CuryTablLanguage CuryTablLanguage { get; set; }

        public async Task<IActionResult> OnPostAsync(string systemUrl)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CuryTablLanguage.CuryTabl.HeadClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);
            CuryTablLanguage.LanguageId = this.GetLanguageId();

            _context.CuryTablLanguage.Add(CuryTablLanguage);
            await _context.SaveChangesAsync();

            var url = _urlBuilder.BuildPropertyLink(
                this.GetLanguage(),
                "Cury",
                system: systemUrl
            );
            return Redirect(url);

        }
    }
}