using Alpha.WinForms;
using Alpha.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alpha.Services;
using Alpha.Enums;
using Alpha.Models;

namespace Alpha
{
    public partial class PopupWindowForEditCheckpoint : Form
    {
        private Checkpoint checkpoint;
        private IDetailing detail;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForEditCheckpoint(Checkpoint checkpoint, IDetailing detail)
        {
            InitializeComponent();
            BindComboboxWithEnum();
            this.checkpoint = checkpoint;
            this.detail = detail;
            this.Text = $"Edit {checkpoint.Name}";
            checkpointNameInput.Text = checkpoint.Name;
            checkpointDescriptionInput.Text = checkpoint.Description;
            checkpointOrderInput.Text = checkpoint.Order.ToString();
            timeEstimateInput.Text = checkpoint.TimeEstimate.ToString();
            taskNameInput.Text = checkpoint.TaskName;
            relativeWeightInput.Text = checkpoint.RelativeWeight.ToString();
            string specialId = checkpoint.GetSpecialId();
            specialIdInput.Text = (specialId == null) ? "" : specialId;
            if(specialId != null && specialId[specialId.Length-1] == 'C')
            {
                buttonAddDegreeOfEvidence.Visible = false;
            }
            if (!(detail.GetBaseObject() is Alpha) || (detail.GetBaseObject() as Alpha)?.ParentAlphaId != null) {
              timeEstimateInput.Visible = false;
              label6.Visible = false;
              relativeWeightInput.Visible = false;
              label8.Visible = false;
          }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string checkpointName = checkpointNameInput.Text;
            if (checkpointName == null || checkpointName == "")
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

            string checkpointDescription = checkpointDescriptionInput.Text;
            if (checkpointDescription == null || checkpointDescription == "")
            {
                MessageBox.Show("Please enter checkpoint's description", "Nullable description", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string checkpointOdred = checkpointOrderInput.Text;
            if (checkpointOdred == null || checkpointOdred == "")
            {
                MessageBox.Show("Please enter checkpoint's order", "Nullable order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int checkpointTimeEstimate = 0;
            if ( ( detail.GetBaseObject() is Alpha ) && ( detail.GetBaseObject() as Alpha )?.ParentAlphaId == null ) {
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
            string specialId = (specialIdInput.Text == "") ? null : specialIdInput.Text;
            checkpoint.Name = checkpointName;
            checkpoint.Description = checkpointDescription;
            checkpoint.Order = Int32.Parse(checkpointOdred);
            checkpoint.TimeEstimate = checkpointTimeEstimate;
            checkpoint.TaskName = taskName;
            checkpoint.RelativeWeight = checkpointRelativeWeight;
            checkpoint.SetSpecialId(specialId);
            dataStorageService.UpdateCheckpoints();
            DegreeOfEvidenceEnum degreeOfEvidenceEnum;
            Enum.TryParse<DegreeOfEvidenceEnum>(comboBoxDegreeOfEvidence.SelectedValue.ToString(), out degreeOfEvidenceEnum);
            checkpoint.DegreeOfEvidenceEnumValueManagerOpinion = degreeOfEvidenceEnum;
            this.Close();
        }

        private void buttonAddDegreeOfEvidence_Click(object sender, EventArgs e)
        {
            PopupWindowForCheckpointDegreeOfEvidences popupWindowForAddDegreeOfEvidence = new PopupWindowForCheckpointDegreeOfEvidences(checkpoint, detail.GetBaseObject(), detail);
            popupWindowForAddDegreeOfEvidence.ShowDialog();
        }

        private void BindComboboxWithEnum()
        {
            comboBoxDegreeOfEvidence.DataSource = Enum.GetValues(typeof(DegreeOfEvidenceEnum));
        }

        private void buttonCreateTheInfluenceOfTheManager_Click(object sender, EventArgs e)
        {
            DegreeOfEvidenceEnum degreeOfEvidenceEnum;
            Enum.TryParse<DegreeOfEvidenceEnum>(comboBoxDegreeOfEvidence.SelectedValue.ToString(), out degreeOfEvidenceEnum);
            checkpoint.DegreeOfEvidenceEnumValueManagerOpinion = degreeOfEvidenceEnum;
            MessageBox.Show("Manager opinion added!!!", "Parampampam", MessageBoxButtons.OK);
        }
  }
}
