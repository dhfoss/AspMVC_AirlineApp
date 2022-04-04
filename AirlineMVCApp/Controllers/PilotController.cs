using AirlineMVCApp.Models;
using AirlineMVCApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirlineMVCApp.Controllers
{
    [Authorize(Roles = "Pilot, Admin")]

    public class PilotController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerInfoRepository _passengerInfoRepository;

        public PilotController(IFlightRepository flightRepository, IPassengerInfoRepository passengerInfoRepository)
        {
            _flightRepository = flightRepository;
            _passengerInfoRepository = passengerInfoRepository;
        }

        public IActionResult PilotAFlight()
        {
            
            var flights = _flightRepository.GetAllFlightsThatHaveNotDeparted();
            var tableWithButtonViewModel = new TableWithButtonViewModel()
            {
                Flights = flights,
                ButtonColumnHeader = "Pilot Flight",
                Controller = "Pilot",
                Action = "PilotFlight"
            };
            var currentflight = _flightRepository.SelectFlightInProgress();
            if(currentflight != null)
            {
                tableWithButtonViewModel.ButtonDisabled = "disabled pilotBtn";
            }

            return View(tableWithButtonViewModel);
        }

        public IActionResult PilotFlight(int id)
        {
            _flightRepository.ProgressFlight(id);
            return RedirectToAction("PilotFlightInProgress");
        }

        public IActionResult PilotFlightInProgress()
        {
            var currentFlight = _flightRepository.SelectFlightInProgress();
            if(currentFlight == null)
            {
                return RedirectToAction("Index", "FlightNotFound");
            }
            var flightWithTasks = _flightRepository.SelectFlightWithFlightTaskByFlightId(currentFlight.FlightId);
            return View(flightWithTasks);
        }

        public IActionResult ProgressFlight(int id)
        {
            var flightWithTasks = _flightRepository.SelectFlightWithFlightTaskByFlightId(id);
            if(flightWithTasks.NextFlightTask.FlightTaskId == 5)
            {
                _flightRepository.EndFlight(id);
                _passengerInfoRepository.DeboardPassengers(id);
                System.Threading.Thread.Sleep(2000);
                return RedirectToAction("FlightEnded", new { flight = flightWithTasks });
            }
            _flightRepository.ProgressFlight(id);
            System.Threading.Thread.Sleep(2000);

            return RedirectToAction("PilotFlightInProgress");
        }

        public IActionResult FlightEnded(Flight flight)
        {
            return View(flight);
        }
    }
}
