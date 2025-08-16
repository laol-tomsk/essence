class AlphaCriterion < ActiveRecord::Base
  belongs_to :issue
  belongs_to :alpha
  belongs_to :alpha_criterion_definition
end
