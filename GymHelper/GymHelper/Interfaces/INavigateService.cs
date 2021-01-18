using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GymHelper.Data.Interfaces
{
    public interface INavigateService
    {
        Task Navigate<TPage>() where TPage : Page;
        Task Navigate<TPage>(object providedObject) where TPage : Page;
        Task NavigateBack();
    }
}
