class MethodDefinitionInstaller

  def initialize(method_definition, spec)
    @id_mapper = {}
    @method = method_definition
    @states = {}

    create_work_products(spec)
    create_level_of_details(spec)

    create_alphas(spec)
    create_states(spec)
    create_checkpoints(spec)

    create_alpha_containments(spec)
    create_work_product_manifests(spec)

    create_activities(spec)
    create_work_product_criterions(spec)
    create_alpha_criterions(spec)
  end

  private

  def create_work_products(spec)
    puts("test workProducts")
    work_products = spec["workProducts"]
    work_products.each do |work_product|
      record = WorkProductDefinition.create(work_product_params(work_product))
      @id_mapper[work_product["id"]] = record.id
    end
  end

  def create_level_of_details(spec)
    puts("test levelsOfDetails")
    level_of_details = spec["levelOfDetails"]
    level_of_details.each do |level_of_detail|
      record = LevelOfDetailsDefinition.create(level_of_detail_params(level_of_detail))
      @id_mapper[level_of_detail["id"]] = record.id
    end
  end

  def create_alphas(spec)
    puts("test alphas")
    alphas = spec["alphas"]
    alphas.each do |alpha|
      record = AlphaDefinition.create(alpha_params(alpha))
      @id_mapper[alpha["id"]] = record.id
    end
  end

  def create_states(spec)
    puts("test states")
    states = spec["states"]
    states.each do |state|
      record = StateDefinition.create(state_params(state))
      @id_mapper[state["id"]] = record.id
      @states[state["id"]] = record.id
    end
  end

  def create_checkpoints(spec)
    puts("test checkpoints")
    checkpoints = spec["checkpoints"]
    checkpoints.each do |checkpoint|
      record = if alpha_state_definition?(checkpoint["detailId"])
                 CheckpointDefinition.create(checkpoint_params(checkpoint))
               else
                 WpCheckpointDefinition.create(wp_checkpoint_params(checkpoint))
               end
      @id_mapper[checkpoint["id"]] = record.id
    end
  end

  def create_alpha_containments(spec)
    puts("test alphaContainments")
    alpha_containments = spec["alphaContainments"]
    alpha_containments.each do |containment|
      record = AlphaContainment.create(alpha_containments_params(containment))
      @id_mapper[containment["id"]] = record.id
    end
  end

  def create_work_product_manifests(spec)
    puts("test workProductManifests")
    manifests = spec["workProductManifests"]
    manifests.each do |manifest|
      record = WorkProductManifest.create(work_product_manifest_params(manifest))
      @id_mapper[manifest["id"]] = record.id
    end
  end

  def create_activities(spec)
    puts("test activities")
    activities = spec["activities"]
    activities.each do |activity|
      record = ActivityDefinition.create(activity_params(activity))
      @id_mapper[activity["id"]] = record.id
    end
  end

  def create_work_product_criterions(spec)
    puts("test workProductCriterions")
    criterions = spec["workProductCriterions"]
    criterions.each do |criterion|
      record = WorkProductCriterionDefinition.create(work_product_criterion_params(criterion))
      @id_mapper[criterion["id"]] = record.id
    end
  end

  def create_alpha_criterions(spec)
    puts("test alphaCriterions")
    criterions = spec["alphaCriterions"]
    criterions.each do |criterion|
      if criterion["activitySpaces_id"].nil?
        record = AlphaCriterionDefinition.create(alpha_criterion_params(criterion))
        @id_mapper[criterion["id"]] = record.id
      end
    end
  end

  def work_product_params(obj)
    {
      name: obj["name"],
      description: obj["description"],
      method_definition_id: @method.id
    }
  end

  def level_of_detail_params(obj)
    {
      name: obj["name"],
      description: obj["description"],
      order: obj["order"],
      level_def_id: obj["id"],
      time_estimate: obj["timeEstimate"],
      work_product_definition_id: @id_mapper[obj["workProductId"]]
    }
  end

  def alpha_params(obj)
    {
      name: obj["name"],
      description: obj["description"],
      parent_id: obj["parentAlphaId"],
      method_definition_id: @method.id
    }
  end

  def state_params(obj)
    {
      name: obj["name"],
      description: obj["description"],
      order: obj["order"],
      state_def_id: obj["id"],
      time_estimate: obj["timeEstimate"],
      alpha_definition_id: @id_mapper[get_alpha_id(obj)],
    }
  end

  def checkpoint_params(obj)
    {
      name: obj["name"],
      description: obj["description"],
      order: obj["order"],
      checkpoint_def_id: obj["id"],
      time_estimate: obj["timeEstimate"],
      state_definition_id: @id_mapper[obj["detailId"]],
    }
  end

  def wp_checkpoint_params(obj)
    {
      name: obj["name"],
      description: obj["description"],
      order: obj["order"],
      checkpoint_def_id: obj["id"],
      level_of_details_definition_id: @id_mapper[obj["detailId"]],
    }
  end

  def alpha_containments_params(obj)
    {
      lower_bound: obj["lowerBound"],
      upper_bound: obj["upperBound"],
      subordinate_id: @id_mapper[obj["subAlphaId"]],
      super_id: @id_mapper[obj["supAlphaId"]]
    }
  end

  def work_product_manifest_params(obj)
    {
      lower_bound: obj["lowerBound"],
      upper_bound: obj["upperBound"],
      alpha_definition_id: @id_mapper[get_alpha_id(obj)],
      work_product_definition_id: @id_mapper[obj["workProductId"]]
    }
  end

  def activity_params(obj)
    {
      name: obj["name"],
      description: obj["description"],
      method_definition_id: @method.id
    }
  end

  def work_product_criterion_params(obj)
    {
      criterion_type: get_criterion_type(obj["criterionTypeEnumValue"]),
      partial: obj["partial"],
      minimal: obj["minimal"],
      activity_definition_id: @id_mapper[obj["activityId"]],
      level_of_details_definition_id: @id_mapper[obj["levelOfDetailId"]]
    }
  end

  def alpha_criterion_params(obj)
    {
      criterion_type: get_criterion_type(obj["criterionTypeEnumValue"]),
      partial: obj["partial"],
      minimal: obj["minimal"],
      activity_definition_id: @id_mapper[obj["activityId"]],
      state_definition_id: @id_mapper[obj["stateId"]]
    }
  end

  def get_criterion_type(x)

    criterion_type = if x == 0
                       :entry
                     else
                       :completion
                     end

    criterion_type
  end

    # Some objects contains either alphas_id or subAlphas_id. We do not need this separation
  def get_alpha_id(obj)
    alpha_id = obj["alphaId"]
    if alpha_id.nil?
      alpha_id = obj["subAlphaId"]
    end
    alpha_id
  end

  def true?(str)
    str == 'true'
  end

  def alpha_state_definition?(id)
    !@states[id].nil?
  end
end