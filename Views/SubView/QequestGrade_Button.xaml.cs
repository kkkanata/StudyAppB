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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace studyApp
{
    /// <summary>
    /// QequestGrade.xaml の相互作用ロジック
    /// </summary>
    public partial class QequestGrade : UserControl
    {
    
        public QequestGrade()
        {
            InitializeComponent();
        }
        public string titleText
        {
            get { return (string)GetValue(titleTextProperty); }
            set { SetValue(titleTextProperty, value); }
        }
        public static readonly DependencyProperty titleTextProperty =
            DependencyProperty.Register("titleText", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));
        public string statusText
        {
            get { return (string)GetValue(statusTextProperty); }
            set { SetValue(statusTextProperty, value); }
        }
        public static readonly DependencyProperty statusTextProperty =
            DependencyProperty.Register("statusText", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));
        public string missCountText
        {
            get { return (string)GetValue(MissProperty); }
            set { SetValue(MissProperty, value); }
        }
        public static readonly DependencyProperty MissProperty =
            DependencyProperty.Register("missCountText", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));
        public string progressText
        {
            get { return (string)GetValue(progressTextProperty); }
            set { SetValue(progressTextProperty, value); }
        }
        public static readonly DependencyProperty progressTextProperty =
            DependencyProperty.Register("progressText", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));

        public int progressValue
        {
            get { return (int)GetValue(progressValueProperty); }
            set { SetValue(progressValueProperty, value); }
        }
        public static readonly DependencyProperty progressValueProperty =
            DependencyProperty.Register("progressValue", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));

        public new string Visibility
        {
            get { return (string)GetValue(VisibilityProperty); }
            set { SetValue(VisibilityProperty, value); }
        }
        public static new readonly DependencyProperty VisibilityProperty =
            DependencyProperty.Register("Visibility", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));

        public  string Color
        {
            get { return (string)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public static  readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));

        public int rNumber
        {
            get { return int.Parse((string)GetValue(rNumberProperty)); }
            set { SetValue(rNumberProperty, value); }
        }

        public static readonly DependencyProperty rNumberProperty =
            DependencyProperty.Register("rNumber", typeof(string), typeof(QequestGrade), new PropertyMetadata(string.Empty));
        private void reviewButton_Click(object sender, RoutedEventArgs e)
        {
            //上記で格納した値を復習画面へ値を格納
            Application.Current.Properties["rNumber"] = rNumber;
        }
    }

}
