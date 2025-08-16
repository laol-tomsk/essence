class WorkProductCriterionDefinition < ActiveRecord::Base
  belongs_to :activity_definition
  belongs_to :level_of_details_definition
  enum criterion_type: { entry: 0, completion: 1 }

  def get_wp_definition
    self.level_of_details_definition.work_product_definition
  end

  def name_with_level
    lod_name = level_of_details_definition.name
    wp_name = level_of_details_definition.work_product_definition.name
    "#{wp_name}: #{lod_name}"
  end

  def get_available_wp(project_id)
    project = Project.find(project_id)
    required_wp_id = level_of_details_definition.work_product_definition.id
    required_level_of_detail = self.level_of_details_definition.order

    project.work_products.select do |wp|
      if self.entry?
        wp.definition.id == required_wp_id && wp.achieved_level_of_details.present? && wp.achieved_level_of_details.order >= required_level_of_detail
      else
        wp.definition.id == required_wp_id
      end
    end
  end

end
