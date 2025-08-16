class CheckpointsReferences < ActiveRecord::Migration[5.2]
  def change
    add_reference :alpha_checkpoint_states, :checkpoints, index: true, foreign_key: true
    add_reference :wp_checkpoint_states, :wp_checkpoints, index: true, foreign_key: true
  end
end
