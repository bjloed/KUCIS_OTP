namespace KUCIS_OTP
{
    partial class Client
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.ip_textbox = new System.Windows.Forms.TextBox();
            this.ip_label = new System.Windows.Forms.Label();
            this.socket_monitor = new System.Windows.Forms.TextBox();
            this.socket_button = new System.Windows.Forms.Button();
            this.filepathLabel = new System.Windows.Forms.Label();
            this.textLabel = new System.Windows.Forms.Label();
            this.filepathBox = new System.Windows.Forms.TextBox();
            this.textBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ip_textbox
            // 
            this.ip_textbox.Font = new System.Drawing.Font("굴림", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ip_textbox.Location = new System.Drawing.Point(85, 22);
            this.ip_textbox.Multiline = true;
            this.ip_textbox.Name = "ip_textbox";
            this.ip_textbox.Size = new System.Drawing.Size(233, 32);
            this.ip_textbox.TabIndex = 0;
            // 
            // ip_label
            // 
            this.ip_label.Font = new System.Drawing.Font("나눔스퀘어 ExtraBold", 21F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ip_label.Location = new System.Drawing.Point(21, 22);
            this.ip_label.Name = "ip_label";
            this.ip_label.Size = new System.Drawing.Size(58, 32);
            this.ip_label.TabIndex = 1;
            this.ip_label.Text = "IP: ";
            // 
            // socket_monitor
            // 
            this.socket_monitor.Location = new System.Drawing.Point(27, 181);
            this.socket_monitor.Multiline = true;
            this.socket_monitor.Name = "socket_monitor";
            this.socket_monitor.ReadOnly = true;
            this.socket_monitor.Size = new System.Drawing.Size(378, 279);
            this.socket_monitor.TabIndex = 2;
            // 
            // socket_button
            // 
            this.socket_button.AutoSize = true;
            this.socket_button.Location = new System.Drawing.Point(333, 22);
            this.socket_button.Name = "socket_button";
            this.socket_button.Size = new System.Drawing.Size(72, 32);
            this.socket_button.TabIndex = 3;
            this.socket_button.Text = "Connect";
            this.socket_button.UseVisualStyleBackColor = true;
            this.socket_button.Click += new System.EventHandler(this.socket_button_Click);
            // 
            // filepathLabel
            // 
            this.filepathLabel.Font = new System.Drawing.Font("나눔스퀘어 Bold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.filepathLabel.Location = new System.Drawing.Point(24, 86);
            this.filepathLabel.Name = "filepathLabel";
            this.filepathLabel.Size = new System.Drawing.Size(79, 20);
            this.filepathLabel.TabIndex = 4;
            this.filepathLabel.Text = "File Path:";
            this.filepathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.filepathLabel.Visible = false;
            // 
            // textLabel
            // 
            this.textLabel.Font = new System.Drawing.Font("나눔스퀘어 Bold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textLabel.Location = new System.Drawing.Point(24, 135);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(79, 20);
            this.textLabel.TabIndex = 5;
            this.textLabel.Text = "Plaintext:";
            this.textLabel.Visible = false;
            // 
            // filepathBox
            // 
            this.filepathBox.Location = new System.Drawing.Point(104, 86);
            this.filepathBox.Multiline = true;
            this.filepathBox.Name = "filepathBox";
            this.filepathBox.Size = new System.Drawing.Size(213, 20);
            this.filepathBox.TabIndex = 6;
            this.filepathBox.Visible = false;
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(104, 135);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(213, 20);
            this.textBox.TabIndex = 7;
            this.textBox.Visible = false;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(333, 86);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(71, 70);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "Send";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Visible = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // Client
            // 
            this.ClientSize = new System.Drawing.Size(434, 472);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.filepathBox);
            this.Controls.Add(this.textLabel);
            this.Controls.Add(this.filepathLabel);
            this.Controls.Add(this.socket_button);
            this.Controls.Add(this.socket_monitor);
            this.Controls.Add(this.ip_label);
            this.Controls.Add(this.ip_textbox);
            this.Name = "Client";
            this.Text = "Sender";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ip_textbox;
        private System.Windows.Forms.Label ip_label;
        private System.Windows.Forms.TextBox socket_monitor;
        private System.Windows.Forms.Button socket_button;
        private System.Windows.Forms.Label filepathLabel;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.TextBox filepathBox;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button okButton;
    }
}

