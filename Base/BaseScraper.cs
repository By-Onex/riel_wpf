using RieltorApp.DB;
using RieltorApp.NewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RieltorApp.Base
{
    public abstract class BaseScraper
    {
        public abstract Task<List<ApartmentItem>> GetApartments(SearchArgumentModel argumentModel);
        public abstract Task<List<FoundApartment>> GetApartments(List<SearchArgumentModel> argumentModel, SearchType searchType);
    }
}
