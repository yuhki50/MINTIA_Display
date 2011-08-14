using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections.Specialized;
using System.Diagnostics;
using System.Media;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using ImageEffect;
using USBDisplay;

namespace MINTIA_Display
{
    public partial class MainForm : Form
    {
        // デバイス //
        GraphicLCD myGLCD;
        Size GLCD_top;
        Size GLCD_wide;

        // アプリケーション設定 //
        const string settingsFilePath = @"settings.config";
        Settings appSettings = new Settings();

        // モード //
        string mode;
        const string MODE_SLIDESHOW = "スライドショー";
        const string MODE_TWITTER = "Twitter";
        const string MODE_NICOVIDEO = "ニコニコ動画";
        const string MODE_PIXIV = "pixiv";
        const string MODE_HATSUNEMIKU = "初音ミク";
        const string MODE_BADAPPLE = "Bad Apple!!";

        // 起動画面 //
        string startupImagePath = @"contents\System\startup.png";

        // SlideShow //
        // 対応画像フォーマット //
        string[] imageFormatList = new string[] { "bmp", "jpg", "png" };

        // Twitter //
        const string twitterConsumerKey = "Q056oo5MkfQ0nSWfPkl4g";  // OAuth認証時に必要なキー //
        const string twitterConsuemrSecret = "hhAc5myjTr8cTygXIzcidM592Cgciuhq0Af6Ib14c";  // OAuth認証時に必要な暗証番号 //
        const int twitterTLLength = 100;
        TwitterLib.Authorization twitterAuth;
        TwitterLib.TimeLine twitterTL;
        Thread twitterTL_thread;
        Font twitterFont = new Font("メイリオ", 8, FontStyle.Regular);  // MS Gothic
        const string twitterRetweetIcon = @"contents\Twitter\retweet.png";
        
        // ニコニコ動画 //
        NicoVideoLib.Ranking nicoVideoRanking = new NicoVideoLib.Ranking();
        Thread nicoVideo_thread;
        Font nicoVideoFont = new Font("メイリオ", 8, FontStyle.Regular);  // MS Gothic

        // Pixiv //
        PixivLib.Ranking pixivRanking = new PixivLib.Ranking();
        Thread pixiv_thread;
        Font pixivFont = new Font("メイリオ", 8, FontStyle.Regular);  // MS Gothic

        // 初音ミク //
        const string hatsuneMikuImageFolderPath = @"contents\HatsuneMiku";
        const string hatsuneMikuBGMFilePath = @"contents\HatsuneMiku\Hatsune Miku sings Ievan Polkka.wav";
        SoundPlayer hatsuneMikuBGMPlayer = new SoundPlayer(hatsuneMikuBGMFilePath);
        Thread hatsuneMiku_thread;

        // Bad Apple //
        const string badAppleImageFolderPath = @"contents\Bad Apple!! PV";
        const string badAppleBGMFilePath = @"contents\Bad Apple!! PV\Bad Apple!! PV.wav";
        SoundPlayer badAppleBGMPlayer = new SoundPlayer(badAppleBGMFilePath);
        Thread badApple_thread;


        public MainForm()
        {
            InitializeComponent();
        }


