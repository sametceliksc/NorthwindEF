using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_NorthwindEF.Products
{
    public partial class ProductForm : Form
    {
        public ProductForm()
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

        private void ProductForm_Load(object sender, EventArgs e)
        {
            GetProductList();
            //foreach (Category category in db.Categories.ToList())
            //{
            //    cmbCategory.Items.Add(category.CategoryName);
            //    cmbCategory.Tag = category;
            //}

            //foreach (Supplier supplier in db.Suppliers.ToList())
            //{
            //    cmbSupplier.Items.Add(supplier);
            //}

            cmbCategory.DataSource = db.Categories.ToList();
            cmbCategory.DisplayMember = "CategoryName";
            cmbCategory.ValueMember = "CategoryId";


            //aşağıdaki işlem Supplier.cs içerisinde overide edilmesini gerektirmektedir.!!!!!
            foreach (Supplier supplier in db.Suppliers)
            {
                cmbSupplier.Items.Add(supplier);
            }
           
        }

        public void GetProductList()
        {
            listView2.Items.Clear();
            foreach (Product product in db.Products.OrderByDescending(x=>x.ProductID).ToList())
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

        private void rbOrderByAsc_CheckedChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            foreach (Product product in db.Products.OrderBy(x => x.UnitPrice).ToList())
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

        private void rbOrderByDesc_CheckedChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            foreach (Product product in db.Products.OrderByDescending(x => x.UnitPrice).ToList())
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            foreach (Product product in db.Products.Where(x=>x.ProductName.StartsWith(txtSearch.Text)))
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ProductName = txtProductName.Text;
            product.UnitPrice = nudUnitPrice.Value;
            product.UnitsInStock = Convert.ToInt16(nudStock.Value);
            product.Category = cmbCategory.SelectedItem as Category;
            db.Products.Add(product);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Ürün Eklendi");
            }
            else
            {
                MessageBox.Show("bir hata meydana geldi!");
            }
            GetProductList();
        }
    }
}
