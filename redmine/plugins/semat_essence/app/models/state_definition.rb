class StateDefinition < ActiveRecord::Base
  belongs_to :alpha_definition
  has_many :checkpoint_definitions, dependent: :destroy
  has_many :alpha_criterion_definitions

  def achieved?(alpha)
    state_checkpoints = alpha.checkpoints.select { |i| i.definition.state_definition.id == self.id }
    fulfilled_checkpoints = state_checkpoints.select { |i| i.fulfilled == true }
    state_checkpoints.length == fulfilled_checkpoints.length
  end

  def completed?(alpha)
    state_checkpoints = alpha.checkpoints.select { |i| i.definition.state_definition.id == self.id }
    fulfilled_checkpoints = state_checkpoints.select { |i| i.fulfilled == true }
    state_checkpoints.length == fulfilled_checkpoints.length
  end
end
