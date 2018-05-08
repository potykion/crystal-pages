using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Solubility
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

        public IList<SuspTablLanguage> SuspTablLanguage { get; set; }
        
        public IDictionary<int, BibliogrLanguage> References { get; set; }
        
        

        public async Task OnGetAsync(string systemUrl )
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            var substanceSuspTabl = _context.SuspTablLanguage
                .Include(m => m.SuspTabl)
                .Where(m => m.SuspTabl.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId());

            

            SuspTablLanguage = await substanceSuspTabl.ToListAsync();

            

            
            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = SuspTablLanguage
                .ToDictionary(h => h.SuspTablId, h =>
                    h.SuspTabl.Bknumber.HasValue ? bibliogrLanguage[(int) h.SuspTabl.Bknumber] : null
                );
            
        }
    }
}