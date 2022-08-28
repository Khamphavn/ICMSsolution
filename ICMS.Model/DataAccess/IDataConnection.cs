using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.Model.DataAccess
{
    public interface IDataConnection
    {
        #region Unit
        List<Unit> Unit_GetAll(string connectionString);
        List<Unit> Unit_GetActive(string connectionString);
        //Unit Unit_GetById(int Id, string connectionString);
        int Unit_Insert(Unit model, string connectionString);
        int Unit_Update(Unit model, string connectionString);
        int Unit_DeleteById(int Id, string connectionString);
        #endregion

        #region City
        List<City> City_GetAll(string connectionString);
        List<City> City_GetActive(string connectionString);
        //City City_GetById(int Id, string connectionString);
        //City City_GetByName(string name, string connectionString);
        int City_Update(City model, string connectionString);
        int City_Insert(City model, string connectionString);
        int City_DeleteById(int Id, string connectionString);
        #endregion

        #region Customer
        List<Customer> Customer_GetAll(string connectionString);
        //Customer Customer_GetById(int Id, string connectionString);
        //Customer Customer_GetByFullName(string FullName, string connectionString);
        int Customer_Update(Customer model, string connectionString);
        //int Customer_Insert(Customer model, string connectionString);
        int Customer_DeleteById(int Id, string connectionString);
        #endregion

        #region Role
        List<Role> Role_GetAll(string connectionString);
        //Role Role_GetById(int Id, string connectionString);
        //Role Role_GetByName(string Name, string connectionString);
        //int Role_Update(Role model, string connectionString);
        //int Role_Insert(Role model, string connectionString);
        //int Role_DeleteById(int Id, string connectionString);
        #endregion

        #region User 
        List<User> User_GetAll(string connectionString);
        //User User_GetById(int Id, string connectionString);
        User User_GetAuthenticatedUser(string loginName, string hashPassword, string connectionString);
        //int User_Update_Infos(User model, string connectionString);
        //int User_Update_Password(User model, string connectionString);
        //int User_Insert(User model, string connectionString);
        //int User_DeleteById(int Id, string connectionString);
        #endregion

        #region TM
        List<TM> TM_GetAll(string connectionString);
        //TM TM_GetById(int Id, string connectionString);
        TM TM_GetByName(string Name, string connectionString);
        int TM_Update(TM model, string connectionString);
        int TM_Insert(TM model, string connectionString);
        int TM_DeleteById(int Id, string connectionString);
        #endregion

        #region SensorType
        List<SensorType> SensorType_GetAll(string connectionString);
        //SensorType SensorType_GetById(int Id, string connectionString);
        //SensorType SensorType_GetByName(string name, string connectionString);
        int SensorType_Update(SensorType model, string connectionString);
        int SensorType_Insert(SensorType model, string connectionString);
        int SensorType_DeleteById(int Id, string connectionString);
        #endregion

        #region Sensor
        //List<Sensor> Sensor_GetByMachineId(int Id, string connectionString);
        //int Sensor_Update(Sensor model, string connectionString);
        //int Sensor_Insert(Sensor model, string connectionString);
        //int Sensor_Delete(Sensor model, string connectionString);
        //int Sensor_DeleteByMachineId(int Id, string connectionString);
        #endregion

        #region MachineType
        List<MachineType> MachineType_GetAll(string connectionString);
        //MachineType MachineType_GetById(int Id, string connectionString);
        //MachineType MachineType_GetByName(string name, string connectionString);
        int MachineType_Update(MachineType model, string connectionString);
        int MachineType_Insert(MachineType model, string connectionString);
        int MachineType_DeleteById(int Id, string connectionString);
        #endregion

        #region Machine
        //List<Machine> Machine_GetAll(string connectionString);
        //Machine Machine_GetById(int Id, string connectionString);
        //int Machine_Update(Machine model, string connectionString);
        //int Machine_Insert(Machine model, string connectionString);
        //int Machine_DeleteById(int Id, string connectionString);

        #endregion

        #region MachineName
        List<MachineName> MachineName_GetAll(string connectionString);
        //MachineName MachineName_GetById(int Id, string connectionString);
        int MachineName_Update(MachineName model, string connectionString);
        int MachineName_Insert(MachineName model, string connectionString);
        int MachineName_DeleteById(int Id, string connectionString);
        #endregion

        #region CalibData 
        ////List<CalibData> CalibResult_GetAll(string connectionString);
        //CalibData CalibData_GetById(int Id, string connectionString);
        //List<CalibData> CalibData_GetByCertificateId(int Id, string connectionString);
        //int CalibData_Update(CalibData model, string connectionString);
        //int CalibData_Insert(CalibData model, string connectionString);
        //int CalibData_DeleteById(int Id, string connectionString);
        #endregion

        #region DoseQuantity
        List<DoseQuantity> DoseQuantity_GetAll(string connectionString);
        List<DoseQuantity> DoseQuantity_GetActive(string connectionString);
        //DoseQuantity DoseQuantity_GetById(int Id, string connectionString);
        //DoseQuantity DoseQuantity_GetByNotation(string notation, string connectionString);
        int DoseQuantity_Update(DoseQuantity model, string connectionString);
        int DoseQuantity_Insert(DoseQuantity model, string connectionString);
        int DoseQuantity_DeleteById(int Id, string connectionString);
        #endregion

        #region RadQuantity
        List<RadQuantity> RadQuantity_GetAll(string connectionString);
        List<RadQuantity> RadQuantity_GetActive(string connectionString);
        //RadQuantity RadQuantity_GetById(int Id, string connectionString);
        int RadQuantity_Update(RadQuantity model, string connectionString);
        int RadQuantity_Insert(RadQuantity model, string connectionString);
        int RadQuantity_DeleteById(int Id, string connectionString);
        #endregion

        #region Certificate 
        //List<Certificate> Certificate_GetAll(string connectionString);
        List<Certificate> Certificate_GetFromDateToDate(DateTime FromDate, DateTime ToDate, string connectionString);
        //Certificate Certificate_GetById(int Id, string connectionString);
        //int Certificate_Update(Certificate model, string connectionString);
        int Certificate_Insert(Certificate model, string connectionString);
        //int Certificate_Delete(Certificate certificate, string connectionString);
        #endregion
    }
}
