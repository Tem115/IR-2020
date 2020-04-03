using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Postgres
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.RowCount = 1;
            if (textBox1.Text == "")
                return;
            string[] words = textBox1.Text.Split(new char[] { ' ' });

            string myConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=Qwerty12;Database=Mirvoda;";
            string queryString = "SELECT * FROM movies WHERE ";

            for (int i = 0; i < words.Length; i++)
            {
                if (int.TryParse(words[i], out int year))
                {
                    queryString = queryString + "(strpos(name, '" + words[i] + "') > 0 OR year = " + year + ")";
                }
                else
                    queryString = queryString + "(strpos(name, '" + words[i] + "') > 0)";

                if (i != words.Length - 1)
                    queryString = queryString + " AND ";
            }


            NpgsqlConnection myConnection = new NpgsqlConnection(myConnectionString);
            myConnection.Open(); //Открываем соединение.


            NpgsqlCommand COM = new NpgsqlCommand(queryString, myConnection);
            NpgsqlDataReader DR = COM.ExecuteReader();


            var rowIndex = 0;
            while (DR.Read())
            {
                dataGridView1.Rows.Add();

                for (int i = 0; i < 3; i++)
                {
                    dataGridView1.Rows[rowIndex].Cells[i].Value = DR.GetValue(i);
                }
                rowIndex++;
                if (rowIndex > 10)
                {
                    MessageBox.Show(
"Поиск завершен",
"Предупреждение",
MessageBoxButtons.OK,
MessageBoxIcon.Information,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }
            }
            myConnection.Close();
            MessageBox.Show(
"Поиск завершен",
"Предупреждение",
MessageBoxButtons.OK,
MessageBoxIcon.Information,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
