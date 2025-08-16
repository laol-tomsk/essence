require 'method_creator'

class MethodInstancesController < ApplicationController
  unloadable
  skip_before_action :verify_authenticity_token
  before_action :find_project, :authorize, only: [:index, :create]

  def index
    if Alphacards.where(projectid: @project.id).count == 0
      generate_cards
    end
    @cards = Alphacards.where(projectid: @project.id)
  end

  def create
    method_definition = MethodDefinition.find(params[:method_definition][:id])
    @project.method_definition_id = method_definition.id

    base_alphas = method_definition.get_base_alphas
    base_alphas.each do |alpha_definition|
      Alpha.create(name: alpha_definition.name, project_id: @project.id, alpha_definition_id: alpha_definition.id)
    end

    if @project.save
      flash[:notice] = "Creation successful"
      redirect_to method_instances_path(id: @project.id, project_id: @project.id)
    else
      flash[:notice] = "Error"
      redirect_to method_instances_path(id: @project.id, project_id: @project.id)
    end
  end

  def updateposition

    card = Alphacards.find(params[:id])
    card.updateposition(params[:id], params[:position])
    if card.save
      render :text => "ok"
    end
  end

  def updatedata
    checkpoint_name = params[:data].delete("\"") #тут лишние кавычки
    card = Alphacards.find(params[:id])
    state_definition = StateDefinition.where(id: card.stateid).first
    checkpoint_definition = state_definition.checkpoint_definitions.where(name: checkpoint_name).first
    alpha = Alpha.where(project_id: card.projectid, name: card.cardtype).first
    checkpoint = Checkpoint.where(checkpoint_definition_id: checkpoint_definition.id, alpha_id: alpha.id).take
    checkpoint.fulfilled = !checkpoint.fulfilled
    if checkpoint.save
      render :text => "ok"
    end

  end

  private

  def generate_cards
    alphas = @project.alphas
    alphas.each do |alpha|
      alpha.definition.state_definitions.order(order: :asc).each_with_index do |state|
        Alphacards.create(:cardtype => alpha.name,
                          :position => 1,
                          :stateid => state.id,
                          :projectid => @project.id)
      end
    end
  end

  def find_project
    # @project variable must be set before calling the authorize filter
    @project = Project.find(params[:project_id])
  end
end
