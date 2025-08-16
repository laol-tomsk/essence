class CreateAlphacards < ActiveRecord::Migration[5.2]
  def change
    create_table :alphacards do |t|
      t.string :cardtype
      t.integer :position
      t.text :data
      t.string :projectid
      t.integer :stateid
    end
  end
end
