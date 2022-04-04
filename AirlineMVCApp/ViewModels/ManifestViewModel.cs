using AirlineMVCApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace AirlineMVCApp.ViewModels
{
    public class ManifestViewModel
    {
        private List<Passenger> passengers;
        public Flight Flight { get; set; }
        public List<Passenger> Passengers
        {
            get
            {
                return this.passengers;
            }
            set
            {
                this.passengers = value.OrderBy(p => p.LastName).ToList();
            }
        }

    }
}
