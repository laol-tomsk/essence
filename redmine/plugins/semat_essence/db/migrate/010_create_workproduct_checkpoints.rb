class CreateWorkproductCheckpoints < ActiveRecord::Migration[5.2]

  def change

    create_table :wp_checkpoint_definitions do |t|
      t.string :name
      t.text :description
      t.integer :order
    end

    create_table :wp_checkpoints do |t|
      t.boolean :fulfilled
    end

  end
end
