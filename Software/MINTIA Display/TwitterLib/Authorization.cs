using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using OAuthLib;

namespace TwitterLib
{
    public class Authorization
    {
        RequestToken reqToken;
        public Consumer TwitterConsumer { get; private set; }
        public string TwitterID { get; private set; }
        public string TwitterTokenValue { get; private set; }
        public string TwitterTokenSecret { get; private set; }


        public Authorization(string twitterConsumerKey, string twitterConsumerSecret)
        {
            TwitterConsumer = new Consumer(twitterConsumerKey, twitterConsumerSecret);
        }

        public Authorization(string twitterConsumerKey, string twitterConsumerSecret, string twitterID, string twitterTokenValue, string twitterTokenSecret)
        {
            TwitterConsumer = new Consumer(twitterConsumerKey, twitterConsumerSecret);
            TwitterID = twitterID;
            TwitterTokenValue = twitterTokenValue;
            TwitterTokenSecret = twitterTokenSecret;
        }

        public string getAuthUrl()
        {
            // リクエスト トークンを生成 //
            reqToken = TwitterConsumer.ObtainUnauthorizedRequestToken("http://twitter.com/oauth/request_token", "http://twitter.com/");

            // 認証ページのURLを返す //
            return Consumer.BuildUserAuthorizationURL("http://twitter.com/oauth/authorize", reqToken);
        }

        public void setPIN(string twitterPIN)
        {
            // 不要な文字を削除 //
            foreach (string c in new string[] { "\r", "\n", " " })
            {
                twitterPIN = twitterPIN.Replace(c, "");
            }


            // アクセストークンを取得 //
            Parameter[] responseParameter = null;

            AccessToken accessToken =
                TwitterConsumer.RequestAccessToken(
                    twitterPIN,
                    reqToken,
                    "http://twitter.com/oauth/access_token",
                    "http://twitter.com/",
                    ref responseParameter);

            TwitterTokenValue = accessToken.TokenValue;
            TwitterTokenSecret = accessToken.TokenSecret;


            // ユーザー名を取得 //
            foreach (Parameter param in responseParameter)
            {
                if (param.Name == "screen_name")
                {
                    TwitterID = param.Value;
                    break;
                }
            }
        }
    }
}
