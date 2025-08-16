class TableReferences < ActiveRecord::Migration[5.2]
  def change

    # Redmine Patches
    add_reference :projects, :method_definition, index: true, foreign_key: true
    add_reference :issues, :activity_definition, index: true, foreign_key: true

    # ***********
    # Definitions
    # ***********

    # Method Definition

    # Alpha Definition

    add_reference :alpha_definitions, :method_definition, index: true, foreign_key: true

    add_reference :state_definitions, :alpha_definition, index: true, foreign_key: true

    add_reference :checkpoint_definitions, :state_definition, index: true, foreign_key: true

    add_column :alpha_containments, :subordinate_id, :bigint
    add_column :alpha_containments, :super_id, :bigint
    add_index :alpha_containments, :subordinate_id
    add_index :alpha_containments, :super_id
    add_foreign_key :alpha_containments, :alpha_definitions, column: :subordinate_id
    add_foreign_key :alpha_containments, :alpha_definitions, column: :super_id

    # Work Product Definition

    add_reference :work_product_definitions, :method_definition, index: true, foreign_key: true

    add_reference :level_of_details_definitions, :work_product_definition, index: true, foreign_key: true

    add_reference :work_product_manifests, :alpha_definition, index: true, foreign_key: true
    add_reference :work_product_manifests, :work_product_definition, index: true, foreign_key: true

    # Activity Definition

    add_reference :activity_definitions, :method_definition, index: true, foreign_key: true

    add_reference :alpha_criterion_definitions, :activity_definition, index: true, foreign_key: true
    add_reference :alpha_criterion_definitions, :state_definition, index: true, foreign_key: true

    add_reference :work_product_criterion_definitions, :activity_definition, index: { name: 'index_wp_criterion_definitions_on_activity_definition_id'}, foreign_key: true
    add_reference :work_product_criterion_definitions, :level_of_details_definition, index: { name: 'index_wp_criterion_definitions_on_level_of_details_definition_id' }, foreign_key: true

    # *********
    # Instances
    # *********

    # Alpha
    change_column :projects, :id, :bigint, null: false, unique: true, auto_increment: true
    add_reference :alphas, :projects, index: true, foreign_key: true
    add_reference :alphas, :alpha_definition, index: true, foreign_key: true
    add_column :alphas, :achieved_state_id, :bigint
    add_index :alphas, :achieved_state_id
    add_foreign_key :alphas, :state_definitions, column: :achieved_state_id
    add_column :alphas, :parent_id, :bigint
    add_index :alphas, :parent_id
    add_foreign_key :alphas, :alphas, column: :parent_id
    rename_column :alphas, :projects_id, :project_id

    add_reference :checkpoints, :alphas, index: true, foreign_key: true
    add_reference :checkpoints, :checkpoint_definition, index: true, foreign_key: true
    rename_column :checkpoints, :alphas_id, :alpha_id
    #  Work Product
    add_reference :work_products, :projects, index: true, foreign_key: true
    add_reference :work_products, :work_product_definition, index: true, foreign_key: true
    add_reference :work_products, :level_of_details_definition, index: true, foreign_key: true
    add_reference :work_products, :alphas, index: true, foreign_key: true
    rename_column :work_products, :alphas_id, :alpha_id
    rename_column :work_products, :projects_id, :project_id

    # Activity
    change_column :issues, :id, :bigint, null: false, unique: true, auto_increment: true
    add_reference :work_product_criterions, :issues, index: true, foreign_key: true
    add_reference :work_product_criterions, :work_product, index: true, foreign_key: true
    add_reference :work_product_criterions, :work_product_criterion_definition, index: { name: 'index_wp_criterions_on_wp_criterion_definition_id' }, foreign_key: true
    rename_column :work_product_criterions, :issues_id, :issue_id

    add_reference :alpha_criterions, :issues, index: true, foreign_key: true
    add_reference :alpha_criterions, :alphas, index: true, foreign_key: true
    add_reference :alpha_criterions, :alpha_criterion_definition, index: { name: 'index_alpha_criterions_on_alpha_criterion_definition_id' }, foreign_key: true
    rename_column :alpha_criterions, :issues_id, :issue_id
    rename_column :alpha_criterions, :alphas_id, :alpha_id

  end
end
