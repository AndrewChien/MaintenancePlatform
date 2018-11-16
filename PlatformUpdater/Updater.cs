using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace PlatformUpdater
{
    public class FileInfo
    {
        #region Fields

        private string _auditor;
        private DateTime _lastUpdate;
        private string _name;
        private string _path;
        private string _ver;

        #endregion

        #region Property

        public DateTime LastUpdate
        {
            get
            {
                return _lastUpdate;
            }
            set
            {
                _lastUpdate = value;
            }
        }

        public string Ver
        {
            get
            {
                return _ver;
            }
            set
            {
                _ver = value;
            }
        }

        public string Auditor
        {
            get
            {
                return _auditor;
            }
            set
            {
                _auditor = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        #endregion
    }

    public class DirectoryInfo
    {
        #region Fields

        private string _auditor;
        private DateTime _lastUpdate;
        private string _name;
        private string _path;
        private string _ver;

        #endregion

        #region Property

        public DateTime LastUpdate
        {
            get
            {
                return _lastUpdate;
            }
            set
            {
                _lastUpdate = value;
            }
        }

        public string Ver
        {
            get
            {
                return _ver;
            }
            set
            {
                _ver = value;
            }
        }

        public string Auditor
        {
            get
            {
                return _auditor;
            }
            set
            {
                _auditor = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }

        #endregion
    }

    public enum ErrorLevel
    {
        None,
        Neglected,
        Serious
    }

    public class ErrorInfo
    {
        #region Fields

        private ErrorLevel _errorLevel = ErrorLevel.None;
        private string _file = string.Empty;
        private string _message = String.Empty;

        #endregion

        #region Property

        public string File
        {
            get
            {
                return _file;
            }
            set
            {
                _file = value;
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        public ErrorLevel ErrorLevel
        {
            get
            {
                return _errorLevel;
            }
            set
            {
                _errorLevel = value;
            }
        }

        #endregion
    }

    public class Updater
    {
        /// <summary>
        /// 要启动的主程序的名称
        /// </summary>
        /// <value>The main program.</value>
        public static string MainProgram
        {
            get;
            set;
        }

        /// <summary>
        /// 主程序的版本
        /// </summary>
        /// <value>The main program version.</value>
        public static string MainProgramVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器端地址
        /// </summary>
        /// <value>The URL.</value>
        public static string URL
        {
            get;
            set;
        }

        public static string User
        {
            get;
            set;
        }

        public static string Password
        {
            get;
            set;
        }

        #region Public Static Methods



        /// <summary>
        /// 得到下载的基础信息
        /// <remark> 
        /// Author:ZhangRongHua 
        /// Create DateTime: 2009-6-21 16:04
        /// Update History:     
        ///  </remark>
        /// </summary>
        /// <param name="srcFile">The SRC file.</param>
        /// <returns></returns>
        public static string GetBaseInfo(string srcFile)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(srcFile);
                XmlNode applicationNode = xDoc.SelectSingleNode("updater/application");
                if (applicationNode != null  )
                {
                    MainProgram = applicationNode.InnerText.Trim();
                    MainProgramVersion = applicationNode.Attributes["ver"].Value;
                }


                XmlNode urlNode = xDoc.SelectSingleNode("updater/updateUrls");
                if (urlNode != null && urlNode.Attributes["defaultUrl"] != null &&
                    !String.IsNullOrEmpty(urlNode.Attributes["defaultUrl"].Value))
                {
                    URL = urlNode.Attributes["defaultUrl"].Value;
                    User = urlNode.Attributes["User"].Value;
                    Password = urlNode.Attributes["Password"].Value;
                }
                else
                {
                    foreach (XmlNode xNode in urlNode.ChildNodes)
                    {
                        if (!String.IsNullOrEmpty(xNode.InnerText))
                        {
                            URL = xNode.InnerText;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return "wat.pms.winform.exe";
        }

        public static List<string> GetUpdateFileList(string localFile, string svrFile)
        {
            XmlDocument localDoc = new XmlDocument();
            XmlDocument svrDoc = new XmlDocument();
            XmlNode localNode;
            XmlNode svrNode;
            try
            {
                try
                {
                    localDoc.Load(localFile);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    svrDoc.Load(svrFile);
                    svrNode = svrDoc.SelectSingleNode("updater/dir");
                    return GetUpdateFileList(svrNode, String.Empty);
                }

                svrDoc.Load(svrFile);
                localNode = localDoc.SelectSingleNode("updater/dir");
                svrNode = svrDoc.SelectSingleNode("updater/dir");

                string path = ""; //svrNode.Attributes["name"].Value;

                return GetUpdateFileList(localNode, svrNode, path);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        #endregion

        #region Private Static Methods

        private static List<string> GetUpdateFileList(XmlNode xNode, string path)
        {
            if (xNode == null)
            {
                return null;
            }

            if (!String.IsNullOrEmpty(path))
            {
                path = string.Concat(path, "/");
            }

            List<string> updateFiles = new List<string>();

            XmlNodeList xNodeList = xNode.SelectNodes("file");
            foreach (XmlNode oNode in xNodeList)
            {
                FileInfo fileInfo = new FileInfo();
                fileInfo.Name = oNode.Attributes["name"].Value;
                fileInfo.LastUpdate = DateTime.Parse(oNode.Attributes["lastUpdate"].Value);
                fileInfo.Ver = oNode.Attributes["ver"].Value;
                fileInfo.Auditor = oNode.Attributes["auditor"].Value;
                fileInfo.Path = path;

                updateFiles.Add(String.Concat(path, fileInfo.Name));
            }

            xNodeList = xNode.SelectNodes("dir");
            foreach (XmlNode oNode in xNodeList)
            {
                DirectoryInfo dirInfo = new DirectoryInfo();
                dirInfo.Name = oNode.Attributes["name"].Value;
                dirInfo.LastUpdate = DateTime.Parse(oNode.Attributes["lastUpdate"].Value);
                dirInfo.Ver = oNode.Attributes["ver"].Value;
                string filePath = String.Concat(path, dirInfo.Name);
                updateFiles.AddRange(GetUpdateFileList(oNode, filePath));
            }
            return updateFiles;
        }

        private static List<string> GetUpdateFileList(XmlNode localNode, XmlNode svrNode, string path)
        {
            //if (String.IsNullOrEmpty(path)) return null;          

            Dictionary<string, FileInfo> localFiles = new Dictionary<string, FileInfo>();
            Dictionary<string, DirectoryInfo> localDirs = new Dictionary<string, DirectoryInfo>();
            List<string> updateFiles = new List<string>(300);

            if (localNode == null && svrNode == null)
            {
                return null;
            }

            if (localNode == null)
            {
                return GetUpdateFileList(svrNode, path);
            }

            path = String.Concat(path, "/");

            XmlNodeList xNodeList = localNode.SelectNodes("file");
            foreach (XmlNode xNode in xNodeList)
            {
                FileInfo fileInfo = new FileInfo();
                fileInfo.Name = xNode.Attributes["name"].Value;
                fileInfo.LastUpdate = DateTime.Parse(xNode.Attributes["lastUpdate"].Value);
                fileInfo.Ver = xNode.Attributes["ver"].Value;
                fileInfo.Auditor = xNode.Attributes["auditor"].Value;
                fileInfo.Path = path;
                localFiles.Add(fileInfo.Name, fileInfo);
            }

            xNodeList = svrNode.SelectNodes("file");
            foreach (XmlNode xNode in xNodeList)
            {
                FileInfo fileInfo = new FileInfo();
                fileInfo.Name = xNode.Attributes["name"].Value;
                fileInfo.LastUpdate = DateTime.Parse(xNode.Attributes["lastUpdate"].Value);
                fileInfo.Ver = xNode.Attributes["ver"].Value;
                fileInfo.Auditor = xNode.Attributes["auditor"].Value;
                fileInfo.Path = path;
                if (localFiles.ContainsKey(fileInfo.Name))
                {
                    if (localFiles[fileInfo.Name].LastUpdate < fileInfo.LastUpdate)
                    {
                        updateFiles.Add(String.Concat(path, fileInfo.Name));
                    }
                }
                else
                {
                    updateFiles.Add(string.Concat(path, fileInfo.Name));
                }
            }

            XmlNodeList localNodeList = localNode.SelectNodes("dir");
            foreach (XmlNode xNode in localNodeList)
            {
                DirectoryInfo dirInfo = new DirectoryInfo();
                dirInfo.Name = xNode.Attributes["name"].Value;
                dirInfo.LastUpdate = DateTime.Parse(xNode.Attributes["lastUpdate"].Value);
                dirInfo.Ver = xNode.Attributes["ver"].Value;
                localDirs.Add(dirInfo.Name, dirInfo);
            }

            XmlNodeList svrNodeList = svrNode.SelectNodes("dir");
            for (int i = 0; i < svrNodeList.Count; i++)
            {
                XmlNode xNode = svrNodeList.Item(i);
                DirectoryInfo dirInfo = new DirectoryInfo();
                dirInfo.Name = xNode.Attributes["name"].Value;
                dirInfo.LastUpdate = DateTime.Parse(xNode.Attributes["lastUpdate"].Value);
                dirInfo.Ver = xNode.Attributes["ver"].Value;
                string filePath = String.Concat(path, dirInfo.Name);

                if (localDirs.ContainsKey(dirInfo.Name))
                {
                    if (localDirs[dirInfo.Name].LastUpdate < dirInfo.LastUpdate)
                    {
                        updateFiles.AddRange(GetUpdateFileList(localNodeList.Item(i), xNode, filePath));
                    }
                }
                else
                {
                    updateFiles.AddRange(GetUpdateFileList(localNodeList.Item(i), xNode, filePath));
                }
            }

            return updateFiles;
        }

        #endregion
    }
}