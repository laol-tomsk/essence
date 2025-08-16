require_dependency 'project'

module ProjectPatch
  def self.included(base)
    base.class_eval do
      unloadable # Send unloadable so it will not be unloaded in development
      belongs_to :method_definition
      has_many :work_products, dependent: :destroy
      has_many :alphas, dependent: :destroy
    end
  end
end

Project.send(:include, ProjectPatch)