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

namespace HamburgerMenu
{
    /// <summary>
    /// Interaction logic for NavMenu.xaml
    /// </summary>
    public partial class NavMenu : UserControl
    {
        public NavMenu()
        {
            InitializeComponent();
            Draw(Root);
        }

        private void Draw(StackPanel root)
        {
            if (ItemSource == null)
                return;

            foreach (var el in ItemSource)
            {
                CreateNavMenuItem(el, root);
            }
        }

        private void CreateNavMenuItem(NavMenuItemData item, Panel toAdd)
        {
            StackPanel result = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                Height = ItemHeight,
                Background = new SolidColorBrush(item.IsSelected ? SelectedItemBackground : Background)
            };


            ColorAnimation mouseEnterAnimation = new ColorAnimation()
            {
                From = item.IsSelected ? SelectedItemBackground : Background,
                To = MouseInBackground,
                Duration = MouseInOverAnimationDuration
            };
            result.MouseEnter += (object sender, MouseEventArgs e) => { result.Background.BeginAnimation(SolidColorBrush.ColorProperty, mouseEnterAnimation); };

            ColorAnimation mouseLeaveAnimation = new ColorAnimation()
            {
                From = MouseInBackground,
                To = item.IsSelected ? SelectedItemBackground : Background,
                Duration = MouseInOverAnimationDuration
            };
            result.MouseLeave += (object sender, MouseEventArgs e) => { result.Background.BeginAnimation(SolidColorBrush.ColorProperty, mouseLeaveAnimation); };


            Image icon = new Image()
            {
                Height = IconSize,
                Width = IconSize,
                Margin = new Thickness((ItemHeight - IconSize) / 2)
            };

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(new Uri(Assembly.GetExecutingAssembly().Location), "1.png");
            bitmap.EndInit();
            icon.Source = bitmap;

            result.Children.Add(icon);


            TextBlock text = new TextBlock()
            {
                VerticalAlignment = VerticalAlignment.Center,
                Text = item.Text
            };
            result.Children.Add(text);

            toAdd.Children.Add(result);

            if (item.IsDropdownItem && item.DropdownItems != null)
            {
                StackPanel dropdownMenu = new StackPanel()
                {
                    Orientation = Orientation.Vertical,
                    Margin = DropdownMenuOffset,
                };

                foreach (var el in item.DropdownItems)
                {
                    CreateNavMenuItem(el, dropdownMenu);
                }


                DoubleAnimation dropupAnimation = new DoubleAnimation()
                {
                    Duration = DropdownMenuAnimationDuration,
                    EasingFunction = DropdownMenuFunction
                };

                DoubleAnimationUsingKeyFrames dropdownAnimation = new DoubleAnimationUsingKeyFrames();
                dropdownAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0)), Value = 0});
                dropdownAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(DropdownMenuAnimationDuration.Milliseconds - 1)), Value = 0 });
                dropdownAnimation.KeyFrames.Add(new EasingDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(DropdownMenuAnimationDuration), Value = 100000 });

                result.MouseLeftButtonUp += (object sender, MouseButtonEventArgs e) =>
                {
                    if (sender is Panel senderPanel)
                    {
                        if (senderPanel.Parent is Panel parentPanel)
                        {
                            var submenu = parentPanel.Children[parentPanel.Children.IndexOf(senderPanel) + 1];

                            if (submenu is Panel submenuPanel)
                            {
                                if (submenuPanel.MaxHeight == 0)
                                {
                                    dropdownAnimation.KeyFrames[1].Value = submenuPanel.RenderSize.Height;
                                    submenuPanel.BeginAnimation(Panel.MaxHeightProperty, dropdownAnimation);
                                }
                                else
                                {
                                    dropupAnimation.From = submenuPanel.RenderSize.Height;
                                    dropupAnimation.To = 0;
                                    submenuPanel.BeginAnimation(Panel.MaxHeightProperty, dropupAnimation);
                                }
                            }
                        }
                    }

                };

                toAdd.Children.Add(dropdownMenu);
            }

        }

        public void ReDraw()
        {
            Root.Children.Clear();
            Draw(Root);
        }




        public List<NavMenuItemData> ItemSource
        {
            get { return (List<NavMenuItemData>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }
        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(List<NavMenuItemData>), typeof(NavMenu), new UIPropertyMetadata(null, OnItemSourceChanged));

        private static void OnItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is NavMenu nm)
            {
                nm.ReDraw();
            }
        }




        public int ItemHeight
        {
            get { return (int)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(int), typeof(NavMenu), new PropertyMetadata(30));



        public int IconSize
        {
            get { return (int)GetValue(IconSizeProperty); }
            set { SetValue(IconSizeProperty, value); }
        }
        public static readonly DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(int), typeof(NavMenu), new PropertyMetadata(20));



        public Color Background
        {
            get { return (Color)GetValue(BackgroundProperty); }
            set { SetValue(BackgroundProperty, value); }
        }
        public static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.Register("Background", typeof(Color), typeof(NavMenu), new PropertyMetadata(Color.FromRgb(0, 255, 0)));



        public Color MouseInBackground
        {
            get { return (Color)GetValue(MouseInBackgroundProperty); }
            set { SetValue(MouseInBackgroundProperty, value); }
        }
        public static readonly DependencyProperty MouseInBackgroundProperty =
            DependencyProperty.Register("MouseInBackground", typeof(Color), typeof(NavMenu), new PropertyMetadata(Color.FromRgb(155, 155, 155)));



        public Duration MouseInOverAnimationDuration
        {
            get { return (Duration)GetValue(MouseInOverAnimationDurationProperty); }
            set { SetValue(MouseInOverAnimationDurationProperty, value); }
        }
        public static readonly DependencyProperty MouseInOverAnimationDurationProperty =
            DependencyProperty.Register("MouseInOverAnimationDuration", typeof(Duration), typeof(NavMenu), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(150))));



        public TimeSpan DropdownMenuAnimationDuration
        {
            get { return (TimeSpan)GetValue(DropdownMenuAnimationDurationProperty); }
            set { SetValue(DropdownMenuAnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DropdownMenuAnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropdownMenuAnimationDurationProperty =
            DependencyProperty.Register("DropdownMenuAnimationDuration", typeof(TimeSpan), typeof(NavMenu), new PropertyMetadata(TimeSpan.FromMilliseconds(350)));







        public Color SelectedItemBackground
        {
            get { return (Color)GetValue(SelectedItemBackgroundProperty); }
            set { SetValue(SelectedItemBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItemBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemBackgroundProperty =
            DependencyProperty.Register("SelectedItemBackground", typeof(Color), typeof(NavMenu), new PropertyMetadata(Color.FromRgb(100, 100, 100)));




        public Thickness DropdownMenuOffset
        {
            get { return (Thickness)GetValue(DropdownMenuOffsetProperty); }
            set { SetValue(DropdownMenuOffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DropdownMenuOffset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropdownMenuOffsetProperty =
            DependencyProperty.Register("DropdownMenuOffset", typeof(Thickness), typeof(NavMenu), new PropertyMetadata(new Thickness(20, 0, 0, 0)));





        public IEasingFunction DropdownMenuFunction
        {
            get { return (IEasingFunction)GetValue(DropdownMenuFunctionProperty); }
            set { SetValue(DropdownMenuFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DropdownMenuFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DropdownMenuFunctionProperty =
            DependencyProperty.Register("DropdownMenuFunction", typeof(IEasingFunction), typeof(NavMenu), new PropertyMetadata(new CircleEase()));




    }
}
