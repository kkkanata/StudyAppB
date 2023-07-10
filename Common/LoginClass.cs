using System.Text.RegularExpressions;
using System.Windows;
using JsonDataPack;

namespace studyApp.Common
{
    class LoginClass
    {
        //パスワードの判定
        public bool Pass(string num,string Inputpass)
        {
            JsonDataClass.Member menber = (JsonDataClass.Member)Application.Current.Properties["Menber"];
            bool result = false;
            string pass = menber.mPassword;//登録したパスワード
            if(pass == Inputpass)
            {
                result = true;
            }
            return result;
        }

        //隊員の判定
        public bool member(string jsonmember, string member)
        {
            bool result = false;
            if (jsonmember == member)
            {
                result = true;
            }
            return result;
        }

        //新規パスワードの判定
        public bool Detect(string Inputpass, string member)
        {
            bool result = false;
            // 条件の確認
            if (Inputpass.Length >= 8 &&
                new Regex("^[0-9a-zA-Z]+$").IsMatch(Inputpass) &&
                !Inputpass.Contains(member))
            {
                result = true;
            }
            return result;
        }
    }
}