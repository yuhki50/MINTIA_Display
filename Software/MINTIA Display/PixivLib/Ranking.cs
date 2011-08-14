using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.IO;

namespace PixivLib
{
    public class Ranking
    {
        NameValueCollection pixivRankingList = new NameValueCollection();

        List<string> rankingList_illustId = new List<string>();  //イラストID
        List<string> rankingList_memberID = new List<string>();  //ユーザーID
        List<string> rankingList_fileExtension = new List<string>();  // 拡張子
        List<string> rankingList_title = new List<string>();  //タイトル
        List<string> rankingList_imgserver = new List<string>();  //imgサーバの番号
        List<string> rankingList_author = new List<string>();  //作者
        List<string> rankingList_thumbURL = new List<string>();  // サムネイルURL
        List<string> rankingList_largeImageURL = new List<string>();  // 画像URL
        List<string> rankingList_submitted = new List<string>();  //投稿日
        List<string> rankingList_tags = new List<string>();  //タグ
        List<string> rankingList_tool = new List<string>();  //ツール
        List<string> rankingList_appreciatedCount = new List<string>();  //評価回数
        List<string> rankingList_totalPoint = new List<string>();  //総合点
        List<string> rankingList_viewCount = new List<string>();  // 閲覧数
        List<string> rankingList_comment = new List<string>();  //作者コメント

        List<Bitmap> thumbnailImageList = new List<Bitmap>();
        List<Bitmap> largeImageList = new List<Bitmap>();

        string illustid_least = "";


        public int Length { get; private set; }


        public Ranking()
        {
            // リストを作成 //
            pixivRankingList.Add("みんなの新着", "http://iphone.pxv.jp/iphone/new_illust.php");
            pixivRankingList.Add("デイリーランキング", "http://iphone.pxv.jp/iphone/ranking.php?mode=day");
            pixivRankingList.Add("ウィークリーランキング", "http://iphone.pxv.jp/iphone/ranking.php?mode=day");
            pixivRankingList.Add("マンスリーランキング", "http://iphone.pxv.jp/iphone/ranking.php?mode=month");
        }

        public string[] getCategoryList()
        {
            return pixivRankingList.AllKeys;
        }

        public string getCategoryUrl(string category)
        {
            return pixivRankingList[category];
        }

