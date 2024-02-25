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
            this.sendXmlButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btn_add_treexml = new System.Windows.Forms.Button();
            this.btn_remove_treexml = new System.Windows.Forms.Button();
            this.btn_rename = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.paramremove = new System.Windows.Forms.Button();
            this.paramadd = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.remove_step = new System.Windows.Forms.Button();
            this.edit_g_various = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // sendXmlButton
            // 
            this.sendXmlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendXmlButton.Location = new System.Drawing.Point(1199, 6);
            this.sendXmlButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.sendXmlButton.Name = "sendXmlButton";
            this.sendXmlButton.Size = new System.Drawing.Size(139, 37);
            this.sendXmlButton.TabIndex = 2;
            this.sendXmlButton.Text = "Send";
            this.sendXmlButton.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView1.Location = new System.Drawing.Point(0, 48);
            this.treeView1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(374, 1079);
            this.treeView1.TabIndex = 4;
            // 
            // btn_add_treexml
            // 
            this.btn_add_treexml.Location = new System.Drawing.Point(6, 5);
            this.btn_add_treexml.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btn_add_treexml.Name = "btn_add_treexml";
            this.btn_add_treexml.Size = new System.Drawing.Size(46, 37);
            this.btn_add_treexml.TabIndex = 5;
            this.btn_add_treexml.Text = "+";
            this.btn_add_treexml.UseVisualStyleBackColor = true;
            // 
            // btn_remove_treexml
            // 
            this.btn_remove_treexml.Location = new System.Drawing.Point(54, 5);
            this.btn_remove_treexml.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btn_remove_treexml.Name = "btn_remove_treexml";
            this.btn_remove_treexml.Size = new System.Drawing.Size(46, 37);
            this.btn_remove_treexml.TabIndex = 6;
            this.btn_remove_treexml.Text = "-";
            this.btn_remove_treexml.UseVisualStyleBackColor = true;
            // 
            // btn_rename
            // 
            this.btn_rename.Location = new System.Drawing.Point(111, 5);
            this.btn_rename.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btn_rename.Name = "btn_rename";
            this.btn_rename.Size = new System.Drawing.Size(139, 37);
            this.btn_rename.TabIndex = 7;
            this.btn_rename.Text = "name edit";
            this.btn_rename.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(388, 6);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(221, 32);
            this.comboBox1.TabIndex = 8;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(1347, 48);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 82;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(658, 546);
            this.dataGridView2.TabIndex = 14;
            // 
            // paramremove
            // 
            this.paramremove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramremove.Location = new System.Drawing.Point(1948, 6);
            this.paramremove.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.paramremove.Name = "paramremove";
            this.paramremove.Size = new System.Drawing.Size(46, 37);
            this.paramremove.TabIndex = 17;
            this.paramremove.Text = "-";
            this.paramremove.UseVisualStyleBackColor = true;
            // 
            // paramadd
            // 
            this.paramadd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.paramadd.Location = new System.Drawing.Point(1898, 6);
            this.paramadd.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.paramadd.Name = "paramadd";
            this.paramadd.Size = new System.Drawing.Size(46, 37);
            this.paramadd.TabIndex = 16;
            this.paramadd.Text = "+";
            this.paramadd.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(382, 48);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(956, 1079);
            this.dataGridView1.TabIndex = 18;
            // 
            // remove_step
            // 
            this.remove_step.Location = new System.Drawing.Point(613, 6);
            this.remove_step.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.remove_step.Name = "remove_step";
            this.remove_step.Size = new System.Drawing.Size(163, 37);
            this.remove_step.TabIndex = 19;
            this.remove_step.Text = "remove step";
            this.remove_step.UseVisualStyleBackColor = true;
            // 
            // edit_g_various
            // 
            this.edit_g_various.Location = new System.Drawing.Point(771, 6);
            this.edit_g_various.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.edit_g_various.Name = "edit_g_various";
            this.edit_g_various.Size = new System.Drawing.Size(171, 37);
            this.edit_g_various.TabIndex = 20;
            this.edit_g_various.Text = "edit_g_various";
            this.edit_g_various.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(1347, 647);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 82;
            this.dataGridView3.RowTemplate.Height = 37;
            this.dataGridView3.Size = new System.Drawing.Size(658, 480);
            this.dataGridView3.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1352, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(211, 24);
            this.label2.TabIndex = 23;
            this.label2.Text = "Message Paramters";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1345, 612);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 24);
            this.label3.TabIndex = 24;
            this.label3.Text = "Global Variable";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(1944, 604);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 37);
            this.button1.TabIndex = 26;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(1894, 604);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(46, 37);
            this.button2.TabIndex = 25;
            this.button2.Text = "+";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // scenarioMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.edit_g_various);
            this.Controls.Add(this.remove_step);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.paramremove);
            this.Controls.Add(this.paramadd);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_rename);
            this.Controls.Add(this.btn_remove_treexml);
            this.Controls.Add(this.btn_add_treexml);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.sendXmlButton);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "scenarioMessage";
            this.Size = new System.Drawing.Size(2011, 1127);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private DataGridView dataGridView3;
        private Label label2;
        private Label label3;
        private Button button1;
        private Button button2;
    }
}