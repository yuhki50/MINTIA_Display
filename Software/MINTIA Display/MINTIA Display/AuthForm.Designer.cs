namespace MINTIA_Display
{
    partial class AuthForm
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
            this.Label_Description = new System.Windows.Forms.Label();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Label_PIN = new System.Windows.Forms.Label();
            this.TextBox_PIN = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label_Description
            // 
            this.Label_Description.AutoSize = true;
            this.Label_Description.Location = new System.Drawing.Point(25, 15);
            this.Label_Description.Name = "Label_Description";
            this.Label_Description.Size = new System.Drawing.Size(242, 12);
            this.Label_Description.TabIndex = 0;
            this.Label_Description.Text = "ブラウザに表示された暗証番号を入力してください。";
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(68, 80);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 2;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(149, 80);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 3;
            this.Button_Cancel.Text = "キャンセル";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Label_PIN
            // 
            this.Label_PIN.AutoSize = true;
            this.Label_PIN.Location = new System.Drawing.Point(20, 50);
            this.Label_PIN.Name = "Label_PIN";
            this.Label_PIN.Size = new System.Drawing.Size(53, 12);
            this.Label_PIN.TabIndex = 5;
            this.Label_PIN.Text = "暗証番号";
            // 
            // TextBox_PIN
            // 
            this.TextBox_PIN.Location = new System.Drawing.Point(120, 47);
            this.TextBox_PIN.Name = "TextBox_PIN";
            this.TextBox_PIN.Size = new System.Drawing.Size(120, 19);
            this.TextBox_PIN.TabIndex = 6;
            this.TextBox_PIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 118);
            this.Controls.Add(this.TextBox_PIN);
            this.Controls.Add(this.Label_PIN);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.Label_Description);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AuthForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ユーザー認証";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Description;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label Label_PIN;
        private System.Windows.Forms.TextBox TextBox_PIN;
    }
}