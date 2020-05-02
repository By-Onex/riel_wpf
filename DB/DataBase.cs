using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RieltorApp.DB
{
    public enum DBTable
    {
        FavoriteApartment,
    }
    public static class DataBase
    {
        public static LiteDatabase db { get; private set; }

        public static void Connect()
        {
            db = new LiteDatabase("Filename=./data.db;Connection=shared");

            db.GetCollection<ApartmentItem>("FavoriteApartment").EnsureIndex(a => a.Address);
        }

        public static List<ApartmentItem> GetFavoriteApartment()
        {
            return db.GetCollection<ApartmentItem>("FavoriteApartment").FindAll().ToList();
        }

        public static void Insert<T>(T item, DBTable collectionName)
        {
            db.GetCollection<T>(collectionName.ToString()).Insert(item);
        }

        public static void Delete<T>(BsonValue id, DBTable collectionName)
        {
            db.GetCollection<T>(collectionName.ToString()).Delete(id);
        }

        public static List<T> GetCollectionList<T>(DBTable collectionName)
        {
            return db.GetCollection<T>(collectionName.ToString()).FindAll().ToList();
        }

    }
}
