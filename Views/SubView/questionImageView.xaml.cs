using studyApp.Common;
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
using System.Windows.Shapes;

namespace studyApp.Views.SubView
{
    /// <summary>
    /// questionImageView.xaml の相互作用ロジック
    /// </summary>
    public partial class QuestionImageView : Window
    {
        public QuestionImageView()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void Button_Click(object sender, RoutedEventArgs e)     //閉じるボタンを押したときの処理
        {
            Window.GetWindow(this).Close();
        }
    }
}
