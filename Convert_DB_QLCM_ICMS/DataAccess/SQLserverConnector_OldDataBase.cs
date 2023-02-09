using Convert_DB_QLCM_ICMS.OldModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_DB_QLCM_ICMS.DataAccess
{
    public class SQLserverConnector_OldDataBase
        {
            public List<OldCertificate> OldCertificate_GetAll(string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<OldCertificate> Certificates = new List<OldCertificate>();

                    string queryString = @"SELECT CertificateId, MachineId, No, CalibrationDate, 
                                              Temperature, Pressure, Humidity, Staff1, Staff2, U
                                      FROM dbo.Certificate
                                      WHERE CalibrationDate >= Convert(datetime, '2017-01-01' )
                                      ORDER BY CalibrationDate";

                    SqlCommand command = new SqlCommand(queryString, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        OldCertificate certificate = new OldCertificate();

                        certificate.CertificateId = int.Parse(row["CertificateId"].ToString());

                        int machineId = int.Parse(row["MachineId"].ToString());
                        certificate.Machine = OldMachine_GetById(machineId, connectionString);
                        certificate.CertificateNumber = row["No"].ToString(); ;

                        DateTime.TryParse(row["CalibrationDate"].ToString(), out DateTime date);
                        certificate.CalibrationDate = date;

                        certificate.Temperature = double.Parse(row["Temperature"].ToString());
                        certificate.Humidity = double.Parse(row["Humidity"].ToString());
                        certificate.Pressure = double.Parse(row["Pressure"].ToString());

                        certificate.Staff1 = row["Staff1"].ToString();
                        certificate.Staff2 = row["Staff2"].ToString();

                        certificate.U = row["U"].ToString();


                        List<OldCertificateRadiation> CertificateRadiations = OldCertificateRadiation_GetByCertificateId(certificate.CertificateId, connectionString);
                        certificate.CertificateRadiations = CertificateRadiations;


                        if (certificate.Machine != null)
                        {
                            OldOrganization organization = OldOrganization_GetById(certificate.Machine.OrganizationId, connectionString);
                            certificate.Organization = organization;

                            //if (organization == null)
                            //{
                            //    int i = 1;
                            //}
                        }
                        else
                        {
                            certificate.Organization = null;
                        }



                        Certificates.Add(certificate);
                    }


                    return Certificates;
                }
            }

            public OldCertificate OldCertificate_GetByCertificateId(string certificateNumber, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldCertificate certificate = new OldCertificate();

                    string queryString = @"SELECT CertificateId, MachineId, CalibrationDate
                                              Temperature, Pressure, Humidity, Staff1, Staff2, U
                                      FROM dbo.Certificate 
                                     WHERE No = @certificateNumber";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@certificateNumber", certificateNumber);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    if (datatable.Rows.Count == 0)
                    {
                        certificate = null;
                    }
                    else
                    {
                        DataRow row = datatable.Rows[0];

                        certificate.CertificateId = int.Parse(row["CertificateId"].ToString());

                        int machineId = int.Parse(row["MachineId"].ToString());
                        certificate.Machine = OldMachine_GetById(machineId, connectionString);
                        certificate.CertificateNumber = row["No"].ToString(); ;

                        DateTime.TryParse(row["CalibrationDate"].ToString(), out DateTime date);
                        certificate.CalibrationDate = date;

                        certificate.Temperature = double.Parse(row["Temperature"].ToString());
                        certificate.Humidity = double.Parse(row["Humidity"].ToString());
                        certificate.Pressure = double.Parse(row["Pressure"].ToString());

                        certificate.Staff1 = row["Staff1"].ToString();
                        certificate.Staff2 = row["Staff2"].ToString();

                        certificate.U = row["U"].ToString();

                        List<OldCertificateRadiation> CertificateRadiations = OldCertificateRadiation_GetByCertificateId(certificate.CertificateId, connectionString);
                        certificate.CertificateRadiations = CertificateRadiations;
                    }
                    return certificate;
                }
            }

            public List<OldCertificateRadiation> OldCertificateRadiation_GetByCertificateId(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<OldCertificateRadiation> certificateRadiation = new List<OldCertificateRadiation>();

                    string queryString = @"SELECT CertificateId, RadiationId, ReferenceDoseRate, SurveyMeterReading, Input1, Input2 
                                       FROM CertificateRadiation 
                                       WHERE CertificateId = @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    if (datatable.Rows.Count == 0)
                    {
                        certificateRadiation = null;
                    }
                    else
                    {
                        foreach (DataRow row in datatable.Rows)
                        {
                            OldCertificateRadiation certiRad = new OldCertificateRadiation();
                            certiRad.Id = Id;
                            certiRad.CertificateId = int.Parse(row["CertificateId"].ToString());

                            int radiationId = int.Parse(row["RadiationId"].ToString());
                            certiRad.Radiation = OldRadiation_GetById(radiationId, connectionString);

                            certiRad.ReferenceDoseRate = row["ReferenceDoseRate"].ToString();
                            certiRad.SurveyMeterReading = row["SurveyMeterReading"].ToString();
                            certiRad.Input1 = row["Input1"].ToString();
                            certiRad.Input2 = row["Input2"].ToString();

                            certificateRadiation.Add(certiRad);
                        }
                    }
                    return certificateRadiation;
                }
            }

            public List<OldCity> OldCity_GetAll(string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<OldCity> Cities = new List<OldCity>();

                    string queryString = @"SELECT CityId, Name
                                        FROM City";

                    SqlCommand command = new SqlCommand(queryString, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        OldCity city = new OldCity();
                        city.CityId = int.Parse(row["CityId"].ToString());
                        city.Name = row["Name"].ToString();
                        Cities.Add(city);
                    }

                    return Cities;
                }
            }
            public OldCity OldCity_GetById(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldCity city = new OldCity();

                    string queryString = @"SELECT Name
                                        FROM City 
                                        WHERE CityId = @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);


                    if (datatable.Rows.Count == 0)
                    {
                        city.CityId = 0;
                        city.Name = "";
                    }
                    else
                    {
                        city.CityId = Id; // int.Parse(Id);
                        city.Name = datatable.Rows[0]["Name"].ToString();
                    }
                    return city;
                }
            }

            public OldMachine OldMachine_GetById(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldMachine machine = new OldMachine();

                    string queryString = @"SELECT Name, Seri, HeadSeri, MachineTypeId, MachineModelId, MachineHeadTypeId, UnitTypeId, OrganizationId, CalibrationDate, 
                                               CertificateId, MachineTypeCode, MachineHeadTypeCode, Manufacture
                                       FROM Machine 
                                       WHERE MachineId = @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    if (datatable.Rows.Count == 0)
                    {
                        machine = null;
                    }
                    else
                    {
                        DataRow row = datatable.Rows[0];

                        machine.MachineId = Id; // int.Parse(Id);
                        machine.Name = row["Name"].ToString();
                        machine.Seri = row["Seri"].ToString();
                        machine.HeadSeri = row["HeadSeri"].ToString();

                        int machineTypeId = int.Parse(row["MachineTypeId"].ToString());
                        machine.MachineType = OldMachineType_GetById(machineTypeId, connectionString);

                        int machineModelId = int.Parse(row["MachineModelId"].ToString());
                        machine.MachineModel = OldMachineModel_GetById(machineModelId, connectionString);

                        int machineHeadTypeId = int.Parse(row["MachineHeadTypeId"].ToString());
                        machine.MachineHeadType = OldMachineHeadType_GetById(machineHeadTypeId, connectionString);

                        int unitTypeId = int.Parse(row["UnitTypeId"].ToString());
                        machine.UnitType = OldUnitType_GetById(unitTypeId, connectionString);

                        machine.OrganizationId = int.Parse(row["OrganizationId"].ToString());

                        DateTime.TryParse(row["CalibrationDate"].ToString(), out DateTime date);
                        machine.CalibrationDate = date;

                        machine.CertificateId = int.Parse(row["CertificateId"].ToString());
                        machine.MachineTypeCode = row["MachineTypeCode"].ToString();
                        machine.MachineHeadTypeCode = row["MachineHeadTypeCode"].ToString();
                        machine.Manufacture = row["Manufacture"].ToString();

                    }
                    return machine;
                }
            }

            public OldMachineType OldMachineType_GetById(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldMachineType machineType = new OldMachineType();

                    string queryString = @"SELECT Code, Name
                                        FROM MachineType
                                        WHERE MachineTypeId = @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    if (datatable.Rows.Count == 0)
                    {
                        machineType = null;
                    }
                    else
                    {
                        DataRow row = datatable.Rows[0];

                        machineType.MachineTypeId = Id;// int.Parse(Id);
                        machineType.Code = row["Code"].ToString();
                        machineType.Name = row["Name"].ToString();
                    }
                    return machineType;
                }
            }

            public List<OldMachineHeadType> OldMachineHeadType_GetAll(string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<OldMachineHeadType> MachineHeadTypes = new List<OldMachineHeadType>();

                    string queryString = @"SELECT MachineHeadTypeId, Name
                                        FROM MachineHeadType";

                    SqlCommand command = new SqlCommand(queryString, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        OldMachineHeadType machineHeadType = new OldMachineHeadType();
                        machineHeadType.MachineHeadTypeId = int.Parse(row["MachineHeadTypeId"].ToString());
                        machineHeadType.Name = row["Name"].ToString();
                        MachineHeadTypes.Add(machineHeadType);
                    }

                    return MachineHeadTypes;
                }
            }
            public OldMachineHeadType OldMachineHeadType_GetById(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldMachineHeadType machineHeadType = new OldMachineHeadType();

                    string queryString = @"SELECT Name
                                        FROM MachineHeadType 
                                        WHERE MachineHeadTypeId = @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    if (datatable.Rows.Count == 0)
                    {
                        machineHeadType.MachineHeadTypeId = 0;
                        machineHeadType.Name = "";
                    }
                    else
                    {
                        machineHeadType.MachineHeadTypeId = Id; // ;
                        machineHeadType.Name = datatable.Rows[0]["Name"].ToString();
                    }
                    return machineHeadType;
                }
            }

            public List<OldMachineModel> OldMachineModel_GetAll(string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<OldMachineModel> MachineModels = new List<OldMachineModel>();

                    string queryString = @"SELECT MachineModelId,  Name 
                                        FROM MachineModel";

                    SqlCommand command = new SqlCommand(queryString, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        OldMachineModel machineModel = new OldMachineModel();

                        machineModel.MachineModelId = int.Parse(row["MachineModelId"].ToString());
                        machineModel.Name = row["Name"].ToString();

                        MachineModels.Add(machineModel);
                    }
                    return MachineModels;
                }
            }

            public OldMachineModel OldMachineModel_GetById(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldMachineModel machineModel = new OldMachineModel();

                    string queryString = @"SELECT Name
                                        FROM MachineModel
                                        WHERE MachineModelId = @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    if (datatable.Rows.Count == 0)
                    {
                        machineModel = null;
                    }
                    else
                    {
                        DataRow row = datatable.Rows[0];

                        machineModel.MachineModelId = Id; // ;
                        machineModel.Name = row["Name"].ToString();
                    }
                    return machineModel;
                }
            }

            public List<OldOrganization> OldOrganization_GetAll(string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<OldOrganization> Organizations = new List<OldOrganization>();

                    string queryString = @"SELECT OrganizationId, ShortName, Name, Address, CityId
                                        FROM Organization";

                    SqlCommand command = new SqlCommand(queryString, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        OldOrganization organization = new OldOrganization();
                        organization.OrganizationId = int.Parse(row["OrganizationId"].ToString());
                        organization.ShortName = row["ShortName"].ToString();
                        organization.Name = row["Name"].ToString();
                        organization.Address = row["Address"].ToString();

                        bool success = int.TryParse(datatable.Rows[0]["CityId"].ToString(), out int cityId);

                        if (success)
                        {
                            organization.City = OldCity_GetById(cityId, connectionString);
                        }
                        else
                        {
                            organization.City = null;
                        }


                        Organizations.Add(organization);
                    }
                    return Organizations;
                }
            }
            public OldOrganization OldOrganization_GetById(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldOrganization organization = new OldOrganization();

                    string queryString = @"SELECT ShortName, Name, CityId, Address, Phone 
                                       FROM Organization 
                                       WHERE OrganizationId =  @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    if (datatable.Rows.Count == 0)
                    {
                        organization = null;
                    }
                    else
                    {
                        organization.OrganizationId = Id; // ;
                        organization.ShortName = datatable.Rows[0]["ShortName"].ToString();
                        organization.Name = datatable.Rows[0]["Name"].ToString();

                        bool success = int.TryParse(datatable.Rows[0]["CityId"].ToString(), out int cityId);

                        if (success)
                        {
                            organization.City = OldCity_GetById(cityId, connectionString);
                        }
                        else
                        {
                            organization.City = null;
                        }

                        organization.Address = datatable.Rows[0]["Address"].ToString();
                        organization.Phone = datatable.Rows[0]["Phone"].ToString();
                    }
                    return organization;
                }
            }

            public List<OldRadiation> OldRadiation_GetAll(string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<OldRadiation> Radiations = new List<OldRadiation>();

                    string queryString = @"SELECT RadiationId,  Name 
                                        FROM Radiation";

                    SqlCommand command = new SqlCommand(queryString, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        OldRadiation radiation = new OldRadiation();

                        radiation.RadiationId = int.Parse(row["RadiationId"].ToString());
                        radiation.Name = row["Name"].ToString();

                        Radiations.Add(radiation);
                    }
                    return Radiations;
                }
            }

            public OldRadiation OldRadiation_GetById(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldRadiation radiation = new OldRadiation();

                    string queryString = @"SELECT Name
                                        FROM Radiation 
                                        WHERE RadiationId = @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    radiation.RadiationId = Id;
                    if (datatable.Rows.Count == 0)
                    {
                        radiation.Name = "";
                    }
                    else
                    {
                        radiation.Name = datatable.Rows[0]["Name"].ToString();
                    }
                    return radiation;
                }
            }


            public List<OldUnitType> OldUnitType_GetAll(string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    List<OldUnitType> unitTypes = new List<OldUnitType>();

                    string queryString = @"SELECT UnitTypeId, Name
                                        FROM UnitType";

                    SqlCommand command = new SqlCommand(queryString, conn);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    foreach (DataRow row in datatable.Rows)
                    {
                        OldUnitType unitType = new OldUnitType();
                        unitType.UnitTypeId = int.Parse(row["UnitTypeId"].ToString());
                        unitType.Name = row["Name"].ToString();
                        unitTypes.Add(unitType);
                    }

                    return unitTypes;
                }
            }
            public OldUnitType OldUnitType_GetById(int Id, string connectionString)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    OldUnitType unitType = new OldUnitType();

                    string queryString = @"SELECT Name
                                        FROM UnitType 
                                        WHERE UnitTypeId = @Id";

                    SqlCommand command = new SqlCommand(queryString, conn);
                    command.Parameters.AddWithValue("@Id", Id);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);

                    unitType.UnitTypeId = Id;// int.Parse(Id);
                    if (datatable.Rows.Count == 0)
                    {
                        unitType.Name = "";
                    }
                    else
                    {
                        unitType.Name = datatable.Rows[0]["Name"].ToString();
                    }
                    return unitType;
                }
            }
        }
}
