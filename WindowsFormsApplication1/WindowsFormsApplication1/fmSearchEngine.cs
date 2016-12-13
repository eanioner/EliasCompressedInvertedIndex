using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Iveonik.Stemmers;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections;
using Coding;
using System.Diagnostics;

namespace InvertedIndex
{
    public partial class fmSearchEngine : Form
    {
        IDBConnection Connection;


        public fmSearchEngine()
        {
            InitializeComponent();

            FileIsChosen += OnFileIsChosen;
            SearchBegin += OnSearchBegin;
            SearchEnd += OnSearchEnd;


            if (Connection != null) Connection.Close();
            Connection = DBConnection.GetInstance(GetConnectionString());
        }

        private string GetConnectionString()
        {
            return tbConnectionString.Text;
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog.ShowDialog())
            {
                lbFileName.Text = openFileDialog.SafeFileName;

                FileIsChosen(this, new EventArgs());
            }
        }

        #region Events

        event EventHandler FileIsChosen;

        event EventHandler SearchBegin;
        event EventHandler SearchEnd;

        #endregion

        #region Event Handlers

        private void OnFileIsChosen(object sender, EventArgs e)
        {
            tbQuery.Enabled = true;
            btnSearch.Enabled = true;
        }


        Stopwatch sw = new Stopwatch();
        private void OnSearchBegin(object sender, EventArgs e)
        {
            lblDocFound.Text = "...";
            lblTimeElapsed.Text = "...";

            sw.Reset();
            sw.Start();
        }
        private void OnSearchEnd(object sender, EventArgs e)
        {
            lblDocFound.Text = dgv.RowCount.ToString();

            lblTimeElapsed.Text = sw.ElapsedMilliseconds.ToString() + " ms";
        }

        #endregion

        
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchBegin(this, new EventArgs());

            string QueryText = tbQuery.Text;
            QueryText = fmInvIndex.ClearText(QueryText);
            RussianStemmer RStemmer = new RussianStemmer();            
            List<string> queryWords = QueryText.Split(' ').ToList().Select(t => RStemmer.Stem(t)).Where(t => t.Length>2)
                .OrderBy(t => t).ToList();

            lblQueryForIIView.Text = queryWords.Aggregate((l, r) => l + " " + r);

            pnResponses.Controls.Clear();

            if (queryWords.Count == 0)
            {
                MessageBox.Show("По данному запросу ничего не найдено");
                return;
            }


            List<int> resultIDs = new List<int>();
            if (openFileDialog.SafeFileName.Split('.').Last() == "ii")
            {
                resultIDs = SearchInSimpleInvertedIndex(queryWords);
            }
            else
            {
                resultIDs = SearchInCompressedInvertedIndex(queryWords);
            }

            if (resultIDs == null || resultIDs.Count == 0)
            {
                MessageBox.Show("По данному запросу ничего не найдено");
                return;
            }

            string forQuery = "(" + resultIDs.Select(n => n.ToString()).Aggregate((l, r) => l + "," + r) + ")";


            string sql = "SELECT [u].[Text] [URL] FROM [dbo].[Urls] [u] inner join [dbo].[Pages] [p] on [u].[UrlId] = [p].[MainUrl_UrlId] WHERE [p].[Id] in " + 
                forQuery; 

            Connection.Open();
            DataTable dt = Connection.ExecuteQuery(sql);
            Connection.Close();

            dgv.DataSource = new DataView(dt);
            pnResponses.Controls.Add(dgv);

