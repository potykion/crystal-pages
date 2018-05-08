using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Dielectric
{
    public class IndexModel : PageModel
    {
        private readonly CrystalContext _context;
        private readonly ContextUtils _contextUtils;

        public IndexModel(CrystalContext context)
        {
            _context = context;
            _contextUtils = new ContextUtils(_context);
        }

        public IList<DielectrLanguage> DielectrLanguage { get; set; }
        
        public IDictionary<int, BibliogrLanguage> References { get; set; }
        

        public async Task OnGetAsync(string systemUrl)
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            DielectrLanguage = await _context.DielectrLanguage
                .Include(m => m.Dielectr)
                .Where(m => m.Dielectr.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId())
                .ToListAsync();

            
            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = DielectrLanguage
                .ToDictionary(h => h.DielectrId, h =>
                    h.Dielectr.Bknumber.HasValue ? bibliogrLanguage[(int) h.Dielectr.Bknumber] : null
                );
            
        }
    }
}