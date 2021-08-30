namespace KUCIS_OTP_Receiver
{
    partial class Server
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
            this.connectionButton = new System.Windows.Forms.Button();
            this.socket_monitor = new System.Windows.Forms.TextBox();
            this.ipLabel = new System.Windows.Forms.Label();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.ipButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectionButton
            // 
            this.connectionButton.BackColor = System.Drawing.SystemColors.ControlLight;
            this.connectionButton.Font = new System.Drawing.Font("나눔스퀘어 Bold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.connectionButton.Location = new System.Drawing.Point(17, 41);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(409, 91);
            this.connectionButton.TabIndex = 0;
            this.connectionButton.Text = "Connection Start";
            this.connectionButton.UseVisualStyleBackColor = false;
            this.connectionButton.Visible = false;
            this.connectionButton.Click += new System.EventHandler(this.connectionButton_Click_1);
            // 
            // socket_monitor
            // 
            this.socket_monitor.Location = new System.Drawing.Point(17, 184);
            this.socket_monitor.Multiline = true;
            this.socket_monitor.Name = "socket_monitor";
            this.socket_monitor.ReadOnly = true;
            this.socket_monitor.Size = new System.Drawing.Size(409, 295);
            this.socket_monitor.TabIndex = 1;
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Font = new System.Drawing.Font("나눔스퀘어 Bold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.ipLabel.Location = new System.Drawing.Point(12, 41);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(54, 30);
            this.ipLabel.TabIndex = 2;
            this.ipLabel.Text = "IP: ";
            // 
            // ipBox
            // 
            this.ipBox.Font = new System.Drawing.Font("굴림", 21F);
            this.ipBox.Location = new System.Drawing.Point(72, 39);
            this.ipBox.Multiline = true;
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(248, 32);
            this.ipBox.TabIndex = 3;
            // 
            // ipButton
            // 
            this.ipButton.Location = new System.Drawing.Point(341, 39);
            this.ipButton.Name = "ipButton";
            this.ipButton.Size = new System.Drawing.Size(85, 32);
            this.ipButton.TabIndex = 4;
            this.ipButton.Text = "Key Send";
            this.ipButton.UseVisualStyleBackColor = true;
            this.ipButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Server
            // 
            this.ClientSize = new System.Drawing.Size(451, 503);
            this.Controls.Add(this.ipButton);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.socket_monitor);
            this.Controls.Add(this.connectionButton);
            this.Name = "Server";
            this.Text = "Receiver";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button connection_button;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button connectionButton;
        private System.Windows.Forms.TextBox socket_monitor;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.TextBox ipBox;
        private System.Windows.Forms.Button ipButton;
    }
}

