using System;
using System.Collections.Generic;
using System.Json;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Alpha.Models;
using Alpha.WinForms;
using Alpha.Services;
using Alpha.Interfaces;
using Alpha.Data;

namespace Alpha
{
    public partial class Form1 : Form
    {
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public Form1()
        {
            InitializeComponent();
            UpdateAlphasTable();
        }
        public List<Activity> GetAllActivities() => dataStorageService.GetActivities(); 
        public List<Alpha> GetListOfAlphas() => dataStorageService.GetAlphas();
        public List<WorkProduct> GetListOfWorkProducts() => dataStorageService.GetWorkProducts();
        public void AddAlphaConteinment(AlphaContaiment alphaContaiment)
        {
            dataStorageService.AddAlphaContaiment(alphaContaiment);
        }
        public void DeleteAlphaConteinmentFromList(AlphaContaiment alphaContaiment)
        {
            dataStorageService.RemoveAlphaContaiment(alphaContaiment);
        }
        public void DeleteWorkProductManifestFromList(WorkProductManifest workProductManifest)
        {
            dataStorageService.RemoveWorkProductManifest(workProductManifest);
        }

        public void UpdateAlphasTable()
        {
            List<Alpha> alphas = dataStorageService.GetAlphas();
            tableLayoutPanel1.Controls.Clear();

            tableLayoutPanel1.RowCount = alphas.Count() + 1;
            tableLayoutPanel1.Controls.Add(new Label
            {
                Text = "Alpha",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Size = new Size(200,20)
            }, 0, 0);
            tableLayoutPanel1.Controls.Add(new Label
            {
                Text = "Alpha Parent",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                Size = new Size(200, 20)
            }, 1, 0);
            tableLayoutPanel1.Controls.Add(new Label
            {
                Text = "More",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 2, 0);
            tableLayoutPanel1.Controls.Add(new Label
            {
                Text = "Delete",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 3, 0);

            for (int i = 1; i <= alphas.Count; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30F));
                Alpha alpha = alphas[i - 1];
                Guid alphaId = alpha.GetAlphaId();

                Button editButton = new Button();
                editButton.Text = "Edit";
                editButton.AccessibleName = alphaId.ToString();
                editButton.Click += new EventHandler(buttonEdit_Click);

                Button deleteButton = new Button();
                deleteButton.Text = "Delete";
                deleteButton.AccessibleName = alphaId.ToString();
                deleteButton.Click += new EventHandler(buttonDelete_Click);

                Label alphaNameLabel = new Label();
                alphaNameLabel.AutoSize = true;
                alphaNameLabel.Text = alpha.Name;

                tableLayoutPanel1.Controls.Add(alphaNameLabel, 0, i);

                if (alpha.GetAlphaParent() != null)
                {
                    Label alphaParentNameLabel = new Label();
                    alphaParentNameLabel.Text = alpha.GetAlphaParent().GetName();
                    alphaParentNameLabel.AutoSize = true;
                    tableLayoutPanel1.Controls.Add(alphaParentNameLabel, 1, i);
                }
                tableLayoutPanel1.Controls.Add(editButton, 2, i);
                tableLayoutPanel1.Controls.Add(deleteButton, 3, i);
            }
        }

            
        private void AddAlpha_Click(object sender, EventArgs e)
        {
            PopupWindowForAddAlpha popupWindowForAddAlpha = new PopupWindowForAddAlpha();
            popupWindowForAddAlpha.ShowDialog();
            UpdateAlphasTable();

        }
        public void AddNewAlpha(Alpha alpha)
        {
            dataStorageService.AddAlpha(alpha);
            UpdateAlphasTable();
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Guid alphaId = Guid.Parse(b.AccessibleName);
            Alpha alpha = dataStorageService.GetAlphas().FirstOrDefault(a => a.GetAlphaId() == alphaId);
            if (alpha == null)
            {
                MessageBox.Show("Some problems with alpha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopupWindowForEditAlpha popupWindowForEditAlpha = new PopupWindowForEditAlpha(this, alpha);
            popupWindowForEditAlpha.ShowDialog();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            Button b = (Button)sender;
            Guid alphaId = Guid.Parse(b.AccessibleName);
            Alpha alpha = dataStorageService.GetAlphas().FirstOrDefault(a => a.GetAlphaId() == alphaId);
            if (alpha == null)
            {
                MessageBox.Show("Some problems with alpha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RemoveFromAlphaParent(alpha);
            RemoveFromAlphaContains(alpha);
            RemoveFromWokrProductManifests(alpha);
            dataStorageService.RemoveAlpha(alpha);
            UpdateAlphasTable();
        }
        private void RemoveFromAlphaParent(Alpha alpha)
        {
            List<Alpha> alphas = dataStorageService.GetAlphas();
            List<Alpha> alphasWithParentCurrentAlpha = alphas.Where(a => a.GetAlphaParentId() == alpha.GetAlphaId()).ToList();
            foreach (Alpha a in alphasWithParentCurrentAlpha)
                a.DeleteAlphaParent();
        }
        private void RemoveFromAlphaContains(Alpha alpha)
        {
            foreach (var subordinateAlphaContaimnent in alpha.GetSubordinateAlphaConteinments())
            {
                dataStorageService.RemoveAlphaContaiment(subordinateAlphaContaimnent);
            }
            dataStorageService.RemoveAlphaContaiment(alpha.GetSupperAlphaContainment());
        }
        private void RemoveFromWokrProductManifests(Alpha alpha)
        {
            dataStorageService.RemoveWorkProductManifest(alpha.GetWorkProductManifest());
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
