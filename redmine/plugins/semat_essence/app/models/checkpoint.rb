class Checkpoint < ActiveRecord::Base
  belongs_to :definition, class_name: "CheckpointDefinition", foreign_key: "checkpoint_definition_id"
  belongs_to :alpha
end
