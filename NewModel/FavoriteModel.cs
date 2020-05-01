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
            var aparts = await Task.Run(() => { return DataBase.GetFavoriteApartment(); });

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
            DataBase.Insert(apartment, DBTable.FavoriteApartment.ToString());
        }
    }
}
