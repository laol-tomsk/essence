class WorkProductsController < ApplicationController
  before_action :find_project, :authorize, only: [:new, :create, :show, :edit]
  skip_before_action :verify_authenticity_token

  def new
    @alpha = Alpha.find(params[:alpha_id])
    @work_product_definitions = @alpha.definition.work_product_definitions
    @work_product = @project.work_products.new
  end

  def create
    @work_product = @project.work_products.new(work_product_params)
    if @work_product.save
      flash[:notice] = "Creation successful"
      redirect_to subalpha_path(id: params[:work_product][:alpha_id], project_id: @project.id)
    else
      render 'new'
    end
  end

  def show
    @work_product = WorkProduct.find(params[:id])
  end

  def edit
    @work_product = WorkProduct.find(params[:id])
  end

  def update
    wp = WorkProduct.find(params[:id])
    alpha = wp.containing_alpha
    wp.update(params.require(:work_product).permit(:name, :link))
    flash[:notice] = "Update successful"
    redirect_to work_product_path(wp, project_id: alpha.project_id)
  end

  def destroy
    wp = WorkProduct.find(params[:id])
    alpha = wp.containing_alpha
    wp.destroy
    flash[:notice] = "Deletion successful"
    redirect_to alpha_path(id: alpha.id, project_id: alpha.project_id)
  end

  def toggle_state
    @work_product = WorkProduct.find(params[:id])
    level_of_detail = LevelOfDetailsDefinition.find(params[:level_of_details_id])
    if params[:checked]
      @work_product.achieved_level_of_details = level_of_detail
    else
      index = @work_product.level_of_details.index { |lod| lod.id == level_of_detail.id }
      @ins = @work_product
      if index == 0
        @work_product.achieved_level_of_details = nil
      else
        @work_product.achieved_level_of_details = @work_product.level_of_details[index - 1]
      end
    end
    @work_product.save

    respond_to do |format|
      format.html { redirect_to @work_product }
      format.js
    end
  end

  def toggle_checkpoint
    @work_product = WorkProduct.find(params[:id])
    level_of_detail = LevelOfDetailsDefinition.find(params[:level_of_details_id])
    if params[:checked]
      @work_product.achieved_level_of_details = level_of_detail
    else
      index = @work_product.level_of_details.index { |lod| lod.id == level_of_detail.id }
      @ins = @work_product
      if index == 0
        @work_product.achieved_level_of_details = nil
      else
        @work_product.achieved_level_of_details = @work_product.level_of_details[index - 1]
      end
    end
    @work_product.save

    respond_to do |format|
      format.html { redirect_to @work_product }
      format.js
    end
  end

  def update_level
    @work_product = WorkProduct.find(params[:work_product_id])
    level_of_detail = LevelOfDetailsDefinition.find(params[:level_id])
    if @work_product.achieved_level_of_details != level_of_detail
      @work_product.achieved_level_of_details = level_of_detail
    else
      index = @work_product.level_of_details.index { |lod| lod.id == level_of_detail.id }
      @ins = @work_product
      if index == 0
        @work_product.achieved_level_of_details = nil
      else
        @work_product.achieved_level_of_details = @work_product.level_of_details[index - 1]
      end
    end
    @work_product.save

    respond_to do |format|
      format.html { redirect_to work_product_path(@work_product, project_id: @work_product.project.id) }
      format.js
    end
  end

  def update_checkpoint
    @work_product = WorkProduct.find(params[:work_product_id])
    checkpoint = WpCheckpoint.where(wp_checkpoint_definition_id: params[:checkpoint_id], work_product_id: @work_product.id).first
    checkpoint.fulfilled = !checkpoint.fulfilled
    checkpoint.save

    respond_to do |format|
      format.html { redirect_to work_product_path(@work_product, project_id: @work_product.project.id) }
      format.js
    end
  end

  private

    def find_project
      @project = Project.find(params[:project_id])
    end

    def work_product_params
      params.require(:work_product).permit(:name, :link, :work_product_definition_id, :alpha_id)
    end

end
