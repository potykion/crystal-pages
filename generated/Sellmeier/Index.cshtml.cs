using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Sellmeier
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

        public IList<ConstSelLanguage> ConstSelLanguage { get; set; }
        
        public IDictionary<int, BibliogrLanguage> References { get; set; }
        

        public async Task OnGetAsync(string systemUrl)
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            ConstSelLanguage = await _context.ConstSelLanguage
                .Include(m => m.ConstSel)
                .Where(m => m.ConstSel.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId())
                .ToListAsync();

            
            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = ConstSelLanguage
                .ToDictionary(h => h.ConstSelId, h =>
                    h.ConstSel.Bknumber.HasValue ? bibliogrLanguage[(int) h.ConstSel.Bknumber] : null
                );
            
        }
    }
}