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

namespace studyApp.Views.SubView
{
    /// <summary>
    /// ResultUserControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ResultUserControl : UserControl
    {
        public ResultUserControl()
        {
            InitializeComponent();
        }
        public string Result_Explanation
        {
            get { return (string)GetValue(ResultExplanationProperty); }
            set { SetValue(ResultExplanationProperty, value); }


        }
        public static readonly DependencyProperty ResultExplanationProperty =
            DependencyProperty.Register("Result_Explanation", typeof(string), typeof(ResultUserControl), new PropertyMetadata(null));

    }

}
