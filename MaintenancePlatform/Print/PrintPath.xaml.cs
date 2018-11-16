using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Configuration;
using System.Data;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brush = System.Drawing.Brush;
using Color = System.Drawing.Color;
using DashStyle = System.Drawing.Drawing2D.DashStyle;
using Pen = System.Drawing.Pen;

namespace MaintenancePlatform.Print
{
    /// <summary>
    /// PrintPath.xaml 的交互逻辑
    /// </summary>
    public partial class PrintPath : Page
    {
        //PrintDocument类是实现打印功能的核心，它封装了打印有关的属性、事件、和方法
        PrintDocument printDocument = new PrintDocument();
        private static System.Drawing.Image image = new Bitmap(1100, 2200);
        Graphics g = Graphics.FromImage(image);//Properties.Resources.card0

        public PrintPath()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        struct SectionData
        {
            public int areaid;//区域编号
            public string areaname;//区域名称
            public string arrive;//到达时间
            public string leave;//离开时间
            public string stay;//停留时间
        }

        private List<SectionData> adddata(string _uid)
        {
            var rtn = new List<SectionData>();
            try
            {
                using (var dt = DBHelper.GetDatable("select * from LOCATE_TJQY where GLBH=" + _uid, ""))
                {
                    int ax = dt.Rows.Count;
                    foreach (DataRow row in dt.Rows)
                    {
                        var a = new SectionData();
                        a.areaid = 0;
                        a.areaname = row["JRQY"] != DBNull.Value ? row["JRQY"].ToString() : "";//停经区域
                        a.arrive = row["JRSJ"] != DBNull.Value ? DateTime.Parse(row["JRSJ"].ToString()).ToShortTimeString() : "";//进入
                        a.leave = row["TCSJ"] != DBNull.Value ? DateTime.Parse(row["TCSJ"].ToString()).ToShortTimeString() : "";//退出
                        a.stay = row["TLSJ"] != DBNull.Value ? row["TLSJ"].ToString() : "";//停留
                        rtn.Add(a);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "数据读取错误！", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                Log.CreateLog(ex);
            }
            return rtn;
        }

        /// <summary>
        /// 1.打印附属信息
        /// </summary>
        private void PrintComment(System.Drawing.Point p, Brush wordbr, Font wordft, string _uid)
        {
            try
            {
                using (var dt = DBHelper.GetDatable("select * from LOCATE_JKDX where DXXZ='医院人员' and RFID <> ''and DXBH=" + _uid, ""))
                {
                    //if (dt.Rows[0]["DXMC"] != DBNull.Value)
                    //{
                    //    if (dt.Rows[0]["DXLX"] != DBNull.Value)
                    //    {
                    //        if (dt.Rows[0]["DXLX"].ToString() == "女")
                    //        {
                    //            g.DrawString("欢迎您的莅临，" + dt.Rows[0]["DXMC"] + "女士：", wordft, wordbr, p.X, p.Y);
                    //        }
                    //        else
                    //        {
                    //            g.DrawString("欢迎您的莅临，" + dt.Rows[0]["DXMC"] + "先生：", wordft, wordbr, p.X, p.Y);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        g.DrawString("欢迎您的莅临，" + dt.Rows[0]["DXMC"] + "先生/女士：", wordft, wordbr, p.X, p.Y);
                    //    }
                    //}
                    //else
                    //{
                    //    g.DrawString("欢迎您的莅临，" + "____" + "先生/女士：", wordft, wordbr, p.X, p.Y);
                    //}

                    p.X -= 30;
                    p.Y -= 85;

                    if (dt.Rows[0]["KSMC"] != DBNull.Value)
                    {
                        g.DrawString("科室名称:" + dt.Rows[0]["KSMC"], wordft, wordbr, p.X + 150, p.Y + 50);
                    }
                    else
                    {
                        g.DrawString("科室名称:", wordft, wordbr, p.X + 150, p.Y + 50);
                    }
                    if (dt.Rows[0]["RFID"] != DBNull.Value)
                    {
                        g.DrawString("RFID编号:" + dt.Rows[0]["RFID"], wordft, wordbr, p.X + 430, p.Y + 50);
                    }
                    else
                    {
                        g.DrawString("RFID编号:）", wordft, wordbr, p.X + 430, p.Y + 50);
                    }
                    if (dt.Rows[0]["LKSJ"] != DBNull.Value)
                    {
                        g.DrawString("领卡时间:" + dt.Rows[0]["LKSJ"], wordft, wordbr, p.X + 150, p.Y + 85);
                    }
                    else
                    {
                        g.DrawString("领卡时间:", wordft, wordbr, p.X + 150, p.Y + 85);
                    }
                    if (dt.Rows[0]["TKSJ"] != DBNull.Value)
                    {
                        g.DrawString("退卡时间:" + dt.Rows[0]["TKSJ"], wordft, wordbr, p.X + 430, p.Y + 85);
                    }
                    else
                    {
                        g.DrawString("退卡时间:", wordft, wordbr, p.X + 430, p.Y + 85);
                    }
                    g.DrawString("打印时间:" + DateTime.Now, wordft, wordbr, p.X + 150, p.Y + 120);
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "附属信息打印失败！", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                Log.CreateLog(ex);
            }
        }

        /// <summary>
        /// 一、开始画
        /// </summary>
        private void Draw(string _uid)
        {
            try
            {
                int jiange = 300;

#pragma warning disable 612,618
                string backimg = ConfigurationSettings.AppSettings["backimg"].ToString(CultureInfo.InvariantCulture);
                System.Drawing.Image temp = System.Drawing.Image.FromFile(backimg);
                int outwidth = int.Parse(ConfigurationSettings.AppSettings["outwidth"]);
                int outheight = int.Parse(ConfigurationSettings.AppSettings["outheight"]);
                Rectangle destRect = new Rectangle(0, 0, outwidth, outheight);
                //g.DrawImage(temp, destRect, 0, 0, outwidth, outheight, GraphicsUnit.Pixel);
                g.DrawImage(temp, destRect);
                var datas = adddata(_uid);

                string area1 = ConfigurationSettings.AppSettings["area1"].ToString(CultureInfo.InvariantCulture);
                string area2 = ConfigurationSettings.AppSettings["area2"].ToString(CultureInfo.InvariantCulture);
                string area3 = ConfigurationSettings.AppSettings["area3"].ToString(CultureInfo.InvariantCulture);
                string area4 = ConfigurationSettings.AppSettings["area4"].ToString(CultureInfo.InvariantCulture);

                var p1 = ConfigurationSettings.AppSettings["nowpoint"].ToString(CultureInfo.InvariantCulture);
                var p2 = ConfigurationSettings.AppSettings["commentpoint"].ToString(CultureInfo.InvariantCulture);
                var nowpoint = new System.Drawing.Point(int.Parse(p1.Split(',')[0]), int.Parse(p1.Split(',')[1]));//初始化第一个区域的坐标
                var commentpoint = new System.Drawing.Point(int.Parse(p2.Split(',')[0]), int.Parse(p2.Split(',')[1]));//附属信息区首坐标初始化
                int rightline = int.Parse(ConfigurationSettings.AppSettings["rightline"]);
                int arrowlength = int.Parse(ConfigurationSettings.AppSettings["arrowlength"]);
                int areawidth = int.Parse(ConfigurationSettings.AppSettings["areawidth"]);
                int areaheight = int.Parse(ConfigurationSettings.AppSettings["areaheight"]);
                int personwidth = int.Parse(ConfigurationSettings.AppSettings["personwidth"]);
                int personheight = int.Parse(ConfigurationSettings.AppSettings["personheight"]);
                int commentfontsize = int.Parse(ConfigurationSettings.AppSettings["commentfontsize"]);
                //var outwidth = 750;//背景宽度
                //var outheight = 1070;//背景高度
                //const string area1 = "护士站";
                //const string area2 = "设备科";
                //const string area3 = "主任办公室";//展区名称，请与数据库存储名称一致
                //var rightline = 800;//右边线,不能超过
                //var arrowlength = 100;//箭头长度
                //var areawidth = 150;//区域宽度
                //var areaheight = 100;//区域高度
                //var personwidth = 30;//小人宽度
                //var personheight = 60;//小人高度
                //var commentfontsize = 12;//备注文字大小
#pragma warning restore 612,618

                var flagoverlay = true;//true向右，false向左
                //1.打印附属信息
                PrintComment(commentpoint, new SolidBrush(Color.Black), new Font("黑体", commentfontsize), _uid);
                var points = new List<System.Drawing.Point>();
                //2.以下构造坐标集
                foreach (var data in datas)
                {
                    points.Add(nowpoint);
                    if (flagoverlay) //向右
                    {
                        if ((nowpoint.X + areawidth * 2 + arrowlength) > rightline) //到最右边
                        {
                            nowpoint.Y = nowpoint.Y + areaheight + arrowlength;
                            flagoverlay = false;
                        }
                        else
                        {
                            nowpoint.X = nowpoint.X + areawidth + arrowlength;
                        }
                    }
                    else //向左
                    {
                        if ((nowpoint.X - arrowlength - areawidth) < 0) //到最左边
                        {
                            nowpoint.Y = nowpoint.Y + areaheight + arrowlength;
                            flagoverlay = true;
                        }
                        else
                        {
                            nowpoint.X = nowpoint.X - areawidth - arrowlength;
                        }
                    }
                }
                for (int i = 0; i < points.Count; i++)
                {
                    var path = "";
                    if (datas[i].areaname == area1)
                    {
                        path = ConfigurationManager.AppSettings["area1img"].ToString(CultureInfo.InvariantCulture);
                    }
                    else if (datas[i].areaname == area2)
                    {
                        path = ConfigurationManager.AppSettings["area2img"].ToString(CultureInfo.InvariantCulture);
                    }
                    else if (datas[i].areaname == area3)
                    {
                        path = ConfigurationManager.AppSettings["area3img"].ToString(CultureInfo.InvariantCulture);
                    }
                    else if (datas[i].areaname == area4)
                    {
                        path = ConfigurationManager.AppSettings["area4img"].ToString(CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        path = ConfigurationManager.AppSettings["areadefimg"].ToString(CultureInfo.InvariantCulture);
                    }
                    //3.画箭头
                    if (i != 0)
                    {
                        MakeArrowByCal(points[i - 1], points[i], areawidth, areaheight, new Pen(Color.Bisque, 20));
                    }
                    var littlepp = ConfigurationManager.AppSettings["littleperson"].ToString(CultureInfo.InvariantCulture);
                    //4.画区域
                    MakeArea(points[i], areawidth, areaheight, datas[i], path, littlepp, personwidth, personheight, new SolidBrush(Color.Black), new Font("Arial", 8));
                }

            }
            catch (Exception ex)
            {
                //timer1.Stop();
                //timer1.Enabled = false;
                System.Windows.Forms.MessageBox.Show(ex.Message, "绘图出错！", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                Log.CreateLog(ex);
            }

        }

        /// <summary>
        /// 4.构造区域
        /// </summary>
        /// <param name="p">图片左上角坐标</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="sd"></param>
        /// <param name="imgurl"></param>
        /// <param name="imgurl2">小人图片（设空则不贴）</param>
        /// <param name="width2">若无小人图留空</param>
        /// <param name="height2">若无小人图留空</param>
        /// <param name="wordbr">文字笔刷</param>
        /// <param name="wordft">文字格式</param>
        private void MakeArea(System.Drawing.Point p, int width, int height, SectionData sd, string imgurl, string imgurl2, int width2, int height2, Brush wordbr, Font wordft)
        {
            //以下微调
            p.Y -= 80;

            ////区域贴图
            //var newImage = System.Drawing.Image.FromFile(imgurl);
            //var r = new Rectangle(p.X, p.Y, width, height);//x,y,宽，高
            //g.DrawImage(newImage, r);

            if (imgurl2 != "")
            {
                var newImage2 = System.Drawing.Image.FromFile(imgurl2);
                var r2 = new Rectangle(p.X + 50, p.Y - 70, width2, height2);//x,y,宽，高
                g.DrawImage(newImage2, r2);//右上角小人贴图
            }
            //文字
            g.DrawString("区域名称:" + sd.areaname, wordft, wordbr, p.X + 10, p.Y - 25);//todo:广东项目新添加，区域名称
            g.DrawString("进入时间:" + sd.arrive, wordft, wordbr, p.X + 10, p.Y - 10);
            g.DrawString("退出时间:" + sd.leave, wordft, wordbr, p.X + 10, p.Y + 5);
            g.DrawString("停留时间:" + sd.stay, wordft, wordbr, p.X + 10, p.Y + 20);
        }

        /// <summary>
        /// 3.算法画箭头
        /// </summary>
        /// <param name="p1">上一图片左上角坐标</param>
        /// <param name="p2">下一图片左上角坐标</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片高度</param>
        /// <param name="pen">画笔</param>
        private void MakeArrowByCal(System.Drawing.Point p1, System.Drawing.Point p2, int width, int height, Pen pen)
        {
            //以下微调
            p1.Y -= 125;
            p2.Y -= 125;
            p1.X += 2;
            p2.X += 2;

            var pstart = new System.Drawing.Point();
            var pend = new System.Drawing.Point();
            if (p1.X == p2.X)//向下
            {
                pstart.X = p1.X + width / 2;
                pstart.Y = p1.Y + height;
                pend.X = p2.X + width / 2;
                pend.Y = p2.Y;
                //以下微调
                pend.Y -= 30;
            }
            else if (p1.X < p2.X)//向右
            {
                pstart.X = p1.X + width;
                pstart.Y = p1.Y + height / 2;
                pend.X = p2.X;
                pend.Y = p2.Y + height / 2;
            }
            else if (p1.X > p2.X)//向左
            {
                pstart.X = p1.X;
                pstart.Y = p1.Y + height / 2;
                pend.X = p2.X + width;
                pend.Y = p2.Y + height / 2;
            }
            //画箭头,只对不封闭曲线有用
            pen.DashStyle = DashStyle.Solid;
            pen.EndCap = LineCap.ArrowAnchor;
            g.DrawLine(pen, pstart, pend);//x1,y1,x2,y2
        }

        /// <summary>
        /// 加载下拉框
        /// </summary>
        private void LoadData()
        {
            try
            {
                using (var dt = DBHelper.GetDatable("select * from LOCATE_JKDX where DXXZ='医院人员' and RFID <> ''", ""))
                {
                    this.cmbname.Items.Clear();
                    //var collection = new ObservableCollection<LOCATE_JKDX>();
                    //if (dt != null)
                    //{
                    //    foreach (DataRow dr in dt.Rows)
                    //    {
                    //        var resource = new LOCATE_JKDX();
                    //        resource.DXBH = int.Parse(dr["DXBH"].ToString());
                    //        resource.GLBH = dr["GLBH"].ToString();
                    //        resource.DXGH = dr["DXGH"].ToString();
                    //        resource.DXXZ = dr["DXXZ"].ToString();
                    //        resource.DXMC = dr["DXMC"].ToString();
                    //        resource.DXLX = dr["DXLX"].ToString();
                    //        resource.KSBH = dr["KSBH"].ToString();
                    //        resource.KSMC = dr["KSMC"].ToString();
                    //        resource.DXJG = dr["DXJG"].ToString();
                    //        resource.CJDW = dr["CJDW"].ToString();
                    //        resource.BZXX = dr["BZXX"].ToString();
                    //        resource.LXFS = dr["LXFS"].ToString();
                    //        resource.RFID = dr["RFID"].ToString();
                    //        if (!string.IsNullOrWhiteSpace(dr["LKSJ"].ToString()))
                    //            resource.LKSJ = DateTime.Parse(dr["LKSJ"].ToString());
                    //        if (!string.IsNullOrWhiteSpace(dr["TKSJ"].ToString()))
                    //            resource.TKSJ = DateTime.Parse(dr["TKSJ"].ToString());
                    //        // 最后一次侦听到信号的时间
                    //        if (!string.IsNullOrWhiteSpace(dr["GXSJ"].ToString()))
                    //            resource.GXSJ = DateTime.Parse(dr["GXSJ"].ToString());
                    //        // 最后一次侦听到信号的位置
                    //        resource.TJQY = dr["TJQY"].ToString();
                    //        // 最后一次侦听到信号的AP编号
                    //        resource.APBH = dr["APBH"].ToString();
                    //        // 位置固定标志，0 表示非固定；1 表示固定
                    //        resource.GDBZ = int.Parse(dr["GDBZ"].ToString());
                    //        if (!string.IsNullOrWhiteSpace(dr["XPOS"].ToString()))
                    //            resource.XPOS = int.Parse(dr["XPOS"].ToString());
                    //        if (!string.IsNullOrWhiteSpace(dr["YPOS"].ToString()))
                    //            resource.YPOS = int.Parse(dr["YPOS"].ToString());
                    //        resource.JYKSBH = dr["JYKSBH"].ToString();
                    //        resource.JYKSMC = dr["JYKSMC"].ToString();
                    //        resource.JYBZ = int.Parse(dr["JYBZ"].ToString());
                    //        if (!string.IsNullOrWhiteSpace(dr["TKBZ"].ToString()))
                    //            resource.TKBZ = int.Parse(dr["TKBZ"].ToString());
                    //        resource.LKBZ = int.Parse(dr["LKBZ"].ToString());
                    //        resource.FLAG = int.Parse(dr["FLAG"].ToString());
                    //        resource.HJBZ = int.Parse(dr["HJBZ"].ToString());
                    //        resource.HJCS = int.Parse(dr["HJCS"].ToString());
                    //        if (!string.IsNullOrWhiteSpace(dr["TPLJ"].ToString()))
                    //        {
                    //            resource.TPLJ = dr["TPLJ"].ToString();
                    //        }
                    //        else
                    //        {
                    //            resource.TPLJ = "";
                    //        }
                    //        resource.Unit = dr["Unit"].ToString();
                    //        resource.Price = dr["Price"].ToString();
                    //        resource.Brand = dr["Brand"].ToString();
                    //        if (!string.IsNullOrWhiteSpace(dr["AcquiredDate"].ToString()))
                    //            resource.AcquiredDate = DateTime.Parse(dr["AcquiredDate"].ToString());
                    //        if (!string.IsNullOrWhiteSpace(dr["First_Aid"].ToString()))
                    //            resource.First_Aid = int.Parse(dr["First_Aid"].ToString());
                    //        resource.Usage = dr["Usage"].ToString();
                    //        try
                    //        {
                    //            resource.CaseID = dr["CaseID"].ToString();
                    //        }
                    //        catch
                    //        {
                    //        }
                    //        collection.Add(resource);
                    //    }
                    //}
                    //cmbname.ItemsSource = collection;
                    //cmbname.DisplayMemberPath = "DXMC";
                    //cmbname.SelectedValuePath = "DXBH";
                    //cmbname.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "加载下拉框出错！", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                Log.CreateLog(ex);
            }
        }

        /// <summary>
        /// 二、开始打印
        /// </summary>
        private void DoPrint()
        {
            try
            {
                //printDocument.PrinterSettings可以获取或设置计算机默认打印相关属性或参数，如：printDocument.PrinterSettings.PrinterName获得默认打印机打印机名称
                //printDocument.DefaultPageSettings   //可以获取或设置打印页面参数信息、如是纸张大小，是否横向打印等

                //设置文档名
                printDocument.DocumentName = "医惠物联网定位卡";//设置完后可在打印对话框及队列中显示（默认显示document）

                ////设置纸张大小（可以不设置取，取默认设置）
                //PaperSize ps = new PaperSize("A4", 100, 70);
                //ps.RawKind = 150; //如果是自定义纸张，就要大于118，（A4值为9，详细纸张类型与值的对照请看http://msdn.microsoft.com/zh-tw/library/system.drawing.printing.papersize.rawkind(v=vs.85).aspx）
                //printDocument.DefaultPageSettings.PaperSize = ps;

                //打印开始前
                //printDocument.BeginPrint += new PrintEventHandler(printDocument_BeginPrint);
                //打印输出（过程）
                printDocument.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                //打印结束
                //printDocument.EndPrint += new PrintEventHandler(printDocument_EndPrint);

                //跳出打印对话框，提供打印参数可视化设置，如选择哪个打印机打印此文档等
                System.Windows.Forms.PrintDialog pd = new System.Windows.Forms.PrintDialog();
                pd.Document = printDocument;
                if (DialogResult.OK == pd.ShowDialog()) //如果确认，将会覆盖所有的打印参数设置
                {
                    //页面设置对话框（可以不使用，其实PrintDialog对话框已提供页面设置）
                    PageSetupDialog psd = new PageSetupDialog();
                    psd.Document = printDocument;
                    if (DialogResult.OK == psd.ShowDialog())
                    {
                        //打印预览
                        PrintPreviewDialog ppd = new PrintPreviewDialog();
                        ppd.Document = printDocument;
                        ppd.WindowState = FormWindowState.Maximized;
                        ppd.PrintPreviewControl.Zoom = 1.0; 
                        if (DialogResult.OK == ppd.ShowDialog())
                            printDocument.Print();          //打印
                    }
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "打印出错！", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1);
                Log.CreateLog(ex);
            }
        }

        //打印内容的设置
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(image, 0, 0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoPrint();
        }

        private void cmbKS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbname.SelectedValue!=null)
            {
                Draw(cmbname.SelectedValue.ToString());

                var myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.StreamSource = new MemoryStream(ImgToByt(image));
                myBitmapImage.EndInit();
                printimg.Stretch = Stretch.Fill;
                printimg.Source = myBitmapImage;
            }
        }

        /// <summary>
        /// 图片转换成字节流
        /// </summary>
        /// <param name="img">要转换的Image对象</param>
        /// <returns>转换后返回的字节流</returns>
        public byte[] ImgToByt(System.Drawing.Image img)
        {
            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            imagedata = ms.GetBuffer();
            return imagedata;
        }
    }

    public static class DBHelper
    {

#pragma warning disable 612,618
        private static string strConn1 = ConfigurationSettings.AppSettings["SqlConnection"].ToString(CultureInfo.InvariantCulture);
#pragma warning restore 612,618

        /// <summary>
        /// 数据库公共查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="_strconn"></param>
        /// <returns></returns>
        public static DataTable GetDatable(string sql, string _strconn)
        {
            if (_strconn == "")
            {
                _strconn = strConn1;
            }
            using (var ds = new DataSet())
            {
                using (var conn = new SqlConnection(_strconn))
                {
                    conn.Open();
                    using (var da = new SqlDataAdapter(sql, conn))
                    {
                        da.Fill(ds);
                        return ds.Tables[0];
                    }
                }
            }
        }
    }

    public class Log
    {
        /// <summary>
        /// 创建日志文件
        /// </summary>
        /// <param name="ex">异常类</param>
        public static void CreateLog(Exception ex)
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\log";
            if (!Directory.Exists(path))
            {
                //创建日志文件夹
                Directory.CreateDirectory(path);
            }
            //发生异常每天都创建一个单独的日子文件[*.log],每天的错误信息都在这一个文件里。方便查找
            path += "\\" + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + ".log";
            if (File.Exists(path))
            {
                WriteLogInfo(ex, path);
            }
            else
            {
                FileStream myFs = new FileStream(path, FileMode.Create);
                StreamWriter mySw = new StreamWriter(myFs);
                mySw.Write("");
                mySw.Close();
                myFs.Close();
                WriteLogInfo(ex, path);
            }
        }
        /// <summary>
        /// 写日志信息
        /// </summary>
        /// <param name="ex">异常类</param>
        /// <param name="path">日志文件存放路径</param>
        private static void WriteLogInfo(Exception ex, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, true, Encoding.Default))
            {
                sw.WriteLine("*****************************************【"
                            + DateTime.Now.ToLongTimeString()
                            + "】*****************************************");
                if (ex != null)
                {
                    sw.WriteLine("【ErrorType】" + ex.GetType());
                    sw.WriteLine("【TargetSite】" + ex.TargetSite);
                    sw.WriteLine("【Message】" + ex.Message);
                    sw.WriteLine("【Source】" + ex.Source);
                    sw.WriteLine("【StackTrace】" + ex.StackTrace);
                }
                else
                {
                    sw.WriteLine("Exception is NULL");
                }
                sw.WriteLine();
            }
        }
    }
}
