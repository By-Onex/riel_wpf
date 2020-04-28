using RieltorApp.NewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RieltorApp.Base
{
    public abstract class BaseScraper
    {
        public abstract Task<List<ApartmentModel>> GetApartments(SearchArgumentModel argumentModel);
        public abstract List<ApartmentModel> GetSyncApartments(SearchArgumentModel argumentModel);
    }
}
