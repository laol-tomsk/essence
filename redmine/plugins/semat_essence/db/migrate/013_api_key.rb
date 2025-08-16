class ApiKey < ActiveRecord::Migration[5.2]
  def change
    add_column :projects, :essence_api_key, :string
  end
end
