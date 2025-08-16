class LevelOfDetailsDefinition < ActiveRecord::Base
  belongs_to :work_product_definition
  has_many :work_product_criterion_definitions
  has_many :wp_checkpoint_definitions, dependent: :destroy
end
