using RieltorApp.DB;
using RieltorApp.NewViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RieltorApp.NewModel
{
    public static class FavoriteModel
    {
        public static async void GetAparts()
        {
            var aparts = await Task.Run(() => { return DataBase.GetCollectionList<ApartmentItem>(DBTable.FavoriteApartment); });
            aparts.ForEach(a => a.IsFavorite = true);
            ResultViewModel.Instance.Items = aparts;

            ResultViewModel.Instance.ShowAnimation = Visibility.Hidden;
            ResultViewModel.Instance.ShowResult = Visibility.Visible;

            if (aparts.Count == 0)
            {
                ResultViewModel.Instance.ShowStatus = Visibility.Visible;
                ResultViewModel.Instance.ShowResult = Visibility.Hidden;
            }
        }

        public static void AddFavorite(ApartmentItem apartment)
        {
            if (apartment.IsFavorite)
            {
                DataBase.Delete(apartment.Id, DBTable.FavoriteApartment);
                apartment.IsFavorite = false;
            }
            else
            {
                DataBase.Insert(apartment, DBTable.FavoriteApartment);
                apartment.IsFavorite = true;
            }
        }
    }
}
