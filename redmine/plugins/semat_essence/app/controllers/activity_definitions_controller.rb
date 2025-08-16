class ActivityDefinitionsController < ApplicationController
  before_action :find_method_definition, only: [:new, :create, :destroy]

  def new
    @activity_definition = @method_definition.activity_definitions.new
  end

  def create
    @activity_definition = @method_definition.activity_definitions.new(activity_definition_params)
    if @activity_definition.save
      flash[:notice] = "Creation successful"
      redirect_to @method_definition
    else
      render 'new'
    end
  end

  def show
    @activity_definition = ActivityDefinition.find(params[:id])
  end

  def edit
  end

  def destroy
    ActivityDefinition.find(params[:id]).destroy
    flash[:notice] = "Deletion successful"
    redirect_to @method_definition
  end

  private

    def find_method_definition
      @method_definition = MethodDefinition.find(params[:method_definition_id])
    end

    def activity_definition_params
      params.require(:activity_definition).permit(:name, :description)
    end
end
