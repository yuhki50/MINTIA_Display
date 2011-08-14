using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.IO;
using System.Xml;

namespace NicoVideoLib
{
    public class Ranking
    {
        NameValueCollection nicoVideoRssList;
        Dictionary<string, XmlNodeList> ranking_NodeList;
        List<Bitmap> thumbnailImageList;

        string pubDate_least = "";


        public int Length { get; private set; }


        public Ranking()
        {
            nicoVideoRssList = new NameValueCollection();
            ranking_NodeList = new Dictionary<string, XmlNodeList>();
            thumbnailImageList = new List<Bitmap>();


            // リストを作成 //
            nicoVideoRssList.Add("新着投稿動画", "http://www.nicovideo.jp/newarrival?rss=2.0");
            nicoVideoRssList.Add("新着コメント動画", "http://www.nicovideo.jp/recent?rss=2.0");
            nicoVideoRssList.Add("毎時ランキング", "http://www.nicovideo.jp/ranking/fav/hourly/all?rss=2.0");
            nicoVideoRssList.Add("デイリーランキング", "http://www.nicovideo.jp/ranking/fav/daily/all?rss=2.0");
            nicoVideoRssList.Add("週間ランキング", "http://www.nicovideo.jp/ranking/fav/weekly/all?rss=2.0");
            nicoVideoRssList.Add("月間ランキング", "http://www.nicovideo.jp/ranking/fav/monthly/all?rss=2.0");
            nicoVideoRssList.Add("新着公式動画", "http://www.nicovideo.jp/tag/%E5%85%AC%E5%BC%8F?sort=f&rss=2.0");
        }

        public string[] getCategoryList()
        {
            return nicoVideoRssList.AllKeys;
        }

        public string getCategoryUrl(string category)
        {
            return nicoVideoRssList[category];
        }

        public void update(string category)
        {
            // ランキングを取得 //
            // XMLドキュメントを読み込む //
            XmlDocument doc = new XmlDocument();
            doc.Load(nicoVideoRssList[category]);


            // ノードからリストを抜き出す //
            ranking_NodeList.Clear();
            ranking_NodeList.Add("title", doc.SelectNodes("//rss/channel/item/title"));
            ranking_NodeList.Add("pubDate", doc.SelectNodes("//rss/channel/item/pubDate"));
            ranking_NodeList.Add("description", doc.SelectNodes("//rss/channel/item/description"));


            // アイテム数を記憶 //
            Length = ranking_NodeList["title"].Count;


            // サムネイル画像を一括取得 //
            if (pubDate_least != ranking_NodeList["pubDate"][0].InnerText)
            {
                pubDate_least = ranking_NodeList["pubDate"][0].InnerText;

                WebClient wc = new WebClient();

                thumbnailImageList.Clear();

                foreach (XmlNode item in ranking_NodeList["description"])
                {
                    string description = item.InnerText;
                    int urlStart = description.IndexOf("src=") + 5;
                    int urlStop = description.IndexOf('"', urlStart);

                    thumbnailImageList.Add(new Bitmap(
                        wc.OpenRead(description.Substring(urlStart, urlStop - urlStart))));
                }

                wc.Dispose();
            }
        }

        public void update(int index)
        {
            update(nicoVideoRssList.Keys[index]);
        }

        public string getTitle(int index)
        {
            return ranking_NodeList["title"][index].InnerText;
        }

        public DateTime getPubDate(int index)
        {
            return DateTime.ParseExact(ranking_NodeList["pubDate"][index].InnerText,
                "ddd, dd MMM yyyy HH':'mm':'ss zz'00'",
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                System.Globalization.DateTimeStyles.None);
        }

        public string getText(int index)
        {
            string description = ranking_NodeList["description"][index].InnerText;
            int textStart = description.IndexOf("nico-description") + 18;
            int textStop = description.IndexOf("</p>", textStart);

            return description.Substring(textStart, textStop - textStart);
        }

        public Bitmap getThumbnailImage(int index)
        {
            return thumbnailImageList[index];
        }
    }
}
