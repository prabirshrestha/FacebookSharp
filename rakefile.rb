require 'albacore'

CONFIGURATION = "Release"

SRC_PATH		= "src/"
LIBS_PATH		= "libs/"
OUTPUT_PATH		= "bin/"

task :default => :full

task :full => [:build_release]

desc "Clean build outputs"
task :clean => [:clean_msbuild] do
	FileUtils.rm_rf OUTPUT_PATH
end

desc "Clean solution outputs"
msbuild :clean_msbuild do |msb|
	msb.properties :configuration => :Release
	msb.solution = SRC_PATH + "FacebookSharp.sln"
	msb.targets	:Clean
end

desc "Build solution"
msbuild :build_release => [:clean] do |msb|
	msb.properties :configuration => :Release
	msb.solution = SRC_PATH + "FacebookSharp.sln"
	msb.targets	:Build
end