using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RieltorApp.DB
{
    public class FoundApartment
    {
        public ObjectId Id { get; set; }
        public int AutoSearchId { get; set; }
        public ApartmentItem apartment { get; set; }
    }
}
