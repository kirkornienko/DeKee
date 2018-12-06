using Syncfusion.XlsIO;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PlainElastic.Net.WebAppMVC.Utils
{
    public class ExcelAction : FileStreamResult
    {
        //public ExcelAction(IEnumerable<object> _items, string name)
        //{

        //}
        //public ExcelAction(Stream fileStream, string contentType) : base(fileStream, contentType)
        //{
        //}

        //public IEnumerable<object> Items { get; set; }


        //Stream GetStream(IEnumerable<object> _items)
        //{

        //}
        ////public override void ExecuteResult(ControllerContext context)
        ////{
        ////    using (ExcelEngine excelEngine = new ExcelEngine())
        ////    {
        ////        //Set the default application version as Excel 2016
        ////        excelEngine.Excel.DefaultVersion = ExcelVersion.Excel2016;

        ////        //Create a workbook with a worksheet
        ////        IWorkbook workbook = excelEngine.Excel.Workbooks.Create(1);

        ////        //Access first worksheet from the workbook instance
        ////        IWorksheet worksheet = workbook.Worksheets[0];

        ////        //Insert sample text into cell “A1”
        ////        worksheet.Range["A1"].Text = "Hello World";

        ////        //Save the workbook to disk in xlsx format
        ////        using (var stream = new System.IO.MemoryStream())
        ////        {
        ////            workbook.SaveAs(stream, ExcelSaveType.SaveAsXLS);
        ////            context.HttpContext.Response.BinaryWrite(stream.GetBuffer());
        ////        }
        ////    }
        ////}
        public ExcelAction(Stream fileStream, string contentType) : base(fileStream, contentType)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void WriteFile(HttpResponseBase response)
        {
            base.WriteFile(response);
        }
    }
}