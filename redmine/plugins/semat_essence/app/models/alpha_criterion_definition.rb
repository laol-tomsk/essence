class AlphaCriterionDefinition < ActiveRecord::Base
  belongs_to :activity_definition
  belongs_to :state_definition
  enum criterion_type: { entry: 0, completion: 1 }

  def get_alpha_definition
    self.state_definition.alpha_definition
  end

  def name_with_state
    state_name = self.state_definition.name
    alpha_name = self.state_definition.alpha_definition.name
    "#{alpha_name}: #{state_name}"
  end

  def get_available_alphas(project_id)
    project = Project.find(project_id)
    required_alpha_id = self.state_definition.alpha_definition.id
    required_state_order_id = self.state_definition.order

    project.alphas.select do |alpha|
      if self.entry?
        alpha.definition.id == required_alpha_id && alpha.achieved_state.present? && alpha.achieved_state.order == required_state_order_id
      else
        alpha.definition.id == required_alpha_id
      end
    end
  end
end
