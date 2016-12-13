using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InvertedIndex
{
    class DocumentCorpus : IDocumentCorpus
    {
        List<IDocument> docList;

        public DocumentCorpus()
        {
            docList = new List<IDocument>();
        }

        public void Adapter(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                int DocID;
                if (!int.TryParse(row[0].ToString(), out DocID)) continue;

                string Content = row[1].ToString();

                docList.Add(new Document(DocID, Content));
            }
        }

        public IEnumerator<IDocument> GetEnumerator()
        {
            return docList.GetEnumerator();
        }

        public int Count
        {
            get
            {
                return docList.Count;
            }
        }

    }
}
