class WorkProductManifest < ActiveRecord::Base
  belongs_to :work_product_definition
  belongs_to :alpha_definition
end
