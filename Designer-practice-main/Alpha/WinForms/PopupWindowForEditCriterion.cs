using Alpha.Interfaces;
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

namespace Alpha.WinForms
{
    public partial class PopupWindowForEditCriterion : Form
    {
        private ICriterion criterion;
        private DataStorageService dataStorageService = DataStorageService.GetInstance();
        public PopupWindowForEditCriterion(ICriterion criterion)
        {
            InitializeComponent();
            this.criterion = criterion;
            UpdateLabels();
        }
        private void UpdateLabels()
        {
            numericUpDownMinimal.Value = criterion.GetMinimal();
            checkBoxPartial.Checked = criterion.GetPartial();
            UpdateDetailLabel();
            BindListBox();
        }
        private void UpdateDetailLabel()
        {
            labelDetail.Text = $"States and Levels of Details: {criterion.GetDetail().GetName()}";
        }
        private void BindListBox()
        {
            listBoxDetails.DataSource = dataStorageService.GetDetails();
            listBoxDetails.DisplayMember = "Name";
            listBoxDetails.ValueMember = "Id";
        }
        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            var selectedObject = listBoxDetails.SelectedItem;
            var partial = checkBoxPartial.Checked;
            var minimal = (int)numericUpDownMinimal.Value;
            IDetailing detail;
            detail = (selectedObject == null) ? criterion.GetDetail() : (IDetailing)selectedObject;
            
            if (detail.GetType() == criterion.GetDetail().GetType())
            {
                criterion.SetDetail(detail);
                criterion.SetPartial(partial);
                criterion.SetMinimal(minimal);
                UpdateCorrectCriterionsList(criterion);

            }
            else if (detail.GetType() == typeof(LevelOfDetail))
            {
                WorkProductCriterion workProductCriterion = new WorkProductCriterion(criterion.GetCriterionType(), partial, minimal, criterion.GetActivity(), (LevelOfDetail)detail);
                Activity activity = criterion.GetActivity();
                CriterionCorrectRemove(activity);
                activity.AddWorkProductCriterion(workProductCriterion);
                dataStorageService.AddWorkProductCriterion(workProductCriterion);
                criterion = workProductCriterion;
            }
            else if (detail.GetType() == typeof(State))
            {
                AlphaCriterion alphaCriterion = new AlphaCriterion(criterion.GetCriterionType(), partial, minimal, criterion.GetActivity(), (State)detail);
                Activity activity = criterion.GetActivity();
                CriterionCorrectRemove(activity);
                activity.AddAlphaCriterion(alphaCriterion);
                dataStorageService.AddAlphaCriterion(alphaCriterion);
                criterion = alphaCriterion;
            }
            UpdateDetailLabel();
            this.Close();
        }
        private void CriterionCorrectRemove(Activity activity)
        {
            if (criterion.GetType() == typeof(WorkProductCriterion))
            {
                activity.RemoveWorkProductCriterion((WorkProductCriterion)criterion);
                dataStorageService.RemoveWorkProductCriterion((WorkProductCriterion)criterion);
                return;
            }
            if (criterion.GetType() == typeof(AlphaCriterion))
            {
                activity.RemoveAlphaCriterion((AlphaCriterion)criterion);
                dataStorageService.RemoveAlphaCriterion((AlphaCriterion)criterion);
                return;
            }
        }
        private void UpdateCorrectCriterionsList(ICriterion criterion)
        {
            if (criterion.GetType() == typeof(WorkProductCriterion))
            {
                dataStorageService.UpdateWorkProductCriterions();
                this.Close();
                return;
            }
            if (criterion.GetType() == typeof(AlphaCriterion))
            {
                dataStorageService.UpdateAlphaCriterions();
                this.Close();
                return;
            }
        }
    }
}
