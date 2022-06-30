using System;
using System.Collections.Generic;

namespace ES_HomeCare_API.Model.Document
{
    public class FolderView
    {
        public string FolderName { get; set; }
        public long FolderId { get; set; }

        public List<DocumentView> DocumentList { get; set; } = new List<DocumentView>();
    }

    public class NewFolderView: DocumentView
    {
        public string FolderName { get; set; }
        public long FolderId { get; set; }
        public int ParentId { get; set; }

        public List<FolderView> SubFolderList { get; set; } = new List<FolderView>();
        public List<DocumentView> DocumentList { get; set; } = new List<DocumentView>();

    }

    public class DocumentView
    {
        public long DocumentId { get; set; }
        public string Title { get; set; }
        public string SearchTag { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class DeleteItem
    {
        public int RequestType { get; set; }
        public int UserId { get; set; }
        public int FolderId { get; set; }
        public int DocumentId { get; set; }
        public string FolderName { get; set; }
        public string FileName { get; set; }

    }




}
