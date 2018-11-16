using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace PlatformUpdater
{
    /// <summary>
    /// Description: 
    /// Author: ZhangRongHua
    /// Create DateTime: 2009-6-21 12:25
    /// UpdateHistory:      
    /// </summary>
    public partial class frmUpdate : Form
    {
        #region Fields

        private const string CONFIGFILE = "update.xml";
        private const string UPDATEDIR = "PMS";
        private string appPath = Application.StartupPath;
        private List<ErrorInfo> errorList = new List<ErrorInfo>();
        private string locFile = String.Concat(Application.StartupPath, "\\", CONFIGFILE);
        private string tmpUpdateFile = "TmpUpdate.xml";
        private List<string> updateList;
        private string updateTmpPath = string.Concat(Path.GetTempPath(), "\\", UPDATEDIR);   
        private string url = String.Empty;

        private FTP ftp = null;

        #endregion

        #region Delegates

        public delegate void AsycDownLoadFile(string srcFile, string destFile, int i);

        public delegate void ExecuteUpdateFiles(string srcPath, string destPath);

        public delegate void UpdateComplete();

        public delegate void UpdateUI(int i, string message);

        #endregion

        public event UpdateComplete OnUpdateComplete;

        #region Constructor

        public frmUpdate()
        {
            InitializeComponent();
            OnUpdateComplete += new UpdateComplete(frmUpdate_OnUpdateComplete);
        }

        #endregion

        #region Event Handler

        private void frmUpdate_Load(object sender, EventArgs e)
        {
           if(Directory.Exists(updateTmpPath))
           {
               Directory.Delete(updateTmpPath, true);
           }
         
            // 如果有主程序启动，则关闭
            Process[] ps = Process.GetProcesses();
            foreach (Process p in ps)
            {
                //MessageBox.Show(p.ProcessName);
                if (p.ProcessName.ToLower() == "wat.pms.winform")
                {
                    p.Kill();
                    break;
                }
            }

            GetUpdateFiles();
        }

        private void frmUpdate_OnUpdateComplete()
        {
            ExecuteUpdateFiles dExecuteUpdateFiles = new ExecuteUpdateFiles(ExecuteUpdate);
            Invoke(dExecuteUpdateFiles, new object[] {updateTmpPath, appPath});
        }

        private void frmUpdate_Shown(object sender, EventArgs e)
        {
            Thread updateThread = new Thread(new ThreadStart(DownLoadUpdateFiles));
            updateThread.SetApartmentState(ApartmentState.STA);
            updateThread.IsBackground = true;
            Thread.Sleep(500);
            updateThread.Start();

  
         }

        #endregion

        #region Private Methods

         /// <summary>
         /// 将目标文件替换为本地文件
         /// <remark> 
         /// Author:ZhangRongHua 
         /// Create DateTime: 2009-6-21 10:28
         /// Update History:     
         ///  </remark>
         /// </summary>
         /// <param name="srcFile">The SRC file.</param>
         /// <param name="destFile">The dest file.</param>
        private void DownLoadFile(string srcFile, string destFile)
        {
            if(ftp == null )
            {
                ftp = new FTP(Updater.URL, "", Updater.User, Updater.Password);
            }

           

            if(!ftp.Connected)
            {
                MessageBox.Show("无法连接远程服务器，无法更新程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(Updater.MainProgram);
                Close();
                
            }

            // 得到服务器端的配置文件
             ftp.Get(srcFile, updateTmpPath, destFile);

          
 
        }

        /// <summary>
        /// 得到需要更新的文件清单
        /// <remark> 
        /// Author:ZhangRongHua 
        /// Create DateTime: 2009-6-21 10:29
        /// Update History:     
        ///  </remark>
        /// </summary>
        private void GetUpdateFiles()
        {
            Updater.GetBaseInfo(locFile);
            url = Updater.URL;
            string svrFile = String.Concat(url, CONFIGFILE);

            if (String.IsNullOrEmpty(svrFile))
            {
                BroadCastOnUpdateComplete();
                return;
            }

            Directory.CreateDirectory(updateTmpPath);
            try
            {
                DownLoadFile(CONFIGFILE, tmpUpdateFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法连接远程服务器，无法更新程序", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(Updater.MainProgram);
                Close();
            }

            updateList = Updater.GetUpdateFileList(locFile, tmpUpdateFile);
            if (updateList == null || updateList.Count < 1)
            {
                BroadCastOnUpdateComplete();
                return;
            }

            pbUpdate.Maximum = updateList.Count;
            pbUpdate.Minimum = 0;
            lblInfo.Text = String.Concat(updateList.Count, "个文件需要更新!");
            lblInfo.BringToFront();
            lblState.BringToFront();
            lblInfo.Update();

            pbUpdate.Maximum = updateList.Count;
            pbUpdate.Minimum = 0;

            for (int i = 0; i < updateList.Count; i++)
            {
                string file = updateList[i];
                ListViewItem lvItem = new ListViewItem();
                lvItem.Text = file;
                lvItem.SubItems.Add(Updater.MainProgramVersion);
                lvUpdateList.Items.Add(lvItem);
            }
        }

        private void UpdateUIInfo(int i, string message)
        {
            pbUpdate.Value = i + 1;
            lblState.Text = message;
            lblState.Update();
        }

        private void BroadCastOnUpdateComplete()
        {
            if (OnUpdateComplete != null)
            {
                OnUpdateComplete();
            }
        }

        private void DownLoadUpdateFiles()
        {
            if (updateList == null || updateList.Count < 1)
            {
                BroadCastOnUpdateComplete();
                return;
            }

            try
            {
                UpdateUI dUpdateUI = new UpdateUI(UpdateUIInfo);
                AsycDownLoadFile dAsycDownLoadFile = new AsycDownLoadFile(DownLoadFile);
                for (int i = 0; i < updateList.Count; i++)
                {
                    string file = updateList[i];
                    string destFile = String.Concat(updateTmpPath, "\\", file);
                    string destFileName = destFile.Substring(destFile.LastIndexOf("/") + 1);
                    string srcFile = String.Concat(url, file);
                    var srcFileName = file.Trim('/');
                    destFile = destFile.Replace("/", "\\");
                    Directory.CreateDirectory(destFile.Substring(0, destFile.LastIndexOf("\\")));
                    string curentFile = String.Concat("正在更新第", i + 1, "/", updateList.Count, "个", file);
                   
                    Invoke(dAsycDownLoadFile, new object[] { srcFileName, srcFileName, i });
                    Thread.Sleep(50);
                    Invoke(dUpdateUI, new object[] {i, curentFile});
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            BroadCastOnUpdateComplete();
        }

        

        private void CopyUpdateFiles(string srcPath, string destPath)
        {
            string[] files = Directory.GetFiles(srcPath);
            for (int i = 0; i < files.Length; i++)
            {
                string srcFile = files[i];
                string destFile = string.Concat(destPath, "\\", Path.GetFileName(srcFile));
                try
                {
                    File.Copy(srcFile, destFile, true);
                }
                catch (System.IO.IOException  ex)
                {
                    
                    
                }
                 
            }

            string[] dirs = Directory.GetDirectories(srcPath);
            for (int i = 0; i < dirs.Length; i++)
            {
                srcPath = dirs[i];
                string tmpDestPath = String.Concat(destPath, "\\", Path.GetFileName(srcPath));
                Directory.CreateDirectory(tmpDestPath);
                CopyUpdateFiles(srcPath, tmpDestPath);
            }
        }



        /// <summary>
        /// 更新完成后，要执行的动作（将下载的文件从临时目录复制到主目录，重启主程序）
        /// <remark> 
        /// Author:ZhangRongHua 
        /// Create DateTime: 2009-6-21 12:25
        /// Update History:     
        ///  </remark>
        /// </summary>
        /// <param name="srcPath">The SRC path.</param>
        /// <param name="destPath">The dest path.</param>
        private void ExecuteUpdate(string srcPath, string destPath)
        {
            if (errorList != null && errorList.Count < 1)
            {
                lblInfo.Text = "正在执行更新";
                lblInfo.Update();
                CopyUpdateFiles(srcPath, destPath);
                File.Copy(tmpUpdateFile, locFile, true);
            }
            Process.Start(Updater.MainProgram);

            Close();
        }

        private void DownLoadFile(string srcFile, string destFile, ListViewItem lvItem)
        {
            try
            {
                DownLoadFile(srcFile, destFile);
                lvItem.SubItems.Add("Ok");
             }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                lvItem.SubItems.Add("fail");
                ErrorInfo errorInfo = new ErrorInfo();
                errorInfo.File = srcFile;
                errorInfo.ErrorLevel = ErrorLevel.Serious;
                errorInfo.Message = ex.Message;
                errorList.Add(errorInfo);
            }
        }

        private void DownLoadFile(string srcFile, string destFile, int i)
        {
            ListViewItem lvItem = lvUpdateList.Items[i];

            lvUpdateList.Items[i].EnsureVisible();
            try
            {
                DownLoadFile(srcFile, destFile);
                lvItem.SubItems.Add("Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                lvItem.SubItems.Add("fail");
                ErrorInfo errorInfo = new ErrorInfo();
                errorInfo.File = srcFile;
                errorInfo.ErrorLevel = ErrorLevel.Serious;
                errorInfo.Message = ex.Message;
                errorList.Add(errorInfo);
                MessageBox.Show(destFile);
            }
        }

        #endregion
    }
}