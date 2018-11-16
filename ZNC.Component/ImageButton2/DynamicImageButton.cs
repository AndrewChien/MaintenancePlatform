using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace ZNC.Component.ImageButton2
{
    public class DynamicImageButton : ButtonBase
    {
        static DynamicImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicImageButton), new FrameworkPropertyMetadata(typeof(DynamicImageButton)));
        }

        public string IconImageUri
        {
            get { return (string)GetValue(IconImageUriProperty); }
            set
            {
                SetValue(IconImageUriProperty, value);
            }
        }
        public static readonly DependencyProperty IconImageUriProperty =
            DependencyProperty.Register("IconImageUri", typeof(string), typeof(DynamicImageButton), new UIPropertyMetadata(string.Empty,
              (o, e) =>
              {
                  try
                  {
                      Uri uriSource = new Uri((string)e.NewValue, UriKind.RelativeOrAbsolute);
                      if (uriSource != null)
                      {
                          DynamicImageButton button = o as DynamicImageButton;
                          BitmapImage img = new BitmapImage(uriSource);
                          button.SetValue(IconImageProperty, img);
                      }
                  }
                  catch (Exception ex)
                  {
                      throw ex;
                  }
              }));

        public BitmapImage IconImage
        {
            get { return (BitmapImage)GetValue(IconImageProperty); }
            set { SetValue(IconImageProperty, value); }
        }

        public static readonly DependencyProperty IconImageProperty =
            DependencyProperty.Register("IconImage", typeof(BitmapImage), typeof(DynamicImageButton), new UIPropertyMetadata(null));
    }
}
