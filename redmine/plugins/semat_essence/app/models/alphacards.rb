class Alphacards < ActiveRecord::Base
  def updatedata(id, data)
    a = Alphacards.find(id)
    a.data = data
    a.save
  end
  def updateposition(id, position)
    a = Alphacards.find(id)
    a.position = position
    a.save
  end
end
