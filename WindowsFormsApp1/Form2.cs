using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Game game { get; set; }
        
        public Form2()
        {
            InitializeComponent();
        }

        // создание игры из того, что енаписал пользователь
        private void button1_Click(object sender, EventArgs e)
        {
            // создаем игру из полей на форме
            game = new Game { Name = textBox1.Text, Genre = textBox2.Text, Rating = Int32.Parse(textBox3.Text), Description = textBox4.Text };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
