class WpCheckpointDefinition < ActiveRecord::Base
  belongs_to :level_of_details_definition
  has_many :wp_checkpoints
end