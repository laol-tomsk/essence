require_dependency 'issue'

module IssuePatch
  def self.included(base)
    base.class_eval do
      unloadable # Send unloadable so it will not be unloaded in development
      belongs_to :activity_definition
      has_many :work_product_criterions
      has_many :alpha_criterions
    end
  end
end

Issue.send(:include, IssuePatch)