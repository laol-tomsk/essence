class TableCreations < ActiveRecord::Migration[5.2]
  def change

    # ***********
    # Definitions
    # ***********

    # Method Definition

    create_table :method_definitions do |t|
      t.string :name
    end

    # Alpha Definition

    create_table :alpha_definitions do |t|
      t.string :name
      t.text :description
    end

    create_table :state_definitions do |t|
      t.string :name
      t.text :description
      t.integer :order
    end

    create_table :checkpoint_definitions do |t|
      t.string :name
      t.text :description
    end

    create_table :alpha_containments do |t|
      t.integer :lower_bound
      t.integer :upper_bound
    end

    # Work Product Definition

    create_table :work_product_definitions do |t|
      t.string :name
      t.text :description
    end

    create_table :level_of_details_definitions do |t|
      t.string :name
      t.text :description
      t.integer :order
    end

    create_table :work_product_manifests do |t|
      t.integer :lower_bound
      t.integer :upper_bound
    end

    # Activity Definition

    create_table :activity_definitions do |t|
      t.string :name
      t.text :description
    end

    create_table :alpha_criterion_definitions do |t|
      t.integer :criterion_type
      t.boolean :partial
      t.boolean :minimal
    end

    create_table :work_product_criterion_definitions do |t|
      t.integer :criterion_type
      t.boolean :partial
      t.boolean :minimal
    end

    # *********
    # Instances
    # *********

    # Alpha

    create_table :alphas do |t|
      t.string :name
      t.text :link
    end

    create_table :checkpoints do |t|
      t.boolean :fulfilled
    end

    #  Work Product

    create_table :work_products do |t|
      t.string :name
      t.text :link
    end

    # Activity

    create_table :alpha_criterions do |t|
    end

    create_table :work_product_criterions do |t|
    end

  end
end
