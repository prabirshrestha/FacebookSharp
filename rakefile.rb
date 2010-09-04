require 'albacore'

CONFIGURATION = "Release"

SRC_PATH		= "src/"
LIBS_PATH		= "libs/"
OUTPUT_PATH		= "bin/"

task :default => :full

task :full => [:build_release]

desc "Clean build outputs"
task :clean do
	FileUtils.rm_rf OUTPUT_PATH
end

desc "Build solution"
msbuild :build_release => [:clean] do |msb|
	msb.properties :configuration => :Release
	msb.solution = SRC_PATH + "FacebookSharp.sln"
	msb.targets	:Build
end