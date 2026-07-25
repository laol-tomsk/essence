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
using Alpha.Models;
using Alpha.Services;

namespace Alpha.WinForms
{
    public partial class PopupWindowForCheckpointDegreeOfEvidences : Form
    {
        private IBaseObject baseObject;
        private IDetailing datail;
        private Checkpoint checkpoint;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForCheckpointDegreeOfEvidences(Checkpoint checkpoint, IBaseObject baseObject, IDetailing datail)
        {
            InitializeComponent();
            this.baseObject = baseObject;
            this.datail = datail;
            this.checkpoint = checkpoint;
            InitAllLabels();
            UpdateDegreesOfEvidenceTable();
        }
        public void UpdateDegreesOfEvidenceTable()
        {
            tableLayoutPanelDegreeOfEvidence.Controls.Clear();
            List<DegreeOfEvidence> degreeOfEvidences = dataStorageService.GetDegreesOfEvidence()
                .Where(d=>d.GetCheckpointId()==checkpoint.GetId()).ToList();
            tableLayoutPanelDegreeOfEvidence.RowCount = degreeOfEvidences.Count() + 1;
            tableLayoutPanelDegreeOfEvidence.Controls.Add(new Label
            {
                Text = "ID",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                AutoSize = true
            }, 0, 0);
            tableLayoutPanelDegreeOfEvidence.Controls.Add(new Label
            {
                Text = "ICheckable name",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                AutoSize = true
            }, 1, 0);
            tableLayoutPanelDegreeOfEvidence.Controls.Add(new Label
            {
                Text = "Type of evidence",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                AutoSize = true
            }, 2, 0);
            tableLayoutPanelDegreeOfEvidence.Controls.Add(new Label
            {
                Text = "Degree of evidence",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                AutoSize = true
            }, 3, 0);
            tableLayoutPanelDegreeOfEvidence.Controls.Add(new Label
            {
                Text = "Edit",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                AutoSize = true
            }, 4, 0);
            tableLayoutPanelDegreeOfEvidence.Controls.Add(new Label
            {
                Text = "Delete",
                Font = new Font(Label.DefaultFont, FontStyle.Bold),
                AutoSize = true
            }, 5, 0);

            for (int i = 1; i <= degreeOfEvidences.Count; i++)
            {
                tableLayoutPanelDegreeOfEvidence.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30F));
                DegreeOfEvidence degreeOfEvidence = degreeOfEvidences[i - 1];
                Guid alphaId = degreeOfEvidence.GetId();

                Label ichekableSpecialIdLabel = new Label();
                ichekableSpecialIdLabel.AutoSize = true;
                string specialIdOfDegreeOfEvidence = degreeOfEvidence.GetICheckableSpecialId();
                ichekableSpecialIdLabel.Text = specialIdOfDegreeOfEvidence == null ? "Null" : specialIdOfDegreeOfEvidence;


                Label ichekableNameLabel = new Label();
                ichekableNameLabel.AutoSize = true;
                ichekableNameLabel.Text = degreeOfEvidence.GetICheckable().GetName();

                Label typeOfEvidenceLabel = new Label();
                typeOfEvidenceLabel.AutoSize = true;
                typeOfEvidenceLabel.Text = degreeOfEvidence.GetTypeOfEvidence() == true ? "Positive" : "Negative";

                Label degreeOfEvidenceLabel = new Label();
                degreeOfEvidenceLabel.AutoSize = true;
                degreeOfEvidenceLabel.Text = $"{degreeOfEvidence.GetDegreeOfEvidenceEnumValue()}";


                Button editButton = new Button();
                editButton.Text = "Edit";
                editButton.AccessibleName = alphaId.ToString();
                editButton.Click += new EventHandler(buttonEditDegreeOfEvidence_Click);

                Button deleteButton = new Button();
                deleteButton.Text = "Delete";
                deleteButton.AccessibleName = alphaId.ToString();
                deleteButton.Click += new EventHandler(buttonDeleteDegreeOfEvidence_Click);

                tableLayoutPanelDegreeOfEvidence.Controls.Add(ichekableSpecialIdLabel, 0, i);
                tableLayoutPanelDegreeOfEvidence.Controls.Add(ichekableNameLabel, 1, i);
                tableLayoutPanelDegreeOfEvidence.Controls.Add(typeOfEvidenceLabel, 2, i);
                tableLayoutPanelDegreeOfEvidence.Controls.Add(degreeOfEvidenceLabel, 3, i);
                tableLayoutPanelDegreeOfEvidence.Controls.Add(editButton, 4, i);
                tableLayoutPanelDegreeOfEvidence.Controls.Add(deleteButton, 5, i);
            }
        }
        private void InitAllLabels()
        {
            baseObjectNameTextBox.Text = baseObject.GetName();
            baseObjectDescriptionTextBox.Text = baseObject.GetDescription();
            checkpointNameTextBox.Text = checkpoint.GetName();
            checkpointDescriptionTextBox.Text = checkpoint.GetDescription();
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAddDegreeOfEvidence_Click(object sender, EventArgs e)
        {
            PopupWindowForAddDegreeOfEvidence popupWindowForAddDegreeOfEvidence = new PopupWindowForAddDegreeOfEvidence(checkpoint);
            popupWindowForAddDegreeOfEvidence.ShowDialog();
            UpdateDegreesOfEvidenceTable();
        }
        private void buttonEditDegreeOfEvidence_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Guid degreeOfEvidenceId = Guid.Parse(b.AccessibleName);
            List<DegreeOfEvidence> degreeOfEvidences = checkpoint.GetDegreeOfEvidences();
            DegreeOfEvidence degreeOfEvidence = degreeOfEvidences.First(d => d.GetId() == degreeOfEvidenceId);
            if (degreeOfEvidence == null)
            {
                MessageBox.Show("Some problems with degree of evidence", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopupWindowForEditDegreeOfEvidence popupWindowForEditDegreeOfEvidence = new PopupWindowForEditDegreeOfEvidence(checkpoint, degreeOfEvidence);
            popupWindowForEditDegreeOfEvidence.ShowDialog();
            UpdateDegreesOfEvidenceTable();
        }
        private void buttonDeleteDegreeOfEvidence_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            Button b = (Button)sender;
            Guid degreeOfEvidenceId = Guid.Parse(b.AccessibleName);
            List<DegreeOfEvidence> degreeOfEvidences = checkpoint.GetDegreeOfEvidences();
            DegreeOfEvidence degreeOfEvidence = degreeOfEvidences.First(d => d.GetId() == degreeOfEvidenceId);
            if (degreeOfEvidence == null)
            {
                MessageBox.Show("Some problems with degree of evidence", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            checkpoint.RemoveDegreeOfEvidence(degreeOfEvidence);
            dataStorageService.RemoveDegreeOfEvidence(degreeOfEvidence);
            UpdateDegreesOfEvidenceTable();
        }
    }
}
