using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Services
{
    public interface INavigationService<T> where T : ViewModelBase
    {
        void Navigate();
    }
}
