using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using ICMS.Model.Models;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocToPDFConverter;
using Syncfusion.OfficeChart;
using Syncfusion.OfficeChartToImageConverter;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Break = DocumentFormat.OpenXml.Wordprocessing.Break;

namespace ICMS.Bussiness.CertificateProcessing
{
    public class ProcessingCertificate
    {
        #region Open XML

        public int CreateTemporaryCertificatePdf(Certificate certificate)
        {
            string templateFilePath = CertificateHelper.GetTemplateFileFullPath(certificate);

            // Check template File
            bool isTemplateFileOK = CertificateHelper.isTemplateFileOK(templateFilePath);

            if (isTemplateFileOK)
            {
                try
                {
                    // Create word file
                    CreateTmpCertificateWord_OpenXML(certificate, templateFilePath);


                    // COnvert to pdf using interop.word
                    string tmpFolder = Path.Combine(Directory.GetCurrentDirectory(), "tmp");
                    string tmpWordFile = Path.Combine(tmpFolder, "tmp.docx");
                    string tmppdfFile = Path.Combine(tmpFolder, "tmp.pdf");

                    CertificateHelper.ConvertWordToPdf(tmpWordFile, tmppdfFile);

                    return 1;
                }
                catch (Exception)
                {
                    throw;
                }


            }
            else
            {
                return 0;
            }

        }

        public int ExportCertificateWordFile(Certificate certificate, string saveFileNamePath)
        {
            string templateFilePath = CertificateHelper.GetTemplateFileFullPath(certificate);

            // Check template File
            bool isTemplateFileOK = CertificateHelper.isTemplateFileOK(templateFilePath);


            if (isTemplateFileOK)
            {
                try
                {
                    CreateTmpCertificateWord_OpenXML(certificate, templateFilePath);

                    string tempWordDoc = Path.Combine(Directory.GetCurrentDirectory(), "tmp", "tmp.docx");
                    File.Copy(tempWordDoc, saveFileNamePath, true);

                    return 1;
                }
                catch (Exception)
                {
                    throw;
                }

            }
            else
            {
                return 0;
            }
        }


        public int CreateTmpCertificateWord_OpenXML(Certificate certificate, string templateFilePath)
        {
            CertificateHelper.CreateTmpFolderIfNotExist();
            string tempWordDoc = Path.Combine(Directory.GetCurrentDirectory(), "tmp", "tmp.docx");

            File.Copy(templateFilePath, tempWordDoc, true);

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(tempWordDoc, true))
            {
                #region Find and Replace fields

                string docText = null;
                using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                {
                    docText = sr.ReadToEnd();
                }

                Regex regexText = new Regex("\\[\\[CertificationNumber]]");
                docText = regexText.Replace(docText, certificate.CertificateNumber);

                regexText = new Regex("\\[\\[CustomerName]]");
                docText = regexText.Replace(docText, certificate.Customer.FullName);


                regexText = new Regex("\\[\\[CustomerAddress]]");
                docText = regexText.Replace(docText, certificate.Customer.Address);

                regexText = new Regex("\\[\\[MachineName]]");
                docText = regexText.Replace(docText, certificate.Machine.Name);

                regexText = new Regex("\\[\\[MachineModel]]");
                if (certificate.Machine.Manufacturer != null)
                {
                    docText = regexText.Replace(docText, certificate.Machine.Model);
                }
                else
                {
                    docText = regexText.Replace(docText, "");
                }


                regexText = new Regex("\\[\\[MachineSeri]]");
                docText = regexText.Replace(docText, certificate.Machine.Serial);

                regexText = new Regex("\\[\\[SensorType]]");

                var result = Regex.Match(docText, "\\[\\[SensorType]]");


                if (certificate.Machine.Sensors.Count != 0)
                {
                    if (certificate.Machine.Sensors[0].SensorType != null)
                    {
                        docText = regexText.Replace(docText, certificate.Machine.Sensors[0].SensorType.Name);
                    }
                    else
                    {
                        docText = regexText.Replace(docText, " ");
                    }
                }
                else
                {
                    docText = regexText.Replace(docText, " ");
                }

                regexText = new Regex("\\[\\[SensorModel]]");
                if (certificate.Machine.Sensors.Count != 0)
                {
                    if (certificate.Machine.Sensors[0].Model != null)
                    {
                        docText = regexText.Replace(docText, certificate.Machine.Sensors[0].Model);
                    }
                }
                else
                {
                    docText = regexText.Replace(docText, "");
                }
                

                regexText = new Regex("\\[\\[SensorSeri]]");
                if (certificate.Machine.Sensors.Count != 0)
                {
                    if (certificate.Machine.Sensors[0].Serial != null)
                    {
                        docText = regexText.Replace(docText, certificate.Machine.Sensors[0].Serial);
                    }
                }
                else
                {
                    docText = regexText.Replace(docText, "");
                }
                

                regexText = new Regex("\\[\\[Manufacturer]]");
                if (certificate.Machine.Manufacturer != null)
                {
                    docText = regexText.Replace(docText, certificate.Machine.Manufacturer);
                }
                else
                {
                    docText = regexText.Replace(docText,"");
                }
               

                regexText = new Regex("\\[\\[T]]");
                docText = regexText.Replace(docText, CertificateHelper.DoubleToStringWithDecimalNumber(certificate.Temperature, 1));

                regexText = new Regex("\\[\\[P]]");
                docText = regexText.Replace(docText, CertificateHelper.DoubleToStringWithDecimalNumber(certificate.Pressure, 0));

                regexText = new Regex("\\[\\[H]]");
                docText = regexText.Replace(docText, CertificateHelper.DoubleToStringWithDecimalNumber(certificate.Humidity, 1));

                regexText = new Regex("\\[\\[CalibDate]]");
                docText = regexText.Replace(docText, certificate.CalibDate.ToString("dd/MM/yyyy"));

                regexText = new Regex("\\[\\[CalibDueDate]]");
                docText = regexText.Replace(docText, certificate.CalibDate.AddYears(1).ToString("dd/MM/yyyy"));

                regexText = new Regex("\\[\\[CalibYear]]");

                docText = regexText.Replace(docText, certificate.CalibDate.Year.ToString());

                result = Regex.Match(docText, "\\[\\[TM]]");
                regexText = new Regex("\\[\\[TM]]");
                docText = regexText.Replace(docText, certificate.TM);

                regexText = new Regex("\\[\\[PerformBy]]");
                docText = regexText.Replace(docText, certificate.PerformedBy);


                using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                {
                    sw.Write(docText);
                }

                #endregion


                #region Table data

                #region Find table place holder
                string tablePlaceholder = "[[ResultTable]]";
                Text tablePl = wordDoc.MainDocumentPart.Document.Body
                    .Descendants<Text>()
                    .Where(x => x.Text.Contains(tablePlaceholder))
                    .First();
                #endregion

                Table table1 = new Table();

                #region Table Properties
                TableProperties tableProperties1 = new TableProperties();
                TableWidth tableWidth1 = new TableWidth() { Width = "10034", Type = TableWidthUnitValues.Dxa };
                TableJustification tableJustification1 = new TableJustification() { Val = TableRowAlignmentValues.Center };

                TableBorders tableBorders1 = new TableBorders();
                TopBorder topBorder1 = new TopBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                LeftBorder leftBorder1 = new LeftBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                BottomBorder bottomBorder1 = new BottomBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                RightBorder rightBorder1 = new RightBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };
                InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder() { Val = BorderValues.Single, Color = "auto", Size = (UInt32Value)4U, Space = (UInt32Value)0U };

