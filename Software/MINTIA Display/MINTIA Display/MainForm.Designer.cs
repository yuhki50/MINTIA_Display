namespace MINTIA_Display
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TabControl_Mode = new System.Windows.Forms.TabControl();
            this.TabPage_SlideShow = new System.Windows.Forms.TabPage();
            this.SlideShow_CheckBox_Random = new System.Windows.Forms.CheckBox();
            this.SlideShow_RadioButton_RotateRight = new System.Windows.Forms.RadioButton();
            this.SlideShow_RadioButton_RotateLeft = new System.Windows.Forms.RadioButton();
            this.SlideShow_ScrollBar_Frequency = new System.Windows.Forms.HScrollBar();
            this.SlideShow_CheckBox_Zoom = new System.Windows.Forms.CheckBox();
            this.SlideShow_CheckBox_Rotate = new System.Windows.Forms.CheckBox();
            this.SlideShow_Button_FolderOpen = new System.Windows.Forms.Button();
            this.SlideShow_TextBox_ImageFolderPath = new System.Windows.Forms.TextBox();
            this.SlideShow_Label_ImageFolderTitle = new System.Windows.Forms.Label();
            this.SlideShow_SlideShow_Label_FrequencyTime = new System.Windows.Forms.Label();
            this.SlideShow_Label_FrequencyLow = new System.Windows.Forms.Label();
            this.SlideShow_Label_FrequencyHigh = new System.Windows.Forms.Label();
            this.SlideShow_Label_FrequencyTitle = new System.Windows.Forms.Label();
            this.Twitter_TabPage_Twitter = new System.Windows.Forms.TabPage();
            this.Twitter_Button_Font = new System.Windows.Forms.Button();
            this.Twitter_Button_Authorization = new System.Windows.Forms.Button();
            this.Twitter_ListView_TimeLine = new System.Windows.Forms.ListView();
            this.Twitter_ColumnHeader_ScreenName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Twitter_ColumnHeader_Tweet = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Twitter_Label_Sound = new System.Windows.Forms.Label();
            this.Twitter_CheckBox_Sound = new System.Windows.Forms.CheckBox();
            this.Twitter_Label_Second = new System.Windows.Forms.Label();
            this.Twitter_NumericUpDown_UpdateTime = new System.Windows.Forms.NumericUpDown();
            this.Twitter_Label_UpdateTime = new System.Windows.Forms.Label();
            this.Twitter_Label_Font = new System.Windows.Forms.Label();
            this.Twitter_Label_Authorization = new System.Windows.Forms.Label();
            this.TabPage_NicoVideo = new System.Windows.Forms.TabPage();
            this.NicoVideo_ComboBox_Category = new System.Windows.Forms.ComboBox();
            this.NicoVideo_Label_Font = new System.Windows.Forms.Label();
            this.NicoVideo_Label_Second = new System.Windows.Forms.Label();
            this.NicoVideo_NumericUpDown_UpdateTime = new System.Windows.Forms.NumericUpDown();
            this.NicoVideo_Label_UpdateTime = new System.Windows.Forms.Label();
            this.NicoVideo_Label_Category = new System.Windows.Forms.Label();
            this.NicoVideo_Button_Font = new System.Windows.Forms.Button();
            this.NicoVideo_ListView_Ranking = new System.Windows.Forms.ListView();
            this.Twitter_ColumnHeader_Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TabPage_Pixiv = new System.Windows.Forms.TabPage();
            this.Pixiv_CheckBox_Zoom = new System.Windows.Forms.CheckBox();
            this.Pixiv_CheckBox_Rotate = new System.Windows.Forms.CheckBox();
            this.Pixiv_NumericUpDown_UpdateTime = new System.Windows.Forms.NumericUpDown();
            this.Pixiv_Label_UpdateTime = new System.Windows.Forms.Label();
            this.Pixiv_Label_CategorySearch = new System.Windows.Forms.Label();
            this.Pixiv_Label_Rotate = new System.Windows.Forms.Label();
            this.Pixiv_Label_Second = new System.Windows.Forms.Label();
            this.Pixiv_Label_Font = new System.Windows.Forms.Label();
            this.Pixiv_RadioButton_RotateRight = new System.Windows.Forms.RadioButton();
            this.Pixiv_RadioButton_RotateLeft = new System.Windows.Forms.RadioButton();
            this.Pixiv_ComboBox_CategorySearch = new System.Windows.Forms.ComboBox();
            this.Pixiv_ListView_Ranking = new System.Windows.Forms.ListView();
            this.Pixiv_ColumnHeader_Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Pixiv_Button_Font = new System.Windows.Forms.Button();
            this.TabPage_HatsuneMiku = new System.Windows.Forms.TabPage();
            this.HatsuneMiku_CheckBox_BGM = new System.Windows.Forms.CheckBox();
            this.HatsuneMiku_PictureBox = new System.Windows.Forms.PictureBox();
            this.TabPage_Version = new System.Windows.Forms.TabPage();
            this.TextBox_License = new System.Windows.Forms.TextBox();
            this.Label_ProductVersion = new System.Windows.Forms.Label();
            this.Label_ProductName = new System.Windows.Forms.Label();
            this.Button_Close = new System.Windows.Forms.Button();
            this.BackgroundWorker_ShowProcess = new System.ComponentModel.BackgroundWorker();
            this.TabPage_BadApple = new System.Windows.Forms.TabPage();
            this.BadApple_PictureBox = new System.Windows.Forms.PictureBox();
            this.BadApple_CheckBox_BGM = new System.Windows.Forms.CheckBox();
            this.TabControl_Mode.SuspendLayout();
            this.TabPage_SlideShow.SuspendLayout();
            this.Twitter_TabPage_Twitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Twitter_NumericUpDown_UpdateTime)).BeginInit();
            this.TabPage_NicoVideo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NicoVideo_NumericUpDown_UpdateTime)).BeginInit();
            this.TabPage_Pixiv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pixiv_NumericUpDown_UpdateTime)).BeginInit();
            this.TabPage_HatsuneMiku.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HatsuneMiku_PictureBox)).BeginInit();
            this.TabPage_Version.SuspendLayout();
            this.TabPage_BadApple.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BadApple_PictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // TabControl_Mode
            // 
            this.TabControl_Mode.Controls.Add(this.TabPage_SlideShow);
            this.TabControl_Mode.Controls.Add(this.Twitter_TabPage_Twitter);
            this.TabControl_Mode.Controls.Add(this.TabPage_NicoVideo);
            this.TabControl_Mode.Controls.Add(this.TabPage_Pixiv);
            this.TabControl_Mode.Controls.Add(this.TabPage_HatsuneMiku);
            this.TabControl_Mode.Controls.Add(this.TabPage_BadApple);
            this.TabControl_Mode.Controls.Add(this.TabPage_Version);
            this.TabControl_Mode.Location = new System.Drawing.Point(5, 5);
            this.TabControl_Mode.Name = "TabControl_Mode";
            this.TabControl_Mode.SelectedIndex = 0;
            this.TabControl_Mode.Size = new System.Drawing.Size(483, 270);
            this.TabControl_Mode.TabIndex = 0;
            this.TabControl_Mode.SelectedIndexChanged += new System.EventHandler(this.TabControl_Mode_SelectedIndexChanged);
            // 
            // TabPage_SlideShow
            // 
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_CheckBox_Random);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_RadioButton_RotateRight);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_RadioButton_RotateLeft);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_ScrollBar_Frequency);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_CheckBox_Zoom);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_CheckBox_Rotate);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_Button_FolderOpen);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_TextBox_ImageFolderPath);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_Label_ImageFolderTitle);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_SlideShow_Label_FrequencyTime);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_Label_FrequencyLow);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_Label_FrequencyHigh);
            this.TabPage_SlideShow.Controls.Add(this.SlideShow_Label_FrequencyTitle);
            this.TabPage_SlideShow.Location = new System.Drawing.Point(4, 21);
            this.TabPage_SlideShow.Name = "TabPage_SlideShow";
            this.TabPage_SlideShow.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_SlideShow.Size = new System.Drawing.Size(475, 245);
            this.TabPage_SlideShow.TabIndex = 0;
            this.TabPage_SlideShow.Text = "スライドショー";
            this.TabPage_SlideShow.UseVisualStyleBackColor = true;
            // 
            // SlideShow_CheckBox_Random
            // 
            this.SlideShow_CheckBox_Random.AutoSize = true;
            this.SlideShow_CheckBox_Random.Location = new System.Drawing.Point(20, 210);
            this.SlideShow_CheckBox_Random.Name = "SlideShow_CheckBox_Random";
            this.SlideShow_CheckBox_Random.Size = new System.Drawing.Size(93, 16);
            this.SlideShow_CheckBox_Random.TabIndex = 13;
            this.SlideShow_CheckBox_Random.Text = "ランダムに表示";
            this.SlideShow_CheckBox_Random.UseVisualStyleBackColor = true;
            // 
            // SlideShow_RadioButton_RotateRight
            // 
            this.SlideShow_RadioButton_RotateRight.AutoSize = true;
            this.SlideShow_RadioButton_RotateRight.Enabled = false;
            this.SlideShow_RadioButton_RotateRight.Location = new System.Drawing.Point(360, 150);
            this.SlideShow_RadioButton_RotateRight.Name = "SlideShow_RadioButton_RotateRight";
            this.SlideShow_RadioButton_RotateRight.Size = new System.Drawing.Size(59, 16);
            this.SlideShow_RadioButton_RotateRight.TabIndex = 12;
            this.SlideShow_RadioButton_RotateRight.Text = "右回転";
            this.SlideShow_RadioButton_RotateRight.UseVisualStyleBackColor = true;
            // 
            // SlideShow_RadioButton_RotateLeft
            // 
            this.SlideShow_RadioButton_RotateLeft.AutoSize = true;
            this.SlideShow_RadioButton_RotateLeft.Checked = true;
            this.SlideShow_RadioButton_RotateLeft.Enabled = false;
            this.SlideShow_RadioButton_RotateLeft.Location = new System.Drawing.Point(280, 149);
            this.SlideShow_RadioButton_RotateLeft.Name = "SlideShow_RadioButton_RotateLeft";
            this.SlideShow_RadioButton_RotateLeft.Size = new System.Drawing.Size(59, 16);
            this.SlideShow_RadioButton_RotateLeft.TabIndex = 11;
            this.SlideShow_RadioButton_RotateLeft.TabStop = true;
            this.SlideShow_RadioButton_RotateLeft.Text = "左回転";
            this.SlideShow_RadioButton_RotateLeft.UseVisualStyleBackColor = true;
            // 
            // SlideShow_ScrollBar_Frequency
            // 
            this.SlideShow_ScrollBar_Frequency.Location = new System.Drawing.Point(62, 30);
            this.SlideShow_ScrollBar_Frequency.Maximum = 189;
            this.SlideShow_ScrollBar_Frequency.Name = "SlideShow_ScrollBar_Frequency";
            this.SlideShow_ScrollBar_Frequency.Size = new System.Drawing.Size(350, 17);
            this.SlideShow_ScrollBar_Frequency.TabIndex = 10;
            this.SlideShow_ScrollBar_Frequency.Value = 5;
            this.SlideShow_ScrollBar_Frequency.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SlideShow_ScrollBar_Frequency_Scroll);
            // 
            // SlideShow_CheckBox_Zoom
            // 
            this.SlideShow_CheckBox_Zoom.AutoSize = true;
            this.SlideShow_CheckBox_Zoom.Location = new System.Drawing.Point(20, 180);
            this.SlideShow_CheckBox_Zoom.Name = "SlideShow_CheckBox_Zoom";
            this.SlideShow_CheckBox_Zoom.Size = new System.Drawing.Size(124, 16);
            this.SlideShow_CheckBox_Zoom.TabIndex = 9;
            this.SlideShow_CheckBox_Zoom.Text = "画像を拡大表示する";
            this.SlideShow_CheckBox_Zoom.UseVisualStyleBackColor = true;
            // 
            // SlideShow_CheckBox_Rotate
            // 
            this.SlideShow_CheckBox_Rotate.AutoSize = true;
            this.SlideShow_CheckBox_Rotate.Location = new System.Drawing.Point(20, 150);
            this.SlideShow_CheckBox_Rotate.Name = "SlideShow_CheckBox_Rotate";
            this.SlideShow_CheckBox_Rotate.Size = new System.Drawing.Size(211, 16);
            this.SlideShow_CheckBox_Rotate.TabIndex = 8;
            this.SlideShow_CheckBox_Rotate.Text = "画像をディスプレイに合わせて回転させる";
            this.SlideShow_CheckBox_Rotate.UseVisualStyleBackColor = true;
            this.SlideShow_CheckBox_Rotate.CheckedChanged += new System.EventHandler(this.SlideShow_CheckBox_Rotate_CheckedChanged);
            // 
            // SlideShow_Button_FolderOpen
            // 
            this.SlideShow_Button_FolderOpen.Location = new System.Drawing.Point(380, 98);
            this.SlideShow_Button_FolderOpen.Name = "SlideShow_Button_FolderOpen";
            this.SlideShow_Button_FolderOpen.Size = new System.Drawing.Size(75, 23);
            this.SlideShow_Button_FolderOpen.TabIndex = 7;
            this.SlideShow_Button_FolderOpen.Text = "参照(&B)";
            this.SlideShow_Button_FolderOpen.UseVisualStyleBackColor = true;
            this.SlideShow_Button_FolderOpen.Click += new System.EventHandler(this.SlideShow_Button_FolderOpen_Click);
            // 
            // SlideShow_TextBox_ImageFolderPath
            // 
            this.SlideShow_TextBox_ImageFolderPath.BackColor = System.Drawing.Color.White;
            this.SlideShow_TextBox_ImageFolderPath.Location = new System.Drawing.Point(62, 100);
            this.SlideShow_TextBox_ImageFolderPath.Name = "SlideShow_TextBox_ImageFolderPath";
            this.SlideShow_TextBox_ImageFolderPath.ReadOnly = true;
            this.SlideShow_TextBox_ImageFolderPath.Size = new System.Drawing.Size(312, 19);
            this.SlideShow_TextBox_ImageFolderPath.TabIndex = 6;
            // 
            // SlideShow_Label_ImageFolderTitle
            // 
            this.SlideShow_Label_ImageFolderTitle.AutoSize = true;
            this.SlideShow_Label_ImageFolderTitle.Location = new System.Drawing.Point(10, 84);
            this.SlideShow_Label_ImageFolderTitle.Name = "SlideShow_Label_ImageFolderTitle";
            this.SlideShow_Label_ImageFolderTitle.Size = new System.Drawing.Size(64, 12);
            this.SlideShow_Label_ImageFolderTitle.TabIndex = 5;
            this.SlideShow_Label_ImageFolderTitle.Text = "画像フォルダ";
            // 
            // SlideShow_SlideShow_Label_FrequencyTime
            // 
            this.SlideShow_SlideShow_Label_FrequencyTime.AutoSize = true;
            this.SlideShow_SlideShow_Label_FrequencyTime.Location = new System.Drawing.Point(226, 58);
            this.SlideShow_SlideShow_Label_FrequencyTime.Name = "SlideShow_SlideShow_Label_FrequencyTime";
            this.SlideShow_SlideShow_Label_FrequencyTime.Size = new System.Drawing.Size(27, 12);
            this.SlideShow_SlideShow_Label_FrequencyTime.TabIndex = 4;
            this.SlideShow_SlideShow_Label_FrequencyTime.Text = "5 秒";
            // 
            // SlideShow_Label_FrequencyLow
            // 
            this.SlideShow_Label_FrequencyLow.AutoSize = true;
            this.SlideShow_Label_FrequencyLow.Location = new System.Drawing.Point(430, 33);
            this.SlideShow_Label_FrequencyLow.Name = "SlideShow_Label_FrequencyLow";
            this.SlideShow_Label_FrequencyLow.Size = new System.Drawing.Size(27, 12);
            this.SlideShow_Label_FrequencyLow.TabIndex = 3;
            this.SlideShow_Label_FrequencyLow.Text = "低い";
            // 
            // SlideShow_Label_FrequencyHigh
            // 
            this.SlideShow_Label_FrequencyHigh.AutoSize = true;
            this.SlideShow_Label_FrequencyHigh.Location = new System.Drawing.Point(20, 33);
            this.SlideShow_Label_FrequencyHigh.Name = "SlideShow_Label_FrequencyHigh";
            this.SlideShow_Label_FrequencyHigh.Size = new System.Drawing.Size(27, 12);
            this.SlideShow_Label_FrequencyHigh.TabIndex = 2;
            this.SlideShow_Label_FrequencyHigh.Text = "高い";
            // 
            // SlideShow_Label_FrequencyTitle
            // 
            this.SlideShow_Label_FrequencyTitle.AutoSize = true;
            this.SlideShow_Label_FrequencyTitle.Location = new System.Drawing.Point(10, 10);
            this.SlideShow_Label_FrequencyTitle.Name = "SlideShow_Label_FrequencyTitle";
            this.SlideShow_Label_FrequencyTitle.Size = new System.Drawing.Size(92, 12);
            this.SlideShow_Label_FrequencyTitle.TabIndex = 0;
            this.SlideShow_Label_FrequencyTitle.Text = "画像を変える頻度";
            // 
            // Twitter_TabPage_Twitter
            // 
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_Button_Font);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_Button_Authorization);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_ListView_TimeLine);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_Label_Sound);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_CheckBox_Sound);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_Label_Second);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_NumericUpDown_UpdateTime);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_Label_UpdateTime);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_Label_Font);
            this.Twitter_TabPage_Twitter.Controls.Add(this.Twitter_Label_Authorization);
            this.Twitter_TabPage_Twitter.Location = new System.Drawing.Point(4, 21);
            this.Twitter_TabPage_Twitter.Name = "Twitter_TabPage_Twitter";
            this.Twitter_TabPage_Twitter.Padding = new System.Windows.Forms.Padding(3);
            this.Twitter_TabPage_Twitter.Size = new System.Drawing.Size(475, 245);
            this.Twitter_TabPage_Twitter.TabIndex = 2;
            this.Twitter_TabPage_Twitter.Text = "Twitter";
            this.Twitter_TabPage_Twitter.UseVisualStyleBackColor = true;
            // 
            // Twitter_Button_Font
            // 
            this.Twitter_Button_Font.Location = new System.Drawing.Point(378, 55);
            this.Twitter_Button_Font.Name = "Twitter_Button_Font";
            this.Twitter_Button_Font.Size = new System.Drawing.Size(75, 23);
            this.Twitter_Button_Font.TabIndex = 18;
            this.Twitter_Button_Font.Text = "設定";
            this.Twitter_Button_Font.UseVisualStyleBackColor = true;
            this.Twitter_Button_Font.Click += new System.EventHandler(this.Twitter_Button_Font_Click);
            // 
            // Twitter_Button_Authorization
            // 
            this.Twitter_Button_Authorization.Location = new System.Drawing.Point(378, 15);
            this.Twitter_Button_Authorization.Name = "Twitter_Button_Authorization";
            this.Twitter_Button_Authorization.Size = new System.Drawing.Size(75, 23);
            this.Twitter_Button_Authorization.TabIndex = 17;
            this.Twitter_Button_Authorization.Text = "認証";
            this.Twitter_Button_Authorization.UseVisualStyleBackColor = true;
            this.Twitter_Button_Authorization.Click += new System.EventHandler(this.Twitter_Button_Authorization_Click);
            // 
            // Twitter_ListView_TimeLine
            // 
            this.Twitter_ListView_TimeLine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Twitter_ColumnHeader_ScreenName,
            this.Twitter_ColumnHeader_Tweet});
            this.Twitter_ListView_TimeLine.FullRowSelect = true;
            this.Twitter_ListView_TimeLine.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.Twitter_ListView_TimeLine.Location = new System.Drawing.Point(6, 6);
            this.Twitter_ListView_TimeLine.MultiSelect = false;
            this.Twitter_ListView_TimeLine.Name = "Twitter_ListView_TimeLine";
            this.Twitter_ListView_TimeLine.Size = new System.Drawing.Size(255, 233);
            this.Twitter_ListView_TimeLine.TabIndex = 14;
            this.Twitter_ListView_TimeLine.UseCompatibleStateImageBehavior = false;
            this.Twitter_ListView_TimeLine.View = System.Windows.Forms.View.Details;
            // 
            // Twitter_ColumnHeader_ScreenName
            // 
            this.Twitter_ColumnHeader_ScreenName.Text = "名前";
            // 
            // Twitter_ColumnHeader_Tweet
            // 
            this.Twitter_ColumnHeader_Tweet.Text = "ツイート";
            this.Twitter_ColumnHeader_Tweet.Width = 200;
            // 
            // Twitter_Label_Sound
            // 
            this.Twitter_Label_Sound.AutoSize = true;
            this.Twitter_Label_Sound.Location = new System.Drawing.Point(280, 120);
            this.Twitter_Label_Sound.Name = "Twitter_Label_Sound";
            this.Twitter_Label_Sound.Size = new System.Drawing.Size(93, 12);
            this.Twitter_Label_Sound.TabIndex = 12;
            this.Twitter_Label_Sound.Text = "リプライを音で通知";
            // 
            // Twitter_CheckBox_Sound
            // 
            this.Twitter_CheckBox_Sound.AutoSize = true;
            this.Twitter_CheckBox_Sound.Location = new System.Drawing.Point(400, 118);
            this.Twitter_CheckBox_Sound.Name = "Twitter_CheckBox_Sound";
            this.Twitter_CheckBox_Sound.Size = new System.Drawing.Size(15, 14);
            this.Twitter_CheckBox_Sound.TabIndex = 11;
            this.Twitter_CheckBox_Sound.UseVisualStyleBackColor = true;
            // 
            // Twitter_Label_Second
            // 
            this.Twitter_Label_Second.AutoSize = true;
            this.Twitter_Label_Second.Location = new System.Drawing.Point(437, 90);
            this.Twitter_Label_Second.Name = "Twitter_Label_Second";
            this.Twitter_Label_Second.Size = new System.Drawing.Size(17, 12);
            this.Twitter_Label_Second.TabIndex = 8;
            this.Twitter_Label_Second.Text = "秒";
            // 
            // Twitter_NumericUpDown_UpdateTime
            // 
            this.Twitter_NumericUpDown_UpdateTime.Location = new System.Drawing.Point(378, 88);
            this.Twitter_NumericUpDown_UpdateTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Twitter_NumericUpDown_UpdateTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Twitter_NumericUpDown_UpdateTime.Name = "Twitter_NumericUpDown_UpdateTime";
            this.Twitter_NumericUpDown_UpdateTime.Size = new System.Drawing.Size(53, 19);
            this.Twitter_NumericUpDown_UpdateTime.TabIndex = 7;
            this.Twitter_NumericUpDown_UpdateTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Twitter_Label_UpdateTime
            // 
            this.Twitter_Label_UpdateTime.AutoSize = true;
            this.Twitter_Label_UpdateTime.Location = new System.Drawing.Point(280, 90);
            this.Twitter_Label_UpdateTime.Name = "Twitter_Label_UpdateTime";
            this.Twitter_Label_UpdateTime.Size = new System.Drawing.Size(53, 12);
            this.Twitter_Label_UpdateTime.TabIndex = 6;
            this.Twitter_Label_UpdateTime.Text = "更新間隔";
            // 
            // Twitter_Label_Font
            // 
            this.Twitter_Label_Font.AutoSize = true;
            this.Twitter_Label_Font.Location = new System.Drawing.Point(280, 60);
            this.Twitter_Label_Font.Name = "Twitter_Label_Font";
            this.Twitter_Label_Font.Size = new System.Drawing.Size(38, 12);
            this.Twitter_Label_Font.TabIndex = 4;
            this.Twitter_Label_Font.Text = "フォント";
            // 
            // Twitter_Label_Authorization
            // 
            this.Twitter_Label_Authorization.AutoSize = true;
            this.Twitter_Label_Authorization.Location = new System.Drawing.Point(280, 20);
            this.Twitter_Label_Authorization.Name = "Twitter_Label_Authorization";
            this.Twitter_Label_Authorization.Size = new System.Drawing.Size(69, 12);
            this.Twitter_Label_Authorization.TabIndex = 0;
            this.Twitter_Label_Authorization.Text = "ユーザー認証";
            // 
            // TabPage_NicoVideo
            // 
            this.TabPage_NicoVideo.Controls.Add(this.NicoVideo_ComboBox_Category);
            this.TabPage_NicoVideo.Controls.Add(this.NicoVideo_Label_Font);
            this.TabPage_NicoVideo.Controls.Add(this.NicoVideo_Label_Second);
            this.TabPage_NicoVideo.Controls.Add(this.NicoVideo_NumericUpDown_UpdateTime);
            this.TabPage_NicoVideo.Controls.Add(this.NicoVideo_Label_UpdateTime);
            this.TabPage_NicoVideo.Controls.Add(this.NicoVideo_Label_Category);
            this.TabPage_NicoVideo.Controls.Add(this.NicoVideo_Button_Font);
            this.TabPage_NicoVideo.Controls.Add(this.NicoVideo_ListView_Ranking);
            this.TabPage_NicoVideo.Location = new System.Drawing.Point(4, 21);
            this.TabPage_NicoVideo.Name = "TabPage_NicoVideo";
            this.TabPage_NicoVideo.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_NicoVideo.Size = new System.Drawing.Size(475, 245);
            this.TabPage_NicoVideo.TabIndex = 4;
            this.TabPage_NicoVideo.Text = "ニコニコ動画";
            this.TabPage_NicoVideo.UseVisualStyleBackColor = true;
            // 
            // NicoVideo_ComboBox_Category
            // 
            this.NicoVideo_ComboBox_Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NicoVideo_ComboBox_Category.FormattingEnabled = true;
            this.NicoVideo_ComboBox_Category.Location = new System.Drawing.Point(340, 17);
            this.NicoVideo_ComboBox_Category.Name = "NicoVideo_ComboBox_Category";
            this.NicoVideo_ComboBox_Category.Size = new System.Drawing.Size(113, 20);
            this.NicoVideo_ComboBox_Category.TabIndex = 10;
            this.NicoVideo_ComboBox_Category.SelectedIndexChanged += new System.EventHandler(this.NicoVideo_ComboBox_Category_SelectedIndexChanged);
            // 
            // NicoVideo_Label_Font
            // 
            this.NicoVideo_Label_Font.AutoSize = true;
            this.NicoVideo_Label_Font.Location = new System.Drawing.Point(280, 60);
            this.NicoVideo_Label_Font.Name = "NicoVideo_Label_Font";
            this.NicoVideo_Label_Font.Size = new System.Drawing.Size(38, 12);
            this.NicoVideo_Label_Font.TabIndex = 9;
            this.NicoVideo_Label_Font.Text = "フォント";
            // 
            // NicoVideo_Label_Second
            // 
            this.NicoVideo_Label_Second.AutoSize = true;
            this.NicoVideo_Label_Second.Location = new System.Drawing.Point(437, 90);
            this.NicoVideo_Label_Second.Name = "NicoVideo_Label_Second";
            this.NicoVideo_Label_Second.Size = new System.Drawing.Size(17, 12);
            this.NicoVideo_Label_Second.TabIndex = 5;
            this.NicoVideo_Label_Second.Text = "秒";
            // 
            // NicoVideo_NumericUpDown_UpdateTime
            // 
            this.NicoVideo_NumericUpDown_UpdateTime.Location = new System.Drawing.Point(378, 88);
            this.NicoVideo_NumericUpDown_UpdateTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.NicoVideo_NumericUpDown_UpdateTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NicoVideo_NumericUpDown_UpdateTime.Name = "NicoVideo_NumericUpDown_UpdateTime";
            this.NicoVideo_NumericUpDown_UpdateTime.Size = new System.Drawing.Size(53, 19);
            this.NicoVideo_NumericUpDown_UpdateTime.TabIndex = 4;
            this.NicoVideo_NumericUpDown_UpdateTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // NicoVideo_Label_UpdateTime
            // 
            this.NicoVideo_Label_UpdateTime.AutoSize = true;
            this.NicoVideo_Label_UpdateTime.Location = new System.Drawing.Point(280, 90);
            this.NicoVideo_Label_UpdateTime.Name = "NicoVideo_Label_UpdateTime";
            this.NicoVideo_Label_UpdateTime.Size = new System.Drawing.Size(53, 12);
            this.NicoVideo_Label_UpdateTime.TabIndex = 3;
            this.NicoVideo_Label_UpdateTime.Text = "更新間隔";
            // 
            // NicoVideo_Label_Category
            // 
            this.NicoVideo_Label_Category.AutoSize = true;
            this.NicoVideo_Label_Category.Location = new System.Drawing.Point(280, 20);
            this.NicoVideo_Label_Category.Name = "NicoVideo_Label_Category";
            this.NicoVideo_Label_Category.Size = new System.Drawing.Size(39, 12);
            this.NicoVideo_Label_Category.TabIndex = 2;
            this.NicoVideo_Label_Category.Text = "カテゴリ";
            // 
            // NicoVideo_Button_Font
            // 
            this.NicoVideo_Button_Font.Location = new System.Drawing.Point(378, 55);
            this.NicoVideo_Button_Font.Name = "NicoVideo_Button_Font";
            this.NicoVideo_Button_Font.Size = new System.Drawing.Size(75, 23);
            this.NicoVideo_Button_Font.TabIndex = 1;
            this.NicoVideo_Button_Font.Text = "設定";
            this.NicoVideo_Button_Font.UseVisualStyleBackColor = true;
            this.NicoVideo_Button_Font.Click += new System.EventHandler(this.NicoVideo_Button_Font_Click);
            // 
            // NicoVideo_ListView_Ranking
            // 
            this.NicoVideo_ListView_Ranking.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Twitter_ColumnHeader_Title});
            this.NicoVideo_ListView_Ranking.FullRowSelect = true;
            this.NicoVideo_ListView_Ranking.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.NicoVideo_ListView_Ranking.Location = new System.Drawing.Point(6, 6);
            this.NicoVideo_ListView_Ranking.MultiSelect = false;
            this.NicoVideo_ListView_Ranking.Name = "NicoVideo_ListView_Ranking";
            this.NicoVideo_ListView_Ranking.Size = new System.Drawing.Size(255, 233);
            this.NicoVideo_ListView_Ranking.TabIndex = 0;
            this.NicoVideo_ListView_Ranking.UseCompatibleStateImageBehavior = false;
            this.NicoVideo_ListView_Ranking.View = System.Windows.Forms.View.Details;
            // 
            // Twitter_ColumnHeader_Title
            // 
            this.Twitter_ColumnHeader_Title.Text = "タイトル";
            this.Twitter_ColumnHeader_Title.Width = 250;
            // 
            // TabPage_Pixiv
            // 
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_CheckBox_Zoom);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_CheckBox_Rotate);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_NumericUpDown_UpdateTime);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_Label_UpdateTime);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_Label_CategorySearch);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_Label_Rotate);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_Label_Second);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_Label_Font);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_RadioButton_RotateRight);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_RadioButton_RotateLeft);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_ComboBox_CategorySearch);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_ListView_Ranking);
            this.TabPage_Pixiv.Controls.Add(this.Pixiv_Button_Font);
            this.TabPage_Pixiv.Location = new System.Drawing.Point(4, 21);
            this.TabPage_Pixiv.Name = "TabPage_Pixiv";
            this.TabPage_Pixiv.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Pixiv.Size = new System.Drawing.Size(475, 245);
            this.TabPage_Pixiv.TabIndex = 6;
            this.TabPage_Pixiv.Text = "pixiv";
            this.TabPage_Pixiv.UseVisualStyleBackColor = true;
            // 
            // Pixiv_CheckBox_Zoom
            // 
            this.Pixiv_CheckBox_Zoom.AutoSize = true;
            this.Pixiv_CheckBox_Zoom.Location = new System.Drawing.Point(282, 180);
            this.Pixiv_CheckBox_Zoom.Name = "Pixiv_CheckBox_Zoom";
            this.Pixiv_CheckBox_Zoom.Size = new System.Drawing.Size(124, 16);
            this.Pixiv_CheckBox_Zoom.TabIndex = 13;
            this.Pixiv_CheckBox_Zoom.Text = "画像を拡大表示する";
            this.Pixiv_CheckBox_Zoom.UseVisualStyleBackColor = true;
            // 
            // Pixiv_CheckBox_Rotate
            // 
            this.Pixiv_CheckBox_Rotate.AutoSize = true;
            this.Pixiv_CheckBox_Rotate.Location = new System.Drawing.Point(282, 120);
            this.Pixiv_CheckBox_Rotate.Name = "Pixiv_CheckBox_Rotate";
            this.Pixiv_CheckBox_Rotate.Size = new System.Drawing.Size(184, 16);
            this.Pixiv_CheckBox_Rotate.TabIndex = 12;
            this.Pixiv_CheckBox_Rotate.Text = "画像をディスプレイに合わせて回転";
            this.Pixiv_CheckBox_Rotate.UseVisualStyleBackColor = true;
            this.Pixiv_CheckBox_Rotate.CheckedChanged += new System.EventHandler(this.Pixiv_CheckBox_Rotate_CheckedChanged);
            // 
            // Pixiv_NumericUpDown_UpdateTime
            // 
            this.Pixiv_NumericUpDown_UpdateTime.Location = new System.Drawing.Point(378, 88);
            this.Pixiv_NumericUpDown_UpdateTime.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.Pixiv_NumericUpDown_UpdateTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Pixiv_NumericUpDown_UpdateTime.Name = "Pixiv_NumericUpDown_UpdateTime";
            this.Pixiv_NumericUpDown_UpdateTime.Size = new System.Drawing.Size(53, 19);
            this.Pixiv_NumericUpDown_UpdateTime.TabIndex = 11;
            this.Pixiv_NumericUpDown_UpdateTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Pixiv_Label_UpdateTime
            // 
            this.Pixiv_Label_UpdateTime.AutoSize = true;
            this.Pixiv_Label_UpdateTime.Location = new System.Drawing.Point(280, 90);
            this.Pixiv_Label_UpdateTime.Name = "Pixiv_Label_UpdateTime";
            this.Pixiv_Label_UpdateTime.Size = new System.Drawing.Size(53, 12);
            this.Pixiv_Label_UpdateTime.TabIndex = 10;
            this.Pixiv_Label_UpdateTime.Text = "更新間隔";
            // 
            // Pixiv_Label_CategorySearch
            // 
            this.Pixiv_Label_CategorySearch.AutoSize = true;
            this.Pixiv_Label_CategorySearch.Location = new System.Drawing.Point(280, 20);
            this.Pixiv_Label_CategorySearch.Name = "Pixiv_Label_CategorySearch";
            this.Pixiv_Label_CategorySearch.Size = new System.Drawing.Size(69, 12);
            this.Pixiv_Label_CategorySearch.TabIndex = 9;
            this.Pixiv_Label_CategorySearch.Text = "カテゴリ・検索";
            // 
            // Pixiv_Label_Rotate
            // 
            this.Pixiv_Label_Rotate.AutoSize = true;
            this.Pixiv_Label_Rotate.Location = new System.Drawing.Point(280, 150);
            this.Pixiv_Label_Rotate.Name = "Pixiv_Label_Rotate";
            this.Pixiv_Label_Rotate.Size = new System.Drawing.Size(53, 12);
            this.Pixiv_Label_Rotate.TabIndex = 7;
            this.Pixiv_Label_Rotate.Text = "表示方向";
            // 
            // Pixiv_Label_Second
            // 
            this.Pixiv_Label_Second.AutoSize = true;
            this.Pixiv_Label_Second.Location = new System.Drawing.Point(437, 90);
            this.Pixiv_Label_Second.Name = "Pixiv_Label_Second";
            this.Pixiv_Label_Second.Size = new System.Drawing.Size(17, 12);
            this.Pixiv_Label_Second.TabIndex = 6;
            this.Pixiv_Label_Second.Text = "秒";
            // 
            // Pixiv_Label_Font
            // 
            this.Pixiv_Label_Font.AutoSize = true;
            this.Pixiv_Label_Font.Location = new System.Drawing.Point(280, 60);
            this.Pixiv_Label_Font.Name = "Pixiv_Label_Font";
            this.Pixiv_Label_Font.Size = new System.Drawing.Size(38, 12);
            this.Pixiv_Label_Font.TabIndex = 5;
            this.Pixiv_Label_Font.Text = "フォント";
            // 
            // Pixiv_RadioButton_RotateRight
            // 
            this.Pixiv_RadioButton_RotateRight.AutoSize = true;
            this.Pixiv_RadioButton_RotateRight.Enabled = false;
            this.Pixiv_RadioButton_RotateRight.Location = new System.Drawing.Point(418, 148);
            this.Pixiv_RadioButton_RotateRight.Name = "Pixiv_RadioButton_RotateRight";
            this.Pixiv_RadioButton_RotateRight.Size = new System.Drawing.Size(35, 16);
            this.Pixiv_RadioButton_RotateRight.TabIndex = 4;
            this.Pixiv_RadioButton_RotateRight.Text = "右";
            this.Pixiv_RadioButton_RotateRight.UseVisualStyleBackColor = true;
            // 
            // Pixiv_RadioButton_RotateLeft
            // 
            this.Pixiv_RadioButton_RotateLeft.AutoSize = true;
            this.Pixiv_RadioButton_RotateLeft.Checked = true;
            this.Pixiv_RadioButton_RotateLeft.Enabled = false;
            this.Pixiv_RadioButton_RotateLeft.Location = new System.Drawing.Point(377, 148);
            this.Pixiv_RadioButton_RotateLeft.Name = "Pixiv_RadioButton_RotateLeft";
            this.Pixiv_RadioButton_RotateLeft.Size = new System.Drawing.Size(35, 16);
            this.Pixiv_RadioButton_RotateLeft.TabIndex = 3;
            this.Pixiv_RadioButton_RotateLeft.TabStop = true;
            this.Pixiv_RadioButton_RotateLeft.Text = "左";
            this.Pixiv_RadioButton_RotateLeft.UseVisualStyleBackColor = true;
            // 
            // Pixiv_ComboBox_CategorySearch
            // 
            this.Pixiv_ComboBox_CategorySearch.FormattingEnabled = true;
            this.Pixiv_ComboBox_CategorySearch.Location = new System.Drawing.Point(353, 17);
            this.Pixiv_ComboBox_CategorySearch.Name = "Pixiv_ComboBox_CategorySearch";
            this.Pixiv_ComboBox_CategorySearch.Size = new System.Drawing.Size(100, 20);
            this.Pixiv_ComboBox_CategorySearch.TabIndex = 2;
            this.Pixiv_ComboBox_CategorySearch.SelectedIndexChanged += new System.EventHandler(this.Pixiv_ComboBox_Category_SelectedIndexChanged);
            this.Pixiv_ComboBox_CategorySearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Pixiv_ComboBox_CategorySearch_KeyDown);
            // 
            // Pixiv_ListView_Ranking
            // 
            this.Pixiv_ListView_Ranking.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Pixiv_ColumnHeader_Title});
            this.Pixiv_ListView_Ranking.FullRowSelect = true;
            this.Pixiv_ListView_Ranking.Location = new System.Drawing.Point(6, 6);
            this.Pixiv_ListView_Ranking.MultiSelect = false;
            this.Pixiv_ListView_Ranking.Name = "Pixiv_ListView_Ranking";
            this.Pixiv_ListView_Ranking.Size = new System.Drawing.Size(255, 233);
            this.Pixiv_ListView_Ranking.TabIndex = 1;
            this.Pixiv_ListView_Ranking.UseCompatibleStateImageBehavior = false;
            this.Pixiv_ListView_Ranking.View = System.Windows.Forms.View.Details;
            // 
            // Pixiv_ColumnHeader_Title
            // 
            this.Pixiv_ColumnHeader_Title.Text = "タイトル";
            this.Pixiv_ColumnHeader_Title.Width = 250;
            // 
            // Pixiv_Button_Font
            // 
            this.Pixiv_Button_Font.Location = new System.Drawing.Point(378, 55);
            this.Pixiv_Button_Font.Name = "Pixiv_Button_Font";
            this.Pixiv_Button_Font.Size = new System.Drawing.Size(75, 23);
            this.Pixiv_Button_Font.TabIndex = 0;
            this.Pixiv_Button_Font.Text = "設定";
            this.Pixiv_Button_Font.UseVisualStyleBackColor = true;
            this.Pixiv_Button_Font.Click += new System.EventHandler(this.Pixiv_Button_Font_Click);
            // 
            // TabPage_HatsuneMiku
            // 
            this.TabPage_HatsuneMiku.Controls.Add(this.HatsuneMiku_CheckBox_BGM);
            this.TabPage_HatsuneMiku.Controls.Add(this.HatsuneMiku_PictureBox);
            this.TabPage_HatsuneMiku.Location = new System.Drawing.Point(4, 21);
            this.TabPage_HatsuneMiku.Name = "TabPage_HatsuneMiku";
            this.TabPage_HatsuneMiku.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_HatsuneMiku.Size = new System.Drawing.Size(475, 245);
            this.TabPage_HatsuneMiku.TabIndex = 5;
            this.TabPage_HatsuneMiku.Text = "初音ミク";
            this.TabPage_HatsuneMiku.UseVisualStyleBackColor = true;
            // 
            // HatsuneMiku_CheckBox_BGM
            // 
            this.HatsuneMiku_CheckBox_BGM.AutoSize = true;
            this.HatsuneMiku_CheckBox_BGM.Location = new System.Drawing.Point(280, 20);
            this.HatsuneMiku_CheckBox_BGM.Name = "HatsuneMiku_CheckBox_BGM";
            this.HatsuneMiku_CheckBox_BGM.Size = new System.Drawing.Size(101, 16);
            this.HatsuneMiku_CheckBox_BGM.TabIndex = 4;
            this.HatsuneMiku_CheckBox_BGM.Text = "BGMを再生する";
            this.HatsuneMiku_CheckBox_BGM.UseVisualStyleBackColor = true;
            // 
            // HatsuneMiku_PictureBox
            // 
            this.HatsuneMiku_PictureBox.Location = new System.Drawing.Point(6, 6);
            this.HatsuneMiku_PictureBox.Name = "HatsuneMiku_PictureBox";
            this.HatsuneMiku_PictureBox.Size = new System.Drawing.Size(255, 233);
            this.HatsuneMiku_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.HatsuneMiku_PictureBox.TabIndex = 3;
            this.HatsuneMiku_PictureBox.TabStop = false;
            // 
            // TabPage_Version
            // 
            this.TabPage_Version.Controls.Add(this.TextBox_License);
            this.TabPage_Version.Controls.Add(this.Label_ProductVersion);
            this.TabPage_Version.Controls.Add(this.Label_ProductName);
            this.TabPage_Version.Location = new System.Drawing.Point(4, 21);
            this.TabPage_Version.Name = "TabPage_Version";
            this.TabPage_Version.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_Version.Size = new System.Drawing.Size(475, 245);
            this.TabPage_Version.TabIndex = 1;
            this.TabPage_Version.Text = "バージョン情報";
            this.TabPage_Version.UseVisualStyleBackColor = true;
            // 
            // TextBox_License
            // 
            this.TextBox_License.BackColor = System.Drawing.Color.White;
            this.TextBox_License.Location = new System.Drawing.Point(22, 80);
            this.TextBox_License.Multiline = true;
            this.TextBox_License.Name = "TextBox_License";
            this.TextBox_License.ReadOnly = true;
            this.TextBox_License.Size = new System.Drawing.Size(430, 140);
            this.TextBox_License.TabIndex = 2;
            // 
            // Label_ProductVersion
            // 
            this.Label_ProductVersion.AutoSize = true;
            this.Label_ProductVersion.Location = new System.Drawing.Point(20, 50);
            this.Label_ProductVersion.Name = "Label_ProductVersion";
            this.Label_ProductVersion.Size = new System.Drawing.Size(114, 12);
            this.Label_ProductVersion.TabIndex = 1;
            this.Label_ProductVersion.Text = "Label_ProductVersion";
            // 
            // Label_ProductName
            // 
            this.Label_ProductName.AutoSize = true;
            this.Label_ProductName.Location = new System.Drawing.Point(20, 20);
            this.Label_ProductName.Name = "Label_ProductName";
            this.Label_ProductName.Size = new System.Drawing.Size(104, 12);
            this.Label_ProductName.TabIndex = 0;
            this.Label_ProductName.Text = "Label_ProductName";
            // 
            // Button_Close
            // 
            this.Button_Close.Location = new System.Drawing.Point(396, 281);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(80, 23);
            this.Button_Close.TabIndex = 1;
            this.Button_Close.Text = "閉じる";
            this.Button_Close.UseVisualStyleBackColor = true;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // BackgroundWorker_ShowProcess
            // 
            this.BackgroundWorker_ShowProcess.WorkerSupportsCancellation = true;
            this.BackgroundWorker_ShowProcess.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundWorker_ShowProcess_DoWork);
            // 
            // TabPage_BadApple
            // 
            this.TabPage_BadApple.Controls.Add(this.BadApple_CheckBox_BGM);
            this.TabPage_BadApple.Controls.Add(this.BadApple_PictureBox);
            this.TabPage_BadApple.Location = new System.Drawing.Point(4, 21);
            this.TabPage_BadApple.Name = "TabPage_BadApple";
            this.TabPage_BadApple.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage_BadApple.Size = new System.Drawing.Size(475, 245);
            this.TabPage_BadApple.TabIndex = 7;
            this.TabPage_BadApple.Text = "Bad Apple!!";
            this.TabPage_BadApple.UseVisualStyleBackColor = true;
            // 
            // BadApple_PictureBox
            // 
            this.BadApple_PictureBox.Location = new System.Drawing.Point(6, 6);
            this.BadApple_PictureBox.Name = "BadApple_PictureBox";
            this.BadApple_PictureBox.Size = new System.Drawing.Size(255, 233);
            this.BadApple_PictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.BadApple_PictureBox.TabIndex = 4;
            this.BadApple_PictureBox.TabStop = false;
            // 
            // BadApple_CheckBox_BGM
            // 
            this.BadApple_CheckBox_BGM.AutoSize = true;
            this.BadApple_CheckBox_BGM.Location = new System.Drawing.Point(280, 20);
            this.BadApple_CheckBox_BGM.Name = "BadApple_CheckBox_BGM";
            this.BadApple_CheckBox_BGM.Size = new System.Drawing.Size(101, 16);
            this.BadApple_CheckBox_BGM.TabIndex = 5;
            this.BadApple_CheckBox_BGM.Text = "BGMを再生する";
            this.BadApple_CheckBox_BGM.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 316);
            this.Controls.Add(this.Button_Close);
            this.Controls.Add(this.TabControl_Mode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TabControl_Mode.ResumeLayout(false);
            this.TabPage_SlideShow.ResumeLayout(false);
            this.TabPage_SlideShow.PerformLayout();
            this.Twitter_TabPage_Twitter.ResumeLayout(false);
            this.Twitter_TabPage_Twitter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Twitter_NumericUpDown_UpdateTime)).EndInit();
            this.TabPage_NicoVideo.ResumeLayout(false);
            this.TabPage_NicoVideo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NicoVideo_NumericUpDown_UpdateTime)).EndInit();
            this.TabPage_Pixiv.ResumeLayout(false);
            this.TabPage_Pixiv.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pixiv_NumericUpDown_UpdateTime)).EndInit();
            this.TabPage_HatsuneMiku.ResumeLayout(false);
            this.TabPage_HatsuneMiku.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HatsuneMiku_PictureBox)).EndInit();
            this.TabPage_Version.ResumeLayout(false);
            this.TabPage_Version.PerformLayout();
            this.TabPage_BadApple.ResumeLayout(false);
            this.TabPage_BadApple.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BadApple_PictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabControl_Mode;
        private System.Windows.Forms.TabPage TabPage_SlideShow;
        private System.Windows.Forms.TabPage TabPage_Version;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.Label SlideShow_Label_FrequencyTitle;
        private System.Windows.Forms.Button SlideShow_Button_FolderOpen;
        private System.Windows.Forms.TextBox SlideShow_TextBox_ImageFolderPath;
        private System.Windows.Forms.Label SlideShow_Label_ImageFolderTitle;
        private System.Windows.Forms.Label SlideShow_SlideShow_Label_FrequencyTime;
        private System.Windows.Forms.Label SlideShow_Label_FrequencyLow;
        private System.Windows.Forms.Label SlideShow_Label_FrequencyHigh;
        private System.Windows.Forms.CheckBox SlideShow_CheckBox_Rotate;
        private System.Windows.Forms.CheckBox SlideShow_CheckBox_Zoom;
        private System.Windows.Forms.HScrollBar SlideShow_ScrollBar_Frequency;
        private System.ComponentModel.BackgroundWorker BackgroundWorker_ShowProcess;
        private System.Windows.Forms.RadioButton SlideShow_RadioButton_RotateRight;
        private System.Windows.Forms.RadioButton SlideShow_RadioButton_RotateLeft;
        private System.Windows.Forms.CheckBox SlideShow_CheckBox_Random;
        private System.Windows.Forms.TextBox TextBox_License;
        private System.Windows.Forms.Label Label_ProductVersion;
        private System.Windows.Forms.Label Label_ProductName;
        private System.Windows.Forms.TabPage Twitter_TabPage_Twitter;
        private System.Windows.Forms.Label Twitter_Label_Authorization;
        private System.Windows.Forms.Label Twitter_Label_Second;
        private System.Windows.Forms.NumericUpDown Twitter_NumericUpDown_UpdateTime;
        private System.Windows.Forms.Label Twitter_Label_UpdateTime;
        private System.Windows.Forms.Label Twitter_Label_Font;
        private System.Windows.Forms.Label Twitter_Label_Sound;
        private System.Windows.Forms.CheckBox Twitter_CheckBox_Sound;
        private System.Windows.Forms.ListView Twitter_ListView_TimeLine;
        private System.Windows.Forms.Button Twitter_Button_Authorization;
        private System.Windows.Forms.Button Twitter_Button_Font;
        private System.Windows.Forms.ColumnHeader Twitter_ColumnHeader_ScreenName;
        private System.Windows.Forms.ColumnHeader Twitter_ColumnHeader_Tweet;
        private System.Windows.Forms.TabPage TabPage_NicoVideo;
        private System.Windows.Forms.Label NicoVideo_Label_Category;
        private System.Windows.Forms.Button NicoVideo_Button_Font;
        private System.Windows.Forms.ListView NicoVideo_ListView_Ranking;
        private System.Windows.Forms.ComboBox NicoVideo_ComboBox_Category;
        private System.Windows.Forms.Label NicoVideo_Label_Font;
        private System.Windows.Forms.Label NicoVideo_Label_Second;
        private System.Windows.Forms.NumericUpDown NicoVideo_NumericUpDown_UpdateTime;
        private System.Windows.Forms.Label NicoVideo_Label_UpdateTime;
        private System.Windows.Forms.ColumnHeader Twitter_ColumnHeader_Title;
        private System.Windows.Forms.TabPage TabPage_HatsuneMiku;
        private System.Windows.Forms.PictureBox HatsuneMiku_PictureBox;
        private System.Windows.Forms.TabPage TabPage_Pixiv;
        private System.Windows.Forms.Button Pixiv_Button_Font;
        private System.Windows.Forms.Label Pixiv_Label_CategorySearch;
        private System.Windows.Forms.Label Pixiv_Label_Rotate;
        private System.Windows.Forms.Label Pixiv_Label_Second;
        private System.Windows.Forms.Label Pixiv_Label_Font;
        private System.Windows.Forms.ComboBox Pixiv_ComboBox_CategorySearch;
        private System.Windows.Forms.ListView Pixiv_ListView_Ranking;
        private System.Windows.Forms.NumericUpDown Pixiv_NumericUpDown_UpdateTime;
        private System.Windows.Forms.Label Pixiv_Label_UpdateTime;
        private System.Windows.Forms.ColumnHeader Pixiv_ColumnHeader_Title;
        private System.Windows.Forms.CheckBox Pixiv_CheckBox_Rotate;
        private System.Windows.Forms.CheckBox Pixiv_CheckBox_Zoom;
        private System.Windows.Forms.CheckBox HatsuneMiku_CheckBox_BGM;
        private System.Windows.Forms.RadioButton Pixiv_RadioButton_RotateRight;
        private System.Windows.Forms.RadioButton Pixiv_RadioButton_RotateLeft;
        private System.Windows.Forms.TabPage TabPage_BadApple;
        private System.Windows.Forms.CheckBox BadApple_CheckBox_BGM;
        private System.Windows.Forms.PictureBox BadApple_PictureBox;
    }
}

