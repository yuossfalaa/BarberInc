using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;


namespace BarberInc.ViewModel
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : BaseViewModel;
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        Visibility isBusy = Visibility.Collapsed;
        [ObservableProperty]
        int font_1 = 12;
        [ObservableProperty]
        int font_2 = 16;
        [ObservableProperty]
        int font_2_5 = 18;
        [ObservableProperty]
        int font_3 = 22;
        [ObservableProperty]
        int font_4 = 28;
        [ObservableProperty]
        int font_5 = 32;
        [ObservableProperty]
        int font_6 = 36;
        [ObservableProperty]
        int headerFont = 42;
        [ObservableProperty]
        int border = 2;
        public virtual void Dispose()
        {

        }
    }
}