                tableBorders1.Append(topBorder1);
                tableBorders1.Append(leftBorder1);
                tableBorders1.Append(bottomBorder1);
                tableBorders1.Append(rightBorder1);
                tableBorders1.Append(insideHorizontalBorder1);
                tableBorders1.Append(insideVerticalBorder1);
                TableLayout tableLayout1 = new TableLayout() { Type = TableLayoutValues.Fixed };
                TableLook tableLook1 = new TableLook() { Val = "0000", FirstRow = false, LastRow = false, FirstColumn = false, LastColumn = false, NoHorizontalBand = false, NoVerticalBand = false };

                tableProperties1.Append(tableWidth1);
                tableProperties1.Append(tableJustification1);
                tableProperties1.Append(tableBorders1);
                tableProperties1.Append(tableLayout1);
                tableProperties1.Append(tableLook1);

                table1.Append(tableProperties1);
                #endregion

                #region Table grid column
                TableGrid tableGrid1 = new TableGrid();
                GridColumn gridColumn1 = new GridColumn() { Width = "2041" };
                GridColumn gridColumn2 = new GridColumn() { Width = "1417" };
                GridColumn gridColumn3 = new GridColumn() { Width = "2324" };
                GridColumn gridColumn4 = new GridColumn() { Width = "2268" };
                GridColumn gridColumn5 = new GridColumn() { Width = "1984" };

                tableGrid1.Append(gridColumn1);
                tableGrid1.Append(gridColumn2);
                tableGrid1.Append(gridColumn3);
                tableGrid1.Append(gridColumn4);
                tableGrid1.Append(gridColumn5);

                table1.Append(tableGrid1);
                #endregion


                #region Table data

                TableRow headerRow = GenerateHeaderRow(certificate);
                table1.Append(headerRow);

                GenerateDataRow(table1, certificate);
                GenerateComment(table1, certificate);

                #endregion


                #region Insert the table to table holder

                var parent = tablePl.Parent.Parent.Parent;
                parent.InsertAfter(table1, tablePl.Parent.Parent);
                tablePl.Text = tablePl.Text.Replace(tablePlaceholder, "");

                #endregion

                #endregion

            }

