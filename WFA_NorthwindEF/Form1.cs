using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WFA_NorthwindEF.Employees;
using WFA_NorthwindEF.Products;

namespace WFA_NorthwindEF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NorthwindEntities db = new NorthwindEntities();
        private void Form1_Load(object sender, EventArgs e)
        {
            GetEmployeeList();
            GetProductList();
            GetOrderList();

            lblEmployee.Text = db.Employees.Count().ToString();
            lblProduct.Text = db.Products.Count().ToString();
            lblOrder.Text = db.Orders.Count().ToString();

        }

        public void GetEmployeeList()
        {
            listView1.Items.Clear();
            foreach (Employee employee in db.Employees.ToList())
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

        public void GetProductList()
        {
            listView2.Items.Clear();
            foreach (Product product in db.Products.ToList())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = product.ProductName;
                lvi.SubItems.Add(product.UnitPrice.ToString());
                lvi.SubItems.Add(product.UnitsInStock.ToString());
                if (product.CategoryID != null)
                {
                    lvi.SubItems.Add(product.Category.CategoryName);
                }
                else
                {
                    lvi.SubItems.Add("Tanımsız");
                }
                listView2.Items.Add(lvi);
            }
        }

        public void GetOrderList()
        {
            listView3.Items.Clear();
            foreach (Order order in db.Orders.ToList())
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = order.OrderID.ToString();
                lvi.SubItems.Add(order.Customer.CompanyName);
                lvi.SubItems.Add(order.Employee.FirstName + " " + order.Employee.LastName);
                lvi.SubItems.Add(order.OrderDate.ToString());
                listView3.Items.Add(lvi);
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
            this.Hide();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            EmployeeForm employeeForm = new EmployeeForm();
            employeeForm.Show();
            this.Hide();
        }
    }
}
