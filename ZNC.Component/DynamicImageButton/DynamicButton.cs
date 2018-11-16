using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace ZNC.Component.DynamicImageButton
{
  public class DynamicButton : ButtonBase
  {
    static DynamicButton()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(DynamicButton), new FrameworkPropertyMetadata(typeof(DynamicButton)));
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
        DependencyProperty.Register("IconImageUri", typeof(string), typeof(DynamicButton), new UIPropertyMetadata(string.Empty,
          (o, e) =>
          {
            try
            {
              Uri uriSource = new Uri((string)e.NewValue, UriKind.RelativeOrAbsolute);
              if (uriSource != null)
              {
                DynamicButton button = o as DynamicButton;
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
        DependencyProperty.Register("IconImage", typeof(BitmapImage), typeof(DynamicButton), new UIPropertyMetadata(null));
  }
}