            return 1;
        }


        private TableRow GenerateHeaderRow(Certificate certificate)
        {
            TableRow tableRow1 = new TableRow() { };

            #region Properties

            TablePropertyExceptions tablePropertyExceptions1 = new TablePropertyExceptions();

            TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
            TopMargin topMargin1 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
            BottomMargin bottomMargin1 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };

            tableCellMarginDefault1.Append(topMargin1);
            tableCellMarginDefault1.Append(bottomMargin1);

            tablePropertyExceptions1.Append(tableCellMarginDefault1);

            TableRowProperties tableRowProperties1 = new TableRowProperties();
            TableJustification tableJustification1 = new TableJustification() { Val = TableRowAlignmentValues.Center };

            tableRowProperties1.Append(tableJustification1);
            #endregion

            #region Common Properties
            Justification justificationCenter = new Justification() { Val = JustificationValues.Center };
            FontSize fontSize26 = new FontSize() { Val = "26" };
            FontSizeComplexScript fontSizeComplexScript26 = new FontSizeComplexScript() { Val = "26" };

            LeftMargin leftMargin8 = new LeftMargin() { Width = "8", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin8 = new RightMargin() { Width = "8", Type = TableWidthUnitValues.Dxa };

            TableCellMargin tableCellMargin8 = new TableCellMargin();
            tableCellMargin8.Append(leftMargin8);
            tableCellMargin8.Append(rightMargin8);

            RunFonts runFontsTimesNewRoman = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };

            TableCellVerticalAlignment tableCellVerticalAlignmentCenter = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };
            TableCellVerticalAlignment tableCellVerticalAlignmentTop = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Top };
            VerticalTextAlignment verticalTextAlignmentSubscrip = new VerticalTextAlignment() { Val = VerticalPositionValues.Subscript };

            Italic italic = new Italic();

            RunProperties runPropertiesNormal = new RunProperties();
            runPropertiesNormal.Append(runFontsTimesNewRoman);
            runPropertiesNormal.Append(fontSize26);
            runPropertiesNormal.Append(fontSizeComplexScript26);

            RunProperties runPropertiesItalic = new RunProperties();
            runPropertiesItalic.Append(runFontsTimesNewRoman.CloneNode(true));
            runPropertiesItalic.Append(fontSizeComplexScript26.CloneNode(true));
            runPropertiesItalic.Append(italic);


            ParagraphMarkRunProperties paragraphMarkRunPropertiesNormal = new ParagraphMarkRunProperties();
            paragraphMarkRunPropertiesNormal.Append(runFontsTimesNewRoman.CloneNode(true));
            paragraphMarkRunPropertiesNormal.Append(fontSize26.CloneNode(true));
            paragraphMarkRunPropertiesNormal.Append(fontSizeComplexScript26.CloneNode(true));

            ParagraphProperties paragraphPropertiesNormal = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines40 = new SpacingBetweenLines() { Before = "40", After = "40" };

            paragraphPropertiesNormal.Append(spacingBetweenLines40);
            paragraphPropertiesNormal.Append(justificationCenter);
            paragraphPropertiesNormal.Append(paragraphMarkRunPropertiesNormal);

            #endregion

            #region Rad Quantity
            TableCell tableCell1 = new TableCell();

            TableCellProperties tableCellProperties1 = new TableCellProperties();
            TableCellWidth tableCellWidth1 = new TableCellWidth() { Width = "2041", Type = TableWidthUnitValues.Dxa };

            LeftMargin leftMargin1 = new LeftMargin() { Width = "8", Type = TableWidthUnitValues.Dxa };
            RightMargin rightMargin1 = new RightMargin() { Width = "8", Type = TableWidthUnitValues.Dxa };

            tableCellProperties1.Append(tableCellWidth1);
            tableCellProperties1.Append(tableCellMargin8);

            Paragraph paragraph1 = new Paragraph() { };

            Run run1 = new Run();
            Text text1 = new Text();
            text1.Text = "Phẩm chất bức xạ";
            run1.Append(runPropertiesNormal);
            run1.Append(text1);

            Run run2 = new Run();
            Break break1 = new Break();
            run2.Append(runPropertiesNormal.CloneNode(true));
            run2.Append(break1);

            Run run3 = new Run() { };
            Text text2 = new Text();
            text2.Text = "(Radiation quality)";
            run3.Append(runPropertiesItalic.CloneNode(true));
            run3.Append(text2);

            paragraph1.Append(paragraphPropertiesNormal);
            paragraph1.Append(run1);
            paragraph1.Append(run2);
            paragraph1.Append(run3);

            tableCell1.Append(tableCellProperties1);
            tableCell1.Append(paragraph1);
            #endregion

            #region Energy
            TableCell tableCell2 = new TableCell();

            TableCellProperties tableCellProperties2 = new TableCellProperties();
            TableCellWidth tableCellWidth2 = new TableCellWidth() { Width = "1417", Type = TableWidthUnitValues.Dxa };

            tableCellProperties2.Append(tableCellWidth2);
            tableCellProperties2.Append(tableCellMargin8.CloneNode(true));
            tableCellProperties2.Append(tableCellVerticalAlignmentTop);

            Paragraph paragraph2 = new Paragraph() { };

            Run run4 = new Run();
            Text text3 = new Text();
            text3.Text = "Năng lượng";
            run4.Append(runPropertiesNormal.CloneNode(true));
            run4.Append(text3);

            Run run5 = new Run();
            Break break2 = new Break();

            run5.Append(runPropertiesNormal.CloneNode(true));
            run5.Append(break2);

            Run run6 = new Run() { };
            Text text4 = new Text();
            text4.Text = "(Energy)";

            run6.Append(runPropertiesItalic.CloneNode(true));
            run6.Append(text4);

            Run run7 = new Run();
            Break break3 = new Break();
            Text text5 = new Text();
            text5.Text = "[keV]";

            run7.Append(runPropertiesNormal.CloneNode(true));
            run7.Append(break3);
            run7.Append(text5);

            paragraph2.Append(paragraphPropertiesNormal.CloneNode(true));
            paragraph2.Append(run4);
            paragraph2.Append(run5);
            paragraph2.Append(run6);
            paragraph2.Append(run7);

            tableCell2.Append(tableCellProperties2);
            tableCell2.Append(paragraph2);
            #endregion

            #region Ref Values
            TableCell tableCell3 = new TableCell();

            TableCellProperties tableCellProperties3 = new TableCellProperties();
            TableCellWidth tableCellWidth3 = new TableCellWidth() { Width = "2324", Type = TableWidthUnitValues.Dxa };

            tableCellProperties3.Append(tableCellWidth3);
            tableCellProperties3.Append(tableCellMargin8.CloneNode(true));
            tableCellProperties3.Append(tableCellVerticalAlignmentTop.CloneNode(true));

            Paragraph paragraph3 = new Paragraph() { };

            Run run8 = new Run();
            Text text6 = new Text();       
            switch (certificate.DoseQuantity.NameEN.Trim())
            {
                case "Air-Kerma":
                    text6.Text = "Giá trị chuẩn, Kair";
                    break;
                case "Ambient Dose Equivalent":
                    text6.Text = "Giá trị chuẩn, H*(10)";
                    break;
                case "Personal Dose Equivalent":
                    text6.Text = "Giá trị chuẩn, Hp(10)";
                    break;
                case "Absorbed Dose":
                    text6.Text = "Giá trị chuẩn, D";
                    break;
                case "Ambient Dose":
                    text6.Text = "Giá trị chuẩn, H*";
                    break;
                case "Personal Dose":
                    text6.Text = "Giá trị chuẩn, Hp";
                    break;
                default:
                    text6.Text = "Giá trị chuẩn";
                    break;
            }


            run8.Append(runPropertiesNormal.CloneNode(true));
            run8.Append(text6);

            Run run9 = new Run();
            Break break4 = new Break();

            run9.Append(runPropertiesNormal.CloneNode(true));
            run9.Append(break4);

            Run run10 = new Run() { };
            Text text7 = new Text();
            text7.Text = "(Reference value)";

            run10.Append(runPropertiesItalic.CloneNode(true));
            run10.Append(text7);

            Run run11 = new Run();
            Break break5 = new Break();
            Text text8 = new Text { Text = "[µSv/h]" };

            run11.Append(runPropertiesNormal.CloneNode(true));
            run11.Append(break5);
            run11.Append(text8);

            paragraph3.Append(paragraphPropertiesNormal.CloneNode(true));
            paragraph3.Append(run8);
            paragraph3.Append(run9);
            paragraph3.Append(run10);
            paragraph3.Append(run11);

            tableCell3.Append(tableCellProperties3);
            tableCell3.Append(paragraph3);

            #endregion

            #region Machine avg reading
            TableCell tableCell4 = new TableCell();

            TableCellProperties tableCellProperties4 = new TableCellProperties();
            TableCellWidth tableCellWidth4 = new TableCellWidth() { Width = "2268", Type = TableWidthUnitValues.Dxa };

            tableCellProperties4.Append(tableCellWidth4);
            tableCellProperties4.Append(tableCellMargin8.CloneNode(true));
            tableCellProperties4.Append(tableCellVerticalAlignmentTop.CloneNode(true));

            Paragraph paragraph4 = new Paragraph() { };

            Run run12 = new Run();
            Text text9 = new Text();
            text9.Text = "Chỉ thị của thiết bị";

            run12.Append(runPropertiesNormal.CloneNode(true));
            run12.Append(text9);

            Run run13 = new Run();
            Break break6 = new Break();
            run13.Append(runPropertiesNormal.CloneNode(true));
            run13.Append(break6);

            Run run14 = new Run() { };
            Text text10 = new Text();
            text10.Text = "(Instrument indicator)";

            run14.Append(runPropertiesItalic.CloneNode(true));
            run14.Append(text10);

            Run run15 = new Run();
            Break break7 = new Break();
            Text text11 = new Text();
            text11.Text = "[µSv/h]";

            run15.Append(runPropertiesNormal.CloneNode(true));
            run15.Append(break7);
            run15.Append(text11);

            paragraph4.Append(paragraphPropertiesNormal.CloneNode(true));
            paragraph4.Append(run12);
            paragraph4.Append(run13);
            paragraph4.Append(run14);
            paragraph4.Append(run15);

            tableCell4.Append(tableCellProperties4);
            tableCell4.Append(paragraph4);

            #endregion


            #region CF + Uc

            TableCell tableCell5 = new TableCell();

            TableCellProperties tableCellProperties5 = new TableCellProperties();
            TableCellWidth tableCellWidth5 = new TableCellWidth() { Width = "1984", Type = TableWidthUnitValues.Dxa };

            tableCellProperties5.Append(tableCellWidth5);
            tableCellProperties5.Append(tableCellMargin8.CloneNode(true));
            tableCellProperties5.Append(tableCellVerticalAlignmentCenter.CloneNode(true));

            Paragraph paragraph5 = new Paragraph() { };

            Run run16 = new Run();
            Text text12 = new Text();
            text12.Text = "Hệ số chuẩn";

            run16.Append(runPropertiesNormal.CloneNode(true));
            run16.Append(text12);

            Run run17 = new Run();
            Break break8 = new Break();

            run17.Append(runPropertiesNormal.CloneNode(true));
            run17.Append(break8);

            Run run18 = new Run() { };
            Text text13 = new Text();
            text13.Text = "(Calibration factor)";

            run18.Append(runPropertiesItalic.CloneNode(true));
            run18.Append(text13);

            Run run19 = new Run();
            Break break9 = new Break();
            Text text14 = new Text();
            text14.Text = "(CF ± U";

            run19.Append(runPropertiesNormal.CloneNode(true));
            run19.Append(break9);
            run19.Append(text14);

            Run run20 = new Run() { };

            RunProperties runProperties20 = new RunProperties();
            runProperties20.Append(runFontsTimesNewRoman.CloneNode(true));
            runProperties20.Append(fontSize26.CloneNode(true));
            runProperties20.Append(fontSizeComplexScript26.CloneNode(true));
            runProperties20.Append(verticalTextAlignmentSubscrip.CloneNode(true));
            Text text15 = new Text();
            text15.Text = "c";

            run20.Append(runProperties20);
            run20.Append(text15);

            Run run21 = new Run();
            Text text16 = new Text();
            text16.Text = ")";

            run21.Append(runPropertiesNormal.CloneNode(true));
            run21.Append(text16);


            Run run22 = new Run();
            Break break10 = new Break();
            run22.Append(runPropertiesNormal.CloneNode(true));
            run22.Append(break10);

            Run run23 = new Run();
            Text text17 = new Text();
            text17.Text = certificate.CalibDatas[0].CF_unit;

            run23.Append(runPropertiesNormal.CloneNode(true));
            run23.Append(text17);
            


            paragraph5.Append(paragraphPropertiesNormal.CloneNode(true));
            paragraph5.Append(run16);
            paragraph5.Append(run17);
            paragraph5.Append(run18);
            paragraph5.Append(run19);
            paragraph5.Append(run20);
            paragraph5.Append(run21);
            paragraph5.Append(run22);
            paragraph5.Append(run23);

            tableCell5.Append(tableCellProperties5);
            tableCell5.Append(paragraph5);

            #endregion



            tableRow1.Append(tablePropertyExceptions1);
            tableRow1.Append(tableRowProperties1);
            tableRow1.Append(tableCell1);
            tableRow1.Append(tableCell2);
            tableRow1.Append(tableCell3);
            tableRow1.Append(tableCell4);
            tableRow1.Append(tableCell5);
            return tableRow1;

        }

        private void GenerateDataRow(Table table, Certificate certificate)
        {
            List<CalibData> CalibDatas = certificate.CalibDatas;

            for (int i = 0; i < CalibDatas.Count; i++)
            {
                TableRow tableRow1 = new TableRow() { };

                #region Table properties
                TablePropertyExceptions tablePropertyExceptions1 = new TablePropertyExceptions();

                TableCellMarginDefault tableCellMarginDefault1 = new TableCellMarginDefault();
                TopMargin topMargin1 = new TopMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };
                BottomMargin bottomMargin1 = new BottomMargin() { Width = "0", Type = TableWidthUnitValues.Dxa };

                tableCellMarginDefault1.Append(topMargin1);
                tableCellMarginDefault1.Append(bottomMargin1);

                tablePropertyExceptions1.Append(tableCellMarginDefault1);

                TableRowProperties tableRowProperties1 = new TableRowProperties();
                TableJustification tableJustification1 = new TableJustification() { Val = TableRowAlignmentValues.Center };

                tableRowProperties1.Append(tableJustification1);
                #endregion


                #region Common properties
                TableCellVerticalAlignment tableCellVerticalAlignmentCenter = new TableCellVerticalAlignment() { Val = TableVerticalAlignmentValues.Center };

                Justification justificationCenter = new Justification() { Val = JustificationValues.Center };

                RunFonts runFontsTimeNewRoman = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                FontSizeComplexScript fontSizeComplexScript26 = new FontSizeComplexScript() { Val = "26" };
                FontSize fontSize26 = new FontSize() { Val = "26" };

                ParagraphMarkRunProperties paragraphMarkRunPropertiesNormal = new ParagraphMarkRunProperties();
                paragraphMarkRunPropertiesNormal.Append(runFontsTimeNewRoman);
                paragraphMarkRunPropertiesNormal.Append(fontSize26);
                paragraphMarkRunPropertiesNormal.Append(fontSizeComplexScript26);

                ParagraphProperties paragraphPropertiesNormal = new ParagraphProperties();
                SpacingBetweenLines spacingBetweenLinesNormal = new SpacingBetweenLines() { Before = "40", After = "40" };
                paragraphPropertiesNormal.Append(spacingBetweenLinesNormal);
                paragraphPropertiesNormal.Append(justificationCenter);
                paragraphPropertiesNormal.Append(paragraphMarkRunPropertiesNormal);


                RunProperties runPropertiesNormal = new RunProperties();
                runPropertiesNormal.Append(runFontsTimeNewRoman.CloneNode(true));
                runPropertiesNormal.Append(fontSize26.CloneNode(true));
                runPropertiesNormal.Append(fontSizeComplexScript26.CloneNode(true));

                RunProperties runPropertiesItalic = new RunProperties();
                Italic italic = new Italic();
                runPropertiesItalic.Append(italic);
                runPropertiesItalic.Append(runFontsTimeNewRoman.CloneNode(true));
                runPropertiesItalic.Append(fontSizeComplexScript26.CloneNode(true));


                TableCellProperties tableCellProperties_Center_MergeRestart = new TableCellProperties();
                VerticalMerge verticalMergeRestart = new VerticalMerge() { Val = MergedCellValues.Restart };
                tableCellProperties_Center_MergeRestart.Append(verticalMergeRestart);
                tableCellProperties_Center_MergeRestart.Append(tableCellVerticalAlignmentCenter);

                TableCellProperties tableCellProperties_Center_Merge = new TableCellProperties();
                VerticalMerge verticalMerge = new VerticalMerge();
                tableCellProperties_Center_Merge.Append(verticalMerge);
                tableCellProperties_Center_Merge.Append(tableCellVerticalAlignmentCenter.CloneNode(true));


                TableCellProperties tableCellProperties_Center = new TableCellProperties();
                tableCellProperties_Center.Append(tableCellVerticalAlignmentCenter.CloneNode(true));

                #endregion

                #region RadQuantity
                TableCell tableCell1 = new TableCell();

                Paragraph paragraph1 = new Paragraph() { };

                Run run1 = new Run();
                Text text1 = new Text();

                if (CalibDatas[i].RadQuantity.NameVN == "Cs-137" | CalibDatas[i].RadQuantity.NameVN == "Co-60")
                {
                    text1.Text = "Gamma";
                }
                else
                {
                    text1.Text = "Tia X";
                }

                run1.Append(runPropertiesNormal);
                run1.Append(text1);

                Run run2 = new Run();
                Break break1 = new Break();
                run2.Append(runPropertiesNormal.CloneNode(true));
                run2.Append(break1);

                Run run3 = new Run() { };
                Text text2 = new Text();
                text2.Text = "(" + CalibDatas[i].RadQuantity.NameVN + ")";
                run3.Append(runPropertiesItalic);
                run3.Append(text2);

                paragraph1.Append(paragraphPropertiesNormal.CloneNode(true));
                paragraph1.Append(run1);
                paragraph1.Append(run2);
                paragraph1.Append(run3);

                if (i >= 1)
                {
                    if (CalibDatas[i].RadQuantity.NameVN == CalibDatas[i - 1].RadQuantity.NameVN)
                    {
                        tableCell1.Append(tableCellProperties_Center_Merge);
                    }
                    else
                    {
                        tableCell1.Append(tableCellProperties_Center_MergeRestart.CloneNode(true));
                    }
                }
                else
                {
                    tableCell1.Append(tableCellProperties_Center_MergeRestart);
                }

                tableCell1.Append(paragraph1);
                #endregion

                #region Energy
                TableCell tableCell2 = new TableCell();

                Paragraph paragraph2 = new Paragraph() { };

                Run run4 = new Run();
                Text text3 = new Text();

                CertificateHelper.DoubleToStringWithDecimalNumber(CalibDatas[i].AvgReading, 2).ToString();


                text3.Text = CertificateHelper.DoubleToStringWithDecimalNumber(CalibDatas[i].RadQuantity.Energy,1).ToString();
                run4.Append(runPropertiesNormal.CloneNode(true));
                run4.Append(text3);

                paragraph2.Append(paragraphPropertiesNormal.CloneNode(true));
                paragraph2.Append(run4);

                if (i >= 1)
                {
                    if (CalibDatas[i].RadQuantity.NameVN == CalibDatas[i - 1].RadQuantity.NameVN)
                    {
                        tableCell2.Append(tableCellProperties_Center_Merge.CloneNode(true));
                    }
                    else
                    {
                        tableCell2.Append(tableCellProperties_Center_MergeRestart.CloneNode(true));
                    }
                }
                else
                {
                    tableCell2.Append(tableCellProperties_Center_MergeRestart.CloneNode(true));
                }

                tableCell2.Append(paragraph2);
                #endregion

                #region ref.values
                TableCell tableCell3 = new TableCell();

                Paragraph paragraph3 = new Paragraph() { };

                Run run5 = new Run();
                Text text4 = new Text();
                text4.Text = CertificateHelper.DoubleToStringWithDecimalNumber(CalibDatas[i].RefValue,1).ToString();
                run5.Append(runPropertiesNormal.CloneNode(true));
                run5.Append(text4);

                paragraph3.Append(paragraphPropertiesNormal.CloneNode(true));
                paragraph3.Append(run5);

                tableCell3.Append(tableCellProperties_Center.CloneNode(true));
                tableCell3.Append(paragraph3);
                #endregion

                #region Machine avg reading
                TableCell tableCell4 = new TableCell();

                Paragraph paragraph4 = new Paragraph() { };

                Run run6 = new Run();
                Text text5 = new Text();
                text5.Text = CertificateHelper.DoubleToStringWithDecimalNumber(CalibDatas[i].AvgReading, 2).ToString();

                run6.Append(runPropertiesNormal.CloneNode(true));
                run6.Append(text5);

                paragraph4.Append(paragraphPropertiesNormal.CloneNode(true));
                paragraph4.Append(run6);

                tableCell4.Append(tableCellProperties_Center.CloneNode(true));
                tableCell4.Append(paragraph4);
                #endregion

                #region CF + Uc
                TableCell tableCell5 = new TableCell();

                Paragraph paragraph5 = new Paragraph() { };

                Run run7 = new Run();
                Text text6 = new Text();
                text6.Text = CertificateHelper.DoubleToStringWithDecimalNumber(CalibDatas[i].CF, 2).ToString()
                    + " ± "
                    + CertificateHelper.DoubleToStringWithDecimalNumber(CalibDatas[i].CF * 2.0 * CalibDatas[i].CF_reUnc, 2).ToString();

                run7.Append(runPropertiesNormal.CloneNode(true));
                run7.Append(text6);

                paragraph5.Append(paragraphPropertiesNormal.CloneNode(true));
                paragraph5.Append(run7);

                tableCell5.Append(tableCellProperties_Center.CloneNode(true));
                tableCell5.Append(paragraph5);
                #endregion

                #region Add all cells to table
                tableRow1.Append(tablePropertyExceptions1);
                tableRow1.Append(tableRowProperties1);
                tableRow1.Append(tableCell1);
                tableRow1.Append(tableCell2);
                tableRow1.Append(tableCell3);
                tableRow1.Append(tableCell4);
                tableRow1.Append(tableCell5);

                table.Append(tableRow1);
                #endregion
            }


        }

        private void GenerateComment(Table table, Certificate certificate)
        {
            #region Common properties
            Justification justificationCenter = new Justification() { Val = JustificationValues.Center };
            Justification justificationBoth = new Justification() { Val = JustificationValues.Both };

            RunFonts runFontsTimeNewRoman = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
            FontSizeComplexScript fontSizeComplexScript11 = new FontSizeComplexScript() { Val = "22" };
            FontSizeComplexScript fontSizeComplexScript12 = new FontSizeComplexScript() { Val = "24" };
            FontSize fontSize11 = new FontSize() { Val = "22" };
            FontSize fontSize12 = new FontSize() { Val = "24" };

            Italic italicFont = new Italic();
            ItalicComplexScript italicComplexScript = new ItalicComplexScript();

            ParagraphMarkRunProperties paragraphMarkRunPropertiesNormal = new ParagraphMarkRunProperties();
            paragraphMarkRunPropertiesNormal.Append(runFontsTimeNewRoman.CloneNode(true));
            paragraphMarkRunPropertiesNormal.Append(fontSize12.CloneNode(true));
            paragraphMarkRunPropertiesNormal.Append(fontSizeComplexScript12.CloneNode(true));

            ParagraphProperties paragraphPropertiesNormal = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLinesNormal = new SpacingBetweenLines() { Before = "40", After = "40" };
            paragraphPropertiesNormal.Append(spacingBetweenLinesNormal);
            paragraphPropertiesNormal.Append(justificationCenter);
            paragraphPropertiesNormal.Append(paragraphMarkRunPropertiesNormal);

            //
            ParagraphProperties paragraphPropertiesItalic = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLines18 = new SpacingBetweenLines() { Before = "120", After = "40" };

            ParagraphMarkRunProperties paragraphMarkRunPropertiesItalic = new ParagraphMarkRunProperties();
            paragraphMarkRunPropertiesItalic.Append(runFontsTimeNewRoman);
            paragraphMarkRunPropertiesItalic.Append(italicFont);
            paragraphMarkRunPropertiesItalic.Append(italicComplexScript);

            paragraphPropertiesItalic.Append(spacingBetweenLines18);
            paragraphPropertiesItalic.Append(justificationBoth);
            paragraphPropertiesItalic.Append(paragraphMarkRunPropertiesItalic);


            //
            ParagraphProperties paragraphPropertiesItalicSmall = new ParagraphProperties();
            SpacingBetweenLines spacingBetweenLinesSmall = new SpacingBetweenLines() { Before = "40", After = "40" };

            ParagraphMarkRunProperties paragraphMarkRunPropertiesItalicSmall = new ParagraphMarkRunProperties();
            paragraphMarkRunPropertiesItalicSmall.Append(runFontsTimeNewRoman.CloneNode(true));
            paragraphMarkRunPropertiesItalicSmall.Append(italicFont.CloneNode(true));
            paragraphMarkRunPropertiesItalicSmall.Append(italicComplexScript.CloneNode(true));

            paragraphPropertiesItalicSmall.Append(spacingBetweenLinesSmall);
            paragraphPropertiesItalicSmall.Append(justificationBoth.CloneNode(true));
            paragraphPropertiesItalicSmall.Append(paragraphMarkRunPropertiesItalicSmall);


            //
            RunProperties runPropertiesNormal = new RunProperties();
            runPropertiesNormal.Append(runFontsTimeNewRoman.CloneNode(true));
            runPropertiesNormal.Append(fontSize12.CloneNode(true));
            runPropertiesNormal.Append(fontSizeComplexScript12.CloneNode(true));

            RunProperties runPropertiesItalic11 = new RunProperties();
            runPropertiesItalic11.Append(italicFont.CloneNode(true));
            runPropertiesItalic11.Append(runFontsTimeNewRoman.CloneNode(true));
            runPropertiesItalic11.Append(fontSize11.CloneNode(true));
            runPropertiesItalic11.Append(fontSizeComplexScript11.CloneNode(true));


            RunProperties runPropertiesItalic12 = new RunProperties();
            runPropertiesItalic12.Append(italicFont.CloneNode(true));
            runPropertiesItalic12.Append(runFontsTimeNewRoman.CloneNode(true));
            runPropertiesItalic12.Append(fontSize12.CloneNode(true));
            runPropertiesItalic12.Append(fontSizeComplexScript12.CloneNode(true));

            RunProperties runPropertiesItalic12Subscript = new RunProperties();
            runPropertiesItalic12Subscript.Append(italicFont.CloneNode(true));
            runPropertiesItalic12Subscript.Append(runFontsTimeNewRoman.CloneNode(true));
            runPropertiesItalic12Subscript.Append(fontSize12.CloneNode(true));
            runPropertiesItalic12Subscript.Append(fontSizeComplexScript12.CloneNode(true));
            VerticalTextAlignment verticalTextAlignment2 = new VerticalTextAlignment() { Val = VerticalPositionValues.Subscript };
            runPropertiesItalic12Subscript.Append(verticalTextAlignment2);

            TableJustification tableJustificationCenter = new TableJustification() { Val = TableRowAlignmentValues.Center };
            TableJustification tableJustificationLeft = new TableJustification() { Val = TableRowAlignmentValues.Left };

            #endregion

            #region Table properties

            TableRow tableRowComment = new TableRow();
            TableRowProperties tableRowPropertiesComment = new TableRowProperties();
            TableRowHeight tableRowHeightComment = new TableRowHeight() { Val = (UInt32Value)372U };

            tableRowPropertiesComment.Append(tableRowHeightComment.CloneNode(true));
            tableRowPropertiesComment.Append(tableJustificationLeft.CloneNode(true));

            TableCell tableCell16 = new TableCell();

            TableCellProperties tableCellProperties16 = new TableCellProperties();
            TableCellWidth tableCellWidth16 = new TableCellWidth() { Width = "9777", Type = TableWidthUnitValues.Dxa };
            GridSpan gridSpan1 = new GridSpan() { Val = 5 };

            //tableCellProperties16.Append(tableCellWidth16);
            tableCellProperties16.Append(gridSpan1);

            tableCell16.Append(tableCellProperties16);

            #endregion

            #region Dose Quanity

            Paragraph paragraphDoseQuanity = new Paragraph();

            // Dosimetry quantity - VN
            Run run18 = new Run();
            Text text18 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text18.Text = "- " 
                + certificate.DoseQuantity.Notation 
                + " là suất " 
                + certificate.DoseQuantity.NameVN.ToLower()
                + ". "
                ;
            run18.Append(runPropertiesItalic12);
            run18.Append(text18);

            // Dosimetry quantity - EN
            Run run19 = new Run();
            Text text19 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text19.Text = " ( "
                + certificate.DoseQuantity.Notation
                + " is the "
                + certificate.DoseQuantity.NameEN.ToLower()
                + " rate)";
            run19.Append(runPropertiesItalic11);
            run19.Append(text19);

            paragraphDoseQuanity.Append(paragraphPropertiesItalic);
            paragraphDoseQuanity.Append(run18); // Dosimetry quantity - VN
            paragraphDoseQuanity.Append(run19); // Dosimetry quantity - EN
   
            
            tableCell16.Append(paragraphDoseQuanity);

            #endregion

            #region CF definition
            // CF definition - VN
            Paragraph paragraphCFDefinitionVN = new Paragraph();
            
            Run run28 = new Run();
            Text text28 = new Text();
            text28.Text = "- Hệ số chuẩn = Giá trị chuẩn / Chỉ thị của thiết bị";
            run28.Append(runPropertiesItalic12.CloneNode(true));
            run28.Append(text28);
            paragraphCFDefinitionVN.Append(paragraphPropertiesItalic.CloneNode(true));
            paragraphCFDefinitionVN.Append(run28); // definition - VN

            // CF definition - EN
            Paragraph paragraphCFDefinitionEN = new Paragraph();
            Run run29 = new Run();
            Text text29 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text29.Text = "   (Calibration factor = Reference value / Instrument indicator) \n";
            run29.Append(runPropertiesItalic11.CloneNode(true));
            run29.Append(text29);
            paragraphCFDefinitionEN.Append(paragraphPropertiesItalicSmall.CloneNode(true));
            paragraphCFDefinitionEN.Append(run29); 
            
            // Add to paragraph and table
            tableCell16.Append(paragraphCFDefinitionVN);
            tableCell16.Append(paragraphCFDefinitionEN);

            #endregion

            #region Uncertainty declaration
            Paragraph paragraphUncertainty = new Paragraph();

            // Uncertainty - VN
            Run run31 = new Run();
            Text text31 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text31.Text = "- U";
            run31.Append(runPropertiesItalic12.CloneNode(true));
            run31.Append(text31);

            Run run32 = new Run();
            Text text32 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text32.Text = "c";
            run32.Append(runPropertiesItalic12Subscript);
            run32.Append(text32);

            Run run33 = new Run();
            Text text33 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text33.Text = " là độ không đảm bảo đo mở rộng, được công bố với hệ số phủ k = 2 tương ứng mức tin cậy xấp xỉ 95%. ";
            run33.Append(runPropertiesItalic12.CloneNode(true));
            run33.Append(text33);

            // Uncertainty - EN
            Run run41 = new Run();
            Text text41 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text41.Text = " (U";
            run41.Append(runPropertiesItalic11.CloneNode(true));
            run41.Append(text41);

            Run run42 = new Run();
            Text text42 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text42.Text = "c";
            run42.Append(runPropertiesItalic12Subscript.CloneNode(true));
            run42.Append(text42);

            Run run43 = new Run();
            Text text43 = new Text() { Space = SpaceProcessingModeValues.Preserve };
            text43.Text = "  is the expanded uncertainty corresponding to a coverage factor, k=2 and a confidence level, P ~ 95% )";
            run43.Append(runPropertiesItalic11.CloneNode(true));
            run43.Append(text43);


            // Add text to paragraph
            paragraphUncertainty.Append(paragraphPropertiesItalic.CloneNode(true));
            paragraphUncertainty.Append(run31);
            paragraphUncertainty.Append(run32);
            paragraphUncertainty.Append(run33);
            paragraphUncertainty.Append(run41);
            paragraphUncertainty.Append(run42);
            paragraphUncertainty.Append(run43);

            // Add to paragraph and table
            tableCell16.Append(paragraphUncertainty);
            #endregion

            tableRowComment.Append(tableRowPropertiesComment);
            tableRowComment.Append(tableCell16);


            table.Append(tableRowComment);
        }

        #endregion


        #region Word.Interop
        //public int CreateTemporaryCertificatePdf_interop(Certificate certificate)
        //{
        //    string templateFilePath = CertificateHelper.GetTemplateFileFullPath(certificate);

        //    object tempoFilePath = CertificateHelper.GetTempoFilePdfFullPath(certificate);

        //    // Check template File
        //    bool isTemplateFileOK = CertificateHelper.isTemplateFileOK(templateFilePath);

        //    // Check tempo file
        //    bool isTempoFileOK = CertificateHelper.isTempoFileOK(tempoFilePath.ToString());


        //    if (isTemplateFileOK & isTempoFileOK)
        //    {
        //        object missing = Missing.Value;  //Create a missing variable for missing value 
        //        object readOnly = false;
        //        object isVisible = false;
        //        wordApp = new Word.Application();
        //        wordApp.ShowAnimation = false;  //Set animation status for word application  
        //        wordApp.Visible = false;     //Set status for word application is to be visible or not. 

        //        try
        //        {
        //            myWordDoc = null;    //Create a new document 
        //            myWordDoc = wordApp.Documents.Open(FileName: templateFilePath,
        //                                           ConfirmConversions: missing, ReadOnly: readOnly, AddToRecentFiles: missing,
        //                                           PasswordDocument: missing, Revert: missing, WritePasswordDocument: missing,
        //                                           Format: missing, Encoding: missing, Visible: isVisible,
        //                                           OpenAndRepair: missing, DocumentDirection: missing, NoEncodingDialog: missing,
        //                                           XMLTransform: missing);
        //            myWordDoc.Activate();

        //            CertificateHelper.CreateFormattedCertificate(certificate, wordApp, myWordDoc);

        //            myWordDoc.SaveAs(ref tempoFilePath, Word.WdSaveFormat.wdFormatPDF,
        //                    ref missing, ref missing, ref missing, ref missing, ref missing,
        //                    ref missing, ref missing, ref missing, ref missing, ref missing,
        //                    ref missing, ref missing, ref missing, ref missing);

        //            return 1;
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            myWordDoc.Close(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges, ref missing, ref missing);
        //            myWordDoc = null;
        //            wordApp.Quit(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges, ref missing, ref missing);
        //            wordApp = null;

        //            // Release all Interop objects.
        //            if (myWordDoc != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(myWordDoc);
        //            if (wordApp != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
        //            myWordDoc = null;
        //            wordApp = null;
        //            GC.Collect();
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}



        //public int ExportCertificateWordFile(Certificate certificate, object saveFileNamePath)
        //{
        //    string templateFilePath = CertificateHelper.GetTemplateFileFullPath(certificate);


        //    // Check template File
        //    bool isTemplateFileOK = CertificateHelper.isTemplateFileOK(templateFilePath);


        //    if (isTemplateFileOK)
        //    {
        //        object missing = Missing.Value;  //Create a missing variable for missing value 
        //        object readOnly = false;
        //        object isVisible = false;
        //        wordApp = new Word.Application();
        //        wordApp.ShowAnimation = false;  //Set animation status for word application  
        //        wordApp.Visible = false;     //Set status for word application is to be visible or not. 

        //        try
        //        {
        //            myWordDoc = null;    //Create a new document 
        //            myWordDoc = wordApp.Documents.Open(FileName: templateFilePath,
        //                                           ConfirmConversions: missing, ReadOnly: readOnly, AddToRecentFiles: missing,
        //                                           PasswordDocument: missing, Revert: missing, WritePasswordDocument: missing,
        //                                           Format: missing, Encoding: missing, Visible: isVisible,
        //                                           OpenAndRepair: missing, DocumentDirection: missing, NoEncodingDialog: missing,
        //                                           XMLTransform: missing);
        //            myWordDoc.Activate();

        //            CertificateHelper.CreateFormattedCertificate(certificate, wordApp, myWordDoc);

        //            myWordDoc.SaveAs(ref saveFileNamePath, ref missing,
        //                    ref missing, ref missing, ref missing, ref missing, ref missing,
        //                    ref missing, ref missing, ref missing, ref missing, ref missing,
        //                    ref missing, ref missing, ref missing, ref missing);

        //            return 1;
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //        finally
        //        {
        //            myWordDoc.Close(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges, ref missing, ref missing);
        //            myWordDoc = null;
        //            wordApp.Quit(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges, ref missing, ref missing);
        //            wordApp = null;

        //            // Release all Interop objects.
        //            if (myWordDoc != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(myWordDoc);
        //            if (wordApp != null)
        //                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
        //            myWordDoc = null;
        //            wordApp = null;
        //            GC.Collect();
        //        }
        //    }
        //    else
        //    {
        //        return 0;
        //    }
        //}



        #endregion

    }
}
