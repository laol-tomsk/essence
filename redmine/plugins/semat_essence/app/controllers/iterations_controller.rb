this_dir = File.expand_path(File.dirname(__FILE__))
lib_dir = File.join(this_dir, 'lib')
$LOAD_PATH.unshift(lib_dir) unless $LOAD_PATH.include?(lib_dir)

require 'config.rb'
require 'json/ext'
require 'json'

class IterationsController < ApplicationController

  include ProbabilitiesHelper
  helper ProbabilitiesHelper

  def index
    @project = Project.find(params[:project_id])
    @probabilities_history = params[:prob_history]
    @iterations_count = get_last_iteration_number(@project.id)
    @checkpoint_codes = get_checkbox_name_from_alphas(@project.alphas)

    load_params

    @probabilities = get_probabilities_for(@iterations_count, 1,2,5, 0.5)
    @checkpoints = request_checkpoints_list
  end

  def create
    # save alphas checkpoints
    @project = Project.find(params[:project_id])
    alphas = @project.alphas
    @checkpoints = get_checkpoints_from_alphas(alphas)
    current_iteration = get_last_iteration_number(@project.id) + 1
    @checkpoints.each do |checkpoint|
      AlphaCheckpointState.create({ iteration: current_iteration, checkbox_state: checkpoint.fulfilled,
                                     checkpoints_id: checkpoint.id })
    end
    #save work_products_checkpoints
    work_products = @project.work_products
    @checkpoints = get_checkpoints_from_work_products(work_products)
    @checkpoints.each do |checkpoint|
      WpCheckpointState.create({ iteration: current_iteration, checkbox_state: checkpoint.fulfilled,
                                 wp_checkpoints_id: checkpoint.id })
    end

    flash[:notice] = "Checkboxes states were saved successfully. Current iteration number for #{@project.name} is #{current_iteration}."
    redirect_to action: "index", project_id: @project.id
  end

  def new
    puts "New?"
    p params[:graph_description]
  end

  def load_params
    @project = Project.find(params[:project_id])
    @api_key = @project.essence_api_key
    @api_key = Config::Api_default_key if @api_key.nil?

    @iterations_count = get_last_iteration_number(@project.id)
    @iteration_to_show = params[:selected_iteration]
    if @iteration_to_show == nil
      @iteration_to_show = @iterations_count
    else
      @iteration_to_show = @iteration_to_show.to_i
    end

    @cards = Alphacards.where(projectid: @project.id)
  end

  def count_probabilities_for_iteration

    load_params

    @core_history = get_core_history
    @checkbox_history = get_subalphas_history

    puts "requesting probabilities for iteration from count_probabilities_for_iteration, passing :get_next_checkbox"

    @probabilities = request_probabilities_for(@iteration_to_show, params[:selected_influence_weak].to_i,
                                                params[:selected_influence_medium].to_i, params[:selected_influence_strong].to_i,
                                                params[:selected_threshold].to_f, params[:get_next_checkbox].to_i, params[:sprint_length].to_i)

    if @probabilities == 0
      return 0
    end

    @alphas = Alpha.where(project_id: @project.id)
    @alphas = @alphas.reject do |alpha|
      alpha.definition.parent_id.nil?
    end

    @work_products = WorkProduct.where(project_id: @project.id)

    ## куча кода была здесь
    return @core_history
  end

  def request_probabilities_for(iteration, w,m,s,threshold, get_next_checkpoint, sprint_length)
    if get_next_checkpoint > 0
      if File.exists?("/usr/src/redmine/plugins/semat_essence/results.json")
        resP = JSON.parse(File.read("/usr/src/redmine/plugins/semat_essence/results.json"))
        return resP
      end
    end

    checkpoint_codes = get_checkbox_def_name_from_alphas(@project.alphas)
    
    core_costs = @project.method_definition
                    .alpha_definitions
                    .select{|i| i.parent == nil}
                    .flat_map { |a| a.state_definitions }
                    .flat_map { |s| s.checkpoint_definitions }
                    .reject{ |cp| cp.time_estimate == nil}
                    .map { |cp| [checkpoint_codes[cp.checkpoint_def_id], cp.time_estimate] }

    state_costs = @project.method_definition
                    .alpha_definitions
                    .reject{|i| i.parent == nil}
                    .flat_map { |a| a.state_definitions }
                    .reject{ |s| s.time_estimate == nil}
                    .map { |s| [s.state_def_id, s.time_estimate] }

    level_costs = @project.method_definition
                .work_product_definitions
                .flat_map { |wp| wp.level_of_details_definitions }
                .reject{ |ld| ld.time_estimate == nil}
                .map { |ld| [ld.level_def_id, ld.time_estimate] }

    api_key = params[:api_key]

    @toSend = {
      "Data_dict" => @core_history,
      "Data_add_dict"=>@checkbox_history,
      "Costs" => core_costs.to_h.merge(state_costs.to_h).merge(level_costs.to_h),
      "Weight" => {"S"=>s,"M"=>m,"W"=>w},
      "Method_id"=>@project.method_definition.id,
      "Iter"=>iteration,
      "IterLength"=>sprint_length,
      "Algorithm"=>get_next_checkpoint-2,
      "Threshold"=>threshold}.to_json

    uri = URI(Config::URL)
    http = Net::HTTP.new(uri.host,Config::PORT)
    http.use_ssl = false
    if get_next_checkpoint == 0
      req = Net::HTTP::Post.new("/calculate", initheader = {'Content-Type' =>'application/json'})
    end
    if get_next_checkpoint == 1
      req = Net::HTTP::Get.new("/select_next", initheader = {'Content-Type' =>'application/json'})
    end
    if get_next_checkpoint >= 2
      req = Net::HTTP::Get.new("/plan_iteration", initheader = {'Content-Type' =>'application/json'})
    end
    req.body = "#{@toSend}"

    if get_next_checkpoint > 0
      http.request(req)
      return 0
    end

    if get_next_checkpoint == 0
      res = http.request(req)
      resP = JSON.parse(res.body)
      puts resP
      probabilities = JSON.parse(resP["res"])
      @project.update(essence_api_key: api_key)
      probabilities
    end
  end
  
  def request_checkpoints_list
    @toSend = {
      "Method_id"=>@project.method_definition.id
    }.to_json

    puts @toSend
    uri = URI(Config::URL)
    http = Net::HTTP.new(uri.host,Config::PORT)
    http.use_ssl = false
    req = Net::HTTP::Get.new("/list_checkpoints", initheader = {'Content-Type' =>'application/json'})
    req.body = "#{@toSend}"
    res = http.request(req)
    JSON.parse(res.body)
  end

  def get_probabilities_from_db(iteration)
    dict = {}
    EssenceNodeProbability.where(["projectId = ? and iteration = ?", @project.id, iteration]).each do |probability|
      dict[probability.nodeId] = probability.probability
    end
    if dict.empty?
      nil
    else
      dict
    end
  end

  def get_core_history
    # get checkpoints for Core Alphas
    core_history = {}
    core_alphas = Alpha.where(project_id: @project.id).to_a
    core_alphas = core_alphas.select do |alpha|
      alpha.definition.parent_id.nil?
    end
    core_checkboxes = get_checkpoints_from_alphas(core_alphas)
    core_checkboxes.each do |checkbox|
      history = AlphaCheckpointState.where(checkpoints_id: checkbox.id).order(:iteration)
      history_array = []
      history.each do |checkbox_state|
        if checkbox_state.iteration <= @iterations_count
          history_array << checkbox_state.checkbox_state
        end
      end
      core_history[checkbox.definition.checkpoint_def_id] = history_array
    end
    return core_history
  end

  def get_probabilities_for(iteration, w, m, s, threshold)

    @core_history = get_core_history
    @checkbox_history = get_subalphas_history

    probabilities = {}

    if w == 1 && m == 2 && s == 5
      probabilities = get_probabilities_from_db(iteration)
    end
    if probabilities == nil || probabilities.empty?
      puts "requesting probabilities for iteration from get_probabilities_for, passing 0"
      probabilities = request_probabilities_for(iteration, w,m,s,threshold, 0, 0)
      save_probabilities_to_db(probabilities, iteration)
    end
    probabilities
  end

  def save_probabilities_to_db(probabilities, iteration)
    probabilities.each do |probability|
      prob = EssenceNodeProbability.find_or_initialize_by(:nodeId => probability[0],
                                    :iteration => iteration,
                                    :projectId => @project.id)
      prob.probability = probability[1]
      prob.save
    end
  end

  def get_subalphas_history

    subalphas = Alpha.where(project_id: @project.id)
    subalphas = subalphas.reject do |alpha|
      alpha.definition.parent_id.nil?
    end

    checkbox_history = {}

    subalphas.each do |alpha|
      subalpha_states = {}
      alpha.definition.state_definitions.each do |state_def|
        subalpha_states[state_def.state_def_id] = {}
      end

      subalpha_checkboxes = get_checkpoints_from_alphas([alpha])
      subalpha_checkboxes.each do |checkbox|
        history = AlphaCheckpointState.where(checkpoints_id: checkbox.id).order(:iteration)
        history_array = []
        history.each do |checkbox_state|
          if checkbox_state.iteration <= @iterations_count
            history_array << {checkbox_state.iteration => checkbox_state.checkbox_state}
          end
        end
        subalpha_states[checkbox.definition.state_definition.state_def_id][checkbox.definition.checkpoint_def_id] = history_array
      end

      subalpha_states.each do |state|
        if checkbox_history[state[0]] == nil
          checkbox_history[state[0]] = []
        end
        checkbox_history[state[0]] << state[1]
      end
    end

    #get all checkpoints for work products

    @project.work_products.each do |work_product|
      wp_levels = {}
      work_product.definition.level_of_details_definitions.each do |level_def|
        wp_levels[level_def.level_def_id] = {}
      end

      wp_checkboxes = get_checkpoints_from_work_products([work_product])
      wp_checkboxes.each do |checkbox|
        history = WpCheckpointState.where(wp_checkpoints_id: checkbox.id).order(:iteration)
        history_array = []
        history.each do |checkbox_state|
          if checkbox_state.iteration <= @iterations_count
            history_array << {checkbox_state.iteration => checkbox_state.checkbox_state}
          end
        end
        wp_levels[checkbox.definition.level_of_details_definition.level_def_id][checkbox.definition.checkpoint_def_id] = history_array
      end

      wp_levels.each do |level|
        if checkbox_history[level[0]] == nil
          checkbox_history[level[0]] = []
        end
        checkbox_history[level[0]] << level[1]
      end
    end
    return checkbox_history
  end
end