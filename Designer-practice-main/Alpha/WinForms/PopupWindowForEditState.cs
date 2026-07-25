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

namespace Alpha
{
    public partial class PopupWindowForEditState : Form
    {
        private State state;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForEditState(State state)
        {
            InitializeComponent();
            this.state = state;
            this.Text = $"Edit {state.Name}";
            if ((state.GetBaseObject() as Alpha)?.ParentAlphaId == null) {
              label5.Visible = false;
              timeEstimateInput.Visible = false;
            }
            UpdateEditStateUI();
        }
        private void UpdateEditStateUI()
        {
            stateNameInput.Text = state.Name;
            stateDescriptionInput.Text = state.Description;
            stateOrderInput.Text = state.Order.ToString();
            timeEstimateInput.Text = state.TimeEstimate.ToString();
            taskNameInput.Text = state.TaskName;
            string specialId = state.GetSpecialId();
            specialIdInput.Text = (specialId == null) ? "" : specialId;
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string stateName = stateNameInput.Text;
            if (stateName == null || stateName == "")
            {
                MessageBox.Show("Please enter state's name", "Nullable name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string taskName = taskNameInput.Text;
            if (taskName == null || taskName == "")
            {
                MessageBox.Show("Please enter task's name", "Task name is null", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string stateDescription = stateDescriptionInput.Text;
            if (stateDescription == null || stateDescription == "")
            {
                MessageBox.Show("Please enter state's description", "Nullable description", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string stateOdred = stateOrderInput.Text;
            if (stateOdred == null || stateOdred == "")
            {
                MessageBox.Show("Please enter state's order", "Nullable order", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int stateTimeEstimate = 0;
            if ( ( state.GetBaseObject() as Alpha )?.ParentAlphaId != null ) {
              string stateTimeEstimateString = timeEstimateInput.Text;
              if ( !int.TryParse(stateTimeEstimateString, out stateTimeEstimate) ) {
                MessageBox.Show("Please enter integer state's estimate", "Non-numertic estimate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
              }
            }
            string specialId = (specialIdInput.Text == "") ? null : specialIdInput.Text;

            state.Name = stateName;
            state.Description = stateDescription;
            state.Order = Int32.Parse(stateOdred);
            state.TimeEstimate = stateTimeEstimate;
            state.TaskName = taskName;
            state.SetSpecialId(specialId);
            dataStorageService.UpdateStates();
            this.Close();
        }
    }
}
