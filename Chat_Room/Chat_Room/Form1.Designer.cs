namespace Chat_Room {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.Public_Chat_textbox = new System.Windows.Forms.TextBox();
            this.Private_Chat_textbox = new System.Windows.Forms.TextBox();
            this.Text_Input = new System.Windows.Forms.TextBox();
            this.Current_User_textbox = new System.Windows.Forms.TextBox();
            this.Login_button = new System.Windows.Forms.Button();
            this.Logout_button = new System.Windows.Forms.Button();
            this.Request_Private_Chat_button = new System.Windows.Forms.Button();
            this.End_Private_Chat_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Send_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.user_list = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // Public_Chat_textbox
            // 
            this.Public_Chat_textbox.Location = new System.Drawing.Point(297, 69);
            this.Public_Chat_textbox.Multiline = true;
            this.Public_Chat_textbox.Name = "Public_Chat_textbox";
            this.Public_Chat_textbox.Size = new System.Drawing.Size(313, 159);
            this.Public_Chat_textbox.TabIndex = 0;
            // 
            // Private_Chat_textbox
            // 
            this.Private_Chat_textbox.Location = new System.Drawing.Point(297, 259);
            this.Private_Chat_textbox.Multiline = true;
            this.Private_Chat_textbox.Name = "Private_Chat_textbox";
            this.Private_Chat_textbox.Size = new System.Drawing.Size(313, 170);
            this.Private_Chat_textbox.TabIndex = 1;
            // 
            // Text_Input
            // 
            this.Text_Input.Location = new System.Drawing.Point(297, 482);
            this.Text_Input.Name = "Text_Input";
            this.Text_Input.Size = new System.Drawing.Size(313, 20);
            this.Text_Input.TabIndex = 2;
            // 
            // Current_User_textbox
            // 
            this.Current_User_textbox.Location = new System.Drawing.Point(429, 12);
            this.Current_User_textbox.Name = "Current_User_textbox";
            this.Current_User_textbox.Size = new System.Drawing.Size(115, 20);
            this.Current_User_textbox.TabIndex = 4;
            // 
            // Login_button
            // 
            this.Login_button.Location = new System.Drawing.Point(12, 329);
            this.Login_button.Name = "Login_button";
            this.Login_button.Size = new System.Drawing.Size(100, 36);
            this.Login_button.TabIndex = 5;
            this.Login_button.Text = "Login";
            this.Login_button.UseVisualStyleBackColor = true;
            this.Login_button.Click += new System.EventHandler(this.Login_button_Click);
            // 
            // Logout_button
            // 
            this.Logout_button.Location = new System.Drawing.Point(118, 329);
            this.Logout_button.Name = "Logout_button";
            this.Logout_button.Size = new System.Drawing.Size(100, 36);
            this.Logout_button.TabIndex = 6;
            this.Logout_button.Text = "Logout";
            this.Logout_button.UseVisualStyleBackColor = true;
            // 
            // Request_Private_Chat_button
            // 
            this.Request_Private_Chat_button.Location = new System.Drawing.Point(12, 392);
            this.Request_Private_Chat_button.Name = "Request_Private_Chat_button";
            this.Request_Private_Chat_button.Size = new System.Drawing.Size(100, 37);
            this.Request_Private_Chat_button.TabIndex = 7;
            this.Request_Private_Chat_button.Text = "Request Private Chat";
            this.Request_Private_Chat_button.UseVisualStyleBackColor = true;
            // 
            // End_Private_Chat_button
            // 
            this.End_Private_Chat_button.Location = new System.Drawing.Point(118, 392);
            this.End_Private_Chat_button.Name = "End_Private_Chat_button";
            this.End_Private_Chat_button.Size = new System.Drawing.Size(100, 37);
            this.End_Private_Chat_button.TabIndex = 8;
            this.End_Private_Chat_button.Text = "End Private Chat";
            this.End_Private_Chat_button.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "User Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(294, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Public Chat";
            // 
            // Send_button
            // 
            this.Send_button.Location = new System.Drawing.Point(510, 508);
            this.Send_button.Name = "Send_button";
            this.Send_button.Size = new System.Drawing.Size(100, 37);
            this.Send_button.TabIndex = 11;
            this.Send_button.Text = "Send";
            this.Send_button.UseVisualStyleBackColor = true;
            this.Send_button.Click += new System.EventHandler(this.Send_button_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(294, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Private Chat";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(294, 466);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Text Input";
            // 
            // user_list
            // 
            this.user_list.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.user_list.FormattingEnabled = true;
            this.user_list.Location = new System.Drawing.Point(15, 48);
            this.user_list.Name = "user_list";
            this.user_list.Size = new System.Drawing.Size(203, 251);
            this.user_list.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(622, 576);
            this.Controls.Add(this.user_list);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Send_button);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.End_Private_Chat_button);
            this.Controls.Add(this.Request_Private_Chat_button);
            this.Controls.Add(this.Logout_button);
            this.Controls.Add(this.Login_button);
            this.Controls.Add(this.Current_User_textbox);
            this.Controls.Add(this.Text_Input);
            this.Controls.Add(this.Private_Chat_textbox);
            this.Controls.Add(this.Public_Chat_textbox);
            this.Name = "Form1";
            this.Text = "Chat Room";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Public_Chat_textbox;
        private System.Windows.Forms.TextBox Private_Chat_textbox;
        private System.Windows.Forms.TextBox Text_Input;
        private System.Windows.Forms.TextBox Current_User_textbox;
        private System.Windows.Forms.Button Login_button;
        private System.Windows.Forms.Button Logout_button;
        private System.Windows.Forms.Button Request_Private_Chat_button;
        private System.Windows.Forms.Button End_Private_Chat_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Send_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox user_list;
    }
}

