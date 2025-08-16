# frozen_string_literal: true

class SaveCheckpoints < ActiveRecord::Migration[5.2]
  def change
    create_table :alpha_checkpoint_states do |t|
      t.integer :iteration
      t.boolean :checkbox_state
    end

    create_table :wp_checkpoint_states do |t|
      t.integer :iteration
      t.boolean :checkbox_state
    end

    add_column :alpha_definitions, :parent_id, :string
    add_column :level_of_details_definitions, :level_def_id, :string
    add_column :state_definitions, :state_def_id, :string
    add_column :checkpoint_definitions, :checkpoint_def_id, :string
    add_column :wp_checkpoint_definitions, :checkpoint_def_id, :string

    add_reference :wp_checkpoint_definitions, :level_of_details_definition, index: { name: 'index_wp_cp_def_on_lod_def_id' }, foreign_key: true
    add_reference :wp_checkpoints, :wp_checkpoint_definition, index: true, foreign_key: true
    add_reference :wp_checkpoints, :work_products, index: true, foreign_key: true
    rename_column :wp_checkpoints, :work_products_id, :work_product_id
  end
end
