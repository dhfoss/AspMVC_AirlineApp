using AirlineMVCApp.Models;
using System.Collections.Generic;

namespace AirlineMVCApp.ViewModels
{
    public class ServicePassengersViewModel
    {
        public Flight Flight { get; set; }
        public List<BoardedPassenger> BoardedPassengers { get; set; }

    }
}
