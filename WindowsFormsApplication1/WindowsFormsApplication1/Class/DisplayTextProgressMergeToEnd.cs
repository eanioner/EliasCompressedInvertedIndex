using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvertedIndex
{
    class DisplayTextProgressMergeToBegin : IDisplayTextProgress
    {
        string Info = "";
        string LastInfo = "";

        public DisplayTextProgressMergeToBegin()
        {

        }

        public void AddInformation(string info)
        {
            this.Info += this.LastInfo;
            this.LastInfo = info;

            if (IsChanged != null)
            {
                IsChanged(this, new EventArgs());
            }
        }

        public void UpdateLastInfo(string info)
        {
            this.LastInfo = info;
            if (IsChanged != null)
            {
                IsChanged(this, new EventArgs());
            }
        }

        public string GetInformation()
        {
            return Info + LastInfo;
        }

        public void Clear()
        {
            this.Info = "";
            this.LastInfo = "";
        }

        public event EventHandler IsChanged;
        
    }
}
