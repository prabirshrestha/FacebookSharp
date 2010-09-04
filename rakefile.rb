require 'albacore'

CONFIGURATION = "Release"

SRC_PATH		= "src/"
LIBS_PATH		= "libs/"
OUTPUT_PATH		= "bin/"
DIST_PATH		= "dist/"

task :default => :full

task :full => [:package_binaries]

desc "Prepare build"
task :prepare do
	mkdir OUTPUT_PATH unless File.exists?(OUTPUT_PATH)
	mkdir DIST_PATH unless File.exists?(DIST_PATH)
	cp "LICENSE.txt", OUTPUT_PATH
	cp "README.md" , OUTPUT_PATH
	cp LIBS_PATH + "RestSharp/RestSharp.License.txt", OUTPUT_PATH
	cp LIBS_PATH + "RestSharp/Newtonsoft.Json.License.txt", OUTPUT_PATH
end

desc "Clean build outputs"
task :clean => [:clean_msbuild] do
	FileUtils.rm_rf OUTPUT_PATH
	FileUtils.rm_rf DIST_PATH
end

desc "Clean solution outputs"
msbuild :clean_msbuild do |msb|
	msb.properties :configuration => :Release
	msb.solution = SRC_PATH + "FacebookSharp.sln"
	msb.targets	:Clean
end

desc "Build solution (default)"
msbuild :build_release => [:clean,:prepare] do |msb|
	msb.properties :configuration => :Release
	msb.solution = SRC_PATH + "FacebookSharp.sln"
	msb.targets	:Build
end

desc "Create a zip package for the release binaries"
zip :package_binaries => [:build_release] do |zip|
	zip.directories_to_zip OUTPUT_PATH
    zip.output_file = 'dist.binaries.zip'
    zip.output_path = DIST_PATH
end

desc "Create a source package (requires git in PATH)"
task :package_source do
	sh "git archive HEAD --format=zip > " + DIST_PATH + "dist.src.zip"
end