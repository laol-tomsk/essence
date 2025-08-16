module MethodInstancesHelper

  def get_base_alphas(project)
    project.alphas.select{ |alpha| alpha.definition.parent == nil }
  end
end
