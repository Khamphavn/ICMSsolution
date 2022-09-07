
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ICMS.Command;
using ICMS.Model;
using ICMS.Model.DataAccess;
using ICMS.Model.Models;

namespace ICMS.ViewModel
{
    public class CertificateFormViewModel : BaseViewModel
    {
        private User _CurrentUser;
        public User CurrentUser { get => _CurrentUser; private set { _CurrentUser = value; OnPropertyChanged(); } }

        //mode operation
        private string _CurrentOperationMode;
        public string CurrentOperationMode { get => _CurrentOperationMode; set { _CurrentOperationMode = value; OnPropertyChanged(); } }
        private enum OperationMode { ViewMode, EditMode, AddMode };

        private Certificate _CurrentCertificate;
        public Certificate CurrentCertificate 
        { 
            get => _CurrentCertificate; 
            set 
            {
                _CurrentCertificate = value;
                if (CurrentCertificate != null)
                {
                    SelectedCustomer = CurrentCertificate.Customer;

                    Temperature = CurrentCertificate.Temperature.ToString();
                    Pressure = CurrentCertificate.Pressure.ToString();
                    Humidity = CurrentCertificate.Humidity.ToString();

                    Machine = CurrentCertificate.Machine;
                    Sensors = new ObservableCollection<Sensor>(CurrentCertificate.Machine.Sensors);

                    CalibDate = CurrentCertificate.CalibDate;
                    CertificateNo = CurrentCertificate.CertificateNumber;
                    PerformedBy = CurrentCertificate.PerformedBy;

                    SelectedTM =  GlobalConfig.Connection.TM_GetByName(CurrentCertificate.TM, GlobalConfig.CnnString("ICMSdatabase")); 
                }
                OnPropertyChanged(); 
            } 
        }

        private bool _IsCertificateSaved;
        public bool IsCertificateSaved { get => _IsCertificateSaved; set { _IsCertificateSaved = value; OnPropertyChanged(); } }

        #region Customer
        private string _CustomerName;
        private ObservableCollection<Customer> _Customers;
        private Customer _SelectedCustomer;

        public string CustomerName { get => _CustomerName; set { _CustomerName = value; OnPropertyChanged(); } }
        public ObservableCollection<Customer> Customers { get => _Customers; set { _Customers = value; OnPropertyChanged(); } }
        public Customer SelectedCustomer { get => _SelectedCustomer; set { _SelectedCustomer = value; OnPropertyChanged(); } }
        #endregion

        #region Environment
        private string _Temperature;
        private string _Pressure;
        private string _Humidity;

        public string Temperature { get => _Temperature; set { _Temperature = value; OnPropertyChanged(); } }
        public string Pressure { get => _Pressure; set { _Pressure = value; OnPropertyChanged(); } }
        public string Humidity { get => _Humidity; set { _Humidity = value; OnPropertyChanged(); } }
        #endregion

        #region Machine infos
        private Machine _Machine;
       
        private ObservableCollection<MachineName> _AvailableMachineNames;
        private ObservableCollection<MachineType> _AvailableMachineTypes;
        private ObservableCollection<SensorType> _AvailableSensorTypes;

        private MachineName _SelectedMachineName;
        private MachineType _SelectedMachineType;
        private string _MachineModel;
        private string _MachineSerial;
        private string _MachineManufacturer;
        private string _MachineMadeIn;

        private ObservableCollection<Sensor> _Sensors;
        private SensorType _SelectedSensorType;
               
        

        public Machine Machine { get => _Machine; set { _Machine = value; OnPropertyChanged(); } }

        public ObservableCollection<MachineName> AvailableMachineNames { get => _AvailableMachineNames; set { _AvailableMachineNames = value; OnPropertyChanged(); } }
        public ObservableCollection<MachineType> AvailableMachineTypes { get => _AvailableMachineTypes; set { _AvailableMachineTypes = value; OnPropertyChanged(); } }
        public ObservableCollection<SensorType> AvailableSensorTypes { get => _AvailableSensorTypes; set { _AvailableSensorTypes = value; OnPropertyChanged(); } }
        

