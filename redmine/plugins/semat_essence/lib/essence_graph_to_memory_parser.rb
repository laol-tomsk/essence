class EssenceGraphToMemoryParser < Nokogiri::XML::SAX::Document

  class EssenceNode
    attr_accessor :nodeId
    attr_accessor :parents
    attr_accessor :probabilities
  end

  attr_reader :nodes

  def start_document
    @stateCounter = 0
    @currentTag = ""
    @currentString = ""
    @nodes = Hash.new
  end

  def start_element(name, attrs = [])
    #p "start element #{name}"
    case name
    when 'cpt', 'decision'
      @node = EssenceNode.new
      @node.nodeId = attrs[0][1]
      @nodes[@node.nodeId] = @node
    when 'parents'
      @currentTag = name
    when 'probabilities'
      @currentTag = name
      @currentString = ""
    end
  end


  def end_element(name)
    if name == 'cpt'
      @stateCounter = 0
      @node = nil
    end
    if name == 'probabilities'
      @node.probabilities = filter_probabilities(@currentString)
      @currentString = ""
    end
    @currentTag = ""
    #p "endOf element #{name}"
  end

  def characters(string)
    case @currentTag
    when 'parents'
      @node.parents = string
    when 'probabilities'
      @currentString = @currentString + string
      #@node.probabilities = filter_probabilities(string)
    end
  end

  def end_document
    p "End of graph description. #{EssenceGraphNode.count} nodes were successfully parsed."
    @nodes
  end

  def filter_probabilities(probabilities)
    array = probabilities.split(" ").map {|s| s.to_f}
    result = array.select.with_index do |elem, index|
      index % 2 == 0
    end
    #"#{result.join(" ")}"
    result
  end
end
