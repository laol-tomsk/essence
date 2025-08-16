class AlphaDefinitionsController < ApplicationController

  def show
    @alpha_definition = AlphaDefinition.find(params[:id])
  end
end
