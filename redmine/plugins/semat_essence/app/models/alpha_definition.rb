class AlphaDefinition < ActiveRecord::Base
  belongs_to :method_definition
  has_many :state_definitions, dependent: :destroy
  has_many :work_product_manifests, dependent: :delete_all
  has_one :subordinate_containments, class_name: "AlphaContainment", foreign_key: "subordinate_id", dependent: :delete
  has_many :super_containments, class_name: "AlphaContainment", foreign_key: "super_id", dependent: :delete_all

  has_many :subalpha_definitions, through: :super_containments, source: :subordinate
  has_one :parent, through: :subordinate_containments, source: :super
  has_many :work_product_definitions, through: :work_product_manifests, source: :work_product_definition

  # AlphaDefinition.where(method_definition_id: 7).select{|i| i.parent == nil}
  #
  def get_area_of_concern_name
    case self.name.downcase
    when "opportunity", "stakeholders"
      return "customer"
    when "requirements", "software system"
      return "solution"
    when "work", "team", "way of working"
      return "endeavour"
    else
      return ""
    end
  end

  def isParentOf?(alpha_definition)
    if alpha_definition.parent.present?
      self.id == alpha_definition.parent.id
    else
      false
    end
  end
end
