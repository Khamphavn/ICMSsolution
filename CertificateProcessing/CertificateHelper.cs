using ICMS.Model.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMS.CertificateProcessing
{
    class CertificateHelper
    {
        public static string certificateTempoFilePdfFormat(Certificate certificate)
        {
            string fileName;
            //fileName = "tmp." + certificate.CertificateNumber + ".pdf";
            fileName = "tmp.pdf";

            return fileName;
        }

        public static void ConvertWordToPDF(string path, string exportDir)
        {
            Application app = new Application();
            app.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            app.Visible = false;

            var objPresSet = app.Documents;
            var objPres = objPresSet.Open(path);

            var pdfFileName = Path.ChangeExtension(path, ".pdf");
            var pdfPath = Path.Combine(exportDir, pdfFileName);

            try
            {
                objPres.ExportAsFixedFormat(
                    pdfPath,
                    WdExportFormat.wdExportFormatPDF,
                    false,
                    WdExportOptimizeFor.wdExportOptimizeForPrint,
                    WdExportRange.wdExportAllDocument
                );
            }
            catch
            {
                throw;
            }
            finally
            {
                objPres.Close();
            }

        }
        public static string GetTempoFilePdfFullPath(Certificate certificate)
        {
            string tempoFileName = certificateTempoFilePdfFormat(certificate);

            string currentDirectoryPath = Directory.GetCurrentDirectory();

            string tempoFilePath = Path.Combine(currentDirectoryPath, "tmp", tempoFileName);

            return tempoFilePath;
        }

        public static void CreateTmpFolderIfNotExist()
        {
            string tmpFolder = Path.Combine(Directory.GetCurrentDirectory(), "tmp");

            bool isExist = Directory.Exists(tmpFolder);

            if (!isExist)
            {
                Directory.CreateDirectory(tmpFolder);
            }
        }
        public static string SelectTemplateWordFile(Certificate certificate)
        {
            List<RadQuantity> radQuantities = certificate.CalibDatas.Select(o => o.RadQuantity).ToList();
            List<RadQuantity> uniqueRadQuantities = radQuantities.GroupBy(p => p.NameVN).Select(g => g.First()).ToList();


            bool gammaRad = false;
            bool xrayRad = false;


            foreach (RadQuantity radQuantity in uniqueRadQuantities)
            {
                gammaRad = gammaRad | radQuantity.NameVN.Contains("Cs") | radQuantity.NameVN.Contains("Co");
                xrayRad = xrayRad | radQuantity.NameVN.Contains("ISO");
            }


            if (gammaRad == true & xrayRad == true)
            {
                return "Template_Xray_Gamma.docx";
            }
            else
            {
                if (gammaRad == false)
                {
                    return "Template_Xray.docx";
                }
                else
                {
                    return "Template_Gamma.docx";
                }
            }

        }

        public static string SelectTemplateXMLFile(Certificate certificate)
        {
            List<RadQuantity> radQuantities = certificate.CalibDatas.Select(o => o.RadQuantity).ToList();
            List<RadQuantity> uniqueRadQuantities = radQuantities.GroupBy(p => p.NameVN).Select(g => g.First()).ToList();


            bool gammaRad = false;
            bool xrayRad = false;


            foreach (RadQuantity radQuantity in uniqueRadQuantities)
            {
                gammaRad = gammaRad | radQuantity.NameVN.Contains("Cs") | radQuantity.NameVN.Contains("Co");
                xrayRad = xrayRad | radQuantity.NameVN.Contains("ISO");
            }


            if (gammaRad == true & xrayRad == true)
            {
                return "Template_Xray_Gamma.xml";
            }
            else
            {
                if (gammaRad == false)
                {
                    return "Template_Xray.xml";
                }
                else
                {
                    return "Template_Gamma.xml";
                }
            }

        }

        public static string GetTemplateFileFullPath(Certificate certificate)
        {
            string templateFileName = SelectTemplateWordFile(certificate);

            string templateFolderPath = AppSettings.AppSettings.TEMPLATEFOLDER;
            //string templateFolderPath = @"C:\Users\nnquynh-laptop\source\repos\QLCM_newVersion\NewDatabase\ConsoleTest.PrintCertificate\bin\Debug\CertificateTemplates";

            string templateFilePath = Path.Combine(templateFolderPath, templateFileName);

            return templateFilePath;
        }
        public static string GetTemplateFileXMLFullPath(Certificate certificate)
        {
            string templateFileName = SelectTemplateXMLFile(certificate);

            string templateFolderPath = @"C:\Users\nnquynh-laptop\source\repos\QLCM_newVersion\NewDatabase\ConsoleTest.PrintCertificate\bin\Debug\CertificateTemplates";

            string templateFilePath = Path.Combine(templateFolderPath, templateFileName);

            return templateFilePath;
        }

        public static void FindAndReplace(Word.Application wordApp, object TextToFind, object TextToReplace)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCard = false;
            object matchSoundLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object warp = 1;
            object replace = 2;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;


            wordApp.Selection.Find.Execute(ref TextToFind,
                ref matchCase, ref matchWholeWord,
                ref matchWildCard, ref matchSoundLike,
                ref matchAllWordForms,
                ref forward,
                ref warp, ref format,
                ref TextToReplace, ref replace, ref matchKashida,
                ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }

        public static string DoubleToStringWithDecimalNumber(double number, int nbrDecimalNumber)
        {
            string formatString = String.Format("F{0:D}", nbrDecimalNumber);
            CultureInfo frFR = CultureInfo.CreateSpecificCulture("fr-FR");
            return number.ToString(formatString, frFR);
        }

        public static bool isTempoFileDelete(string tempoFilePath)
        {
            // Delete tempo file before create. If file could not be deleted, file is in use by another process
            try
            {
                File.Delete(tempoFilePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }


        public static bool isTempoFileOK(string tempoFilePath)
        {
            return isTempoFileDelete(tempoFilePath);
        }

        public static bool isTemplateFileOK(string templateFilePath)
        {
            if (File.Exists(templateFilePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void CreateFormattedCertificate(Certificate certificate, Word.Application wordApp, Word.Document myWordDoc)
        {
            try
            {
                #region Find and Replace infos in template file
                CertificateHelper.FindAndReplace(wordApp, "[[CertificationNumber]]", certificate.CertificateNumber);
                CertificateHelper.FindAndReplace(wordApp, "[[CalibDate]]", certificate.CalibDate.ToString("dd/MM/yyyy"));
                CertificateHelper.FindAndReplace(wordApp, "[[CalibDueDate]]", certificate.CalibDate.AddYears(1).ToString("dd/MM/yyyy"));
                CertificateHelper.FindAndReplace(wordApp, "[[CustomerName]]", certificate.Customer.FullName);
                CertificateHelper.FindAndReplace(wordApp, "[[CustomerAddress]]", certificate.Customer.Address);
                CertificateHelper.FindAndReplace(wordApp, "[[MachineName]]", certificate.Machine.Name);
                CertificateHelper.FindAndReplace(wordApp, "[[MachineModel]]", certificate.Machine.Model);
                CertificateHelper.FindAndReplace(wordApp, "[[MachineSeri]]", certificate.Machine.Serial);
                CertificateHelper.FindAndReplace(wordApp, "[[MachineType]]", certificate.Machine.MachineType.Name);

                CertificateHelper.FindAndReplace(wordApp, "[[SensorModel]]", certificate.Machine.Sensors[0].Model);
                CertificateHelper.FindAndReplace(wordApp, "[[SensorSeri]]", certificate.Machine.Sensors[0].Serial);

                if (certificate.Machine.Sensors[0].SensorType != null)
                {
                    CertificateHelper.FindAndReplace(wordApp, "[[SensorType]]", certificate.Machine.Sensors[0].SensorType.Name);
                }
                else
                {
                    CertificateHelper.FindAndReplace(wordApp, "[[SensorType]]", "");
                }

                CertificateHelper.FindAndReplace(wordApp, "[[Manufacturer]]", certificate.Machine.Manufacturer);
                CertificateHelper.FindAndReplace(wordApp, "[[T]]", DoubleToStringWithDecimalNumber(certificate.Temperature, AppSettings.AppSettings.DECIMALPOINTTEMPERATURE));
                CertificateHelper.FindAndReplace(wordApp, "[[H]]", DoubleToStringWithDecimalNumber(certificate.Humidity, AppSettings.AppSettings.DECIMALPOINTUMIDITY));
                CertificateHelper.FindAndReplace(wordApp, "[[P]]", DoubleToStringWithDecimalNumber(certificate.Pressure, AppSettings.AppSettings.DECIMALPOINTPRESSURE));
                CertificateHelper.FindAndReplace(wordApp, "[[PerformBy]]", certificate.PerformedBy);
                CertificateHelper.FindAndReplace(wordApp, "[[TM]]", certificate.TM);

                CertificateHelper.FindAndReplace(wordApp, "[[CalibYear]]", certificate.CalibDate.Year);
                #endregion

                #region Result table
                // Get table dimension: nbr RadQuantity, Ref. Values
                List<RadQuantity> radQuantities = certificate.CalibDatas.Select(o => o.RadQuantity).ToList();
                List<RadQuantity> uniqueRadQuantities = radQuantities.GroupBy(p => p.NameVN).Select(g => g.First()).ToList();
                int nbrRefValues = certificate.CalibDatas.Count();
                int nbrUniqueRadQuantities = uniqueRadQuantities.Count();
                List<int> nbrRefValueInEachRadQuanity = new List<int>();

                //for (int i = 0; i < uniqueRadQuantities.Count; i++)
                //{
                //    int nbrRefValue = certificate.CalibDatas.Where(s => s.RadQuantity.NameVN == uniqueRadQuantities[i].NameVN).Count();
                //    nbrRefValueInEachRadQuanity.Add(nbrRefValue);
                //}

                foreach (RadQuantity radQuantity in uniqueRadQuantities)
                {
                    int nbrRefValue = certificate.CalibDatas.Where(s => s.RadQuantity.NameVN == radQuantity.NameVN).Count();
                    nbrRefValueInEachRadQuanity.Add(nbrRefValue);
                }

                Word.Range rng = myWordDoc.Range();
                if (rng.Find.Execute("[[ResultTable]]"))
                {
                    #region Create table
                    Word.Table resultsTable = myWordDoc.Tables.Add(rng, nbrRefValues + 2, 5); // 5 columns
                    resultsTable.Borders.Enable = 1;
                    resultsTable.Range.Font.Bold = 0;
                    resultsTable.Range.Font.Size = 13;
                    resultsTable.Range.ParagraphFormat.SpaceBefore = 2f;
                    resultsTable.Range.ParagraphFormat.SpaceAfter = 2f;
                    resultsTable.Range.ParagraphFormat.RightIndent = 0f;
                    resultsTable.Range.Font.Name = "Times New Roman";
                    resultsTable.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    resultsTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    resultsTable.Range.Rows.Alignment = Word.WdRowAlignment.wdAlignRowCenter;

                    resultsTable.Columns[1].SetWidth(wordApp.CentimetersToPoints(3.6f), Word.WdRulerStyle.wdAdjustFirstColumn);   // Radiation quality
                    resultsTable.Columns[2].SetWidth(wordApp.CentimetersToPoints(2.5f), Word.WdRulerStyle.wdAdjustFirstColumn);   // Energy
                    resultsTable.Columns[3].SetWidth(wordApp.CentimetersToPoints(4.1f), Word.WdRulerStyle.wdAdjustFirstColumn);   // Reference value
                    resultsTable.Columns[4].SetWidth(wordApp.CentimetersToPoints(4.0f), Word.WdRulerStyle.wdAdjustFirstColumn); // Indicated of instrument
                    resultsTable.Columns[5].SetWidth(wordApp.CentimetersToPoints(3.5f), Word.WdRulerStyle.wdAdjustFirstColumn); // Calibration Factor
                    #endregion

                    #region Header

                    float LEFTPADDING = 0.4f;
                    float RIGHTPADDING = 0.4f;

                    // Radiation quality
                    resultsTable.Cell(1, 1).LeftPadding = LEFTPADDING;
                    resultsTable.Cell(1, 1).RightPadding = RIGHTPADDING;
                    resultsTable.Cell(1, 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalTop;
                    resultsTable.Cell(1, 1).Range.Text = "Phẩm chất bức xạ\v" + "(Radiation quality)";
                    object oStart = resultsTable.Cell(1, 1).Range.Start + 17;
                    object oEnd = resultsTable.Cell(1, 1).Range.Start + 36;
                    Word.Range englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Italic = 1;
                    englishText.Font.Size = 11;

                    // Energy
                    resultsTable.Cell(1, 2).LeftPadding = LEFTPADDING;
                    resultsTable.Cell(1, 2).RightPadding = RIGHTPADDING;
                    resultsTable.Cell(1, 2).Range.Text = "Năng lượng\v" + "(Energy)\v" + "[keV]";
                    oStart = resultsTable.Cell(1, 2).Range.Start + 11;
                    oEnd = resultsTable.Cell(1, 2).Range.Start + 19;
                    englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Italic = 1;
                    englishText.Font.Size = 11;

                    // Reference value:               "Giá trị chuẩn, Kair\v" + "(Reference value)\v" + "[" + certiInfo.Unit + "]";
                    resultsTable.Cell(1, 3).LeftPadding = LEFTPADDING;
                    resultsTable.Cell(1, 3).RightPadding = RIGHTPADDING;
                    string refValueVN = "Giá trị chuẩn, " + certificate.DoseQuantity.Notation;
                    string refValueEN = "(Reference value)";
                    string refUnit = certificate.CalibDatas[0].RefUnit;
                    resultsTable.Cell(1, 3).Range.Text = refValueVN + "\v" + refValueEN + "\v" + "[" + refUnit + "]";
                    oStart = resultsTable.Cell(1, 3).Range.Start + refValueVN.Length + 1;
                    oEnd = resultsTable.Cell(1, 3).Range.Start + refValueVN.Length + refValueEN.Length + 1;
                    englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Italic = 1;
                    englishText.Font.Size = 11;
                    //englishText.Font.Color = Word.WdColor.wdColorRed;

                    // Machine reading:               "Chỉ thị của thiết bị\v" + "(Instrument indicator)\v" + "[" + certiInfo.Unit + "]";
                    resultsTable.Cell(1, 4).LeftPadding = LEFTPADDING;
                    resultsTable.Cell(1, 4).RightPadding = RIGHTPADDING;
                    string machineReadingVN = "Chỉ thị của thiết bị";
                    string machineReadingEN = "(Instrument indicator)";
                    string machineUnit = certificate.CalibDatas[0].MachineUnit;
                    resultsTable.Cell(1, 4).Range.Text = machineReadingVN + "\v" + machineReadingEN + "\v" + "[" + machineUnit + "]";
                    oStart = resultsTable.Cell(1, 4).Range.Start + machineReadingVN.Length + 1;
                    oEnd = resultsTable.Cell(1, 4).Range.Start + machineReadingVN.Length + machineReadingEN.Length + 1;
                    englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Italic = 1;
                    englishText.Font.Size = 11;
                    //englishText.Font.Color = Word.WdColor.wdColorRed;

                    // Calib Factor
                    resultsTable.Cell(1, 5).LeftPadding = LEFTPADDING;
                    resultsTable.Cell(1, 5).RightPadding = RIGHTPADDING;
                    string calibFactorVN = "Hệ số chuẩn";
                    string calibFactorEN = "";
                    string notation = "(CF ± Uc)"; ;
                    if (certificate.CalibDatas[0].RefUnit == certificate.CalibDatas[0].MachineUnit)
                    {
                        calibFactorEN = "(Calibration factor)";
                        resultsTable.Cell(1, 5).Range.Text = calibFactorVN + "\v" + calibFactorEN + "\v" + notation;
                    }
                    else
                    {
                        calibFactorEN = "(Calibration coefficient)";
                        string CFunit = "[" + certificate.CalibDatas[0].RefUnit + "/" + certificate.CalibDatas[0].MachineUnit + "]";
                        resultsTable.Cell(1, 5).Range.Text = calibFactorVN + "\v" + calibFactorEN + "\v" + notation + "\v" + CFunit;
                    }
                    oStart = resultsTable.Cell(1, 5).Range.Start + calibFactorVN.Length + 1;
                    oEnd = resultsTable.Cell(1, 5).Range.Start + calibFactorVN.Length + calibFactorEN.Length + 1;
                    englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Italic = 1;
                    englishText.Font.Size = 11;
                    //englishText.Font.Color = Word.WdColor.wdColorRed;

                    oStart = resultsTable.Cell(1, 5).Range.Start + calibFactorVN.Length + calibFactorEN.Length + notation.Length;
                    oEnd = resultsTable.Cell(1, 5).Range.Start + calibFactorVN.Length + calibFactorEN.Length + notation.Length + 1;
                    englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Font.Subscript = 1;
                    //englishText.Font.Color = Word.WdColor.wdColorRed;

                    #endregion

                    #region Merge cell for RadQuantity and Energy
                    Word.Cell cell;
                    int firstRowMerge = 2;  //Starting of 2 row to ignore header row

                    for (int i = 0; i < uniqueRadQuantities.Count(); i++)
                    {
                        RadQuantity radQuantity = uniqueRadQuantities[i];

                        // Merge cell if there are more than 1 ref. values
                        if (nbrRefValueInEachRadQuanity[i] > 1)
                        {
                            cell = resultsTable.Cell(firstRowMerge, 1);
                            cell.Merge(resultsTable.Cell(cell.RowIndex + nbrRefValueInEachRadQuanity[i] - 1, 1));

                            cell = resultsTable.Cell(firstRowMerge, 2);
                            cell.Merge(resultsTable.Cell(cell.RowIndex + nbrRefValueInEachRadQuanity[i] - 1, 2));
                        }

                        //write radQuantity Name
                        string radCategory = "";
                        if (radQuantity.NameVN == "Cs-137" | radQuantity.NameVN == "Co-60")
                        {
                            radCategory = "Gamma";
                        }
                        else
                        {
                            radCategory = "Tia X";
                        }
                        resultsTable.Cell(firstRowMerge, 1).Range.Text = radCategory + "\v" + "(" + radQuantity.NameEN + ")";
                        oStart = resultsTable.Cell(firstRowMerge, 1).Range.Start + radCategory.Length + 1;
                        oEnd = resultsTable.Cell(firstRowMerge, 1).Range.Start + radCategory.Length + radQuantity.NameEN.Length + 3;
                        englishText = myWordDoc.Range(ref oStart, ref oEnd);
                        englishText.Italic = 1;
                        englishText.Font.Size = 11;
                        //englishText.Font.Color = Word.WdColor.wdColorRed;

                        //write radQuantity Energy 
                        resultsTable.Cell(firstRowMerge, 2).Range.Text = radQuantity.Energy.ToString();

                        if (nbrRefValueInEachRadQuanity[i] > 1)
                        {
                            firstRowMerge = firstRowMerge + nbrRefValueInEachRadQuanity[i];
                        }
                        else
                        {
                            firstRowMerge = firstRowMerge + 1;
                        }
                    }
                    #endregion

                    #region RefValues, machine reading, CF
                    for (int i = 0; i < nbrRefValues; i++)
                    {
                        resultsTable.Cell(i + 2, 3).Range.Text = CertificateHelper.DoubleToStringWithDecimalNumber(certificate.CalibDatas[i].RefValue, 1);
                        resultsTable.Cell(i + 2, 4).Range.Text = CertificateHelper.DoubleToStringWithDecimalNumber(certificate.CalibDatas[i].AvgReading, 1);
                        resultsTable.Cell(i + 2, 5).Range.Text = CertificateHelper.DoubleToStringWithDecimalNumber(certificate.CalibDatas[i].CF, 2)
                            + " ± "
                            + CertificateHelper.DoubleToStringWithDecimalNumber(certificate.CalibDatas[i].CF_reUnc * certificate.CalibDatas[i].CF * 2, 2);
                    }
                    #endregion

                    #region Notes at table bottom 
                    // Merge last row
                    cell = resultsTable.Cell(nbrRefValues + 2, 1);
                    cell.Merge(resultsTable.Cell(nbrRefValues + 2, 5));

                    cell.Range.Font.Size = 12;
                    cell.Range.Italic = 1;
                    cell.TopPadding = 5.0f;
                    cell.LeftPadding = 10f;
                    cell.RightPadding = 20f;
                    cell.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    cell.Range.ParagraphFormat.SpaceAfter = 6f;

                    string doseQuantityVN = "";
                    string doseQuantityEN = "";

                    if (certificate.CalibDatas[0].RefUnit.Contains("/h"))
                    {
                        doseQuantityVN = "-  " + certificate.DoseQuantity.Notation + " là suất " + certificate.DoseQuantity.NameVN.ToLower();
                        doseQuantityEN = "(" + certificate.DoseQuantity.Notation + " is the " + certificate.DoseQuantity.NameEN.ToLower() + "rate)";
                    }
                    else
                    {
                        doseQuantityVN = certificate.DoseQuantity.Notation + " là " + certificate.DoseQuantity.NameVN.ToLower();
                        doseQuantityEN = certificate.DoseQuantity.Notation + " is the " + certificate.DoseQuantity.NameEN.ToLower();
                    }

                    string CFequationTextVN = "-  Hệ số chuẩn = Giá trị chuẩn / Chỉ thị của thiết bị ";
                    string CFequationTextEN = "   (Calibration factor = Reference value / Instrument indicator)";

                    string uncertaintyTextVN = "-  Uc là độ không đảm bảo đo mở rộng, được công bố với hệ số phủ k = 2 tương ứng mức tin cậy xấp xỉ 95%. ";
                    string uncertaintyTextEN = "(Uc is the expanded uncertainty corresponding to a coverage factor, k = 2 with a confidence level, P ~ 95%)";

                    resultsTable.Cell(nbrRefValues + 2, 1).Range.Text = doseQuantityVN + doseQuantityEN + Environment.NewLine +
                                                                        CFequationTextVN + "\v" + CFequationTextEN + Environment.NewLine +
                                                                        uncertaintyTextVN + uncertaintyTextEN;

                    // format doseQuantityEN
                    oStart = resultsTable.Cell(nbrRefValues + 2, 1).Range.Start + doseQuantityVN.Length;
                    oEnd = resultsTable.Cell(nbrRefValues + 2, 1).Range.Start + doseQuantityVN.Length + doseQuantityEN.Length;
                    englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Italic = 1;
                    englishText.Font.Size = 11;
                    //englishText.Font.Color = Word.WdColor.wdColorRed;

                    // format CFequationTextEN
                    oStart = resultsTable.Cell(nbrRefValues + 2, 1).Range.Start + doseQuantityVN.Length + doseQuantityEN.Length + CFequationTextVN.Length + 2;
                    oEnd = resultsTable.Cell(nbrRefValues + 2, 1).Range.Start + doseQuantityVN.Length + doseQuantityEN.Length + CFequationTextVN.Length + CFequationTextEN.Length + 3;
                    englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Italic = 1;
                    englishText.Font.Size = 11;
                    //englishText.Font.Color = Word.WdColor.wdColorRed;

                    // format uncertaintyTextEN
                    oStart = resultsTable.Cell(nbrRefValues + 2, 1).Range.Start + doseQuantityVN.Length + doseQuantityEN.Length +
                                                                                  CFequationTextVN.Length + CFequationTextEN.Length +
                                                                                  uncertaintyTextVN.Length + 3;
                    oEnd = resultsTable.Cell(nbrRefValues + 2, 1).Range.Start + doseQuantityVN.Length + doseQuantityEN.Length +
                                                                                CFequationTextVN.Length + CFequationTextEN.Length +
                                                                                uncertaintyTextVN.Length + uncertaintyTextEN.Length + 5;
                    englishText = myWordDoc.Range(ref oStart, ref oEnd);
                    englishText.Italic = 1;
                    englishText.Font.Size = 11;
                    //englishText.Font.Color = Word.WdColor.wdColorRed;



                    #endregion

                }

                #endregion

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
