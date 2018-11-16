using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MaintenancePlatform.Converters
{
    /// <summary>
    /// 将资源文件地址或资源名称转换为ImageSource, 可将其绑定到Image控件的Source属性.
    /// </summary>
    /// <example>
    /// EX1. 资源名称:
    /// 定义资源
    /// <code>&lt;BitmapImage x:Key="menu1" UriSource="../image/meum_01.png"&gt;&lt;/BitmapImage&gt;</code>
    /// <code>&lt;telerik:RadPanelBarItem Style="{DynamicResource radPanelBarInterLockStyle}" Header="报警管理" DefaultImageSrc="menu1"&gt;</code>
    /// EX2. 资源文件:
    /// <code>&lt;telerik:RadPanelBarItem Style="{DynamicResource radPanelBarInterLockStyle}" Header="报警管理" DefaultImageSrc="menu_01.png"&gt;</code>
    /// 调用Converter如下:
    /// <code>&lt;Image Source="{Binding DefaultImageSrc, RelativeSource={RelativeSource Mode=TemplatedParent}, Converter={StaticResource imagesourceConverter}}" Stretch="Fill" /&gt;</code>
    /// </example>
    public class ImageSourceConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            string fileName = value.ToString();
            //return fileName;
            if (App.Current.Resources.Contains(fileName))
            {
                object temp = App.Current.FindResource(fileName);
                if (temp != null)
                {
                    return temp;
                }
            }

            return GetGlowingImage(fileName);
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Gets the glowing image.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ImageSource GetGlowingImage(string name)
        {
            BitmapImage glowIcon = new BitmapImage();
            glowIcon.CacheOption = BitmapCacheOption.OnLoad;
            glowIcon.BeginInit();
            //glowIcon.UriSource = new Uri("pack://application:,,,/EOLMS;component/Resources/" + name);
            glowIcon.UriSource = new Uri("pack://siteoforigin:,,,/image/" + name ); 
            glowIcon.EndInit();
            glowIcon.Freeze();
            return glowIcon;
        }
    }
}
