class SeCheckpoint < ActiveRecord::Base
  belongs_to :se_state

  def set_checked(value)
    update_attribute(:checked, value)
  end

end
