require 'albacore'
task :default => [:git_commit_and_push]

desc "Build Solution"
msbuild :build do |msb|
	msb.properties :configuration => :Debug
	msb.targets :Clean, :Build
	msb.solution = "SalonManager.sln"
end

desc "Run unit tests"
nunit :test => :build do |nunit|
	nunit.command = "../Library/NUnit/bin/nunit-console.exe"
	nunit.assemblies "./SalonManager.web.test/sllabres.web.test.csproj"
	nunit.options '/xml=SalonManager.Tests-Results.xml'
end

task :git_commit_and_push => :test_javascript do
	puts "Committing changes."
	puts `git add .`	
	puts `git commit -m "Automated Commit"`
	puts `git push origin master`
end

task :test_javascript => :test do
	Dir["SalonManager.web/Scripts/tests/*.htm"].each do |file|
		phantom_result = `phantomjs SalonManager.web/Scripts/tests/run-qunit.js #{file}`
		puts phantom_result
		fail "Javascript test failure" if !phantom_result.include? '0 failed'
	end	
end