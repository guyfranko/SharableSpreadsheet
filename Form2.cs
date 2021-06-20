using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpreadsheetApp
{
    public partial class Form2 : Form
    {
        public Form2(ShareableSpreadSheet SH)
        {
            InitializeComponent();
            int row = 0, col = 0;
            SH.getSize(ref row, ref col);
            spreadsheet.ColumnCount = col;
            spreadsheet.RowCount = row;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    spreadsheet[j, i].Value = SH.getCell(i + 1, j + 1);
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {  
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

