require_dependency 'issues_controller'

module IssuesControllerPatch
  def self.included(base)
    base.send(:include, InstanceMethods)

    base.class_eval do
      unloadable # Send unloadable so it will not be unloaded in development
      before_action :authorize, :except => [:add_criterions_to_issue_form, :get_completion_criterions]
    end
  end

  module InstanceMethods
    def add_criterions_to_issue_form
      @project = Project.find(params[:project_id])
      @activity_definition = ActivityDefinition.find(params[:issue][:activity_definition_id])

      respond_to do |format|
        format.html {redirect_to @activity_definition}
        format.js
      end
    end

    def get_completion_criterions
      @project = Project.find(params[:project_id])
      @activity_definition = ActivityDefinition.find(params[:activity_definition_id])

      alpha_criterions = @activity_definition.alpha_completion_criterion_definition
      wp_criterions = @activity_definition.wp_completion_criterion_definition
      hash = {}

      alpha_criterions.each do |criterion|
        avaliable_alphas = criterion.get_available_alphas(@project.id).map do |alpha|
          {id: alpha.id, parent_id: alpha.parent_id, title: alpha.name_with_state }
        end

        hash[criterion.get_alpha_definition.id] = {
            data: {
                id: criterion.id,
                type: 'alpha',
                label: criterion.name_with_state,
                options: avaliable_alphas
            },
            childs: Array.new,
            isChild: false,
        }
      end

      alpha_criterions.each do |parent|
        alpha_criterions.each do |child|
          if parent.get_alpha_definition.isParentOf?(child.get_alpha_definition)
            child_el = hash[child.get_alpha_definition.id]
            child_el[:isChild] = true
            hash[parent.get_alpha_definition.id][:childs].push(child_el)
          end
        end
      end

      wp_criterions.each do |criterion|

        avaliable_wps = criterion.get_available_wp(@project.id).map do |wp|
          {id: wp.id, parent_id: wp.containing_alpha.id, title: wp.name_with_achieved_level }
        end

        criterionObj = {
            data: {
                id: criterion.id,
                type: 'work-product',
                label: criterion.name_with_level,
                options: avaliable_wps
            },
            childs: Array.new,
            isChild: true
        }

        parentAlpha = hash[criterion.get_wp_definition.alpha_definition.id]
        if parentAlpha.present?
          parentAlpha[:childs].push(criterionObj)
        else
          criterionObj[:isChild] = false
          hash[criterion.get_wp_definition.id] = criterionObj
        end
      end

      @roots = Array.new

      alpha_criterions.each do |criterion|
        if hash[criterion.get_alpha_definition.id][:isChild] == false
          @roots.push(hash[criterion.get_alpha_definition.id])
        end
      end

      wp_criterions.each do |criterion|
        if hash[criterion.get_wp_definition.id][:isChild] == false
          @roots.push(hash[criterion.get_wp_definition.id])
        end
      end

      respond_to do |format|
        format.html {redirect_to @activity_definition}
        format.js
      end
    end

  end
end

IssuesController.send(:include, IssuesControllerPatch)