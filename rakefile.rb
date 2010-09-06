require File.join(File.dirname(__FILE__), 'libs/albacore/albacore.rb')
#require_relative 'libs/albacore/albacore.rb'
require 'open3'    # required for capturing standard output

CONFIGURATION = "Release"

ROOT_DIR				= File.dirname(__FILE__) + "/"
SRC_PATH				= ROOT_DIR + "src/"
LIBS_PATH				= ROOT_DIR + "libs/"
OUTPUT_PATH				= ROOT_DIR + "bin/"
DIST_PATH				= ROOT_DIR + "dist/"
TEST_OUTPUT_PATH		= ROOT_DIR + "bin/Tests/"
XUNIT32_CONSOLE_PATH	= LIBS_PATH + "xunit-1.6.1/xunit.console.clr4.x86.exe"

task :default => :full

task :full => [:build_release,:test,:package_binaries]

desc "Run Tests"
task :test => [:main_test]

desc "Prepare build"
task :prepare => [:clean] do
	mkdir OUTPUT_PATH unless File.exists?(OUTPUT_PATH)
	mkdir DIST_PATH unless File.exists?(DIST_PATH)
	mkdir TEST_OUTPUT_PATH unless File.exists?(TEST_OUTPUT_PATH)
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
msbuild :build_release => [:prepare] do |msb|
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
	mkdir DIST_PATH unless File.exists?(DIST_PATH)
	sh "git archive HEAD --format=zip > dist/dist.source-" + getGitLastCommit + ".zip"
	#git archive HEAD --format=zip > dist/dist.source-`git reflog | grep 'HEAD@{0}' | cut -d " " -f1 | sed 's/[.]*//g'`.zip
end

def getGitLastCommit

	buffer = [] 
	Open3::popen3("git reflog | grep 'HEAD@{0}' | cut -d \" \" -f1 | sed 's/[.]*//g'") do |stdin,stdout,stderr| 
	  begin 
		while line = stdout.readline 
		  buffer << line 
		end 
	  rescue 
	  end 
	end

	return buffer[0].chop # get the first line and chop the \n
end

xunit :main_test => [:build_release] do |xunit|
	xunit.command = XUNIT32_CONSOLE_PATH
	xunit.assembly = SRC_PATH + "Tests/FacebookSharp.Tests/bin/Release/FacebookSharp.Tests.dll"
	xunit.html_output = TEST_OUTPUT_PATH
	xunit.options '/nunit ' + TEST_OUTPUT_PATH + 'FacebookSharp.Tests.nUnit.xml', '/xml ' + TEST_OUTPUT_PATH + 'FacebookSharp.Tests.xUnit.xml'
end