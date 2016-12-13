using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndex
{
    class Document : IDocument
    {
        public int Id { get; private set; }
        public string Content { get; set; }

        public Document(int Id, string Content)
        {
            this.Id = Id;
            this.Content = Content;
        }
    }
}
