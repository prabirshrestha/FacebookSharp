# this ruby script generates the flag values for enums
# and saves it with .data extension
data_path="extended_permissions.data"

counter = 0

File.open(data_path,"r") do |infile|
	File.open(data_path+".dat","w") do |f2|
		while(line = infile.gets)
			line = line.rstrip
			if line.length > 0 and line[line.length-1]==44
				flagval = 2**counter;
				f2.puts "#{line.chop} = #{flagval},"
				counter = counter + 1
			else
				f2.puts line
			end
		end
		f2.puts "// #{2**counter}"
	end
end