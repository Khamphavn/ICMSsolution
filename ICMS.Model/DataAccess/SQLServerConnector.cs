using Dapper;
using ICMS.Model.Models;
using ICMS.Model.TableModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ICMS.Model.DataAccess
{
    public class SQLServerConnector : IDataConnection
    {
        #region Unit
        public List<Unit> Unit_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Unit> output = Unit_GetAll(conn);
                return output;
            }
        }
        private List<Unit> Unit_GetAll(SqlConnection conn)
        {
            List<Unit> output = conn.Query<Unit>("dbo.SpUnit_GetAll", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public List<Unit> Unit_GetActive(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Unit> output = Unit_GetActive(conn);
                return output;
            }
        }
        private List<Unit> Unit_GetActive(SqlConnection conn)
        {
            List<Unit> output = conn.Query<Unit>("dbo.SpUnit_GetActive", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public Unit Unit_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Unit output = Unit_GetById(Id, conn);
                return output;
            }
        }
        private Unit Unit_GetById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpUnit_GetById";
            var p = new DynamicParameters();
            p.Add("@UnitId", Id);

            try
            {
                Unit output = conn.Query<Unit>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch
            {
                return null;
            }
        }

        public int Unit_Insert(Unit model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = Unit_Insert(model, conn);
                return insertResult;
            }
        }
        private int Unit_Insert(Unit model, SqlConnection conn)
        {
            int output = 0;
            string procedure = "dbo.SpUnit_Insert";

            var p = new DynamicParameters();
            p.Add("@Name", model.Name);
            p.Add("IsActive", model.IsActive);
            p.Add("Order", model.Order);
            p.Add("@UnitId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.UnitId = p.Get<int>("@UnitId");
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int Unit_Update(Unit model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = Unit_Update(model, conn);
                return updateResult;
            }
        }
        private int Unit_Update(Unit model, SqlConnection conn)
        {
            int output = 0;

            string procedure = "dbo.SpUnit_Update";
            var p = new DynamicParameters();
            p.Add("@UnitID", model.UnitId);
            p.Add("@Name", model.Name);
            p.Add("@Order", model.Order);
            p.Add("@IsActive", model.IsActive);

            try
            {
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int Unit_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int deleteResult = Unit_DeleteById(Id, conn);
                return deleteResult;
            }
        }
        private int Unit_DeleteById(int Id, SqlConnection conn)
        {
            try
            {
                string procedure = "dbo.SpUnit_DeleteById";
                var p = new DynamicParameters();
                p.Add("@UnitId", Id);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region City
        public List<City> City_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<City> output = City_GetAll(conn);
                return output;
            }
        }
        private List<City> City_GetAll(SqlConnection conn)
        {
            List<City> output = conn.Query<City>("dbo.SpCity_GetAll", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public City City_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                City output = City_GetById(Id, conn);
                return output;
            }
        }
        private City City_GetById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpCity_GetById";
            var p = new DynamicParameters();
            p.Add("@CityId", Id);

            try
            {
                City output = conn.Query<City>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public City City_GetByName(string name, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                City output = City_GetByName(name, conn);
                return output;
            }
        }
        private City City_GetByName(string name, SqlConnection conn)
        {
            string procedure = "dbo.SpCity_GetByName";
            var p = new DynamicParameters();
            p.Add("@Name", name);

            try
            {
                City output = conn.Query<City>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public List<City> City_GetActive(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<City> output = City_GetActive(conn);
                return output;
            }
        }
        private List<City> City_GetActive(SqlConnection conn)
        {
            List<City> output = conn.Query<City>("dbo.SpCity_GetActive", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public int City_Update(City model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = City_Update(model, conn);
                return updateResult;
            }
        }
        private int City_Update(City model, SqlConnection conn)
        {
            int output = 0;

            string procedure = "dbo.SpCity_Update";
            var p = new DynamicParameters();
            p.Add("@CityId", model.CityId);
            p.Add("@Name", model.Name);
            p.Add("@PhoneCode", model.PhoneCode);
            p.Add("@IsActive", model.IsActive);

            try
            {
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int City_Insert(City model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = City_Insert(model, conn);
                return insertResult;
            }
        }
        private int City_Insert(City model, SqlConnection conn)
        {
            int output = 0;

            string procedure = "dbo.SpCity_Insert";
            var p = new DynamicParameters();
            p.Add("@Name", model.Name);
            p.Add("@PhoneCode", model.PhoneCode);
            p.Add("@IsActive", model.IsActive);
            p.Add("@CityId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.CityId = p.Get<int>("@CityId");
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int City_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = City_DeleteById(Id, conn);
                return output;
            }
        }
        private int City_DeleteById(int Id, SqlConnection conn)
        {
            try
            {
                string procedure = "dbo.SpCity_DeleteById";
                var p = new DynamicParameters();
                p.Add("@CityId", Id);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region Customer
        public List<Customer> Customer_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Customer> output = Customer_GetAll(conn);
                return output;
            }
        }
        private List<Customer> Customer_GetAll(SqlConnection conn)
        {
            string procedure = "SpCustomer_GetAll";
            string splitColumns = "CityId";


            List<Customer> output = conn.Query<Customer, City, Customer>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                map: (customerDTO, city) =>
                {
                    customerDTO.City = city;
                    return customerDTO;
                },
                splitOn: splitColumns
                ).ToList();

            return output;
        }

        public Customer Customer_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Customer output = Customer_GetById(Id, conn);
                return output;
            }
        }
        private Customer Customer_GetById(int Id, SqlConnection conn)
        {
            string procedure = "SpCustomer_GetById";
            string splitColumns = "CityId";
            var p = new DynamicParameters();
            p.Add("@CustomerId", Id);

            Customer output = conn.Query<Customer, City, Customer>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                param: p,
                map: (customer, city) =>
                {
                    customer.City = city;
                    return customer;
                },
                splitOn: splitColumns
                ).FirstOrDefault();

            return output;
        }

        public Customer Customer_GetByFullName(string FullName, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Customer output = Customer_GetByFullName(FullName, conn);
                return output;
            }
        }
        private Customer Customer_GetByFullName(string FullName, SqlConnection conn)
        {
            string procedure = "SpCustomer_GetByFullName";
            string splitColumns = "CityId";
            var p = new DynamicParameters();
            p.Add("@FullName", FullName);

            Customer output = conn.Query<Customer, City, Customer>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                param: p,
                map: (customer, city) =>
                {
                    customer.City = city;
                    return customer;
                },
                splitOn: splitColumns
                ).FirstOrDefault();

            return output;
        }

        public int Customer_Update(Customer model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = Customer_Update(model, conn);
                return updateResult;
            }
        }
        private int Customer_Update(Customer model, SqlConnection conn)
        {
            int output = 0;

            string procedure = "dbo.SpCustomer_Update";
            var p = new DynamicParameters();
            p.Add("@CustomerId", model.CustomerId);
            p.Add("@ShortName", model.ShortName);
            p.Add("@FullName", model.FullName);
            p.Add("@Address", model.Address);

            if (model.City != null)
            {
                p.Add("@CityId", model.City.CityId);
            }
            else
            {
                p.Add("@CityId", null);
            }

            try
            {
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int Customer_Insert(Customer model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = Customer_Insert(model, conn);
                return insertResult;
            }
        }
        private int Customer_Insert(Customer model, SqlConnection conn)
        {
            int output = 0;

            string procedure = "dbo.SpCustomer_Insert";
            var p = new DynamicParameters();
            p.Add("@ShortName", model.ShortName);
            p.Add("@FullName", model.FullName);
            p.Add("@Address", model.Address);

            if (model.City != null)
            {
                p.Add("@CityId", model.City.CityId);
            }
            else
            {
                p.Add("@CityId", null);
            }

            p.Add("@CustomerId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.CustomerId = p.Get<int>("@CustomerId");
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        public int Customer_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = Customer_DeleteById(Id, conn);
                return output;
            }
        }
        private int Customer_DeleteById(int Id, SqlConnection conn)
        {
            try
            {
                string procedure = "dbo.SpCustomer_DeleteById";
                var p = new DynamicParameters();
                p.Add("@CustomerId", Id);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Role
        public List<Role> Role_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Role> output = Role_GetAll(conn);
                return output;
            }
        }
        private List<Role> Role_GetAll(SqlConnection conn)
        {
            List<RoleTable> roleTables = RoleTable_GetAll(conn);

            List<Role> roles = new List<Role>();
            foreach (RoleTable roleTable in roleTables)
            {
                List<RoleAction> roleActions = RoleTableToRoleActions(roleTable);

                Role role = new Role() 
                { 
                   RoleId = roleTable.RoleId,
                   Name   = roleTable.Name,
                   RoleActions = roleActions
                };

                roles.Add(role);
            }

            return roles;
        }
        private List<RoleTable> RoleTable_GetAll(SqlConnection conn)
        {
            List<RoleTable> output = conn.Query<RoleTable>("dbo.SpRoleTable_GetAll", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }


        //public Role Role_GetByName(string name, string connectionString)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        Role output = Role_GetByName(name, conn);
        //        return output;
        //    }
        //}
        //private Role Role_GetByName(string name, SqlConnection conn)
        //{
        //    string procedure = "dbo.SpRole_GetByName";
        //    var p = new DynamicParameters();
        //    p.Add("@Name", name);

        //    try
        //    {
        //        Role output = conn.Query<Role>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //        return output;
        //    }
        //    catch (SqlException ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return null;
        //    }
        //}

        public Role Role_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Role output = Role_GetById(Id, conn);
                return output;
            }
        }
        private Role Role_GetById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpRoleTable_GetById";
            var p = new DynamicParameters();
            p.Add("@RoleId", Id);

            try
            {
                RoleTable roleTable = conn.Query<RoleTable>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();

                List<RoleAction> roleActions = RoleTableToRoleActions(roleTable);

                Role role = new Role()
                {
                    RoleId = roleTable.RoleId,
                    Name = roleTable.Name,
                    RoleActions = roleActions
                };


                return role;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Role_Update(Role model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = Role_Update(model, conn);
                return updateResult;
            }
        }
        private int Role_Update(Role role, SqlConnection conn)
        {
            int output = 0;

            RoleTable roleTable = ConvertRoleToRoleTable(role);

            string procedure = "dbo.SpRoleTable_Update";
            var p = new DynamicParameters();
            p.Add("@RoleId", roleTable.RoleId);
            p.Add("@Name", roleTable.Name);
            p.Add("@User", roleTable.User);
            p.Add("@Permission", roleTable.Permission);
            p.Add("@BackupDB", roleTable.BackupDB);
            p.Add("@RestoreDB", roleTable.RestoreDB);
            p.Add("@RadQuantity", roleTable.RadQuantity);
            p.Add("@DoseQuantity", roleTable.DoseQuantity);
            p.Add("@Unit", roleTable.Unit);
            p.Add("@TM", roleTable.TM);
            p.Add("@Certificate", roleTable.Certificate);
            p.Add("@Customer", roleTable.Customer);
            p.Add("@City", roleTable.City);
            p.Add("@MachineName", roleTable.MachineName);
            p.Add("@MachineType", roleTable.MachineType);
            p.Add("@SensorType", roleTable.SensorType);

            try
            {
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        //public int Role_Insert(Role model, string connectionString)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        int insertResult = Role_Insert(model, conn);
        //        return insertResult;
        //    }
        //}
        //private int Role_Insert(Role model, SqlConnection conn)
        //{
        //    int output = 0;

        //    string procedure = "dbo.SpRole_Insert";
        //    var p = new DynamicParameters();
        //    p.Add("@Name", model.Name);
        //    p.Add("@RoleId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

        //    try
        //    {
        //        conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
        //        model.RoleId = p.Get<int>("@RoleId");
        //        output = 1;
        //    }
        //    catch (SqlException)
        //    {
        //        throw;
        //    }

        //    return output;
        //}

        //public int Role_DeleteById(int Id, string connectionString)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        int output = Role_DeleteById(Id, conn);
        //        return output;
        //    }
        //}

        //private int Role_DeleteById(int Id, SqlConnection conn)
        //{
        //    try
        //    {
        //        string procedure = "dbo.SpRole_DeleteById";
        //        var p = new DynamicParameters();
        //        p.Add("@RoleId", Id);
        //        conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
        //        return 1;
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private bool ConvertAccessCodeToRoleAction(int accessCode, string action)
        {
            string actionBinary = Convert.ToString(accessCode, 2).PadLeft(5);
            int index ;

            switch (action)
            {
                case "View":
                    index = 0;
                    break;
                    
                case "Add":
                    index = 1;
                    break;

                case "Edit":
                    index = 2;
                    break;

                case "Delete":
                    index = 3;
                    break;

                case "Print":
                    index = 4;
                    break;

                default:
                    index = 4;
                    break;
            }

            bool actionPermission = actionBinary[index] == '1';

            return actionPermission;
        }

        private int ConvertRoleActionToAccessCode(RoleAction roleAction)
        {
            int output = 0;

            string accessCodeBinaryString = "";

            accessCodeBinaryString += roleAction.View ? 1 : 0;

            if(roleAction.Add == null)
            {
                accessCodeBinaryString += 0;
            }
            else
            {
                accessCodeBinaryString += (bool)roleAction.Add ? 1 : 0;
            }

            if (roleAction.Edit == null)
            {
                accessCodeBinaryString += 0;
            }
            else
            {
                accessCodeBinaryString += (bool)roleAction.Edit ? 1 : 0;
            }

            if (roleAction.Delete == null)
            {
                accessCodeBinaryString += 0;
            }
            else
            {
                accessCodeBinaryString += (bool)roleAction.Delete ? 1 : 0;
            }

            if (roleAction.Print == null)
            {
                accessCodeBinaryString += 0;
            }
            else
            {
                accessCodeBinaryString += (bool)roleAction.Print ? 1 : 0;
            }

            output = Convert.ToInt32(accessCodeBinaryString, 2);


            return output;
        }


        private RoleTable ConvertRoleToRoleTable(Role role)
        {
            RoleTable roleTable = new RoleTable();

            roleTable.RoleId = role.RoleId;
            roleTable.Name = role.Name;
            roleTable.User = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "User"));
            roleTable.Permission = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "Permission"));
            roleTable.BackupDB = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "BackupDB"));
            roleTable.RestoreDB = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "RestoreDB"));
            roleTable.RadQuantity = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "RadQuantity"));
            roleTable.DoseQuantity = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "DoseQuantity"));
            roleTable.Unit = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "Unit"));
            roleTable.TM = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "TM"));
            roleTable.Certificate = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "Certificate"));
            roleTable.Customer = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "Customer"));
            roleTable.City = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "City"));
            roleTable.MachineName = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "MachineName"));
            roleTable.MachineType = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "MachineType"));
            roleTable.SensorType = ConvertRoleActionToAccessCode(role.RoleActions.FirstOrDefault(s => s.ActionCode == "SensorType"));

            return roleTable;
        }


        private List<RoleAction> RoleTableToRoleActions(RoleTable roleTable)
        {
            List<RoleAction> roleActions = new List<RoleAction>();

            RoleAction userAction = new RoleAction()
            {
                STT = 1,
                ActionCode = "User",
                ActionName = "Quản lý tài khoản",
                View = ConvertAccessCodeToRoleAction(roleTable.User, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.User, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.User, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.User, "Delete"),
                Print = null //ConvertAccessCodeToRoleAction(roleTable.User, "Print")
            };
            roleActions.Add(userAction);

            RoleAction permissionAction = new RoleAction()
            {
                STT = 2,
                ActionCode = "Permission",
                ActionName = "Quản lý nhóm",
                View = ConvertAccessCodeToRoleAction(roleTable.Permission, "View"),
                Add = null, //ConvertAccessCodeToRoleAction(roleTable.Permission, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.Permission, "Edit"),
                Delete = null, // ConvertAccessCodeToRoleAction(roleTable.Permission, "Delete"),
                Print = null //ConvertAccessCodeToRoleAction(roleTable.Permission, "Print")
            };
            roleActions.Add(permissionAction);

            RoleAction backupDBAction = new RoleAction()
            {
                STT = 3,
                ActionCode = "BackupDB",
                ActionName = "Sao lưu dữ liệu",
                View = ConvertAccessCodeToRoleAction(roleTable.BackupDB, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.BackupDB, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.BackupDB, "Edit"),
                Delete = null, //ConvertAccessCodeToRoleAction(roleTable.BackupDB, "Delete"),
                Print = null // = ConvertAccessCodeToRoleAction(roleTable.BackupDB, "Print")
            };
            roleActions.Add(backupDBAction);

            RoleAction restoreDBAction = new RoleAction()
            {
                STT = 4,
                ActionCode = "RestoreDB",
                ActionName = "Khôi phục dữ liệu",
                View = ConvertAccessCodeToRoleAction(roleTable.RestoreDB, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.RestoreDB, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.RestoreDB, "Edit"),
                Delete = null, // ConvertAccessCodeToRoleAction(roleTable.RestoreDB, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.RestoreDB, "Print")
            };
            roleActions.Add(restoreDBAction);

            RoleAction radQuantityAction = new RoleAction()
            {
                STT = 5,
                ActionCode = "RadQuantity",
                ActionName = "Phẩm chất bức xạ",
                View = ConvertAccessCodeToRoleAction(roleTable.RadQuantity, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.RadQuantity, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.RadQuantity, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.RadQuantity, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.RadQuantity, "Print")
            };
            roleActions.Add(radQuantityAction);

            RoleAction doseQuantityAction = new RoleAction()
            {
                STT = 6,
                ActionCode = "DoseQuantity",
                ActionName = "Đại lượng liều bức xạ",
                View = ConvertAccessCodeToRoleAction(roleTable.DoseQuantity, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.DoseQuantity, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.DoseQuantity, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.DoseQuantity, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.DoseQuantity, "Print")
            };
            roleActions.Add(doseQuantityAction);

            RoleAction unitAction = new RoleAction()
            {
                STT = 7,
                ActionCode = "Unit",
                ActionName = "Đơn vị đo",
                View = ConvertAccessCodeToRoleAction(roleTable.Unit, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.Unit, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.Unit, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.Unit, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.Unit, "Print")
            };
            roleActions.Add(unitAction);

            RoleAction TMAction = new RoleAction()
            {
                STT = 8,
                ActionCode = "TM",
                ActionName = "Danh sách TM",
                View = ConvertAccessCodeToRoleAction(roleTable.TM, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.TM, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.TM, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.TM, "Delete"),
                Print = null //  ConvertAccessCodeToRoleAction(roleTable.TM, "Print")
            };
            roleActions.Add(TMAction);

            RoleAction certificateAction = new RoleAction()
            {
                STT = 9,
                ActionCode = "Certificate",
                ActionName = "Chứng chỉ",
                View = ConvertAccessCodeToRoleAction(roleTable.Certificate, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.Certificate, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.Certificate, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.Certificate, "Delete"),
                Print = ConvertAccessCodeToRoleAction(roleTable.Certificate, "Print")
            };
            roleActions.Add(certificateAction);

            RoleAction customerAction = new RoleAction()
            {
                STT = 10,
                ActionCode = "Customer",
                ActionName = "Khách hàng",
                View = ConvertAccessCodeToRoleAction(roleTable.Customer, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.Customer, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.Customer, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.Customer, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.Customer, "Print")
            };
            roleActions.Add(customerAction);

            RoleAction cityAction = new RoleAction()
            {
                STT = 11,
                ActionCode = "City",
                ActionName = "Tỉnh/thành",
                View = ConvertAccessCodeToRoleAction(roleTable.City, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.City, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.City, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.City, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.City, "Print")
            };
            roleActions.Add(cityAction);

            RoleAction machineNameAction = new RoleAction()
            {
                STT = 12,
                ActionCode = "MachineName",
                ActionName = "Tên thiết bị",
                View = ConvertAccessCodeToRoleAction(roleTable.MachineName, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.MachineName, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.MachineName, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.MachineName, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.MachineName, "Print")
            };
            roleActions.Add(machineNameAction);

            RoleAction machineTypeAction = new RoleAction()
            {
                STT = 13,
                ActionCode = "MachineType",
                ActionName = "Loại thiết bị",
                View = ConvertAccessCodeToRoleAction(roleTable.MachineType, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.MachineType, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.MachineType, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.MachineType, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.MachineType, "Print")
            };
            roleActions.Add(machineTypeAction);

            RoleAction sensorTypeAction = new RoleAction()
            {
                STT = 14,
                ActionCode = "SensorType",
                ActionName = "Loại đầu dò",
                View = ConvertAccessCodeToRoleAction(roleTable.SensorType, "View"),
                Add = ConvertAccessCodeToRoleAction(roleTable.SensorType, "Add"),
                Edit = ConvertAccessCodeToRoleAction(roleTable.SensorType, "Edit"),
                Delete = ConvertAccessCodeToRoleAction(roleTable.SensorType, "Delete"),
                Print = null // ConvertAccessCodeToRoleAction(roleTable.SensorType, "Print")
            };
            roleActions.Add(sensorTypeAction);


            return roleActions;
        }

        #endregion

        #region User
        public List<User> User_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<User> output = User_GetAll(conn);
                return output;
            }
        }
        private List<User> User_GetAll(SqlConnection conn)
        {
            string procedure = "SpUserTable_GetAll";

            List<UserTable> UserTables = conn.Query<UserTable>(
                sql: procedure,
                commandType: CommandType.StoredProcedure
                ).ToList();

            List<User> Users = new List<User>();

            foreach (var userTable in UserTables)
            {
                User user = new User();

                user.UserId = userTable.UserId;
                user.LoginName = userTable.LoginName;
                user.FullName = userTable.FullName;
                user.Password = userTable.Password;
                user.IsActive = userTable.IsActive;
                user.Role = Role_GetById(userTable.RoleId, conn);

                Users.Add(user);
            }


            return Users;
        }

        public User User_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                User output = User_GetById(Id, conn);
                return output;
            }
        }
        private User User_GetById(int Id, SqlConnection conn)
        {
            string procedure = "SpUser_GetById";
            string splitColumns = "RoleId";
            var p = new DynamicParameters();
            p.Add("@UserId", Id);

            User output = conn.Query<User, Role, User>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                param: p,
                map: (userDTO, role) =>
                {
                    userDTO.Role = role;
                    return userDTO;
                },
                splitOn: splitColumns
                ).FirstOrDefault();

            return output;
        }

        public User User_GetAuthenticatedUser(string loginName, string hashPassword, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                User output = User_GetAuthenticatedUser(loginName, hashPassword, conn);
                return output;
            }
        }
        private User User_GetAuthenticatedUser(string loginName, string hashPassword, SqlConnection conn)
        {
            string procedure = "SpUserTable_GetAuthenticatedUser";
            var p = new DynamicParameters();
            p.Add("@LoginName", loginName);
            p.Add("@Password", hashPassword);

            try
            {
                UserTable userTable = conn.Query<UserTable>(
                    sql: procedure,
                    commandType: CommandType.StoredProcedure,
                    param: p
                    ).FirstOrDefault();


                if(userTable != null)
                {
                    Role role = Role_GetById(userTable.RoleId, conn);

                    User user = new User()
                    {
                        UserId = userTable.UserId,
                        LoginName = userTable.LoginName,
                        FullName = userTable.FullName,
                        Password = userTable.Password,
                        Role = role,
                        IsActive = userTable.IsActive
                    };

                    return user;
                }
                else
                {
                    return null;
                }

               
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int User_Update_Infos(User model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = User_Update_Infos(model, conn);
                return updateResult;
            }
        }
        private int User_Update_Infos(User model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpUser_Update_Infos";
                var p = new DynamicParameters();
                p.Add("@UserId", model.UserId);
                p.Add("@LoginName", model.LoginName);
                p.Add("@FullName", model.FullName);
                //p.Add("@Password", model.Password);
                p.Add("@RoleId", model.Role.RoleId);
                p.Add("@IsActive", model.IsActive);
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        public int User_Update_Password(User model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = User_Update_Password(model, conn);
                return updateResult;
            }
        }
        private int User_Update_Password(User model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpUser_Update_Password";

                var p = new DynamicParameters();
                p.Add("@UserId", model.UserId);
                p.Add("@Password", model.Password);
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }

            return output;

        }

        public int User_Insert(User model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = User_Insert(model, conn);
                return insertResult;
            }
        }
        private int User_Insert(User model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpUser_Insert";
                var p = new DynamicParameters();
                p.Add("@LoginName", model.LoginName);
                p.Add("@FullName", model.FullName);
                p.Add("@Password", model.Password);
                p.Add("@RoleId", model.Role.RoleId);
                p.Add("@IsActive", model.IsActive);
                p.Add("@UserId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.UserId = p.Get<int>("@UserId");

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int User_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = User_DeleteById(Id, conn);
                return output;
            }
        }
        private int User_DeleteById(int Id, SqlConnection conn)
        {
            try
            {
                string procedure = "dbo.SpUser_DeleteById";
                var p = new DynamicParameters();
                p.Add("@UserId", Id);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region TM
        public List<TM> TM_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<TM> output = TM_GetAll(conn);
                return output;
            }
        }
        private List<TM> TM_GetAll(SqlConnection conn)
        {
            List<TM> output = conn.Query<TM>("dbo.SpTM_GetAll", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public TM TM_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                TM output = TM_GetById(Id, conn);
                return output;
            }
        }
        private TM TM_GetById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpTM_GetById";
            var p = new DynamicParameters();
            p.Add("@TMId", Id);

            try
            {
                TM output = conn.Query<TM>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public TM TM_GetByName(string Name, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                TM output = TM_GetByName(Name, conn);
                return output;
            }
        }
        private TM TM_GetByName(string Name, SqlConnection conn)
        {
            string procedure = "dbo.SpTM_GetByName";
            var p = new DynamicParameters();
            p.Add("@Name", Name);

            try
            {
                TM output = conn.Query<TM>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int TM_Update(TM model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = TM_Update(model, conn);
                return updateResult;
            }
        }
        private int TM_Update(TM model, SqlConnection conn)
        {
            string procedure = "dbo.SpTM_Update";

            var p = new DynamicParameters();
            p.Add("@TMId", model.TMId);
            p.Add("@Name", model.Name);

            try
            {
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public int TM_Insert(TM model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = TM_Insert(model, conn);
                return insertResult;
            }
        }
        private int TM_Insert(TM model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpTM_Insert";
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@TMId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.TMId = p.Get<int>("@TMId");

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        public int TM_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = TM_DeleteById(Id, conn);
                return insertResult;
            }
        }
        private int TM_DeleteById(int Id, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpTM_DeleteById";
                var p = new DynamicParameters();
                p.Add("@TMId", Id);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        #endregion

        #region DoseQuantity
        public List<DoseQuantity> DoseQuantity_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<DoseQuantity> output = DoseQuantity_GetAll(conn);
                return output;
            }
        }
        private List<DoseQuantity> DoseQuantity_GetAll(SqlConnection conn)
        {
            List<DoseQuantity> output = conn.Query<DoseQuantity>("dbo.SpDoseQuantity_GetAll", commandType: CommandType.StoredProcedure).ToList();
            return output;

        }

        public List<DoseQuantity> DoseQuantity_GetActive(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<DoseQuantity> output = DoseQuantity_GetActive(conn);
                return output;
            }
        }

        private List<DoseQuantity> DoseQuantity_GetActive(SqlConnection conn)
        {
            List<DoseQuantity> output = conn.Query<DoseQuantity>("dbo.SpDoseQuantity_GetActive", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public DoseQuantity DoseQuantity_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DoseQuantity output = DoseQuantity_GetById(Id, conn);
                return output;
            }
        }
        private DoseQuantity DoseQuantity_GetById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpDoseQuantity_GetById";
            var p = new DynamicParameters();
            p.Add("@DoseQuantityId", Id);

            try
            {
                DoseQuantity output = conn.Query<DoseQuantity>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public DoseQuantity DoseQuantity_GetByNotation(string notation, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DoseQuantity output = DoseQuantity_GetByNotation(notation, conn);
                return output;
            }
        }
        private DoseQuantity DoseQuantity_GetByNotation(string notation, SqlConnection conn)
        {
            string procedure = "dbo.SpDoseQuantity_GetByNotation";
            var p = new DynamicParameters();
            p.Add("@Notation", notation);

            try
            {
                DoseQuantity output = conn.Query<DoseQuantity>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public int DoseQuantity_Update(DoseQuantity model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = DoseQuantity_Update(model, conn);
                return updateResult;
            }
        }
        private int DoseQuantity_Update(DoseQuantity model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpDoseQuantity_Update";
                var p = new DynamicParameters();
                p.Add("@doseQuantityId", model.DoseQuantityId);
                p.Add("@NameVN", model.NameVN);
                p.Add("@NameEN", model.NameEN);
                p.Add("@Notation", model.Notation);
                p.Add("@IsActive", model.IsActive);
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int DoseQuantity_Insert(DoseQuantity model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = DoseQuantity_Insert(model, conn);
                return insertResult;
            }
        }
        private int DoseQuantity_Insert(DoseQuantity model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpDoseQuantity_Insert";
                var p = new DynamicParameters();
                p.Add("@NameVN", model.NameVN);
                p.Add("@NameEN", model.NameEN);
                p.Add("@Notation", model.Notation);
                p.Add("@IsActive", model.IsActive);
                p.Add("@DoseQuantityId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.DoseQuantityId = p.Get<int>("@DoseQuantityId");

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int DoseQuantity_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = DoseQuantity_DeleteById(Id, conn);
                return output;
            }
        }

        private int DoseQuantity_DeleteById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpDoseQuantity_DeleteById";
            var p = new DynamicParameters();
            p.Add("@DoseQuantityId", Id);

            try
            {
                conn.Query<RadQuantity>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }


        #endregion

        #region RadQuantity
        public List<RadQuantity> RadQuantity_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<RadQuantity> output = RadQuantity_GetAll(conn);
                return output;
            }
        }
        private List<RadQuantity> RadQuantity_GetAll(SqlConnection conn)
        {
            try
            {
                List<RadQuantity> output = conn.Query<RadQuantity>("dbo.SpRadQuantity_GetAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public List<RadQuantity> RadQuantity_GetActive(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<RadQuantity> output = RadQuantity_GetActive(conn);
                return output;
            }
        }
        private List<RadQuantity> RadQuantity_GetActive(SqlConnection conn)
        {
            try
            {
                List<RadQuantity> output = conn.Query<RadQuantity>("dbo.SpRadQuantity_GetActive", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public RadQuantity RadQuantity_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                RadQuantity output = RadQuantity_GetById(Id, conn);
                return output;
            }
        }
        private RadQuantity RadQuantity_GetById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpRadQuantity_GetById";
            var p = new DynamicParameters();
            p.Add("@RadQuantityId", Id);

            try
            {
                RadQuantity output = conn.Query<RadQuantity>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int RadQuantity_Update(RadQuantity model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = RadQuantity_Update(model, conn);
                return updateResult;
            }
        }
        private int RadQuantity_Update(RadQuantity model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpRadQuantity_Update";
                var p = new DynamicParameters();
                p.Add("@RadQuantityId", model.RadQuantityId);
                p.Add("@NameVN", model.NameVN);
                p.Add("@NameEN", model.NameEN);
                p.Add("@Energy", model.Energy);
                p.Add("@ReUnc", model.ReUnc);
                p.Add("@IsActive", model.IsActive);
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int RadQuantity_Insert(RadQuantity model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = RadQuantity_Insert(model, conn);
                return insertResult;
            }
        }
        private int RadQuantity_Insert(RadQuantity model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpRadQuantity_Insert";
                var p = new DynamicParameters();
                p.Add("@NameVN", model.NameVN);
                p.Add("@NameEN", model.NameEN);
                p.Add("@Energy", model.Energy);
                p.Add("@ReUnc", model.ReUnc);
                p.Add("@IsActive", model.IsActive);
                p.Add("@RadQuantityId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.RadQuantityId = p.Get<int>("@RadQuantityId");

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int RadQuantity_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = RadQuantity_DeleteById(Id, conn);
                return output;
            }
        }
        private int RadQuantity_DeleteById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpRadQuantity_DeleteById";
            var p = new DynamicParameters();
            p.Add("@RadQuantityId", Id);

            try
            {
                conn.Query<RadQuantity>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        #endregion

        #region SensorType
        public List<SensorType> SensorType_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<SensorType> output = SensorType_GetAll(conn);
                return output;
            }
        }
        private List<SensorType> SensorType_GetAll(SqlConnection conn)
        {
            List<SensorType> output = conn.Query<SensorType>("dbo.SpSensorType_GetAll", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public SensorType SensorType_GetById(int Id, string connectionString)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SensorType output = SensorType_GetById(Id, conn);
                return output;
            }
        }
        private SensorType SensorType_GetById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpSensorType_GetById";
            var p = new DynamicParameters();
            p.Add("@SensorTypeId", Id);

            try
            {
                SensorType output = conn.Query<SensorType>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public SensorType SensorType_GetByName(string name, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SensorType output = SensorType_GetByName(name, conn);
                return output;
            }
        }
        private SensorType SensorType_GetByName(string name, SqlConnection conn)
        {
            string procedure = "dbo.SpSensorType_GetByName";
            var p = new DynamicParameters();
            p.Add("@Name", name);

            try
            {
                SensorType output = conn.Query<SensorType>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public int SensorType_Update(SensorType model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = SensorType_Update(model, conn);
                return updateResult;
            }
        }
        private int SensorType_Update(SensorType model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpSensorType_Update";
                var p = new DynamicParameters();
                p.Add("@SensorTypeId", model.SensorTypeId);
                p.Add("@Name", model.Name);
                p.Add("@IsActive", model.IsActive);
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        public int SensorType_Insert(SensorType model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = SensorType_Insert(model, conn);
                return insertResult;
            }
        }
        private int SensorType_Insert(SensorType model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpSensorType_Insert";
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@IsActive", model.IsActive);
                p.Add("@SensorTypeId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.SensorTypeId = p.Get<int>("@SensorTypeId");

                output = 0;
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        public int SensorType_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = SensorType_DeleteById(Id, conn);
                return output;
            }
        }
        private int SensorType_DeleteById(int Id, SqlConnection conn)
        {
            try
            {
                string procedure = "dbo.SpSensorType_DeleteById";
                var p = new DynamicParameters();
                p.Add("@SensorTypeId", Id);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region Sensor
        //public Sensor Sensor_GetById(int Id, string connectionString)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        Sensor output = Sensor_GetById(Id, conn);
        //        return output;
        //    }
        //}
        //private Sensor Sensor_GetById(int Id, SqlConnection conn)
        //{
        //    string procedure = "SpSensor_GetById";
        //    string splitColumns = "SensorTypeId";
        //    var p = new DynamicParameters();
        //    p.Add("@SensorId", Id);

        //    Sensor output = conn.Query<Sensor, SensorType, Sensor>(
        //        sql: procedure,
        //        commandType: CommandType.StoredProcedure,
        //        param: p,
        //        map: (sensorDTO, sensorType) =>
        //        {
        //            sensorDTO.SensorType = sensorType;
        //            return sensorDTO;
        //        },
        //        splitOn: splitColumns
        //        ).FirstOrDefault();

        //    return output;
        //}

        public List<Sensor> Sensor_GetByMachineId(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Sensor> output = Sensor_GetByMachineId(Id, conn);
                return output;
            }
        }
        private List<Sensor> Sensor_GetByMachineId(int Id, SqlConnection conn)
        {
            string procedure = "SpSensor_GetByMachineId";
            string splitColumns = "SensorTypeId";
            var p = new DynamicParameters();
            p.Add("@MachineId", Id);

            List<Sensor> output = conn.Query<Sensor, SensorType, Sensor>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                param: p,
                map: (sensor, sensorType) =>
                {
                    sensor.SensorType = sensorType;
                    return sensor;
                },
                splitOn: splitColumns
                ).ToList();

            return output;
        }

        public int Sensor_Update(Sensor model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = Sensor_Update(model, conn);
                return updateResult;
            }
        }
        private int Sensor_Update(Sensor model, SqlConnection conn)
        {
            SensorType updateSensorType = SensorType_GetByName(model.SensorType.Name, conn);

            string procedure = "dbo.SpSensor_Update";

            var p = new DynamicParameters();
            p.Add("@SensorId", model.SensorId);
            p.Add("@MachineId", model.MachineId);
            p.Add("@SensorTypeId", model.SensorType.SensorTypeId);
            p.Add("@Model", model.Model);
            p.Add("@Serial", model.Serial);

            try
            {
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return 0;
                throw;
            }


        }

        public int Sensor_Insert(Sensor model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = Sensor_Insert(model, conn);
                return insertResult;
            }
        }
        private int Sensor_Insert(Sensor model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpSensorTable_Insert";
                var p = new DynamicParameters();
                p.Add("@MachineId", model.MachineId);


                if (model.SensorType != null)
                {
                    if (model.SensorType.SensorTypeId != 0)
                    {
                        p.Add("@SensorTypeId", model.SensorType.SensorTypeId);
                    }
                    else
                    {
                        p.Add("@SensorTypeId", null);
                    }
                }
                else
                {
                    p.Add("@SensorTypeId", null);
                }
                p.Add("@Model", model.Model);
                p.Add("@Serial", model.Serial);
                p.Add("@SensorId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.SensorId = p.Get<int>("@SensorId");

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int Sensor_Delete(Sensor model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int deleteResult = Sensor_Delete(model, conn);
                return deleteResult;
            }
        }
        private int Sensor_Delete(Sensor model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpSensor_Delete";
                var p = new DynamicParameters();
                p.Add("@SensorId", model.SensorId);
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }


        public int Sensor_DeleteByMachineId(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int deleteResult = Sensor_DeleteByMachineId(Id, conn);
                return deleteResult;
            }
        }
        private int Sensor_DeleteByMachineId(int Id, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpSensor_DeleteByMachineId";
                var p = new DynamicParameters();
                p.Add("@MachineId", Id);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        #endregion


        #region MachineType
        public List<MachineType> MachineType_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<MachineType> output = MachineType_GetAll(conn);
                return output;
            }
        }
        private List<MachineType> MachineType_GetAll(SqlConnection conn)
        {
            List<MachineType> output = conn.Query<MachineType>("dbo.SpMachineType_GetAll", commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public MachineType MachineType_GetByName(string name, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                MachineType output = MachineType_GetByName(name, conn);
                return output;
            }
        }
        private MachineType MachineType_GetByName(string name, SqlConnection conn)
        {
            string procedure = "dbo.SpMachineType_GetByName";
            var p = new DynamicParameters();
            p.Add("@Name", name);

            try
            {
                MachineType output = conn.Query<MachineType>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public MachineType MachineType_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                MachineType output = MachineType_GetById(Id, conn);
                return output;
            }
        }
        private MachineType MachineType_GetById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpMachineType_GetById";
            var p = new DynamicParameters();
            p.Add("@MachineTypeId", Id);

            try
            {
                MachineType output = conn.Query<MachineType>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return output;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public int MachineType_Update(MachineType model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = MachineType_Update(model, conn);
                return updateResult;
            }
        }
        private int MachineType_Update(MachineType model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpMachineType_Update";
                var p = new DynamicParameters();
                p.Add("@MachineTypeId", model.MachineTypeId);
                p.Add("@Name", model.Name);
                p.Add("@IsActive", model.IsActive);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }
            return output;
        }

        public int MachineType_Insert(MachineType model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = MachineType_Insert(model, conn);
                return insertResult;
            }
        }
        private int MachineType_Insert(MachineType model, SqlConnection conn)
        {
            int output = 0;
            try
            {
                string procedure = "dbo.SpMachineType_Insert";
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@IsActive", model.IsActive);
                p.Add("@MachineTypeId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.MachineTypeId = p.Get<int>("@MachineTypeId");

                output = 1;
            }
            catch (SqlException)
            {
                throw;
            }

            return output;
        }

        public int MachineType_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = MachineType_DeleteById(Id, conn);
                return output;
            }
        }
        private int MachineType_DeleteById(int Id, SqlConnection conn)
        {
            try
            {
                string procedure = "dbo.SpMachineType_DeleteById";
                var p = new DynamicParameters();
                p.Add("@MachineTypeId", Id);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region Machine
        public List<Machine> Machine_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Machine> output = Machine_GetAll(conn);
                return output;
            }
        }
        private List<Machine> Machine_GetAll(SqlConnection conn)
        {
            string procedure = "SpMachine_GetAll";
            string splitColumns = "MachineTypeId";

            List<Machine> output = conn.Query<Machine, MachineType, Machine>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                map: (machine, machineType) =>
                {
                    machine.MachineType = machineType;
                    return machine;
                },
                splitOn: splitColumns
                ).ToList();

            foreach (Machine machine in output)
            {
                machine.Sensors = Sensor_GetByMachineId(machine.MachineId, conn);
            }

            return output;
        }

        public Machine Machine_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Machine output = Machine_GetById(Id, conn);
                return output;
            }
        }
        private Machine Machine_GetById(int Id, SqlConnection conn)
        {
            string procedure = "SpMachine_GetById";
            string splitColumns = "MachineTypeId";
            var p = new DynamicParameters();
            p.Add("@MachineId", Id);

            Machine output = conn.Query<Machine, MachineType, Machine>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                param: p,
                map: (machine, machineType) =>
                {
                    machine.MachineType = machineType;
                    return machine;
                },
                splitOn: splitColumns
                ).FirstOrDefault();


            output.Sensors = Sensor_GetByMachineId(output.MachineId, conn);

            return output;
        }

        public int Machine_Update(Machine model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = Machine_Update(model, conn);
                return updateResult;
            }
        }
        private int Machine_Update(Machine model, SqlConnection conn)
        {
            // Step 1: Update Sensor first
            List<Sensor> existedSensors = Sensor_GetByMachineId(model.MachineId, conn);

            int nbrExistedSensors = existedSensors.Count();
            int nbrNewSensors = model.Sensors.Count();

            int updateSensorResult = 0;
            int insertSensorResult = 0;
            int deleteSensorResult = 0;
            if (nbrNewSensors == nbrExistedSensors)
            {
                for (int i = 0; i < nbrExistedSensors; i++)
                {
                    existedSensors[i].MachineId = model.MachineId;
                    existedSensors[i].SensorType = model.Sensors[i].SensorType;
                    existedSensors[i].Model = model.Sensors[i].Model;
                    existedSensors[i].Serial = model.Sensors[i].Serial;

                    updateSensorResult = Sensor_Update(existedSensors[i], conn);
                }
            }
            if (nbrNewSensors > nbrExistedSensors)
            {
                for (int i = 0; i < nbrExistedSensors; i++)
                {
                    existedSensors[i].MachineId = model.MachineId;
                    existedSensors[i].SensorType = model.Sensors[i].SensorType;
                    existedSensors[i].Model = model.Sensors[i].Model;
                    existedSensors[i].Serial = model.Sensors[i].Serial;
                    updateSensorResult = Sensor_Update(existedSensors[i], conn);
                }
                for (int i = nbrExistedSensors; i < nbrNewSensors; i++)
                {
                    model.Sensors[i].MachineId = model.MachineId;
                    insertSensorResult = Sensor_Insert(model.Sensors[i], conn);
                }
            }
            if (nbrNewSensors < nbrExistedSensors)
            {
                for (int i = 0; i < nbrNewSensors; i++)
                {
                    existedSensors[i].MachineId = model.MachineId;
                    existedSensors[i].SensorType = model.Sensors[i].SensorType;
                    existedSensors[i].Model = model.Sensors[i].Model;
                    existedSensors[i].Serial = model.Sensors[i].Serial;

                    updateSensorResult = Sensor_Update(existedSensors[i], conn);
                }
                for (int i = nbrNewSensors; i < nbrExistedSensors; i++)
                {
                    deleteSensorResult = Sensor_Delete(existedSensors[i], conn);
                }
            }

            // Step 2: Update Machine table


            try
            {
                string procedure = "dbo.SpMachine_Update";

                var p = new DynamicParameters();
                p.Add("@MachineId", model.MachineId);
                p.Add("@Name", model.Name);
                p.Add("@MachineTypeId", model.MachineType.MachineTypeId);
                p.Add("@Model", model.Model);
                p.Add("@Serial", model.Serial);
                p.Add("@Manufacturer", model.Manufacturer);
                p.Add("@MadeIn", model.MadeIn);
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int Machine_Insert(Machine model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = Machine_Insert(model, conn);
                return insertResult;
            }
        }
        private int Machine_Insert(Machine model, SqlConnection conn)
        {
            try
            {
                string procedure = "dbo.SpMachineTable_Insert";
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@MachineTypeId", model.MachineType.MachineTypeId);
                p.Add("@Model", model.Model);
                p.Add("@Serial", model.Serial);
                p.Add("@Manufacturer", model.Manufacturer);
                p.Add("@MadeIn", model.MadeIn);
                p.Add("@MachineId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.MachineId = p.Get<int>("@MachineId");

                int insertSensorResult = 0;
                foreach (Sensor sensor in model.Sensors)
                {
                    sensor.MachineId = model.MachineId;

                    insertSensorResult = Sensor_Insert(sensor, conn);
                }

                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int Machine_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int deleteResult = Machine_DeleteById(Id, conn);
                return deleteResult;
            }
        }
        private int Machine_DeleteById(int Id, SqlConnection conn)
        {
            string procedureSensor = "dbo.SpSensor_DeleteByMachineId";
            string procedureMachine = "dbo.SpMachine_DeleteById";

            var p = new DynamicParameters();
            p.Add("@MachineId", Id);

            try
            {
                var resultDeleteSensor = conn.Execute(procedureSensor, p, commandType: CommandType.StoredProcedure);
                var resultDeleteMachine = conn.Execute(procedureMachine, p, commandType: CommandType.StoredProcedure);

                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        #endregion

        #region MachineName
        public List<MachineName> MachineName_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<MachineName> output = MachineName_GetAll(conn);
                return output;
            }
        }
        private List<MachineName> MachineName_GetAll(SqlConnection conn)
        {
            string procedure = "dbo.SpMachineName_GetAll";

            List<MachineName> output = conn.Query<MachineName>(procedure, commandType: CommandType.StoredProcedure).ToList();
            return output;
        }

        public MachineName MachineName_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                MachineName output = MachineName_GetById(Id, conn);
                return output;
            }
        }
        private MachineName MachineName_GetById(int Id, SqlConnection conn)
        {
            throw new NotImplementedException();
        }

        public int MachineName_Update(MachineName model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = MachineName_Update(model, conn);
                return output;
            }
        }
        private int MachineName_Update(MachineName model, SqlConnection conn)
        {
            string procedure = "dbo.SpMachineName_Update";

            var p = new DynamicParameters();
            p.Add("@MachineNameId", model.MachineNameId);
            p.Add("@Name", model.Name);

            try
            {
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int MachineName_Insert(MachineName model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = MachineName_Insert(model, conn);
                return output;
            }
        }
        private int MachineName_Insert(MachineName model, SqlConnection conn)
        {
            string procedure = "dbo.SpMachineName_Insert";

            var p = new DynamicParameters();
            p.Add("@Name", model.Name);
            p.Add("@MachineNameId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.MachineNameId = p.Get<int>("@MachineNameId");
                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int MachineName_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int output = MachineName_DeleteById(Id, conn);
                return output;
            }
        }
        private int MachineName_DeleteById(int Id, SqlConnection conn)
        {
            string procedure = "dbo.SpMachineName_DeleteById";

            var p = new DynamicParameters();
            p.Add("@MachineNameId", Id);

            try
            {
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        #endregion

        #region CalibData
        public CalibData CalibData_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                CalibData output = CalibData_GetById(Id, conn);
                return output;
            }
        }
        private CalibData CalibData_GetById(int Id, SqlConnection conn)
        {
            CalibDataTable calibDataTable = CalibDataTable_GetById(Id, conn);
            RadQuantity radQuantity = RadQuantity_GetById(calibDataTable.RadQuantityId, conn);

            CalibData calibData = new CalibData()
            {
                CalibDataId = Id,
                CertificateId = calibDataTable.CertificateId,
                RadQuantity = radQuantity,
                MachineReading = calibDataTable.MachineReading,
                MachineUnit = calibDataTable.MachineUnit,
                RefValue = calibDataTable.RefValue,
                RefUnit = calibDataTable.RefUnit,
                CF = calibDataTable.CF,
                CF_reUnc = calibDataTable.CF_reUnc
            };

            return calibData;
        }
        private CalibDataTable CalibDataTable_GetById(int Id, SqlConnection conn)
        {
            string procedure = "SpCalibDataTable_GetById";
            var p = new DynamicParameters();
            p.Add("@CalibDataTableId", Id);

            CalibDataTable output = conn.Query<CalibDataTable>(procedure, p, commandType: CommandType.StoredProcedure).FirstOrDefault();

            return output;
        }

        public List<CalibData> CalibData_GetByCertificateId(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<CalibData> output = CalibData_GetByCertificateId(Id, conn);
                return output;
            }
        }
        private List<CalibData> CalibData_GetByCertificateId(int Id, SqlConnection conn)
        {
            List<CalibDataTable> calibDataTables = CalibDataTable_GetByCertificateId(Id, conn);

            List<CalibData> calibDatas = new List<CalibData>();

            foreach (CalibDataTable calibDataTable in calibDataTables)
            {
                RadQuantity radQuantity = RadQuantity_GetById(calibDataTable.RadQuantityId, conn);

                CalibData calibData = new CalibData()
                {
                    CalibDataId = calibDataTable.CalibDataId,
                    CertificateId = calibDataTable.CertificateId,
                    RadQuantity = radQuantity,
                    MachineReading = calibDataTable.MachineReading,
                    AvgReading = calibDataTable.AvgReading,
                    MachineUnit = calibDataTable.MachineUnit,
                    RefValue = calibDataTable.RefValue,
                    RefUnit = calibDataTable.RefUnit,
                    CF = calibDataTable.CF,
                    CF_reUnc = calibDataTable.CF_reUnc
                };

                if (calibDataTable.MachineUnit != calibDataTable.RefUnit)
                {
                    calibData.CF_unit = "(" + calibDataTable.RefUnit + ")/(" + calibDataTable.MachineUnit + ")";
                }
                else
                {
                    calibData.CF_unit = "";
                }

                calibDatas.Add(calibData);
            }
            return calibDatas;
        }
        private List<CalibDataTable> CalibDataTable_GetByCertificateId(int Id, SqlConnection conn)
        {
            string procedure = "SpCalibDataTable_GetByCertificateId";
            var p = new DynamicParameters();
            p.Add("@CertificateId", Id);

            List<CalibDataTable> output = conn.Query<CalibDataTable>(procedure, p, commandType: CommandType.StoredProcedure).ToList();

            return output;
        }

        public int CalibData_Update(CalibData model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = CalibData_Update(model, conn);
                return updateResult;
            }
        }
        private int CalibData_Update(CalibData model, SqlConnection conn)
        {
            string procedure = "dbo.SpCalibData_Update";

            var p = new DynamicParameters();
            p.Add("@CalibDataId", model.CalibDataId);
            p.Add("@CertificateId", model.CertificateId);
            p.Add("@RadQuantityId", model.RadQuantity.RadQuantityId);
            p.Add("@MachineReading", model.MachineReading);
            p.Add("@AvgReading", model.AvgReading);
            p.Add("@MachineUnit", model.MachineUnit);
            p.Add("@RefValue", model.RefValue);
            p.Add("@RefUnit", model.RefUnit);
            p.Add("@CF", model.CF);
            p.Add("@CF_reUnc", model.CF_reUnc);

            try
            {
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int CalibData_Insert(CalibData model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int insertResult = CalibData_Insert(model, conn);
                return insertResult;
            }
        }
        private int CalibData_Insert(CalibData model, SqlConnection conn)
        {
            string procedure = "dbo.SpCalibData_Insert";

            var p = new DynamicParameters();
            p.Add("@CertificateId", model.CertificateId);

            if (model.RadQuantity == null)
            {
                p.Add("@RadQuantityId", 1);
            }
            else
            {
                p.Add("@RadQuantityId", model.RadQuantity.RadQuantityId);
            }
            
            p.Add("@MachineReading", model.MachineReading);
            p.Add("@AvgReading", model.AvgReading);
            p.Add("@MachineUnit", model.MachineUnit);
            p.Add("@RefValue", model.RefValue);
            p.Add("@RefUnit", model.RefUnit);
            p.Add("@CF", model.CF);
            p.Add("@CF_reUnc", model.CF_reUnc);

            p.Add("@CalibDataId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            try
            {
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.CalibDataId = p.Get<int>("@CalibDataId");
                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int CalibData_DeleteByCertificateId(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int deleteResult = CalibData_DeleteByCertificateId(Id, conn);
                return deleteResult;
            }
        }
        private int CalibData_DeleteByCertificateId(int Id, SqlConnection conn)
        {
            string procedure = "SpCalibData_DeleteByCertificateId";
            var p = new DynamicParameters();
            p.Add("@CertificateId", Id);

            try
            {
                var result = conn.Query<CalibDataTable>(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CalibData_DeleteById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int deleteResult = CalibData_DeleteById(Id, conn);
                return deleteResult;
            }
        }
        private int CalibData_DeleteById(int Id, SqlConnection conn)
        {
            string procedure = "SpCalibData_DeleteById";
            var p = new DynamicParameters();
            p.Add("@CalibDataId", Id);

            try
            {
                var result = conn.Query<CalibData>(procedure, p, commandType: CommandType.StoredProcedure);
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Certificate
        public List<Certificate> Certificate_GetAll(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Certificate> output = Certificate_GetAll(conn);
                return output;
            }
        }
        private List<Certificate> Certificate_GetAll(SqlConnection conn)
        {
            List<CertificateTable> certificateTables = CertificateTable_GetAll(conn);

            List<Certificate> certificates = new List<Certificate>();

            foreach (CertificateTable certificateTable in certificateTables)
            {
                DoseQuantity doseQuantity = DoseQuantity_GetById(certificateTable.DoseQuantityId, conn);
                Machine machine = Machine_GetById(certificateTable.MachineId, conn);
                Customer customer = Customer_GetById(certificateTable.CustomerId, conn);
                List<CalibData> calibDatas = CalibData_GetByCertificateId(certificateTable.CertificateId, conn);

                Certificate certificate = new Certificate()
                {
                    CertificateId = certificateTable.CertificateId,
                    CertificateNumber = certificateTable.CertificateNumber,
                    DoseQuantity = doseQuantity,
                    CalibDate = certificateTable.CalibDate,
                    Machine = machine,
                    Customer = customer,
                    Temperature = certificateTable.Temperature,
                    Humidity = certificateTable.Humidity,
                    Pressure = certificateTable.Pressure,
                    PerformedBy = certificateTable.PerformedBy,
                    TM = certificateTable.TM,
                    Note = certificateTable.Note,
                    CalibDatas = calibDatas
                };

                certificates.Add(certificate);
            }

            return certificates;
        }
        private List<CertificateTable> CertificateTable_GetAll(SqlConnection conn)
        {
            string procedure = "SpCertificateTable_GetAll";
            var p = new DynamicParameters();

            List<CertificateTable> output = conn.Query<CertificateTable>(
                sql: procedure,
                commandType: CommandType.StoredProcedure
                ).ToList();

            return output;
        }

        public List<Certificate> Certificate_GetFromDateToDate(DateTime FromDate, DateTime ToDate, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                List<Certificate> output = Certificate_GetFromDateToDate(FromDate, ToDate, conn);
                return output;
            }
        }
        private List<Certificate> Certificate_GetFromDateToDate(DateTime FromDate, DateTime ToDate, SqlConnection conn)
        {
            List<CertificateTable> certificateTables = CertificateTable_GetFromDateToDate(FromDate, ToDate, conn);
            List<Certificate> certificates = new List<Certificate>();

            foreach (CertificateTable certificateTable in certificateTables)
            {
                DoseQuantity doseQuantity = DoseQuantity_GetById(certificateTable.DoseQuantityId, conn);
                Machine machine = Machine_GetById(certificateTable.MachineId, conn);
                Customer customer = Customer_GetById(certificateTable.CustomerId, conn);
                List<CalibData> calibDatas = CalibData_GetByCertificateId(certificateTable.CertificateId, conn);

                Certificate certificate = new Certificate()
                {
                    CertificateId = certificateTable.CertificateId,
                    CertificateNumber = certificateTable.CertificateNumber,
                    DoseQuantity = doseQuantity,
                    CalibDate = certificateTable.CalibDate,
                    Machine = machine,
                    Customer = customer,
                    Temperature = certificateTable.Temperature,
                    Humidity = certificateTable.Humidity,
                    Pressure = certificateTable.Pressure,
                    PerformedBy = certificateTable.PerformedBy,
                    TM = certificateTable.TM,
                    Note = certificateTable.Note,
                    CalibDatas = calibDatas
                };

                certificates.Add(certificate);
            }

            return certificates;
        }
        private List<CertificateTable> CertificateTable_GetFromDateToDate(DateTime FromDate, DateTime ToDate, SqlConnection conn)
        {

            string procedure = "SpCertificateTable_GetFromDateToDate";
            var p = new DynamicParameters();

            string beginDate = FromDate.ToString("yyyy/MM/dd");

            p.Add("@FromDate", FromDate.ToString("yyyy/MM/dd"));
            p.Add("@ToDate", ToDate.ToString("yyyy/MM/dd"));

            List<CertificateTable> output = conn.Query<CertificateTable>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                param: p
                ).ToList();

            return output;
        }


        public Certificate Certificate_GetById(int Id, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                Certificate output = Certificate_GetById(Id, conn);
                return output;
            }
        }
        private Certificate Certificate_GetById(int Id, SqlConnection conn)
        {

            CertificateTable certificateTable = CertificateTable_GetById(Id, conn);

            DoseQuantity doseQuantity = DoseQuantity_GetById(certificateTable.DoseQuantityId, conn);
            Machine machine = Machine_GetById(certificateTable.MachineId, conn);
            Customer customer = Customer_GetById(certificateTable.CustomerId, conn);
            List<CalibData> calibDatas = CalibData_GetByCertificateId(certificateTable.CertificateId, conn);

            Certificate certificate = new Certificate()
            {
                CertificateId = certificateTable.CertificateId,
                CertificateNumber = certificateTable.CertificateNumber,
                DoseQuantity = doseQuantity,
                CalibDate = certificateTable.CalibDate,
                Machine = machine,
                Customer = customer,
                Temperature = certificateTable.Temperature,
                Humidity = certificateTable.Humidity,
                Pressure = certificateTable.Pressure,
                PerformedBy = certificateTable.PerformedBy,
                TM = certificateTable.TM,
                Note = certificateTable.Note,
                CalibDatas = calibDatas
            };

            return certificate;
        }
        private CertificateTable CertificateTable_GetById(int Id, SqlConnection conn)
        {
            string procedure = "SpCertificateTable_GetById";
            var p = new DynamicParameters();
            p.Add("@CertificateId", Id);

            CertificateTable output = conn.Query<CertificateTable>(
                sql: procedure,
                commandType: CommandType.StoredProcedure,
                param: p
                ).FirstOrDefault();

            return output;
        }

        public int Certificate_Update(Certificate model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = Certificate_Update(model, conn);
                return updateResult;
            }
        }
        private int Certificate_Update(Certificate model, SqlConnection conn)
        {
            string procedure = "dbo.SpCertificateTable_Update";

            var p = new DynamicParameters();
            p.Add("@CertificateId", model.CertificateId);
            p.Add("@CertificateNumber", model.CertificateNumber);
            p.Add("@DoseQuantityId", model.DoseQuantity.DoseQuantityId);
            p.Add("@CalibDate", model.CalibDate);
            p.Add("@MachineId", model.Machine.MachineId);
            p.Add("@CustomerId", model.Customer.CustomerId);
            p.Add("@Temperature", model.Temperature);
            p.Add("@Humidity", model.Humidity);
            p.Add("@Pressure", model.Pressure);
            p.Add("@PerformedBy", model.PerformedBy);
            p.Add("@TM", model.TM);
            p.Add("@Note", model.Note);

            try
            {
                // Update Machine first
                int Certificate_Update_Machine_Result = Certificate_Update_Machine(model.Machine, conn);

                // Update CalibDatas
                int Certificate_Update_CalibData_Result = Certificate_Update_CalibData(model, conn);

                // Update other information in Certificate Table
                var result = conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);

                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }
        private int Certificate_Update_Machine(Machine machine, SqlConnection conn)
        {
            try
            {
                int updateResult = Machine_Update(machine, conn);

                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private int Certificate_Update_CalibData(Certificate certificate, SqlConnection conn)
        {
            List<CalibData> newCalibDatas = certificate.CalibDatas;
            List<CalibData> exsitedCalibDatas = CalibData_GetByCertificateId(certificate.CertificateId, conn);

            int nbrNewCalibData = newCalibDatas.Count();
            int nbrExsitedCalibData = exsitedCalibDatas.Count();

            int updateCalibData = 0;
            int insertCalibData = 0;
            int deleteCalibData = 0;

            if (nbrNewCalibData == nbrExsitedCalibData)
            {
                for (int i = 0; i < nbrNewCalibData; i++)
                {
                    exsitedCalibDatas[i].CertificateId = newCalibDatas[i].CertificateId;
                    exsitedCalibDatas[i].RadQuantity = newCalibDatas[i].RadQuantity;
                    exsitedCalibDatas[i].MachineReading = newCalibDatas[i].MachineReading;
                    exsitedCalibDatas[i].AvgReading = newCalibDatas[i].AvgReading;
                    exsitedCalibDatas[i].MachineUnit = newCalibDatas[i].MachineUnit;
                    exsitedCalibDatas[i].RefValue = newCalibDatas[i].RefValue;
                    exsitedCalibDatas[i].RefUnit = newCalibDatas[i].RefUnit;
                    exsitedCalibDatas[i].CF = newCalibDatas[i].CF;
                    exsitedCalibDatas[i].CF_reUnc = newCalibDatas[i].CF_reUnc;

                    updateCalibData = CalibData_Update(exsitedCalibDatas[i], conn);
                }
            }
            if (nbrNewCalibData > nbrExsitedCalibData)
            {
                for (int i = 0; i < nbrExsitedCalibData; i++)
                {
                    exsitedCalibDatas[i].CertificateId = newCalibDatas[i].CertificateId;
                    exsitedCalibDatas[i].RadQuantity = newCalibDatas[i].RadQuantity;
                    exsitedCalibDatas[i].MachineReading = newCalibDatas[i].MachineReading;
                    exsitedCalibDatas[i].MachineUnit = newCalibDatas[i].MachineUnit;
                    exsitedCalibDatas[i].RefValue = newCalibDatas[i].RefValue;
                    exsitedCalibDatas[i].RefUnit = newCalibDatas[i].RefUnit;
                    exsitedCalibDatas[i].CF = newCalibDatas[i].CF;
                    exsitedCalibDatas[i].CF_reUnc = newCalibDatas[i].CF_reUnc;

                    updateCalibData = CalibData_Update(exsitedCalibDatas[i], conn);
                }
                for (int i = nbrExsitedCalibData; i < nbrNewCalibData; i++)
                {
                    insertCalibData = CalibData_Insert(newCalibDatas[i], conn);
                }
            }
            if (nbrNewCalibData < nbrExsitedCalibData)
            {
                for (int i = 0; i < nbrNewCalibData; i++)
                {
                    exsitedCalibDatas[i].CertificateId = newCalibDatas[i].CertificateId;
                    exsitedCalibDatas[i].RadQuantity = newCalibDatas[i].RadQuantity;
                    exsitedCalibDatas[i].MachineReading = newCalibDatas[i].MachineReading;
                    exsitedCalibDatas[i].MachineUnit = newCalibDatas[i].MachineUnit;
                    exsitedCalibDatas[i].RefValue = newCalibDatas[i].RefValue;
                    exsitedCalibDatas[i].RefUnit = newCalibDatas[i].RefUnit;
                    exsitedCalibDatas[i].CF = newCalibDatas[i].CF;
                    exsitedCalibDatas[i].CF_reUnc = newCalibDatas[i].CF_reUnc;

                    updateCalibData = CalibData_Update(exsitedCalibDatas[i], conn);
                }
                for (int i = nbrNewCalibData; i < nbrExsitedCalibData; i++)
                {
                    deleteCalibData = CalibData_DeleteById(exsitedCalibDatas[i].CalibDataId, conn);
                }
            }

            return 1;
        }


        public int Certificate_Insert(Certificate model, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int updateResult = Certificate_Insert(model, conn);
                return updateResult;
            }
        }
        private int Certificate_Insert(Certificate model, SqlConnection conn)
        {
            // Insert Machine first to get MachineId
            int insertMachineResult = Machine_Insert(model.Machine, conn);

            if (insertMachineResult == 0) { return 0; }

            try
            {
                string procedure = "dbo.SpCertificateTable_Insert";
                var p = new DynamicParameters();
                p.Add("@CertificateNumber", model.CertificateNumber);
                p.Add("@DoseQuantityId", model.DoseQuantity.DoseQuantityId);
                p.Add("@CalibDate", model.CalibDate);
                p.Add("@MachineId", model.Machine.MachineId);
                p.Add("@CustomerId", model.Customer.CustomerId);
                p.Add("@Temperature", model.Temperature);
                p.Add("@Humidity", model.Humidity);
                p.Add("@Pressure", model.Pressure);
                p.Add("@PerformedBy", model.PerformedBy);
                p.Add("@TM", model.TM);
                p.Add("@Note", model.Note);
                p.Add("@CertificateId", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                conn.Execute(procedure, p, commandType: CommandType.StoredProcedure);
                model.CertificateId = p.Get<int>("@CertificateId");

                foreach (CalibData calibData in model.CalibDatas)
                {
                    calibData.CertificateId = model.CertificateId;
                    int insertCalibDataResult = CalibData_Insert(calibData, conn);
                    if (insertCalibDataResult == 0) { return 0; }
                }

                return 1;
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public int Certificate_Delete(Certificate certificate, string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                int deleteResult = Certificate_Delete(certificate, conn);
                return deleteResult;
            }
        }
        private int Certificate_Delete(Certificate certificate, SqlConnection conn)
        {
            string procedureCalibData = "dbo.SpCalibData_DeleteByCertificateId";
            string procedureCertificate = "dbo.SpCertificateTable_DeleteById";

            var p = new DynamicParameters();
            p.Add("@CertificateId", certificate.CertificateId);

            try
            {
                var resultDeleteCalibData = conn.Execute(procedureCalibData, p, commandType: CommandType.StoredProcedure);
                var resultDeleteCertificate = conn.Execute(procedureCertificate, p, commandType: CommandType.StoredProcedure);
                int deleteMachine = Machine_DeleteById(certificate.Machine.MachineId, conn);

                return 1;

            }
            catch (SqlException)
            {
                throw; ;
            }
        }
        #endregion
    }
}
