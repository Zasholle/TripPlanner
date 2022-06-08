using System;
using System.Threading.Tasks;
using System.Windows;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Commands
{
    public class LoadCommand : AsyncCommandBase
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly IHouseDataService _houseDataService;

        public LoadCommand(HomeViewModel homeViewModel, IHouseDataService houseDataService)
        {
            _homeViewModel = homeViewModel;
            _houseDataService = houseDataService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                await _houseDataService.LoadData();
                _homeViewModel.Houses = _houseDataService.Houses;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
