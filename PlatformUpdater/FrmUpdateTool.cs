using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PlatformUpdater
{
    public partial class FrmUpdateTool : Form
    {
        #region Fileds

        private string appName = "WAT.PMS.WINFORM.EXE";
        public string auditor = "tanrh";
        public string curVer = "1.0.0.0";
        private string path = "http://localhost/";
        private XmlDocument xDoc = new XmlDocument();

        #endregion

        #region Constructor

        public FrmUpdateTool()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Methods

        private string BuildUpdateConfigFile(string path)
        {
            string tab = "\t";
            StringBuilder sb = new StringBuilder(1500);
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sb.AppendLine("<updater>");

            sb.Append(tab);
            sb.AppendLine(string.Format("<description>{0}</description>", "update"));
            sb.AppendLine();

            sb.Append(tab);
            string updateUrl = String.Format("{0}{1}/", "http://localhost/", Path.GetFileName(path));
            sb.AppendLine(String.Format("<updateUrls defaultUrl=\"{0}\" User=\"{1}\" Password=\"{2}\" >", txtFtp.Text.Trim(), txtUser.Text.Trim(), txtPassword.Text.Trim()));
            sb.Append(tab).Append(tab);
            sb.AppendLine(String.Format("<updateUrl>{0}</updateUrl>", txtFtp.Text.Trim()));
            sb.Append(tab);
            sb.AppendLine("</updateUrls>");
            sb.AppendLine();

            sb.Append(tab);
            sb.AppendLine(String.Format("<application ver=\"{0}\" path=\"{1}\" lastUpdate=\"{2}\" >",
                                        curVer,
                                        path,
                                        DateTime.Now.ToString()));
            sb.Append(tab).Append(tab).AppendLine(appName);
            sb.Append(tab);
            sb.AppendLine("</application>");
            sb.AppendLine();

            //生成目录
            sb.AppendLine(BuildUpdateConfigFile(path, "\t"));

            sb.AppendLine();
            sb.Append(tab);
            sb.AppendLine(String.Format("<updateLog updateTime=\"{0}\">", DateTime.Now.ToString()));
            sb.Append(tab).Append(tab);
            sb.AppendLine("<files>");
            sb.Append(tab).Append(tab).Append(tab);
            sb.AppendLine(String.Format("<file name=\"{0}\" path=\"{1}\" ver=\"{2}\" error=\"\" />",
                                        String.Empty,
                                        String.Empty,
                                        String.Empty));
            sb.Append(tab).Append(tab);
            sb.AppendLine("</files>");
            sb.Append(tab);
            sb.AppendLine("</updateLog>");

            sb.AppendLine("</updater>");

            return sb.ToString();
        }

        private string BuildUpdateConfigFile(string path, string tab)
        {
            string[] files = Directory.GetFiles(path);
            StringBuilder sb = new StringBuilder(100);
            string pathName = path.Substring(path.LastIndexOf("\\") + 1);
            string dirTab = tab;
            sb.Append(dirTab);
            sb.AppendLine(String.Format("<dir name=\"{0}\" lastUpdate=\"{1}\" ver=\"{2}\" >",
                                        pathName,
                                        Directory.GetLastWriteTimeUtc(path),
                                        curVer));
            tab = string.Concat(tab, "\t");
            for (int i = 0; i < files.Length; i++)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo();//files[i]
                    sb.Append(tab);
                    sb.AppendFormat("<file lastUpdate=\"{0}\" ver=\"{1}\" auditor=\"{2}\" name=\"{3}\" />",
                                    fileInfo.LastUpdate,
                                    curVer,
                                    auditor,
                                    fileInfo.Name);
                    sb.AppendLine();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            string[] dirs = Directory.GetDirectories(path);
            for (int j = 0; j < dirs.Length; j++)
            {
                sb.AppendLine(BuildUpdateConfigFile(dirs[j], tab));
            }
            sb.Append(dirTab);
            sb.AppendFormat("</dir>");
            return sb.ToString();
        }

        private void SaveConfigureFile(string fileName)
        {
            FileStream fs = null;
            try
            {
                fs = File.Open(fileName, FileMode.Create, FileAccess.Write);
                TextWriter tw = new StreamWriter(fs, Encoding.UTF8);
                tw.Write(txtContent.Text);
                tw.Flush();
                tw.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Dispose();
                    fs = null;
                }
            }
        }

        private void LoadDirectoryFiles(TreeNode dirNode, XmlNode xNode)
        {
            XmlNodeList xNodeList = xNode.SelectNodes("file");
            foreach (XmlNode oNode in xNodeList)
            {
                string fileName = oNode.Attributes["name"] != null ? oNode.Attributes["name"].Value : String.Empty;
                if (!String.IsNullOrEmpty(fileName))
                {
                    TreeNode fileNode = new TreeNode();
                    fileNode.Text = fileName;
                    fileNode.Tag = oNode;
                    fileNode.ImageKey = "file2";
                    fileNode.SelectedImageKey = "file2";
                    dirNode.Nodes.Add(fileNode);
                }
            }

            xNodeList = xNode.SelectNodes("dir");
            foreach (XmlNode oNode in xNodeList)
            {
                string directory = oNode.Attributes["name"] != null ? oNode.Attributes["name"].Value : "文件夹";
                TreeNode subDirNode = new TreeNode(directory);
                subDirNode.Tag = oNode;
                subDirNode.ImageKey = "directory";
                subDirNode.SelectedImageKey = "directory";
                dirNode.Nodes.Add(subDirNode);
                LoadDirectoryFiles(subDirNode, oNode);
            }
        }

        private void LoadUpdateConfigureFile(TreeView tv, string text)
        {
            xDoc.LoadXml(text);

            XmlNode rootNode = xDoc.DocumentElement;

            XmlNode xNode = rootNode.SelectSingleNode("description");
            TreeNode tNode = new TreeNode("描述信息");
            tNode.ImageKey = "file2";
            tNode.SelectedImageKey = "file2";
            tNode.Tag = xNode.InnerText;
            tv.Nodes.Add(tNode);

            xNode = rootNode.SelectSingleNode("updateUrls");
            tNode = new TreeNode("服务器列表");
            tNode.ImageKey = "select";
            tNode.SelectedImageKey = "select";
            tv.Nodes.Add(tNode);
            tNode.Tag = xNode.Attributes["defaultUrl"] != null ? xNode.Attributes["defaultUrl"].Value : "";
            XmlNodeList xNodelist = xNode.SelectNodes("updateUrl");
            foreach (XmlNode oNode in xNodelist)
            {
                if (String.IsNullOrEmpty(oNode.InnerText))
                {
                    TreeNode urlNode = new TreeNode(oNode.InnerText);
                    urlNode.Tag = oNode;
                    urlNode.ImageKey = "urlfile";
                    urlNode.SelectedImageKey = "urlfile";
                    tNode.Nodes.Add(urlNode);
                }
            }

            xNode = rootNode.SelectSingleNode("application");
            tNode = new TreeNode("应用程序");
            tNode.ImageKey = "directory";
            tNode.SelectedImageKey = "directory";
            tNode.Tag = xNode;
            tv.Nodes.Add(tNode);
            TreeNode appNode = new TreeNode(xNode.InnerText);
            appNode.SelectedImageKey = "application";
            appNode.ImageKey = "application";
            tNode.Nodes.Add(appNode);

            xNode = rootNode.SelectSingleNode("dir");
            string appDirectory = xNode.Attributes["name"] != null ? xNode.Attributes["name"].Value : "主程序文件夹";
            TreeNode tAppDirNode = new TreeNode(appDirectory);
            tAppDirNode.ImageKey = "directory";
            tAppDirNode.SelectedImageKey = "directory";
            tAppDirNode.Tag = xNode;
            tv.Nodes.Add(tAppDirNode);

            LoadDirectoryFiles(tAppDirNode, xNode);

            xNode = rootNode.SelectSingleNode("updateLog");
            TreeNode logNode = new TreeNode("更新日志");
            logNode.ImageKey = "directory";
            logNode.SelectedImageKey = "directory";
            logNode.Tag = xNode;
            tv.Nodes.Add(logNode);
            xNodelist = xNode.SelectNodes("files/file");
            foreach (XmlNode oNode in xNodelist)
            {
                if (oNode.Attributes["name"] != null && String.IsNullOrEmpty(oNode.Attributes["name"].Value))
                {
                    TreeNode logfileNode = new TreeNode(oNode.Attributes["name"].Value);
                    logfileNode.ImageKey = "file1";
                    logfileNode.SelectedImageKey = "file1";
                    logNode.Nodes.Add(logfileNode);
                }
            }
        }

        #endregion

        #region EventHandler

        private void btnBuild_Click(object sender, EventArgs e)
        {
            string selectPath = string.Empty;
            FolderBrowserDialog fdDlg = new FolderBrowserDialog();
            if (fdDlg.ShowDialog() == DialogResult.OK)
            {
                selectPath = fdDlg.SelectedPath;
                if (String.IsNullOrEmpty(selectPath))
                {
                    return;
                }

                txtContent.Text = BuildUpdateConfigFile(selectPath);

                LoadUpdateConfigureFile(tvFiles, txtContent.Text);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.FileName = "update";
            saveDlg.Filter = "(*.xml)|*.xml";
            if (saveDlg.ShowDialog() == DialogResult.OK)
            {
                SaveConfigureFile(saveDlg.FileName);
            }
        }

        #endregion
    }
}