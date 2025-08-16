class Alpha < ActiveRecord::Base
  belongs_to :definition, class_name: "AlphaDefinition", foreign_key: "alpha_definition_id"
  belongs_to :project
  belongs_to :achieved_state, class_name: "StateDefinition", foreign_key: "achieved_state_id"
  has_many :checkpoints, dependent: :destroy
  belongs_to :parent, class_name: "Alpha", foreign_key: "parent_id"
  has_many :work_products

  after_create :create_containing_elements

  def subalphas
    Alpha.where(parent_id: self.id, project_id: self.project.id)
  end

  def root_alpha?
    parent.nil?
  end

  def name_with_state
    state = self.achieved_state.present? ? ": #{self.achieved_state.name}" : ""
    "#{self.name}#{state}"
  end

  def subalphas_of(alpha_definition)
    self.subalphas.select do |subalpha|
      subalpha.definition.id == alpha_definition.id
    end
  end

  def work_products_of(wp_definition)
    self.work_products.select do |wp|
      wp.definition.id == wp_definition.id
    end
  end

  def get_area_of_concern_name
    root_alpha = get_root_alpha
    root_alpha.definition.get_area_of_concern_name
  end

  private

    def create_containing_elements
      create_checkpoints
      create_work_products
      create_subalphas
    end

    def create_checkpoints
      self.definition.state_definitions.each do |state|
        state.checkpoint_definitions.each do |checkpoint|
          Checkpoint.create(fulfilled: false, alpha_id: self.id, checkpoint_definition_id: checkpoint.id)
        end
      end
    end

    def create_work_products
      self.definition.work_product_manifests.each do |manifest|
        if manifest.lower_bound > 0
          manifest.lower_bound.times do |i|
            wp_definition = manifest.work_product_definition
            WorkProduct.create(
                name: "#{wp_definition.name} #{i+1}",
                work_product_definition_id: wp_definition.id,
                project_id: self.project.id,
                alpha_id: self.id)
          end
        end
      end
    end

    def create_subalphas
      self.definition.super_containments.each do |containment|
        if containment.lower_bound > 0
          containment.lower_bound.times do |i|
            alpha_definition = containment.subordinate
            Alpha.create(
                     name: "#{alpha_definition.name} #{i+1}",
                     alpha_definition_id: alpha_definition.id,
                     project_id: self.project_id,
                     parent_id: self.id)
          end
        end
      end
    end

    def get_root_alpha
      alpha = self
      while !alpha.root_alpha? do
        alpha = alpha.parent
      end
      return alpha
    end

end
