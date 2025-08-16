class MethodDefinition < ActiveRecord::Base
  has_many :projects
  has_many :activity_definitions, dependent: :destroy
  has_many :work_product_definitions, dependent: :destroy
  has_many :alpha_definitions, dependent: :destroy

  def get_base_alphas
    self.alpha_definitions.select{|i| i.parent == nil}
  end
end
