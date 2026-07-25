using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alpha.Enums;
using Alpha.Interfaces;
using Alpha.Models;
using Alpha.Services;

namespace Alpha.WinForms
{
    public partial class PopupWindowForEditActivity : Form
    {
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        private Activity activity;
        private string oldActivityName;
        public PopupWindowForEditActivity(Activity activity)
        {
            InitializeComponent();
            this.activity = activity;
            oldActivityName = activity.GetName();
            activityNameInput.Text = activity.GetName();
            activityDescriptionInput.Text = activity.GetDescription();
            this.Text = $"Edit {activity.GetName()}";
            UpdateCriterionsTable(tableLayoutPanelInputCriterions, CriterionTypeEnum.Input);
            UpdateCriterionsTable(tableLayoutPanelOutputCriterions, CriterionTypeEnum.Output);

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            string activityName = activityNameInput.Text;
            if (activityName == null || activityName == "")
            {
                MessageBox.Show("Please enter activity's name", "Nullable name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (oldActivityName != activityName)
            {
                foreach (var a in dataStorageService.GetActivities())
                {

                    if (a.GetName() == activityName)
                    {
                        MessageBox.Show("Activity with same name already exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            string activityDescription = activityDescriptionInput.Text;
            if (activityDescription == null || activityDescription == "")
            {
                MessageBox.Show("Please enter activity's description", "Nullable description", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            activity.SetName(activityName);
            activity.SetDescription(activityDescription);
            dataStorageService.UpdateActivities();
        }

        public void UpdateCriterionsTable(TableLayoutPanel tableLayoutPanel, CriterionTypeEnum criterionTypeEnum)
        {
            List<ICriterion> criteriaByEnum = dataStorageService.GetCriterions().Where(c => c.GetCriterionType() == criterionTypeEnum &&
            activity.GetId() == c.GetActivityId()).ToList();

            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.RowCount = criteriaByEnum.Count() + 1;
            tableLayoutPanel.Controls.Add(new Label
            {
                Text = "Detail",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 0, 0);
            tableLayoutPanel.Controls.Add(new Label
            {
                Text = "Partial",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 1, 0);
            tableLayoutPanel.Controls.Add(new Label
            {
                Text = "Minimal",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 2, 0);
            tableLayoutPanel.Controls.Add(new Label
            {
                Text = "Edit",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 3, 0);
            tableLayoutPanel.Controls.Add(new Label
            {
                Text = "Delete",
                Font = new Font(Label.DefaultFont, FontStyle.Bold)
            }, 4, 0);
            for (int i = 1; i <= criteriaByEnum.Count; i++)
            {
                tableLayoutPanelInputCriterions.RowStyles.Add(new RowStyle(SizeType.AutoSize, 30F));
                ICriterion criterion = criteriaByEnum[i - 1];
                Guid criterionId = criterion.GetId();
                Label criterionDetailName = new Label();
                criterionDetailName.Text = criterion.GetDetail().GetName();

                Label criterionPartial = new Label();
                bool partial = criterion.GetPartial();
                criterionPartial.Text = partial.ToString();

                Label criterionMinimal = new Label();
                criterionMinimal.Text = criterion.GetMinimal().ToString();


                Button deleteStateButton = new Button();
                deleteStateButton.Text = "Delete";
                deleteStateButton.AccessibleName = criterionId.ToString();
                deleteStateButton.Click += new EventHandler(buttonDeleteCriterion_Click);

                Button editStateButton = new Button();
                editStateButton.Text = "Edit";
                editStateButton.AccessibleName = criterionId.ToString();
                editStateButton.Click += new EventHandler(buttonEditCriterion_Click);


                tableLayoutPanel.Controls.Add(criterionDetailName, 0, i);
                tableLayoutPanel.Controls.Add(criterionPartial, 1, i);
                tableLayoutPanel.Controls.Add(criterionMinimal, 2, i);
                tableLayoutPanel.Controls.Add(editStateButton, 3, i);
                tableLayoutPanel.Controls.Add(deleteStateButton, 4, i);
            }

        }
        private void buttonDeleteCriterion_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }
            Button b = (Button)sender;
            Guid criterionId = Guid.Parse(b.AccessibleName);
            ICriterion criterion = dataStorageService.GetCriterions().FirstOrDefault(c => c.GetId() == criterionId);
            if (criterion == null)
            {
                MessageBox.Show("Some problems with criterion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DoCorrectCriterionRemove(criterion);
            UpdateCriterionsTable(tableLayoutPanelInputCriterions, CriterionTypeEnum.Input);
            UpdateCriterionsTable(tableLayoutPanelOutputCriterions, CriterionTypeEnum.Output);
        }

        private void buttonEditCriterion_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            Guid criterionId = Guid.Parse(b.AccessibleName);
            ICriterion criterion = dataStorageService.GetCriterions().FirstOrDefault(c => c.GetId() == criterionId);
            if (criterion == null)
            {
                MessageBox.Show("Some problems with criterion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopupWindowForEditCriterion popupWindowForEditCriterion = new PopupWindowForEditCriterion(criterion);
            popupWindowForEditCriterion.ShowDialog();
            UpdateCriterionsTable(tableLayoutPanelInputCriterions, CriterionTypeEnum.Input);
            UpdateCriterionsTable(tableLayoutPanelOutputCriterions, CriterionTypeEnum.Output);
        }
        private void DoCorrectCriterionRemove(ICriterion criterion)
        {
            if (criterion.GetType() == typeof(AlphaCriterion))
            {
                activity.RemoveAlphaCriterion((AlphaCriterion)criterion);
                dataStorageService.RemoveAlphaCriterion((AlphaCriterion)criterion);
                return;
            }
            if (criterion.GetType() == typeof(WorkProductCriterion))
            {
                activity.RemoveWorkProductCriterion((WorkProductCriterion)criterion);
                dataStorageService.RemoveWorkProductCriterion((WorkProductCriterion)criterion);
                return;
            }
        }
        private void buttonAddInput_Click(object sender, EventArgs e)
        {
            if (activity == null)
            {
                MessageBox.Show("Create an activity before adding criteria", "Nullable activity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopupWindowForAddCriterionInTable popupWindowForAddCriterionInTable = new PopupWindowForAddCriterionInTable(activity, Enums.CriterionTypeEnum.Input);
            popupWindowForAddCriterionInTable.ShowDialog();
            UpdateCriterionsTable(tableLayoutPanelInputCriterions, CriterionTypeEnum.Input);
        }

        private void buttonAddOutput_Click(object sender, EventArgs e)
        {
            if (activity == null)
            {
                MessageBox.Show("Create an activity before adding criteria", "Nullable activity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PopupWindowForAddCriterionInTable popupWindowForAddCriterionInTable = new PopupWindowForAddCriterionInTable(activity, Enums.CriterionTypeEnum.Output);
            popupWindowForAddCriterionInTable.ShowDialog();
            UpdateCriterionsTable(tableLayoutPanelOutputCriterions, CriterionTypeEnum.Output);
        }
    }
}