            SearchEnd(this, new EventArgs());
        }

        private List<int> SearchInSimpleInvertedIndex(List<string> queryWords)
        {

            Dictionary<string, string> term2terminfo = new Dictionary<string, string>();

            int currentIndexOnWordInQuery = 0;
            string queryWord = queryWords[currentIndexOnWordInQuery];


            using (StreamReader sr = new StreamReader(openFileDialog.FileName))
            {
                while (sr.Peek() >= 0)
                {
                    string readline = sr.ReadLine();
                    int indexOfColon = readline.IndexOf(':');
                    if (indexOfColon < 0) continue;

                    string word = readline.Substring(0, indexOfColon);


                    if (word == queryWord)
                    {
                        term2terminfo[word] = readline;
                        currentIndexOnWordInQuery++;

                        if (currentIndexOnWordInQuery < queryWords.Count)
                            queryWord = queryWords[currentIndexOnWordInQuery];
                        else
                            break;

                        continue;
                    }
                    else if (String.Compare(word, queryWord) > 0)
                    {
                        MessageBox.Show("Слово '" + queryWord + "' отсутствует в индексе. ");
                        return null;
                    }
                }
            }



            List<List<int>> lst = 
            term2terminfo.Values.ToList()
                .Select(s => s.Substring(s.IndexOf(':') + 1).Split().Where(w => w.Length > 0).Select(w => int.Parse(w)).ToList()).ToList();

            List<int> result_set = new List<int>();

            for (int i = 0; i < lst.Count; ++i)
            {
                List<int> inner_set = new List<int>();

                for (int j = 0; j < lst[i].Count; j += 2)
                {
                    inner_set.Add(lst[i][j]);
                }

                if (i == 0)
                {
                    result_set = inner_set;
                }
                else
                {
                    result_set = result_set.Intersect(inner_set).ToList();

                    if (result_set.Count == 0) return result_set;
                }
            }

            return result_set;
            
        }
        private List<int> SearchInCompressedInvertedIndex(List<string> queryWords) 
        {
            Dictionary<string, byte[]> term2terminfo = new Dictionary<string, byte[]>();

            int currentIndexOnWordInQuery = 0;
            string queryWord = queryWords[currentIndexOnWordInQuery];

            #region ReadFromFile
            using (FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read)) {

                
                List<byte> Term = new List<byte>();
                string term = "";
                List<byte> Num = new List<byte>();
                int num = 0;
                List<byte> Code = new List<byte>();

                int numBytesToRead = (int)fs.Length;
                int numBytesRead = 0;

                int nColon = 0;
                while (numBytesToRead > 0)
                {

                    byte b = (byte)fs.ReadByte();
                    numBytesToRead--;
                    char ch = Convert.ToChar(b);

                    if (nColon == 0) {
                        if (ch == ':') {

                            nColon = 1;
                            term = Encoding.UTF8.GetString(Term.ToArray());
                            Term.Clear();

                        } else {
                            Term.Add(b);
                        }
                    }
                    else if (nColon == 1) {
                        if (ch == ':')
                        {
                            nColon = 2;
                            string snum = Encoding.UTF8.GetString(Num.ToArray());
                            if (!int.TryParse(snum, out num)) {
                                MessageBox.Show("Индекс поврежден");
                                return null; 
                            }
                            Num.Clear();
                        } else {
                            Num.Add(b);
                        }
                    } else {
                        Code.Add(b);

                        if (Code.Count == num)
                        {
                            nColon = 0;


                            if (term == queryWord)
                            {
                                term2terminfo[term] = Code.ToArray();
                                Code.Clear();
                                currentIndexOnWordInQuery++;

                                if (currentIndexOnWordInQuery < queryWords.Count)
                                    queryWord = queryWords[currentIndexOnWordInQuery];
                                else
                                    break;

                                
                            }
                            else if (String.Compare(term, queryWord) > 0)
                            {
                                MessageBox.Show("Слово '" + queryWord + "' отсутствует в индексе. ");
                                return null;
                            }

                            Code.Clear();
                        }

                    }
                }


            }
            #endregion




            //List<string> lst = new List<string>(); // term2terminfo.Values.ToList()
                //.Select(s => s.Substring(s.IndexOf(':') + 2)).ToList();

            List<byte[]> lst = term2terminfo.Values.ToList();
            
            List<int> result_set = new List<int>();

            GammaEliasCoding.BufferDecoder decoder;
            for (int i = 0; i < lst.Count; ++i) 
            {
                byte[] b = lst[i];
                decoder = new GammaEliasCoding.BufferDecoder(b);

                List<int> inner_set = new List<int>();
                foreach (int value in decoder.GetValue())
                {
                    inner_set.Add(value);
                }
                inner_set = BackConversion(inner_set.Take(inner_set.Count / 2).ToList());


                if (i == 0)
                {
                    result_set = inner_set;
                }
                else
                {
                    result_set = result_set.Intersect(inner_set).ToList();

                    if (result_set.Count == 0) return result_set;
                }
            }

            return result_set;
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
}
