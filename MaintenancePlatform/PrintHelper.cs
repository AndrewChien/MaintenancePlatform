using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows.Data;
using MaintenancePlatform.Print;
using ZNC.Component;

namespace MaintenancePlatform
{
    public class PrintHelper
    {

        /// <summary>
        /// 将DATAGRID 内容保存为XPS打印文件
        /// </summary>
        /// <param name="PageSize">每页显示数据条数</param>
        /// <param name="PageData">DATAGRID数据集合</param>
        /// <param name="FixedPageAddress">FIXEDPAGE文件路径</param>
        /// <param name="EffectiveTime">参数生效时间</param>
        public static void PrintDataPage(int PageSize, PrintCtrls PageData, string FixedPageAddress, string EffectiveTime,object Jkdx)
        {
            ICollectionView paraCollection;
            int iCount = PageData.Count;
            if (iCount == 0)
            {
                MessageBox.Show(string.Format("未查询到相应数据，无法打印！"));
                return;
            }
            int CacheIndex = 0;//记录索引
            int iPageCount = PageBarXPS.getPageCount(PageSize, iCount)[0];
            int iPageCount1 = iPageCount;
            int PageAmount = PageBarXPS.getPageCount(PageSize, iCount)[1];
            FixedPage[] tt = new FixedPage[iPageCount];
            if (PageAmount != 0)
            {
                iPageCount = iPageCount - 1;
                PrintCtrls paraCache = new PrintCtrls();
                Uri printViewUri = new Uri(FixedPageAddress, UriKind.Relative);
                tt[iPageCount] = (FixedPage)Application.LoadComponent(printViewUri);
                for (int i = iPageCount * PageSize; i < iCount; i++)
                {
                    paraCache.Add(PageData[i]);
                }

                DataGrid dg = tt[iPageCount].FindName("dg") as DataGrid;
                TextBlock tb1 = tt[iPageCount].FindName("tb") as TextBlock;
                tb1.Text = EffectiveTime;
                //TextBlock tb2 = tt[iPageCount].FindName("currentTime") as TextBlock;
                //tb2.Text = DateTime.Now.ToString();
                Label l1 = tt[iPageCount].FindName("StrPageBar") as Label;
                l1.Content = PageBarXPS.createPageBar(PageSize, iCount, iPageCount1);
                paraCollection = new CollectionView(paraCache);
                dg.ItemsSource = paraCollection;
            }
            for (int i = 0; i < iPageCount; i++)
            {
                PrintCtrls paraCache = new PrintCtrls();
                Uri printViewUri = new Uri(FixedPageAddress, UriKind.Relative);
                tt[i] = (FixedPage)Application.LoadComponent(printViewUri);
                for (; CacheIndex < PageSize * (i + 1); CacheIndex++)
                {
                    paraCache.Add(PageData[CacheIndex]);
                }
                Image logo = tt[i].FindName("logo") as Image;
                
                DataGrid dg = tt[i].FindName("dg") as DataGrid;
                TextBlock tb1 = tt[i].FindName("tb") as TextBlock;
                tb1.Text = "医疗设备管理系统定位记录表";
                //TextBlock tb2 = tt[i].FindName("currentTime") as TextBlock;
                //TextBlock sbmc = tt[i].FindName("sbmc") as TextBlock;
                //sbmc.Text = Jkdx.DXMC.ToString();
                //TextBlock lx = tt[i].FindName("lx") as TextBlock;
                //lx.Text = Jkdx.DXLX.ToString();
                //TextBlock rfid = tt[i].FindName("rfid") as TextBlock;
                //rfid.Text = Jkdx.RFID.ToString();
                //TextBlock cjdw = tt[i].FindName("cjdw") as TextBlock;
                //cjdw.Text = Jkdx.CJDW.ToString();
                //TextBlock lxfs = tt[i].FindName("lxfs") as TextBlock;
                //lxfs.Text = Jkdx.LXFS.ToString();
                //TextBlock lksj = tt[i].FindName("lksj") as TextBlock;
                //lksj.Text = Jkdx.LKSJ.ToShortDateString();
                //TextBlock tksj = tt[i].FindName("tksj") as TextBlock;
                //tksj.Text = Jkdx.TKSJ.ToString();
                //tb2.Text = DateTime.Now.ToString();
                Label l1 = tt[i].FindName("StrPageBar") as Label;
                l1.Content = PageBarXPS.createPageBar(PageSize, iCount, i + 1);
                paraCollection = new CollectionView(paraCache);
                dg.ItemsSource = paraCollection;
            }
            FileHelper.SaveXPS(tt, false, iPageCount1);
            string xpsFileName = FileHelper.GetXPSFromDialog(false);//得到临时的文件存储
            PrintWindow window = new PrintWindow();//调用显示容器
            window.fixedDocFile = xpsFileName;
            window.Show();
        }
    }

    public class PrintCtrls : ObservableCollection<Object>
    {
    }

    public class PageBarXPS
    {
        /// <summary>
        /// 创建FixedPage的PageBar
        /// </summary>
        /// <param name="iSize">总页数</param>
        /// <param name="iCount">每页记录数</param>
        /// <param name="curPageNo">当前页号</param>
        /// <returns></returns>
        public static string createPageBar(int iSize, int iCount, int curPageNo)
        {
            string strPageBar = string.Empty;
            int Size = iSize;
            int Count = iCount;
            int iPageCount = 0;
            int iLastPageCount = 0;
            int iCurPageNo = curPageNo;

            if ((iCount / iSize) == 0)
            {
                iPageCount = 1;
            }
            else
            {
                iPageCount = iCount / iSize;
                iLastPageCount = iCount - iPageCount * iSize;
                if (iLastPageCount == 0)
                {
                    iPageCount = iCount / iSize;
                }
                else
                {
                    iPageCount = iCount / iSize + 1;
                }
            }

            strPageBar = "第" + iCurPageNo + "页//" + "共" + iPageCount + "页";

            return strPageBar;
        }

        /// <summary>
        /// 获得页数即需要创建的fixedPage数,A[0]为页数，A[1]为最后一页记录数
        /// </summary>
        /// <param name="iSize"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public static int[] getPageCount(int iSize, int iCount)
        {
            int Size = iSize;
            int Count = iCount;
            int iPageCount = 0;
            int iLastPageCount = 0;
            int[] result = new int[2];
            if ((iCount / iSize) == 0)
            {
                iPageCount = 1;
                iLastPageCount = iCount;
            }
            else
            {
                iPageCount = iCount / iSize;
                iLastPageCount = iCount - iPageCount * iSize;
                if (iLastPageCount == 0)
                {
                    iPageCount = iCount / iSize;
                    iLastPageCount = iCount;
                }
                else
                {
                    iPageCount = iCount / iSize + 1;
                }
            }
            result[0] = iPageCount;
            result[1] = iLastPageCount;
            return result;
        }
    }
}
