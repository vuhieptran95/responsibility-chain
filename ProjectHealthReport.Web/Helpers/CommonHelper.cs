﻿using System;
 using System.IO;

 namespace ProjectHealthReport.Web.Helpers
{
    public static class CommonHelper
    {
        public static string PDFPath =  ".\\PDF_Reports";
        public static string PDFPathDebug =  ".\\bin\\Debug\\netcoreapp3.1\\PDF_Reports";

        public static string CreateTempReportsFolder()
        {
            var folder = DateTime.Now.Ticks;
            var path = Path.Join(PDFPath,"PHR_" + folder);
#if DEBUG
            path = Path.Join(PDFPathDebug,"PHR_" + folder);
#endif
            Directory.CreateDirectory(path);

            return path;
        }
    }
}