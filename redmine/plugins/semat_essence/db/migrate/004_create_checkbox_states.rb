class CreateCheckboxStates < ActiveRecord::Migration[5.2]
  def change

    create_table :checkbox_states do |t|
      t.string :nodeId
      t.integer :iteration
      t.integer :projectId
      t.boolean :checkbox_state

      t.timestamps null: false
    end
  end
end
