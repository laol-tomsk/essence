class WorkProductCriterion < ActiveRecord::Base
  belongs_to :issue
  belongs_to :work_product
  belongs_to :work_product_criterion_definition
end
