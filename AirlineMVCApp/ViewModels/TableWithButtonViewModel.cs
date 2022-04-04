using AirlineMVCApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace AirlineMVCApp.ViewModels
{
    public class TableWithButtonViewModel
    {
        private List<Flight> flights = new List<Flight>();
        public List<Flight> Flights
        {
            get
            {
                return this.flights;
            }
            set
            {
                this.flights = value.OrderBy(f => f.TravelDate).ToList();
            }
        }

        public string ButtonColumnHeader { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ButtonDisabled { get; set; } = "";

    }
}
