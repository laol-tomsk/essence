class EssenceGraphParser < Nokogiri::XML::SAX::Document
  def start_document
    @stateCounter = 0
    @currentTag = ""
  end

  def start_element(name, attrs = [])
    #p "start element #{name}"
    case name
    when 'cpt'
      @node = EssenceGraphNode.create({nodeId: attrs[0][1]})
    when 'decision'
      @node = EssenceGraphNode.create({nodeId: attrs[0][1]})
    when 'state'
      if @stateCounter == 0
        @node.update({firstState: attrs[0][1]})
      else
        @node.update({secondState: attrs[0][1]})
      end
      @stateCounter = @stateCounter + 1
    when 'parents'
      @currentTag = name
    when 'probabilities'
      @currentTag = name
    end
  end


  def end_element(name)
    if name == 'cpt'
      @stateCounter = 0
      @node = nil
    end
    @currentTag = ""
    #p "endOf element #{name}"
  end

  def characters(string)
    case @currentTag
    when 'parents'
      @node.update({parents: string})
    when 'probabilities'
      @node.update({probabilities: filter_probabilities(string)})
    end
    # p [:characters, string]
    #  @text_stack.each do |text|
    #  text << string
    #end
  end

  def end_document
    p "End of graph description. #{EssenceGraphNode.count} nodes were successfully parsed."
  end

  def filter_probabilities(probabilities)
    array = probabilities.split(" ").map {|s| s.to_f}
    result = array.select.with_index do |elem, index|
      index % 2 == 0
    end
    "#{result.join(" ")}"
  end
end
