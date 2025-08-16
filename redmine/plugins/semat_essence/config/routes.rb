# Plugin's routes
# See: http://guides.rubyonrails.org/routing.html

resources :alpha_definitions
resources :method_definitions
resources :work_product_definitions
resources :level_of_details_definitions
resources :activity_definitions
resources :work_product_criterion_definitions
resources :iterations
get 'add_criterions_to_issue_form' => 'issues#add_criterions_to_issue_form'
get 'get_completion_criterions' => 'issues#get_completion_criterions'

resources :method_instances
resources :alphas do
  collection do
    get 'update_checkpoint'
  end
end
resources :subalphas do
  collection do
    get 'update_checkpoint'
  end
end
resources :work_products do
  member do
    post 'toggle_state'
    post 'toggle_checkpoint'
    post 'update_level'
    post 'update_checkpoint'
  end
end
post 'run_essence_algorithm' => 'iterations#count_probabilities_for_iteration'
post 'select_next_checkbox' => 'iterations#select_next_checkbox'
post 'post/method_instances/updateposition', :to => 'method_instances#updateposition'
post 'post/method_instances/updatedata', :to => 'method_instances#updatedata'