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

namespace Alpha
{
    public partial class PopupWindowForEditAlpha : Form
    {
        private Form1 form1;
        private Alpha alpha;
        private Alpha alphaParent;
        private AlphaContaiment alphaContaiment;
        private WorkProductManifest workProductManifest;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForEditAlpha(Form1 form, Alpha alpha)
        {
            InitializeComponent();
            this.Text = $"Edit {alpha.Name}";
            form1 = form;
            this.alpha = alpha;
            alphaNameInput.Text = alpha.Name;
            alphaDescriptionInput.Text = alpha.Description;

            UpdateListBoxes();
            UpdateAplhaContainmentAndLabel();
            UpdateWorkProductManifestAndLabel();
            UpdateAlphaParentUI();
            UpdateStatesTable();
        }
        private void UpdateAlphaParentUI()
        {
            if (alpha.GetAlphaParent() == null)
            {
                labelAlphaParent.Text = "Alpha parent: null";
                checkBoxKeyAlpha.Visible = false;
                checkBoxKeyAlpha.Checked = false;
            }
            else
            {
                labelAlphaParent.Text = $"Alpha parent: {alpha.GetAlphaParent().GetName()}";
                checkBoxChildAlpha.Checked = true;
                listBoxAlphas.Enabled = true;
                checkBoxKeyAlpha.Visible = true;
                checkBoxKeyAlpha.Checked = alpha.IsKey;
            }
        }
        private void UpdateListBoxes()
        {
            var alphasName = form1.GetListOfAlphas().Select(o => o.Name).ToList();
            foreach (var name in alphasName)
            {
                if (alpha.GetName() != name)
                {
                    listBoxAlphas.Items.Add(name);
                    listBoxOfSubAlphas.Items.Add(name);
                }
            }

            var workProductsName = form1.GetListOfWorkProducts().Select(o => o.Name).ToList();
            foreach (var name in workProductsName)
            {
                listBoxOfWorkProducts.Items.Add(name);
            }
        }
        private void UpdateWorkProductManifestAndLabel()
        {

            workProductManifest = dataStorageService.GetWorkProductManifests()
                .FirstOrDefault(w => w.GetAlphaId() == alpha.GetAlphaId());
            if (workProductManifest == null)
            {
                labelWorkProductManifest.Text = "Work Product: null";
                upperBoundOfWorkProductManifestUpDown.Value = 0;
                lowerBoundOfWorkProductManifestUpDown.Value = 0;
                normalValueWorkProductManifestUpDown.Value = 0;
                return;
            }
            string workProductManifestName = workProductManifest.GetWorkProduct().GetName();
            labelWorkProductManifest.Text = $"Work Product: {workProductManifestName}";
            upperBoundOfWorkProductManifestUpDown.Value = workProductManifest.GetUpperBound();
            lowerBoundOfWorkProductManifestUpDown.Value = workProductManifest.GetLowerBound();
            normalValueWorkProductManifestUpDown.Value = workProductManifest.GetNormalValue();
        }
        private void UpdateAplhaContainmentAndLabel()
        {

            alphaContaiment = dataStorageService.GetAlphaContaiments()
                .FirstOrDefault(a => a.GetSupAlphaId() == alpha.GetAlphaId());
            if (alphaContaiment == null)
            {
                labelSubAlpha.Text = "Subordinate Alpha: null";
                upperBoundOfAlphaCotainmentNumericUpDown.Value = 0;
                lowerBoundOfAlphaCotainmentNumericUpDown.Value = 0;
                normalValueOfAlphaCotainmentNumericUpDown.Value = 0;
                return;
            }
            string subAlphaName = alphaContaiment.GetSubAlpha().GetName();
            labelSubAlpha.Text = $"Subordinate Alpha: {subAlphaName}";
            upperBoundOfAlphaCotainmentNumericUpDown.Value = alphaContaiment.GetUpperBound();
            lowerBoundOfAlphaCotainmentNumericUpDown.Value = alphaContaiment.GetLowerBound();
            normalValueOfAlphaCotainmentNumericUpDown.Value = alphaContaiment.GetNormalValue();
        }
        public void UpdateStatesTable()
        {
            tableLayoutPanelOfStates.Controls.Clear();

            tableLayoutPanelOfStates.RowCount = alpha.GetStates().Count() + 1;
            tableLayoutPanelOfStates.Controls.Add(new Label
            {
                Text = "State",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 0, 0);
            tableLayoutPanelOfStates.Controls.Add(new Label
            {
                Text = "Description",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 1, 0);
            tableLayoutPanelOfStates.Controls.Add(new Label
            {
                Text = "Orders",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 2, 0);
            tableLayoutPanelOfStates.Controls.Add(new Label
            {
                Text = "Checkpoints",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 3, 0);
            tableLayoutPanelOfStates.Controls.Add(new Label
            {
                Text = "Edit",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 4, 0);
            tableLayoutPanelOfStates.Controls.Add(new Label
            {
                Text = "Delete",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 5, 0);
            List<State> states = alpha.GetStates();
            for (int i = 1; i <= states.Count; i++)
            {
                tableLayoutPanelOfStates.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30F));
                State state = states[i - 1];
                Guid stateId = state.GetId();
                Label stateNameLabel = new Label();
                stateNameLabel.Text = state.Name;

                Label stateDescription = new Label();
                stateDescription.Text = state.Description;

                Label stateOrder = new Label();
                stateOrder.Text = state.Order.ToString();

                Button openCheckpointsButton = new Button();
                openCheckpointsButton.Text = "Open";
                openCheckpointsButton.AccessibleName = stateId.ToString();
                openCheckpointsButton.Click += new EventHandler(buttonOpenCheckpointTable_Click);

                Button deleteStateButton = new Button();
                deleteStateButton.Text = "Delete";
                deleteStateButton.AccessibleName = stateId.ToString();
                deleteStateButton.Click += new EventHandler(buttonDeleteState_Click);

                Button editStateButton = new Button();
                editStateButton.Text = "Edit";
                editStateButton.AccessibleName = stateId.ToString();
                editStateButton.Click += new EventHandler(buttonEditState_Click);


                tableLayoutPanelOfStates.Controls.Add(stateNameLabel, 0, i);
                tableLayoutPanelOfStates.Controls.Add(stateDescription, 1, i);
                tableLayoutPanelOfStates.Controls.Add(stateOrder, 2, i);
                tableLayoutPanelOfStates.Controls.Add(openCheckpointsButton, 3, i);
                tableLayoutPanelOfStates.Controls.Add(editStateButton, 4, i);
                tableLayoutPanelOfStates.Controls.Add(deleteStateButton, 5, i);
            }

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string alphaName = alphaNameInput.Text;
            if (alphaName == null || alphaName == "")
            {
                MessageBox.Show("Please enter alpha's name", "Nullable name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string alphaDescription = alphaDescriptionInput.Text;
            if (alphaDescription == null || alphaDescription == "")
            {
                MessageBox.Show("Please enter alpha's description", "Nullable description", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EditAlphaParent();

            foreach (var a in form1.GetListOfAlphas())
            {
                if (alpha.Name == alphaName)
                {
                    alpha.Description = alphaDescription;
                    dataStorageService.UpdateAlphas();
                    form1.UpdateAlphasTable();
                    this.Close();
                    return;
                }
                if (a.Name == alphaName)
                {
                    MessageBox.Show("Alpha with same name already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            alpha.Name = alphaName;
            alpha.Description = alphaDescription;
            dataStorageService.UpdateAlphas();
            form1.UpdateAlphasTable();
            this.Close();
        }

        private void checkBoxChildAlpha_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxChildAlpha.Checked)
            {
                listBoxAlphas.Enabled = true;
                alphaParent = alpha.GetAlphaParent();
                checkBoxKeyAlpha.Visible = true;
            }
            else
            {
                listBoxAlphas.Enabled = false;
                alphaParent = null;
                checkBoxKeyAlpha.Visible = false;
                checkBoxKeyAlpha.Checked = false;
            }
        }
        private void EditAlphaParent()
        {
            if (checkBoxChildAlpha.Checked)
            {
                var alphaPatentName = listBoxAlphas.Text;
                if (alphaPatentName != null && alphaPatentName != "")
                {
                    var allAlphas = form1.GetListOfAlphas();
                    alphaParent = allAlphas.FirstOrDefault(a => a.Name == alphaPatentName);
                    alpha.SetParentAlpha(alphaParent);
                    alpha.IsKey = checkBoxKeyAlpha.Checked;
                }
                else
                {
                    alpha.DeleteAlphaParent();
                    alpha.IsKey = false;
                }
            }
            else
            {
                alpha.DeleteAlphaParent();
                alpha.IsKey = false;
            }
        }

        private void buttonAddState_Click(object sender, EventArgs e)
        {
            PopupWindowForAddState popupWindowForAddState = new PopupWindowForAddState(alpha);
            popupWindowForAddState.ShowDialog();
            UpdateStatesTable();
        }
        private void buttonOpenCheckpointTable_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Guid stateId = Guid.Parse(b.AccessibleName);
            List<State> alphaStates = alpha.GetStates();
            var state = alphaStates.First(s => s.GetId() == stateId);
            if (state == null)
            {
                MessageBox.Show("Some problems with state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopupWindowForCheckpointsTable popupWindowForCheckpointsTable = new PopupWindowForCheckpointsTable(state);
            popupWindowForCheckpointsTable.ShowDialog();
        }
        private void buttonEditState_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Guid stateId = Guid.Parse(b.AccessibleName);
            List<State> states = alpha.GetStates();
            State state = states.First(s => s.GetId() == stateId);
            if (state == null)
            {
                MessageBox.Show("Some problems with state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopupWindowForEditState popupWindowForEditState = new PopupWindowForEditState(state);
            popupWindowForEditState.ShowDialog();
            UpdateStatesTable();
        }
        private void buttonDeleteState_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            Button b = (Button)sender;
            Guid stateId = Guid.Parse(b.AccessibleName);
            List<State> states = alpha.GetStates();
            State state = states.First(s => s.GetId() == stateId);
            if (state == null)
            {
                MessageBox.Show("Some problems with state", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            states.Remove(state);
            dataStorageService.RemoveState(state);
            UpdateStatesTable();
        }

        private void buttonSaveConteinment_Click(object sender, EventArgs e)
        {
            var subAlphaName = listBoxOfSubAlphas.Text;
            if (subAlphaName == null || subAlphaName == "" && alphaContaiment == null)
            {
                MessageBox.Show("Subordinate Alpha was not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var allAlphas = form1.GetListOfAlphas();
            var subAlpha = allAlphas.FirstOrDefault(a => a.Name == subAlphaName);
            if (subAlpha == null && alphaContaiment == null)
            {
                MessageBox.Show("Some problems with Subordinate Alpha", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal upperBound = upperBoundOfAlphaCotainmentNumericUpDown.Value;
            decimal lowerBound = lowerBoundOfAlphaCotainmentNumericUpDown.Value;
            int normalValue = (int)normalValueOfAlphaCotainmentNumericUpDown.Value;
            if ((int)upperBound == (int)lowerBound && normalValue != (int)upperBound)
            {
                MessageBox.Show(
                    $"Значение некорректно, normal value был изменён на {(int)upperBound}",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                normalValue = (int)upperBound;
            }
            if (alphaContaiment == null)
            {
                alphaContaiment = new AlphaContaiment(upperBound, lowerBound, alpha, subAlpha,normalValue);
                alpha.SetSupperAlphaContainment(alphaContaiment);
                subAlpha.AddSubordinateAlphaContainment(alphaContaiment);
                form1.AddAlphaConteinment(alphaContaiment);
            }
            else
            {
                if (subAlpha == null)
                {
                    subAlpha = alphaContaiment.GetSubAlpha();
                }
                subAlpha.DeleteSubordinateAlphaContainment(alphaContaiment);
                alphaContaiment.SetSubAlpha(subAlpha);
                alphaContaiment.UpperBound = upperBound;
                alphaContaiment.LowerBound = lowerBound;
                alphaContaiment.NormalValue = normalValue;
                dataStorageService.UpdateAlphaContaiments();

            }
            UpdateAplhaContainmentAndLabel();
        }

        private void buttonSaveWorkProductManifest_Click(object sender, EventArgs e)
        {
            var workProductName = listBoxOfWorkProducts.Text;
            if (workProductName == null || workProductName == "" && workProductManifest == null)
            {
                MessageBox.Show("Work product was not selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var allWorkProducts = form1.GetListOfWorkProducts();
            var workProduct = allWorkProducts.FirstOrDefault(wp => wp.GetName() == workProductName);
            if (workProduct == null && workProductManifest == null)
            {
                MessageBox.Show("Some problems with Work Product Manifest", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            decimal upperBound = upperBoundOfWorkProductManifestUpDown.Value;
            decimal lowerBound = lowerBoundOfWorkProductManifestUpDown.Value;
            int normalValue = (int)normalValueWorkProductManifestUpDown.Value;
            if((int)upperBound == (int)lowerBound && normalValue != (int)upperBound)
            {
                MessageBox.Show(
                    $"Значение некорректно, normal value был изменён на {(int)upperBound}",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                normalValue = (int)upperBound;
            }
            if (workProductManifest == null)
            {
                workProductManifest = new WorkProductManifest(upperBound, lowerBound, alpha, workProduct,normalValue);
                alpha.SetWorkProductManifest(workProductManifest);
                workProduct.AddWorkProductManifest(workProductManifest);
                dataStorageService.AddWorkProductManifest(workProductManifest);
            }
            else
            {
                if (workProduct == null)
                {
                    workProduct = workProductManifest.GetWorkProduct();
                }
                workProduct.DeleteWorkProductManifest(workProductManifest);
                workProductManifest.SetWorkProduct(workProduct);
                workProductManifest.UpperBound = upperBound;
                workProductManifest.LowerBound = lowerBound;
                workProductManifest.NormalValue = normalValue;
                dataStorageService.UpdateWorkProductManifests();

            }
            UpdateWorkProductManifestAndLabel();
        }

        private void buttonDeleteWorkProductManifest_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            if (workProductManifest == null)
            {
                return;
            }
            workProductManifest.GetAlpha().DeleteWorkProductManifest();
            WorkProduct workProduct = workProductManifest.GetWorkProduct();
            workProduct.DeleteWorkProductManifest(workProductManifest);
            form1.DeleteWorkProductManifestFromList(workProductManifest);
            workProductManifest = null;
            UpdateWorkProductManifestAndLabel();
        }

        private void buttonDeleteAlphaContainment_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            if (alphaContaiment == null)
            {
                return;
            }
            alpha.DeleteSupperAlphaContainment();
            Alpha subordinateAlpha = alphaContaiment.GetSubAlpha();
            subordinateAlpha.DeleteSubordinateAlphaContainment(alphaContaiment);
            form1.DeleteAlphaConteinmentFromList(alphaContaiment);
            alphaContaiment = null;
            UpdateAplhaContainmentAndLabel();
        }
  }
}
