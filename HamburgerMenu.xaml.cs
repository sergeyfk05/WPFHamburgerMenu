using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HamburgerMenu
{
    /// <summary>
    /// Interaction logic for HamburgerMenu.xaml
    /// </summary>
    public partial class HamburgerMenu : UserControl
    {
        public HamburgerMenu()
        {
            InitializeComponent();
            this.Loaded += HamburgerMenu_Loaded;

            
        }

        private void HamburgerMenu_Loaded(object sender, RoutedEventArgs e)
        {
            Width = CollapsedWidth;

            if(sender is HamburgerMenu hm)
            {
                if (hm.Template.FindName("ToggleButton", hm) is NavMenuToggleButton button)
                {
                    button.Clicked += (object senderL, RoutedEventArgs eL) => 
                    {
                        IsCollapsed = !IsCollapsed;

                        if(IsCollapsed)
                        {
                            Width = CollapsedWidth;
                        }
                        else
                        {
                            double newWidth = CollapsedWidth;
                            if (hm.Template.FindName("TopNavMenu", hm) is NavMenu topNavMenu && newWidth < topNavMenu.MinCorrectWidth)
                            {
                                newWidth = topNavMenu.MinCorrectWidth;
                            }


                            if (hm.Template.FindName("BottomNavMenu", hm) is NavMenu bottomNavMenu && newWidth < bottomNavMenu.MinCorrectWidth)
                            {
                                newWidth = bottomNavMenu.MinCorrectWidth;
                            }

                            Width = newWidth;
                            
                        }

                        NavMenusIsEnabled = !IsCollapsed;


                    };
                }
            }
        }

        public Brush ToggleButtonBackground
        {
            get { return (Brush)GetValue(ToggleButtonBackgroundProperty); }
            set { SetValue(ToggleButtonBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToggleButtonBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToggleButtonBackgroundProperty =
            DependencyProperty.Register("ToggleButtonBackground", typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 0, 0))));



        public Brush ToggleButtonColor
        {
            get { return (Brush)GetValue(ToggleButtonColorProperty); }
            set { SetValue(ToggleButtonColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToggleButtonColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToggleButtonColorProperty =
            DependencyProperty.Register("ToggleButtonColor", typeof(Brush), typeof(HamburgerMenu), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 255, 0))));



        public bool NavMenusIsEnabled
        {
            get { return (bool)GetValue(NavMenusIsEnabledProperty); }
            set { SetValue(NavMenusIsEnabledProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NavMenusIsEnabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavMenusIsEnabledProperty =
            DependencyProperty.Register("NavMenusIsEnabled", typeof(bool), typeof(HamburgerMenu), new PropertyMetadata(false));



        public double CollapsedWidth
        {
            get { return (double)GetValue(CollapsedWidthProperty); }
            set { SetValue(CollapsedWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CollapsedWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CollapsedWidthProperty =
            DependencyProperty.Register("CollapsedWidth", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(50.0));




        public double ToggleButtonBlockHeight
        {
            get { return (double)GetValue(ToggleButtonBlockHeightProperty); }
            set { SetValue(ToggleButtonBlockHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ToggleButtonBlockHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ToggleButtonBlockHeightProperty =
            DependencyProperty.Register("ToggleButtonBlockHeight", typeof(double), typeof(HamburgerMenu), new PropertyMetadata(50.0));




        public List<NavMenuItemData> TopNavMenuItemSource
        {
            get { return (List<NavMenuItemData>)GetValue(TopNavMenuItemSourceProperty); }
            set { SetValue(TopNavMenuItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TopNavMenuItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TopNavMenuItemSourceProperty =
            DependencyProperty.Register("TopNavMenuItemSource", typeof(List<NavMenuItemData>), typeof(HamburgerMenu), new PropertyMetadata(null));



        public List<NavMenuItemData> BottomNavMenuItemSource
        {
            get { return (List<NavMenuItemData>)GetValue(BottomNavMenuItemSourceProperty); }
            set { SetValue(BottomNavMenuItemSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BottomNavMenuItemSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BottomNavMenuItemSourceProperty =
            DependencyProperty.Register("BottomNavMenuItemSource", typeof(List<NavMenuItemData>), typeof(HamburgerMenu), new PropertyMetadata(null));





        public bool IsCollapsed
        {
            get { return (bool)GetValue(IsCollapsedProperty); }
            set { SetValue(IsCollapsedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCollapsed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCollapsedProperty =
            DependencyProperty.Register("IsCollapsed", typeof(bool), typeof(HamburgerMenu), new PropertyMetadata(true));




        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Width.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(HamburgerMenu), new UIPropertyMetadata(0.0, OnWidthChanged));

        private static void OnWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is HamburgerMenu sender)
            {
                if (sender.Template.FindName("Root", sender) is DockPanel panel)
                {
                    DoubleAnimation widthAnimation = new DoubleAnimation()
                    {
                        From = (double)e.OldValue,
                        To = (double)e.NewValue,
                        Duration = sender.AnimationDuration,
                        EasingFunction = sender.AnimationFunction,
                    };

                    panel.BeginAnimation(Panel.WidthProperty, widthAnimation);
                }
            }
        }




        public TimeSpan AnimationDuration
        {
            get { return (TimeSpan)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(TimeSpan), typeof(HamburgerMenu), new PropertyMetadata(TimeSpan.FromMilliseconds(350)));




        public IEasingFunction AnimationFunction
        {
            get { return (IEasingFunction)GetValue(AnimationFunctionProperty); }
            set { SetValue(AnimationFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationFunctionProperty =
            DependencyProperty.Register("AnimationFunction", typeof(IEasingFunction), typeof(HamburgerMenu), new PropertyMetadata(new CircleEase()));


    }
}
