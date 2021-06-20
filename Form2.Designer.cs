
using System;
using System.Windows.Forms;

namespace SpreadsheetApp
{
    partial class Form2
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
            this.spreadsheet = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.spreadsheet)).BeginInit();
            this.SuspendLayout();
            // 
            // spreadsheet
            // 
            this.spreadsheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.spreadsheet.Location = new System.Drawing.Point(0, 1);
            this.spreadsheet.Name = "spreadsheet";
            this.spreadsheet.RowTemplate.Height = 25;
            this.spreadsheet.Size = new System.Drawing.Size(1231, 663);
            this.spreadsheet.TabIndex = 0;
            this.spreadsheet.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 664);
            this.Controls.Add(this.spreadsheet);
            this.Name = "Form2";
            this.Text = "d";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spreadsheet)).EndInit();
            this.ResumeLayout(false);

        }



        #endregion

        private System.Windows.Forms.DataGridView spreadsheet;
    }
}