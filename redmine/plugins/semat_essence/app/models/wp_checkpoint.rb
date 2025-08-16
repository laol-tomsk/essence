class WpCheckpoint < ActiveRecord::Base
  belongs_to :definition, class_name: "WpCheckpointDefinition", foreign_key: "wp_checkpoint_definition_id"
  belongs_to :workproduct
end