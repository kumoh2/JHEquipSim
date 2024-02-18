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
            treeView1 = new TreeView();
            btn_add_treexml = new Button();
            btn_remove_treexml = new Button();
            btn_rename = new Button();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Location = new Point(209, 29);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(586, 418);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // sendXmlButton
            // 
            sendXmlButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sendXmlButton.Location = new Point(720, 2);
            sendXmlButton.Name = "sendXmlButton";
            sendXmlButton.Size = new Size(75, 23);
            sendXmlButton.TabIndex = 2;
            sendXmlButton.Text = "Send";
            sendXmlButton.UseVisualStyleBackColor = true;
            sendXmlButton.Click += sendXmlButton_Click;
            // 
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            treeView1.Location = new Point(0, 29);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(203, 418);
            treeView1.TabIndex = 4;
            treeView1.NodeMouseDoubleClick += TreeView1_NodeMouseDoubleClick;
            // 
            // btn_add_treexml
            // 
            btn_add_treexml.Location = new Point(3, 3);
            btn_add_treexml.Name = "btn_add_treexml";
            btn_add_treexml.Size = new Size(25, 23);
            btn_add_treexml.TabIndex = 5;
            btn_add_treexml.Text = "+";
            btn_add_treexml.UseVisualStyleBackColor = true;
            btn_add_treexml.Click += btn_add_treexml_Click;
            // 
            // btn_remove_treexml
            // 
            btn_remove_treexml.Location = new Point(29, 3);
            btn_remove_treexml.Name = "btn_remove_treexml";
            btn_remove_treexml.Size = new Size(25, 23);
            btn_remove_treexml.TabIndex = 6;
            btn_remove_treexml.Text = "-";
            btn_remove_treexml.UseVisualStyleBackColor = true;
            btn_remove_treexml.Click += btn_remove_treexml_Click;
            // 
            // btn_rename
            // 
            btn_rename.Location = new Point(60, 3);
            btn_rename.Name = "btn_rename";
            btn_rename.Size = new Size(75, 23);
            btn_rename.TabIndex = 7;
            btn_rename.Text = "name edit";
            btn_rename.UseVisualStyleBackColor = true;
            btn_rename.Click += btn_rename_Click;
            // 
            // singleMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btn_rename);
            Controls.Add(btn_remove_treexml);
            Controls.Add(btn_add_treexml);
            Controls.Add(treeView1);
            Controls.Add(richTextBox1);
            Controls.Add(sendXmlButton);
            Name = "singleMessage";
            Size = new Size(800, 450);
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBox1;
        private Button sendXmlButton;
        private TreeView treeView1;
        private Button btn_add_treexml;
        private Button btn_remove_treexml;
        private Button btn_rename;
    }
}