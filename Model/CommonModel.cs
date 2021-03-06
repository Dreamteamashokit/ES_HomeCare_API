using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ES_HomeCare_API.Model
{
    public class ItemList
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
    }


    public class FolderData
    {
        public int EmpId { get; set; }
        public string FolderName { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UploadFileFolder
    {
        public string Title { get; set; }
        public int FolderId { get; set; }
        public string Search { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public int UserId { get; set; }
        public int CreatedBy { get; set; }
    }

    public class UploadFileRecord
    {
        public string FolderName { get; set; }
        public int FolderId { get; set; }
        public string FileName { get; set; }
    }

    public class SelectList
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }

    public class ItemObj
    {
        public int ItemId { get; set; }
        public short MasterType { get; set; }
        public string ItemName { get; set; }
    }

    public class Email
    {
        public string Message { get; set; }
    }
}
