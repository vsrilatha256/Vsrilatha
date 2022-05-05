namespace VehicleCRUD.Contract.DBManagers
{
    /// <summary>
    /// Interface that forces its child to implement a single connection to DB at a time.
    /// </summary>
    public interface IDBManager
    {
        void ConnectToDB();
    }
}