        /*** フォーム ***/
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                // デバイスを切断 //
                myGLCD.End();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 全てのスレッドを終了させる //
            twitterTL_thread.Abort();
            nicoVideo_thread.Abort();
            pixiv_thread.Abort();
            hatsuneMiku_thread.Abort();
            badApple_thread.Abort();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 設定ファイルを読み込み //
            try
            {
                if (File.Exists(settingsFilePath) == true)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fs =
                        new FileStream(settingsFilePath, FileMode.Open);
                    appSettings = (Settings)bf.Deserialize(fs);
                    fs.Close();

                    twitterAuth = new TwitterLib.Authorization(twitterConsumerKey,
                        twitterConsuemrSecret,
                        appSettings.TwitterID,
                        appSettings.TwitterTokenValue,
                        appSettings.TwitterTokenSecret);
                    twitterTL = new TwitterLib.TimeLine(twitterAuth);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            // フォーム //
            this.Text = this.ProductName + "  " + this.ProductVersion;


            // バージョン タブ //
            Label_ProductName.Text = this.ProductName + " Windows 版";
            Label_ProductVersion.Text = "バージョン: " + this.ProductVersion;

            System.Reflection.AssemblyDescriptionAttribute asmdc =
            (System.Reflection.AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(System.Reflection.Assembly.GetExecutingAssembly(),
            typeof(System.Reflection.AssemblyDescriptionAttribute));

            TextBox_License.Text = asmdc.Description;


            // Twitter タブ //
            TwitterTL_ThreadStart();


            // ニコニコ動画 タブ //
            NicoVideo_ComboBox_Category.Items.AddRange(nicoVideoRanking.getCategoryList());
            NicoVideo_ComboBox_Category.SelectedIndex = 0;
            NicoVideoRanking_ThreadStart();

            
            // Pixiv タブ //
            Pixiv_ComboBox_CategorySearch.Items.AddRange(pixivRanking.getCategoryList());
            Pixiv_ComboBox_CategorySearch.SelectedIndex = 0;
            PixivRanking_ThreadStart();


            // 初音ミク タブ //
            HatsuneMiku_CheckBox_BGM.Enabled = Directory.Exists(Path.GetDirectoryName(hatsuneMikuBGMFilePath)) && File.Exists(hatsuneMikuBGMFilePath);
            HatsuneMiku_ThreadStart();


            // Bad Apple //
            BadApple_CheckBox_BGM.Enabled = Directory.Exists(Path.GetDirectoryName(badAppleBGMFilePath)) && File.Exists(badAppleBGMFilePath);
            BadApple_ThreadStart();

            
            try
            {
                // デバイスを接続 //
                myGLCD = new GraphicLCD(7, 6, 5, 4, 0, 1, 2, 3);
                myGLCD.Begin();


                // GLCDサイズを設定 //
                GLCD_top = new Size(myGLCD.Width, myGLCD.Height);
                GLCD_wide = new Size(myGLCD.Height, myGLCD.Width);


                // 起動画像を表示 //
                GLCD_Draw(new Bitmap(startupImagePath));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void TabControl_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (TabControl_Mode.SelectedTab.Text)
            {
                case MODE_SLIDESHOW:  // SlideShow
                    break;

                case MODE_TWITTER:  // Twitter
                    // タイムラインを更新 //
                    TwitterTL_ThreadStart();
                    break;

                case MODE_NICOVIDEO:  // NicoVideo
                    // ランキングを更新 //
                    NicoVideoRanking_ThreadStart();
                    break;

                case MODE_PIXIV:  // Pixiv
                    // ランキングを更新 //
                    PixivRanking_ThreadStart();
                    break;

                case MODE_HATSUNEMIKU:  // 初音ミク
                    HatsuneMiku_ThreadStart();
                    break;

                case MODE_BADAPPLE:  // Bad Apple
                    BadApple_ThreadStart();
                    break;

                default:
                    break;

            }
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            // モード設定 //
            mode = TabControl_Mode.SelectedTab.Text;


            // モードチェック //
            switch (mode)
            {
                case MODE_SLIDESHOW:  // スライドショー
                    if (SlideShow_TextBox_ImageFolderPath.Text == "")
                    {
                        MessageBox.Show("画像フォルダが選択されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;

                case MODE_TWITTER:  // Twitter
                    if (twitterAuth == null)
                    {
                        MessageBox.Show("Twitterのアカウントが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    break;

                case MODE_NICOVIDEO:  // ニコニコ動画
                    break;

                case MODE_PIXIV:  // Pixiv
                    break;

                case MODE_HATSUNEMIKU:  // 初音ミク
                    if (HatsuneMiku_CheckBox_BGM.Checked == true)
                    {
                        hatsuneMikuBGMPlayer.PlayLooping();
                    }
                    break;

                case MODE_BADAPPLE:  // Bad Apple
                    if (BadApple_CheckBox_BGM.Checked == true)
                    {
                        badAppleBGMPlayer.PlayLooping();
                    }
                    break;

                default:
                    break;
            }


            switch (BackgroundWorker_ShowProcess.IsBusy)
            {
                case true:
                    BackgroundWorker_ShowProcess.CancelAsync();
                    MessageBox.Show("停止しました。");
                    GLCD_Draw(new Bitmap(startupImagePath));
                    break;

                case false:
                    BackgroundWorker_ShowProcess.RunWorkerAsync();
                    break;

            }
        }


        /*** スライドショー タブ ***/
        private void SlideShow_ScrollBar_Frequency_Scroll(object sender, ScrollEventArgs e)
        {
            // 値を表示 //
            int minute = SlideShow_ScrollBar_Frequency.Value / 60;
            int second = SlideShow_ScrollBar_Frequency.Value - minute * 60;

            SlideShow_SlideShow_Label_FrequencyTime.Text = ((minute > 0) ? minute + " 分 " : "") + ((second > 0) ? second + " 秒" : "最速");
        }

        private void SlideShow_Button_FolderOpen_Click(object sender, EventArgs e)
        {
            // ダイアログを表示 //
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.Description = "画像が保存されているフォルダを選択してください。";
            fbd.SelectedPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SlideShow_TextBox_ImageFolderPath.Text = fbd.SelectedPath;
            }

            fbd.Dispose();
        }

        private void SlideShow_CheckBox_Rotate_CheckedChanged(object sender, EventArgs e)
        {
            // ラジオボタンを有効化 //
            SlideShow_RadioButton_RotateRight.Enabled = SlideShow_CheckBox_Rotate.Checked;
            SlideShow_RadioButton_RotateLeft.Enabled = SlideShow_CheckBox_Rotate.Checked;
        }


        /*** Twitter タブ ***/
        private void Twitter_Button_Authorization_Click(object sender, EventArgs e)
        {
            try
            {
                // 認証キーを設定 //
                 twitterAuth = new TwitterLib.Authorization(twitterConsumerKey, twitterConsuemrSecret);


                // 認証ページを開く //
                Process.Start(twitterAuth.getAuthUrl());


                // 暗証番号入力フォームを開く //
                AuthForm f = new AuthForm();

                if (f.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    return;
                }


                // PINを設定 //
                twitterAuth.setPIN(f.PIN);
                f.Dispose();


                // メッセージを表示 //
                MessageBox.Show("正常に登録されました。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // リストボックスを更新 //
                TwitterTL_Update();


                // 設定を保存 //
                try
                {
                    appSettings.TwitterID = twitterAuth.TwitterID;
                    appSettings.TwitterTokenValue = twitterAuth.TwitterTokenValue;
                    appSettings.TwitterTokenSecret = twitterAuth.TwitterTokenSecret;

                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream fs =
                        new FileStream(settingsFilePath, FileMode.Create);
                    bf.Serialize(fs, appSettings);
                    fs.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Twitter_Button_Font_Click(object sender, EventArgs e)
        {
            // ダイアログを表示 //
            FontDialog fd = new FontDialog();

            fd.Font = twitterFont;
            fd.MinSize = 5;
            fd.ShowEffects = false;
            
            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                twitterFont = fd.Font;
            }

            fd.Dispose();
        }


        /*** ニコニコ動画 タブ ***/
        private void NicoVideo_ComboBox_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ランキングを更新 //
            NicoVideoRanking_ThreadStart();
        }

        private void NicoVideo_Button_Font_Click(object sender, EventArgs e)
        {
            // ダイアログを表示 //
            FontDialog fd = new FontDialog();

            fd.Font = nicoVideoFont;
            fd.MinSize = 5;
            fd.ShowEffects = false;

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nicoVideoFont = fd.Font;
            }

            fd.Dispose();
        }


        /*** pixiv ***/
        private void Pixiv_ComboBox_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ランキングを更新 //
            PixivRanking_ThreadStart();
        }

        private void Pixiv_ComboBox_CategorySearch_KeyDown(object sender, KeyEventArgs e)
        {
            // ランキングを更新 //
            if (e.KeyCode == Keys.Enter)
            {
                System.Diagnostics.Debug.Print("Enter");
                PixivRanking_ThreadStart();
            }
        }

        private void Pixiv_Button_Font_Click(object sender, EventArgs e)
        {
            // ダイアログを表示 //
            FontDialog fd = new FontDialog();

            fd.Font = nicoVideoFont;
            fd.MinSize = 5;
            fd.ShowEffects = false;

            if (fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                nicoVideoFont = fd.Font;
            }

            fd.Dispose();
        }

        private void Pixiv_CheckBox_Rotate_CheckedChanged(object sender, EventArgs e)
        {
            // ラジオボタンを有効化 //
            Pixiv_RadioButton_RotateRight.Enabled = Pixiv_CheckBox_Rotate.Checked;
            Pixiv_RadioButton_RotateLeft.Enabled = Pixiv_CheckBox_Rotate.Checked;
        }

        
        /*** 表示処理 ***/
        private void BackgroundWorker_ShowProcess_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    switch (mode)
                    {
                        case MODE_SLIDESHOW:  // スライドショー
                            {
                                // 画像ファイルを取得 //
                                List<string> imagePathList = new List<string>();
                                foreach (string imageExtension in imageFormatList)
                                {
                                    imagePathList.AddRange(System.IO.Directory.GetFiles(SlideShow_TextBox_ImageFolderPath.Text,
                                        "*." + imageExtension, System.IO.SearchOption.AllDirectories));
                                }

                                imagePathList.Sort();


                                // 画像ファイルを表示 //
                                while (imagePathList.Count > 0)
                                {
                                    // バックグラウンドワーカーのキャンセル検出 //
                                    if (BackgroundWorker_ShowProcess.CancellationPending == true)
                                    {
                                        return;
                                    }


                                    // 名前順・ランダムに取り出す //
                                    int index = 0;

                                    if (SlideShow_CheckBox_Random.Checked == true)
                                    {
                                        Random rnd = new Random();
                                        index = rnd.Next(imagePathList.Count - 1);
                                    }

                                    BasicEdit campus = new BasicEdit(imagePathList[index]);
                                    imagePathList.RemoveAt(index);


                                    // 画像を回転表示 //
                                    if (SlideShow_CheckBox_Rotate.Checked == true)
                                    {
                                        if (SlideShow_RadioButton_RotateRight.Checked == true)
                                        {
                                            campus.autoRoute(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.RouteType.right);
                                        }
                                        else
                                        {
                                            campus.autoRoute(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.RouteType.left);
                                        }
                                    }


                                    // 画像を拡大表示 //
                                    if (SlideShow_CheckBox_Zoom.Checked == true)
                                    {
                                        campus.campusResize(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.FitType.zoom, Color.White);
                                    }
                                    else
                                    {
                                        campus.campusResize(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.FitType.normal, Color.White);
                                    }


                                    // 表示 //
                                    GLCD_Draw(campus.export(), DisplayType.Wide);


                                    // 表示時間 //
                                    Thread.Sleep(SlideShow_ScrollBar_Frequency.Value * 1000);
                                }
                            }
                            break;

                        case MODE_TWITTER:  // Twitter
                            {
                                // Twitterタイムラインを取得 //
                                TwitterTL_Update();


                                // ツイートを表示 //
                                for (int index = 0; index < twitterTL.Length; index++)
                                {
                                    // バックグラウンドワーカーのキャンセル検出 //
                                    if (BackgroundWorker_ShowProcess.CancellationPending == true)
                                    {
                                        return;
                                    }


                                    Bitmap b = new Bitmap(myGLCD.Height, myGLCD.Width);  // 回転したビットマップ
                                    Graphics g = Graphics.FromImage(b);


                                    // 背景色を設定 //
                                    g.Clear(Color.White);


                                    // 自分宛のリプライを強調表示 //
                                    if (twitterTL.getInReplyToScreenName(index) == twitterAuth.TwitterID)
                                    {
                                        // タイトルの背景色を変更 //
                                        g.FillRectangle(Brushes.LightGreen, 3, 3, 156, 34);

                                        // 音を鳴らす //
                                        if (Twitter_CheckBox_Sound.Checked == true)
                                        {
                                            System.Media.SystemSounds.Asterisk.Play();
                                        }
                                    }


                                    // リツイートアイコンを表示 //
                                    Bitmap reTweetIcon = new Bitmap(twitterRetweetIcon);

                                    if (twitterTL.getRetweeted(index) == true)
                                    {
                                        g.DrawImage(reTweetIcon, 40, 25, 10, 8);
                                    }


                                    // プロフィールイメージを描画 //
                                    g.DrawImage(twitterTL.getProfileImage(index), 5, 5, 30, 30);


                                    // スクリーン名を描画 //
                                    g.DrawString(twitterTL.getScreenName(index), twitterFont, Brushes.Black, 40, 10);


                                    // 時間を表示 //
                                    g.DrawString(twitterTL.getHowOld(index), twitterFont, Brushes.Black, 120, 20);


                                    // テキストを描画 //
                                    g.DrawString(twitterTL.getText(index), twitterFont, Brushes.Black, new RectangleF(3, 40, 159, 92));
                                    g.Dispose();


                                    // 表示 //
                                    GLCD_Draw(b, DisplayType.Wide);
                                    b.Dispose();


                                    // 表示時間 //
                                    Thread.Sleep((int)Twitter_NumericUpDown_UpdateTime.Value * 1000);
                                }
                            }
                            break;

                        case MODE_NICOVIDEO:  // ニコニコ動画
                            {
                                // ランキングを取得 //
                                NicoVideoRanking_Update();


                                // ランキングを表示 //
                                for (int index = 0; index < nicoVideoRanking.Length ; index++)
                                {
                                    // バックグラウンドワーカーのキャンセル検出 //
                                    if (BackgroundWorker_ShowProcess.CancellationPending == true)
                                    {
                                        return;
                                    }


                                    Bitmap b = new Bitmap(myGLCD.Height, myGLCD.Width);  // 回転したビットマップ
                                    Graphics g = Graphics.FromImage(b);


                                    // 背景色を設定 //
                                    g.Clear(Color.White);


                                    // サムネイルを描画 //
                                    g.DrawImage(nicoVideoRanking.getThumbnailImage(index), 5, 35, 43, 33);


                                    // タイトルを描画 //
                                    g.FillRectangle(Brushes.LightBlue, 3, 3, 156, 30);
                                    g.DrawString(nicoVideoRanking.getTitle(index), nicoVideoFont, Brushes.Black, new RectangleF(3, 3, 156, 30));


                                    // 投稿日を表示 //
                                    //g.FillRectangle(Brushes.Magenta, 50, 35, 100, 30);
                                    DateTime dt = nicoVideoRanking.getPubDate(index);
                                    string dt_string =
                                        dt.Year.ToString() + "年" +
                                        dt.Month.ToString() + "月" +
                                        dt.Day.ToString() + "日" +
                                        System.Environment.NewLine +
                                        dt.Hour.ToString() + "時" +
                                        dt.Minute.ToString() + "分" +
                                        dt.Second.ToString() + "秒";
                                    g.DrawString(dt_string, nicoVideoFont, Brushes.Black, new RectangleF(50, 35, 100, 30));


                                    // テキストを描画 //
                                    g.FillRectangle(Brushes.LightGreen, 3, 70, 156, 60);
                                    g.DrawString(nicoVideoRanking.getText(index), nicoVideoFont, Brushes.Black, new RectangleF(3, 70, 156, 60));

                                    g.Dispose();


                                    // 表示 //
                                    GLCD_Draw(b, DisplayType.Wide);
                                    b.Dispose();


                                    // 表示時間 //
                                    System.Threading.Thread.Sleep((int)NicoVideo_NumericUpDown_UpdateTime.Value * 1000);
                                }
                            }
                            break;

                        case MODE_PIXIV:  // Pixiv
                            {
                                // ランキングを取得 //
                                PixivRanking_Update();


                                // ランキングを表示 //
                                for (int index = 0; index < pixivRanking.Length; index++)
                                {
                                    // バックグラウンドワーカーのキャンセル検出 //
                                    if (BackgroundWorker_ShowProcess.CancellationPending == true)
                                    {
                                        return;
                                    }


                                    BasicEdit campus = new BasicEdit(pixivRanking.getLargeImageBitmap(index));
                                    //BasicEdit campus = new BasicEdit(pixivRanking.getThumbnailBitmap(index));


                                    // 画像を回転表示 //
                                    if (Pixiv_CheckBox_Rotate.Checked == true)
                                    {
                                        if (Pixiv_RadioButton_RotateRight.Checked == true)
                                        {
                                            campus.autoRoute(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.RouteType.right);
                                        }
                                        else
                                        {
                                            campus.autoRoute(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.RouteType.left);
                                        }
                                    }


                                    // 画像を拡大表示 //
                                    if (Pixiv_CheckBox_Zoom.Checked == true)
                                    {
                                        campus.campusResize(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.FitType.zoom, Color.White);
                                    }
                                    else
                                    {
                                        campus.campusResize(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.FitType.normal, Color.White);
                                    }

                                    GLCD_Draw(campus.export(), DisplayType.Wide);


                                    //Bitmap b = new Bitmap(myGLCD.Height, myGLCD.Width);  // 回転したビットマップ
                                    //Graphics g = Graphics.FromImage(b);


                                    //// 背景色を設定 //
                                    //g.Clear(Color.White);


                                    //// サムネイルを描画 //
                                    //g.DrawImage(pixivRanking.getThumbnailBitmap(index), 5, 35, 128, 128);


                                    //// タイトルを描画 //
                                    //g.FillRectangle(Brushes.LightBlue, 3, 3, 156, 30);
                                    //g.DrawString(pixivRanking.getTitle(index), pixivFont, Brushes.Black, new RectangleF(3, 3, 156, 30));


                                    //// 投稿日を表示 //
                                    ////g.FillRectangle(Brushes.Magenta, 50, 35, 100, 30);
                                    //DateTime dt = pixivRanking.getSubmitted(index);
                                    //string dt_string =
                                    //    dt.Year.ToString() + "年" +
                                    //    dt.Month.ToString() + "月" +
                                    //    dt.Day.ToString() + "日" +
                                    //    System.Environment.NewLine +
                                    //    dt.Hour.ToString() + "時" +
                                    //    dt.Minute.ToString() + "分" +
                                    //    dt.Second.ToString() + "秒";
                                    //g.DrawString(dt_string, pixivFont, Brushes.Black, new RectangleF(50, 35, 100, 30));


                                    //// テキストを描画 //
                                    ////g.FillRectangle(Brushes.LightGreen, 3, 70, 156, 60);
                                    ////g.DrawString(pixivRanking. .getText(index), nicoVideoFont, Brushes.Black, new RectangleF(3, 70, 156, 60));

                                    //g.Dispose();


                                    //// 表示 //
                                    //GLCD_Draw(b);
                                    //b.Dispose();


                                    // 表示時間 //
                                    System.Threading.Thread.Sleep((int)Pixiv_NumericUpDown_UpdateTime.Value * 1000);
                                }
                            }
                            break;

                        case MODE_HATSUNEMIKU:  // 初音ミク
                            {
                                // 画像ファイルを取得 //
                                List<string> imagePathList = new List<string>();
                                foreach (string imageExtension in imageFormatList)
                                {
                                    imagePathList.AddRange(System.IO.Directory.GetFiles(hatsuneMikuImageFolderPath,
                                        "*." + imageExtension,
                                        System.IO.SearchOption.AllDirectories));
                                }

                                imagePathList.Sort();


                                // 画像ファイルを表示 //
                                for (int index = 0; index < imagePathList.Count; index++)
                                {
                                    // バックグラウンドワーカーのキャンセル検出 //
                                    if (BackgroundWorker_ShowProcess.CancellationPending == true)
                                    {
                                        hatsuneMikuBGMPlayer.Stop();
                                        return;
                                    }

                                    BasicEdit campus = new BasicEdit(imagePathList[index]);


                                    // 画像を拡大表示 //
                                    campus.campusResize(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.FitType.zoom, Color.White);


                                    GLCD_Draw(campus.export(), DisplayType.Wide);
                                }
                            }
                            break;

                        case MODE_BADAPPLE:  // Bad Apple
                            {
                                // 画像ファイルを取得 //
                                List<string> imagePathList = new List<string>();
                                foreach (string imageExtension in imageFormatList)
                                {
                                    imagePathList.AddRange(System.IO.Directory.GetFiles(badAppleImageFolderPath,
                                        "*." + imageExtension,
                                        System.IO.SearchOption.AllDirectories));
                                }

                                imagePathList.Sort();


                                // 画像ファイルを表示 //
                                for (int index = 0; index < imagePathList.Count; index += 2)
                                {
                                    // バックグラウンドワーカーのキャンセル検出 //
                                    if (BackgroundWorker_ShowProcess.CancellationPending == true)
                                    {
                                        badAppleBGMPlayer.Stop();
                                        return;
                                    }

                                    BasicEdit campus = new BasicEdit(imagePathList[index]);


                                    // 画像を拡大表示 //
                                    campus.campusResize(GLCD_wide.Width, GLCD_wide.Height, BasicEdit.FitType.zoom, Color.White);


                                    GLCD_Draw(campus.export(), DisplayType.Wide);
                                }
                            }
                            break;

                        default:
                            return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private enum DisplayType
        {
            Tall,
            Wide,
        }

        private void GLCD_Draw(Bitmap bmp, DisplayType type)
        {
            // 回転する //
            switch (type)
            {
                case DisplayType.Tall:
                    break;

                case DisplayType.Wide:
                    bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;

            }

            // 表示 //
            myGLCD.Draw(bmp);
            System.Diagnostics.Stopwatch sw = new Stopwatch();
            sw.Start();
            myGLCD.Sync();
            sw.Stop();
            System.Diagnostics.Debug.Print(sw.ElapsedMilliseconds.ToString());
        }

        private void GLCD_Draw(Bitmap bmp)
        {
            GLCD_Draw(bmp, DisplayType.Tall);
        }


        private void TwitterTL_ThreadStart()
        {
            if (BackgroundWorker_ShowProcess.IsBusy == false && (twitterTL_thread == null || twitterTL_thread.IsAlive == false))
            {
                twitterTL_thread = new Thread(new ThreadStart(TwitterTL_Update));
                twitterTL_thread.Start();
            }
        }

        private void TwitterTL_Update()
        {
            // 認証を確認 //
            if (twitterAuth == null)
            {
                return;
            }


            try
            {
                // Twitterタイムラインを取得 //
                twitterTL = new TwitterLib.TimeLine(twitterAuth);
                twitterTL.update(twitterTLLength);


                // リストビューにツイートを表示 //
                List<ListViewItem> itemList = new List<ListViewItem>();
                ListViewItem itemx;

                for (int index = 0; index < twitterTL.Length; index++)
                {
                    itemx = new ListViewItem();

                    itemx.Text = twitterTL.getScreenName(index);
                    itemx.SubItems.Add(twitterTL.getText(index));

                    if (twitterTL.getInReplyToScreenName(index) == twitterAuth.TwitterID)
                    {
                        itemx.BackColor = Color.LightGreen;
                    }

                    if (twitterTL.getRetweeted(index) == true)
                    {
                        itemx.BackColor = Color.LightBlue;
                    }

                    itemList.Add(itemx);
                }

                // タイムラインを更新 //
                Invoke(new MethodInvoker(delegate
                {
                    Twitter_ListView_TimeLine.Items.Clear();
                    Twitter_ListView_TimeLine.Items.AddRange(itemList.ToArray());
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void NicoVideoRanking_ThreadStart()
        {
            if (BackgroundWorker_ShowProcess.IsBusy == false && (nicoVideo_thread == null || nicoVideo_thread.IsAlive == false))
            {
                nicoVideo_thread = new Thread(new ThreadStart(NicoVideoRanking_Update));
                nicoVideo_thread.Start();
            }
        }

        private void NicoVideoRanking_Update()
        {
            try
            {
                // NicoVideoランキングを取得 //
                int categoryIndex = 0;
                Invoke(new MethodInvoker(delegate
                {
                    categoryIndex = NicoVideo_ComboBox_Category.SelectedIndex;
                }));
                nicoVideoRanking.update(categoryIndex);


                // リストビューにツイートを表示 //
                List<ListViewItem> itemList = new List<ListViewItem>();
                ListViewItem itemx;

                for (int index = 0; index < nicoVideoRanking.Length; index++)
                {
                    itemx = new ListViewItem();

                    itemx.Text = nicoVideoRanking.getTitle(index);

                    itemList.Add(itemx);
                }


                // ランキングを更新 //
                Invoke(new MethodInvoker(delegate
                {
                    NicoVideo_ListView_Ranking.Items.Clear();
                    NicoVideo_ListView_Ranking.Items.AddRange(itemList.ToArray());
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void PixivRanking_ThreadStart()
        {
            if (BackgroundWorker_ShowProcess.IsBusy == false && (pixiv_thread == null || pixiv_thread.IsAlive == false))
            {
                pixiv_thread = new Thread(new ThreadStart(PixivRanking_Update));
                pixiv_thread.Start();
            }
        }

        private void PixivRanking_Update()
        {
            try
            {
                // Pixivランキングを取得 //
                string target = "";
                Invoke(new MethodInvoker(delegate
                {
                    target = Pixiv_ComboBox_CategorySearch.Text;
                }));
                pixivRanking.update(target);


                // リストビューにツイートを表示 //
                List<ListViewItem> itemList = new List<ListViewItem>();
                ListViewItem itemx;

                for (int index = 0; index < pixivRanking.Length; index++)
                {
                    itemx = new ListViewItem();

                    itemx.Text = pixivRanking.getTitle(index);

                    itemList.Add(itemx);
                }


                // ランキングを更新 //
                Invoke(new MethodInvoker(delegate
                {
                    Pixiv_ListView_Ranking.Items.Clear();
                    Pixiv_ListView_Ranking.Items.AddRange(itemList.ToArray());
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void HatsuneMiku_ThreadStart()
        {
            if (BackgroundWorker_ShowProcess.IsBusy == false && (hatsuneMiku_thread == null || hatsuneMiku_thread.IsAlive == false))
            {
                hatsuneMiku_thread = new Thread(new ThreadStart(HatsuneMiku_Update));
                hatsuneMiku_thread.Start();
            }
        }

        private void HatsuneMiku_Update()
        {
            try
            {
                List<string> HatsuneMiku_ImagePaths = new List<string>();
                Random rnd = new Random();

                foreach (string imageExtension in imageFormatList)
                {
                    HatsuneMiku_ImagePaths.AddRange(System.IO.Directory.GetFiles(hatsuneMikuImageFolderPath,
                        "*." + imageExtension, System.IO.SearchOption.AllDirectories));
                }

                HatsuneMiku_PictureBox.ImageLocation =
                    HatsuneMiku_ImagePaths[rnd.Next(0, HatsuneMiku_ImagePaths.Count - 1)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BadApple_ThreadStart()
        {
            if (BackgroundWorker_ShowProcess.IsBusy == false && (badApple_thread == null || badApple_thread.IsAlive == false))
            {
                badApple_thread = new Thread(new ThreadStart(BadApple_Update));
                badApple_thread.Start();
            }
        }

        private void BadApple_Update()
        {
            try
            {
                List<string> BadApple_ImagePaths = new List<string>();
                Random rnd = new Random();

                foreach (string imageExtension in imageFormatList)
                {
                    BadApple_ImagePaths.AddRange(System.IO.Directory.GetFiles(badAppleImageFolderPath,
                        "*." + imageExtension, System.IO.SearchOption.AllDirectories));
                }

                BadApple_PictureBox.ImageLocation =
                    BadApple_ImagePaths[rnd.Next(BadApple_ImagePaths.Count - 1)];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
