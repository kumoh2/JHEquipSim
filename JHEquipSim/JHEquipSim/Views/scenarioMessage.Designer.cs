using System.Drawing;
using System.Windows.Forms;

namespace JHEquipSim.Views
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
            dataGridView2 = new DataGridView();
            paramremove = new Button();
            paramadd = new Button();
            dataGridView1 = new DataGridView();
            remove_step = new Button();
            edit_g_various = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // sendXmlButton
            // 
            sendXmlButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sendXmlButton.Location = new Point(540, 4);
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
            treeView1.Location = new Point(0, 30);
            treeView1.Name = "treeView1";
            treeView1.Size = new Size(203, 424);
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
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new Point(621, 30);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.Size = new Size(377, 423);
            dataGridView2.TabIndex = 14;
            dataGridView2.CellEndEdit += dataGridView2_CellEndEdit;
            // 
            // paramremove
            // 
            paramremove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            paramremove.Location = new Point(648, 4);
            paramremove.Name = "paramremove";
            paramremove.Size = new Size(25, 23);
            paramremove.TabIndex = 17;
            paramremove.Text = "-";
            paramremove.UseVisualStyleBackColor = true;
            paramremove.Click += paramremove_Click;
            // 
            // paramadd
            // 
            paramadd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            paramadd.Location = new Point(621, 4);
            paramadd.Name = "paramadd";
            paramadd.Size = new Size(25, 23);
            paramadd.TabIndex = 16;
            paramadd.Text = "+";
            paramadd.UseVisualStyleBackColor = true;
            paramadd.Click += paramadd_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(209, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(406, 423);
            dataGridView1.TabIndex = 18;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // remove_step
            // 
            remove_step.Location = new Point(330, 4);
            remove_step.Name = "remove_step";
            remove_step.Size = new Size(88, 23);
            remove_step.TabIndex = 19;
            remove_step.Text = "remove step";
            remove_step.UseVisualStyleBackColor = true;
            remove_step.Click += remove_step_Click;
            // 
            // edit_g_various
            // 
            edit_g_various.Location = new Point(415, 4);
            edit_g_various.Name = "edit_g_various";
            edit_g_various.Size = new Size(92, 23);
            edit_g_various.TabIndex = 20;
            edit_g_various.Text = "edit_g_various";
            edit_g_various.UseVisualStyleBackColor = true;
            edit_g_various.Click += edit_g_various_Click;
            // 
            // scenarioMessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(edit_g_various);
            Controls.Add(remove_step);
            Controls.Add(dataGridView1);
            Controls.Add(paramremove);
            Controls.Add(paramadd);
            Controls.Add(dataGridView2);
            Controls.Add(comboBox1);
            Controls.Add(btn_rename);
            Controls.Add(btn_remove_treexml);
            Controls.Add(btn_add_treexml);
            Controls.Add(treeView1);
            Controls.Add(sendXmlButton);
            Name = "scenarioMessage";
            Size = new Size(1003, 456);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button sendXmlButton;
        private TreeView treeView1;
        private Button btn_add_treexml;
        private Button btn_remove_treexml;
        private Button btn_rename;
        private ComboBox comboBox1;
        private DataGridView dataGridView2;
        private Button paramremove;
        private Button paramadd;
        private DataGridView dataGridView1;
        private Button remove_step;
        private Button edit_g_various;
    }
}