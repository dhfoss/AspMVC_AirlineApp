using AirlineMVCApp.Models;
using AirlineMVCApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AirlineMVCApp.Components
{
    public class PassengerFlightInProgress : ViewComponent
    {
        private readonly IPassengerInfoRepository _passengerInfoRepository;
        private UserManager<ApplicationUser> _userManager;
        private readonly IFlightRepository _flightRepository;
        public PassengerFlightInProgress(IPassengerInfoRepository passengerInfoRepository, IFlightRepository flightRepository, UserManager<ApplicationUser> userManager)
        {
            _passengerInfoRepository = passengerInfoRepository;
            _userManager = userManager;
            _flightRepository = flightRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            if (userId == null)
                return View(new PassengerFlightInProgressViewModel() { Visibility = "d-none"});

            var user = await _userManager.FindByIdAsync(userId);
            var role = await _userManager.GetRolesAsync(user);



            if (role.Count == 0)
            {
                var passenger = _passengerInfoRepository.SelectPassengerById(userId);
                var passengerFlightInProgressViewModel = new PassengerFlightInProgressViewModel();
                if (passenger.IsBoarded)
                {
                    string flightNumber = _flightRepository.SelectFlighByFlightId(passenger.BoardedFlight).FlightNumber;
                    passengerFlightInProgressViewModel.Visibility = "";
                    passengerFlightInProgressViewModel.Controller = "Passenger";
                    passengerFlightInProgressViewModel.Action = "PassengerFlightInProgress";
                    passengerFlightInProgressViewModel.DisplayText = $"On Flight {flightNumber}. Request Service?";
                }
                else
                {
                    passengerFlightInProgressViewModel.Visibility = "d-none";
                    passengerFlightInProgressViewModel.Controller = "";
                    passengerFlightInProgressViewModel.Action = "";
                    passengerFlightInProgressViewModel.DisplayText = "";

                }
                return View(passengerFlightInProgressViewModel);
            }

            PassengerFlightInProgressViewModel viewModel = GetFlightInProgressViewModel(role[0]);
            return View(viewModel);
        }

        private PassengerFlightInProgressViewModel GetFlightInProgressViewModel(string roleName)
        {
            var viewModel = new PassengerFlightInProgressViewModel() { Controller = roleName, Visibility = ""};
            var flight = _flightRepository.SelectFlightInProgress();
            if(flight == null)
            {
                viewModel.Visibility = "d-none";
                return viewModel;
            }

            switch (roleName)
            {
                case "FlightAttendant":
                    viewModel.Action = "ServicePassengers";
                    viewModel.DisplayText = $"Service Passengers on {flight.FlightNumber}";
                    break;
                case "Pilot":
                    viewModel.Action = "PilotFlightInProgress";
                    viewModel.DisplayText = $"Pilot Flight {flight.FlightNumber}";
                    break;
                case "Admin":
                    viewModel.Action = "PilotFlightInProgress";
                    viewModel.DisplayText = $"Pilot Flight {flight.FlightNumber}";
                    viewModel.Controller = "Pilot";
                    break;
                default:
                    break;
            }
            return viewModel;
        }
    }
}
