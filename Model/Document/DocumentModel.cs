using System;

namespace ES_HomeCare_API.Model.Document
{
    //public class DocumentModel
    //{
    //}

    public class FolderModel
    {
        public long FolderId { get; set; }
        public string FolderName { get; set; }
        public int UserId { get; set; }
        public short IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

    }











}
