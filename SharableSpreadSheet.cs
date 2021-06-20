using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
namespace Simulator
{
    class ShareableSpreadSheet
    {
        public String[,] SpreadSheet;
        public int Rows;
        public int Cols;
        public Mutex MutexTable;
        public Mutex MutexNum;
        public int NumActions;
        public int numUsers;
        public ShareableSpreadSheet(int nRows, int nCols)
        {
            // construct a nRows*nCols spreadsheet
            SpreadSheet = new String[nRows, nCols];
            NumActions = 0;
            numUsers = -1;
            Rows = nRows;
            Cols = nCols;
            MutexTable = new Mutex();
            MutexNum = new Mutex();
        }
        public String getCell(int row, int col)
        {
            if(row > Rows || col > Cols)
            {
                return null;
            }
            // return the string at [row,col]
            MutexTable.WaitOne();
            while (numUsers != -1 && NumActions >= numUsers)
            {
                //busy wait
            }
            MutexNum.WaitOne();
            NumActions++;
            MutexNum.ReleaseMutex();
            MutexTable.ReleaseMutex();
            String tmp = SpreadSheet[row - 1, col - 1];
            //Thread.Sleep(1000);
            MutexNum.WaitOne();
            NumActions--;
            MutexNum.ReleaseMutex();
            return tmp;
        }
        public bool setCell(int row, int col, String str)
        {
            if (row > Rows || col > Cols)
            {
                return false;
            }
            // set the string at [row,col]
            MutexTable.WaitOne();
            while (NumActions != 0)
            {
                //busy wait
            }
            SpreadSheet[row - 1, col - 1] = str;
            MutexTable.ReleaseMutex();
            return true;
        }
        public bool searchString(String str, ref int row, ref int col)
        {
            // search the cell with string str, and return true/false accordingly.
            // stores the location in row,col.
            // return the first cell that contains the string (search from first row to the last row)
            MutexTable.WaitOne();
            while (numUsers != -1 && NumActions >= numUsers)
            {
                //busy wait
            }
            MutexNum.WaitOne();
            NumActions++;
            MutexNum.ReleaseMutex();
            MutexTable.ReleaseMutex();
            //Thread.Sleep(1000);
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (SpreadSheet[i, j] == str)
                    {
                        MutexNum.WaitOne();
                        NumActions--;
                        MutexNum.ReleaseMutex();
                        row = i + 1;
                        col = j + 1;
                        return true;
                    }
                }
            }
            MutexNum.WaitOne();
            NumActions--;
            MutexNum.ReleaseMutex();
            return false;
        }
        public bool exchangeRows(int row1, int row2)
        {
            if (row1 > Rows || row2 > Rows)
            {
                return false;
            }
            MutexTable.WaitOne();
            while (NumActions != 0)
            {
                //busy wait
            }
            //CS
            for (int i = 0; i < Cols; i++)
            {
                String tmp1 = SpreadSheet[row1 - 1, i];
                String tmp2 = SpreadSheet[row2 - 1, i];
                SpreadSheet[row1 - 1, i] = tmp2;
                SpreadSheet[row2 - 1, i] = tmp1;
            }
            //End CS
            MutexTable.ReleaseMutex();
            return true;
        }
        public bool exchangeCols(int col1, int col2)
        {
            if (col1 > Cols || col2 > Cols)
            {
                return false;
            }
            // exchange the content of col1 and col2
            MutexTable.WaitOne();
            while (NumActions != 0)
            {
                //busy wait
            }
            //CS
            for (int i = 0; i < Rows; i++)
            {
                String tmp1 = SpreadSheet[i, col1 - 1];
                String tmp2 = SpreadSheet[i, col2 - 1];
                SpreadSheet[i, col1 - 1] = tmp2;
                SpreadSheet[i, col2 - 1] = tmp1;
            }
            //End CS
            MutexTable.ReleaseMutex();
            return true;
        }
        public bool searchInRow(int row, String str, ref int col)
        {
            if (row > Rows)
            {
                return false;
            }
            // perform search in specific row
            MutexTable.WaitOne();
            while (numUsers != -1 && NumActions >= numUsers)
            {
                //busy wait
            }
            MutexNum.WaitOne();
            NumActions++;
            MutexNum.ReleaseMutex();
            MutexTable.ReleaseMutex();
            //Thread.Sleep(1000);

            for (int j = 0; j < Cols; j++)
            {
                if (SpreadSheet[row - 1, j] == str)
                {
                    MutexNum.WaitOne();
                    NumActions--;
                    MutexNum.ReleaseMutex();
                    col = j + 1;
                    return true;
                }
            }
            MutexNum.WaitOne();
            NumActions--;
            MutexNum.ReleaseMutex();
            return false;
        }
        public bool searchInCol(int col, String str, ref int row)
        {
            if (col > Cols)
            {
                return false;
            }
            // perform search in specific col
            MutexTable.WaitOne();
            while (numUsers != -1 && NumActions >= numUsers)
            {
                //busy wait
                Console.WriteLine("searchInCol");
            }
            MutexNum.WaitOne();
            NumActions++;
            MutexNum.ReleaseMutex();
            MutexTable.ReleaseMutex();
            //Thread.Sleep(1000);

            for (int j = 0; j < Rows; j++)
            {
                if (SpreadSheet[j, col - 1] == str)
                {
                    MutexNum.WaitOne();
                    NumActions--;
                    MutexNum.ReleaseMutex();
                    row = j + 1;
                    return true;
                }
            }
            MutexNum.WaitOne();
            NumActions--;
            MutexNum.ReleaseMutex();
            return false;
        }
        public bool searchInRange(int col1, int col2, int row1, int row2, String str, ref int row, ref int col)
        {
            if (col1 > Cols || col2 > Cols || row1 > Rows || row2 > Rows)
            {
                return false;
            }
            // perform search within spesific range: [row1:row2,col1:col2] 
            //includes col1,col2,row1,row2
            MutexTable.WaitOne();
            while (numUsers != -1 && NumActions >= numUsers)
            {
                //busy wait
            }
            MutexNum.WaitOne();
            NumActions++;
            MutexNum.ReleaseMutex();
            MutexTable.ReleaseMutex();
            //Thread.Sleep(1000);

            for (int i = row1 - 1; i < row2; i++)
            {
                for (int j = col1 - 1; j < col2; j++)
                {
                    if (SpreadSheet[i, j] == str)
                    {
                        MutexNum.WaitOne();
                        NumActions--;
                        MutexNum.ReleaseMutex();
                        row = i + 1;
                        col = j + 1;
                        return true;
                    }
                }
            }
            MutexNum.WaitOne();
            NumActions--;
            MutexNum.ReleaseMutex();
            return false;
        }
        public bool addRow(int row1)
        {
            if (row1 > Rows)
            {
                return false;
            }
            //add a row after row1
            MutexTable.WaitOne();
            while (NumActions != 0)
            {
                //busy wait
            }
            //CS
            String[,] TmpSpreadSheet = new String[Rows + 1, Cols];
            for (int i = 0; i < row1; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    TmpSpreadSheet[i, j] = SpreadSheet[i, j];
                }
            }
            for (int i = row1; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    TmpSpreadSheet[i + 1, j] = SpreadSheet[i, j];
                }
            }
            SpreadSheet = new String[Rows + 1, Cols];
            for (int i = 0; i < Rows + 1; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    SpreadSheet[i, j] = TmpSpreadSheet[i, j];
                }
            }
            Rows++;
            MutexTable.ReleaseMutex();
            //End CS
            return true;
        }
        public bool addCol(int col1)
        {
            if (col1 > Cols)
            {
                return false;
            }
            //add a column after col1
            MutexTable.WaitOne();
            while (NumActions != 0)
            {
                //busy wait
            }
            //CS
            String[,] TmpSpreadSheet = new String[Rows, Cols + 1];
            for (int i = 0; i < col1; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    TmpSpreadSheet[j, i] = SpreadSheet[j, i];
                }
            }
            for (int i = col1; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    TmpSpreadSheet[j, i + 1] = SpreadSheet[j, i];
                }
            }
            SpreadSheet = new String[Rows, Cols + 1];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols + 1; j++)
                {
                    SpreadSheet[i, j] = TmpSpreadSheet[i, j];
                }
            }
            Cols++;
            MutexTable.ReleaseMutex();
            //End CS
            return true;
        }
        public void getSize(ref int nRows, ref int nCols)
        {
            // return the size of the spreadsheet in nRows, nCols
            nRows = Rows;
            nCols = Cols;
        }
        public bool setConcurrentSearchLimit(int nUsers)
        {
            // this function aims to limit the number of users that can perform the search operations concurrently.
            // The default is no limit. When the function is called, the max number of concurrent search operations is set to nUsers. 
            // In this case additional search operations will wait for existing search to finish
            MutexTable.WaitOne();
            if (NumActions > nUsers)
            {
                MutexTable.ReleaseMutex();
                return false;
            }
            else
            {
                numUsers = nUsers;
                MutexTable.ReleaseMutex();
            }
            return true;
        }
        public bool save(String fileName)
        {
            // save the spreadsheet to a file fileName.
            // you can decide the format you save the data. There are several options.
            MutexTable.WaitOne();
            while (NumActions != 0)
            {
                //busy wait
            }
            using (StreamWriter outfile = new(fileName))
            {
                for (int i = 0; i < Rows; i++)
                {
                    string content = "";
                    for (int j = 0; j < Cols; j++)
                    {
                        content += SpreadSheet[i, j] + ",";
                    }
                    outfile.WriteLine(content);
                }
            }
            MutexTable.ReleaseMutex();
            return true;
        }
        public bool load(String fileName)
        {
            // load the spreadsheet from fileName
            // replace the data and size of the current spreadsheet with the loaded data
            MutexTable.WaitOne();
            while (NumActions != 0)
            {
                //busy wait
            }
            StreamReader stread;
            try
            {
                stread = new StreamReader(fileName);
            }
            catch
            {
                return false;
            }
            var lines = new List<string[]>();
            while (!stread.EndOfStream)
            {
                string[] Line = stread.ReadLine().Split(',');
                lines.Add(Line);
            }
            SpreadSheet = new string[lines.Count, lines[0].Length - 1];
            for (int i = 0; i < SpreadSheet.GetLength(0); i++)
            {
                for (int j = 0; j < SpreadSheet.GetLength(1); j++)
                {
                    SpreadSheet[i, j] = lines[i][j];
                }
            }
            Rows = lines.Count;
            Cols = lines[0].Length - 1;
            MutexTable.ReleaseMutex();
            return true;
        }
    }
}