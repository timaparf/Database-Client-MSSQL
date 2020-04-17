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
    public partial class Form1 : Form
    {
        Form childform;
        public Form1()
        {
            InitializeComponent();
            using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
            {
                var scales = db.Scale;
                foreach (var scale in scales)
                {
                    comboBox1.Items.Add(scale.Name);
                }

            }
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            CheckCheckBox();

        }
        public void CheckCheckBox()
        {
            if (checkBox1.Checked && !checkBox2.Checked)
            {
                IDBox.Enabled = true;
                NameBox.Enabled = false;
                OwnersBox.Enabled = false;
                ShopsBox.Enabled = false;
                CityBox.Enabled = false;
                comboBox1.Enabled = false;

            }
            if (checkBox2.Checked && !checkBox1.Checked)
            {
                IDBox.Enabled = false;
                NameBox.Enabled = false;
                OwnersBox.Enabled = false;
                ShopsBox.Enabled = false;
                CityBox.Enabled = false;
                comboBox1.Enabled = true;
            }
            if (checkBox2.Checked && checkBox1.Checked)
            {
                IDBox.Enabled = true;
                NameBox.Enabled = false;
                OwnersBox.Enabled = false;
                ShopsBox.Enabled = false;
                CityBox.Enabled = false;
                comboBox1.Enabled = true;

            }
            if (!checkBox2.Checked && !checkBox1.Checked)
            {
                IDBox.Enabled = false;
                NameBox.Enabled = false;
                OwnersBox.Enabled = false;
                ShopsBox.Enabled = false;
                CityBox.Enabled = false;
                comboBox1.Enabled = false;
            }
        }
        public void ChangeCheckBox(bool cb1, bool cb2)
        {
            checkBox1.Enabled = cb1;
            checkBox2.Enabled = cb2;
        }
        private void CleanTextBoxes()
        {
            IDBox.Text = "";
            NameBox.Text = "";
            OwnersBox.Text = "";
            ShopsBox.Text = "";
            CityBox.Text = "";
            comboBox1.Text = "";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckBox(true, true);
            CheckCheckBox();
            CleanTextBoxes();
        }
        private void radioAdd_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckBox(false, false);
            IDBox.Enabled = false;
            NameBox.Enabled = true;
            OwnersBox.Enabled = true;
            ShopsBox.Enabled = true;
            CityBox.Enabled = true;
            comboBox1.Enabled = true;
            CleanTextBoxes();

            //IDBox.Text = "";
            //NameBox.Text = "";
            //OwnersBox.Text = "";
            //ShopsBox.Text = "";
            //CityBox.Text = "";
        }
        private void radioPut_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckBox(false, false);
            IDBox.Enabled = true;
            NameBox.Enabled = true;
            OwnersBox.Enabled = true;
            ShopsBox.Enabled = true;
            CityBox.Enabled = true;
            comboBox1.Enabled = true;
            CleanTextBoxes();

            //IDBox.Text = "";
            //NameBox.Text = "";
            //OwnersBox.Text = "";
            //ShopsBox.Text = "";
            //CityBox.Text = "";
        }

        private void radioDelete_CheckedChanged(object sender, EventArgs e)
        {
            ChangeCheckBox(false, false);
            IDBox.Enabled = true;
            NameBox.Enabled = false;
            OwnersBox.Enabled = false;
            ShopsBox.Enabled = false;
            CityBox.Enabled = false;
            comboBox1.Enabled = false;
            CleanTextBoxes();

            //IDBox.Text = "";
            //NameBox.Text = "";
            //OwnersBox.Text = "";
            //ShopsBox.Text = "";
            //CityBox.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            CheckCheckBox();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckCheckBox();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            childform = new Form2();
            childform.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioGet.Checked)
            {
                if (checkBox1.Checked && !checkBox2.Checked)
                {
                    using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                    {
                        Regex r = new Regex("[0-9]$");
                        Match m = r.Match(IDBox.Text);
                        if (m.Success)
                        {
                            int id = int.Parse(IDBox.Text);
                            var company = db.Company.Where(u => u.ID == id);
                            dataGridView1.Visible = true;
                            dataGridView1.DataSource = company.ToList();
                        }
                        else MessageBox.Show("Wrong input!");

                    }
                }
                if (checkBox1.Checked && checkBox2.Checked)
                {
                    using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                    {
                        Regex r = new Regex("^[0-9]$");
                        Match m1 = r.Match(IDBox.Text);
                        if (m1.Success && comboBox1.Text != "")
                        {
                            int id = int.Parse(IDBox.Text);
                            var scaletxt = comboBox1.Text;
                            var scale = db.Scale.FirstOrDefault(u => u.Name == scaletxt);
                            var company = db.Company.Where(u => u.ID == id && u.TradingScaleId == scale.ID);
                            dataGridView1.Visible = true;
                            dataGridView1.DataSource = company.ToList();
                        }
                        else MessageBox.Show("Wrong input!");

                    }
                }
                if (!checkBox1.Checked && checkBox2.Checked)
                {
                    using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                    {
                        if (comboBox1.Text != "")
                        {
                            var scaletxt = comboBox1.Text;
                            var scale = db.Scale.FirstOrDefault(u => u.Name == scaletxt);
                            var company = db.Company.Where(u => u.TradingScaleId == scale.ID);
                            dataGridView1.Visible = true;
                            dataGridView1.DataSource = company.ToList();
                        }
                        else MessageBox.Show("Wrong input!");

                    }
                }
                if (!checkBox1.Checked && !checkBox2.Checked)
                {
                    using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                    {
                        var company = db.Company.Join(db.Scale,
                            c => c.TradingScaleId,
                            s => s.ID,
                            (c, s) => new
                            {
                                Id = c.ID,
                                Name = c.Name,
                                Owner = c.Owner,
                                Shops = c.NumberOfShops,
                                HeadCity = c.HeadCity,
                                TradingScaleName = s.Name,
                                TradingScale = s.Description

                            });
                        dataGridView1.Visible = true;
                        dataGridView1.DataSource = company.ToList();

                    }
                }
            }
            if (radioDelete.Checked)
            {
                using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                {
                    Regex r = new Regex("[0-9]$");
                    Match m = r.Match(IDBox.Text);
                    if (m.Success)
                    {
                        int id = int.Parse(IDBox.Text);
                        var cShow = db.Company.Where(c => c.ID == id);
                        dataGridView1.DataSource = cShow.ToList();
                        var company = db.Company.FirstOrDefault(c => c.ID == id);
                        db.Company.Remove(company);
                        db.SaveChanges();
                        MessageBox.Show("1 row(s) deleted");

                    }
                    else MessageBox.Show("Wrong input!");
                }
            }
            if (radioAdd.Checked)
            {
                using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                {
                    var scaletxt = comboBox1.Text;
                    var scale = db.Scale.FirstOrDefault(u => u.Name == scaletxt);

                    Regex r = new Regex("[0-9]$");

                    if (scale!=null&&r.Match(scale.ID.ToString()).Success && r.Match(ShopsBox.Text).Success
                        && !String.IsNullOrWhiteSpace(NameBox.Text)
                        && !String.IsNullOrWhiteSpace(OwnersBox.Text))
                    {

                        Company company = new Company
                        {
                            Name = NameBox.Text,
                            TradingScaleId = scale.ID,
                            HeadCity = CityBox.Text,
                            Owner = OwnersBox.Text,
                            NumberOfShops = int.Parse(ShopsBox.Text)

                        };
                        //dataGridView1.DataSource;
                        db.Company.Add(company);
                        MessageBox.Show("1 row(s) added");
                        db.SaveChanges();
                        var cShow = db.Company
                            .Where(u => u.Name == NameBox.Text && u.TradingScaleId == scale.ID
                            && u.HeadCity == CityBox.Text && u.Owner == OwnersBox.Text);
                        dataGridView1.DataSource = cShow.ToList();
                    }
                    else MessageBox.Show("Wrong input!");

                }
            }
            if (radioPut.Checked)
            {
                using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
                {
                    var scaletxt = comboBox1.Text;
                    var scale = db.Scale.FirstOrDefault(u => u.Name == scaletxt);

                    Regex r = new Regex("[0-9]$");
                    if (scale != null && r.Match(IDBox.Text).Success
                       && r.Match(scale.ID.ToString()).Success
                       && r.Match(ShopsBox.Text).Success
                       && !String.IsNullOrWhiteSpace(NameBox.Text)
                       && !String.IsNullOrWhiteSpace(OwnersBox.Text))
                    {
                        int id = int.Parse(IDBox.Text);
                        var comp = db.Company.FirstOrDefault(c => c.ID == id);


                        comp.Name = NameBox.Text;
                        comp.TradingScaleId = scale.ID;
                        comp.HeadCity = CityBox.Text;
                        comp.Owner = OwnersBox.Text;
                        comp.NumberOfShops = int.Parse(ShopsBox.Text);

                        db.SaveChanges();
                        var cShow = db.Company.Where(c => c.ID == id);
                        MessageBox.Show("1 row(s) updated");
                        dataGridView1.DataSource = cShow.ToList();

                    }
                    else MessageBox.Show("Wrong input!");

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (Trade_Of_UkraineEntities db = new Trade_Of_UkraineEntities())
            {
                var scales = db.Scale;
                if (scales.Count() ==0)
                {
                    var scale = new Scale
                    {
                        Name = "All",
                        Description = "All teriotory of Ukraine"

                    };
                    db.Scale.Add(scale);
                    MessageBox.Show("1 row(s) added");
                    db.SaveChanges();
                    var scale2 = new Scale
                    {
                        Name = "Kyiv",
                        Description = "All teriotory of Ukraine"

                    };
                    db.Scale.Add(scale2);
                    MessageBox.Show("1 row(s) added");
                    db.SaveChanges();
                }
            }
        }
    }
}
