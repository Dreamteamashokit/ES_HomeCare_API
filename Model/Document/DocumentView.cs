using System.Collections.Generic;

namespace ES_HomeCare_API.Model.Document
{
    public class FolderView
    {
        public string FolderName { get; set; }
        public long FolderId { get; set; }

        public List<DocumentView> DocumentList { get; set; }
    }


    public class DocumentView
    {
        public long DocumentId { get; set; }
        public string Title { get; set; }
        public string SearchTag { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

    }

    public class DeleteItem
    {
        public int RequestType { get; set; }
        public int EmpId { get; set; }
        public int FolderId { get; set; }
        public int DocumentId { get; set; }
        public string FolderName { get; set; }
        public string FileName { get; set; }

    }



}
