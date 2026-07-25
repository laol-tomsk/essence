class TaskNames < ActiveRecord::Migration[5.2]
  def change
    add_column :level_of_details_definitions, :task_name, :string
    add_column :checkpoint_definitions, :task_name, :string
    add_column :state_definitions, :task_name, :string
    add_column :wp_checkpoint_definitions, :task_name, :string
  end
end
