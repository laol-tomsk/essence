module WorkProductsHelper
  def achieved_level_of_details?(work_product, level_of_detail)
    achieved_level_of_details = work_product.achieved_level_of_details
    if achieved_level_of_details
      achieved_level_of_details.order >= level_of_detail.order
    else
      false
    end
  end

  def completed?(work_product, checkpoint_definition)
    checkpoint = WpCheckpoint.where(work_product_id: work_product.id, wp_checkpoint_definition_id: checkpoint_definition.id).first
    checkpoint.fulfilled
  end
end
