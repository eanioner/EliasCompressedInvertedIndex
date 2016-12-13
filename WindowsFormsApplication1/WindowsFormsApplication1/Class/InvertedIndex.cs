using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Coding;
using System.IO;

namespace InvertedIndex
{
    class InvertedIndex : IInvertedIndex, IEnumerable
    {
        public DefaultSortedDict<Term, ITermInfo> Index { get; private set; }

        public InvertedIndex()
        {
            Index = new DefaultSortedDict<Term, ITermInfo>(t => new TermInfo_Doc_tf(t));
        }

        public IEnumerator GetEnumerator()
        {
            return Index.Keys.GetEnumerator();
        }
        public ITermInfo this[Term term]
        {
            get
            {
                return this.Index[term];
            }
        }

        public void Add(string Term, int DocID, int PosInDoc)
        {
            Term term = new Term(Term);

            Index[term].AddInfo(DocID, PosInDoc);
        }

        public override string ToString()
        {
            return Index.Keys.Aggregate("", (l, r) => l + "\n" + r.ToString() + ":" + Index[r].ToString());
            //return Index.Keys.Select(k => k.ToString() + ":" + Index[k].ToString()).Aggregate((l, r) => l + "\n" + r);
        }

        //public void SaveToFile(string FileName, IDisplayTextProgress displayProgress)
        //{
        //    File.WriteAllText(FileName, "");

        //    int NumberOfWords = Index.Count;
        //    int counter = 0;

        //    displayProgress.AddInformation(String.Format("Прогресс: {0}/{1}\r\n", counter, NumberOfWords));

        //    foreach (Term term in Index.Keys)
        //    {
        //        counter++;
        //        displayProgress.UpdateLastInfo(String.Format("Прогресс: {0}/{1}\r\n", counter, NumberOfWords));


        //        File.AppendAllText(FileName, term + ":");

        //        Index[term].SaveToFile(FileName);

        //        File.AppendAllText(FileName, "\n");
        //    }
        //}

        public string ToStringWithCompression()
        {
            return Index.Keys.Select(k => k.ToString() + ":" + Index[k].ToStringWithCompress()).Aggregate((l, r) => l + "\n" + r);
        }


        public void Clear()
        {
            Index.Clear();
        }
        //public void Load(string TextReprOfIndex)
        //{
        //    this.Clear();


        //    TextReprOfIndex.Split('\n')
        //        .ToList()
        //        .ForEach(s =>
        //        {
        //            int index = s.IndexOf(':');

        //            string TermText = s.Substring(0, index);
        //            string ForTermInfo = s.Substring(index + 1);

        //            Regex.Split(ForTermInfo.Substring(1, ForTermInfo.Length - 2), "\\),\\(")
        //                .ToList()
        //                .ForEach(s1 =>
        //                {
        //                    //s1 = s1.Substring(1, s1.Length - 2);
        //                    int s1Index = s1.IndexOf(':');
        //                    int docid = int.Parse(s1.Substring(0, s1Index));
        //                    string positions = s1.Substring(s1Index + 1);

        //                    positions.Substring(1, positions.Length - 2)
        //                        .Split(',')
        //                        .Select(p => int.Parse(p))
        //                        .ToList()
        //                        .ForEach(pos => this.Add(TermText, docid, pos));
        //                    return;
        //                });
        //            return;
        //        }
        //          );

        //}
    }


    #region Interface

    interface ITermInfo
    {
        void AddInfo(int DocID, int PosInDoc);
        string ToString();

        string ToStringWithCompress();

        //void SaveToFile(string FileName);
    }

    #endregion

    #region Class
    internal class Term : IComparable
    {
        public string Text { get; private set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Term t = obj as Term;

            if (t != null)
            {
                return this.Text.CompareTo(t.Text);
            }
            else
            {
                throw new ArgumentException("Object is not a Term");
            }
        }

        public Term(string Text)
        {
            this.Text = Text;
        }

