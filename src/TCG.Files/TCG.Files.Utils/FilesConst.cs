using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

using TCG.Data;

namespace TCG.Files.Utils
{
    public class FilesConst
    {
        public static int[] FilesDbLinks ={ DBLinkNums.Files_1,
            DBLinkNums.Files_2,
            DBLinkNums.Files_3,
            DBLinkNums.Files_4,
            DBLinkNums.Files_5,
            DBLinkNums.Files_6,
            DBLinkNums.Files_7,
            DBLinkNums.Files_8,
            DBLinkNums.Files_9,
            DBLinkNums.Files_10,
        };

        public static string CACHING_ALL_FILECLASS = "allfilesclass";

        public static string FilePath { get { return _filepath; } }
        private static string _filepath = ConfigurationManager.ConnectionStrings["filePatch"].ToString();

        public static string alowFileType = "'gif','bmp','jpg','rar','zip','png'";
        public static int fileSize = 500*1024;

    }
}