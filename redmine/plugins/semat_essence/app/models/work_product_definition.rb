class WorkProductDefinition < ActiveRecord::Base
  belongs_to :method_definition
  has_many :level_of_details_definitions, dependent: :destroy
  has_one :work_product_manifest, dependent: :delete

  has_one :alpha_definition, through: :work_product_manifest, source: :alpha_definition

  has_many :work_products
end