        public MachineName SelectedMachineName { get => _SelectedMachineName; set { _SelectedMachineName = value; OnPropertyChanged(); } }
        public MachineType SelectedMachineType { get => _SelectedMachineType; set { _SelectedMachineType = value; OnPropertyChanged(); } }
        public string MachineModel { get => _MachineModel; set { _MachineModel = value; OnPropertyChanged(); } }
        public string MachineSerial { get => _MachineSerial; set { _MachineSerial = value; OnPropertyChanged(); } }
        public string MachineManufacturer { get => _MachineManufacturer; set { _MachineManufacturer = value; OnPropertyChanged(); } }
        public string MachineMadeIn { get => _MachineMadeIn; set { _MachineMadeIn = value; OnPropertyChanged(); } }

        public ObservableCollection<Sensor> Sensors { get => _Sensors; set { _Sensors = value; OnPropertyChanged(); } }

        public SensorType SelectedSensorType { get => _SelectedSensorType; set { _SelectedSensorType = value; OnPropertyChanged(); } }

        #endregion

        #region Other infos
        private DateTime _CalibDate;
        private string _CertificateNo;
        private string _PerformedBy;
        private TM _SelectedTM;
        private ObservableCollection<TM> _AvailableTMs;
        private ObservableCollection<DoseQuantity> _AvailableDoseQuantities;
        private DoseQuantity _SelectedDoseQuantity;

        public DateTime CalibDate { get => _CalibDate; set { _CalibDate = value; OnPropertyChanged(); } }
        public string CertificateNo { get => _CertificateNo; set { _CertificateNo = value; OnPropertyChanged(); } }
        public string PerformedBy { get => _PerformedBy; set { _PerformedBy = value; OnPropertyChanged(); } }
        public TM SelectedTM { get => _SelectedTM; set { _SelectedTM = value; OnPropertyChanged(); } }
        public ObservableCollection<TM> AvailableTMs { get => _AvailableTMs; set { _AvailableTMs = value; OnPropertyChanged(); } }
        public ObservableCollection<DoseQuantity> AvailableDoseQuantities { get => _AvailableDoseQuantities; set { _AvailableDoseQuantities = value; OnPropertyChanged(); } }
        public DoseQuantity SelectedDoseQuantity { get => _SelectedDoseQuantity; set { _SelectedDoseQuantity = value; OnPropertyChanged(); } }
        #endregion

        #region Calib Data
        private ObservableCollection<CalibDataDTO> _CalibDatas;
        private RadQuantity _SelectedRadQuantity;
        private Unit _SelectedRefUnit;
        private Unit _SelectedMachineUnit;

        private ObservableCollection<RadQuantity> _AvailableRadQuantities;
        private ObservableCollection<Unit> _AvailableUnits;

        public ObservableCollection<CalibDataDTO> CalibDatas { get => _CalibDatas; set { _CalibDatas = value; OnPropertyChanged(); } }
        public RadQuantity SelectedRadQuantity { get => _SelectedRadQuantity; set { _SelectedRadQuantity = value; OnPropertyChanged(); } }
        public Unit SelectedRefUnit { get => _SelectedRefUnit; set { _SelectedRefUnit = value; OnPropertyChanged(); OnPropertyChanged(nameof(CF_Unit)); } }
        public Unit SelectedMachineUnit { get => _SelectedMachineUnit; set { _SelectedMachineUnit = value; OnPropertyChanged(); OnPropertyChanged(nameof(CF_Unit)); } }

        public ObservableCollection<RadQuantity> AvailableRadQuantities { get => _AvailableRadQuantities; set { _AvailableRadQuantities = value; OnPropertyChanged(); } }
        public ObservableCollection<Unit> AvailableUnits { get => _AvailableUnits; set { _AvailableUnits = value; OnPropertyChanged(); } }

        public string CF_Unit
        {
            get
            {
                if (SelectedRefUnit != null & SelectedMachineUnit != null)
                {
                    return SelectedRefUnit.Name == SelectedMachineUnit.Name ? "No unit" : $"({SelectedRefUnit.Name})/({SelectedMachineUnit.Name})";
                }
                else
                {
                    return null;
                }
                    
            }
        }

        #endregion

