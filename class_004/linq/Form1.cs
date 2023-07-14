using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ex_LINQ
{
    public partial class Form1 : Form
    {
        List<Customer> customers = new List<Customer> {
            new Customer{CustomerId="ALFKI",CompanyName="Alfreds Futterkiste", Country="Germany"},
            new Customer{CustomerId="ANATR",CompanyName="Ana Trujillo Emparedados y helados", Country="Mexico"},
            new Customer{CustomerId="ANTON",CompanyName="Antonio Moreno Taquería", Country="Mexico"},
            new Customer{CustomerId="AROUT",CompanyName="Around the Horn", Country="UK"},
            new Customer{CustomerId="BERGS",CompanyName="Berglunds snabbköp", Country="Sweden"},
            new Customer{CustomerId="BLAUS",CompanyName="Blauer See Delikatessen", Country="Germany"},
            new Customer{CustomerId="BLONP",CompanyName="Blondesddsl père et fils", Country="France"},
            new Customer{CustomerId="BOLID",CompanyName="Bólido Comidas preparadas", Country="Spain"},
            new Customer{CustomerId="BONAP",CompanyName="Bon app'", Country="France"},
            new Customer{CustomerId="BOTTM",CompanyName="Bottom-Dollar Markets", Country="Canada"},
        };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var result = from c in customers select new { c.CustomerId, c.CompanyName, c.Country };
            dataGridView1.DataSource = result.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = from c in customers select new { c.CustomerId, c.CompanyName, c.Country };
            dataGridView1.DataSource = result.ToList();
            var result2 = customers.Select(c => new { c.CustomerId, c.Country });
            dataGridView1.DataSource = result2.ToList();
        }

        private void Q2_Click(object sender, EventArgs e)
        {
            var res = from c in customers where c.CustomerId.Equals(textBox1.Text) select c;
            //dataGridView1.DataSource = res.ToList();
            var result = customers.Where(c=> c.Country.Equals(textBox1.Text));
            dataGridView1.DataSource = result.ToList();
        }

        private void Q3_Click(object sender, EventArgs e)
        {
            //var res = from c in customers where c.CompanyName.StartsWith("B")
            //          select c;
            //var res = from c in customers
            //          where c.CompanyName.EndsWith("A")
            //          select c;
            var res = from c in customers
                      where c.CompanyName.Contains("s")
                      select c;
            //dataGridView1.DataSource = res.ToList();
            var res2 = customers.Where(c=> c.CompanyName.StartsWith("B"));
            dataGridView1.DataSource=res2.ToList();
        }

        private void Q4_Click(object sender, EventArgs e)
        {
            var res = from c in customers
                      orderby c.Country descending
                      select c;
            //dataGridView1.DataSource = res.ToList();
            //var res2 = customers.OrderBy(c => c.Country);
            var res2 = customers.OrderByDescending(c => c.Country);
            dataGridView1.DataSource=res2.ToList();
        }

        private void Q5_Click(object sender, EventArgs e)
        {
            var res = (from c in customers
                      select new { c.Country }).Distinct();
            //dataGridView1.DataSource = res.ToList();
            var res2= customers.Select(c=> new { c.Country }).Distinct();
            dataGridView1.DataSource = res2.ToList();
        }
    }
}
