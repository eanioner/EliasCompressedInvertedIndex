using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndex
{
    interface IDisplayTextProgress
    {
        void Clear();
        void AddInformation(string info);
        void UpdateLastInfo(string info);
        string GetInformation();

        event EventHandler IsChanged;
    }
}
