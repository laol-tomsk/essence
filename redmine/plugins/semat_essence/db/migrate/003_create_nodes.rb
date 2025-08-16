class CreateNodes < ActiveRecord::Migration[5.2]
  def change
    create_table :essence_graph_nodes do |t|
      t.string :nodeId
      t.string :firstState
      t.string :secondState
      t.string :parents, limit: 1000

      t.timestamps null: false
    end

    create_table :essence_node_probabilities do |t|
      t.string :nodeId
      t.integer :iteration
      t.integer :projectId
      t.float :probability

      t.timestamps null: false
    end
  end
end
