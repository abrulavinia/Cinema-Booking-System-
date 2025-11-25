namespace CinemaBooking
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            labelLoginManager = new Label();
            buttonLogin = new Button();
            textBoxUsername = new TextBox();
            textBoxParola = new TextBox();
            labelUsername = new Label();
            labelParola = new Label();
            panel1 = new Panel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // labelLoginManager
            // 
            labelLoginManager.AutoSize = true;
            labelLoginManager.BackColor = Color.Transparent;
            labelLoginManager.Font = new Font("Consolas", 19.8000011F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            labelLoginManager.Location = new Point(380, 61);
            labelLoginManager.Name = "labelLoginManager";
            labelLoginManager.Size = new Size(264, 40);
            labelLoginManager.TabIndex = 0;
            labelLoginManager.Text = "Login Manager";
            labelLoginManager.TextAlign = ContentAlignment.TopCenter;
            // 
            // buttonLogin
            // 
            buttonLogin.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            buttonLogin.Location = new Point(126, 147);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(109, 37);
            buttonLogin.TabIndex = 1;
            buttonLogin.Text = "Log in\r\n";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // textBoxUsername
            // 
            textBoxUsername.Font = new Font("Segoe UI", 12F);
            textBoxUsername.Location = new Point(198, 28);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(160, 34);
            textBoxUsername.TabIndex = 2;
            // 
            // textBoxParola
            // 
            textBoxParola.Font = new Font("Segoe UI", 12F);
            textBoxParola.Location = new Point(198, 85);
            textBoxParola.Name = "textBoxParola";
            textBoxParola.Size = new Size(160, 34);
            textBoxParola.TabIndex = 3;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.BackColor = Color.Transparent;
            labelUsername.Font = new Font("Segoe UI", 16.2F);
            labelUsername.ForeColor = SystemColors.ActiveCaptionText;
            labelUsername.Location = new Point(27, 24);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(142, 38);
            labelUsername.TabIndex = 4;
            labelUsername.Text = "Username";
         
            // 
            // labelParola
            // 
            labelParola.AutoSize = true;
            labelParola.BackColor = Color.Transparent;
            labelParola.Font = new Font("Segoe UI", 16.2F);
            labelParola.ForeColor = SystemColors.ActiveCaptionText;
            labelParola.Location = new Point(27, 81);
            labelParola.Name = "labelParola";
            labelParola.Size = new Size(93, 38);
            labelParola.TabIndex = 5;
            labelParola.Text = "Parola";
            // 
            // panel1
            // 
            panel1.BackColor = Color.MistyRose;
            panel1.Controls.Add(labelUsername);
            panel1.Controls.Add(labelParola);
            panel1.Controls.Add(buttonLogin);
            panel1.Controls.Add(textBoxUsername);
            panel1.Controls.Add(textBoxParola);
            panel1.Location = new Point(316, 137);
            panel1.Name = "panel1";
            panel1.Size = new Size(391, 257);
            panel1.TabIndex = 6;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Cinema_login;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(971, 591);
            Controls.Add(panel1);
            Controls.Add(labelLoginManager);
            Name = "Login";
            Text = "Login";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelLoginManager;
        private Button buttonLogin;
        private TextBox textBoxUsername;
        private TextBox textBoxParola;
        private Label labelUsername;
        private Label labelParola;
        private Panel panel1;
    }
}