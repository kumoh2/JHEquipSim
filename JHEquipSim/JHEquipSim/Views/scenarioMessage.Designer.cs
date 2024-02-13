﻿namespace JHEquipSim.Views
{
    partial class scenarioMessage
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
            sendXmlButton = new Button();
            treeView1 = new TreeView();
            btn_add_treexml = new Button();
            btn_remove_treexml = new Button();
            btn_rename = new Button();
            comboBox1 = new ComboBox();
            btn_add_scenario = new Button();
            listView1 = new ListView();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // sendXmlButton
            // 
            sendXmlButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sendXmlButton.Location = new Point(720, 3);
            sendXmlButton.Name = "sendXmlButton";
            sendXmlButton.Size = new Size(75, 23);
            sendXmlButton.TabIndex = 2;
            sendXmlButton.Text = "Send";
            sendXmlButton.UseVisualStyleBackColor = true;
            sendXmlButton.Click += sendXmlButton_Click;
            // 
            // treeView1
            // 
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
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(209, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 8;
            // 
            // btn_add_scenario
            // 
            btn_add_scenario.Location = new Point(336, 4);
            btn_add_scenario.Name = "btn_add_scenario";
            btn_add_scenario.Size = new Size(88, 23);
            btn_add_scenario.TabIndex = 9;
            btn_add_scenario.Text = "add scenario";
            btn_add_scenario.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Location = new Point(207, 29);
            listView1.Name = "listView1";
            listView1.Size = new Size(588, 418);
            listView1.TabIndex = 10;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button1
            // 
            button1.Location = new Point(425, 4);
            button1.Name = "button1";
            button1.Size = new Size(107, 23);
            button1.TabIndex = 11;
            button1.Text = "edit global value";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(533, 4);
            button2.Name = "button2";
            button2.Size = new Size(106, 23);
            button2.TabIndex = 12;
            button2.Text = "edit param value";
            button2.UseVisualStyleBackColor = true;
            // 
            // scenarioMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listView1);
            Controls.Add(btn_add_scenario);
            Controls.Add(comboBox1);
            Controls.Add(btn_rename);
            Controls.Add(btn_remove_treexml);
            Controls.Add(btn_add_treexml);
            Controls.Add(treeView1);
            Controls.Add(sendXmlButton);
            Name = "scenarioMessage";
            Size = new Size(800, 450);
            ResumeLayout(false);
        }

        #endregion
        private Button sendXmlButton;
        private TreeView treeView1;
        private Button btn_add_treexml;
        private Button btn_remove_treexml;
        private Button btn_rename;
        private ComboBox comboBox1;
        private Button btn_add_scenario;
        private ListView listView1;
        private Button button1;
        private Button button2;
    }
}