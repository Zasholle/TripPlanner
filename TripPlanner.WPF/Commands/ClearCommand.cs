using System.Threading.Tasks;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Commands
{
    public class ClearCommand : AsyncCommandBase
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly IHouseDataService _houseDataService;

        public ClearCommand(HomeViewModel homeViewModel, IHouseDataService houseDataService)
        {
            _homeViewModel = homeViewModel;
            _houseDataService = houseDataService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _houseDataService.Houses?.Clear();
            _homeViewModel.MinArea = _homeViewModel.MaxArea = _homeViewModel.MinPrice = _homeViewModel.MaxPrice = 0;
            _homeViewModel.MinBeds = _homeViewModel.MaxBeds = 0;
            await _houseDataService.LoadData();
            _homeViewModel.Houses = _houseDataService.Houses;
        }
    }
}
