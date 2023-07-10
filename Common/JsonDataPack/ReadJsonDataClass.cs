using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Windows;

namespace JsonDataPack
{
    public class ReadJsonDataClass
    {
        public JsonDataClass jsonDataClass { get; set; }
        public class ReadMember
        {
            //パスワードが一致したときに格納するためのmember
            public static JsonDataClass.Member member { get; set; }
            //member一覧をとるためのデータ
            public static IList<JsonDataClass.Member> members = new List<JsonDataClass.Member>();

            public IList<JsonDataClass.Member> ReadAllMember()
            {
                //TODO:pathが指定してあるので[プロジェクトファイル/bin/Debug/temp/member.json]があるかの確認お願いします。
                //見つからないまま回して新たに書き込みの処理を書けるとnullに対しての代入になるのでエラーが出ます。
                //TODO:NULLチェックの実装
                try
                {
                    //相対パスで、temp/member.json を開いて文字列をすべて読み取ります。
                    using (var sr = new StreamReader(@"temp/member.json", System.Text.Encoding.UTF8))
                    {
                        var memberJson = sr.ReadToEnd();
                        members = JsonConvert
                            .DeserializeObject<List<JsonDataClass.Member>>(memberJson); //member.jsonをデシリアライズ
                    }

                    return members;
                }
                //エラー：ファイルが見つからなかった時の処理。
                catch (FileNotFoundException fileNotFound)
                {
                    //TODO:WPFでエラー表示する処理 コンソールアプリケーションの処理のままなので書き換えてください。
                    MessageBox.Show("FileNotFound");
                }
                return members;
            }
            
            //ReadOwnMember 入力されたIDとパスワードに一致するデータをmember.jsonから取得する。
            public JsonDataClass.Member ReadOwnMember(string ID, string pass)
            {
                IList<JsonDataClass.Member> listMember = ReadAllMember();
                //メンバー一覧から、それぞれに対して処理をかける
                //listMember.CountでlistMemberの要素数を取得している。
                for (int i = 0; i < listMember.Count; i++)
                {
                    //メンバー一覧から隊員番号と対応するパスワードが一致したとき。
                    if (listMember[i].mNumber.Equals(ID) && listMember[i].mPassword.Equals(pass))
                    {
                        //JsonDataClass.Member型のmemberに格納する。
                        member = listMember[i];
                        break;
                    }
                }
                //取得できたmemberを返す
                return member;
            }

        }

        public class ReadGrade
        {
            public JsonDataClass.Grade grade { get; set; }
            //grande.jsonの読み込み
            public JsonDataClass.Grade ReadFile(string number){
                string FolderPath = @"JsonResource/Personal/grade"+number+".json";
                try
                {
                    var jsonData = File.ReadAllText(FolderPath);
                    grade = JsonConvert.DeserializeObject<JsonDataClass.Grade>(jsonData);
                }
                //エラー：ファイルが見つからなかった時の処理。
                catch (FileNotFoundException fileNotFound)
                {
                    grade = new JsonDataClass.Grade();
                    grade.gNumber = number;
                    grade.rescueRequestState = new JsonDataClass.Grade.RescueRequestState[0];
                }
                
                return grade;
            }
        }

        public class ReadShibu
        {

        }

        public class ReadKichi
        {
            
        }

        public class ReadRescueRequestData
        {
            static string FolderPath = @"JsonResource/Rescue";
            public static JsonDataClass.RescueRequestData rescueRequestData { get; set; }
            public void example1()
            {
                var jsonData = File.ReadAllText(FolderPath);
                var JRData = JsonConvert.DeserializeObject<JsonDataClass.RescueRequestData>(jsonData);
                Console.WriteLine(JRData.rCapacity);
            }
            
            public static IList<JsonDataClass.RescueRequestData> rescueRequestDatas =
                new List<JsonDataClass.RescueRequestData>();
            
            //この関数はRescueRequestDataのデータを読み取る為の物です。
            //注意 jsonファイルのデータ中にnull値があるとエラーを返します。
            
            //TODO:テストデータからnull値の置き換え。(JAFTestDataJson)
            //TODO:初期データ作成時にnullを置かないようにする。(WriteJsonDataClass)
            public JsonDataClass.RescueRequestData ReadAllRescueRequestData(string path)
            {
                try
                {
                    var jsonData = File.ReadAllText(path);
                    var JRData = JsonConvert.DeserializeObject<JsonDataClass.RescueRequestData>(jsonData);
                    
                    //相対パスで、指定されたファイル を開いて文字列をすべて読み取ります。
                    return JRData;
                }
                //エラー：ファイルが見つからなかった時の処理。
                catch (FileNotFoundException fileNotFound)
                {
                    //TODO:WPFでエラー表示する処理 コンソールアプリケーションの処理のままなので書き換えてください。
                    MessageBox.Show("FileNotFound");
                }
                return null;
            }
        }

        public static JObject ParseJson(string path)
        {
            JObject JObjectData = new JObject();
            using (var ReadJsonRawTextData = new StreamReader(path, System.Text.Encoding.UTF8))
            {
                var ReadEndJsonRawTextData = ReadJsonRawTextData.ReadToEnd();
                
                Console.WriteLine(ReadEndJsonRawTextData);
                Console.ReadLine();
                JObjectData = JObject.Parse(ReadEndJsonRawTextData);
                return JObjectData;
            }
        }
    }
}