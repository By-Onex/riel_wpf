using RieltorApp.NewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RieltorApp.DB
{
    public class AutoSearchItem : BaseViewModel
    {
        public int Id { get; set; }
        public SearchType SearchType { get; set; } = SearchType.Buy;
        public int MinPrice { get; set; } = 0;
        public int MaxPrice { get; set; } = 10000;
        public int MinArea { get; set; } = 0;
        public int MaxArea { get; set; } = 100;
        public int MinFloor { get; set; } = 0;
        public int MaxFloor { get; set; } = 10;
        public int MinStoreys { get; set; } = 0;
        public int MaxStoreys { get; set; } = 10;
        public string District { get; set; } = "Любой";
        public string Description { get; set; } = "";
        public int FoundApart { get; set; } = 0;
        public RoomCount RoomCount { get; set; } = RoomCount.Any;

        [LiteDB.BsonIgnore]
        public string DescriptionInfo { get => string.IsNullOrWhiteSpace(Description)? "Примечание отсутствует" : "Примечание: " + Description;}
        [LiteDB.BsonIgnore]
        public string FoundApartInfo { get => "Найдено: " + FoundApart; }

        [LiteDB.BsonIgnore]
        private string _num = "";
        [LiteDB.BsonIgnore]
        public string Num { get => _num; set { _num = value; NotifyPropertyChanged("Num"); NotifyPropertyChanged("DescriptionInfo"); NotifyPropertyChanged("FoundApartInfo"); } }

    }
}
