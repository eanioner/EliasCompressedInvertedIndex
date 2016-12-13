using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace InvertedIndex
{
    interface IDocumentCorpus
    {
        
        void Adapter(DataTable dt);

        IEnumerator<IDocument> GetEnumerator();

        int Count { get; }
    }
}
