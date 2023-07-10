using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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


namespace studyApp.Views
{

    public class ControData2
    {
        public String Text { get; set; }    //106行目Controlで問題文を格納するための変数
    }

    /// <summary>
    /// Page2.xaml の相互作用ロジック
    /// </summary>
    public partial class QuestionView2 : Page
    {
        IList<JsonDataClass.RescueRequestData> rescu = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
        JsonDataClass.Grade grade = (JsonDataClass.Grade)Application.Current.Properties["Grade"];
        JsonDataClass.Grade.RescueRequestState res = (JsonDataClass.Grade.RescueRequestState)Application.Current.Properties["Rescue"];

        public QuestionView2()
        {
            InitializeComponent();
            DataContext = new Control();
            qNum= (int)Application.Current.Properties["mondai"];    //次に表示する問題が格納されている配列の添え字を挿入

            for (int i = 0; i < 4; i++)//初期化↓
            {
                Select[i] = null;
                right_text[i] = null;
                false_text[i] = null;
                right_num[i] = -1;
                false_num[i] = -1;
            }
            bad_text = null;
            bad_num = -1;              //初期化↑
            
            int rNum = (int)Application.Current.Properties["next"];     //大問が格納されてい配列の添え字
            questionStatementText.DataContext = rescu[rNum].question[qNum].qText;   //問題文を格納

            currentSheetNumberText.DataContext = (j + 1) + "/" + rescu[rNum].question[qNum].qPhotos.Count();    //写真の数を格納

            for (int i = 0; i < rescu[rNum].question[qNum].choices.Count(); i++)//問題の選択肢の解答を"正解"、"ミス"、"作業事故"で区別してそれぞれ配列に格納
            {
                answer[i] = rescu[rNum].question[qNum].choices[i].cAnswer;
                if (rescu[rNum].question[qNum].choices[i].cRight == "正解")
                {
                    right_text[i] = answer[i];
                    right_num[i] = i;
                }
                else if (rescu[rNum].question[qNum].choices[i].cRight == "ミス")
                {
                    false_text[i] = answer[i];
                    false_num[i] = i;
                }
                else if (rescu[rNum].question[qNum].choices[i].cRight == "作業事故")
                {
                    bad_text = answer[i];
                    bad_num = i;
                }
            }

            btmimg.DataContext = rescu[rNum].question[qNum].qPhotos[j];     //問題画像を格納

            menuPullDown.Items.Add("指令画面へ");
            menuPullDown.Items.Add("問題を閉じてメイン画面へ");

        }
        int qNum;   //問題が格納されているquestion配列の添え字
        int j = 0;  //210～250行目　画像遷移に使う変数
        string[] right_text = { "", "", "", "" };   //cRightが"正解"の問題文を格納するための配列
        int[] right_num = { -1, -1, -1, -1 };       //cRightが"正解"の問題の選択肢番号の添え字を入れておくための配列
        string[] false_text = { "", "", "", "" };   //cRightが"ミス"の問題文を格納するための配列
        int[] false_num = { -1, -1, -1, -1 };       //cRightが"ミス"の問題の選択肢番号の添え字を入れておくための配列
        string bad_text="";                         //cRightが"作業事故"の問題文を格納するための配列
        int bad_num=-1;                             //cRightが"作業事故"の問題の選択肢番号の添え字を入れておくための配列
        string[] Select = { "", "", "", "" };       //選んだ選択肢の問題文を格納するための配列
        string[] answer = { "", "", "", "" };       //一時的に問題文を格納しておくための配列

        public class Control        //問題文を表示するための処理↓
        {
            IList<JsonDataClass.RescueRequestData> rescu = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
            int rNum = (int)Application.Current.Properties["next"];     //大門番号の添え字を格納
            int qNum = (int)Application.Current.Properties["mondai"];   //問題番号の添え字を格納
            public List<ControData2> Controls { get; set; }

            public Control()
            {
                List<ControData2> list1 = new List<ControData2>();
                for (int i = 0; i < rescu[rNum].question[qNum].choices.Count(); i++)    //問題文を選択肢の数分表示
                {
                    list1.Add(
                        new ControData2()
                        {
                            Text = rescu[rNum].question[qNum].choices[i].cAnswer    //選択肢の問題文を格納
                        }
                    );
                }
                this.Controls = list1;
            }
        }                          //問題文を表示するための処理↑

