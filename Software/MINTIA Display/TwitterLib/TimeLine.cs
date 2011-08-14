using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Net;
using System.IO;
using System.Xml;
using OAuthLib;

namespace TwitterLib
{
    public class TimeLine
    {
        Authorization _auth;

        Dictionary<string, XmlNodeList> timeLine_NodeList;
        List<Bitmap> profileImageList;

        string id_least = "";


        public int Length { get; private set; }


        public TimeLine(Authorization auth)
        {
            _auth = auth;

            timeLine_NodeList = new Dictionary<string, XmlNodeList>();
            profileImageList = new List<Bitmap>();
        }

        public void update(int count)
        {
            // タイムラインを取得 //
            //string url = "https://twitter.com/statuses/friends_timeline/" + _auth.TwitterID + ".xml";
            string url = "https://twitter.com/statuses/home_timeline/" + _auth.TwitterID + ".xml";

            AccessToken accessToken = new AccessToken(_auth.TwitterTokenValue, _auth.TwitterTokenSecret);

            Parameter[] parameters = { new Parameter("count", count.ToString()) };

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            AsyncCallback callback = delegate { };

            IAsyncResult iresult = _auth.TwitterConsumer.BeginAccessProtectedResource(
                accessToken, url, "GET", "https://twitter.com/oauth/authorize",
                parameters, null, out request, callback, null);

            while (!iresult.IsCompleted);


            // XMLドキュメントを読み込む //
            WebResponse response = request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(reader.ReadToEnd());
            reader.Close();
            response.Close();


            // ノードからリストを抜き出す //
            timeLine_NodeList.Clear();
            timeLine_NodeList.Add("name", doc.SelectNodes("//statuses/status/user/name"));
            timeLine_NodeList.Add("created_at", doc.SelectNodes("//statuses/status/created_at"));
            timeLine_NodeList.Add("id", doc.SelectNodes("//statuses/status/id"));
            timeLine_NodeList.Add("text", doc.SelectNodes("//statuses/status/text"));
            timeLine_NodeList.Add("in_reply_to_screen_name", doc.SelectNodes("//statuses/status/in_reply_to_screen_name"));
            timeLine_NodeList.Add("retweeted", doc.SelectNodes("//statuses/status/retweeted"));
            timeLine_NodeList.Add("screen_name", doc.SelectNodes("//statuses/status/user/screen_name"));
            timeLine_NodeList.Add("profile_image_url", doc.SelectNodes("//statuses/status/user/profile_image_url"));


            // アイテム数を記憶 //
            Length = timeLine_NodeList["name"].Count;


            // プロファイル画像を一括取得 //
            if (id_least != timeLine_NodeList["id"][0].InnerText)
            {
                id_least = timeLine_NodeList["id"][0].InnerText;

                WebClient wc = new WebClient();

                profileImageList.Clear();

                foreach (XmlNode item in timeLine_NodeList["profile_image_url"])
                {
                    profileImageList.Add(new Bitmap(wc.OpenRead(item.InnerText)));
                }

                wc.Dispose();
            }
        }

        public string getName(int index)
        {
            return timeLine_NodeList["name"][index].InnerText;
        }

        public DateTime getCreatedAt(int index)
        {
            return DateTime.ParseExact(timeLine_NodeList["created_at"][index].InnerText,
                "ddd MMM dd HH':'mm':'ss zz'00' yyyy",
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                System.Globalization.DateTimeStyles.None);
        }

        public string getId(int index)
        {
            return timeLine_NodeList["id"][index].InnerText;
        }

        public string getText(int index)
        {
            return timeLine_NodeList["text"][index].InnerText;
        }

        public string getInReplyToScreenName(int index)
        {
            return timeLine_NodeList["in_reply_to_screen_name"][index].InnerText;
        }

        public bool getRetweeted(int index)
        {
            bool status = false;
            
            if (timeLine_NodeList["retweeted"][index].InnerText != "false")
	        {
                status = true;
        	}

            return status;
        }

        public string getScreenName(int index)
        {
            return timeLine_NodeList["screen_name"][index].InnerText;
        }

        public Bitmap getProfileImage(int index)
        {
            return profileImageList[index];
        }

        public string getHowOld(int index)
        {
            TimeSpan ts = DateTime.Now - DateTime.ParseExact(timeLine_NodeList["created_at"][index].InnerText,
                                        "ddd MMM dd HH':'mm':'ss zz'00' yyyy",
                                        System.Globalization.DateTimeFormatInfo.InvariantInfo,
                                        System.Globalization.DateTimeStyles.None);

            string ts_string = ((int)ts.TotalSeconds).ToString() + "秒前"; ;

            if (ts.TotalMinutes > 1)
            {
                ts_string = ((int)ts.TotalMinutes).ToString() + "分前";
            }

            if (ts.TotalHours > 1)
            {
                ts_string = ((int)ts.TotalHours).ToString() + "時間前";
            }

            if (ts.TotalDays > 1)
            {
                ts_string = ((int)ts.TotalDays).ToString() + "日前";
            }

            return ts_string;
        }
    }
}
