require 'jcode'

# this ruby script generates the flag values for enums
# and saves it with .dat extension
data_path="extended_permissions.data"

counter = 0

File.open(data_path,"r") do |infile|
	File.open(data_path+".dat","w") do |f2|
		while(line = infile.gets)
			line = line.rstrip
			if line.length > 0 and line[line.length-1]==44
				str = '';
				caps = true
				line.each_char { |c|
					if c == '_'
						caps = true
						next 
					end

					if caps
						str += c.upcase
						caps = false
					else
						str += c.downcase
					end
				}

				flagval = 2**counter;

				f2.puts "[StringValue(\"#{line.chop}\")]"
				f2.puts "#{str.chop} = #{flagval},"
								
				counter = counter + 1
			else
				f2.puts line
			end
		end
		f2.puts "// #{2**counter}"
	end
end