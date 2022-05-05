namespace VehicleCRUD.Contract.Interfaces
{
    /// <summary>
    /// This interface forces its child to implement certian core functionalities that are required for a Vehicle entity
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// Adds new vehicle with details
        /// </summary>
        bool AddNewVehicleDetails(VehicleDTO vDto);

        /// <summary>
        /// Updates an existing vehicle's details
        /// </summary>
        bool UpdateVehicleDetails(VehicleDTO vDto);

        /// <summary>
        /// Deletes an existing vehicle by ID
        /// </summary>
        bool DeleteVehicle(int id);
    }
}
