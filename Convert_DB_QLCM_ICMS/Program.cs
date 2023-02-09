using Convert_DB_QLCM_ICMS.ConvertTable;
using Convert_DB_QLCM_ICMS.DataAccess;
using Convert_DB_QLCM_ICMS.OldModel;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Convert_DB_QLCM_ICMS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SQLserverConnector_OldDataBase sqlConnector = new SQLserverConnector_OldDataBase();
            string QLCMold_connString = GetConnectionStringHelper.GetConnectionString("conString");


            SQLServerConnector sqlConnectorNew = new SQLServerConnector();
            string QLCMnew_connString = GetConnectionStringHelper.GetConnectionString("ICMSdatabase");


            GlobalConfig.InitializeConnections();


            #region get all certificates and delete invalid certificates
            Console.WriteLine("Step 1: Get all certificates in old database");
            List<OldCertificate> OldCertificates = sqlConnector.OldCertificate_GetAll(QLCMold_connString);

            Console.WriteLine("Step 2: Remove all invalid certificates");
            Console.WriteLine("  - No machine infos");
            // no machine infomation
            int Nbr_OldCertificate_No_Machine = OldCertificates.Count(s => s.Machine == null);
            OldCertificates.RemoveAll(s => s.Machine == null);

            Console.WriteLine("  - No organization infos");
            // No organization info
            int Nbr_OldCertificate_No_Organization = OldCertificates.Count(s => s.Organization == null);
            OldCertificates.RemoveAll(s => s.Organization == null);

            for (int i = 0; i < OldCertificates.Count(); i++)
            {
                //Console.WriteLine($"     + No: {OldCertificates[i].CertificateNumber}   -  data: {OldCertificates[i].CalibrationDate} ");

                if (OldCertificates[i].Organization == null)
                {
                    OldCertificates.RemoveAt(i);
                }

  
            }
           

            Console.WriteLine("  - No machine serial");
            // No machine Serial
            int Nbr_OldCertificate_No_Machine_Serial = OldCertificates.Count(s => s.Machine.Seri == null);
            Nbr_OldCertificate_No_Machine_Serial = Nbr_OldCertificate_No_Machine_Serial + OldCertificates.Count(s => s.Machine.Seri == "");

            Console.WriteLine("  - No certificate number");
            // No Certificate Number
            int Nbr_OldCertificate_No_CertificateNumber = OldCertificates.Count(s => s.CertificateNumber == null);
            Nbr_OldCertificate_No_CertificateNumber = Nbr_OldCertificate_No_CertificateNumber + OldCertificates.Count(s => s.CertificateNumber == "");

            OldCertificates.RemoveAll(s => s.CertificateNumber == null);
            OldCertificates.RemoveAll(s => s.CertificateNumber == "");

            Console.WriteLine("  - Environement outside range");
            // Temperature outside range [18-30]
            OldCertificates.RemoveAll(s => s.Temperature < 18.0);
            OldCertificates.RemoveAll(s => s.Temperature > 30.0);

            // Pressure outside [990-1100]
            OldCertificates.RemoveAll(s => s.Pressure > 1100.0);
            OldCertificates.RemoveAll(s => s.Pressure < 990.0);

            // Humidity outside [40-70]
            OldCertificates.RemoveAll(s => s.Humidity > 70.0);
            OldCertificates.RemoveAll(s => s.Humidity < 30.0);

            Console.WriteLine("  - No machine readings");
            // Input 2 == null
            int Nbr_OldCertificate_No_data = OldCertificates.Count(s => s.CertificateRadiations == null);
            OldCertificates.RemoveAll(s => s.CertificateRadiations == null);

            for (int i = 0; i < OldCertificates.Count(); i++)
            {
                int certificateId = getCertificateId_Where_Input2IsNullOrEmpty(OldCertificates[i]);

                if (certificateId != 0)
                {
                    OldCertificates.RemoveAt(i);
                }

 
            }

            

            Console.WriteLine("  - No machine type");
            // REMOVE MachineModel = null  (in new database: MachineType)
            int Nbr_OldCertificate_MachineModel_Null = OldCertificates.Count(s => s.Machine.MachineModel == null);
            OldCertificates.RemoveAll(s => s.Machine.MachineModel == null);

            // MachineModel = 5
            int Nbr_OldCertificate_MachineModel_Unknown = OldCertificates.Count(s => s.Machine.MachineModel.MachineModelId == 5);
            OldCertificates.RemoveAll(s => s.Machine.MachineModel.MachineModelId == 5);

            // No Unit: UnitTypeId = 0
            int Nbr_OldCertificate_Unit_Unknown = OldCertificates.Count(s => s.Machine.UnitType.UnitTypeId == 0);
            OldCertificates.RemoveAll(s => s.Machine.UnitType.UnitTypeId == 0);


            // Duplicate CertificateNumber
            Console.WriteLine("  - Duplicate CertificateNumber");
            var groupedByCertificateNumber = OldCertificates.GroupBy(x => x.CertificateNumber);
            var duplicates = groupedByCertificateNumber.Where(item => item.Count() > 1);
            foreach (var duplicate in duplicates)
            {
                Console.WriteLine("        {0} occurs {1} times", duplicate.Key, duplicate.Count().ToString());

                OldCertificates.RemoveAll(s => s.CertificateNumber == duplicate.Key);
                //foreach (var item in duplicate)
                //{
                //    Console.WriteLine("   {0}", item.CertificateNumber);
                //}
            }

            OldCertificate test = OldCertificates[3137];

            #endregion

            #region Add, edit Organization name
            Console.WriteLine("Step 3: Add, edit Organization name");
            // remove duplicate Organization's Name 
            for (int i = 0; i < OldCertificates.Count(); i++)
            {
                OldOrganization organ = new OldOrganization();

                //Công Ty SGS Việt Nam TNHH - Chi Nhánh Hải Phòng   Id:65
                // remove Công ty SGS Việt Nam trách nhiệm hữu hạn - chi nhánh Hải Phòng    Id:186
                if (OldCertificates[i].Organization.OrganizationId == 186)
                {
                    organ = sqlConnector.OldOrganization_GetById(65, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                //Công ty TNHH Emerson Process Management - Việt Nam      Id:349
                // REMOVE Công Ty TNHH Emerson Process Management (Việt Nam)   Id: 741
                if (OldCertificates[i].Organization.OrganizationId == 741)
                {
                    organ = sqlConnector.OldOrganization_GetById(349, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                //Công ty TNHH Giám định Vinacontrol TP. Hồ Chí Minh      Id:67
                // REMOVE Công ty TNHH Giám định Vinacontrol Thành phố Hồ Chí Minh   Id: 317
                if (OldCertificates[i].Organization.OrganizationId == 317)
                {
                    organ = sqlConnector.OldOrganization_GetById(67, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                //Công Ty TNHH Sản Xuất Lốp Xe Bridgestone Việt Nam      Id:438
                // REMOVE Công Ty TNHH Sản Xuất Lốp Xe Bridgestone Việt Nam   Id: 439
                if (OldCertificates[i].Organization.OrganizationId == 439)
                {
                    organ = sqlConnector.OldOrganization_GetById(438, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }


                //Công ty trách nhiệm hữu hạn Bell Việt Nam      Id:172
                // REMOVE Công ty trách nhiệm hữu hạn BEL Việt Nam   Id: 147
                //if (OldCertificates[i].Organization.OrganizationId == 147)
                //{
                //    organ = sqlConnector.OldOrganization_GetById(172, QLCMold_connString);
                //    OldCertificates[i].Organization = organ;
                //}

                // Liên Đoàn Địa Chất Xạ  Hiếm      Id:47
                // REMOVE Liên Đoàn Địa Chất Xạ  Hiếm   Id: 496

                OldCertificates.RemoveAll(s => s.Organization == null);
                OldCertificate test2 = OldCertificates[i];
                int j = i;
                if (OldCertificates[i].Organization.OrganizationId == 496)
                {
                    organ = sqlConnector.OldOrganization_GetById(47, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                // SamSung Electronics HCMC CE Complex Co.LTD      Id:683
                // REMOVE Samsung Electronics HCMC CE Complex Co.,Ltd   Id: 650
                if (OldCertificates[i].Organization.OrganizationId == 650)
                {
                    organ = sqlConnector.OldOrganization_GetById(683, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                // Trung Tâm Đào Tạo Và Phát Triển Sắc Ký      Id:628
                // REMOVE Trung Tâm Đào Tạo Và Phát Triển Sắc Ký   Id: 629
                if (OldCertificates[i].Organization.OrganizationId == 629)
                {
                    organ = sqlConnector.OldOrganization_GetById(628, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                // Trung tâm Kiểm soát Bệnh tật tỉnh Gia Lai      Id: 1778
                // REMOVE Trung tâm Kiểm soát Bệnh tật tỉnh Gia Lai   Id: 1777
                if (OldCertificates[i].Organization.OrganizationId == 1777)
                {
                    organ = sqlConnector.OldOrganization_GetById(1778, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                // Trung tâm phân tích và kiểm nghiệm - Bình Định      Id: 32
                // REMOVE Trung tâm phân tích và kiểm nghiệm (Bình Định)   Id: 246
                if (OldCertificates[i].Organization.OrganizationId == 246)
                {
                    organ = sqlConnector.OldOrganization_GetById(32, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                // Công ty dịch vụ sửa chữa các nhà máy điện - EVNGENCO3      Id: 283
                // REMOVE Công ty dịch vụ sửa chữa các nhà máy điện EVNGENCO 3   Id: 372
                if (OldCertificates[i].Organization.OrganizationId == 372)
                {
                    organ = sqlConnector.OldOrganization_GetById(283, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                // Công ty dịch vụ sửa chữa các nhà máy điện - EVNGENCO3      Id: 283
                // REMOVE Công ty dịch vụ sửa chữa các nhà máy điện EVNGENCO3   Id:120
                if (OldCertificates[i].Organization.OrganizationId == 120)
                {
                    organ = sqlConnector.OldOrganization_GetById(283, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }

                // Chi Nhánh Công Ty TNHH Apave Châu Á - Thái Bình Dương Tại Tỉnh Bà Rịa - Vũng Tàu      Id: 429
                // REMOVE Chi Nhánh Công Ty TNHH Apave Châu Á - Thái Bình Dương Tại Tỉnh Bà Rịa Vũng Tàu   Id: 176
                if (OldCertificates[i].Organization.OrganizationId == 176)
                {
                    organ = sqlConnector.OldOrganization_GetById(429, QLCMold_connString);
                    OldCertificates[i].Organization = organ;
                }
            }



            #endregion

            #region change City Id
            foreach (OldCertificate oldCertificate in OldCertificates)
            {
                if (oldCertificate.Organization.City != null)
                {
                    // Id = 66: Tp. Hồ chí Minh -> Id=30
                    if (oldCertificate.Organization.City.CityId == 66)
                    {
                        oldCertificate.Organization.City = sqlConnector.OldCity_GetById(30, QLCMold_connString);
                    }
                }
            }

            #endregion


            #region Format old certificates

            Console.WriteLine("Step 4: Format infos: Organization's name, adress, manufacturer");

            List<string> NewNames = new List<string>();
            List<string> NewAdresses = new List<string>();

            List<string> AllMachineTypeCode = new List<string>();     // Machine.Model
            List<string> AllManufacturer = new List<string>();
            List<string> AllMachineHeadTypeCode = new List<string>(); // Sensor.Model

            // Get infos
            foreach (OldCertificate certificate in OldCertificates)
            {
                AllMachineTypeCode.Add(certificate.Machine.MachineHeadTypeCode);
                AllManufacturer.Add(certificate.Machine.Manufacture);
                AllMachineHeadTypeCode.Add(certificate.Machine.MachineHeadTypeCode);
            }

            AllMachineTypeCode = AllMachineTypeCode.Distinct().ToList();
            AllManufacturer = AllManufacturer.Distinct().ToList();
            AllMachineHeadTypeCode = AllMachineHeadTypeCode.Distinct().ToList();

            foreach (OldCertificate certificate in OldCertificates)
            {
                certificate.Organization.Name = Convert_Old_data.Format_Organization_Name(certificate.Organization.Name);
                NewNames.Add(certificate.Organization.Name);

                certificate.Organization.Address = Convert_Old_data.Format_Organization_Adress(certificate.Organization.Address);
                NewAdresses.Add(certificate.Organization.Address);

                certificate.Staff1 = Convert_Old_data.Format_Staff_Name(certificate.Staff1);
                certificate.Staff2 = Convert_Old_data.Format_Staff_Name(certificate.Staff2);

                // MachineHeadTypeCode (Sensor.Model) n/a, N/A => ""
                if (certificate.Machine.MachineHeadTypeCode == null) { certificate.Machine.MachineHeadTypeCode = ""; }
                if (certificate.Machine.MachineHeadTypeCode.ToLower() == "n/a") { certificate.Machine.MachineHeadTypeCode = ""; }
                if (certificate.Machine.MachineHeadTypeCode.ToLower() == "n.a") { certificate.Machine.MachineHeadTypeCode = ""; }

                // Machine.HeadSeri (Sensor.Serial) n/a, N/A => ""
                if (certificate.Machine.HeadSeri == null) { certificate.Machine.HeadSeri = ""; }
                if (certificate.Machine.HeadSeri.ToLower() == "n/a") { certificate.Machine.HeadSeri = ""; }
                if (certificate.Machine.HeadSeri.ToLower() == "n.a") { certificate.Machine.HeadSeri = ""; }

                // MachineTypeCode (Machine.Model)
                if (certificate.Machine.MachineTypeCode == null) { certificate.Machine.MachineTypeCode = ""; }
                if (certificate.Machine.MachineTypeCode.ToLower() == "n/a") { certificate.Machine.MachineTypeCode = ""; }

                // Manufacturer
                if (certificate.Machine.Manufacture == null) { certificate.Machine.Manufacture = ""; }
                if (certificate.Machine.Manufacture.ToLower() == "n/a") { certificate.Machine.Manufacture = ""; }
                if (certificate.Machine.Manufacture.ToLower() == "usa") { certificate.Machine.Manufacture = "Hoa Kỳ"; }
                if (certificate.Machine.Manufacture.ToLower() == "my") { certificate.Machine.Manufacture = "Hoa Kỳ"; }
                if (certificate.Machine.Manufacture.ToLower() == "mỹ") { certificate.Machine.Manufacture = "Hoa Kỳ"; }
                if (certificate.Machine.Manufacture.ToLower() == "germany") { certificate.Machine.Manufacture = "Đức"; }
                if (certificate.Machine.Manufacture.ToLower() == "taiwan") { certificate.Machine.Manufacture = "Đài Loan"; }
                if (certificate.Machine.Manufacture.ToLower() == "taiwan") { certificate.Machine.Manufacture = "Đài Loan"; }
                if (certificate.Machine.Manufacture.ToLower() == "russia") { certificate.Machine.Manufacture = "Nga"; }
                if (certificate.Machine.Manufacture.ToLower() == "japan") { certificate.Machine.Manufacture = "Nhật Bản"; }
                if (certificate.Machine.Manufacture.ToLower() == "finland") { certificate.Machine.Manufacture = "Phần Lan"; }
                if (certificate.Machine.Manufacture.ToLower() == "korea") { certificate.Machine.Manufacture = "Hàn Quốc"; }

            }


            int Nbr_OldCertificate_No_MachineHeadType = OldCertificates.Count(s => s.Machine.MachineHeadType.MachineHeadTypeId == 0);
            List<OldCertificate> List_OldCertificate_No_MachineHeadType = OldCertificates.FindAll(s => s.Machine.MachineHeadType.MachineHeadTypeId == 0);

            NewNames = NewNames.Distinct().ToList();



            #endregion

            #region decimal point "," to "." in machine reading
            foreach (OldCertificate certificate in OldCertificates)
            {
                foreach (var certificateRadiation in certificate.CertificateRadiations)
                {
                    certificateRadiation.Input2 = certificateRadiation.Input2.Replace(",", ".");
                }
            }

            #endregion

            #region Get all utils tables in old database

            Console.WriteLine("Step 5:  Get all utils tables in old database");
            //City

            // Units

            // MachineHeadType  -> SensorType

            // MachineModel -> MachineType

            // Radiation -> RadQuantity

            // MachineType -> MachineName


            // Organization
            List<OldOrganization> OldOrganizations = new List<OldOrganization>();

            foreach (OldCertificate certificate in OldCertificates)
            {
                OldOrganizations.Add(certificate.Organization);
            }

            #endregion

            #region Get all utils tables in new database
            Console.WriteLine("Step 6:  Get all utils tables in new database");

            List<City> newCityList = GlobalConfig.Connection.City_GetAll(GlobalConfig.CnnString("ICMSdatabase"));
            List<MachineType> newMachineTypeList = GlobalConfig.Connection.MachineType_GetAll(GlobalConfig.CnnString("ICMSdatabase"));
            List<SensorType> newSenSorTypeList = GlobalConfig.Connection.SensorType_GetAll(GlobalConfig.CnnString("ICMSdatabase"));
            List<RadQuantity> newRadQuantityList = GlobalConfig.Connection.RadQuantity_GetAll(GlobalConfig.CnnString("ICMSdatabase"));
            List<DoseQuantity> newDoseQuantityList = GlobalConfig.Connection.DoseQuantity_GetAll(GlobalConfig.CnnString("ICMSdatabase"));
            List<Unit> newUnitList = GlobalConfig.Connection.Unit_GetAll(GlobalConfig.CnnString("ICMSdatabase"));

            //

            #endregion

            #region Convert to new certificate format 
            Console.WriteLine("Step 7:  Convert to new certificate format ");

            List<Certificate> newCertificateList = new List<Certificate>();



            foreach (OldCertificate oldCertificate in OldCertificates)
            {
                Certificate newCertificate = new Certificate();

                // City
                City newCity = new City();
                if (oldCertificate.Organization.City != null)
                {
                    newCity.CityId = oldCertificate.Organization.City.CityId;
                }
                else
                {
                    newCity = null;
                }


                // Customer
                Customer newCustomer = new Customer();
                newCustomer.ShortName = oldCertificate.Organization.ShortName;
                newCustomer.FullName = oldCertificate.Organization.Name;
                newCustomer.Address = oldCertificate.Organization.Address;
                newCustomer.City = newCity;

                // MachineType
                MachineType newMachineType = new MachineType();
                if (oldCertificate.Machine.MachineModel.MachineModelId == 1) { newMachineType = (MachineType)newMachineTypeList.FirstOrDefault(s => s.MachineTypeId == 1); }

                if (oldCertificate.Machine.MachineModel.MachineModelId == 2) { newMachineType = (MachineType)newMachineTypeList.FirstOrDefault(s => s.MachineTypeId == 2); }

                if (oldCertificate.Machine.MachineModel.MachineModelId == 3) { newMachineType = (MachineType)newMachineTypeList.FirstOrDefault(s => s.MachineTypeId == 3); }

                // Sensor Type
                SensorType newSensorType = new SensorType();
                if (oldCertificate.Machine.MachineHeadType.MachineHeadTypeId == 0) { newSensorType = null; }
                if (oldCertificate.Machine.MachineHeadType.MachineHeadTypeId == 1) { newSensorType = (SensorType)newSenSorTypeList.FirstOrDefault(s => s.SensorTypeId == 1); }
                if (oldCertificate.Machine.MachineHeadType.MachineHeadTypeId == 2) { newSensorType = (SensorType)newSenSorTypeList.FirstOrDefault(s => s.SensorTypeId == 2); }
                if (oldCertificate.Machine.MachineHeadType.MachineHeadTypeId == 3) { newSensorType = (SensorType)newSenSorTypeList.FirstOrDefault(s => s.SensorTypeId == 3); }
                if (oldCertificate.Machine.MachineHeadType.MachineHeadTypeId == 4) { newSensorType = (SensorType)newSenSorTypeList.FirstOrDefault(s => s.SensorTypeId == 4); }
                if (oldCertificate.Machine.MachineHeadType.MachineHeadTypeId == 5) { newSensorType = (SensorType)newSenSorTypeList.FirstOrDefault(s => s.SensorTypeId == 5); }

                // Sensors
                Sensor newSensor = new Sensor();
                newSensor.Model = oldCertificate.Machine.MachineHeadTypeCode;
                newSensor.Serial = oldCertificate.Machine.HeadSeri;
                newSensor.SensorType = newSensorType;

                List<Sensor> newSensors = new List<Sensor>
                {
                    newSensor
                };

                // Machine
                Machine newMachine = new Machine();
                newMachine.Name = oldCertificate.Machine.MachineType.Name;
                newMachine.MachineType = newMachineType;
                newMachine.Model = oldCertificate.Machine.MachineTypeCode;
                newMachine.Serial = oldCertificate.Machine.Seri;
                newMachine.Manufacturer = "";
                newMachine.MadeIn = oldCertificate.Machine.Manufacture;
                newMachine.Sensors = newSensors;

                // CalibData
                List<CalibData> newCalibDatas = new List<CalibData>();


                foreach (OldCertificateRadiation certificateRadiation in oldCertificate.CertificateRadiations)
                {
                    CalibData calibData = new CalibData();
                    RadQuantity radQuantity = new RadQuantity();

                    // RadQuantity
                    if (certificateRadiation.Radiation.Name == "Cs-137") { calibData.RadQuantity = (RadQuantity)newRadQuantityList.FirstOrDefault(s => s.RadQuantityId == 1); }
                    if (certificateRadiation.Radiation.Name == "Tia X  ISO L70") { calibData.RadQuantity = (RadQuantity)newRadQuantityList.FirstOrDefault(s => s.RadQuantityId == 10); }
                    if (certificateRadiation.Radiation.Name == "Tia X  ISO N80") { calibData.RadQuantity = (RadQuantity)newRadQuantityList.FirstOrDefault(s => s.RadQuantityId == 6); }
                    if (certificateRadiation.Radiation.Name == "Co-60") { calibData.RadQuantity = (RadQuantity)newRadQuantityList.FirstOrDefault(s => s.RadQuantityId == 3); }
                    if (certificateRadiation.Radiation.Name == "Sr-90") { calibData.RadQuantity = (RadQuantity)newRadQuantityList.FirstOrDefault(s => s.RadQuantityId == 23); }
                    if (certificateRadiation.Radiation.Name == "Am-241") { calibData.RadQuantity = (RadQuantity)newRadQuantityList.FirstOrDefault(s => s.RadQuantityId == 24); }

                    if (string.IsNullOrWhiteSpace(certificateRadiation.Radiation.Name))
                    {
                        calibData.RadQuantity = (RadQuantity)newRadQuantityList.FirstOrDefault(s => s.RadQuantityId == 1);
                    }

                    // Machine reading
                    calibData.MachineReading = certificateRadiation.Input2;

                    // Avg reading 


                    bool success = double.TryParse(certificateRadiation.SurveyMeterReading, out double avgReading);
                    if (success)
                    {
                        calibData.AvgReading = avgReading;
                    }
                    else
                    {
                        Console.WriteLine("  - Failed to Convert to new certificate format ");
                    }

                    // Machine Unit and RefUnit

                    if (oldCertificate.Machine.UnitType.UnitTypeId == 0) { throw new Exception(); }
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 1) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 1).Name; calibData.MachineUnit = calibData.RefUnit; } //µSv/h
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 2) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 2).Name; calibData.MachineUnit = calibData.RefUnit; } // mSv/h
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 3) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 3).Name; calibData.MachineUnit = calibData.RefUnit; } // mR/h
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 4) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 13).Name; calibData.MachineUnit = calibData.RefUnit; } // (µSv/h)/cps
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 5) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 14).Name; calibData.MachineUnit = calibData.RefUnit; } // (µSv/h)/cpm
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 6) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 4).Name; calibData.MachineUnit = calibData.RefUnit; } // µSv
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 7) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 5).Name; calibData.MachineUnit = calibData.RefUnit; } // mSv
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 8) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 6).Name; calibData.MachineUnit = calibData.RefUnit; } // mR
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 9) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 12).Name; calibData.MachineUnit = calibData.RefUnit; } // Bq
                    if (oldCertificate.Machine.UnitType.UnitTypeId == 10) { calibData.RefUnit = (string)newUnitList.FirstOrDefault(s => s.UnitId == 3).Name; calibData.MachineUnit = calibData.RefUnit; } // µR/h

                    // RefValue
                    calibData.RefValue = double.Parse(certificateRadiation.ReferenceDoseRate);

                    // CF, CF_reUnc evaluation
                    int nbrMachineReading = ConvertStringToListDouble(calibData.MachineReading).Count();
                    if (nbrMachineReading == 0)
                    {
                        Console.WriteLine($"   --   {calibData.MachineReading}");
                    }

                    calibData.CF = CF_evaluation(calibData.MachineReading, calibData.RefValue);
                    calibData.CF_reUnc = CF_reUnc_evaluation(calibData.MachineReading, calibData.RefValue, calibData.RadQuantity);

                    newCalibDatas.Add(calibData);
                }

                // Certificate
                newCertificate.CertificateNumber = oldCertificate.CertificateNumber;
                newCertificate.CalibDate = oldCertificate.CalibrationDate;
                newCertificate.Temperature = oldCertificate.Temperature;
                newCertificate.Pressure = oldCertificate.Pressure;
                newCertificate.Humidity = oldCertificate.Humidity;
                if (String.IsNullOrEmpty(oldCertificate.Staff1))
                {
                    newCertificate.PerformedBy = "";
                }
                else
                {
                    newCertificate.PerformedBy = oldCertificate.Staff1;
                }
                if (String.IsNullOrEmpty(oldCertificate.Staff2))
                {
                    newCertificate.TM = "";
                }
                else
                {
                    newCertificate.TM = oldCertificate.Staff2;
                }

                // DoseQuanity
                int nbrRawValue = oldCertificate.CertificateRadiations[0].Input2.Split(';').ToList().Count();
                if (nbrRawValue == 0) { throw new Exception(); }
                if (nbrRawValue == 1) { newCertificate.DoseQuantity = (DoseQuantity)newDoseQuantityList.FirstOrDefault(s => s.DoseQuantityId == 3); } // Hp(10)
                if (nbrRawValue > 1) { newCertificate.DoseQuantity = (DoseQuantity)newDoseQuantityList.FirstOrDefault(s => s.DoseQuantityId == 2); }  // H*(10)
                if (newCertificate.DoseQuantity == null) { throw new Exception(); }

                newCertificate.Customer = newCustomer;
                newCertificate.Machine = newMachine;
                newCertificate.CalibDatas = newCalibDatas;


                if (newCertificate.CertificateNumber == "21728")
                {

                    int i2 = 1;
                }

                newCertificateList.Add(newCertificate);
            }


            OldCertificate oldCertificate1 = OldCertificates.FirstOrDefault(s => s.CertificateNumber == "18290");
            Certificate newCertificate1 = newCertificateList.FirstOrDefault(s => s.CertificateNumber == "18290");
           

            #endregion


            #region Write to new database

            Console.WriteLine("Step 8:   Write to new database");
            int insertCustomerResult = 0;
            int insertCertificateResult = 0;

            foreach (Certificate certificate in newCertificateList)
            {


                try
                {
                    Console.WriteLine($"   - Certificate Number: {certificate.CertificateNumber}    Calib.Date: {certificate.CalibDate}");

                    insertCustomerResult = sqlConnectorNew.Customer_Insert(certificate.Customer, QLCMnew_connString);
                }
                catch (Exception)
                {
                    //Console.Write(ex.Message);
                    //Console.WriteLine(ex.StackTrace);

                    //Console.WriteLine("\n\nPress any key to continue ... \n\n");
                    //Console.ReadKey();

                    Customer exsistedCustomer = sqlConnectorNew.Customer_GetByFullName(certificate.Customer.FullName, QLCMnew_connString);
                    certificate.Customer.CustomerId = exsistedCustomer.CustomerId;
                }


                try
                {
                    MachineName machineName = new MachineName() { Name = certificate.Machine.Name };

                    sqlConnectorNew.MachineName_Insert(machineName, QLCMnew_connString);
                }
                catch (Exception ex)
                {
                    //insertCertificateResult = 0;
                    //Console.Write(ex.Message);
                    //Certificate certificate2 = certificate;
                }

                try
                {
                    insertCertificateResult = sqlConnectorNew.Certificate_Insert(certificate, QLCMnew_connString);
                }
                catch (Exception ex)
                {
                    insertCertificateResult = 0;
                    //Console.Write(ex.Message);
                    //Console.WriteLine(ex.StackTrace);

                    //Console.WriteLine("\n\nPress any key to continue ... \n\n");
                    //Console.ReadKey();
                }


                if (insertCertificateResult == 0)
                {
                    //Console.WriteLine($"\n\n ===================================================");
                    Console.WriteLine("\n\n");
                    Console.WriteLine($"        Error: Certificate      Id: {certificate.CertificateId}    No:{certificate.CertificateNumber}   Calib.Date:{certificate.CalibDate}\n");
                    Console.WriteLine("\n\n");
                    Console.ReadKey();
                }

            }
            #endregion

            Console.ReadLine();

        }


        #region Private function
        private static List<double> ConvertStringToListDouble(string stringNumbers)
        {
            List<double> output = new List<double>();

            stringNumbers = stringNumbers.Trim();   // remove white space at the begin and the end

            Regex rgx2 = new Regex("\t|\\s+");         // replace tab by whitespace
            stringNumbers = rgx2.Replace(stringNumbers, " ");

            stringNumbers = stringNumbers.Replace(',', '.');           //  replace ',' by '.'

            string[] dataString = stringNumbers.Split(new char[] { ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in dataString)
            {
                double dat;
                bool isValid = true;
                isValid = double.TryParse(item, out dat);

                if (isValid == false)
                {
                    return null;
                }
                output.Add(dat);
            }

            if (output.Count() == 0)
            {
                Console.WriteLine(stringNumbers);
            }

            return output;
        }

        private static double standardDeviationOfTheMean(List<double> sequence)
        {
            double result = 0;

            if (sequence.Count() >= 2)
            {
                double average = sequence.Average();
                double sum = sequence.Sum(d => Math.Pow(d - average, 2));
                double StdDev = Math.Sqrt((sum) / sequence.Count());

                result = StdDev / Math.Sqrt(sequence.Count());
            }
            return result;
        }


        private static double AverageReading(List<double> MachineReading)
        {
            return MachineReading.Average();
        }

        private static double CF_reUnc_evaluation(RadQuantity radQuantity, double AverageReading_reUnc)
        {
            double result = 0;

            double RefValue_reUnce = radQuantity != null ? radQuantity.ReUnc : 0.0;
            result = Math.Sqrt(RefValue_reUnce * RefValue_reUnce + AverageReading_reUnc * AverageReading_reUnc);

            return result;
        }


        private static double CF_reUnc_evaluation(string MachineReadingString, double RefValue, RadQuantity radQuantity)
        {
            List<double> MachineReading = ConvertStringToListDouble(MachineReadingString);

            double AvgReading = AverageReading(MachineReading);

            double AvgReading_StdMean = standardDeviationOfTheMean(MachineReading);

            double AvgReading_reUnc = AvgReading_StdMean / AvgReading;

            double CF_reUnc = CF_reUnc_evaluation(radQuantity, AvgReading_reUnc);

            return CF_reUnc;
        }

        private static double CF_evaluation(string MachineReadingString, double RefValue)
        {
            List<double> MachineReading = ConvertStringToListDouble(MachineReadingString);

            double AvgReading = AverageReading(MachineReading);

            double CF = RefValue / AvgReading;

            return CF;
        }


        private static int getCertificateId_Where_Input2IsNullOrEmpty(OldCertificate oldCertificate)
        {
            int certificateId = 0;
            foreach (OldCertificateRadiation certificateRadiation in oldCertificate.CertificateRadiations)
            {
                if (string.IsNullOrWhiteSpace(certificateRadiation.Input2) | certificateRadiation.Input2 == "Overload")
                {
                    certificateId = oldCertificate.CertificateId;
                }
            }
            return certificateId;
        }

        #endregion
    }
}
