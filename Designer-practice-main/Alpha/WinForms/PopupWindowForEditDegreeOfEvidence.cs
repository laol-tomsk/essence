using Alpha.Enums;
using Alpha.Models;
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

namespace Alpha.WinForms
{
    public partial class PopupWindowForEditDegreeOfEvidence : Form
    {
        private Checkpoint checkpoint;
        private DegreeOfEvidence degreeOfEvidence;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForEditDegreeOfEvidence(Checkpoint checkpoint, DegreeOfEvidence degreeOfEvidence)
        {
            InitializeComponent();
            this.checkpoint = checkpoint;
            this.degreeOfEvidence = degreeOfEvidence;            
            BindListBox();
            BindComboboxWithEnum();
            UpdateLabels();
        }
        private void UpdateLabels()
        {
            labelICheckable.Text = $"ICheckable: {degreeOfEvidence.GetICheckable().GetName()}";
            comboBoxDegreeOfEvidence.SelectedIndex = (int)degreeOfEvidence.GetDegreeOfEvidenceEnumValue();
            checkBoxTypeOfEvidence.Checked = degreeOfEvidence.GetTypeOfEvidence();
        }
        private void BindListBox()
        {
            listBoxOfICheckable.DataSource = dataStorageService.GetICheckables();
            listBoxOfICheckable.DisplayMember = "Name";
            listBoxOfICheckable.ValueMember = "Id";
        }
        private void BindComboboxWithEnum()
        {
            comboBoxDegreeOfEvidence.DataSource = Enum.GetValues(typeof(DegreeOfEvidenceEnum));
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var icheckable = listBoxOfICheckable.SelectedItem;
            if (icheckable == null)
            {
                MessageBox.Show("Please choose ichekable", "Nullable icheckable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ICheckable icheckableCopy = (ICheckable)icheckable;
            bool typeOfEvidenceBool = checkBoxTypeOfEvidence.Checked;

            DegreeOfEvidenceEnum degreeOfEvidenceEnum;
            Enum.TryParse<DegreeOfEvidenceEnum>(comboBoxDegreeOfEvidence.SelectedValue.ToString(), out degreeOfEvidenceEnum);
            degreeOfEvidence.SetTypeOfEvidence(typeOfEvidenceBool);
            degreeOfEvidence.SetICheckable(icheckableCopy);
            degreeOfEvidence.SetDegreeOfEvidenceEnum(degreeOfEvidenceEnum);
            dataStorageService.UpdateDegreesOfEvidence();
            this.Close();
        }


    }
}
