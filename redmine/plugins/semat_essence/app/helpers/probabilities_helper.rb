module ProbabilitiesHelper

  def get_node_name_for_checkpoint(alpha_name, state_number, checkbox_number)
    "#{get_alpha_code(alpha_name)}#{state_number}#{checkbox_number}"
  end

  def get_probability_for_checkpoint(probabilities, alpha_name, state_number, checkbox_number)
    if probabilities != nil
      probabilities[get_node_name_for_checkpoint(alpha_name, state_number, checkbox_number)]
    end
    0
  end

  def count_probabilities(checkboxes, project_id, nodes, iteration, previous_probabilities)

    probabilities = Hash.new

    checkboxes.each do |current_checkbox|
      unless probabilities.has_key?(current_checkbox[0])
        probabilities[current_checkbox[0]] = count_probability(current_checkbox[0], nodes, checkboxes, probabilities, project_id, iteration, previous_probabilities)
      end
    end
    probabilities
  end

  def get_checkbox_values_from_alphas(alphas)
    checkboxes = Hash.new
    alphas.each do |alpha|
      if alpha.definition.parent.nil?
        alpha.definition.state_definitions.order(order: :asc).each_with_index do |state, state_number|
          state.checkpoint_definitions.order(order: :asc).each_with_index do |checkpoint_definition, checkbox_number|
            checkpoint = Checkpoint.where(alpha_id: alpha.id, checkpoint_definition_id: checkpoint_definition.id).take
            checkboxes["#{get_alpha_code(alpha.name)}#{state_number + 1}#{checkbox_number + 1}"] = checkpoint.fulfilled
            #p "#{get_alpha_code(alpha.name)}#{state_number + 1}#{checkbox_number + 1}      #{checkpoint_definition.name}"
          end
        end
      end
    end
    checkboxes
  end

  def get_checkpoints_from_alphas(alphas)
    checkpoints = Array.new
    alphas.each do |alpha|
        alpha.definition.state_definitions.order(order: :asc).each_with_index do |state, state_number|
          state.checkpoint_definitions.order(order: :asc).each_with_index do |checkpoint_definition, checkbox_number|
            checkpoint = Checkpoint.where(alpha_id: alpha.id, checkpoint_definition_id: checkpoint_definition.id).take
            checkpoints.push(checkpoint)
          end
        end
    end
    checkpoints
  end

  def get_checkpoints_from_work_products(work_products)
    checkpoints = Array.new
    work_products.each do |work_product|
        work_product.definition.level_of_details_definitions.order(order: :asc).each do |level_of_details_def|
          level_of_details_def.wp_checkpoint_definitions.order(order: :asc).each do |wp_checkpoint_def|
            wp_checkpoint = WpCheckpoint.where(work_product_id: work_product.id, wp_checkpoint_definition_id: wp_checkpoint_def.id).take
            checkpoints.push(wp_checkpoint)
          end
        end
    end
    checkpoints
  end

  def get_checkbox_name_from_alphas(alphas)
    checkboxes_name = Hash.new
    alphas.each do |alpha|
      alpha.definition.state_definitions.order(order: :asc).each_with_index do |state, state_number|
        state.checkpoint_definitions.order(order: :asc).each_with_index do |checkpoint_definition, checkbox_number|
          checkpoint = Checkpoint.where(alpha_id: alpha.id, checkpoint_definition_id: checkpoint_definition.id).take
          if alpha.definition.parent_id == nil
            checkboxes_name[checkpoint.id] = "#{get_alpha_code(alpha.name)}#{state_number + 1}#{checkbox_number + 1}"
          else
            checkboxes_name[checkpoint.id] = ""
          end
        end
      end
    end
    checkboxes_name
  end

    def get_checkbox_def_name_from_alphas(alphas)
    checkboxes_name = Hash.new
    alphas.each do |alpha|
      alpha.definition.state_definitions.order(order: :asc).each_with_index do |state, state_number|
        state.checkpoint_definitions.order(order: :asc).each_with_index do |checkpoint_definition, checkbox_number|
          checkpoint = Checkpoint.where(alpha_id: alpha.id, checkpoint_definition_id: checkpoint_definition.id).take
          if alpha.definition.parent_id == nil
            checkboxes_name[checkpoint_definition.checkpoint_def_id] = "#{get_alpha_code(alpha.name)}#{state_number + 1}#{checkbox_number + 1}"
          else
            checkboxes_name[checkpoint_definition.checkpoint_def_id] = ""
          end
        end
      end
    end
    checkboxes_name
  end

  def count_probability(current_node_name, nodes, checkboxes, probabilities, project_id, iteration, previous_probabilities)
    #current_node - string with nodeId
    # #Определяем есть ли у чекбокса неподсчитанные предшественники
    current_node = get_node_for_checkbox(current_node_name, nodes)

    current_node_parents = get_parents(current_node.parents)

    current_node_parents.each do |parent|
      if probabilities.has_key?(parent) == false
        probabilities[parent] = count_probability(parent, nodes, checkboxes, probabilities, project_id, iteration, previous_probabilities)
      end
    end

    if is_state_node(current_node_name)
      result = count_probability_state_node(current_node_name, nodes, checkboxes, probabilities, previous_probabilities)
    else
      if iteration == 0 #&& (!is_state_node(current_node_name))
        result = count_probability_first_iteration(current_node_name, nodes, checkboxes)
      else
        result = count_probability_not_first_iteration(current_node_name, nodes, checkboxes, probabilities, previous_probabilities, project_id, iteration)
      end
    end
    result
  end

  def get_alpha_code(name)
    case name
    when "Opportunity"
      "O"
    when "Stakeholders"
      "S"
    when "Requirements"
      "R"
    when "Software System"
      "SS"
    when "Team"
      "T"
    when "Way of Working"
      "WW"
    when "Work"
      "W"
    end
  end

  def get_node_for_checkbox(checkbox_code, nodes)
    nodes[checkbox_code]
  end

  def get_parents(string)
    #get parent string and return array of strings without symbols S62S S62C S5 O5
    if string.nil?
      return nil
    end
    string.split(" ").select do |parent|
      is_without_symbols(parent)
    end
  end

  def count_probability_first_iteration(checkbox_code, nodes, checkboxes)
    #get parent with S symbol
    dynamic_parent_code = get_parents_array(checkbox_code, nodes).select do |parent|
      parent[parent.length - 1] == 'S'
    end
    dynamic_parent = get_node_for_checkbox(dynamic_parent_code[0], nodes)
    node_probabilities = dynamic_parent.probabilities
    if checkboxes[checkbox_code] == true
      node_probabilities[0]
    else
      node_probabilities[0]
    end
  end

  def count_probability_state_node(checkbox_code, nodes, checkboxes, probabilities, previous_probabilities)
    node = get_node_for_checkbox(checkbox_code, nodes)
    parents = node.parents.split(" ")

    y = 1.0
    parents.each do |parent|
      if checkboxes[parent] == true
        x = probabilities[parent]
      else
        x = probabilities[parent]
      end
      y = y * x
    end
    y
  end

  def get_parents_array(checkbox_code, nodes)
    node = get_node_for_checkbox(checkbox_code, nodes)
    node.parents.split(" ")
  end

  def is_state_node(node_id)
    if node_id.length == 2
      return true
    end
    if node_id.length == 3 && (node_id.include?("SS") || node_id.include?("WW"))
      return true
    end
    false
  end

  def count_probability_not_first_iteration(checkbox_code, nodes, checkboxes, probabilities, previous_probabilities, project_id, iteration)
    node = get_node_for_checkbox(checkbox_code, nodes)
    #p checkbox_code
    #get mediate vector
    mediate_vector = node.probabilities
    mediate_vector_codes = node.parents.split(" ")
    #p mediate_vector
    #p mediate_vector.length
    if checkboxes[checkbox_code] != nil
      if checkboxes[checkbox_code] == true
        #get first half
        mediate_vector = mediate_vector[0, mediate_vector.length / 2]
      else
        mediate_vector = mediate_vector[mediate_vector.length / 2, mediate_vector.length / 2]
      end
    end
    if mediate_vector_codes[0][mediate_vector_codes[0].length - 1] == 'C'
      mediate_vector_codes = mediate_vector_codes.drop(1)
    end
    #if checkbox_code.to_s == "O11"
    #p mediate_vector
    #p mediate_vector.length
    #end
    x = 0.0
    y = 0.0
    mediate_vector.each_with_index do |elem, index|
      if checkboxes[checkbox_code] == true
        y = elem
      elsif y = elem
      end
      bits = int_to_bit_array(index)
      #if checkbox_code.to_s == "O11"
      #p y
      #end
      mediate_vector_codes.each_with_index do |parent, parent_number|
        current_bit_number = bits.length - 1 - parent_number
        if current_bit_number < 0
          current_bit = 0
        else
          current_bit = bits[current_bit_number]
        end

        if has_s_symbol(parent)
          #if previous_probabilities
          previous = get_previous_iteration_probability_from_hash(checkbox_code, previous_probabilities)
          # if checkbox_code.to_s == "O11"
          #    p "previous="+ previous.to_s
          #end
          #else
          #  previous = get_previous_iteration_probability(checkbox_code, project_id, iteration)
          #end
          #p "previous "+previous.to_s
          if current_bit == 0
            y = y * previous
          else
            y = y * (1.0 - previous)
          end
        elsif is_without_symbols(parent)
          if current_bit == 0
            y = y * probabilities[parent]
          else
            y = y * (1.0 - probabilities[parent])
          end
          #p "parent "+parent+probabilities[parent].to_s
        end
        #p "y = " + y.to_s
      end
      x = x + y
      #      if checkbox_code.to_s == "O11"
      # p "x= "+x.to_s
      #end
      #p "x = " + x.to_s
    end

    if checkboxes[checkbox_code] == true
      x
    else
      x
      #BigDecimal("1.0") - x
    end
  end

  def int_to_bit_array(n)
    (n.to_s(2)).split("").map { |k| k.to_i }
  end

  def is_without_symbols(parent)
    parent[parent.length - 1] != 'C' && parent[parent.length - 1] != 'S'
  end

  def has_s_symbol(parent)
    parent[parent.length - 1] == 'S'
  end

  def get_previous_iteration_probability(nodeCode, project_id, current_iteration)
    prob = EssenceNodeProbability.where(["nodeId = ? and projectId = ? and iteration = ?",
                                         nodeCode, project_id, current_iteration - 1])
    prob[0].probability
  end

  def get_previous_iteration_probabilities(project_id, current_iteration)
    prob = EssenceNodeProbability.where(["projectId = ? and iteration = ?",
                                         project_id, current_iteration - 1])
    result = Hash.new
    prob.map { |node| result[node.nodeId] = node }
    result
  end

  def get_previous_iteration_probability_from_hash(nodeCode, probabilities)
    probabilities[nodeCode]
  end

  def get_last_iteration_number(project_id)
    alpha = Alpha.where(project_id: project_id).first
    checkpoint = Checkpoint.where(alpha_id: alpha.id).first
    checkpoint_state = AlphaCheckpointState.where(checkpoints_id: checkpoint.id).last
    if checkpoint_state.nil?
      -1
    else
      checkpoint_state.iteration
    end
  end

  def count_entropy_from_database(probabilities)
    sum = 0
    if probabilities.nil?
      return 0
    end
    probabilities.each do |current_node|
      if is_without_symbols(current_node.nodeId) && current_node.nodeId.count('0123456789') >= 2

        sum += current_node.probability * Math.log2(current_node.probability)

      end
    end
    sum = -sum
    sum
  end

  def count_entropy_from_hash(probabilities)
    sum = 0.0
    if probabilities.nil?
      return 0
    end
    probabilities.each do |current_node|
      if is_without_symbols(current_node[0]) && current_node[0].count('0123456789') >= 2
        if current_node[1] > 0
          sum += current_node[1] * Math.log2(current_node[1])
        end
      end
    end
    sum = -sum
    sum
  end

  def get_current_iteration_probabilities(project_id)
    EssenceNodeProbability.where(["projectId = ? and iteration = ?",
                                  project_id, get_last_iteration_number(project_id)])
  end
end