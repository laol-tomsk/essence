class SeState < ActiveRecord::Base
  has_many :se_checkpoints, dependent: :destroy
  belongs_to :se_alpha

  def completed?
    checked_checkpoints_count = SeCheckpoint.where("se_state_id = ? AND checked = ?", id, true).count
    se_checkpoints.count == checked_checkpoints_count
  end

end
