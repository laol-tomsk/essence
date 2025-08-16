class CheckpointDefinition < ActiveRecord::Base
  belongs_to :state_definition
  has_many :checkpoints
end
