class WorkProductCriterionDefinitionsController < ApplicationController
  before_action :find_activity_definition, only: [:new, :create]

  def new
    @wp_criterion_definition = @activity_definition.work_product_criterion_definitions.new
    @work_product_definitions = @activity_definition.method_definition.work_product_definitions
  end

  def create
    @wp_criterion_definition = @activity_definition.work_product_criterion_definitions.new(wp_criterion_definition_params)
    if @wp_criterion_definition.save
      flash[:notice] = "Creation successful"
      redirect_to @activity_definition
    else
      render 'new'
    end
  end

  private

    def find_activity_definition
      @activity_definition = ActivityDefinition.find(params[:activity_definition_id])
    end

    def wp_criterion_definition_params
      params.require(:work_product_criterion_definition).permit(:criterion_type, :level_of_details_definition_id)
    end

end
