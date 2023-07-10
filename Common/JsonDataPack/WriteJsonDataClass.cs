using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JsonDataPack
{
    public class WriteJsonDataClass{
        //JSON処理参考:https://www.newtonsoft.com/json/help/html/WriteJsonWithJsonTextWriter.htm
        
        public class Member
        {
            //TODO:実際にIListにデータを格納して実行してみる.
            
            public void WriteAllMember(IList<JsonDataClass.Member> memberIList)
            {
                try
            {
                //string変換
                string jsonData = JsonConvert.SerializeObject(memberIList);

                //ファイル書き込み
                File.WriteAllText("temp/" + "member" + ".json", jsonData);

            }
            catch
            {
                Console.WriteLine("Menber.jsonの書き込みに失敗");
            }
        }
                //StringBuilder sb = new StringBuilder();
                //StringWriter sw = new StringWriter(sb);

                //using(JsonWriter writer = new JsonTextWriter(sw))
                //{
                //    writer.Formatting = Formatting.None;
                    
                //    writer.WriteStartArray();
                        
                //    foreach (var member in memberIList)
                //    {//foreach---
                //        writer.WriteStartObject();
                //        writer.WritePropertyName("mNumber");    //書き込むkeyを指定
                //        writer.WriteValue(member.mNumber);      //keyに対して書き込む内容を指定
                //        writer.WritePropertyName("mShibu");
                //        writer.WriteValue(member.mShibu);
                //        writer.WritePropertyName("mKichi");
                //        writer.WriteValue(member.mKichi);
                //        writer.WritePropertyName("mFuriganaName");
                //        writer.WriteValue(member.mFuriganaName);
                //        writer.WritePropertyName("mKanjiName");
                //        writer.WriteValue(member.mKanjiName);
                //        writer.WritePropertyName("mPassword");
                //        writer.WriteValue(member.mPassword);
                //        writer.WritePropertyName("mResetFlag");
                //        writer.WriteValue(member.mResetFlag);
                //        writer.WritePropertyName("mChangeDate");
                //        writer.WriteValue(member.mChangeDate);
                //        writer.WritePropertyName("mKanriFlag");
                //        writer.WriteValue(member.mKanriFlag);
                //        writer.WritePropertyName("mLoginDate");
                //        writer.WriteValue(member.mLoginDate);
                //        writer.WriteEndObject();
                //        // File.WriteAllText(@"testFiles/""test.json", JsonConvert.SerializeObject(member));
                //    }//---foreach
                //    writer.WriteEnd();
                    
                //    writeStream("temp/" + "member" + ".json",sb);

                //}
            }
            public void WritePersonal(JsonDataClass.Member member)
            {
            //    StringBuilder sb = new StringBuilder();
            //    StringWriter sw = new StringWriter(sb);
            //    using (JsonWriter writer = new JsonTextWriter(sw))
            //    {
            //        writer.Formatting = Formatting.None;
            //        writer.WriteStartObject();
            //        writer.WritePropertyName("mNumber");    //書き込むkeyを指定
            //        writer.WriteValue(member.mNumber);      //keyに対して書き込む内容を指定
            //        writer.WritePropertyName("mShibu");
            //        writer.WriteValue(member.mShibu);
            //        writer.WritePropertyName("mKichi");
            //        writer.WriteValue(member.mKichi);
            //        writer.WritePropertyName("mFuriganaName");
            //        writer.WriteValue(member.mFuriganaName);
            //        writer.WritePropertyName("mKanjiName");
            //        writer.WriteValue(member.mKanjiName);
            //        writer.WritePropertyName("mPassword");
            //        writer.WriteValue(member.mPassword);
            //        writer.WritePropertyName("mResetFlag");
            //        writer.WriteValue(member.mResetFlag);
            //        writer.WritePropertyName("mChangeDate");
            //        writer.WriteValue(member.mChangeDate);
            //        writer.WritePropertyName("mKanriFlag");
            //        writer.WriteValue(member.mKanriFlag);
            //        writer.WritePropertyName("mLoginDate");
            //        writer.WriteValue(member.mLoginDate);
            //        writer.WriteEndObject();
            //    }
            //    writeStream("JsonResource/Personal/memberpersonal.json",sb);
            //}
        }
        public class Grade
        {
            public void WriteGrade(JsonDataClass.Grade grade,string number)
            {
                try
                {
                    //string変換
                    string jsonData = JsonConvert.SerializeObject(grade);

                    //ファイル書き込み
                    File.WriteAllText("JsonResource/Personal/grade" + number + ".json", jsonData);

                }
                catch
                {
                    Console.WriteLine("grade.jsonの書き込みに失敗");
                }
                //StringBuilder sb = new StringBuilder();
                //StringWriter sw = new StringWriter(sb);
                //using (JsonWriter writer = new JsonTextWriter(sw))
                //{
                //    writer.Formatting = Formatting.None;
                //    writer.WriteStartObject();//一番最初の 

                //    writer.WritePropertyName("gNumber");
                //    writer.WriteValue(grade.gNumber);

                //    writer.WritePropertyName("rescueRequestState");
                //    writer.WriteStartArray(); //rescueRequest [
                //    foreach (var rrState in grade.rescueRequestState)
                //    {
                //        writer.WriteStartObject();

                //        writer.WritePropertyName("rNumber");
                //        writer.WriteValue(rrState.rNumber);
                //        writer.WritePropertyName("rProgress");
                //        writer.WriteValue(rrState.rProgress);
                //        writer.WritePropertyName("rAnswered");
                //        writer.WriteValue(rrState.rAnswered);
                //        writer.WritePropertyName("rSavePoint");
                //        writer.WriteValue(rrState.rSavePoint);
                //        writer.WritePropertyName("rMissCount");
                //        writer.WriteValue(rrState.rMissCount);
                //        //writer.WritePropertyName("workAccident");
                //        //writer.WriteValue(rrState.workAccident);

                //        writer.WritePropertyName("Miss");
                //        writer.WriteStartArray();
                //        foreach (var miss in rrState.miss)
                //        {
                //            writer.WriteStartObject();
                //            writer.WritePropertyName("mNumber");
                //            writer.WriteValue(miss.mNumber);

                //            writer.WritePropertyName("mChoices");
                //            writer.WriteStartArray();
                //            foreach (var commomMiss in miss.mChoices)
                //            {
                //                writer.WriteValue(commomMiss);

                //            }
                //            writer.WriteEndArray();

                //            writer.WriteEnd();
                //        }
                //        writer.WriteEndArray();

                //        writer.WritePropertyName("workAccident");
                //        writer.WriteStartObject();
                //        writer.WritePropertyName("sNumber");
                //        writer.WriteValue(rrState.workAccident.sNumber);
                //        writer.WritePropertyName("sChoices");
                //        writer.WriteValue(rrState.workAccident.sChoices);
                //        writer.WriteEndObject();
                //        writer.WritePropertyName("rScore");
                //        writer.WriteValue(rrState.rScore);
                //    }
                //    writer.WriteEndArray();
                //    // ] end of rescueRequest 
                //    writer.WriteEndObject();//一番最後の }
                //}//---using

                //writeStream("temp/grade"+number+".json",sb);

            }
        }
        public class ResqueRequestData
        
        {
            public static void Example01_createClassData(JsonDataClass.RescueRequestData data,string path)//JsonDataClass.RescueRequestData data
            {                
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);

                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();
                    writer.WritePropertyName("rNumber");
                    writer.WriteValue(data.rNumber);
                    writer.WritePropertyName("rCapacity");
                    writer.WriteValue(data.rCapacity);
                    writer.WritePropertyName("rVehicle");
                    writer.WriteValue(data.rVehicle);
                    writer.WritePropertyName("rCategory");
                    writer.WriteValue(data.rCategory);
                    writer.WritePropertyName("rTitle");
                    writer.WriteValue(data.rTitle);
                    writer.WritePropertyName("rStatus");
                    writer.WriteValue(data.rStatus);
                    writer.WritePropertyName("rPlace");
                    writer.WriteValue(data.rPlace);
                    writer.WritePropertyName("rDetail");
                    writer.WriteValue(data.rDetail);
                    writer.WritePropertyName("rCarColor");
                    writer.WriteValue(data.rCarColor);
                    writer.WritePropertyName("rPhoto");
                    writer.WriteValue(data.rPhoto);
                    writer.WritePropertyName("rSuccessScore");
                    writer.WriteValue(data.rSuccessScore);
                    writer.WritePropertyName("rCreateDate");
                    writer.WriteValue(data.rCreateDate);
                    writer.WritePropertyName("rPublic");
                    writer.WriteValue(data.rPublic);
                    writer.WritePropertyName("rStartQuestion");
                    writer.WriteValue(data.rStartQuestion);
                    writer.WritePropertyName("Question");
                    writer.WriteStartArray();
                    foreach (var question in data.question)
                    {
                        writer.WriteStartObject(); //questionのStartObject

                        writer.WritePropertyName("qNumber");
                        writer.WriteValue(question.qNumber);
                        writer.WritePropertyName("qCheckPoint");
                        writer.WriteValue(question.qCheckPoint);
                        writer.WritePropertyName("qType");
                        writer.WriteValue(question.qType);
                        writer.WritePropertyName("qText");
                        writer.WriteValue(question.qText);
                        writer.WritePropertyName("qPhotos");
                        writer.WriteStartArray(); //qPhotos[]用  

                        // for (var photo = 0; i < data.question[i].qPhotos.Length; photo = photo +1)
                        // {
                            // writer.WriteValue(data.question[i].qPhotos);
                        // }
                        foreach (string qPhoto in question.qPhotos)
                        {
                            writer.WriteValue(qPhoto);
                        }
                        writer.WriteEnd();//qPhotos[]の終わりを示す
                    
                        writer.WritePropertyName("qNext");
                        writer.WriteValue(question.qNext);
                        writer.WritePropertyName("qCoordinateX");
                        writer.WriteValue(question.qCoordinateX);
                        writer.WritePropertyName("qCoordinateY");
                        writer.WriteValue(question.qCoordinateY);
                        Console.Write("115");
                        //choice---------------
                        writer.WritePropertyName("choices");
                        writer.WriteStartArray();//choice
                        foreach (var choice in question.choices)
                        {
                            writer.WriteStartObject();//choice用のStartObj
                            writer.WritePropertyName("cNumber");
                            writer.WriteValue(choice.cNumber);
                            writer.WritePropertyName("cAnswer");
                            writer.WriteValue(choice.cAnswer);
                            writer.WritePropertyName("cRight");
                            writer.WriteValue(choice.cRight);
                            writer.WritePropertyName("cDecrement");
                            writer.WriteValue(choice.cDecrement);
                            writer.WritePropertyName("cNext");
                            writer.WriteValue(choice.cNext);
                            writer.WritePropertyName("cExplanation");
                            writer.WriteValue(choice.cExplanation);   
                            Console.Write("choice",choice);
                            writer.WriteEndObject();//choice用のEndObj
                        }
                        
                        writer.WriteEnd();//choice[]の終わりを示す
                        //choice---------------
                        
                        writer.WriteEndObject();//question用のEndObj
                    }
                    //-------------------------question
                    
                    writer.WriteEnd();//question[]の終わり("]")を示すEnd
                    
                    writer.WriteEndObject();//RescueRequest用のEndObj
                }//using
                // Console.WriteLine(sb.ToString());
                // File.Create(@"test/"+text+"res.json");
                // File.WriteAllText(@"test/"+text+"res.json",sb.ToString());
                writeStream("JsonResource/Rescue/" + path + ".json",sb);
            }
        }

        public WriteJsonDataClass() {}
        void ExportSerializeJsonFile_utf8(JObject json,string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(json.ToString());
            }
        }
        //StringBuilderを受け取り指定されたパスにjsonファイルを出力する関数。
        //pathには拡張子「.json」まで必要です。
        static void writeStream(string path,StringBuilder sb)
        {
            //using (FileStream fs = File.Create(@path))
            using (StreamWriter swr = new StreamWriter(@path))
            {
                swr.WriteLine(sb.ToString());
            }
        }
    }//---WriteJsonDataClass
}