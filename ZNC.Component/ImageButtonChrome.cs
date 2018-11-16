using System;
using System.Runtime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ZNC.Component
{
    public sealed class ImageButtonChrome : Decorator
    {
        private class LocalResources
        {
            public Pen BorderOverlayPen;
            public Pen InnerBorderPen;
            public LinearGradientBrush BackgroundOverlay;
            public LinearGradientBrush LeftDropShadowBrush;
            public LinearGradientBrush TopDropShadowBrush;
        }
        public static readonly DependencyProperty BackgroundProperty;
        public static readonly DependencyProperty BorderBrushProperty;
        public static readonly DependencyProperty RenderDefaultedProperty;
        public static readonly DependencyProperty RenderMouseOverProperty;
        public static readonly DependencyProperty RenderPressedProperty;
        public static readonly DependencyProperty RoundCornersProperty;
        private static Pen _commonBorderPen;
        private static Pen _commonInnerBorderPen;
        private static Pen _commonDisabledBorderOverlay;
        private static SolidColorBrush _commonDisabledBackgroundOverlay;
        private static Pen _commonDefaultedInnerBorderPen;
        private static LinearGradientBrush _commonHoverBackgroundOverlay;
        private static Pen _commonHoverBorderOverlay;
        private static LinearGradientBrush _commonPressedBackgroundOverlay;
        private static Pen _commonPressedBorderOverlay;
        private static LinearGradientBrush _commonPressedLeftDropShadowBrush;
        private static LinearGradientBrush _commonPressedTopDropShadowBrush;
        private static object _resourceAccess;
        private ImageButtonChrome.LocalResources _localResources;
        public Brush Background
        {
            get
            {
                return (Brush)base.GetValue(ImageButtonChrome.BackgroundProperty);
            }
            set
            {
                base.SetValue(ImageButtonChrome.BackgroundProperty, value);
            }
        }
        public Brush BorderBrush
        {
            get
            {
                return (Brush)base.GetValue(ImageButtonChrome.BorderBrushProperty);
            }
            set
            {
                base.SetValue(ImageButtonChrome.BorderBrushProperty, value);
            }
        }
        public bool RenderDefaulted
        {
            get
            {
                return (bool)base.GetValue(ImageButtonChrome.RenderDefaultedProperty);
            }
            set
            {
                base.SetValue(ImageButtonChrome.RenderDefaultedProperty, value);
            }
        }
        public bool RenderMouseOver
        {
            get
            {
                return (bool)base.GetValue(ImageButtonChrome.RenderMouseOverProperty);
            }
            set
            {
                base.SetValue(ImageButtonChrome.RenderMouseOverProperty, value);
            }
        }
        public bool RenderPressed
        {
            get
            {
                return (bool)base.GetValue(ImageButtonChrome.RenderPressedProperty);
            }
            set
            {
                base.SetValue(ImageButtonChrome.RenderPressedProperty, value);
            }
        }
        public bool RoundCorners
        {
            get
            {
                return (bool)base.GetValue(ImageButtonChrome.RoundCornersProperty);
            }
            set
            {
                base.SetValue(ImageButtonChrome.RoundCornersProperty, value);
            }
        }
        internal int EffectiveValuesInitialSize
        {
            get
            {
                return 9;
            }
        }
        private bool Animates
        {
            get
            {
                return SystemParameters.PowerLineStatus == PowerLineStatus.Online && SystemParameters.ClientAreaAnimation && RenderCapability.Tier > 0 && base.IsEnabled;
            }
        }
        private static LinearGradientBrush CommonHoverBackgroundOverlay
        {
            get
            {
                if (ImageButtonChrome._commonHoverBackgroundOverlay == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonHoverBackgroundOverlay == null)
                        {
                            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                            linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                            linearGradientBrush.EndPoint = new Point(0.0, 1.0);
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 234, 246, 253), 0.0));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 217, 240, 252), 0.5));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 190, 230, 253), 0.5));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 167, 217, 245), 1.0));
                            linearGradientBrush.Freeze();
                            ImageButtonChrome._commonHoverBackgroundOverlay = linearGradientBrush;
                        }
                    }
                }
                return ImageButtonChrome._commonHoverBackgroundOverlay;
            }
        }
        private static LinearGradientBrush CommonPressedBackgroundOverlay
        {
            get
            {
                if (ImageButtonChrome._commonPressedBackgroundOverlay == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonPressedBackgroundOverlay == null)
                        {
                            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                            linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                            linearGradientBrush.EndPoint = new Point(0.0, 1.0);
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 194, 228, 246), 0.5));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 171, 218, 243), 0.5));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 144, 203, 235), 1.0));
                            linearGradientBrush.Freeze();
                            ImageButtonChrome._commonPressedBackgroundOverlay = linearGradientBrush;
                        }
                    }
                }
                return ImageButtonChrome._commonPressedBackgroundOverlay;
            }
        }
        private static SolidColorBrush CommonDisabledBackgroundOverlay
        {
            get
            {
                if (ImageButtonChrome._commonDisabledBackgroundOverlay == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonDisabledBackgroundOverlay == null)
                        {
                            SolidColorBrush solidColorBrush = new SolidColorBrush(Color.FromRgb(244, 244, 244));
                            solidColorBrush.Freeze();
                            ImageButtonChrome._commonDisabledBackgroundOverlay = solidColorBrush;
                        }
                    }
                }
                return ImageButtonChrome._commonDisabledBackgroundOverlay;
            }
        }
        private Brush BackgroundOverlay
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return Brushes.Transparent;
                    //return ImageButtonChrome.CommonDisabledBackgroundOverlay;
                }
                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return ImageButtonChrome.CommonPressedBackgroundOverlay;
                    }
                    if (this.RenderMouseOver)
                    {
                        return ImageButtonChrome.CommonHoverBackgroundOverlay;
                    }
                    return null;
                }
                else
                {
                    if (this._localResources != null)
                    {
                        if (this._localResources.BackgroundOverlay == null)
                        {
                            this._localResources.BackgroundOverlay = ImageButtonChrome.CommonHoverBackgroundOverlay.Clone();
                            this._localResources.BackgroundOverlay.Opacity = 0.0;
                        }
                        return this._localResources.BackgroundOverlay;
                    }
                    return null;
                }
            }
        }
        private static Pen CommonHoverBorderOverlay
        {
            get
            {
                if (ImageButtonChrome._commonHoverBorderOverlay == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonHoverBorderOverlay == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(60, 127, 177));
                            pen.Freeze();
                            ImageButtonChrome._commonHoverBorderOverlay = pen;
                        }
                    }
                }
                return ImageButtonChrome._commonHoverBorderOverlay;
            }
        }
        private static Pen CommonPressedBorderOverlay
        {
            get
            {
                if (ImageButtonChrome._commonPressedBorderOverlay == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonPressedBorderOverlay == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(44, 98, 139));
                            pen.Freeze();
                            ImageButtonChrome._commonPressedBorderOverlay = pen;
                        }
                    }
                }
                return ImageButtonChrome._commonPressedBorderOverlay;
            }
        }
        private static Pen CommonDisabledBorderOverlay
        {
            get
            {
                if (ImageButtonChrome._commonDisabledBorderOverlay == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonDisabledBorderOverlay == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromRgb(173, 178, 181));
                            pen.Freeze();
                            ImageButtonChrome._commonDisabledBorderOverlay = pen;
                        }
                    }
                }
                return ImageButtonChrome._commonDisabledBorderOverlay;
            }
        }
        private Pen BorderOverlayPen
        {
            get
            {
                if (!base.IsEnabled)
                {
                    if (this.RoundCorners)
                    {
                        return ImageButtonChrome.CommonDisabledBorderOverlay;
                    }
                    return null;
                }
                else
                {
                    if (!this.Animates)
                    {
                        if (this.RenderPressed)
                        {
                            return ImageButtonChrome.CommonPressedBorderOverlay;
                        }
                        if (this.RenderMouseOver)
                        {
                            return ImageButtonChrome.CommonHoverBorderOverlay;
                        }
                        return null;
                    }
                    else
                    {
                        if (this._localResources != null)
                        {
                            if (this._localResources.BorderOverlayPen == null)
                            {
                                this._localResources.BorderOverlayPen = ImageButtonChrome.CommonHoverBorderOverlay.Clone();
                                this._localResources.BorderOverlayPen.Brush.Opacity = 0.0;
                            }
                            return this._localResources.BorderOverlayPen;
                        }
                        return null;
                    }
                }
            }
        }
        private static Pen CommonInnerBorderPen
        {
            get
            {
                if (ImageButtonChrome._commonInnerBorderPen == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonInnerBorderPen == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new LinearGradientBrush
                            {
                                StartPoint = new Point(0.0, 0.0),
                                EndPoint = new Point(0.0, 1.0),
                                GradientStops = 
								{
									new GradientStop(Color.FromArgb(250, 255, 255, 255), 0.0),
									new GradientStop(Color.FromArgb(133, 255, 255, 255), 1.0)
								}
                            };
                            pen.Freeze();
                            ImageButtonChrome._commonInnerBorderPen = pen;
                        }
                    }
                }
                return ImageButtonChrome._commonInnerBorderPen;
            }
        }
        private static Pen CommonDefaultedInnerBorderPen
        {
            get
            {
                if (ImageButtonChrome._commonDefaultedInnerBorderPen == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonDefaultedInnerBorderPen == null)
                        {
                            Pen pen = new Pen();
                            pen.Thickness = 1.0;
                            pen.Brush = new SolidColorBrush(Color.FromArgb(249, 0, 204, 255));
                            pen.Freeze();
                            ImageButtonChrome._commonDefaultedInnerBorderPen = pen;
                        }
                    }
                }
                return ImageButtonChrome._commonDefaultedInnerBorderPen;
            }
        }
        private Pen InnerBorderPen
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return ImageButtonChrome.CommonInnerBorderPen;
                }
                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return null;
                    }
                    if (this.RenderDefaulted)
                    {
                        return ImageButtonChrome.CommonDefaultedInnerBorderPen;
                    }
                    return ImageButtonChrome.CommonInnerBorderPen;
                }
                else
                {
                    if (this._localResources != null)
                    {
                        if (this._localResources.InnerBorderPen == null)
                        {
                            this._localResources.InnerBorderPen = ImageButtonChrome.CommonInnerBorderPen.Clone();
                        }
                        return this._localResources.InnerBorderPen;
                    }
                    return ImageButtonChrome.CommonInnerBorderPen;
                }
            }
        }
        private static LinearGradientBrush CommonPressedLeftDropShadowBrush
        {
            get
            {
                if (ImageButtonChrome._commonPressedLeftDropShadowBrush == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonPressedLeftDropShadowBrush == null)
                        {
                            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                            linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                            linearGradientBrush.EndPoint = new Point(1.0, 0.0);
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(128, 51, 51, 51), 0.0));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0, 51, 51, 51), 1.0));
                            linearGradientBrush.Freeze();
                            ImageButtonChrome._commonPressedLeftDropShadowBrush = linearGradientBrush;
                        }
                    }
                }
                return ImageButtonChrome._commonPressedLeftDropShadowBrush;
            }
        }
        private LinearGradientBrush LeftDropShadowBrush
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return null;
                }
                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return ImageButtonChrome.CommonPressedLeftDropShadowBrush;
                    }
                    return null;
                }
                else
                {
                    if (this._localResources != null)
                    {
                        if (this._localResources.LeftDropShadowBrush == null)
                        {
                            this._localResources.LeftDropShadowBrush = ImageButtonChrome.CommonPressedLeftDropShadowBrush.Clone();
                            this._localResources.LeftDropShadowBrush.Opacity = 0.0;
                        }
                        return this._localResources.LeftDropShadowBrush;
                    }
                    return null;
                }
            }
        }
        private static LinearGradientBrush CommonPressedTopDropShadowBrush
        {
            get
            {
                if (ImageButtonChrome._commonPressedTopDropShadowBrush == null)
                {
                    lock (ImageButtonChrome._resourceAccess)
                    {
                        if (ImageButtonChrome._commonPressedTopDropShadowBrush == null)
                        {
                            LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
                            linearGradientBrush.StartPoint = new Point(0.0, 0.0);
                            linearGradientBrush.EndPoint = new Point(0.0, 1.0);
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(128, 51, 51, 51), 0.0));
                            linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(0, 51, 51, 51), 1.0));
                            linearGradientBrush.Freeze();
                            ImageButtonChrome._commonPressedTopDropShadowBrush = linearGradientBrush;
                        }
                    }
                }
                return ImageButtonChrome._commonPressedTopDropShadowBrush;
            }
        }
        private LinearGradientBrush TopDropShadowBrush
        {
            get
            {
                if (!base.IsEnabled)
                {
                    return null;
                }
                if (!this.Animates)
                {
                    if (this.RenderPressed)
                    {
                        return ImageButtonChrome.CommonPressedTopDropShadowBrush;
                    }
                    return null;
                }
                else
                {
                    if (this._localResources != null)
                    {
                        if (this._localResources.TopDropShadowBrush == null)
                        {
                            this._localResources.TopDropShadowBrush = ImageButtonChrome.CommonPressedTopDropShadowBrush.Clone();
                            this._localResources.TopDropShadowBrush.Opacity = 0.0;
                        }
                        return this._localResources.TopDropShadowBrush;
                    }
                    return null;
                }
            }
        }
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public ImageButtonChrome()
        {
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            UIElement child = this.Child;
            Size desiredSize;
            if (child != null)
            {
                Size availableSize2 = default(Size);
                bool flag = availableSize.Width < 4.0;
                bool flag2 = availableSize.Height < 4.0;
                if (!flag)
                {
                    availableSize2.Width = availableSize.Width - 4.0;
                }
                if (!flag2)
                {
                    availableSize2.Height = availableSize.Height - 4.0;
                }
                child.Measure(availableSize2);
                desiredSize = child.DesiredSize;
                if (!flag)
                {
                    desiredSize.Width += 4.0;
                }
                if (!flag2)
                {
                    desiredSize.Height += 4.0;
                }
            }
            else
            {
                desiredSize = new Size(Math.Min(4.0, availableSize.Width), Math.Min(4.0, availableSize.Height));
            }
            return desiredSize;
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            Rect finalRect = default(Rect);
            finalRect.Width = Math.Max(0.0, finalSize.Width - 4.0);
            finalRect.Height = Math.Max(0.0, finalSize.Height - 4.0);
            finalRect.X = (finalSize.Width - finalRect.Width) * 0.5;
            finalRect.Y = (finalSize.Height - finalRect.Height) * 0.5;
            UIElement child = this.Child;
            if (child != null)
            {
                child.Arrange(finalRect);
            }
            return finalSize;
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            Rect rect = new Rect(0.0, 0.0, base.ActualWidth, base.ActualHeight);
            this.DrawBackground(drawingContext, ref rect);
            this.DrawDropShadows(drawingContext, ref rect);
            if (IsEnabled)
            {
                this.DrawBorder(drawingContext, ref rect);
            }
            this.DrawInnerBorder(drawingContext, ref rect);
        }
        static ImageButtonChrome()
        {
            ImageButtonChrome.BackgroundProperty = Control.BackgroundProperty.AddOwner(typeof(ImageButtonChrome), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            ImageButtonChrome.BorderBrushProperty = Border.BorderBrushProperty.AddOwner(typeof(ImageButtonChrome), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));
            ImageButtonChrome.RenderDefaultedProperty = DependencyProperty.Register("RenderDefaulted", typeof(bool), typeof(ImageButtonChrome), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ImageButtonChrome.OnRenderDefaultedChanged)));
            ImageButtonChrome.RenderMouseOverProperty = DependencyProperty.Register("RenderMouseOver", typeof(bool), typeof(ImageButtonChrome), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ImageButtonChrome.OnRenderMouseOverChanged)));
            ImageButtonChrome.RenderPressedProperty = DependencyProperty.Register("RenderPressed", typeof(bool), typeof(ImageButtonChrome), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(ImageButtonChrome.OnRenderPressedChanged)));
            ImageButtonChrome.RoundCornersProperty = DependencyProperty.Register("RoundCorners", typeof(bool), typeof(ImageButtonChrome), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
            ImageButtonChrome._resourceAccess = new object();
            UIElement.IsEnabledProperty.OverrideMetadata(typeof(ImageButtonChrome), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));
        }
        private static void OnRenderDefaultedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ImageButtonChrome buttonChrome = (ImageButtonChrome)o;
            if (buttonChrome.Animates)
            {
                if (!buttonChrome.RenderPressed)
                {
                    if ((bool)e.NewValue)
                    {
                        if (buttonChrome._localResources == null)
                        {
                            buttonChrome._localResources = new ImageButtonChrome.LocalResources();
                            buttonChrome.InvalidateVisual();
                        }
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.3));
                        ColorAnimation animation = new ColorAnimation(Color.FromArgb(249, 0, 204, 255), duration);
                        GradientStopCollection gradientStops = ((LinearGradientBrush)buttonChrome.InnerBorderPen.Brush).GradientStops;
                        gradientStops[0].BeginAnimation(GradientStop.ColorProperty, animation);
                        gradientStops[1].BeginAnimation(GradientStop.ColorProperty, animation);
                        DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, TimeSpan.FromSeconds(0.5)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteDoubleKeyFrame(1.0, TimeSpan.FromSeconds(0.75)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(0.0, TimeSpan.FromSeconds(2.0)));
                        doubleAnimationUsingKeyFrames.RepeatBehavior = RepeatBehavior.Forever;
                        Timeline.SetDesiredFrameRate(doubleAnimationUsingKeyFrames, new int?(10));
                        buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, doubleAnimationUsingKeyFrames);
                        buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimationUsingKeyFrames);
                        return;
                    }
                    if (buttonChrome._localResources == null)
                    {
                        buttonChrome.InvalidateVisual();
                        return;
                    }
                    Duration duration2 = new Duration(TimeSpan.FromSeconds(0.2));
                    DoubleAnimation doubleAnimation = new DoubleAnimation();
                    doubleAnimation.Duration = duration2;
                    buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                    buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                    ColorAnimation colorAnimation = new ColorAnimation();
                    colorAnimation.Duration = duration2;
                    GradientStopCollection gradientStops2 = ((LinearGradientBrush)buttonChrome.InnerBorderPen.Brush).GradientStops;
                    gradientStops2[0].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
                    gradientStops2[1].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
                    return;
                }
            }
            else
            {
                buttonChrome._localResources = null;
                buttonChrome.InvalidateVisual();
            }
        }
        private static void OnRenderMouseOverChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ImageButtonChrome buttonChrome = (ImageButtonChrome)o;
            if (buttonChrome.Animates)
            {
                if (!buttonChrome.RenderPressed)
                {
                    if ((bool)e.NewValue)
                    {
                        if (buttonChrome._localResources == null)
                        {
                            buttonChrome._localResources = new ImageButtonChrome.LocalResources();
                            buttonChrome.InvalidateVisual();
                        }
                        Duration duration = new Duration(TimeSpan.FromSeconds(0.3));
                        DoubleAnimation animation = new DoubleAnimation(1.0, duration);
                        buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                        buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation);
                        return;
                    }
                    if (buttonChrome._localResources == null)
                    {
                        buttonChrome.InvalidateVisual();
                        return;
                    }
                    if (buttonChrome.RenderDefaulted)
                    {
                        double opacity = buttonChrome.BackgroundOverlay.Opacity;
                        double num = (1.0 - opacity) * 0.5;
                        DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(1.0, TimeSpan.FromSeconds(num)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new DiscreteDoubleKeyFrame(1.0, TimeSpan.FromSeconds(num + 0.25)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(0.0, TimeSpan.FromSeconds(num + 1.5)));
                        doubleAnimationUsingKeyFrames.KeyFrames.Add(new LinearDoubleKeyFrame(opacity, TimeSpan.FromSeconds(2.0)));
                        doubleAnimationUsingKeyFrames.RepeatBehavior = RepeatBehavior.Forever;
                        Timeline.SetDesiredFrameRate(doubleAnimationUsingKeyFrames, new int?(10));
                        buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, doubleAnimationUsingKeyFrames);
                        buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimationUsingKeyFrames);
                        return;
                    }
                    Duration duration2 = new Duration(TimeSpan.FromSeconds(0.2));
                    DoubleAnimation doubleAnimation = new DoubleAnimation();
                    doubleAnimation.Duration = duration2;
                    buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                    buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                    return;
                }
            }
            else
            {
                buttonChrome._localResources = null;
                buttonChrome.InvalidateVisual();
            }
        }
        private static void OnRenderPressedChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ImageButtonChrome buttonChrome = (ImageButtonChrome)o;
            if (!buttonChrome.Animates)
            {
                buttonChrome._localResources = null;
                buttonChrome.InvalidateVisual();
                return;
            }
            if ((bool)e.NewValue)
            {
                if (buttonChrome._localResources == null)
                {
                    buttonChrome._localResources = new ImageButtonChrome.LocalResources();
                    buttonChrome.InvalidateVisual();
                }
                Duration duration = new Duration(TimeSpan.FromSeconds(0.1));
                DoubleAnimation animation = new DoubleAnimation(1.0, duration);
                buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, animation);
                buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                buttonChrome.LeftDropShadowBrush.BeginAnimation(Brush.OpacityProperty, animation);
                buttonChrome.TopDropShadowBrush.BeginAnimation(Brush.OpacityProperty, animation);
                animation = new DoubleAnimation(0.0, duration);
                buttonChrome.InnerBorderPen.Brush.BeginAnimation(Brush.OpacityProperty, animation);
                ColorAnimation animation2 = new ColorAnimation(Color.FromRgb(194, 228, 246), duration);
                GradientStopCollection gradientStops = ((LinearGradientBrush)buttonChrome.BackgroundOverlay).GradientStops;
                gradientStops[0].BeginAnimation(GradientStop.ColorProperty, animation2);
                gradientStops[1].BeginAnimation(GradientStop.ColorProperty, animation2);
                animation2 = new ColorAnimation(Color.FromRgb(171, 218, 243), duration);
                gradientStops[2].BeginAnimation(GradientStop.ColorProperty, animation2);
                animation2 = new ColorAnimation(Color.FromRgb(144, 203, 235), duration);
                gradientStops[3].BeginAnimation(GradientStop.ColorProperty, animation2);
                animation2 = new ColorAnimation(Color.FromRgb(44, 98, 139), duration);
                buttonChrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.ColorProperty, animation2);
                return;
            }
            if (buttonChrome._localResources == null)
            {
                buttonChrome.InvalidateVisual();
                return;
            }
            bool renderMouseOver = buttonChrome.RenderMouseOver;
            Duration duration2 = new Duration(TimeSpan.FromSeconds(0.1));
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = duration2;
            buttonChrome.LeftDropShadowBrush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
            buttonChrome.TopDropShadowBrush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
            buttonChrome.InnerBorderPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
            if (!renderMouseOver)
            {
                buttonChrome.BorderOverlayPen.Brush.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
                buttonChrome.BackgroundOverlay.BeginAnimation(Brush.OpacityProperty, doubleAnimation);
            }
            ColorAnimation colorAnimation = new ColorAnimation();
            colorAnimation.Duration = duration2;
            buttonChrome.BorderOverlayPen.Brush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            GradientStopCollection gradientStops2 = ((LinearGradientBrush)buttonChrome.BackgroundOverlay).GradientStops;
            gradientStops2[0].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
            gradientStops2[1].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
            gradientStops2[2].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
            gradientStops2[3].BeginAnimation(GradientStop.ColorProperty, colorAnimation);
        }
        private void DrawBackground(DrawingContext dc, ref Rect bounds)
        {
            if (!base.IsEnabled && !this.RoundCorners)
            {
                return;
            }
            Brush brush = this.Background;
            if (bounds.Width > 4.0 && bounds.Height > 4.0)
            {
                Rect rectangle = new Rect(bounds.Left + 1.0, bounds.Top + 1.0, bounds.Width - 2.0, bounds.Height - 2.0);
                if (brush != null)
                {
                    dc.DrawRectangle(brush, null, rectangle);
                }
                brush = this.BackgroundOverlay;
                if (brush != null)
                {
                    dc.DrawRectangle(brush, null, rectangle);
                }
            }
        }
        private void DrawDropShadows(DrawingContext dc, ref Rect bounds)
        {
            if (bounds.Width > 4.0 && bounds.Height > 4.0)
            {
                Brush leftDropShadowBrush = this.LeftDropShadowBrush;
                if (leftDropShadowBrush != null)
                {
                    dc.DrawRectangle(leftDropShadowBrush, null, new Rect(1.0, 1.0, 2.0, bounds.Bottom - 2.0));
                }
                Brush topDropShadowBrush = this.TopDropShadowBrush;
                if (topDropShadowBrush != null)
                {
                    dc.DrawRectangle(topDropShadowBrush, null, new Rect(1.0, 1.0, bounds.Right - 2.0, 2.0));
                }
            }
        }
        private void DrawBorder(DrawingContext dc, ref Rect bounds)
        {
            if (bounds.Width >= 5.0 && bounds.Height >= 5.0)
            {
                Brush brush = this.BorderBrush;
                Pen pen = null;
                if (brush != null)
                {
                    if (ImageButtonChrome._commonBorderPen == null)
                    {
                        lock (ImageButtonChrome._resourceAccess)
                        {
                            if (ImageButtonChrome._commonBorderPen == null)
                            {
                                if (!brush.IsFrozen && brush.CanFreeze)
                                {
                                    brush = brush.Clone();
                                    brush.Freeze();
                                }
                                Pen pen2 = new Pen(brush, 1.0);
                                if (pen2.CanFreeze)
                                {
                                    pen2.Freeze();
                                    ImageButtonChrome._commonBorderPen = pen2;
                                }
                            }
                        }
                    }
                    if (ImageButtonChrome._commonBorderPen != null && brush == ImageButtonChrome._commonBorderPen.Brush)
                    {
                        pen = ImageButtonChrome._commonBorderPen;
                    }
                    else
                    {
                        if (!brush.IsFrozen && brush.CanFreeze)
                        {
                            brush = brush.Clone();
                            brush.Freeze();
                        }
                        pen = new Pen(brush, 1.0);
                        if (pen.CanFreeze)
                        {
                            pen.Freeze();
                        }
                    }
                }
                Pen borderOverlayPen = this.BorderOverlayPen;
                if (pen != null || borderOverlayPen != null)
                {
                    if (this.RoundCorners)
                    {
                        Rect rectangle = new Rect(bounds.Left + 0.5, bounds.Top + 0.5, bounds.Width - 1.0, bounds.Height - 1.0);
                        if (base.IsEnabled && pen != null)
                        {
                            dc.DrawRoundedRectangle(null, pen, rectangle, 2.75, 2.75);
                        }
                        if (borderOverlayPen != null)
                        {
                            dc.DrawRoundedRectangle(null, borderOverlayPen, rectangle, 2.75, 2.75);
                            return;
                        }
                    }
                    else
                    {
                        PathFigure pathFigure = new PathFigure();
                        pathFigure.StartPoint = new Point(0.5, 0.5);
                        pathFigure.Segments.Add(new LineSegment(new Point(0.5, bounds.Bottom - 0.5), true));
                        pathFigure.Segments.Add(new LineSegment(new Point(bounds.Right - 2.5, bounds.Bottom - 0.5), true));
                        pathFigure.Segments.Add(new ArcSegment(new Point(bounds.Right - 0.5, bounds.Bottom - 2.5), new Size(2.0, 2.0), 0.0, false, SweepDirection.Counterclockwise, true));
                        pathFigure.Segments.Add(new LineSegment(new Point(bounds.Right - 0.5, bounds.Top + 2.5), true));
                        pathFigure.Segments.Add(new ArcSegment(new Point(bounds.Right - 2.5, bounds.Top + 0.5), new Size(2.0, 2.0), 0.0, false, SweepDirection.Counterclockwise, true));
                        pathFigure.IsClosed = true;
                        PathGeometry pathGeometry = new PathGeometry();
                        pathGeometry.Figures.Add(pathFigure);
                        if (base.IsEnabled && pen != null)
                        {
                            dc.DrawGeometry(null, pen, pathGeometry);
                        }
                        if (borderOverlayPen != null)
                        {
                            dc.DrawGeometry(null, borderOverlayPen, pathGeometry);
                        }
                    }
                }
            }
        }
        private void DrawInnerBorder(DrawingContext dc, ref Rect bounds)
        {
            if (!base.IsEnabled && !this.RoundCorners)
            {
                return;
            }
            if (bounds.Width >= 4.0 && bounds.Height >= 4.0)
            {
                Pen innerBorderPen = this.InnerBorderPen;
                if (innerBorderPen != null)
                {
                    dc.DrawRoundedRectangle(null, innerBorderPen, new Rect(bounds.Left + 1.5, bounds.Top + 1.5, bounds.Width - 3.0, bounds.Height - 3.0), 1.75, 1.75);
                }
            }
        }
    }

}
