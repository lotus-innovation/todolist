using System;
using System.IO;
using System.Data.OleDb;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string toDo = textBox1.Text;
            string deadline = dateTimePicker1.Value.ToShortDateString();


            dataGridView1.Rows.Add(false,deadline,toDo);


        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("削除するToDoを選択してください。","削除");
            dataGridView1.Columns[0].Visible = false;
            button6.Visible = true;
            button7.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            
            DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();
            column.HeaderText = "削除選択";
            column.Name = "Delete";
            dataGridView1.Columns.Add(column);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("本当に削除しますか？",
            "質問",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation);

            //何が選択されたか調べる
            if (result == DialogResult.Yes)
            {
                //「はい」が選択された時
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value))
                    {
                        dataGridView1.Rows.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].Visible = true;
            button6.Visible = false;
            button7.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[3].Value))
                {
                    dataGridView1.Rows[i].Cells[0].Value = !(bool)dataGridView1.Rows[i].Cells[0].Value;
                }
            }

            dataGridView1.Columns.Remove("Delete");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value))
                {
                    dataGridView1.Rows[i].Visible = false;
                    
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value))
                {
                    dataGridView1.Rows[i].Visible = true;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            sfd.FileName = "新しいファイル.csv";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            sfd.Filter = "CSVファイル|*.csv|すべてのファイル|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 2;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //csv書き込み宣言
                StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Default);

                //行数だけループを回す
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    //列数だけループを回す
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        sw.Write(dataGridView1[j, i].Value + ",");

                    }
                    sw.WriteLine();
                }

                //書き込みを終了する
                sw.Close();

                MessageBox.Show(sfd.FileName + "　保存完了！");
            }


            
        }
/*
        private void button8_Click(object sender, EventArgs e)
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            ofd.FileName = "default.csv";
            //はじめに表示されるフォルダを指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            ofd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しないとすべてのファイルが表示される
            ofd.Filter = "CSVファイル|*.csv|すべてのファイル|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //2番目の「すべてのファイル」が選択されているようにする
            ofd.FilterIndex = 2;
            //タイトルを設定する
            ofd.Title = "開くファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            ofd.RestoreDirectory = true;
            //存在しないファイルの名前が指定されたとき警告を表示する
            ofd.CheckFileExists = true;
            //存在しないパスが指定されたとき警告を表示する
            ofd.CheckPathExists = true;

            //ダイアログを表示する
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 読み込みたいCSVファイルのパスを指定して開く
                StreamReader sr = new StreamReader(ofd.FileName);
                {
                    // 末尾まで繰り返す
                    while (!sr.EndOfStream)
                    {
                        // CSVファイルの一行を読み込む
                        string line = sr.ReadLine();
                        // 読み込んだ一行をカンマ毎に分けて配列に格納する
                        string[] values = line.Split(',');

                        // 配列からリストに格納する
                        List<string> lists = new List<string>();
                        lists.AddRange(values);

                        dataGridView1.Rows.Add();

                        foreach (string list in lists)
                        {
                        }
                        
                    }

                    MessageBox.Show(ofd.FileName + "　読み込み完了！");
                }
            }
        }*/
    }
}
