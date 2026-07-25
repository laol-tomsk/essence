using Alpha.Services;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alpha.Interfaces;
using Alpha.Enums;

namespace Alpha.WinForms
{

    public partial class PopupWindowForAddDegreeOfEvidence : Form
    {
        private Checkpoint checkpoint;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForAddDegreeOfEvidence(Checkpoint checkpoint)
        {
            InitializeComponent();
            this.checkpoint = checkpoint;
            BindListBox();
            BindComboboxWithEnum();
        }

        public PopupWindowForAddDegreeOfEvidence(Checkpoint checkpoint, string ManagerSpecelId)
        {
            InitializeComponent();
            this.checkpoint = checkpoint;
            List<ICheckable> newList = new List<ICheckable>
            {
                dataStorageService.GetICheckables().First(x => x.SpecialId == ManagerSpecelId)
            };
            listBoxOfICheckable.DataSource = newList;
            listBoxOfICheckable.DisplayMember = "Name";
            listBoxOfICheckable.ValueMember = "Id";
            BindComboboxWithEnum();
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

        private void buttonAdd_Click(object sender, EventArgs e)
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
            DegreeOfEvidence degreeOfEvidence = new DegreeOfEvidence(checkpoint, icheckableCopy, typeOfEvidenceBool, degreeOfEvidenceEnum);
            checkpoint.AddDegreeOfEvidence(degreeOfEvidence);
            dataStorageService.AddDegreeOfEvidence(degreeOfEvidence);
            this.Close();
        }

        private void comboBoxDegreeOfEvidence_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
