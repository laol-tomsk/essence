class AlphasController < ApplicationController
  before_action :find_project, :authorize, only: [:index, :settings, :create_base_alphas, :show, :new, :create, :edit]
  before_action :find_alpha, only: [:show, :edit, :update, :destroy]

  def index
    @alphas = Alpha.where(project_id: @project.id).to_a
    @alphas = @alphas.select do |alpha|
      alpha.definition.parent_id.nil?
    end
  end

  def settings
  end

  def show
  end

  def edit
  end

  def update
    @alpha.update(params.require(:alpha).permit(:name, :link))
    flash[:notice] = "Update successful"
    redirect_to alpha_path(@alpha, project_id: @alpha.project_id)
  end

  def destroy
    # we can destroy only subalphas so parent alpha should be exist
    parent_alpha = @alpha.parent
    @alpha.destroy
    flash[:notice] = "Deletion successful"
    redirect_to alpha_path(parent_alpha, project_id: parent_alpha.project_id)
  end

  def create_base_alphas
    definitions = import_definitions(params[:definitions])
    create_alphas(definitions, @project.id)

    flash[:notice] = "Alphas was created"

    redirect_to action: 'settings', project_id: @project.id
  end

  def update_checkpoint
    checkpoint = Checkpoint.find(params[:checkpoint_id])
    checkpoint.update_attribute :fulfilled, params[:checkpoint_new_state]
    update_achieved_state(checkpoint.alpha_id)
    @alpha = checkpoint.alpha

    respond_to do |format|
      format.html {redirect_to @alpha}
      format.js
    end
  end

  private

  def find_project
    # @project variable must be set before calling the authorize filter
      @project = Project.find(params[:project_id])
  end

  def find_alpha
    @alpha = Alpha.find(params[:id])
  end

  def alpha_create_params
    params.require(:alpha).permit(:name, :link, :alpha_definition_id, :parent_id)
  end

  def import_definitions(url)
    uri = URI(url)
    response = Net::HTTP.get(uri)
    JSON.parse(response)
  end

  def create_alphas(definitions, project_id)
    alphas = definitions.fetch("alphas")
    states = definitions.fetch("states")
    checkpoints = definitions.fetch("checkpoints")

    alphas.each do |alpha|
      created_alpha = SeAlpha.create(name: alpha.fetch("name"), description: alpha.fetch("description"), project_id: project_id)

      states.each do |state|
        if state.fetch("alpha_id") == alpha.fetch("id")
          created_state = created_alpha.se_states.create(name: state.fetch("name"), description: state.fetch("description"), order: state.fetch("order"))
        end

        checkpoints.each do |checkpoint|
          if checkpoint.fetch("state_id") == state.fetch("id")
            created_state.se_checkpoints.create(name: checkpoint.fetch("name"), description: checkpoint.fetch("description"))
          end
        end
      end

    end

  end

  def update_achieved_state(alpha_id)
    alpha = Alpha.find(alpha_id)
    state_definitions = alpha.definition.state_definitions.order(order: :asc)

    achieved_state = nil

    state_definitions.each do |state|
      if state.achieved?(alpha)
        achieved_state = state
      else
        break
      end
    end

    alpha.update_attribute(:achieved_state, achieved_state)
  end

end
