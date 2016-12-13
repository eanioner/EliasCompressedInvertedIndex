using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndex
{
    interface IDocument
    {
        int Id { get; }
        string Content { get; set; }
    }
}
