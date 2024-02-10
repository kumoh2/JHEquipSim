namespace JHEquipSim
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            sendXmlButton = new Button();
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // sendXmlButton
            // 
            sendXmlButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sendXmlButton.Location = new Point(713, 12);
            sendXmlButton.Name = "sendXmlButton";
            sendXmlButton.Size = new Size(75, 23);
            sendXmlButton.TabIndex = 0;
            sendXmlButton.Text = "button1";
            sendXmlButton.UseVisualStyleBackColor = true;
            sendXmlButton.Click += sendXmlButton_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Location = new Point(12, 41);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(776, 397);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(richTextBox1);
            Controls.Add(sendXmlButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button sendXmlButton;
        private RichTextBox richTextBox1;
    }
}
