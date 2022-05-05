using System.Collections.Generic;
using VehicleCRUD.Contract;
using VehicleCRUD.Contract.DBManagers;
using VehicleCRUD.Contract.Interfaces;
using VehicleCRUD.Impl.DBStore;

namespace VehicleCRUD.Impl.Handlers
{
    /// <summary>
    /// Interface that inherits from IVehicle, this interface adds additional non-core functional changes to the Vehicle entity.
    /// </summary>
    public interface IVehicleCRUDHandler: IVehicle
    {
        /// <summary>
        ///  Gets all the vehicles
        /// </summary>
        List<VehicleDTO> GetVehicles();
        /// <summary>
        /// Gets a specific vehicle based on a filter
        /// </summary>
        VehicleDTO GetVehicleByID(int id);
        /// <summary>
        /// Gets vehicles based on a filter
        /// </summary>
        List<VehicleDTO> GetVehiclesByFilter(string filter, string value);
        /// <summary>
        /// Adds new vehicle with details
        /// </summary>
        new bool AddNewVehicleDetails(VehicleDTO vDto);

        /// <summary>
        /// Updates an existing vehicle's details
        /// </summary>
        new bool UpdateVehicleDetails(VehicleDTO vDto);

        /// <summary>
        /// Deletes an existing vehicle by ID
        /// </summary>
        new bool DeleteVehicle(int id);
    }
    /// <summary>
    /// Implements all the business functionalities that are added to its parent interface chain.
    /// </summary>
    public class VehicleCRUDHandler : IVehicleCRUDHandler
    {

        private VehicleDBStore _vehicleDBStore;
        /// <summary>
        /// Constructor to initialise the class dependencies
        /// </summary>
        /// <param name="dbstore"></param>
        public VehicleCRUDHandler(IDBManager dbstore)
        {
            _vehicleDBStore = (VehicleDBStore)dbstore;
            _vehicleDBStore.ConnectToDB();
        }
        /// <summary>
        /// Adds new vehicle with the supplied object 
        /// </summary>
        /// <param name="vDto"></param>
        /// <returns></returns>
        public bool AddNewVehicleDetails(VehicleDTO vDto)
        {
            bool status;
            try
            {
                status= _vehicleDBStore.AddNewVehicle(vDto);
            }
            catch
            {
                status = false;
                //log into a DB/File/Cloud or throw to the controller
            }
            return status;
        }
        /// <summary>
        /// Deletes a vehicle based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteVehicle(int id)
        {
            bool status;
            try
            {
                status = _vehicleDBStore.DeleteVehicleByID(id);
            }
            catch
            {
                status = false;
                //log into a DB/File/Cloud or throw to the controller
            }
            return status;
        }
        /// <summary>
        /// Returns a vehicles based on the id param
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public VehicleDTO GetVehicleByID(int id)
        {
            VehicleDTO vDto;
            try
            {
                vDto = _vehicleDBStore.GetVehicleByID(id);
            }
            catch
            {
                vDto = new VehicleDTO();
                //log into a DB/File/Cloud or throw to the controller
            }
            return vDto;
        }
        /// <summary>
        /// Gets all the list of vehicles from the DB.
        /// </summary>
        /// <returns></returns>
        public List<VehicleDTO> GetVehicles()
        {
            List<VehicleDTO> lstVehicleDto;
            try
            {
                lstVehicleDto = _vehicleDBStore.GetVehicles();
            }
            catch
            {
                lstVehicleDto = new List<VehicleDTO>();
                //log into a DB/File/Cloud or throw to the controller
            }
            return lstVehicleDto;
        }
        /// <summary>
        /// Gets all the list of vehicles from the DB based on the Filter params
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<VehicleDTO> GetVehiclesByFilter(string filter, string value)
        {
            List<VehicleDTO> lstVehicleDto;
            try
            {
                lstVehicleDto = _vehicleDBStore.GetVehiclesByFilter(filter,value);
            }
            catch
            {
                lstVehicleDto = new List<VehicleDTO>();
                //log into a DB/File/Cloud or throw to the controller
            }
            return lstVehicleDto;
        }
        /// <summary>
        /// Updates an existing vehicles with new details
        /// </summary>
        /// <param name="vDto"></param>
        /// <returns></returns>
        public bool UpdateVehicleDetails(VehicleDTO vDto)
        {
            bool status;
            try
            {
                status = _vehicleDBStore.UpdateVehicleDetails(vDto);
            }
            catch
            {
                status = false;
                //log into a DB/File/Cloud or throw to the controller
            }
            return status;
        }
    }
}
