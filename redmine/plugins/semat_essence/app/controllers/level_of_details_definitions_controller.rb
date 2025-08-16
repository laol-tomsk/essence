class LevelOfDetailsDefinitionsController < ApplicationController
  before_action :find_work_product_definition, only: [:new, :create, :destroy]

  def new
    @lod_definition = @work_product_definition.level_of_details_definitions.new
  end

  def create
    @lod_definition = @work_product_definition.level_of_details_definitions.new(lod_definition_params)
    if @lod_definition.save
      flash[:notice] = "Creation successful"
      redirect_to @work_product_definition
    else
      render 'new'
    end
  end

  def destroy
    LevelOfDetailsDefinition.find(params[:id]).destroy
    flash[:notice] = "Deletion successful"
    redirect_to @work_product_definition
  end

  private

    def find_work_product_definition
      @work_product_definition = WorkProductDefinition.find(params[:work_product_definition_id])
    end

    def lod_definition_params
      params.require(:level_of_details_definition).permit(:name, :description, :order)
    end

end
