require File.join(File.dirname(__FILE__), 'libs/albacore/albacore.rb')
#require_relative 'libs/albacore/albacore.rb'

def get_version_from_file
	file = File.new('VERSION','r')
	return file.gets.chomp
end

BASE_VERSION = get_version_from_file

CONFIGURATION			= :Release

ROOT_DIR				= File.dirname(__FILE__) + "/"
SRC_PATH				= ROOT_DIR + "src/"
LIBS_PATH				= ROOT_DIR + "libs/"
OUTPUT_PATH				= ROOT_DIR + "bin/"
DIST_PATH				= ROOT_DIR + "dist/"
TEST_OUTPUT_PATH		= ROOT_DIR + "bin/Tests/"
XUNIT32_CONSOLE_PATH	= LIBS_PATH + "xunit-1.6.1/xunit.console.clr4.x86.exe"
DOTNET_VERSION			= :net40
CI_BUILD_NUMBER_PARAM_NAME = 'BUILD_NUMBER'

begin
	gitcommit = `git log -1 --pretty=format:%H`
rescue
	gitcommit = "nogit"
end

puts gitcommit

NIGHTLY = ENV['NIGHTLY'].nil? ? true : ENV[NIGHTLY]

CI_BUILD_NUMBER = ENV[CI_BUILD_NUMBER_PARAM_NAME] || 0

if ENV[CI_BUILD_NUMBER_PARAM_NAME] == nil || NIGHTLY then
	# if we are not running under teamcity or someother CI like hudson.
	# or if nightly is true.
	# generate the version number based on VERSION file.
	VERSION_NO = "#{BASE_VERSION}.#{CI_BUILD_NUMBER}"
else
	# if we are running inside teamcity, then it passes the full version
	# so ignore the VERSION file and overwrite the VERSION_NO and VERSION_LONG
	VERSION_NO = ENV['BUILD_NUMBER']
end

if NIGHTLY then
	VERSION_LONG = "#{VERSION_NO}-nightly-#{gitcommit[0..5]}" 
else
	VERSION_LONG = "#{VERSION_NO}-#{gitcommit[0..5]}" 
end

puts
puts "Base Version: #{BASE_VERSION}"
puts "Version Number: #{VERSION_NO}   :  #{VERSION_LONG} "
print "CI Build Number: "
print CI_BUILD_NUMBER
print " (not running under CI mode)" if CI_BUILD_NUMBER == 0
puts
puts "Git Commit Hash: #{gitcommit}"
puts

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
    zip.output_file = "FacebookSharp-#{VERSION_LONG}-bin.zip"
    zip.output_path = DIST_PATH
end

desc "Create a source package (requires git in PATH)"
task :package_source do
	mkdir DIST_PATH unless File.exists?(DIST_PATH)
	sh "git archive HEAD --format=zip > dist/FacebookSharp-#{VERSION_LONG}-src.zip"
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
