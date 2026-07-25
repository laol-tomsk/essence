using Alpha.Interfaces;
using Alpha.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alpha
{
    public partial class PopupWindowForAddCheckpoint : Form
    {
        private IDetailing detail;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForAddCheckpoint(IDetailing detail)
        {
            this.detail = detail;
            InitializeComponent();
            this.Text = $"Add checkpoint for {detail.GetName()}";
            if (!(detail.GetBaseObject() is Alpha) || (detail.GetBaseObject() as Alpha)?.ParentAlphaId != null) {
                timeEstimateInput.Visible = false;
                label3.Visible = false;
                relativeWeightInput.Visible = false;
                label6.Visible = false;
            }
        }

        public PopupWindowForAddCheckpoint(IDetailing detail, Checkpoint _checkpoint)
        {
            this.detail = detail;
            this.Text = $"Add checkpoint for Influence of the manager";
            Checkpoint checkpoint = new Checkpoint(_checkpoint.GetName() + " Influence of the manager", "Influence of the manager on " + _checkpoint.GetName(), 0, detail, _checkpoint.GetSpecialId()+"C", 0, "", 0);
            detail.AddCheckpoint(checkpoint);
            dataStorageService.AddCheckpoint(checkpoint);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string stateName = checkpointNameInput.Text;
            if (stateName == null || stateName == "")
            {
                MessageBox.Show("Please enter checkpoint's name", "Nullable name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string taskName = taskNameInput.Text;
            if (taskName == null || taskName == "")
            {
                MessageBox.Show("Please enter checkpoint's task name", "No task name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string stateDescription = checkpointDescriptionInput.Text;
            if (stateDescription == null || stateDescription == "")
            {
                MessageBox.Show("Please enter checkpoint's description", "Nullable description", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int checkpointTimeEstimate = 0;
            if ( (detail.GetBaseObject() is Alpha) && ( detail.GetBaseObject() as Alpha )?.ParentAlphaId == null ) {
              string checkpointTimeEstimateString = timeEstimateInput.Text;
              if ( !int.TryParse(checkpointTimeEstimateString, out checkpointTimeEstimate) ) {
                MessageBox.Show("Please enter integer checkpoint's estimate", "Non-numertic estimate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
              }
            }

            int checkpointRelativeWeight = 0;
            if ( (detail.GetBaseObject() is Alpha) && ( detail.GetBaseObject() as Alpha )?.ParentAlphaId == null ) {
              string checkpointRelativeWeightString = relativeWeightInput.Text;
              if ( !int.TryParse(checkpointRelativeWeightString, out checkpointRelativeWeight) ) {
                MessageBox.Show("Please enter integer checkpoint's relative weight", "Non-numertic relative weight", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
              }
            }

            int stateOrder = detail.GetCheckpoints().Count() * 10;
            string specialId = (specialIdInput.Text == "") ? null : specialIdInput.Text;
            Checkpoint checkpoint = new Checkpoint(stateName, stateDescription, stateOrder, detail, specialId, checkpointTimeEstimate, taskName, checkpointRelativeWeight);
            detail.AddCheckpoint(checkpoint);
            dataStorageService.AddCheckpoint(checkpoint);
            this.Close();
        }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {

    }
  }
}
