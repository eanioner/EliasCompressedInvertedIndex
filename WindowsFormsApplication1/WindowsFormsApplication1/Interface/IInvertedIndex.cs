using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndex
{
    interface IInvertedIndex
    {
        void Add(string Term, int DocID, int PosInDoc);

        string ToString();
        string ToStringWithCompression();
        void Clear();

        IEnumerator GetEnumerator();

        //void SaveToFile(string FileName, IDisplayTextProgress displayProgress);
    }
}
