using DataAccess.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

namespace WpfProductManagement
{
    /// <summary>
    /// Interaction logic for AddEditProduct.xaml
    /// </summary>
    public partial class AddEditProduct : Window
    {
        private ProductDataAccess productDataAccess;
        private Product editingProduct;
        private bool editMode = false;

        public AddEditProduct(ProductDataAccess productDtAccess)
        {
            InitializeComponent();
            productDataAccess = productDtAccess;
        }

        public AddEditProduct(ProductDataAccess productDtAccess, Product product)
        {
            InitializeComponent();
            productDataAccess = productDtAccess;
            tbName.Text = product.Name;
            tbAuthor.Text = product.Author;
            tbAvailable.Text = product.AvailableCount.ToString();
            tbPrice.Text = product.Price.ToString();
            editMode = true;
            editingProduct = product;
            
            
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
                if (editMode)
                {
                    Product prd = new Product()
                    {
                        Id = editingProduct.Id,
                        Name = tbName.Text,
                        Author = tbAuthor.Text,
                        AvailableCount = Convert.ToInt32(tbAvailable.Text),
                        Price = Convert.ToDecimal(tbPrice.Text)
                    };
                    productDataAccess.EditProduct(prd);

                }
                else
                {
                    Product prd = new Product()
                    {
                        Id = productDataAccess.GetNextId(),
                        Name = tbName.Text,
                        Author = tbAuthor.Text,
                        AvailableCount = Convert.ToInt32(tbAvailable.Text),
                        Price = Convert.ToDecimal(tbPrice.Text)
                    };
                    productDataAccess.AddProduct(prd);
                }
                this.Close();
        }

    }
}
