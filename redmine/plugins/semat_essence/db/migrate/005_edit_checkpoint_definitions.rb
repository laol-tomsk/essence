class EditCheckpointDefinitions < ActiveRecord::Migration[5.2]
  def change
    add_column :checkpoint_definitions, :order, :integer
  end
end