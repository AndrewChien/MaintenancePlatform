using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace PlatformUpdater
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private string dir = Application.StartupPath;
        private void rtbInfo_TextChanged(object sender, EventArgs e)
        {
        }

        private void CopyFile(string srcFile, string destFile)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string locFile = @"E:\update1.configure";
            //string svrFile = @"E:\update2.configure";
            string locFile = txtlocal.Text;
            string svrFile = txtsvr.Text;

            if (String.IsNullOrEmpty(svrFile))
            {
                return;
            }

            List<string> updatelst = Updater.GetUpdateFileList(locFile, svrFile);
            if (updatelst == null)
            {
                return;
            }
            foreach (string s in updatelst)
            {
                rtbInfo.AppendText(s);
                rtbInfo.AppendText("\r\n");
            }

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(svrFile);

            FolderBrowserDialog fdDlg = new FolderBrowserDialog();
            if (fdDlg.ShowDialog() == DialogResult.OK)
            {
                string dir = fdDlg.SelectedPath;
                foreach (string file in updatelst)
                {
                    string destFile = String.Concat(dir, "\\", file);
                    Directory.CreateDirectory(destFile.Substring(0, destFile.LastIndexOf("\\")));
                    try
                    {
                        File.Copy(String.Concat(txturl.Text, file), destFile);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void btnlocal_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "(*.configure;*.*)|*.configure;*.*;";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                txtlocal.Text = openDlg.FileName;
            }
        }

        private void btnsvr_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "(*.configure;*.*)|*.configure;*.*;";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                txtsvr.Text = openDlg.FileName;
            }
        }

        private void btnurl_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdDlg = new FolderBrowserDialog();
            if (fdDlg.ShowDialog() == DialogResult.OK)
            {
                txturl.Text = fdDlg.SelectedPath + "\\";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //WCFService.WCFService ser = new WCFService.WCFService();
            //DataSet ds = ser.getUpdateList();
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{

            //    int flag = int.Parse(ds.Tables[0].Rows[i]["Flag"].ToString());
            //    string url = ds.Tables[0].Rows[i]["URL"].ToString();
            //        if(flag == 1)
            //        {
            //             Download(url, dir);
            //        }
            //}
        }

        public bool Download(string URL, string Dir)
        {
            WebClient client = new WebClient();
            string fileName = URL.Substring(URL.LastIndexOf("/") + 1);  //被下载的文件名
            string Path = Dir + fileName;   //另存为的绝对路径＋文件名
            try
            {
                WebRequest myre = WebRequest.Create(URL);
                client.DownloadFile(URL, fileName);
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);
                byte[] mbyte = r.ReadBytes((int)fs.Length);
                FileStream fstr = new FileStream(Path, FileMode.OpenOrCreate, FileAccess.Write);
                fstr.Write(mbyte, 0, (int)fs.Length);
                fstr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return false;
            }
            return true;
        }
    }
}