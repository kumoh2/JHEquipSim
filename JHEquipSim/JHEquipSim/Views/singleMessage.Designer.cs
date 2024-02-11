namespace JHEquipSim.Views
{
    partial class singleMessage
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
            richTextBox1 = new RichTextBox();
            sendXmlButton = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Location = new Point(3, 29);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(792, 418);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // sendXmlButton
            // 
            sendXmlButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sendXmlButton.Location = new Point(720, 0);
            sendXmlButton.Name = "sendXmlButton";
            sendXmlButton.Size = new Size(75, 23);
            sendXmlButton.TabIndex = 2;
            sendXmlButton.Text = "Send";
            sendXmlButton.UseVisualStyleBackColor = true;
            sendXmlButton.Click += sendXmlButton_Click;
            // 
            // singleMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(richTextBox1);
            Controls.Add(sendXmlButton);
            Name = "singleMessage";
            Size = new Size(800, 450);
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button sendXmlButton;
    }
}