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

namespace InvertedIndex
{
    public partial class fmSearchEngine : Form
    {
        IDBConnection Connection;


        public fmSearchEngine()
        {
            InitializeComponent();

            FileIsChosen += OnFileIsChosen;


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

        #endregion

        #region Event Handlers

        private void OnFileIsChosen(object sender, EventArgs e)
        {
            tbQuery.Enabled = true;
            btnSearch.Enabled = true;
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
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

            Dictionary<string, string> word2index = new Dictionary<string, string>();

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
                        word2index[word] = readline;
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
                        return;
                    } 
                }
            }


            List<int> resultIDs = new List<int>();
            if (openFileDialog.SafeFileName.Split('.').Last() == "ii")
            {
                resultIDs = SearchInSimpleInvertedIndex(word2index);
            }
            else
            {
                resultIDs = SearchInCompressedInvertedIndex(word2index);
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

        }

        private List<int> SearchInSimpleInvertedIndex(Dictionary<string, string> term2terminfo)
        {

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
        private List<int> SearchInCompressedInvertedIndex(Dictionary<string, string> term2terminfo) 
        {
            
            List<string> lst = term2terminfo.Values.ToList()
                .Select(s => s.Substring(s.IndexOf(':') + 1)).ToList();

            
            List<int> result_set = new List<int>();

            GammaEliasCoding.BufferDecoder decoder;
            for (int i = 0; i < lst.Count; ++i) 
            {
                string s = lst[i];
                byte[] b = s.Select(ch => Convert.ToByte(ch)).ToArray();
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
