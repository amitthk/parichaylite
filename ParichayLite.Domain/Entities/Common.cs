using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParichayLite.Domain.Entities
{
    public enum LogType { Debug, Error, Fatal, Info, Warn }
    public enum InviteTypes { Join = 0, Friend = 1, Group = 2 }
    public enum RequestTypes { Join = 0, Friend = 1, Group = 2 }
    public enum UploadedMediaOperationType { Resize, Crop, None }
    public enum UploadedMediaLocation { Database = 1, FileFolder = 2 }

    public struct Constants
    {
        public const string UploadFileLocation = "/Uploads/";
    }
}
