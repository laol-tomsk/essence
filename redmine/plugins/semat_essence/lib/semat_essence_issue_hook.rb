class SematEssenceIssueHook < Redmine::Hook::ViewListener
  render_on :view_issues_form_details_bottom, partial: "issues/issue_form_activity"
  render_on :view_issues_show_description_bottom, partial: "issues/issue_show_activity"

  # Context:
  # * :issue => Issue being saved
  # * :params => HTML parameters
  def controller_issues_new_before_save(context = {})
    issue = context[:issue]
    params = context[:params]
    if params && params[:issue]
      if params[:issue][:activity_definition_id].present?
        issue.activity_definition_id = params[:issue][:activity_definition_id]
        activity_definition = ActivityDefinition.find(params[:issue][:activity_definition_id])
        if params[:work_product_criterion]
          activity_definition.work_product_criterion_definitions.each do |wp_criterion|
            wp_id = params[:work_product_criterion][wp_criterion.id]
            WorkProductCriterion.create('')
          end
        end
      end
    end
  end

  def controller_issues_new_after_save(context = {})
    issue = context[:issue]
    params = context[:params]
    if params && params[:issue][:activity_definition_id].present? && params[:work_product_criterion]
      activity_definition = ActivityDefinition.find(params[:issue][:activity_definition_id])

      activity_definition.work_product_criterion_definitions.each do |wp_criterion_definition|
        wp_id = params[:work_product_criterion][wp_criterion_definition.id.to_s]
        WorkProductCriterion.create(issue_id: issue.id, work_product_id: wp_id, work_product_criterion_definition_id: wp_criterion_definition.id) if wp_id.present?
      end
    end
  end

  def controller_issues_edit_before_save(context = {})
    issue = context[:issue]
    params = context[:params]
    if params && params[:issue] && params[:issue][:activity_definition_id].present?
      issue.activity_definition_id = params[:issue][:activity_definition_id]
    end
  end

  def controller_issues_edit_after_save(context ={})
    issue = context[:issue]
    params = context[:params]
    if params && params[:completion_alpha]
      params[:completion_alpha].each do |alpha_criterion_definition_id, alpha_id|
        AlphaCriterion.create(issue_id: issue.id, alpha_id: alpha_id, alpha_criterion_definition_id: alpha_criterion_definition_id)
      #   todo update state of alphas except for root alphas
      end
    end

    if params && params[:completion_work_product]
      params[:completion_work_product].each do |wp_criterion_definition_id, wp_id|
        WorkProductCriterion.create(issue_id: issue.id, work_product_id: wp_id, work_product_criterion_definition_id: wp_criterion_definition_id)
        #   todo update state of wp
      end
    end
  end
end