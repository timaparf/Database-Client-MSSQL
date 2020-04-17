using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_2._1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new Form1();
            form.Show();
            this.Close();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                {
                    var scales = db.Scale;
                    dataGridView1.Visible = true;
                    dataGridView1.DataSource = scales.ToList();

                }
            }
            if (radioButton2.Checked)
            {
                using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                {
                    if (!String.IsNullOrWhiteSpace(textBox2.Text) && !String.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        var scale = new Scale
                        {
                            Name = textBox2.Text,
                            Description = textBox3.Text

                        };
                        db.Scale.Add(scale);
                        MessageBox.Show("1 row(s) added");
                        db.SaveChanges();
                        var sShow = db.Scale.Where(u => u.Name == textBox2.Text && u.Description == textBox3.Text);
                        dataGridView1.DataSource = sShow.ToList();
                    }
                    else MessageBox.Show("Wrong input!");
                }
            }
            if (radioButton3.Checked)
            {
                using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                {
                    Regex r = new Regex("[0-9]$");
                    Match m = r.Match(textBox1.Text);
                    if (m.Success && !String.IsNullOrWhiteSpace(textBox2.Text) && !String.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        var scaletxt = int.Parse(textBox1.Text);
                        var scale = db.Scale.FirstOrDefault(u => u.ID == scaletxt);
                        scale.Name = textBox2.Text;
                        scale.Description = textBox3.Text;
                        db.SaveChanges();
                        MessageBox.Show("1 row(s) updated");
                        var sShow = db.Scale.Where(u => u.ID == scaletxt);
                        dataGridView1.DataSource = sShow.ToList();
                    }
                    else MessageBox.Show("Wrong input!");
                }
            }
            if (radioButton4.Checked)
            {
                using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                {
                    Regex r = new Regex("[0-9]$");
                    Match m = r.Match(textBox1.Text);
                    if (m.Success)
                    {
                        int id = int.Parse(textBox1.Text);
                        var scale = db.Scale.FirstOrDefault(c => c.ID == id);
                        var sShow = db.Scale.Where(u => u.ID == id);
                        db.Scale.Remove(scale);
                        db.SaveChanges();
                        MessageBox.Show("1 row(s) deleted");
                        
                        dataGridView1.DataSource = sShow.ToList();
                    }
                    else MessageBox.Show("Wrong input!");
                }
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
        }
    }
}
