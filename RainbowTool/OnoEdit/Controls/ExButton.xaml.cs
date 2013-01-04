using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace OnoEdit.Controls
{
    /// <summary>
    /// Interaction logic for ExButton.xaml
    /// </summary>
    public partial class ExButton : UserControl
    {
        public String Text
        {
            get { return GetValue(TextProperty) as String; }
            set
            {
                learnlink.Inlines.Clear();
                learnlink.Inlines.Add("Learn about " + value);
                SetValue(TextProperty, value);
            }
        }

        public String ResourceLink
        {
            get { return GetValue(ResourceLinkProperty) as String; }
            set { SetValue(ResourceLinkProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("TextProperty", typeof(String), typeof(ExButton), new UIPropertyMetadata("Ex Button"));

        public static readonly DependencyProperty ResourceLinkProperty =
        DependencyProperty.Register("ResourceLinkProperty", typeof(String), typeof(ExButton), new UIPropertyMetadata("http://onotool.wikispaces.com/home"));

        public event EventHandler OnClick;


        private Storyboard Enter { get; set; }
        private Storyboard Leave { get; set; }
        private Storyboard Entertype2 { get; set; }
        private Storyboard Leavetype2 { get; set; }

        public ExButton()
        {
            InitializeComponent();

            Enter = (Storyboard)FindResource("menter");
            Leave = (Storyboard) FindResource("mleave");

            Entertype2 = (Storyboard) FindResource("mentertype2");
            Leavetype2 = (Storyboard) FindResource("mleavetype2");

            MouseEnter += ExButtonMouseEnter;
            MouseLeave += ExButtonMouseLeave;
        }

        void ExButtonMouseLeave(object sender, MouseEventArgs e)
        {
            if (!UserSettings.CurrentSettings.LearnLinkEnabled)
            {
                Leavetype2.Begin();
                return;
            }

            Leave.Begin();

        }

        void ExButtonMouseEnter(object sender, MouseEventArgs e)
        {
            if (!UserSettings.CurrentSettings.LearnLinkEnabled)
            {
                Entertype2.Begin();
                return;
            }

            Enter.Begin();

        }

        private void HyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.ToString());
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (OnClick != null)
                OnClick(this, new EventArgs());
        }
    }
}
