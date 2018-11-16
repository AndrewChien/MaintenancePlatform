using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Microsoft.Win32;

namespace ZNC.Component
{
    public class FileHelper
    {
        public static string GetXPSFromDialog(bool isSaved)
        {
            if (isSaved)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Filter = "XPS Document files (*.xps)|*.xps";
                saveFileDialog.FilterIndex = 1;

                if (saveFileDialog.ShowDialog() == true)
                {
                    return saveFileDialog.FileName;
                }
                else
                {
                    return null;
                }
            }
            else return string.Format("{0}\\temp.xps", Environment.CurrentDirectory);//制造一个临时存储
        }


        /// <summary>
        /// 这个静态方法主要是显示选择对话框以提供文件的保存位置
        /// 将传入的FixedPage对象数组（多页）写入到.xps文件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="page"></param>
        /// <param name="isSaved"></param>
        /// <returns></returns>
        public static bool SaveXPS(FixedPage[] page, bool isSaved, int iPageCount)
        {
            FixedDocument fixedDoc = new FixedDocument();//创建一个文档
            fixedDoc.DocumentPaginator.PageSize = new Size(96 * 8.5, 96 * 11);
            PageContent[] pageContent = new PageContent[iPageCount];
            for (int i = 0; i < iPageCount; i++)
            {
                pageContent[i] = new PageContent();
                ((IAddChild)pageContent[i]).AddChild(page[i]);
                fixedDoc.Pages.Add(pageContent[i]);//将对象加入到当前文档中
            }
            FileHelper fh = new FileHelper();
            string containerName = GetXPSFromDialog(isSaved);
            if (containerName != null)
            {
                try
                {
                    File.Delete(containerName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

                XpsDocument _xpsDocument = new XpsDocument(containerName, FileAccess.Write);

                XpsDocumentWriter xpsdw = XpsDocument.CreateXpsDocumentWriter(_xpsDocument);
                xpsdw.Write(fixedDoc);//写入XPS文件
                _xpsDocument.Close();
                return true;
            }
            else return false;
        }
        static XpsDocument xpsPackage = null;
        public static void LoadDocumentViewer(string xpsFileName, DocumentViewer viewer)
        {
            XpsDocument oldXpsPackage = xpsPackage;//保存原来的XPS包
            xpsPackage = new XpsDocument(xpsFileName, FileAccess.Read, CompressionOption.NotCompressed);//从文件中读取XPS文档

            FixedDocumentSequence fixedDocumentSequence = xpsPackage.GetFixedDocumentSequence();//从XPS文档对象得到FixedDocumentSequence
            viewer.Document = fixedDocumentSequence as IDocumentPaginatorSource;

            if (oldXpsPackage != null)
                oldXpsPackage.Close();
            xpsPackage.Close();
        }
    }
}
