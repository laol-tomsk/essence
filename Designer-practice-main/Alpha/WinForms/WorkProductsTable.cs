using Alpha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alpha.Services;
using Alpha.Interfaces;
using Alpha.Data;

namespace Alpha.WinForms
{
    public partial class WorkProductsTable : Form
    {
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public List<Activity> GetActivities() => dataStorageService.GetActivities();
        public List<WorkProduct> GetWorkProducts() => dataStorageService.GetWorkProducts();
        public WorkProductsTable()
        {
            InitializeComponent();
            UpdateWorkProductsTable();
        }
        public void UpdateWorkProductsTable()
        {
            List<WorkProduct> workProducts = dataStorageService.GetWorkProducts();
            tableLayoutPanelWP.Controls.Clear();
            tableLayoutPanelWP.RowCount = workProducts.Count() + 1;
            tableLayoutPanelWP.Controls.Add(new Label
            {
                Text = "Work Product",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Size = new Size(250, 20)
            }, 0, 0);
            tableLayoutPanelWP.Controls.Add(new Label
            {
                Text = "More",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 1, 0);
            tableLayoutPanelWP.Controls.Add(new Label
            {
                Text = "Delete",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 2, 0);



            for (int i = 1; i <= workProducts.Count; i++)
            {
                tableLayoutPanelWP.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30F));
                WorkProduct workProduct = workProducts[i - 1];
                Guid workProductId = workProduct.GetWorkProductId();

                Button editButton = new Button();
                editButton.Text = "Edit";
                editButton.AccessibleName = workProductId.ToString();
                editButton.Click += new EventHandler(buttonEdit_Click);

                Button deleteButton = new Button();
                deleteButton.Text = "Delete";
                deleteButton.AccessibleName = workProductId.ToString();
                deleteButton.Click += new EventHandler(buttonDelete_Click);

                Label workProductNameLabel = new Label();
                workProductNameLabel.AutoSize = true;
                workProductNameLabel.Text = workProduct.Name;

                tableLayoutPanelWP.Controls.Add(workProductNameLabel, 0, i);
                tableLayoutPanelWP.Controls.Add(editButton, 1, i);
                tableLayoutPanelWP.Controls.Add(deleteButton, 2, i);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Guid workProductId = Guid.Parse(b.AccessibleName);
            WorkProduct workProduct = dataStorageService.GetWorkProducts().FirstOrDefault(wp => wp.GetWorkProductId() == workProductId);
            if (workProduct == null)
            {
                MessageBox.Show("Some problems with work product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopupWindowForEditWorkProduct popupWindowForEditWorkProduct = new PopupWindowForEditWorkProduct(workProduct);
            popupWindowForEditWorkProduct.ShowDialog();
            UpdateWorkProductsTable();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            Button b = (Button)sender;
            Guid workProductId = Guid.Parse(b.AccessibleName);
            WorkProduct workProduct = dataStorageService.GetWorkProducts().FirstOrDefault(wp => wp.GetWorkProductId() == workProductId);
            if (workProduct == null)
            {
                MessageBox.Show("Some problems with work product", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RemoveFromWorkProductManifests(workProduct);
            dataStorageService.RemoveWorkProduct(workProduct);
            UpdateWorkProductsTable();
        }
        private void RemoveFromWorkProductManifests(WorkProduct workProduct)
        {
            foreach (var workProductManifest in workProduct.GetWorkProductManifests())
                dataStorageService.RemoveWorkProductManifest(workProductManifest);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonAddWorkProduct_Click(object sender, EventArgs e)
        {
            new PopupWindowForAddWorkProduct().ShowDialog();
            UpdateWorkProductsTable();
        }

    }
}
