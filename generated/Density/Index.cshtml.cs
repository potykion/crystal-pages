using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Crystal.Utils;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Density
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

        public IList<DensTablLanguage> DensTablLanguage { get; set; }
        
        public IDictionary<int, BibliogrLanguage> References { get; set; }
        

        public async Task OnGetAsync(string systemUrl)
        {
            var headClue = _contextUtils.GetHeadClueBySystemUrl(systemUrl);

            DensTablLanguage = await _context.DensTablLanguage
                .Include(m => m.DensTabl)
                .Where(m => m.DensTabl.HeadClue == headClue)
                .Where(m => m.LanguageId == this.GetLanguageId())
                .ToListAsync();

            
            var bibliogrLanguage = await _context.BibliogrLanguage
                .Include(b => b.Bibliogr)
                .Where(b => b.LanguageId == this.GetLanguageId())
                .ToDictionaryAsync(b => b.BibliogrId, b => b);

            References = DensTablLanguage
                .ToDictionary(h => h.DensTablId, h =>
                    h.DensTabl.Bknumber.HasValue ? bibliogrLanguage[(int) h.DensTabl.Bknumber] : null
                );
            
        }
    }
}