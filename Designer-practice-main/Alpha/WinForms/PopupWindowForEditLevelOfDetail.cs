using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alpha.Models;
using Alpha.Services;

namespace Alpha.WinForms
{
    public partial class PopupWindowForEditLevelOfDetail : Form
    {
        private LevelOfDetail levelOfDetail;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForEditLevelOfDetail(LevelOfDetail levelOfDetail)
        {
            InitializeComponent();
            this.levelOfDetail = levelOfDetail;
            this.Text = $"Edit {levelOfDetail.GetName()}";
            UpdateEditLevelOfDetailsUI();
        }
        private void UpdateEditLevelOfDetailsUI()
        {
            levelOfDetailNameInput.Text = levelOfDetail.GetName();
            taskNameInput.Text = levelOfDetail.TaskName;
            levelOfDetailDescriptionInput.Text = levelOfDetail.GetDescription();
            levelOfDetailOrderInput.Text = levelOfDetail.GetOrder().ToString();
            levelOfDetailTimeEstimateInput.Text = levelOfDetail.TimeEstimate.ToString();
            string specialId = levelOfDetail.GetSpecialId();
            specialIdInput.Text = (specialId == null) ? "" : specialId;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string levelOfDetailName = levelOfDetailNameInput.Text;
            if (levelOfDetailName == null || levelOfDetailName == "")
            {
                MessageBox.Show("Please enter level of detail's name", "Nullable name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string taskName = taskNameInput.Text;
            if (taskName == null || taskName == "")
            {
                MessageBox.Show("Please enter level's task name", "No task name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string levelOfDetailDescription = levelOfDetailDescriptionInput.Text;
            if (levelOfDetailDescription == null || levelOfDetailDescription == "")
            {
                MessageBox.Show("Please enter level of detail's description", "Nullable description", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string levelOfDetailOdred = levelOfDetailOrderInput.Text;
            if (levelOfDetailOdred == null || levelOfDetailOdred == "")
            {
                MessageBox.Show("Please enter level of detail's order", "Nullable order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string levelOfDetailTimeEstimateString = levelOfDetailTimeEstimateInput.Text;
            int levelOfDetailTimeEstimate;
            if (!int.TryParse(levelOfDetailTimeEstimateString, out levelOfDetailTimeEstimate)) {
                MessageBox.Show("Please enter integer level of detail's estimate", "Non-numertic estimate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string specialId = (specialIdInput.Text == "") ? null : specialIdInput.Text;

            levelOfDetail.Name = levelOfDetailName;
            levelOfDetail.TaskName = taskName;
            levelOfDetail.Description = levelOfDetailDescription;
            levelOfDetail.Order = Int32.Parse(levelOfDetailOdred);
            levelOfDetail.TimeEstimate = levelOfDetailTimeEstimate;
            levelOfDetail.SetSpecialId(specialId);
            dataStorageService.UpdateLevelOfDetails();
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
             
    }
}
