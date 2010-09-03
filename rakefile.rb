require 'albacore'

task :default => :build

desc "Build solution using MSBuild"
msbuild :build do |msb|
	msb.solution = "src/FacebookSharp.sln"
	msb.targets	:clean, :build
	msb.properties :configuration => :release
end