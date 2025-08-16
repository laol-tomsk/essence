module CheckboxFixingHelper

  def check_checkbox_correctness(alphas)
    definition = CheckpointDefinition.all.first
    if definition.order == nil || definition.order == 0
      fix_checkboxes
      p "Checkboxes order was added."
      #fix_old_nodes(alphas)
      p "Incorrect checkbox state node names were fixed."
    end
  end

  def fix_checkboxes
    order = { 13 => 20, 61 => 10, 99 => 30,
              14 => 50, 86 => 40, 111 => 30, 150 => 10, 164 => 20,
              98 => 10, 100 => 50, 151 => 20, 165 => 40, 181 => 30,
              114 => 60, 118 => 50, 144 => 30, 152 => 10, 153 => 20, 154 => 40,
              96 => 10, 155 => 20, 162 => 30,
              115 => 20, 149 => 10,

              53 => 30, 97 => 40, 160 => 10, 199 => 20,
              12 => 90, 30 => 80, 41 => 10, 109 => 70, 129 => 50, 132 => 60, 147 => 40, 178 => 20, 180 => 30,
              27 => 40, 45 => 50, 62 => 80, 73 => 60, 108 => 70, 116 => 30, 133 => 10, 134 => 20, 187 => 90,
              1 => 10, 18 => 20, 20 => 40, 191 => 50, 201 => 30,
              43 => 10, 130 => 20, 182 => 40, 200 => 30,
              88 => 20, 131 => 30, 159 => 10,

              11 => 10, 17 => 60, 38 => 50, 59 => 20, 72 => 70, 170 => 40, 190 => 30,
              10 => 60, 36 => 30, 37 => 40, 68 => 50, 69 => 10, 172 => 20,
              6 => 70, 39 => 40, 120 => 60, 171 => 10, 173 => 50, 174 => 20, 177 => 30,
              95 => 40, 163 => 30, 168 => 20, 198 => 10,
              8 => 30, 169 => 10, 175 => 20,
              87 => 30, 90 => 20, 123 => 10, 196 => 40,

              71 => 20, 137 => 30, 158 => 10,
              21 => 30, 125 => 20, 136 => 10, 203 => 40,
              19 => 30, 124 => 10, 192 => 20,
              83 => 10, 107 => 50, 121 => 20, 122 => 30, 189 => 40,
              157 => 10, 179 => 20,
              50 => 10, 176 => 20,

              26 => 40, 31 => 20, 57 => 90, 58 => 30, 74 => 100, 85 => 10, 126 => 60, 127 => 70, 139 => 50, 148 => 80,
              24 => 80, 44 => 10, 48 => 70, 60 => 30, 63 => 50, 77 => 60, 79 => 90, 80 => 40, 146 => 20,
              25 => 20, 51 => 30, 81 => 40, 204 => 10,
              7 => 30, 28 => 10, 33 => 20, 142 => 40, 202 => 50,
              78 => 20, 84 => 30, 138 => 10,

              9 => 40, 94 => 50, 101 => 60, 161 => 20, 186 => 10, 193 => 30,
              55 => 40, 56 => 50, 66 => 60, 70 => 10, 92 => 30, 105 => 20,
              5 => 30, 49 => 50, 102 => 10, 103 => 60, 119 => 20, 167 => 40,
              4 => 20, 65 => 30, 197 => 10,
              32 => 40, 104 => 20, 106 => 10, 194 => 30,
              75 => 20, 89 => 10,

              3 => 50, 29 => 20, 54 => 30, 64 => 40, 110 => 70, 128 => 10, 156 => 60,
              2 => 50, 15 => 100, 22 => 10, 34 => 20, 35 => 80, 52 => 90, 67 => 110, 135 => 30, 143 => 40, 166 => 60, 185 => 70,
              40 => 30, 42 => 10, 113 => 20, 184 => 40,
              23 => 70, 46 => 40, 112 => 50, 117 => 60, 145 => 30, 183 => 10, 195 => 20,
              93 => 10, 140 => 30, 141 => 20,
              16 => 40, 47 => 30, 76 => 10, 82 => 20, 91 => 60, 188 => 50 }

    order.each do |definition_id|
      definition = CheckpointDefinition.where(["id = ?", definition_id[0]]).first
      definition.order = definition_id[1]
      definition.save
    end
  end

  def fix_old_nodes(alphas)
    # get array with incorrect nodes names
    incorrect_names = Hash.new
    alphas.each do |alpha|
      if alpha.definition.parent == nil
        alpha.definition.state_definitions.order(order: :asc).each_with_index do |state, state_number|
          state.checkpoint_definitions.each_with_index do |checkpoint_definition, checkbox_number|
            incorrect_names["#{get_alpha_code(alpha.name)}#{state_number + 1}#{checkbox_number + 1}"] = checkpoint_definition.id
            #node_names << "#{get_alpha_code(alpha.name)}#{state_number + 1}#{checkbox_number + 1}"
          end
        end
      end
    end
    p "incorrect " + incorrect_names.to_s

    # get array with correct nodes names
    correct_names = Hash.new
    alphas.each do |alpha|
      if alpha.definition.parent == nil
        alpha.definition.state_definitions.order(order: :asc).each_with_index do |state, state_number|
          state.checkpoint_definitions.order(order: :asc).each_with_index do |checkpoint_definition, checkbox_number|
            correct_names[checkpoint_definition.id] = "#{get_alpha_code(alpha.name)}#{state_number + 1}#{checkbox_number + 1}"
          end
        end
      end
    end
    p "correct " + correct_names.to_s

    # for every node name get all checkbox states and change name to correct
    CheckboxState.all.each do |state|
      checkpoint_id = incorrect_names[state.nodeId]
      state.nodeId = correct_names[checkpoint_id]
      state.save
    end
  end
end
