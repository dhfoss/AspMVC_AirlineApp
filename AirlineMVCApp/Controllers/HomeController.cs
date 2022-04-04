using AirlineMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NonFactors.Mvc.Grid;
using OfficeOpenXml;
using System;

namespace AirlineMVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        public HomeController(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public IActionResult Index()
        {
            var flights = _flightRepository.GetThreeSoonestFlights();
            return View(flights);
        }

        //public ViewResult ViewAllFlights()
        //{
        //    var flights = _flightRepository.GetAllFlightsThatHaveNotDeparted();
        //    return View(flights);
        //}

        [HttpGet]
        public ViewResult ViewAllFlights()
        {
            return View(CreateExportableGrid());
        }

        [HttpGet]
        public IActionResult ExportIndex()
        {
            return Export(CreateExportableGrid(), "Flights");
        }
        // Using EPPlus from nuget.
        // Export grid method can be reused for all grids.
        private FileContentResult Export(IGrid grid, String fileName)
        {
            Int32 col = 1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using ExcelPackage package = new ExcelPackage();
            ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Data");

            foreach (IGridColumn column in grid.Columns)
            {
                sheet.Cells[1, col].Value = column.Title;
                sheet.Column(col++).Width = 18;

                column.IsEncoded = false;
            }

            foreach (IGridRow<Object> row in grid.Rows)
            {
                col = 1;

                foreach (IGridColumn column in grid.Columns)
                    sheet.Cells[row.Index + 2, col++].Value = column.ValueFor(row);
            }

            return File(package.GetAsByteArray(), "application/unknown", $"{fileName}.xlsx");
        }
        private IGrid<Flight> CreateExportableGrid()
        {
            IGrid<Flight> grid = new Grid<Flight>(_flightRepository.GetAllFlightsThatHaveNotDeparted());
            grid.ViewContext = new ViewContext { HttpContext = HttpContext };
            grid.Query = Request.Query;

            grid.Columns.Add(model => model.FlightNumber).Titled("Flight Number");
            grid.Columns.Add(model => model.DepartureCity).Titled("Departure City");

            grid.Columns.Add(model => model.ArrivalCity).Titled("Arrival City");
            grid.Columns.Add(model => model.TravelDate).Titled("Travel Date").Formatted("{0:d}");

            // Pager should be excluded on export if all data is needed.
            grid.Pager = new GridPager<Flight>(grid);
            grid.Processors.Add(grid.Pager);
            grid.Processors.Add(grid.Sort);
            grid.Pager.RowsPerPage = 6;

            foreach (IGridColumn column in grid.Columns)
            {
                column.Filter.IsEnabled = true;
                column.Sort.IsEnabled = true;
            }

            return grid;
        }

    }
}
