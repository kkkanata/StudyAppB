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
    /// henkousippai.xaml の相互作用ロジック
    /// </summary>
    public partial class Change_Failed : Page
    {
        public Change_Failed()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordView Chpas = new ChangePasswordView();
            Chpas.ShowDialog();
            Window.GetWindow(this).Close();
        }
    }
}