require 'method_definition_installer'

class MethodDefinitionsController < ApplicationController
  def index
    @method_definitions = MethodDefinition.all
  end

  def new
    @method_definition = MethodDefinition.new
  end

  def create
    @method_definition = MethodDefinition.new(method_definition_params)

    if @method_definition.save
      file = params[:method_definition_file].read
      method_spec = JSON.parse(file)

      MethodDefinitionInstaller.new(@method_definition, method_spec[0])

      flash[:notice] = "Creation successful"
      redirect_to @method_definition
    else
      render 'new'
    end
  end

  def show
    @method_definition = MethodDefinition.find(params[:id])
  end

  def destroy
    MethodDefinition.find(params[:id]).destroy
    flash[:notice] = "Deletion successful"
    redirect_to method_definitions_path
  end

  private

    def method_definition_params
      params.require(:method_definition).permit(:name)
    end

end
