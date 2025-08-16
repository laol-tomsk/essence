# Patches to the Redmine core.
require 'project_patch'
require 'issue_patch'
require 'issues_controller_patch'

# Hooks
require_dependency 'semat_essence_issue_hook'

Redmine::Plugin.register :semat_essence do
  name 'SEMAT Essence plugin'
  author 'Daniil Tamazlykar'
  description 'This plugin implement SEMAT Essence ideas'
  version '0.0.1'

  project_module :semat_essence do
    permission :semat_essence, {
        alphas: [:index, :new, :create, :show, :settings, :edit],
        subalphas: [:index, :new, :create, :show, :settings, :edit],
        alpha_states: [:show],
        method_definitions: [:index, :new, :show],
        method_instances: [:index, :create],
        work_product_definitions: [:index, :new, :show],
        work_products: [:new, :show, :edit, :create],
        activity_definitions: [:new, :create, :show, :edit, :destroy],
        work_product_criterion_definitions: [:new, :create, :destroy],
        level_of_details_definitions: [:new, :create, :destroy],
        graph_descriptions: [:index, :new, :show],
        entropy: [:show],
        iterations: [:create, :index]
    }, :public => true
    end
  # menu :project_menu, :alphas, { :controller => 'alphas', :action => 'index' }, :caption => 'Alphas', :after => :activity, :param => :project_id
  menu :project_menu, :essence_method, { :controller => 'method_instances', :action => 'index' }, :caption => 'Essence Method', :after => :overview, :param => :project_id
  menu :project_menu, :iterations, { :controller => 'iterations', :action => 'index' }, :caption => 'Iterations', :after => :essence_method ,:param => :project_id
  menu :project_menu, :alphas, { :controller => 'alphas', :action => 'index' }, :caption => 'Alphas', :after => :iterations ,:param => :project_id
  menu :project_menu, :subalphas, { :controller => 'subalphas', :action => 'index' }, :caption => 'Subalphas', :after => :alphas ,:param => :project_id
  menu :top_menu, :methods, { :controller => 'method_definitions', :action => 'index' }, :caption => 'Method Definitions'
end