        public override string ToString()
        {
            return Text;
        }
    }


    internal class TermInfo
    {
        Term term;

        protected TermInfo(Term term)
        {
            this.term = term;
        }
    }
    internal class TermInfo_Doc_Poses : TermInfo, ITermInfo
    {

        DefaultSortedDict<int, PositionSortedCollection> TermIndex;

        public TermInfo_Doc_Poses(Term term)
            : base(term)
        {
            TermIndex = new DefaultSortedDict<int, PositionSortedCollection>((k) => new PositionSortedCollection());
        }

        public void AddInfo(int DocID, int PosInDoc)
        {
            TermIndex[DocID].Add(PosInDoc);
        }

        public override string ToString()
        {
            
            return TermIndex.Keys.Select(k => "(" + k.ToString() + ":" + TermIndex[k].ToString() + ")").Aggregate((l, r) => l + "," + r);
        }

        public string ToStringWithCompress()
        {
            return this.ToString();
        }

        //public void SaveToFile(string FileName)
        //{

        //}

        internal class PositionSortedCollection : SortedSet<int>
        {
            public override string ToString()
            {
                return "[" + this.Select(t => t.ToString()).Aggregate((a, b) => a + "," + b) + "]";
            }
        }

       

    }

    internal class TermInfo_Doc_tf : TermInfo, ITermInfo
    {
        DefaultSortedDict<int, int> TermIndex;

        public TermInfo_Doc_tf(Term term)
            : base(term)
        {
            TermIndex = new DefaultSortedDict<int, int>((k) => 0);
        }
        public void AddInfo(int DocID, int PosInDoc)
        {
            TermIndex[DocID]++;
        }
        public override string ToString()
        {
            return TermIndex.Keys.Aggregate("", (l, r) => l + " " + r + " " + TermIndex[r].ToString());


            //return TermIndex.Keys.Select(k => "(" + k.ToString() + "," + TermIndex[k].ToString() + ")").Aggregate((l, r) => l + "," + r);
        }

        //public void SaveToFile(string FileName)
        //{
        //    int counter = 0;
        //    foreach (int key in TermIndex.Keys)
        //    {
        //        string comma = (counter == TermIndex.Count - 1) ? "" : ",";
        //        counter++;
        //        File.AppendAllText(FileName, String.Format("({0},{1}){2}", key, TermIndex[key], comma));
        //    }
        //}

        public string ToStringWithCompress()
        {
            GammaEliasCoding.BufferEncoder encoder = new GammaEliasCoding.BufferEncoder();

            ForwardConversion(TermIndex.Keys.ToList()).ForEach(d => encoder.Append(d));
            TermIndex.Values.ToList().ForEach(d => encoder.Append(d));

            string s = encoder.GetByteArray().ToList().Select(b => Convert.ToChar(b).ToString()).Aggregate((l, r) => l + r);

            return s;
        }

        public static List<int> ForwardConversion(List<int> source)
        {
            List<int> dest = new List<int>();

            dest.Add(source[0]);
            for (int i = 1; i < source.Count; ++i)
                dest.Add(source[i] - source[i - 1]);
            return dest;
        }
        public static List<int> BackConversion(List<int> source)
        {
            List<int> dest = new List<int>();

            dest.Add(source[0]);
            for (int i = 1; i < source.Count; ++i)
                dest.Add(dest[i - 1] + source[i]);

            return dest;
        }
    }
    internal class DefaultSortedDict<T1, T2> : SortedDictionary<T1, T2>
    {
        Func<T1, T2> GetDefaultValue;

        public DefaultSortedDict(Func<T1, T2> GetDefaultValue)
        {
            this.GetDefaultValue = GetDefaultValue;
        }

        public T2 this[T1 key]
        {
            get
            {
                if (!base.Keys.Contains(key))
                    base[key] = GetDefaultValue(key);

                return base[key];
            }

            set
            {
                base[key] = value;
            }
        }
    }

    #endregion
}


