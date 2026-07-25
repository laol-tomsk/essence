using Alpha.Enums;
using Alpha.Interfaces;
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
    public partial class PopupWindowForAddCriterionInTable : Form
    {
        private Activity activity;
        private CriterionTypeEnum criterionTypeEnum;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForAddCriterionInTable(Activity activity, CriterionTypeEnum criterionTypeEnum)
        {
            InitializeComponent();
            this.activity = activity;
            this.criterionTypeEnum = criterionTypeEnum;
            BindListBox();
        }
        private void BindListBox()
        {
            listBoxDetails.DataSource = dataStorageService.GetDetails();
            listBoxDetails.DisplayMember = "Name";
            listBoxDetails.ValueMember = "Id";
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var selectedObject = listBoxDetails.SelectedItem;
            if (selectedObject == null)
            {
                MessageBox.Show("Please choose detail", "Nullable detail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            IDetailing detail = (IDetailing)selectedObject;
            bool partial = checkBoxPartial.Checked;
            int minimal = (int)numericUpDownMinimal.Value;

            if(detail.GetType() == typeof(State))
            {
                AlphaCriterion alphaCriterion = new AlphaCriterion(criterionTypeEnum, partial, minimal, activity, (State)detail);
                dataStorageService.AddAlphaCriterion(alphaCriterion);
                activity.AddAlphaCriterion(alphaCriterion);
            }
            if (detail.GetType() == typeof(LevelOfDetail))
            {
                WorkProductCriterion workProductCriterion = new WorkProductCriterion(criterionTypeEnum, partial, minimal, activity, (LevelOfDetail)detail);
                dataStorageService.AddWorkProductCriterion(workProductCriterion);
                activity.AddWorkProductCriterion(workProductCriterion);
            }
            this.Close();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
