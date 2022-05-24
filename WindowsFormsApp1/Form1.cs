using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // список с играми
        private List<Game> games = new List<Game>();
        
        /// <summary>
        /// конструктор формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// кнопка открытия формы добавления новой игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                // добавляем в список новую игру
                games.Add(form.game);
                LoadData();
            }
        }

        private void LoadData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = games;
            comboBox1.DataSource = null;
            comboBox1.DataSource = games;
            // отображаем жанры в комбобоксе
            comboBox1.DisplayMember = "Genre";
            comboBox1.ValueMember = "Genre";
        }

        // кнопка чтения из файла
        private void button2_Click(object sender, EventArgs e)
        {
            using (FileStream fileStream = File.OpenRead(Directory.GetCurrentDirectory() + "/jsonFile.txt"))
            {
                // десериализуем и приводим к типу Game
                games = JsonSerializer.Deserialize<List<Game>>(fileStream);
                LoadData();
            };
        }

        // кнопка записи в файл
        private void button3_Click(object sender, EventArgs e)
        {
            // создаём/открываем файл
            FileStream fileStream = new FileStream(Directory.GetCurrentDirectory() + "/jsonFile.txt", FileMode.OpenOrCreate);
            // сериализуем список игр
            var test = JsonSerializer.Serialize(games);
            // пишем в файл всё что сериализовалось
            fileStream.Write(System.Text.Encoding.UTF8.GetBytes(test), 0, test.Length);
            // чистим fileStream от нашего файла
            fileStream.Flush();
            fileStream.Close();
        }

        // linq запрос на выборку игр по выбранному жанру и вывод списка с играми в выбранном жанре
        private void button4_Click(object sender, EventArgs e)
        {
            // сам linq запрос
            var selected = from game in games
                           where game.Genre == comboBox1.SelectedValue.ToString()
                           select game;
            // список с выбранными играми
            List<Game> test = selected.ToList();
            dataGridView2.DataSource = test;
        }

        // показать сколько игр в самом частом жанре
        private void button5_Click(object sender, EventArgs e)
        {
            // группируем, выбираем, создаём списочек
            var res = games
                .GroupBy(x => x.Genre)
                .Select(x => new
                {
                    Name = x.Key,
                    Count = x.Count()
                });
            // пишем в текстбокс количество игр в самом частом жанре
            textBox1.Text = res.Single(x => x.Count == res.Max(y => y.Count)).Count.ToString();
        }
    }
}
