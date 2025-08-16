class WorkProductDefinitionsController < ApplicationController
  before_action :find_method_definition, only: [:new, :destroy]

  def index
  end

  def new
    @work_product_definition = @method_definition.work_product_definitions.new
  end

  def create
    @work_product_definition = WorkProductDefinition.new(work_product_definition_params)
    if @work_product_definition.save
      flash[:notice] = "Creation successful"
      redirect_to method_definition_path(work_product_definition_params[:method_definition_id])
    else
      render 'new'
    end
  end

  def show
    @work_product_definition = WorkProductDefinition.find(params[:id])
  end

  def destroy
    WorkProductDefinition.find(params[:id]).destroy
    flash[:notice] = "Deletion successful"
    redirect_to @method_definition
  end

  private

    def find_method_definition
      @method_definition = MethodDefinition.find(params[:method_definition_id])
    end

    def work_product_definition_params
      params.require(:work_product_definition).permit(:name, :description, :method_definition_id)
    end
end
