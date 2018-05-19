using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Crystal.Models
{
    public class SearchRequestProcessor
    {
        private readonly CrystalContext _context;

        public SearchRequestProcessor(CrystalContext context)
        {
            _context = context;
        }

        public async Task<IList<int>> Process(SearchRequest searchRequest)
        {
            IEnumerable<int> matchingHeadClues = await _context.HeadTablInvariant
                .Select(h => h.HeadClue)
                .ToListAsync();

            var filters = await CollectFilters(searchRequest);
            filters.ForEach(filter => { matchingHeadClues = matchingHeadClues.Intersect(filter); });

            return matchingHeadClues.ToList();
        }

        private async Task<List<List<int>>> CollectFilters(SearchRequest searchRequest)
        {
            var filters = new List<List<int>>();


            IQueryable<AcOpTablLanguage> matchedAcOpTabl = _context.AcOpTablLanguage
                .Include(m => m.AcOpTabl);

            var isAcOpTablDataFilled = false;
            if (searchRequest.AcOpTablMin.AcOpTabl.M1.HasValue)
            {
                matchedAcOpTabl = matchedAcOpTabl.Where(m => m.AcOpTabl.M1 >= searchRequest.AcOpTablMin.AcOpTabl.M1);
                isAcOpTablDataFilled = true;
            }
            if (searchRequest.AcOpTablMax.AcOpTabl.M1.HasValue)
            {
                matchedAcOpTabl = matchedAcOpTabl.Where(m => m.AcOpTabl.M1 <= searchRequest.AcOpTablMax.AcOpTabl.M1);
                isAcOpTablDataFilled = true;
            }
            if (searchRequest.AcOpTablMin.AcOpTabl.M2.HasValue)
            {
                matchedAcOpTabl = matchedAcOpTabl.Where(m => m.AcOpTabl.M2 >= searchRequest.AcOpTablMin.AcOpTabl.M2);
                isAcOpTablDataFilled = true;
            }
            if (searchRequest.AcOpTablMax.AcOpTabl.M2.HasValue)
            {
                matchedAcOpTabl = matchedAcOpTabl.Where(m => m.AcOpTabl.M2 <= searchRequest.AcOpTablMax.AcOpTabl.M2);
                isAcOpTablDataFilled = true;
            }
            if (searchRequest.AcOpTablMin.AcOpTabl.M3.HasValue)
            {
                matchedAcOpTabl = matchedAcOpTabl.Where(m => m.AcOpTabl.M3 >= searchRequest.AcOpTablMin.AcOpTabl.M3);
                isAcOpTablDataFilled = true;
            }
            if (searchRequest.AcOpTablMax.AcOpTabl.M3.HasValue)
            {
                matchedAcOpTabl = matchedAcOpTabl.Where(m => m.AcOpTabl.M3 <= searchRequest.AcOpTablMax.AcOpTabl.M3);
                isAcOpTablDataFilled = true;
            }

            if (isAcOpTablDataFilled)
            {
                var matchedAcOpTablHeadClue = await matchedAcOpTabl
                    .Select(m => m.AcOpTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedAcOpTablHeadClue);
            }

            IQueryable<CuryTablLanguage> matchedCuryTabl = _context.CuryTablLanguage
                .Include(m => m.CuryTabl);

            var isCuryTablDataFilled = false;
            if (searchRequest.CuryTablMin.CuryTabl.CuryTemp.HasValue)
            {
                matchedCuryTabl = matchedCuryTabl.Where(m => m.CuryTabl.CuryTemp >= searchRequest.CuryTablMin.CuryTabl.CuryTemp);
                isCuryTablDataFilled = true;
            }
            if (searchRequest.CuryTablMax.CuryTabl.CuryTemp.HasValue)
            {
                matchedCuryTabl = matchedCuryTabl.Where(m => m.CuryTabl.CuryTemp <= searchRequest.CuryTablMax.CuryTabl.CuryTemp);
                isCuryTablDataFilled = true;
            }

            if (isCuryTablDataFilled)
            {
                var matchedCuryTablHeadClue = await matchedCuryTabl
                    .Select(m => m.CuryTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedCuryTablHeadClue);
            }

            IQueryable<DecrTablLanguage> matchedDecrTabl = _context.DecrTablLanguage
                .Include(m => m.DecrTabl);

            var isDecrTablDataFilled = false;
            if (searchRequest.DecrTablMin.DecrTabl.Decrement.HasValue)
            {
                matchedDecrTabl = matchedDecrTabl.Where(m => m.DecrTabl.Decrement >= searchRequest.DecrTablMin.DecrTabl.Decrement);
                isDecrTablDataFilled = true;
            }
            if (searchRequest.DecrTablMax.DecrTabl.Decrement.HasValue)
            {
                matchedDecrTabl = matchedDecrTabl.Where(m => m.DecrTabl.Decrement <= searchRequest.DecrTablMax.DecrTabl.Decrement);
                isDecrTablDataFilled = true;
            }
            if (searchRequest.DecrTablMin.DecrTabl.WaveSpeed.HasValue)
            {
                matchedDecrTabl = matchedDecrTabl.Where(m => m.DecrTabl.WaveSpeed >= searchRequest.DecrTablMin.DecrTabl.WaveSpeed);
                isDecrTablDataFilled = true;
            }
            if (searchRequest.DecrTablMax.DecrTabl.WaveSpeed.HasValue)
            {
                matchedDecrTabl = matchedDecrTabl.Where(m => m.DecrTabl.WaveSpeed <= searchRequest.DecrTablMax.DecrTabl.WaveSpeed);
                isDecrTablDataFilled = true;
            }

            if (isDecrTablDataFilled)
            {
                var matchedDecrTablHeadClue = await matchedDecrTabl
                    .Select(m => m.DecrTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedDecrTablHeadClue);
            }

            IQueryable<DensTablLanguage> matchedDensTabl = _context.DensTablLanguage
                .Include(m => m.DensTabl);

            var isDensTablDataFilled = false;
            if (searchRequest.DensTablMin.DensTabl.Density.HasValue)
            {
                matchedDensTabl = matchedDensTabl.Where(m => m.DensTabl.Density >= searchRequest.DensTablMin.DensTabl.Density);
                isDensTablDataFilled = true;
            }
            if (searchRequest.DensTablMax.DensTabl.Density.HasValue)
            {
                matchedDensTabl = matchedDensTabl.Where(m => m.DensTabl.Density <= searchRequest.DensTablMax.DensTabl.Density);
                isDensTablDataFilled = true;
            }

            if (isDensTablDataFilled)
            {
                var matchedDensTablHeadClue = await matchedDensTabl
                    .Select(m => m.DensTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedDensTablHeadClue);
            }

            IQueryable<DielectrLanguage> matchedDielectr = _context.DielectrLanguage
                .Include(m => m.Dielectr);

            var isDielectrDataFilled = false;
            if (searchRequest.DielectrMin.Dielectr.Diel.HasValue)
            {
                matchedDielectr = matchedDielectr.Where(m => m.Dielectr.Diel >= searchRequest.DielectrMin.Dielectr.Diel);
                isDielectrDataFilled = true;
            }
            if (searchRequest.DielectrMax.Dielectr.Diel.HasValue)
            {
                matchedDielectr = matchedDielectr.Where(m => m.Dielectr.Diel <= searchRequest.DielectrMax.Dielectr.Diel);
                isDielectrDataFilled = true;
            }

            if (isDielectrDataFilled)
            {
                var matchedDielectrHeadClue = await matchedDielectr
                    .Select(m => m.Dielectr.HeadClue)
                    .ToListAsync();
                filters.Add(matchedDielectrHeadClue);
            }

            IQueryable<SuspTablLanguage> matchedSuspTabl = _context.SuspTablLanguage
                .Include(m => m.SuspTabl);

            var isSuspTablDataFilled = false;
            if (searchRequest.SuspTablMin.SuspTabl.Temper.HasValue)
            {
                matchedSuspTabl = matchedSuspTabl.Where(m => m.SuspTabl.Temper >= searchRequest.SuspTablMin.SuspTabl.Temper);
                isSuspTablDataFilled = true;
            }
            if (searchRequest.SuspTablMax.SuspTabl.Temper.HasValue)
            {
                matchedSuspTabl = matchedSuspTabl.Where(m => m.SuspTabl.Temper <= searchRequest.SuspTablMax.SuspTabl.Temper);
                isSuspTablDataFilled = true;
            }
            if (searchRequest.SuspTablMin.SuspTabl.Suspense.HasValue)
            {
                matchedSuspTabl = matchedSuspTabl.Where(m => m.SuspTabl.Suspense >= searchRequest.SuspTablMin.SuspTabl.Suspense);
                isSuspTablDataFilled = true;
            }
            if (searchRequest.SuspTablMax.SuspTabl.Suspense.HasValue)
            {
                matchedSuspTabl = matchedSuspTabl.Where(m => m.SuspTabl.Suspense <= searchRequest.SuspTablMax.SuspTabl.Suspense);
                isSuspTablDataFilled = true;
            }

            if (isSuspTablDataFilled)
            {
                var matchedSuspTablHeadClue = await matchedSuspTabl
                    .Select(m => m.SuspTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedSuspTablHeadClue);
            }

            IQueryable<ElOpTablLanguage> matchedElOpTabl = _context.ElOpTablLanguage
                .Include(m => m.ElOpTabl);

            var isElOpTablDataFilled = false;
            if (searchRequest.ElOpTablMin.ElOpTabl.R.HasValue)
            {
                matchedElOpTabl = matchedElOpTabl.Where(m => m.ElOpTabl.R >= searchRequest.ElOpTablMin.ElOpTabl.R);
                isElOpTablDataFilled = true;
            }
            if (searchRequest.ElOpTablMax.ElOpTabl.R.HasValue)
            {
                matchedElOpTabl = matchedElOpTabl.Where(m => m.ElOpTabl.R <= searchRequest.ElOpTablMax.ElOpTabl.R);
                isElOpTablDataFilled = true;
            }

            if (isElOpTablDataFilled)
            {
                var matchedElOpTablHeadClue = await matchedElOpTabl
                    .Select(m => m.ElOpTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedElOpTablHeadClue);
            }

            IQueryable<Elastic1Language> matchedElastic1 = _context.Elastic1Language
                .Include(m => m.Elastic1);

            var isElastic1DataFilled = false;
            if (searchRequest.Elastic1Min.Elastic1.E1.HasValue)
            {
                matchedElastic1 = matchedElastic1.Where(m => m.Elastic1.E1 >= searchRequest.Elastic1Min.Elastic1.E1);
                isElastic1DataFilled = true;
            }
            if (searchRequest.Elastic1Max.Elastic1.E1.HasValue)
            {
                matchedElastic1 = matchedElastic1.Where(m => m.Elastic1.E1 <= searchRequest.Elastic1Max.Elastic1.E1);
                isElastic1DataFilled = true;
            }

            if (isElastic1DataFilled)
            {
                var matchedElastic1HeadClue = await matchedElastic1
                    .Select(m => m.Elastic1.HeadClue)
                    .ToListAsync();
                filters.Add(matchedElastic1HeadClue);
            }

            IQueryable<MechTablLanguage> matchedMechTabl = _context.MechTablLanguage
                .Include(m => m.MechTabl);

            var isMechTablDataFilled = false;
            if (searchRequest.MechTablMin.MechTabl.K.HasValue)
            {
                matchedMechTabl = matchedMechTabl.Where(m => m.MechTabl.K >= searchRequest.MechTablMin.MechTabl.K);
                isMechTablDataFilled = true;
            }
            if (searchRequest.MechTablMax.MechTabl.K.HasValue)
            {
                matchedMechTabl = matchedMechTabl.Where(m => m.MechTabl.K <= searchRequest.MechTablMax.MechTabl.K);
                isMechTablDataFilled = true;
            }

            if (isMechTablDataFilled)
            {
                var matchedMechTablHeadClue = await matchedMechTabl
                    .Select(m => m.MechTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedMechTablHeadClue);
            }

            IQueryable<HardTablLanguage> matchedHardTabl = _context.HardTablLanguage
                .Include(m => m.HardTabl);

            var isHardTablDataFilled = false;
            if (searchRequest.HardTablMin.HardTabl.Hard1.HasValue)
            {
                matchedHardTabl = matchedHardTabl.Where(m => m.HardTabl.Hard1 >= searchRequest.HardTablMin.HardTabl.Hard1);
                isHardTablDataFilled = true;
            }
            if (searchRequest.HardTablMax.HardTabl.Hard1.HasValue)
            {
                matchedHardTabl = matchedHardTabl.Where(m => m.HardTabl.Hard1 <= searchRequest.HardTablMax.HardTabl.Hard1);
                isHardTablDataFilled = true;
            }
            if (searchRequest.HardTablMin.HardTabl.Hard2.HasValue)
            {
                matchedHardTabl = matchedHardTabl.Where(m => m.HardTabl.Hard2 >= searchRequest.HardTablMin.HardTabl.Hard2);
                isHardTablDataFilled = true;
            }
            if (searchRequest.HardTablMax.HardTabl.Hard2.HasValue)
            {
                matchedHardTabl = matchedHardTabl.Where(m => m.HardTabl.Hard2 <= searchRequest.HardTablMax.HardTabl.Hard2);
                isHardTablDataFilled = true;
            }
            if (searchRequest.HardTablMin.HardTabl.Mohs.HasValue)
            {
                matchedHardTabl = matchedHardTabl.Where(m => m.HardTabl.Mohs >= searchRequest.HardTablMin.HardTabl.Mohs);
                isHardTablDataFilled = true;
            }
            if (searchRequest.HardTablMax.HardTabl.Mohs.HasValue)
            {
                matchedHardTabl = matchedHardTabl.Where(m => m.HardTabl.Mohs <= searchRequest.HardTablMax.HardTabl.Mohs);
                isHardTablDataFilled = true;
            }

            if (isHardTablDataFilled)
            {
                var matchedHardTablHeadClue = await matchedHardTabl
                    .Select(m => m.HardTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedHardTablHeadClue);
            }

            IQueryable<HeatExpnLanguage> matchedHeatExpn = _context.HeatExpnLanguage
                .Include(m => m.HeatExpn);

            var isHeatExpnDataFilled = false;
            if (searchRequest.HeatExpnMin.HeatExpn.S11.HasValue)
            {
                matchedHeatExpn = matchedHeatExpn.Where(m => m.HeatExpn.S11 >= searchRequest.HeatExpnMin.HeatExpn.S11);
                isHeatExpnDataFilled = true;
            }
            if (searchRequest.HeatExpnMax.HeatExpn.S11.HasValue)
            {
                matchedHeatExpn = matchedHeatExpn.Where(m => m.HeatExpn.S11 <= searchRequest.HeatExpnMax.HeatExpn.S11);
                isHeatExpnDataFilled = true;
            }

            if (isHeatExpnDataFilled)
            {
                var matchedHeatExpnHeadClue = await matchedHeatExpn
                    .Select(m => m.HeatExpn.HeadClue)
                    .ToListAsync();
                filters.Add(matchedHeatExpnHeadClue);
            }

            IQueryable<PlavTablLanguage> matchedPlavTabl = _context.PlavTablLanguage
                .Include(m => m.PlavTabl);

            var isPlavTablDataFilled = false;
            if (searchRequest.PlavTablMin.PlavTabl.PlavTemp.HasValue)
            {
                matchedPlavTabl = matchedPlavTabl.Where(m => m.PlavTabl.PlavTemp >= searchRequest.PlavTablMin.PlavTabl.PlavTemp);
                isPlavTablDataFilled = true;
            }
            if (searchRequest.PlavTablMax.PlavTabl.PlavTemp.HasValue)
            {
                matchedPlavTabl = matchedPlavTabl.Where(m => m.PlavTabl.PlavTemp <= searchRequest.PlavTablMax.PlavTabl.PlavTemp);
                isPlavTablDataFilled = true;
            }

            if (isPlavTablDataFilled)
            {
                var matchedPlavTablHeadClue = await matchedPlavTabl
                    .Select(m => m.PlavTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedPlavTablHeadClue);
            }

            IQueryable<NlOpTablLanguage> matchedNlOpTabl = _context.NlOpTablLanguage
                .Include(m => m.NlOpTabl);

            var isNlOpTablDataFilled = false;
            if (searchRequest.NlOpTablMin.NlOpTabl.Rij.HasValue)
            {
                matchedNlOpTabl = matchedNlOpTabl.Where(m => m.NlOpTabl.Rij >= searchRequest.NlOpTablMin.NlOpTabl.Rij);
                isNlOpTablDataFilled = true;
            }
            if (searchRequest.NlOpTablMax.NlOpTabl.Rij.HasValue)
            {
                matchedNlOpTabl = matchedNlOpTabl.Where(m => m.NlOpTabl.Rij <= searchRequest.NlOpTablMax.NlOpTabl.Rij);
                isNlOpTablDataFilled = true;
            }

            if (isNlOpTablDataFilled)
            {
                var matchedNlOpTablHeadClue = await matchedNlOpTabl
                    .Select(m => m.NlOpTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedNlOpTablHeadClue);
            }

            IQueryable<ElemTablLanguage> matchedElemTabl = _context.ElemTablLanguage
                .Include(m => m.ElemTabl);

            var isElemTablDataFilled = false;
            if (searchRequest.ElemTablMin.ElemTabl.Znparam.HasValue)
            {
                matchedElemTabl = matchedElemTabl.Where(m => m.ElemTabl.Znparam >= searchRequest.ElemTablMin.ElemTabl.Znparam);
                isElemTablDataFilled = true;
            }
            if (searchRequest.ElemTablMax.ElemTabl.Znparam.HasValue)
            {
                matchedElemTabl = matchedElemTabl.Where(m => m.ElemTabl.Znparam <= searchRequest.ElemTablMax.ElemTabl.Znparam);
                isElemTablDataFilled = true;
            }
            if (!string.IsNullOrEmpty(searchRequest.ElemTablCommon.ElemTabl.NazvAngl))
            {
                matchedElemTabl = matchedElemTabl.Where(m => m.ElemTabl.NazvAngl.Contains(searchRequest.ElemTablCommon.ElemTabl.NazvAngl));
                isElemTablDataFilled = true;
            }
            if (searchRequest.ElemTablMin.ElemTabl.ZnAngle.HasValue)
            {
                matchedElemTabl = matchedElemTabl.Where(m => m.ElemTabl.ZnAngle >= searchRequest.ElemTablMin.ElemTabl.ZnAngle);
                isElemTablDataFilled = true;
            }
            if (searchRequest.ElemTablMax.ElemTabl.ZnAngle.HasValue)
            {
                matchedElemTabl = matchedElemTabl.Where(m => m.ElemTabl.ZnAngle <= searchRequest.ElemTablMax.ElemTabl.ZnAngle);
                isElemTablDataFilled = true;
            }

            if (isElemTablDataFilled)
            {
                var matchedElemTablHeadClue = await matchedElemTabl
                    .Select(m => m.ElemTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedElemTablHeadClue);
            }

            IQueryable<ModfTablLanguage> matchedModfTabl = _context.ModfTablLanguage
                .Include(m => m.ModfTabl);

            var isModfTablDataFilled = false;
            if (!string.IsNullOrEmpty(searchRequest.ModfTablCommon.ModfTabl.PointGrp))
            {
                matchedModfTabl = matchedModfTabl.Where(m => m.ModfTabl.PointGrp.Contains(searchRequest.ModfTablCommon.ModfTabl.PointGrp));
                isModfTablDataFilled = true;
            }

            if (isModfTablDataFilled)
            {
                var matchedModfTablHeadClue = await matchedModfTabl
                    .Select(m => m.ModfTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedModfTablHeadClue);
            }

            IQueryable<EsOpTablLanguage> matchedEsOpTabl = _context.EsOpTablLanguage
                .Include(m => m.EsOpTabl);

            var isEsOpTablDataFilled = false;
            if (searchRequest.EsOpTablMin.EsOpTabl.P.HasValue)
            {
                matchedEsOpTabl = matchedEsOpTabl.Where(m => m.EsOpTabl.P >= searchRequest.EsOpTablMin.EsOpTabl.P);
                isEsOpTablDataFilled = true;
            }
            if (searchRequest.EsOpTablMax.EsOpTabl.P.HasValue)
            {
                matchedEsOpTabl = matchedEsOpTabl.Where(m => m.EsOpTabl.P <= searchRequest.EsOpTablMax.EsOpTabl.P);
                isEsOpTablDataFilled = true;
            }

            if (isEsOpTablDataFilled)
            {
                var matchedEsOpTablHeadClue = await matchedEsOpTabl
                    .Select(m => m.EsOpTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedEsOpTablHeadClue);
            }

            IQueryable<RefrcIndLanguage> matchedRefrcInd = _context.RefrcIndLanguage
                .Include(m => m.RefrcInd);

            var isRefrcIndDataFilled = false;
            if (searchRequest.RefrcIndMin.RefrcInd.ZnachInd.HasValue)
            {
                matchedRefrcInd = matchedRefrcInd.Where(m => m.RefrcInd.ZnachInd >= searchRequest.RefrcIndMin.RefrcInd.ZnachInd);
                isRefrcIndDataFilled = true;
            }
            if (searchRequest.RefrcIndMax.RefrcInd.ZnachInd.HasValue)
            {
                matchedRefrcInd = matchedRefrcInd.Where(m => m.RefrcInd.ZnachInd <= searchRequest.RefrcIndMax.RefrcInd.ZnachInd);
                isRefrcIndDataFilled = true;
            }

            if (isRefrcIndDataFilled)
            {
                var matchedRefrcIndHeadClue = await matchedRefrcInd
                    .Select(m => m.RefrcInd.HeadClue)
                    .ToListAsync();
                filters.Add(matchedRefrcIndHeadClue);
            }

            IQueryable<HeatTablLanguage> matchedHeatTabl = _context.HeatTablLanguage
                .Include(m => m.HeatTabl);

            var isHeatTablDataFilled = false;
            if (!string.IsNullOrEmpty(searchRequest.HeatTablCommon.HeatTabl.ZnC))
            {
                matchedHeatTabl = matchedHeatTabl.Where(m => m.HeatTabl.ZnC.Contains(searchRequest.HeatTablCommon.HeatTabl.ZnC));
                isHeatTablDataFilled = true;
            }
            if (searchRequest.HeatTablMin.HeatTabl.Temperat.HasValue)
            {
                matchedHeatTabl = matchedHeatTabl.Where(m => m.HeatTabl.Temperat >= searchRequest.HeatTablMin.HeatTabl.Temperat);
                isHeatTablDataFilled = true;
            }
            if (searchRequest.HeatTablMax.HeatTabl.Temperat.HasValue)
            {
                matchedHeatTabl = matchedHeatTabl.Where(m => m.HeatTabl.Temperat <= searchRequest.HeatTablMax.HeatTabl.Temperat);
                isHeatTablDataFilled = true;
            }
            if (searchRequest.HeatTablMin.HeatTabl.C.HasValue)
            {
                matchedHeatTabl = matchedHeatTabl.Where(m => m.HeatTabl.C >= searchRequest.HeatTablMin.HeatTabl.C);
                isHeatTablDataFilled = true;
            }
            if (searchRequest.HeatTablMax.HeatTabl.C.HasValue)
            {
                matchedHeatTabl = matchedHeatTabl.Where(m => m.HeatTabl.C <= searchRequest.HeatTablMax.HeatTabl.C);
                isHeatTablDataFilled = true;
            }

            if (isHeatTablDataFilled)
            {
                var matchedHeatTablHeadClue = await matchedHeatTabl
                    .Select(m => m.HeatTabl.HeadClue)
                    .ToListAsync();
                filters.Add(matchedHeatTablHeadClue);
            }

            IQueryable<DielDissLanguage> matchedDielDiss = _context.DielDissLanguage
                .Include(m => m.DielDiss);

            var isDielDissDataFilled = false;
            if (searchRequest.DielDissMin.DielDiss.TangentD.HasValue)
            {
                matchedDielDiss = matchedDielDiss.Where(m => m.DielDiss.TangentD >= searchRequest.DielDissMin.DielDiss.TangentD);
                isDielDissDataFilled = true;
            }
            if (searchRequest.DielDissMax.DielDiss.TangentD.HasValue)
            {
                matchedDielDiss = matchedDielDiss.Where(m => m.DielDiss.TangentD <= searchRequest.DielDissMax.DielDiss.TangentD);
                isDielDissDataFilled = true;
            }

            if (isDielDissDataFilled)
            {
                var matchedDielDissHeadClue = await matchedDielDiss
                    .Select(m => m.DielDiss.HeadClue)
                    .ToListAsync();
                filters.Add(matchedDielDissHeadClue);
            }
            
            return filters;
        }
    }
}