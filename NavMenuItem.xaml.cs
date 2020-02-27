using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HamburgerMenu.Calcs;

namespace HamburgerMenu
{
    /// <summary>
    /// Interaction logic for NavMenuItem.xaml
    /// </summary>
    public partial class NavMenuItem : UserControl
    {
        public NavMenuItem()
        {
            InitializeComponent();
            OnDropdownMenuLeftOffsetChanged(this, new DependencyPropertyChangedEventArgs());
            //this.Loaded += (object sender, RoutedEventArgs e) => { MinCorrectWidth = Root.CalcMinItemCorrectWidth(); };
            this.MouseDoubleClick += (object sender, MouseButtonEventArgs e) => { DropdownIconIsUpsideDown = !DropdownIconIsUpsideDown;  };
        }

        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(NavMenuItem), new PropertyMetadata(30.0));




        public string ItemText
        {
            get { return (string)GetValue(ItemTextProperty); }
            set { SetValue(ItemTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemTextProperty =
            DependencyProperty.Register("ItemText", typeof(string), typeof(NavMenuItem), new PropertyMetadata("1"));




        public double IconSize
        {
            get { return (double)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double), typeof(NavMenuItem), new PropertyMetadata(20.0));


        public Color MouseInItemBackground
        {
            get { return (Color)GetValue(MouseInItemBackgroundProperty); }
            set { SetValue(MouseInItemBackgroundProperty, value); }
        }
        public static readonly DependencyProperty MouseInItemBackgroundProperty =
            DependencyProperty.Register("MouseInItemBackground", typeof(Color), typeof(NavMenuItem), new PropertyMetadata(Color.FromRgb(155, 155, 155)));



        public Duration MouseInOverAnimationDuration
        {
            get { return (Duration)GetValue(MouseInOverAnimationDurationProperty); }
            set { SetValue(MouseInOverAnimationDurationProperty, value); }
        }
        public static readonly DependencyProperty MouseInOverAnimationDurationProperty =
            DependencyProperty.Register("MouseInOverAnimationDuration", typeof(Duration), typeof(NavMenuItem), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(150))));



        public Duration DropdownMenuAnimationDuration
        {
            get { return (Duration)GetValue(DropdownMenuAnimationDurationProperty); }
            set { SetValue(DropdownMenuAnimationDurationProperty, value); }
        }
        public static readonly DependencyProperty DropdownMenuAnimationDurationProperty =
            DependencyProperty.Register("DropdownMenuAnimationDuration", typeof(Duration), typeof(NavMenuItem), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(350))));




        public double DropdownMenuLeftOffset
        {
            get { return (double)GetValue(DropdownMenuLeftOffsetProperty); }
            set { SetValue(DropdownMenuLeftOffsetProperty, value); }
        }
        public static readonly DependencyProperty DropdownMenuLeftOffsetProperty =
            DependencyProperty.Register("DropdownMenuLeftOffset", typeof(double), typeof(NavMenuItem), new UIPropertyMetadata(20.0, OnDropdownMenuLeftOffsetChanged));
        private static void OnDropdownMenuLeftOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NavMenuItem nmi)
            {
                var result = new Thickness();               
                result.Top = result.Bottom = result.Right = (nmi.ItemHeight - nmi.IconSize) / 2;
                result.Left = result.Right + nmi.DropdownMenuLeftOffset;
                nmi.IconMargin = result;
            }
        }


        public IEasingFunction DropdownMenuFunction
        {
            get { return (IEasingFunction)GetValue(DropdownMenuFunctionProperty); }
            set { SetValue(DropdownMenuFunctionProperty, value); }
        }
        public static readonly DependencyProperty DropdownMenuFunctionProperty =
            DependencyProperty.Register("DropdownMenuFunction", typeof(IEasingFunction), typeof(NavMenuItem), new PropertyMetadata(new CircleEase()));




        public Visibility DropdownIconVisibility
        {
            get { return (Visibility)GetValue(DropdownIconVisibilityProperty); }
            set { SetValue(DropdownIconVisibilityProperty, value); }
        }
        public static readonly DependencyProperty DropdownIconVisibilityProperty =
            DependencyProperty.Register("DropdownIconVisibility", typeof(Visibility), typeof(NavMenuItem), new PropertyMetadata(Visibility.Hidden));




        public double DropdownIconSize
        {
            get { return (double)GetValue(DropdownIconSizeProperty); }
            set { SetValue(DropdownIconSizeProperty, value); }
        }
        public static readonly DependencyProperty DropdownIconSizeProperty =
            DependencyProperty.Register("DropdownIconSize", typeof(double), typeof(NavMenuItem), new PropertyMetadata(10.0));



        public ImageSource DropdownIconSource
        {
            get { return (ImageSource)GetValue(DropdownIconSourceProperty); }
            set { SetValue(DropdownIconSourceProperty, value); }
        }
        public static readonly DependencyProperty DropdownIconSourceProperty =
            DependencyProperty.Register("DropdownIconSource", typeof(ImageSource), typeof(NavMenuItem), new PropertyMetadata(new BitmapImage(new Uri(new Uri(Assembly.GetExecutingAssembly().Location), "1.png"))));




        public Color ItemTextColor
        {
            get { return (Color)GetValue(ItemTextColorProperty); }
            set { SetValue(ItemTextColorProperty, value); }
        }
        public static readonly DependencyProperty ItemTextColorProperty =
            DependencyProperty.Register("ItemTextColor", typeof(Color), typeof(NavMenuItem), new PropertyMetadata(Color.FromRgb(0, 0, 0)));



        public Color MouseInItemTextColor
        {
            get { return (Color)GetValue(MouseInItemTextColorProperty); }
            set { SetValue(MouseInItemTextColorProperty, value); }
        }
        public static readonly DependencyProperty MouseInItemTextColorProperty =
            DependencyProperty.Register("MouseInItemTextColor", typeof(Color), typeof(NavMenuItem), new PropertyMetadata(Color.FromRgb(0, 255, 0)));



        public FontFamily ItemTextFontFamily
        {
            get { return (FontFamily)GetValue(ItemTextFontFamilyProperty); }
            set { SetValue(ItemTextFontFamilyProperty, value); }
        }
        public static readonly DependencyProperty ItemTextFontFamilyProperty =
            DependencyProperty.Register("ItemTextFontFamily", typeof(FontFamily), typeof(NavMenuItem), new PropertyMetadata(new FontFamily("Arial")));




        public FontWeight ItemTextFontWeight
        {
            get { return (FontWeight)GetValue(ItemTextFontWeightProperty); }
            set { SetValue(ItemTextFontWeightProperty, value); }
        }
        public static readonly DependencyProperty ItemTextFontWeightProperty =
            DependencyProperty.Register("ItemTextFontWeight", typeof(FontWeight), typeof(NavMenuItem), new PropertyMetadata(FontWeights.Bold));



        public double ItemTextFontSize
        {
            get { return (double)GetValue(ItemTextFontSizeProperty); }
            set { SetValue(ItemTextFontSizeProperty, value); }
        }
        public static readonly DependencyProperty ItemTextFontSizeProperty =
            DependencyProperty.Register("ItemTextFontSize", typeof(double), typeof(NavMenuItem), new PropertyMetadata(12.0));


        public double MinCorrectWidth
        {
            get { return (double)GetValue(MinCorrectWidthProperty); }
            set { SetValue(MinCorrectWidthProperty, value); }
        }
        public static readonly DependencyProperty MinCorrectWidthProperty =
            DependencyProperty.Register("MinCorrectWidth", typeof(double), typeof(NavMenuItem), new PropertyMetadata(0.0));



        public ImageSource IconSource
        {
            get { return (ImageSource)GetValue(IconSourceProperty); }
            set { SetValue(IconSourceProperty, value); }
        }
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register("IconSource", typeof(ImageSource), typeof(NavMenuItem), new PropertyMetadata(new BitmapImage(new Uri(new Uri(Assembly.GetExecutingAssembly().Location), "1.png"))));



        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(NavMenuItem), new PropertyMetadata(new Thickness()));




        public bool DropdownIconIsUpsideDown
        {
            get { return (bool)GetValue(DropdownIconIsUpsideDownProperty); }
            set { SetValue(DropdownIconIsUpsideDownProperty, value); }
        }
        public static readonly DependencyProperty DropdownIconIsUpsideDownProperty =
            DependencyProperty.Register("DropdownIconIsUpsideDown", typeof(bool), typeof(NavMenuItem), new UIPropertyMetadata(false, OnDropdownIconIsUpsideDownChanged));

        private static void OnDropdownIconIsUpsideDownChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is NavMenuItem nmi)
            {
                if (nmi.DropdownIconIsUpsideDown)
                    nmi.OnDropdownIconUpsideDown();
                else
                    nmi.OnDropdownIconUpsideUp();
            }
        }







        public static readonly RoutedEvent DropdownIconUpsideDownEvent = EventManager.RegisterRoutedEvent(
           "DropdownIconUpsideDown", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavMenuItem));
        public event RoutedEventHandler DropdownIconUpsideDown
        {
            add { AddHandler(DropdownIconUpsideDownEvent, value); }
            remove { RemoveHandler(DropdownIconUpsideDownEvent, value); }
        }
        public void OnDropdownIconUpsideDown()
        {
            RaiseEvent(new RoutedEventArgs(NavMenuItem.DropdownIconUpsideDownEvent));
        }


        public static readonly RoutedEvent DropdownIconUpsideUpEvent = EventManager.RegisterRoutedEvent(
   "DropdownIconUpsideUp", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(NavMenuItem));
        public event RoutedEventHandler DropdownIconUpsideUp
        {
            add { AddHandler(DropdownIconUpsideUpEvent, value); }
            remove { RemoveHandler(DropdownIconUpsideUpEvent, value); }
        }
        public void OnDropdownIconUpsideUp()
        {
            RaiseEvent(new RoutedEventArgs(NavMenuItem.DropdownIconUpsideUpEvent));
        }

    }
}
