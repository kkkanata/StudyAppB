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

using studyApp.Common;

using studyApp.Views.SubView;
using JsonDataPack;
using System.Windows.Forms;
using System.Configuration;

namespace studyApp.Views
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void loginCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private static Mainpage Screen = null;
        private void loginOKButton_Click(object sender, RoutedEventArgs e)
        {
            var jsonMember = new ReadJsonDataClass.ReadMember();
            var jsonGrade = new ReadJsonDataClass.ReadGrade();
            string enployeeNumber = enployeeNumberTextField.Text;
            string password = passwordTextField.Password;
            var member = jsonMember.ReadOwnMember(enployeeNumber,password);
            if(member == null)
            {
                System.Windows.MessageBox.Show("パスワード又は職員番号が違います。");
            }
            else
            {
                var cal = new OtherClass();
                var interval = cal.CalcMonthInterval(DateTime.Parse(member.mChangeDate));
                if (member.mResetFlag == true || interval > 3)
                {
                    System.Windows.MessageBox.Show("パスワードを再設定してください");
                    ChangePasswordView Chpas = new ChangePasswordView();
                    Chpas.ShowDialog();
                }
                else
                {
                    DateTime dt = DateTime.Now;
                    member.mLoginDate = dt.ToString("yyyy/MM/dd");
                    System.Windows.Application.Current.Properties["Menber"] = member;
                    //次のページへ
                    if (Screen == null)
                    {
                        // 次に表示するページを生成、以後使いまわし
                        Screen = new Mainpage();
                    }
                    System.Windows.Application.Current.Properties["Grade"] = jsonGrade.ReadFile(enployeeNumber);
                    // Mainpageへ移動
                    this.NavigationService.Navigate(Screen);
                }
            }
        }

        private void changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordView Chpas = new ChangePasswordView();
            Chpas.ShowDialog();
        }

        private void pathButton_Click(object sender, RoutedEventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            System.Windows.MessageBox.Show(ConfigurationManager.AppSettings["FolderPath"]);
            using (var fbd = new FolderBrowserDialog()
            {
                Description = "フォルダを選択してください",
                SelectedPath = @"D:\Users",
            })
            {
                // OKが押されずに終わった場合はキャンセルされている
                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }

                // SelectedPathで選択されたフォルダを取得する
                System.Windows.MessageBox.Show($"{fbd.SelectedPath}を選択しました");
                config.AppSettings.Settings["FolderPath"].Value = fbd.SelectedPath;
                config.Save();
            }
        }
    }
}
