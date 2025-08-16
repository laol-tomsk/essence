class SeAlpha < ActiveRecord::Base
  has_many :se_states, dependent: :destroy
end
