using System.Linq;
using System.Threading.Tasks;
using Crystal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Pages.Substances.Thermal_Conductivity
{
    public class EditModel : PageModel
    {
        private readonly CrystalContext _context;

        public EditModel(CrystalContext context)
        {
            _context = context;
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
            var HeatExpnLanguageToUpdate = await _context.HeatExpnLanguage
                .Include(m => m.HeatExpn)
                .FirstAsync(m => m.Id == id);

            var HeatExpnInvariantToUpdate = HeatExpnLanguageToUpdate.HeatExpn;

            await TryUpdateModelAsync(
                HeatExpnLanguageToUpdate,
                "HeatExpnLanguage",
                    m => m.MethodEx,                    m => m.Znak1            );

            await TryUpdateModelAsync(
                HeatExpnInvariantToUpdate,
                "HeatExpnInvariant",
m => m.DataType ,m => m.Temper_1 ,m => m.Temper_2 ,m => m.S11 ,m => m.ErrHExp             );

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}