using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web.Http.Description;
using Unity;
using VehicleCRUD.Contract;
using VehicleCRUD.Impl.Handlers;

namespace VehicleCRUD.Controllers
{
    /// <summary>
    /// Client(Web/android/ios) facing controller layer handling basic Rest services like GET, POST, PUT, DELETE 
    /// </summary>
    [Route("v1/vehicleService")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleCRUDHandler _vehicleCRUDHandler;
        /// <summary>
        /// Constructor with Dependency Injection via Startup class(.Net Core)
        /// </summary>
        [InjectionConstructor]
        public VehicleController(IVehicleCRUDHandler vehicleCRUDHandler)
        {
            _vehicleCRUDHandler = vehicleCRUDHandler;
           
        }
        /// <summary>
        /// Gets all the vehicles
        /// </summary>
        [HttpGet, Route("vehicles"), ResponseType(typeof(List<VehicleDTO>))]
        public ActionResult Get()
        {
            return Ok(_vehicleCRUDHandler.GetVehicles());
        }
        /// <summary>
        /// Gets a specific vehicle with the id
        /// </summary>
        /// <param name="id"></param>
        // GetVehiclesById/1
        [HttpGet, Route("vehicles/{id}"), ActionName("GetVehiclesById"), ResponseType(typeof(VehicleDTO))]
        public ActionResult Get(int id)
        {
            VehicleDTO vehicle = _vehicleCRUDHandler.GetVehicleByID(id);
            if(vehicle != null)
            {
                return Ok(vehicle);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Inserts the vehicle into the database
        /// </summary>
        // POST vehicles
        [HttpPost, Route("AddVehicle"), ResponseType(typeof(string))]
        public ActionResult Post([FromBody] VehicleDTO vehicle)
        {
            string status;
            if (ModelState.IsValid)
            {
                if(vehicle.Make != null && vehicle.Make != string.Empty && vehicle.Model != null && vehicle.Model != string.Empty)
                {
                    if(vehicle.Year.ToString() != "" && vehicle.Year > 1950 && vehicle.Year <2050)
                    {
                        status = _vehicleCRUDHandler.AddNewVehicleDetails(vehicle).ToString();
                    }
                    else
                    {
                        status = "Year of Manufacture must be a non-empty value between 1950 & 2050";
                    }
                }
                else
                {
                    status = "Vehicle Make & Model must be non-null & non-empty values";
                }

            }
            else
            {
                status = "Invalid Model";
            }
            return Ok(status);
        }
        /// <summary>
        /// Updates the vehicle for an existing one
        /// </summary>
        // PUT vehicles
        [HttpPut, Route("UpdateVehicle"), ResponseType(typeof(string))]
        public ActionResult Put([FromBody] VehicleDTO vehicle)
        {
            string status;
            if (ModelState.IsValid)
            {
                if (vehicle.Make != null && vehicle.Make != string.Empty && vehicle.Model != null && vehicle.Model != string.Empty)
                {
                    if (vehicle.Year.ToString() != "" && vehicle.Year > 1950 && vehicle.Year < 2050)
                    {
                        status = _vehicleCRUDHandler.UpdateVehicleDetails(vehicle).ToString();
                    }
                    else
                    {
                        status = "Year of Manufacture must be a non-empty value between 1950 & 2050";
                    }
                }
                else
                {
                    status = "Vehicle Make & Model must be non-null & non-empty values";
                }

            }
            else
            {
                status = "Invalid Model";
            }
            return Ok(status);
        }
        /// <summary>
        /// Delete the vehicle with that id
        /// </summary>
        /// <param name="id"></param>
        // DELETE vehicles/1
        [HttpDelete, Route("vehicles/{id}"), ActionName("DeleteVehicleById"), ResponseType(typeof(bool))]
        public ActionResult Delete(int id)
        {
            if (_vehicleCRUDHandler.DeleteVehicle(id))
                return Ok();
            else
                return NotFound();
        }
        /// <summary>
        /// Gets vehicles based on filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        [HttpGet, Route("vehicles/{filter}/{value}"), ActionName("GetVehiclesByFilter"), ResponseType(typeof(VehicleDTO))]
        public ActionResult Get(string filter, string value)
        {
            List<VehicleDTO> lstvehicles = new List<VehicleDTO>();
            lstvehicles = _vehicleCRUDHandler.GetVehiclesByFilter(filter,value);
            if (lstvehicles.Count != 0)
            {
                return Ok(lstvehicles);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
