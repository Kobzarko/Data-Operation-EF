using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_OperationData_EF
{
    public partial class Form1 : Form
    {

        SoccerContext db;
        int playersCnt;
      

        public Form1()
        {
            InitializeComponent();
            db = new SoccerContext();
            db.Players.Load();

            dataGridView1.DataSource = db.Players.Local.ToBindingList();
            playersCnt = db.Players.Count();
           
        }




        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // добавление
        private void button1_Click(object sender, EventArgs e)
        {
            PlayerForm plForm = new PlayerForm();
            DialogResult result = plForm.ShowDialog(this);

            if (result == DialogResult.Cancel)
            {
                return;
            }

            Player player = new Player();
            // добавляем возраст через поле в форме PlayerForm
            player.Age = (int)plForm.numericUpDown1.Value;
            // добавляем имя 
            player.Name = plForm.textBox1.Text;
            // выбираем позицию игрока 
            player.Position = plForm.comboBox1.SelectedItem.ToString();
            // добавляем игрока в БД
            db.Players.Add(player);
            playersCnt++;
            // сохраняем изменения
            db.SaveChanges();

            MessageBox.Show(" Новый объект добавлен");
           
        }
         // редактирование
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Player player = db.Players.Find(id);
                PlayerForm plForm = new PlayerForm();

                // выводим наши данные из БД в форму
                plForm.numericUpDown1.Value = player.Age;
                plForm.comboBox1.SelectedItem = player.Position;
                plForm.textBox1.Text = player.Name;

                DialogResult result = plForm.ShowDialog(this);

                if (result == DialogResult.Cancel)
                {
                    return;
                }
                // заносим наши отредактированные данные в БД
                player.Age = (int)plForm.numericUpDown1.Value;
                player.Position = plForm.comboBox1.SelectedItem.ToString();
                player.Name = plForm.textBox1.Text;

                // ссохраняем изменения
                db.SaveChanges();
                // обновляем таблицу
                dataGridView1.Refresh();
                MessageBox.Show("Объект обновлен");
            }
        }
        // удаление
        private void button3_Click(object sender, EventArgs e)
        {
            // если строк больше 0 то выполняем удаление
            if (dataGridView1.SelectedRows.Count>0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                // 
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                // поиск по id нужного игрока
                Player player = db.Players.Find(id);
                // удаление записи
                db.Players.Remove(player);
                // сохраняем результат
                db.SaveChanges();
                MessageBox.Show("Запись была удалена");
            }
        }
    }
}
