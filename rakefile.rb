require 'albacore'

CONFIG		= "Release"

SRC_PATH	= "src/"
LIBS_PATH	= "libs/"
OUTPUT_PATH = "bin/"

task :default => :full

task :full => [:clean, :build_release]

desc "Clean"
task :clean do
	FileUtils.rm_rf OUTPUT_PATH
end

desc "Build solution using MSBuild"
msbuild :build_release do |msb|
	msb.properties :configuration => :release	
	msb.solution = SRC_PATH + "FacebookSharp.sln"
	msb.targets	:Build
end