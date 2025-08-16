class MethodCreator

  def self.initialize(project, method_definition)
    @project = project
    @method_definition = method_definition

    create_method
  end

  private

    def get_base_alphas(method_definition_id)
      AlphaDefinition.where(method_definition_id: method_definition_id).select{|i| i.parent == nil}
    end

    def create_method
      base_alphas = get_base_alphas(@method_definition.id)
      base_alphas.each do |alpha|
        create_alpha(alpha, nil, nil)
      end
    end

    def create_alpha(alpha, name_index, parent_alpha)
      if name_index.nil?
        if parent_alpha.nil?
          new_alpha = Alpha.create(name: alpha.name, project_id: @project.id, alpha_definition_id: alpha.id)
        else
          new_alpha = Alpha.create(name: alpha.name, project_id: @project.id, alpha_definition_id: alpha.id, parent_alpha: parent_alpha.id)
        end
      else
        if parent_alpha.nil?
          new_alpha = Alpha.create(name: alpha.name + " Instance #{name_index + 1}", project_id: @project.id, alpha_definition_id: alpha.id)
        else
          new_alpha = Alpha.create(name: alpha.name + " Instance #{name_index + 1}", project_id: @project.id, alpha_definition_id: alpha.id, parent_id: parent_alpha.id)
        end
      end

      alpha.work_product_manifests.each do |manifest|
        if manifest.lower_bound > 0
          manifest.lower_bound.times do |i|
            create_work_product(manifest.work_product_definition, i, new_alpha.id)
          end
        end
      end

      alpha.super_containments.each do |containment|
        if containment.lower_bound > 0
          containment.lower_bound.times do |i|
            create_alpha(containment.subordinate, i, new_alpha)
          end
        end
      end
    end

    def create_work_product(wp_definition, name_index, alpha_id)
      WorkProduct.create(name: wp_definition.name + " Instance #{name_index + 1}",
                         project_id: @project.id,
                         work_product_definition_id: wp_definition.id,
                         alpha_id: alpha_id)
    end

end