        private void menuPullDown_DropDownClosed(object sender, EventArgs e)    //menuPullDownボタンの処理↓
        {
            DataSearch dataSearch = new DataSearch();
            int rNum = (int)Application.Current.Properties["next"];
            if (menuPullDown.Text == "指令画面へ")
            {
                var instrc = new instructionView();
                instrc.ShowDialog();
            }
            else if (menuPullDown.Text == "問題を閉じてメイン画面へ")  //問題を閉じる時の処理↓
            {
                bool flg = false;
                if (rescu[rNum].question[qNum].qCheckPoint == false)    //チェックポイントがfalse
                {
                    MessageBoxResult result= MessageBox.Show("進行状況は保存できませんがよろしいですか？","保存", MessageBoxButton.OKCancel);

                    if(result== MessageBoxResult.OK)
                    {
                        flg = true;
                    }
                }
                else    //チェックポイントがtrue
                {
                    MessageBoxResult result= MessageBox.Show("進行状況を保存しますか？", "保存",MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.No)     //"進行状況を保存しますか？"の選択で"いいえ"を選択した時の処理
                    {
                        flg = true;
                    }
                    else if(result == MessageBoxResult.Yes)     //"進行状況を保存しますか？"の選択で"はい"を選択した時の処理
                    {
                        res.rAnswered = "解答中";
                        res.rSavePoint = rescu[rNum].question[qNum].qNumber;   //セーブポイントに今解いている問題番号を格納
                        if (dataSearch.ResqueSearch(res.rNumber) == -1)    //成績データにrNumberに該当するデータがなければ追加する
                        {
                            JsonDataClass.Grade.RescueRequestState[] rescue_resize = new JsonDataClass.Grade.RescueRequestState[grade.rescueRequestState.Length+1];
                            for(var i = 0; i < grade.rescueRequestState.Length; i++)
                            {
                                rescue_resize[i] = grade.rescueRequestState[i];
                            }
                            rescue_resize[rescue_resize.Length-1] = res;
                            grade.rescueRequestState = rescue_resize;

                        }
                        else        //成績データにrNumberに該当するデータがあれば上書きする
                        {
                            int gNum = dataSearch.ResqueSearch(res.rNumber);
                            grade.rescueRequestState[gNum] = res;
                        }
                        this.writeGradeData(grade);
                        flg = true;
                    }
                }
                if (flg == true)  //flgがtrueならメイン画面へ
                {
                    var mainPage = new Mainpage();
                    this.NavigationService.Navigate(mainPage);
                }
            }           //問題を閉じる時の処理↑
        }                                                       //menuPullDownボタンの処理↑

        private void questionImage_Click(object sender, RoutedEventArgs e)  //問題画面で画像を押したときの処理（画像拡大、画面遷移）
        {
            int rNum = (int)Application.Current.Properties["next"];
            var queimg = new QuestionImageView();
            queimg.questionImage.DataContext = rescu[rNum].question[qNum].qPhotos[j];   //問題の画像を格納
            queimg.ShowDialog();
        }

        private void backImageButton_Click(object sender, RoutedEventArgs e)    //矢印ボタン"＞"の処理
        {
            int rNum = (int)Application.Current.Properties["next"];
            if (rescu[rNum].question[qNum].qPhotos.Count() >= 2)    //画像の数が2個以上の時
            {
                j += 1;

                if (j > rescu[rNum].question[qNum].qPhotos.Count() - 1)     //最後の画像で、"＞"を押したとき
                {
                    j = 0;      //1個目の画像の添え字を格納
                    btmimg.DataContext = rescu[rNum].question[qNum].qPhotos[j];     //画像変更
                    currentSheetNumberText.DataContext = (j + 1) + "/" + rescu[rNum].question[qNum].qPhotos.Count();    //ページ数を変更
                }
                else     //次の画像に遷移
                {
                    btmimg.DataContext = rescu[rNum].question[qNum].qPhotos[j];     //画像変更
                    currentSheetNumberText.DataContext = (j + 1) + "/" + rescu[rNum].question[qNum].qPhotos.Count();    //ページ数を変更
                }
            }
        }

