class WorkProduct < ActiveRecord::Base
  belongs_to :definition, class_name: "WorkProductDefinition", foreign_key: "work_product_definition_id"
  belongs_to :project
  belongs_to :achieved_level_of_details, class_name: "LevelOfDetailsDefinition", foreign_key: "level_of_details_definition_id"
  belongs_to :containing_alpha, class_name: "Alpha", foreign_key: "alpha_id"

  after_create :create_containing_elements

  def level_of_details
    self.definition.level_of_details_definitions.order(order: :asc)
  end

  def name_with_achieved_level
    achieved_level_name = achieved_level_of_details.present? ? achieved_level_of_details.name : "none"
    "#{name}: #{achieved_level_name}"
  end

  private

  def create_containing_elements
    create_checkpoints
  end

  def create_checkpoints
    self.definition.level_of_details_definitions.each do |level_of_details_def|
      level_of_details_def.wp_checkpoint_definitions.each do |checkpoint_def|
        WpCheckpoint.create(fulfilled: false, work_product_id: self.id, wp_checkpoint_definition_id: checkpoint_def.id)
      end
    end
  end

end
