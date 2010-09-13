require File.join(File.dirname(__FILE__), 'libs/albacore/albacore.rb')
#require_relative 'libs/albacore/albacore.rb'
require 'open3'    # required for capturing standard output

VERSION_NO = "0.1.0.0"

CONFIGURATION = :Release

ROOT_DIR				= File.dirname(__FILE__) + "/"
SRC_PATH				= ROOT_DIR + "src/"
LIBS_PATH				= ROOT_DIR + "libs/"
OUTPUT_PATH				= ROOT_DIR + "bin/"
DIST_PATH				= ROOT_DIR + "dist/"
TEST_OUTPUT_PATH		= ROOT_DIR + "bin/Tests/"
XUNIT32_CONSOLE_PATH	= LIBS_PATH + "xunit-1.6.1/xunit.console.clr4.x86.exe"
DOTNET_VERSION			= :net40

if ENV['BUILD_NUMBER'].nil? then ENV['BUILD_NUMBER'] = VERSION_NO end
puts 'Version Number: ' + VERSION_NO

task :default => :full

task :full => [:build_release,:test,:precompile_samples_webapplication,:package_binaries]

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
	msb.properties :configuration => CONFIGURATION
	msb.solution = SRC_PATH + "FacebookSharp.sln"
	msb.targets	:Clean
end

desc "Build solution (default)"
msbuild :build_release => [:prepare] do |msb|
	msb.properties :configuration => CONFIGURATION
	msb.solution = SRC_PATH + "FacebookSharp.sln"
	msb.targets	:Build
end

desc "Create a zip package for the release binaries"
zip :package_binaries => [:build_release] do |zip|
	zip.directories_to_zip OUTPUT_PATH
    zip.output_file = "FacebookSharp-#{VERSION_NO}.zip"
    zip.output_path = DIST_PATH
end

desc "Create a source package (requires git in PATH)"
task :package_source do
	mkdir DIST_PATH unless File.exists?(DIST_PATH)
	sh "git archive HEAD --format=zip > dist/FacebookSharp-#{VERSION_NO}-" + getGitLastCommit + ".zip"
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

exec :precompile_samples_webapplication_task do |exec|
	include Configuration::NetVersion
	exec.command = dotnet_path + '/aspnet_compiler.exe'
	exec.parameters = ['-f','-u','-p','src/Samples/FacebookSharp.Samples.WebApplication','-v','/','bin/Samples/FacebookSharp.Samples.WebApplication']
end

task :precompile_samples_webapplication => [:build_release,:precompile_samples_webapplication_task] do
	root_path = OUTPUT_PATH + 'Samples/FacebookSharp.Samples.WebApplication/'
	files_to_remove = 
				[root_path + 'FacebookSharp.Samples.WebApplication.csproj',
				 root_path + 'FacebookSharp.Samples.WebApplication.csproj.user']
	files_to_remove.each{|file| File.delete(file) if File.exist?(file)}
	FileUtils.rm_rf root_path + 'obj'
	FileUtils.rm_rf root_path + 'Properties'
end

def dotnet_path
	include Configuration::NetVersion
	return get_net_version DOTNET_VERSION
end