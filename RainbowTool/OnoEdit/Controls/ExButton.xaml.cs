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
                SetValue(TextProperty, value);
                if (_usingoverridelink) return;
                learnlink.Inlines.Clear();
                learnlink.Inlines.Add("Learn about " + value);
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
        public event EventHandler OnLinkClick;

        public bool RouteLinkClickEvent { get; set; }

        private bool _usingoverridelink;
        private bool _linkhover;

        public String LinkOverrideText
        {
            set
            {
                _usingoverridelink = true;
                learnlink.Inlines.Clear();
                learnlink.Inlines.Add(value);
            }
        }

        private Storyboard Enter { get; set; }
        private Storyboard Leave { get; set; }
        private Storyboard Entertype2 { get; set; }
        private Storyboard Leavetype2 { get; set; }

        private Storyboard NoAeroEnter { get; set; }
        private Storyboard NoAeroLeave { get; set; }

        public ExButton()
        {
            InitializeComponent();

            Microsoft.Windows.Shell.SystemParameters2.Current.PropertyChanged += CurrentPropertyChanged;

            if (UserSettings.CurrentSettings.UseAeroScheme)
                if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                {
                    button.Style = (Style) FindResource("ButtonStyle");
                    Height = 20;
                }


            UserSettings.OnSettingsChanged += UserSettings_OnSettingsChanged;

            Enter = (Storyboard)FindResource("menter");
            Leave = (Storyboard) FindResource("mleave");

            Entertype2 = (Storyboard) FindResource("mentertype2");
            Leavetype2 = (Storyboard) FindResource("mleavetype2");

            NoAeroEnter = (Storyboard) FindResource("namenter");
            NoAeroLeave = (Storyboard) FindResource("namleave");

            MouseEnter += ExButtonMouseEnter;
            MouseLeave += ExButtonMouseLeave;
        }

        void UserSettings_OnSettingsChanged(object sender, object oVar, Type pType)
        {
            CurrentPropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("IsGlassEnabled"));
        }

        void CurrentPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals("IsGlassEnabled")) return;

            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled && UserSettings.CurrentSettings.UseAeroScheme)
            {
                button.Style = (Style)FindResource("ButtonStyle");
                Height = 20;
            }
            else{
                button.Style = null;
                Height = double.NaN;
            }
        }

        void ExButtonMouseLeave(object sender, MouseEventArgs e)
        {
            if(!RouteLinkClickEvent)
            if (!UserSettings.CurrentSettings.LearnLinkEnabled)
            {
                if (!Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled | !UserSettings.CurrentSettings.UseAeroScheme) return;               

                Leavetype2.Begin();
                return;
            }

            if (UserSettings.CurrentSettings.UseAeroScheme && Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                Leave.Begin();
            else
                NoAeroLeave.Begin();
        }

        void ExButtonMouseEnter(object sender, MouseEventArgs e)
        {
            if (!RouteLinkClickEvent)
            if (!UserSettings.CurrentSettings.LearnLinkEnabled)
            {
                if (!Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled | !UserSettings.CurrentSettings.UseAeroScheme) return;

                Entertype2.Begin();
                return;
            }


            if (UserSettings.CurrentSettings.UseAeroScheme && Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled)
                Enter.Begin();
            else
                NoAeroEnter.Begin();
        }

        private void HyperlinkRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (RouteLinkClickEvent)
            {
                if (OnLinkClick != null)
                    OnLinkClick(this, new EventArgs());
            }else
            Process.Start(e.Uri.ToString());
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            if (_linkhover) return;

            if (OnClick != null)
                OnClick(this, new EventArgs());
        }

        private void LinkMouseEnter(object sender, MouseEventArgs e)
        {
            _linkhover = true;
        }

        private void LinkMouseLeave(object sender, MouseEventArgs e)
        {
            _linkhover = false;
        }
    }
}
