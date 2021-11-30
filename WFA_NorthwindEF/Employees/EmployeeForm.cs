using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_NorthwindEF.Employees
{
    public partial class EmployeeForm : Form
    {
        public EmployeeForm()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void btnBack_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        public void GetEmployeeList()
        {
            listView1.Items.Clear();
            foreach (Employee employee in db.Employees.OrderByDescending(x => x.EmployeeID).ToList())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = employee.FirstName;
                lvi.SubItems.Add(employee.LastName);
                lvi.SubItems.Add(employee.TitleOfCourtesy);
                lvi.SubItems.Add(employee.Country);
                lvi.SubItems.Add(employee.City);
                listView1.Items.Add(lvi);
            }
        }
        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            GetEmployeeList();
            
        }

        private void rbOrderByAsc_CheckedChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            foreach (Employee employee in db.Employees.OrderBy(x => x.FirstName).ToList())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = employee.FirstName;
                lvi.SubItems.Add(employee.LastName);
                lvi.SubItems.Add(employee.TitleOfCourtesy);
                lvi.SubItems.Add(employee.Country);
                lvi.SubItems.Add(employee.City);
                listView1.Items.Add(lvi);
            }
        }

        private void rbOrderByDesc_CheckedChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            foreach (Employee employee in db.Employees.OrderByDescending(x => x.FirstName).ToList())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = employee.FirstName;
                lvi.SubItems.Add(employee.LastName);
                lvi.SubItems.Add(employee.TitleOfCourtesy);
                lvi.SubItems.Add(employee.Country);
                lvi.SubItems.Add(employee.City);
                listView1.Items.Add(lvi);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            foreach (Employee employee in db.Employees.Where(x => x.FirstName.StartsWith(txtSearch.Text) || x.LastName.StartsWith(txtSearch.Text)))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = employee.FirstName;
                lvi.SubItems.Add(employee.LastName);
                lvi.SubItems.Add(employee.TitleOfCourtesy);
                lvi.SubItems.Add(employee.Country);
                lvi.SubItems.Add(employee.City);
                listView1.Items.Add(lvi);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Employee employee = new Employee();
            employee.FirstName = txtFirstName.Text;
            employee.LastName = txtLastName.Text;
            employee.TitleOfCourtesy = txtTitleOfCourtesy.Text;
            employee.Country = txtCountry.Text;
            employee.City = txtCity.Text;
            db.Employees.Add(employee);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Kişi Eklendi");
            }
            else
            {
                MessageBox.Show("bir hata meydana geldi!");
            }
            GetEmployeeList();
        }
    }
}
