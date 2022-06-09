using System;
using System.Threading.Tasks;
using System.Windows;
using TripPlanner.Domain.Models;
using TripPlanner.WPF.Services;
using TripPlanner.WPF.ViewModels;

namespace TripPlanner.WPF.Commands
{
    public class SearchCommand : AsyncCommandBase
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly IHouseDataService _houseDataService;

        public SearchCommand(HomeViewModel homeViewModel, IHouseDataService houseDataService)
        {
            _homeViewModel = homeViewModel;
            _houseDataService = houseDataService;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                var filter = new Filters
                {
                    MinArea = _homeViewModel.MinArea,
                    MaxArea = _homeViewModel.MaxArea == 0 ? double.MaxValue : _homeViewModel.MaxArea,
                    MinBeds = _homeViewModel.MinBeds,
                    MaxBeds = _homeViewModel.MaxBeds == 0 ? int.MaxValue : _homeViewModel.MaxBeds,
                    MinPrice = _homeViewModel.MinPrice,
                    MaxPrice = _homeViewModel.MaxPrice == 0 ? double.MaxValue : _homeViewModel.MaxPrice
                };

                _houseDataService.Houses?.Clear();
                await _houseDataService.GetByFilters(filter);
                _homeViewModel.Houses = _houseDataService.Houses;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