        public void update(string category)
        {
            WebClient wc = new WebClient();
            string csv;


            // CSVファイルをダウンロード //
            wc.Encoding = Encoding.GetEncoding("UTF-8");

            if (pixivRankingList[category] == null)
            {
                // 検索モード //
                csv = wc.DownloadString("http://iphone.pxv.jp/iphone/search.php?s_mode=s_tag&word=" + category);
            }
            else
        	{
                // ラインキングモード //
                csv = wc.DownloadString(pixivRankingList[category]);
	        }


            // 行単位に分割 //
            string[] lines = csv.Split(new string[] { ",,\n" }, StringSplitOptions.RemoveEmptyEntries);

            string[] col;

            rankingList_illustId.Clear();  //イラストID
            rankingList_memberID.Clear();  //ユーザーID
            rankingList_fileExtension.Clear();  // 拡張子
            rankingList_title.Clear();  //タイトル
            rankingList_imgserver.Clear();  //imgサーバの番号
            rankingList_author.Clear();  //作者
            rankingList_thumbURL.Clear();  // サムネイルURL
            rankingList_largeImageURL.Clear();  // 画像URL
            rankingList_submitted.Clear();  //投稿日
            rankingList_tags.Clear();  //タグ
            rankingList_tool.Clear();  //ツール
            rankingList_appreciatedCount.Clear();  //評価回数
            rankingList_totalPoint.Clear();  //総合点
            rankingList_viewCount.Clear();  // 閲覧数
            rankingList_comment.Clear();  //作者コメント

            foreach (string line in lines)
            {
                col = line.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                rankingList_illustId.Add(col[0].TrimStart('\"').TrimEnd('\"'));  //イラストID
                rankingList_memberID.Add(col[1].TrimStart('\"').TrimEnd('\"'));  //ユーザーID
                rankingList_fileExtension.Add(col[2].TrimStart('\"').TrimEnd('\"'));  // 拡張子
                rankingList_title.Add(col[3].TrimStart('\"').TrimEnd('\"'));  //タイトル
                rankingList_imgserver.Add(col[4].TrimStart('\"').TrimEnd('\"'));  //imgサーバの番号
                rankingList_author.Add(col[5].TrimStart('\"').TrimEnd('\"'));  //作者
                rankingList_thumbURL.Add(col[6].TrimStart('\"').TrimEnd('\"'));  // サムネイルURL
                rankingList_largeImageURL.Add(col[7].TrimStart('\"').TrimEnd('\"'));  // 画像URL
                rankingList_submitted.Add(col[8].TrimStart('\"').TrimEnd('\"'));  //投稿日
                rankingList_tags.Add(col[9].TrimStart('\"').TrimEnd('\"'));  //タグ
//                rankingList_tool.Add(col[10].TrimStart('\"').TrimEnd('\"'));  //ツール
//                rankingList_appreciatedCount.Add(col[11].TrimStart('\"').TrimEnd('\"'));  //評価回数
//                rankingList_totalPoint.Add(col[12].TrimStart('\"').TrimEnd('\"'));  //総合点
//                rankingList_viewCount.Add(col[13].TrimStart('\"').TrimEnd('\"'));  // 閲覧数
//                rankingList_comment.Add(col[14].TrimStart('\"').TrimEnd('\"'));  //作者コメント
            }


            // アイテム数を記憶 //
            Length = rankingList_illustId.Count;


            // サムネイルを一括取得 //
            //if (illustid_least != rankingList_illustId[0])
            //{
            //    illustid_least = rankingList_illustId[0];

            //    thumbnailImageList.Clear();
                
            //    foreach (string thumbURL in rankingList_thumbURL)
            //    {
            //        thumbnailImageList.Add(new Bitmap(wc.OpenRead(thumbURL)));
            //    }
            //}


            // 画像を一括取得 //
            if (illustid_least != rankingList_illustId[0])
            {
                illustid_least = rankingList_illustId[0];

                largeImageList.Clear();

                foreach (string largeImageURL in rankingList_largeImageURL)
                {
                    largeImageList.Add(new Bitmap(wc.OpenRead(largeImageURL)));
                }
            }

            wc.Dispose();
        }

        public void update(int index)
        {
            update(pixivRankingList.Keys[index]);
        }

        public string getIllustId(int index)
        {
            return rankingList_illustId[index];
        }

        public string getMemberID(int index)
        {
            return rankingList_memberID[index];
        }

        public string getFileExtension(int index)
        {
            return rankingList_fileExtension[index];
        }

        public string getTitle(int index)
        {
            return rankingList_title[index];
        }

        public string getImgserver(int index)
        {
            return rankingList_imgserver[index];
        }

        public string getAuthor(int index)
        {
            return rankingList_author[index];
        }

        public string getThumbnailURL(int index)
        {
            return rankingList_thumbURL[index];
        }

        public Bitmap getThumbnailBitmap(int index)
        {
            return thumbnailImageList[index];
        }

        public string getLargeImageURL(int index)
        {
            return rankingList_largeImageURL[index];
        }

        public Bitmap getLargeImageBitmap(int index)
        {
            return largeImageList[index];
        }

        public DateTime getSubmitted(int index)
        {
            return DateTime.ParseExact(rankingList_submitted[index],
                "yyyy'-'MM'-'dd HH':'mm':'ss",
                System.Globalization.DateTimeFormatInfo.InvariantInfo,
                System.Globalization.DateTimeStyles.None);
        }

        public string getTags(int index)
        {
            return rankingList_tags[index];
        }

        public string getTool(int index)
        {
            return rankingList_tool[index];
        }

        public string getAppreciatedCount(int index)
        {
            return rankingList_appreciatedCount[index];
        }

        public string getTotalPoint(int index)
        {
            return rankingList_totalPoint[index];
        }

        public string getViewCount(int index)
        {
            return rankingList_viewCount[index];
        }

        public string getComment(int index)
        {
            return rankingList_comment[index];
        }

        public string getPageUrl(int index)
        {
            return "http://www.pixiv.net/member_illust.php?mode=medium&illust_id=" + rankingList_illustId[index];
        }
    }
}
