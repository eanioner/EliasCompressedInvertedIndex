using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Iveonik.Stemmers;
using System.IO;
using Coding;

namespace InvertedIndex
{
    public partial class fmInvIndex : Form
    {
        IDBConnection Connection;
        IDisplayTextProgress TextProgress;
        IDocumentCorpus DocumentCorpus;
        
        IInvertedIndex InvertedIndex;
        event EventHandler InvertedIndexBuildBegin;
        event EventHandler InvertedIndexHasBeenBuilt;

        event EventHandler SavingBegin;
        event EventHandler SavingEnd;


        public fmInvIndex()
        {
            InitializeComponent();

            DocumentCorpus = new DocumentCorpus();
            
            InvertedIndex = new InvertedIndex();
            InvertedIndexBuildBegin += OnInvertedIndexBuildBegin;
            InvertedIndexHasBeenBuilt += OnInvertedIndexHasBeenBuilt;

            TextProgress = new DisplayTextProgressMergeToBegin();
            TextProgress.IsChanged += ProgressInfoIsChanged;

            SavingBegin += OnSavingBegin;
            SavingEnd += OnSavingEnd;

            InitBackGrounWorkers();
        }

        private void btnBuildIndex_Click(object sender, EventArgs e)
        {
            try
            {
                GetDataAndBuildInvertedIndex();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetDataAndBuildInvertedIndex()
        {
            InvertedIndexBuildBegin(this, new EventArgs());

            TextProgress.Clear();
            TextProgress.AddInformation("Загрузка данных из БД...\r\n");
            LoadDataFromDB();
            TextProgress.AddInformation("Загрузка данных завершена.\r\n");

            TextProgress.AddInformation("Предобработка данных...\r\n");
            PreprocessData();
            TextProgress.AddInformation("Предобработка данных завершена\r\n");

            TextProgress.AddInformation("Построение индекса...\r\n");
            
            RussianStemmer RStemmer = new RussianStemmer();

            int SizeOfCorpus = DocumentCorpus.Count;
            int counter = 0;

            TextProgress.AddInformation(String.Format("Прогресс: {0}/{1}\r\n", counter, SizeOfCorpus));
            foreach (IDocument doc in DocumentCorpus)
            {
                counter++;
                TextProgress.UpdateLastInfo(String.Format("Прогресс: {0}/{1}\r\n", counter, SizeOfCorpus));

                string[] terms = doc.Content.Split(' ');

                for(int i = 0; i < terms.Length; ++i)
                {
                    int position = i + 1;
                    string term = RStemmer.Stem(terms[i]);
                    if (term.Length < 2) continue;

                    InvertedIndex.Add(term, doc.Id, position);
                }

                if (counter > 1000) break;
            }

            TextProgress.AddInformation("Индекс построен.\r\n");

            InvertedIndexHasBeenBuilt(this, new EventArgs());
        }

        private void LoadDataFromDB(bool ReconnectFlag = true)
        {
            if (ReconnectFlag || Connection == null)
            {
                if (Connection != null) Connection.Close();
                Connection = DBConnection.GetInstance(GetConnectionString());
            }

            string sql = "SELECT [Id],[Content] FROM [dbo].[Pages];";

            Connection.Open();
            DataTable dt = Connection.ExecuteQuery(sql);
            Connection.Close();

            DocumentCorpus.Adapter(dt);
        }
        private void PreprocessData()
        {
            foreach (IDocument Doc in DocumentCorpus)
            {
                Doc.Content = ClearText(Doc.Content);

                //Console.WriteLine("*******************************");
                //Console.WriteLine("Было:");
                //Console.WriteLine(Text);
                //Console.WriteLine("*******************************");
                //Console.WriteLine("Стало:");
                //Console.WriteLine(ClearText(Text));
            }
        }

        public static string ClearText(string Text)
        {
            Text = Text.ToLower();
            
            Regex reg = new Regex("[^абвгдеёжзийклмнопрстуфxцчшщъыьэюя-]");
            Text = reg.Replace(Text, " ");

            reg = new Regex(@"[\s]+");
            Text = reg.Replace(Text, " ");

            return Text;
        }


        private void fmInvIndex_Load(object sender, EventArgs e)
        {
            
        }

        private string GetConnectionString()
        {
            return tbConnectionString.Text;   
        }

        private void ProgressInfoIsChanged(object sender, EventArgs e)
        {
            tbProgressDisplay.Text = TextProgress.GetInformation();
            tbProgressDisplay.Refresh();
            tbConnectionString.Update();

            tbProgressDisplay.SelectionStart = tbProgressDisplay.TextLength;
            tbProgressDisplay.ScrollToCaret();
        }

       

        #region EventHandlers

        private void OnInvertedIndexBuildBegin(object sender, EventArgs e)
        {
            btnSaveInvertedIndexToFile.Enabled = false;
            btnSaveInvertedIndexToFileUsingCompression.Enabled = false;
        }
        private void OnInvertedIndexHasBeenBuilt(object sender, EventArgs e)
        {
            btnSaveInvertedIndexToFile.Enabled = true;
            btnSaveInvertedIndexToFileUsingCompression.Enabled = true;
        }

        private void OnSavingBegin(object sender, EventArgs e)
        {
            btnBuildIndex.Enabled = btnSaveInvertedIndexToFile.Enabled = btnSaveInvertedIndexToFileUsingCompression.Enabled
                = false;
        }

        private void OnSavingEnd(object sender, EventArgs e){
            btnBuildIndex.Enabled = btnSaveInvertedIndexToFile.Enabled = btnSaveInvertedIndexToFileUsingCompression.Enabled
                = true;
        }
        #endregion

        private void btnSaveInvertedIndexToFile_Click(object sender, EventArgs e)
        {
            
            saveFileDialog.Title = "Save an Inverted Index";

            saveFileDialog.FileName = "InvertedIndex";
            saveFileDialog.DefaultExt = ".ii";
            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                SavingBegin(null, null);
                TextProgress.AddInformation("Сохранение инвертированного индекса...\r\n");
                pbLoading.Visible = true;
                bgwSaving.RunWorkerAsync(saveFileDialog.FileName);

            }         
        }

        private void btnSaveInvertedIndexToFileUsingCompression_Click(object sender, EventArgs e)
        {

            saveFileDialog.Title = "Save a Compressed Inverted Index";
            saveFileDialog.FileName = "GammaEliasCompressedInvertedIndex";
            saveFileDialog.DefaultExt = ".cii";

            if (DialogResult.OK == saveFileDialog.ShowDialog())
            {
                SavingBegin(null, null);
                TextProgress.AddInformation("Сохранение сжатого инвертированного индекса...\r\n");

                pbLoading.Visible = true;
                bgwCompressingSaving.RunWorkerAsync(saveFileDialog.FileName); 
            }               
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        
        
        
        private void Save(string FileName)
        {
            try
            {
                File.WriteAllText(FileName, InvertedIndex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





        private void CompressingSave(string FileName)
        {
            try
            {
                using (FileStream fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    InvertedIndex.SaveCompressedToFile(fs);
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region BGWorkers() 
        BackgroundWorker bgwSaving;
        private void bgwSaving_DoWork(object sender, DoWorkEventArgs e)
        {
            string FileName = e.Argument.ToString();

            Save(FileName);
        }
        private void bgwSaving_RunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbLoading.Visible = false;
            TextProgress.AddInformation("Инвертированный индекс сохранен\r\n");

            SavingEnd(null, null);
        }
        
        
        BackgroundWorker bgwCompressingSaving;
        private void bgwCompressingSaving_DoWork(object sender, DoWorkEventArgs e)
        {
            string FileName = e.Argument.ToString();

            CompressingSave(FileName);
        }
        private void bgwCompressingSaving_RunCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbLoading.Visible = false;
            TextProgress.AddInformation("Сжатый инвертированный индекс сохранен\r\n");

            SavingEnd(null, null);
        }

        private void InitBackGrounWorkers()
        {
            bgwSaving = new BackgroundWorker();
            bgwSaving.DoWork += bgwSaving_DoWork;
            bgwSaving.RunWorkerCompleted += bgwSaving_RunCompleted;

            bgwCompressingSaving = new BackgroundWorker();
            bgwCompressingSaving.DoWork += bgwCompressingSaving_DoWork;
            bgwCompressingSaving.RunWorkerCompleted += bgwCompressingSaving_RunCompleted;
        }


        #endregion



    }
}
