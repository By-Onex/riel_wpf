using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RieltorApp.DB
{
    public class ApartmentItem
    {
        public int Id { get; set; }
        public Address Address { get; set; }
        public int Price { get; set; }
        public float Area { get; set; }
        public int Floor { get; set; }
        public int Storeys { get; set; }
        public int RoomCount { get; set; }
        public string PageUrl { get; set; } = "";
        public string ImageUrl { get; set; } = "/RieltorApp;component/Resource/newBg.jpg";
        [LiteDB.BsonIgnore]
        public string PriceInfo { get => string.Format("{0} руб.", Price); }
        [LiteDB.BsonIgnore]
        public string Info { get => string.Format("{0}-к квартира {1} м. {2}/{3} эт.", RoomCount, Area, Floor, Storeys); }
    }

    public class Address
    {
        public string District { get; set; }
        public string Street { get; set; }
        public string Num { get; set; }
        public string Info { get => string.Format("{0}, {1}", Street, Num);
    }
}
}
