class ActivityDefinition < ActiveRecord::Base
  belongs_to :method_definition
  has_many :work_product_criterion_definitions, dependent: :destroy
  has_many :alpha_criterion_definitions, dependent: :destroy

  def wp_entry_criterion_definition
    self.work_product_criterion_definitions.select do |criterion|
      criterion.entry?
    end
  end

  def wp_completion_criterion_definition
    self.work_product_criterion_definitions.select do |criterion|
      criterion.completion?
    end
  end

  def alpha_entry_criterion_definition
    self.alpha_criterion_definitions.select do |criterion|
      criterion.entry?
    end
  end

  def alpha_completion_criterion_definition
    self.alpha_criterion_definitions.select do |criterion|
      criterion.completion?
    end
  end

  def entry_criterions
    alpha_entry_criterions = alpha_entry_criterion_definition
    alpha_dependency_map = Array.new(alpha_entry_criterions.length) { Array.new(alpha_entry_criterions.length) { 0 } }
    for i in 0..alpha_entry_criterions.length do
      for j in 0..alpha_entry_criterions.length do
        alpha_dependency_map[i][j] = alpha_entry_criterions[i] == alpha_entry_criterions[j].parent
      end
    end
    alpha_dependency_map
  end

end