        #region Command
        public ICommand AddSensorRowCommand { get; set; }
        public ICommand RemoveSensorRowCommand { get; set; }
        public ICommand AddCalibDataRowCommand { get; set; }
        public ICommand RemoveCalibDataRowCommand { get; set; }
        public ICommand UpdateAvgReadingCommand { get; set; }

        public ICommand SaveCertificateFormCommand { get; set; }
        public ICommand CancelCertificateFormCommand { get; set; }
        public ICommand NewCertificateFormCommand { get; set; }


        #endregion

        public CertificateFormViewModel(MainViewModel mainViewModel, Certificate certificate, string operationMode, User currentUser)
        {
            #region Init
            Mouse.OverrideCursor = Cursors.Wait;

            CurrentUser = mainViewModel.CurrentUser;
            PerformedBy = mainViewModel.CurrentUser.FullName;
            //List<Customer> test = new List<Customer>();
            //test = (GlobalConfig.Connection.Customer_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            IsCertificateSaved = false;

            Customers = new ObservableCollection<Customer>(GlobalConfig.Connection.Customer_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            AvailableTMs = new ObservableCollection<TM>(GlobalConfig.Connection.TM_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            AvailableMachineNames = new ObservableCollection<MachineName>(GlobalConfig.Connection.MachineName_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            AvailableSensorTypes = new ObservableCollection<SensorType>(GlobalConfig.Connection.SensorType_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            AvailableMachineTypes = new ObservableCollection<MachineType>(GlobalConfig.Connection.MachineType_GetAll(GlobalConfig.CnnString("ICMSdatabase")));
            AvailableRadQuantities = new ObservableCollection<RadQuantity>(GlobalConfig.Connection.RadQuantity_GetActive(GlobalConfig.CnnString("ICMSdatabase")));
            AvailableUnits = new ObservableCollection<Unit>(GlobalConfig.Connection.Unit_GetActive(GlobalConfig.CnnString("ICMSdatabase")));
            AvailableDoseQuantities = new ObservableCollection<DoseQuantity>(GlobalConfig.Connection.DoseQuantity_GetActive(GlobalConfig.CnnString("ICMSdatabase")));

            if (certificate != null)
            {
                SelectedCustomer = Customers.FirstOrDefault(s => s.FullName == certificate.Customer.FullName);
                Temperature = certificate.Temperature.ToString();
                Pressure = certificate.Pressure.ToString();
                Humidity = certificate.Humidity.ToString();

                SelectedDoseQuantity = certificate.DoseQuantity;
                CalibDate = certificate.CalibDate;
                CertificateNo = certificate.CertificateNumber;

                PerformedBy = certificate.PerformedBy;
                SelectedTM = AvailableTMs.FirstOrDefault(s => s.Name == certificate.TM);


                SelectedMachineName = AvailableMachineNames.FirstOrDefault(s => s.Name == certificate.Machine.Name);
                SelectedMachineType = AvailableMachineTypes.FirstOrDefault(s => s.Name == certificate.Machine.MachineType.Name);
                MachineModel = certificate.Machine.Model;
                MachineSerial = certificate.Machine.Serial;
                MachineManufacturer = certificate.Machine.Manufacturer;
                MachineMadeIn = certificate.Machine.MadeIn;

                Sensors = new ObservableCollection<Sensor>(certificate.Machine.Sensors);

                SelectedRefUnit = AvailableUnits.FirstOrDefault(s => s.Name == certificate.CalibDatas[0].RefUnit);
                SelectedMachineUnit = AvailableUnits.FirstOrDefault(s => s.Name == certificate.CalibDatas[0].MachineUnit);

                List<CalibDataDTO> calibDatasDTO = new List<CalibDataDTO>();

                int i = 1;
                foreach (CalibData calibData in certificate.CalibDatas)
                {
                    CalibDataDTO calibDataDTO = new CalibDataDTO();
                    calibDataDTO.STT = i;
                    i = i + 1;

                    //calibDataDTO.RadQuantity =   calibData.RadQuantity;
                    calibDataDTO.RadQuantity = AvailableRadQuantities.FirstOrDefault(s => s.NameVN == calibData.RadQuantity.NameVN);
                    calibDataDTO.MachineReading = calibData.MachineReading;
                    //calibDataDTO.MachineUnit = AvailableUnits.FirstOrDefault(s => s.Name == calibData.MachineUnit);
                    calibDataDTO.RefValue = calibData.RefValue;
                    //calibDataDTO.RefUnit = AvailableUnits.FirstOrDefault(s => s.Name == calibData.RefUnit);
                    
                    calibDatasDTO.Add(calibDataDTO);
                }

                CalibDatas = new ObservableCollection<CalibDataDTO>(calibDatasDTO);

                if (operationMode == OperationMode.ViewMode.ToString())
                {
                    IsCertificateSaved = true;
                }
                
            }
            else
            {
                CalibDate = DateTime.Today;

                Sensors = new ObservableCollection<Sensor>(new List<Sensor>());
                Sensors.Add(new Sensor()
                {
                    Model = "",
                    Serial = "",
                    SensorType = new SensorType(),
                });

                // Show at leat 4 ref. value
                CalibDatas = new ObservableCollection<CalibDataDTO>(new List<CalibDataDTO>());

                CalibDatas.Add(new CalibDataDTO() { STT = 1, RefValue = 20.0 });
                CalibDatas.Add(new CalibDataDTO() { STT = 2, RefValue = 80.0 });
                CalibDatas.Add(new CalibDataDTO() { STT = 3, RefValue = 200.0 });
                CalibDatas.Add(new CalibDataDTO() { STT = 4, RefValue = 800.0 });


                CreateDemoCertificate();
            }
            Mouse.OverrideCursor = null;







            //CreateDemoCertificate();


            #endregion

            #region AddSensorRowCommand
            AddSensorRowCommand = new RelayCommand<object>
                     (
                        (p) => {
                            if (Sensors.Count >= AppSettings.AppSettings.MAXSENSORS)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        },
                        (p) =>
                        {
                            Sensors.Add(new Sensor()
                            {
                                Model = "newmodel",
                                Serial = "N/A",
                                SensorType = new SensorType(),
                                //AvailbleSensorType = AvailableSensorType,
                                //AvailbleSensorTypeString = availbleSensorTypeString
                            });

                            Trace.WriteLine(Sensors.Count);
                        }
                    );
            #endregion

            #region RemoveSensorRowCommand
            RemoveSensorRowCommand = new RelayCommand<object>
                     (
                        (p) => 
                        {
                            if (Sensors.Count <= 1)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        },
                        (p) =>
                        {
                            if (Sensors.Count >= 2)
                            {
                                Sensors.RemoveAt(Sensors.Count - 1);
                                Trace.WriteLine(Sensors.Count);
                            }

                        }
                    );
            #endregion

            #region RemoveCalibDataRowCommand
            RemoveCalibDataRowCommand = new RelayCommand<object>
                     (
                        (p) =>
                        {
                            if ( CalibDatas==null) { return false;  }
                            if ( CalibDatas.Count <= 1 ) { return false; }

                            return true;
                        },
                        (p) =>
                        {
                            if (CalibDatas.Count >= 2)
                            {
                                CalibDatas.RemoveAt(CalibDatas.Count - 1);
                            }

                        }
                    );
            #endregion

            #region AddCalibDataRowCommand
            AddCalibDataRowCommand = new RelayCommand<object>
                     (
                        (p) => {
                            if (CalibDatas.Count >= AppSettings.AppSettings.MAXCALIBDATA)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        },
                        (p) =>
                        {
                            CalibDataDTO calibData = new CalibDataDTO()
                            {
                                STT = CalibDatas.Count() + 1,
                                RadQuantity = CalibDatas.Last().RadQuantity
                                
                            };

                            CalibDatas.Add(calibData);
                        }
                    );
            #endregion

            #region SaveCertificateFormCommand
            SaveCertificateFormCommand = new RelayCommand<object>
                     (
                        (p) =>
                        {
                            if (CertificateFormIsValid())
                            {
                                if (IsCertificateSaved)
                                {
                                    return false;
                                }
                                else
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        },
                        (p) =>
                        {
                            try
                            {
                                // Show recommand messagge
                                List<string> messages = RecommendMessageFromInputForm();
                                var finalResult = MessageBoxResult.Yes;

                                if (messages != null)
                                {
                                    foreach (string message in messages)
                                    {
                                        var result = MessageBox.Show($"{message}\n\nAre you sure ?", "Infos", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                                        if (result == MessageBoxResult.No) { finalResult = MessageBoxResult.No; }
                                        break;
                                    }
                                }

                                if (finalResult == MessageBoxResult.Yes)
                                {
                                    //MessageBox.Show("Certificate has been saved to the databse !", "Infos", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                    Certificate newCertificate = CreateCertificateFromInputForm();
                                    GlobalConfig.Connection.Certificate_Insert(newCertificate, GlobalConfig.CnnString("ICMSdatabase"));

                                    IsCertificateSaved = true;
                                }
                               
                            }
                            catch (SqlException ex)
                            {
                                    MessageBox.Show(ex.Errors[0].Message, "SQL Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                IsCertificateSaved = false;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Could not insert this certificate into database !\n\n{ex.Message}\n{ex.StackTrace}", "Unknown Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                IsCertificateSaved = false;
                            }
                            

                        //MessageBox.Show(newCertificate.CalibDate.ToString());
                        //MessageBox.Show(newCertificate.Customer.FullName + "\n" + newCertificate.Customer.Address);
                        //MessageBox.Show("Temperature: " + "\n" + newCertificate.Temperature.ToString());
                        //MessageBox.Show("Humidity: " + "\n" + newCertificate.Humidity.ToString());
                        //MessageBox.Show("Pressure: " + "\n" + newCertificate.Pressure.ToString());
                        //MessageBox.Show("Performed By: " + "\n" + newCertificate.PerformedBy);
                        //MessageBox.Show("Certificate no: " + "\n" + newCertificate.CertificateNumber);
                        // MessageBox.Show("TM: " + "\n" + newCertificate.TM);
                        //MessageBox.Show("Machine Name " + "\n" + newCertificate.Machine.Name);
                        //MessageBox.Show("Machine Type " + "\n" + newCertificate.Machine.MachineType.Name);
                        //MessageBox.Show("Machine Model: " + "\n" + newCertificate.Machine.Model);
                        //MessageBox.Show("Machine Model: " + "\n" + newCertificate.Machine.Serial);
                        //MessageBox.Show("Machine Manufacturer: " + "\n" + newCertificate.Machine.Manufacturer);

                            //MessageBox.Show("Machine sensor: " + "\n" + "Number of sensor: " + Sensors.Count);
                            //MessageBox.Show("Machine sensor 1: \n" + "  SensorType: " + Sensors[0].SensorType.Name + "\n  Sensor Model: " + Sensors[0].Model + "\n  Sensor Serial: " + Sensors[0].Serial);

                            //MessageBox.Show("Machine sensor: " + "\n" + "Number of sensor: " + newCertificate.Machine.Sensors.Count);
                            //MessageBox.Show("Machine sensor 2: \n" + "  " +
                            //    "SensorType: " + newCertificate.Machine.Sensors[1].SensorType.Name + 
                            //    "\n  Sensor Model: " +  newCertificate.Machine.Sensors[1].Model + 
                            //    "\n  Sensor Serial: " + newCertificate.Machine.Sensors[1].Serial);

                            //MessageBox.Show("Calibdata: " + "\n" + "Number of data: " + CalibDatas.Count());
                            //MessageBox.Show("Calib Data 1: \n" + 
                            //    "Rad. Quantity " + CalibDatas[0].RadQuantity.Name
                            //    + "\n  Ref. Value: " + CalibDatas[0].RefValue
                            //    + "\n  Ref. Unit: " + CalibDatas[0].RefUnit.Name
                            //    //+ "\n  Machie. Unit: " + CalibDatas[0].MachineUnit 
                            //    );

                            //MessageBox.Show("Machine reading: " + CalibDatas[0].MachineReading)
                            //MessageBox.Show("Avg Reading: " + CalibDatas[0].AvgReading);

                        }
                    );
            #endregion

            #region CancelCertificateFormCommand
            CancelCertificateFormCommand = new RelayCommand<object>
                     (
                        (p) =>{ return true; },
                        (p) =>
                        {
                            mainViewModel.CurrentCertificate = null;

                            mainViewModel.CurrentControl = new CertificateListViewModel(mainViewModel);
                        }
                    );
            #endregion

            #region NewCertificateFormCommand
            NewCertificateFormCommand = new RelayCommand<object> (
                (p) => 
                { 
                    if (IsCertificateSaved) 
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                            
                },
                (p) =>
                {
                    IsCertificateSaved = false;

                    SelectedMachineName = null;
                    SelectedMachineType = null;
                    MachineModel = null;
                    MachineSerial = null;
                    MachineManufacturer = null;
                    MachineMadeIn = null;

                    Sensors = new ObservableCollection<Sensor>(new List<Sensor>())
                    {
                        new Sensor()
                        {
                            Model = "",
                            Serial = "",
                            SensorType = new SensorType(),
                        }
                    };

                    CertificateNo = "";

                    foreach (CalibDataDTO calibData in CalibDatas)
                    {
                        calibData.MachineReading = null;
                    }
                    
                }
            );
            #endregion
        }


        #region private function
        private Certificate CreateCertificateFromInputForm()
        {
            Certificate newCertificate = new Certificate();

            newCertificate.Customer = SelectedCustomer;

            newCertificate.DoseQuantity = SelectedDoseQuantity;

            newCertificate.Temperature = double.Parse(Temperature);

            newCertificate.Humidity = double.Parse(Humidity);

            newCertificate.Pressure = double.Parse(Pressure);

            newCertificate.CalibDate = CalibDate;
            newCertificate.CertificateNumber = CertificateNo;
            newCertificate.PerformedBy = PerformedBy;
            if (SelectedTM != null)
            {
                newCertificate.TM = SelectedTM.Name;
            }
            


            Machine newMachine = new Machine();
            newMachine.Name = SelectedMachineName.Name;
            newMachine.MachineType = SelectedMachineType;
            newMachine.Model = MachineModel;
            newMachine.Serial = MachineSerial;
            newMachine.Manufacturer = MachineManufacturer;
            newMachine.MadeIn = MachineMadeIn;

            newMachine.Sensors = Sensors.ToList();

            newCertificate.Machine = newMachine;

            List<CalibData> newCalibDatas = new List<CalibData>();
           
            foreach (CalibDataDTO calib in CalibDatas)
            {
                CalibData newCalibData = new CalibData();
                newCalibData.RadQuantity = calib.RadQuantity;
                newCalibData.MachineReading = calib.MachineReading;
                newCalibData.AvgReading = calib.AvgReading;
                newCalibData.MachineUnit = SelectedMachineUnit.Name;
                newCalibData.RefValue = calib.RefValue;
                newCalibData.RefUnit = SelectedRefUnit.Name;
                newCalibData.CF = calib.CF;
                newCalibData.CF_reUnc = calib.CF_reUnc;

                newCalibDatas.Add(newCalibData);

            }

            newCertificate.CalibDatas = newCalibDatas;

            return newCertificate;
        }

        private void CreateDemoCertificate()
        {
            SelectedCustomer = Customers[0];
            Temperature = 23.4.ToString();
            Pressure = 1006.ToString();
            Humidity = 50.ToString();

            SelectedDoseQuantity = AvailableDoseQuantities[0];
            CalibDate = DateTime.Now.Date;
            CertificateNo = $"{DateTime.Now.Year}{DateTime.Now.Month}.{DateTime.Now.Second}";
            PerformedBy = "Nguyễn Ngọc Quỳnh";
            SelectedTM = AvailableTMs[0];

            SelectedMachineName = AvailableMachineNames[2];
            SelectedMachineType = AvailableMachineTypes[1];

            SelectedRefUnit = AvailableUnits[0];
            SelectedMachineUnit= AvailableUnits[0];
            MachineModel = "Demo model";
            MachineSerial = "Demo Serial";
            MachineManufacturer = "Fluke";
            MachineMadeIn = "Đức";

            CalibDatas = new ObservableCollection<CalibDataDTO>(new List<CalibDataDTO>());

            CalibDatas.Add(new CalibDataDTO() { STT = 1, RadQuantity = AvailableRadQuantities[2], RefValue = 20.0, MachineReading = "23; 24; 25;26", MachineUnit = AvailableUnits[0], RefUnit = AvailableUnits[0] });
            CalibDatas.Add(new CalibDataDTO() { STT = 2, RadQuantity = AvailableRadQuantities[1], RefValue = 80.0 ,MachineReading = "23; 24; 25; 26", MachineUnit = AvailableUnits[0], RefUnit = AvailableUnits[0] });
            CalibDatas.Add(new CalibDataDTO() { STT = 3, RadQuantity = AvailableRadQuantities[3], RefValue = 200.0, MachineReading = "23; 24; 25; 26", MachineUnit = AvailableUnits[0], RefUnit = AvailableUnits[0] });
            CalibDatas.Add(new CalibDataDTO() { STT = 4, RadQuantity = AvailableRadQuantities[4], RefValue = 800.0, MachineReading = "23; 24; 25; 26",  MachineUnit = AvailableUnits[0], RefUnit = AvailableUnits[0] });
        }

        private bool CertificateFormIsValid()
        {
            //bool output = false;
            bool isDouble;
            // Customer
            if (SelectedCustomer == null) { return false; }

            // Temperature
            isDouble = double.TryParse(Temperature, out _);
            if (!isDouble) { return false; }

            // Pressure
            isDouble = double.TryParse(Pressure, out _);
            if (!isDouble) { return false; }

            // Humidity
            isDouble = double.TryParse(Humidity, out _);
            if (!isDouble) { return false; }

            // Dose Quantity
            if (SelectedDoseQuantity == null) { return false; }

            // CalibDate
            if (CalibDate == null) { return false; }

            // Certificate Number
            if (string.IsNullOrWhiteSpace(CertificateNo)) { return false; }

            // Performed By
            if (string.IsNullOrWhiteSpace(PerformedBy)) { return false; }

            // Machine.Name
            if (SelectedMachineName == null) { return false; }

            // Machine.MachineType
            if (SelectedMachineType == null) { return false; }

            // Machine.Serial
            if (string.IsNullOrWhiteSpace(MachineSerial)) { return false; }

            // Sensors
            //if (Sensors.Count() >= 1)
            //{
            //    foreach (Sensor sensor in Sensors)
            //    {
            //        bool hasModel = !string.IsNullOrWhiteSpace(sensor.Model);
            //        bool hasSerial = !string.IsNullOrWhiteSpace(sensor.Serial);
            //        bool hasSensorType = sensor.SensorType != null;
            //    }
            //}

            // MachineUnit
            if (SelectedMachineUnit == null) { return false; }

            // RefUnit
            if (SelectedRefUnit == null) { return false; }

            // Calibdata
            if ( (CalibDatas == null) | (CalibDatas.Count() < 1))
            {
                return false;
            }
            else
            {
                foreach (CalibDataDTO calibData in CalibDatas)
                {
                    if (calibData.RadQuantity == null) { return false; }

                    if (calibData.RefValue <= 0) { return false; }

                    if (calibData.AvgReading == double.NaN) { return false; }
                }
            }



            return true;
        }

        private List<string> RecommendMessageFromInputForm()
        {
            List<string> recommendMessage = new List<string>();

            if ( string.IsNullOrWhiteSpace(MachineModel) ) { recommendMessage.Add("Machine model is empty"); }

            if ( string.IsNullOrWhiteSpace(MachineSerial) ) { recommendMessage.Add("Machine serial is empty"); }

            if ( string.IsNullOrWhiteSpace(MachineManufacturer) ) { recommendMessage.Add("Machine's manufacturer is empty"); }

            if ( string.IsNullOrWhiteSpace(MachineMadeIn) ) { recommendMessage.Add("Country is empty"); }

            if (SelectedTM == null) { recommendMessage.Add("Technical Manager (TM) is empty"); }

            if (Sensors == null) { recommendMessage.Add("No information abount machine's sensor"); }

            return recommendMessage;
        }
        #endregion
    }



}
