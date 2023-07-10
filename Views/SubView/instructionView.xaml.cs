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
using studyApp.Common;
using JsonDataPack;


namespace studyApp.Views.SubView
{
    /// <summary>
    /// instruction.xaml の相互作用ロジック
    /// </summary>
    public partial class instructionView : Window
    {
        IList<JsonDataClass.RescueRequestData> rescu = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
        public instructionView()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            int rNum = (int)Application.Current.Properties["next"];     //大門の番号が格納されている配列の添え字
            vehicleInformationImage.Items.Add("車両情報　　: " + rescu[rNum].rVehicle);        //指令画面の表示内容↓
            vehicleInformationImage.Items.Add("車色　　　　: " + rescu[rNum].rCarColor);
            vehicleInformationImage.Items.Add("車両No　　  : " + rescu[rNum].rVNumber);
            vehicleInformationImage.Items.Add("最大積載量　: " + rescu[rNum].rCapacity);
            vehicleInformationImage.Items.Add("依頼区分　　: " + rescu[rNum].rCategory);
            vehicleInformationImage.Items.Add("依頼詳細　　: " + rescu[rNum].rDetail);
            vehicleInformationImage.Items.Add("道路・場所　  : " + rescu[rNum].rPlace);
            vehicleInformationImage.Items.Add("出勤場所　　: " + rescu[rNum].rWorkPlace);
            vehicleInformationImage.Items.Add("待合場所　　: " + rescu[rNum].rMeetingPlace);

            vehicleImage.DataContext = rescu[rNum].rPhoto;
        }                                                                                                       　//指令画面の表示内容↑

        private void commandScreenCloseButton_Click(object sender, RoutedEventArgs e)   //閉じるボタンを押したときの処理
        {
            Window.GetWindow(this).Close();
        }
    }
}