        private void forwardImageButton_Click(object sender, RoutedEventArgs e)     //矢印ボタン"＜"の処理
        {
            int rNum = (int)Application.Current.Properties["next"];
            if (rescu[rNum].question[qNum].qPhotos.Count() >= 2)    //画像の数が2個以上の時
            {
                j -= 1;

                if (j < 0)      //最初の画像で、"＜"を押したとき
                {
                    j = rescu[rNum].question[qNum].qPhotos.Count() - 1;     //最後の画像の添え字を格納
                    btmimg.DataContext = rescu[rNum].question[qNum].qPhotos[j];     //画像変更
                    currentSheetNumberText.DataContext = (j + 1) + "/" + rescu[rNum].question[qNum].qPhotos.Count();    //ページ数を変更
                }
                else     //前の画像に遷移
                {
                    btmimg.DataContext = rescu[rNum].question[qNum].qPhotos[j];     //画像変更
                    currentSheetNumberText.DataContext = (j + 1) + "/" + rescu[rNum].question[qNum].qPhotos.Count();    //ページ数を変更
                }
            }
        }

        private void executionButton_Click(object sender, RoutedEventArgs e)
        {
            Boolean ExecutionFlag = false;  //ボタンを押下の有無を決める変数

            int rNum = (int)Application.Current.Properties["next"];
            for (int i = 0; i < rescu[rNum].question[qNum].choices.Count(); i++)    //大門の中にある問題の数、回す
            {
                var text = UserControl_QuestionView2.answerArr[i].answer;     //選択肢の問題文を格納
                var flag = UserControl_QuestionView2.answerArr[i].flag;   //選択肢が選ばれたかどうかのフラグ
                if (flag == true)   //選択されていたら...
                {
                    Select[i] = text;           //選択された問題文を格納
                    ExecutionFlag = true;
                }
            }

            if (ExecutionFlag == true)          //1つでも選択された問題があれば実行される
            {
                DataSearch dataSearch = new DataSearch();
                int cnt = (int)Application.Current.Properties["count"];
                Application.Current.Properties["count"] = cnt + 1;
                for (int i = 0; i < Select.Length; i++)//選んだ選択肢の数、回す
                {
                    for (int j = 0; j < right_num.Length; j++)//問題にある"正解"の数、回す
                    {
                        if (Select[i] == right_text[j])//選んだ問題文が"正解"だったらright_numの添え字に沿った値を-1に変える
                        {
                            right_num[j] = -1;      //選べなかった"正解"の問題を識別するための処理
                        }
                    }
                }
                for (int i = 0; i < right_num.Length; i++)//選べなかった"正解"の減点数(cDecrement)を合計点(score)からマイナス　*複数選択問題の場合、選べなかった"正解"があると減点になる(単一選択の場合これはない)
                {
                    if (right_num[i] != -1)         //right_numの配列の中に-1じゃない値があれば"選べなかった"正解"になる
                    {
                        res.rScore -= rescu[rNum].question[qNum].choices[right_num[i]].cDecrement; //合計点からマイナス
                    }
                }

                int[] miss_num = new int[0];           //ミスを選んだ時の減点処理↓
                for (int i = 0; i < Select.Length; i++)//選んだ選択肢の数、回す
                {
                    for (int j = 0; j < false_num.Length; j++)//問題にある"ミス"の数、回す
                    {
                        if (Select[i] == false_text[j] && Select[i] != null)        //選択した問題が"ミス"なら減点する
                        {
                            Array.Resize(ref miss_num, miss_num.Length + 1); 
                            miss_num[miss_num.Length-1]=false_num[j]+1;         //ミスした問題の選択肢の添え字を格納する
                            res.rScore -= rescu[rNum].question[qNum].choices[false_num[j]].cDecrement; //合計点からマイナス
                        }
                    }
                }                                   //ミスを選んだ時の減点処理↑
                res.rMissCount += miss_num.Count();    //ミスの数をカウント
                JsonDataClass.Grade.RescueRequestState.Miss miss = new JsonDataClass.Grade.RescueRequestState.Miss { mNumber = rescu[rNum].question[qNum].qNumber, mChoices = miss_num }; //選んだ"ミス"の問題番号と選択肢番号を格納
                int num;
                if (res.miss == null)
                {
                    num = 1;
                }
                else
                {
                    num = res.miss.Length + 1;
                }
                JsonDataClass.Grade.RescueRequestState.Miss[] miss_resize = new JsonDataClass.Grade.RescueRequestState.Miss[num];
                if (res.miss != null)
                {
                    for (var i = 0; i < res.miss.Length; i++)
                    {
                        miss_resize[i] = res.miss[i];
                    }
                }
                miss_resize[miss_resize.Length - 1] = miss; //ミス配列を追加
                res.miss = miss_resize;

                var q = dataSearch.QuestionSearch(rNum, rescu[rNum].question[qNum].qNext); //次の問題の添え字を格納
                for (int i = 0; i < Select.Length; i++)//作業事故の処理↓
                {
                    if (Select[i] == bad_text && Select[i] != null) //選んだ問題が"作業事故"のとき
                    {
                        res.rScore = 0;    //合計点を0にする
                        q = -1;
                        res.rAnswered = "解答ミス";
                        res.workAccident.sNumber = rescu[rNum].question[qNum].qNumber;
                        res.workAccident.sChoices = bad_num+1;       //作業事故を起こした問題番号と選択肢番号を格納
                    }
                }                                      //作業事故の処理↑

                if (q != -1)//最後の問題でなければ次の問題への遷移の処理     -1なら最後の問題
                {
                    res.rProgress = (int)Math.Ceiling(((int)Application.Current.Properties["count"] / (double)rescu[rNum].question.Length) * 100);
                    Application.Current.Properties["mondai"] = q;   //次の問題の添え字を格納
                    if (rescu[rNum].question[q].qType == "単一選択")    //次の問題が単一選択の時
                    {
                        var mondai1 = new QuestionView1();
                        this.NavigationService.Navigate(mondai1);
                    }
                    else //if (rescu[0].question[q].qType == "複数選択")    //次の問題が複数選択の時
                    {
                        var mondai2 = new QuestionView2();
                        this.NavigationService.Navigate(mondai2);
                    }
                }
                else                 //次の問題がなければ結果画面への遷移の処理
                {
                    if (res.rScore != 0)
                    {
                        res.rProgress = 100;
                    }
                    if (rescu[rNum].rSuccessScore > res.rScore)    //点数が合格点に満たなかった時の処理
                    {

                        if (dataSearch.ResqueSearch(res.rNumber) == -1)    //成績データにrNumberに該当するデータがなければ新しく追加する
                        {
                            JsonDataClass.Grade.RescueRequestState[] rescue_resize = new JsonDataClass.Grade.RescueRequestState[grade.rescueRequestState.Length + 1];
                            for (var i = 0; i < grade.rescueRequestState.Length; i++)
                            {
                                rescue_resize[i] = grade.rescueRequestState[i];
                            }
                            rescue_resize[rescue_resize.Length-1] = res;
                            grade.rescueRequestState = rescue_resize;

                        }
                        else        //成績データにrNumberに該当するデータがあればそのデータを上書きする
                        {
                            int gNum = dataSearch.ResqueSearch(res.rNumber);
                            grade.rescueRequestState[gNum] = res;
                        }
                        writeGradeData(grade);
                        var Failed = new Result_Failed();
                        this.NavigationService.Navigate(Failed);        //失敗画面へ
                    }
                    else    //点数が合格点に満たした時の処理
                    {
                        if (res.rScore == 100) //点数が100点の時
                        {
                            res.rAnswered = "解答済";
                        }
                        else    //点数が100点じゃない時
                        {
                            res.rAnswered = "解答ミス";
                        }

                        if (dataSearch.ResqueSearch(res.rNumber) == -1)    //成績データにrNumberに該当するデータがなければ新しく追加する
                        {
                            JsonDataClass.Grade.RescueRequestState[] rescue_resize = new JsonDataClass.Grade.RescueRequestState[grade.rescueRequestState.Length + 1];
                            for (var i = 0; i < grade.rescueRequestState.Length; i++)
                            {
                                rescue_resize[i] = grade.rescueRequestState[i];
                            }
                            rescue_resize[rescue_resize.Length-1] = res;
                            grade.rescueRequestState = rescue_resize;
                        }
                        else        //成績データにrNumberに該当するデータがあればそのデータを上書きする
                        {
                            int gNum = dataSearch.ResqueSearch(res.rNumber);
                            grade.rescueRequestState[gNum] = res;
                        }
                        this.writeGradeData(grade);
                        var Success = new Result_Success();
                        this.NavigationService.Navigate(Success);        //成功画面へ
                    }
                }
            }
        }
        private void writeGradeData(JsonDataClass.Grade grade)
        {
            try
            {
                WriteJsonDataClass.Grade writeGrade = new WriteJsonDataClass.Grade();
                writeGrade.WriteGrade(grade, grade.gNumber);
            }
            catch (Exception)
            {
                MessageBox.Show("結果を保存できませんでした。");
            }

        }
    }
}