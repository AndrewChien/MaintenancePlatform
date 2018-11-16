using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Markup;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using ZNC.Component;

namespace MaintenancePlatform.Print
{
    /// <summary>
    /// Interaction logic for PrintWindow.xaml
    /// </summary>
    public partial class PrintWindow : Window
    {
        public PrintWindow()
        {
            this.InitializeComponent();

            // Insert code required on object creation below this point.
        }

        //传递一个公共的数据类
        public string fixedDocFile;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FileHelper.LoadDocumentViewer(fixedDocFile, docViewer);
        }
    }
}
