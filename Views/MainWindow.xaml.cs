using System;
using System.Collections.Generic;
using System.IO;
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

using JsonDataPack;


namespace studyApp.Views
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new initialize();
            var json = new ReadJsonDataClass.ReadRescueRequestData();
            string FolderPath = @"JsonResource/Rescue";
            try
            {
                string[] names = Directory.GetFiles(FolderPath, "*", SearchOption.TopDirectoryOnly);
                IList<JsonDataClass.RescueRequestData> rescues = new List<JsonDataClass.RescueRequestData>();
                foreach (string name in names)
                {
                    rescues.Add(json.ReadAllRescueRequestData(name));
                }
                Application.Current.Properties["RescuRecest"] = rescues;
            }
            catch (FileNotFoundException fileNotFound)
            {
                MessageBox.Show("読み込みエラー");
            }
            
            // 最大化表示
            this.WindowState = WindowState.Maximized;
            Uri uri = new Uri("LoginView.xaml", UriKind.Relative);

            frame.Source = uri;
        }
    }
}
