using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VehicleCRUD.Contract;
using VehicleCRUD.Contract.DBManagers;

namespace VehicleCRUD.Impl.DBStore
{
    /// <summary>
    /// Connects to the DB and Implements the CRUD operatiosn on a Table. 
    /// Since there is no actual DB, saw it fit to use SQL commands instead of calling stored procedures.
    /// </summary>
    public class VehicleDBStore : IDBManager
    {
        private readonly string _connectionString;
        private readonly int _commandTimeout;
        private SqlConnection _connection;
        private IConfiguration Configuration;
        /// <summary>
        /// Constructor to initialise connections strings from appsettings.json.
        /// </summary>
        /// <param name="_configuration"></param>
        public VehicleDBStore(IConfiguration _configuration)
        {

            Configuration = _configuration;
            _connectionString = this.Configuration.GetConnectionString("VehicleDBConnectionString");
            _commandTimeout = Convert.ToInt32(this.Configuration.GetConnectionString("CommandTimeOut"));
        }
        /// <summary>
        /// Returns List of Vehicles
        /// </summary>
        /// <returns></returns>
        public List<VehicleDTO> GetVehicles()
        {
            List<VehicleDTO> lstVehicleDTO = new List<VehicleDTO>();
            try
            {
                DataTable dt = new DataTable();
                
                string query = "SELECT * FROM dbo.Vehicles";
                _connection.Open();
                SqlCommand command = new SqlCommand(query, _connection);
                
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                foreach(DataRow dr in dt.Rows)
                {
                    VehicleDTO vDto = new VehicleDTO() { 
                        
                        Id = dr.Field<int>("ID"), 
                        Year = dr.Field<int>("Year"),
                        Make = dr.Field<string>("Make"),
                        Model = dr.Field<string>("Model")
                    };
                    lstVehicleDTO.Add(vDto);
                }
            }
            catch
            {
                throw;
            }
            
            return lstVehicleDTO;
        }
        /// <summary>
        /// Returns a Vehicles by Id
        /// </summary>
        /// <returns></returns>
        public VehicleDTO GetVehicleByID(int id) 
        {
            VehicleDTO vDto = new VehicleDTO();
            try
            {
                DataTable dt = new DataTable();

                string query = "SELECT * FROM dbo.Vehicles WHERE ID = "+id.ToString();
                _connection.Open();
                SqlCommand command = new SqlCommand(query, _connection);
                
                foreach (DataRow dr in dt.Rows)
                {
                    vDto = new VehicleDTO()
                    {

                        Id = dr.Field<int>("ID"),
                        Year = dr.Field<int>("Year"),
                        Make = dr.Field<string>("Make"),
                        Model = dr.Field<string>("Model")
                    };
                }
            }
            catch
            {
                throw;
            }
            return vDto;
        }
        /// <summary>
        /// Returns List of Vehicles by filters
        /// </summary>
        /// <returns></returns>
        public List<VehicleDTO> GetVehiclesByFilter(string filter, string value) 
        {
            List<VehicleDTO> lstVehicleDTO = new List<VehicleDTO>();
            try
            {
                string query = string.Empty;
                switch (filter.ToLower())
                {
                    case "year":
                        query = "SELECT * FROM dbo.Vehicles where Year =" + value.ToString();
                        break;
                    case "make":
                        query = "SELECT * FROM dbo.Vehicles where Make =" + value.ToString();
                        break;

                    case "model":
                        query = "SELECT * FROM dbo.Vehicles where Model =" + value.ToString();
                        break;

                    case "id":
                        query = "SELECT * FROM dbo.Vehicles where Id =" + value.ToString();
                        break;
                    default:
                        query = "SELECT * FROM dbo.Vehicles";
                        break;
                }
                DataTable dt = new DataTable();
                _connection.Open();
                SqlCommand command = new SqlCommand(query, _connection);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    VehicleDTO vDto = new VehicleDTO()
                    {

                        Id = dr.Field<int>("ID"),
                        Year = dr.Field<int>("Year"),
                        Make = dr.Field<string>("Make"),
                        Model = dr.Field<string>("Model")
                    };
                    lstVehicleDTO.Add(vDto);
                }
            }
            catch
            {
                throw;
            }
            return lstVehicleDTO;
        }
        /// <summary>
        /// Adds a Vehicle
        /// </summary>
        /// <returns></returns>
        public bool AddNewVehicle(VehicleDTO vehicleDTO) 
        {
            bool status;
            try
            {
                string query = "INSERT INTO dbo.Vehicles (Id, Make, Model, Year) Values("+ vehicleDTO.Id.ToString()+","+ vehicleDTO.Make.ToString()+","+
                                vehicleDTO.Model.ToString()+","+ vehicleDTO.Year.ToString()+";";
                _connection.Open();
                SqlCommand command = new SqlCommand(query, _connection);
                command.CommandType = CommandType.Text;
                command.ExecuteScalar();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }
        /// <summary>
        /// Updates an existing Vehicle's details
        /// </summary>
        /// <returns></returns>
        public bool UpdateVehicleDetails(VehicleDTO vehicleDTO) 
        {
            bool status;
            try
            {
                string query = "UPDATE dbo.Vehicles SET Make =" + vehicleDTO.Make.ToString() +
                    ", Model =" + vehicleDTO.Model.ToString() + ", Year = " + vehicleDTO.Year.ToString() + "WHERE Id = " + vehicleDTO.Id + ";";
                _connection.Open();
                SqlCommand command = new SqlCommand(query, _connection);
                command.CommandType = CommandType.Text;
                command.ExecuteScalar();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }
        /// <summary>
        /// Deletes List of Vehicles
        /// </summary>
        /// <returns></returns>
        public bool DeleteVehicleByID(int id) 
        {
             bool status;
            try
            {
                string query = "DELETE FROM dbo.Vehicles WHERE Id =" + id.ToString();
                _connection.Open();
                SqlCommand command = new SqlCommand(query, _connection);
                command.CommandType = CommandType.Text;
                command.ExecuteScalar();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }
        /// <summary>
        /// Establishes a connection to the DB if it does not exist already.
        /// </summary>
        /// <returns></returns>
        public void ConnectToDB()
        {
            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    _connection = new SqlConnection(_connectionString);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
