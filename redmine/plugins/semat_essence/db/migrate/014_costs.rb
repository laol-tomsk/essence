class Costs < ActiveRecord::Migration[5.2]
  def change
    add_column :level_of_details_definitions, :time_estimate, :integer
    add_column :checkpoint_definitions, :time_estimate, :integer
    add_column :state_definitions, :time_estimate, :integer
  end
end
