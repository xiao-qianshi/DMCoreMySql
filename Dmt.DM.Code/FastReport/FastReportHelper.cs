using FastReport.Export.Html;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Dmt.DM.Code
{
    public static class FastReportHelper
    {    
        public static byte[] GetReportBytes(string reportFilePath, string dataSetName, DataSet data ,string exportFormat)
        {
            FastReport.Report report = new FastReport.Report();
            byte[] result = new byte[] { };
            if( FileHelper.IsExistFile(reportFilePath))
            {
                try
                {
                    report.Load(reportFilePath); // Download the report
                    report.RegisterData(data, dataSetName); // Register data in the report
                }
                catch(Exception ex)
                {
                    //var log = LogFactory.GetLogger("FastReportHelper");
                    //log.Error(ex.Message);
                    return result; 
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    if (exportFormat.Equals("pdf", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport();
                        //report.Export(pdf, stream);
                    }
                    else if (exportFormat.Equals("Jpeg", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg
                        };
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Bmp", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp
                        };
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Gif", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif
                        };
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Png", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Png
                        };
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Tiff", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff
                        };
                        report.Export(image, stream);
                    }
                    result = stream.ToArray();
                   
                }
            }
            else
            {
                //var log = LogFactory.GetLogger("FastReportHelper");
                //log.Error(reportFilePath + "，目录不存在！");
            }
            return result;
        }

        public static Stream GetReportStream(string reportFilePath, string dataSetName, DataSet data, string exportFormat)
        {
            FastReport.Report report = new FastReport.Report();
            MemoryStream result = new MemoryStream();
            if (FileHelper.IsExistFile(reportFilePath))
            {
                try
                {
                    report.Load(reportFilePath); // Download the report
                    report.RegisterData(data, dataSetName); // Register data in the report
                }
                catch
                {
                    //var log = LogFactory.GetLogger("FastReportHelper");
                    return result;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    if (exportFormat.Equals("pdf", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport();
                        //report.Export(pdf, stream);
                    }
                    else if (exportFormat.Equals("Jpeg", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport();
                        image.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Bmp", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport();
                        image.ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp;
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Gif", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport();
                        image.ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif;
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Png", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport();
                        image.ImageFormat = FastReport.Export.Image.ImageExportFormat.Png;
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Tiff", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff
                        };
                        report.Export(image, stream);
                    }
                    result = stream;
                }
            }
            else
            {
                //var log = LogFactory.GetLogger("FastReportHelper");
                //log.Error(reportFilePath + "，目录不存在！");
            }
            return result;
        }

        public static string GetReportString(string reportFilePath, string dataSetName, DataSet data, string exportFormat)
        {
            FastReport.Report report = new FastReport.Report();
            string result = string.Empty;
            if (FileHelper.IsExistFile(reportFilePath))
            {
                try
                {
                    report.Load(reportFilePath); // Download the report
                    report.RegisterData(data, dataSetName); // Register data in the report
                }
                catch
                {
                    //var log = LogFactory.GetLogger("FastReportHelper");
                    //log.Error(ex.Message);
                    return result;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    if (exportFormat.Equals("pdf", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport {
                        //    Producer = "",
                        //    AllowPrint=true,
                        //    AllowModify=true,
                        //    PrintScaling=true,
                          
                        //    RichTextQuality=100,
                        //    Compressed=false
                        //};
                        //report.Export(pdf, stream);

                        //FileHelper.CreateFile("\\log\\PDF" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg", stream.ToArray());

                    }
                    else if (exportFormat.Equals("Jpeg", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg,
                            SeparateFiles = false,
                            JpegQuality=100
                        };
                        report.Prepare();
                        report.Export(image, stream);
                        FileHelper.CreateFile("\\log\\JPG" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg", stream.ToArray());

                        //foreach (var item in image.GeneratedFiles)
                        //{
                        //    var temp = item;
                        //    var log = LogFactory.GetLogger("FastReportHelper");
                        //    log.Info(temp);
                        //}
                        //return report.SaveToStringBase64();
                    }
                    else if (exportFormat.Equals("Bmp", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp
                        };
                        report.Prepare();
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Gif", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif
                        };
                        report.Prepare();
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Png", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Png,
                            SeparateFiles = false
                    };
                        report.Prepare();
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Tiff", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff
                        };
                        report.Prepare();
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("html", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Html.HTMLExport image = new FastReport.Export.Html.HTMLExport
                        {
                            EnableMargins=false,
                            Pictures =true,
                            EmbedPictures =true,
                            ImageFormat= ImageFormat.Png
                        };
                        report.Prepare();
                        report.Export(image, stream);
                        return System.Text.Encoding.UTF8.GetString(stream.ToArray());
                    }


                    result =Convert.ToBase64String(stream.ToArray());
                }
            }
            else
            {
                //var log = LogFactory.GetLogger("FastReportHelper");
                //log.Error(reportFilePath + "，目录不存在！");
            }
            return result;
        }
        public static string GetReportString(string reportFilePath, string dataSetName, IEnumerable<object> data, string exportFormat)
        {
            FastReport.Report report = new FastReport.Report();
            string result = string.Empty;
            if (FileHelper.IsExistFile(reportFilePath))
            {
                try
                {
                    report.Load(reportFilePath); // Download the report
                    report.RegisterData(data, dataSetName); // Register data in the report
                }
                catch
                {
                    //var log = LogFactory.GetLogger("FastReportHelper");
                    //log.Error(ex.Message);
                    return result;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    if (exportFormat.Equals("pdf", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //FastReport.Export.Pdf.PDFExport pdf = new FastReport.Export.Pdf.PDFExport {
                        //    Producer = "",
                        //    AllowPrint=true,
                        //    AllowModify=true,
                        //    PrintScaling=true,

                        //    RichTextQuality=100,
                        //    Compressed=false
                        //};
                        //report.Export(pdf, stream);

                        //FileHelper.CreateFile("\\log\\PDF" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg", stream.ToArray());

                    }
                    else if (exportFormat.Equals("Jpeg", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg,
                            SeparateFiles = false,
                            JpegQuality = 100
                        };
                        report.Prepare();
                        report.Export(image, stream);
                        FileHelper.CreateFile("\\log\\JPG" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg", stream.ToArray());

                        //foreach (var item in image.GeneratedFiles)
                        //{
                        //    var temp = item;
                        //    var log = LogFactory.GetLogger("FastReportHelper");
                        //    log.Info(temp);
                        //}
                        //return report.SaveToStringBase64();
                    }
                    else if (exportFormat.Equals("Bmp", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Bmp
                        };
                        report.Prepare();
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Gif", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Gif
                        };
                        report.Prepare();
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Png", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Png,
                            SeparateFiles = false
                        };
                        report.Prepare();
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("Tiff", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Image.ImageExport image = new FastReport.Export.Image.ImageExport
                        {
                            ImageFormat = FastReport.Export.Image.ImageExportFormat.Tiff
                        };
                        report.Prepare();
                        report.Export(image, stream);
                    }
                    else if (exportFormat.Equals("html", StringComparison.InvariantCultureIgnoreCase))
                    {
                        FastReport.Export.Html.HTMLExport image = new FastReport.Export.Html.HTMLExport
                        {
                            EnableMargins = false,
                            Pictures = true,
                            EmbedPictures = true,
                            ImageFormat = ImageFormat.Png
                        };
                        report.Prepare();
                        report.Export(image, stream);
                        return System.Text.Encoding.UTF8.GetString(stream.ToArray());
                    }


                    result = Convert.ToBase64String(stream.ToArray());
                }
            }
            else
            {
                //var log = LogFactory.GetLogger("FastReportHelper");
                //log.Error(reportFilePath + "，目录不存在！");
            }
            return result;
        }
    }
}
