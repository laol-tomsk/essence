class AlphaContainment < ActiveRecord::Base
  belongs_to :super, class_name: "AlphaDefinition", foreign_key: "super_id"
  belongs_to :subordinate, class_name: "AlphaDefinition", foreign_key: "subordinate_id"
end
