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
using JsonDataPack;



namespace studyApp.Views.SubView
{
    /// <summary>
    /// ChangePassword.xaml の相互作用ロジック
    /// </summary>
    public partial class ChangePasswordView : Window
    {
        public ChangePasswordView()
        {
            InitializeComponent();
        }

        private void cpCancelButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void cpChangeButton_Click(object sender, RoutedEventArgs e)
        {
            LoginClass login = new LoginClass();
            var jsonMember = new ReadJsonDataClass.ReadMember();
            var member = jsonMember.ReadOwnMember(cpCurrentenployeeNumberTextField.Text, cpCurrentPasswordTextField.Text);
            IList<JsonDataClass.Member> listMember = jsonMember.ReadAllMember();
            Uri uri;

            if (member != null && login.Detect(cpNewPasswordTextField.Text, member.mNumber) && member.mPassword != cpNewPasswordTextField.Text)
            {
                //メンバー一覧から、それぞれに対して処理をかける
                //listMember.CountでlistMemberの要素数を取得している。
                for (int i = 0; i < listMember.Count; i++)
                {
                    //メンバー一覧から隊員番号が一致したとき。
                    if (listMember[i].mNumber.Equals(cpCurrentenployeeNumberTextField.Text))
                    {
                        //JsonDataClass.Member型のmemberに格納する。
                        listMember[i].mPassword = cpNewPasswordTextField.Text;
                        // リセットフラグ変更
                        listMember[i].mResetFlag = false;
                        // 変更日
                        DateTime dt = DateTime.Now;
                        listMember[i].mChangeDate = dt.ToString("yyyy/MM/dd");
                        break;
                    }
                }
                // 変更内容の書き込み
                var writeJson = new WriteJsonDataClass.Member();
                writeJson.WriteAllMember(listMember);
                uri = new Uri("Change_Completed.xaml", UriKind.Relative);
            }
            else
            {
                uri = new Uri("Change_Failed.xaml", UriKind.Relative);
            }
            Height = 350;
            frame.Source = uri;
        }
    }
}
