﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by Reqnroll (https://www.reqnroll.net/).
//      Reqnroll Version:2.0.0.0
//      Reqnroll Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AdventOfCode.Specs
{
    using Reqnroll;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class Day06GuardGallivantFeature : object, Xunit.IClassFixture<Day06GuardGallivantFeature.FixtureData>, Xunit.IAsyncLifetime
    {
        
        private global::Reqnroll.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private static global::Reqnroll.FeatureInfo featureInfo = new global::Reqnroll.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "", "Day 06: Guard Gallivant", null, global::Reqnroll.ProgrammingLanguage.CSharp, featureTags);
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Day06.feature"
#line hidden
        
        public Day06GuardGallivantFeature(Day06GuardGallivantFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
        }
        
        public static async System.Threading.Tasks.Task FeatureSetupAsync()
        {
        }
        
        public static async System.Threading.Tasks.Task FeatureTearDownAsync()
        {
        }
        
        public async System.Threading.Tasks.Task TestInitializeAsync()
        {
            testRunner = global::Reqnroll.TestRunnerManager.GetTestRunnerForAssembly(featureHint: featureInfo);
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Equals(featureInfo) == false)))
            {
                await testRunner.OnFeatureEndAsync();
            }
            if ((testRunner.FeatureContext == null))
            {
                await testRunner.OnFeatureStartAsync(featureInfo);
            }
        }
        
        public async System.Threading.Tasks.Task TestTearDownAsync()
        {
            await testRunner.OnScenarioEndAsync();
            global::Reqnroll.TestRunnerManager.ReleaseTestRunner(testRunner);
        }
        
        public void ScenarioInitialize(global::Reqnroll.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public async System.Threading.Tasks.Task ScenarioStartAsync()
        {
            await testRunner.OnScenarioStartAsync();
        }
        
        public async System.Threading.Tasks.Task ScenarioCleanupAsync()
        {
            await testRunner.CollectScenarioErrorsAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
        {
            await this.TestInitializeAsync();
        }
        
        async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
        {
            await this.TestTearDownAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Part 1, loading the map and finding the guard with the test data")]
        [Xunit.TraitAttribute("FeatureTitle", "Day 06: Guard Gallivant")]
        [Xunit.TraitAttribute("Description", "Part 1, loading the map and finding the guard with the test data")]
        public async System.Threading.Tasks.Task Part1LoadingTheMapAndFindingTheGuardWithTheTestData()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Part 1, loading the map and finding the guard with the test data", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 3
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 4
        await testRunner.GivenAsync("a map of the guard situation", ("....#.....\r\n.........#\r\n..........\r\n..#.......\r\n.......#..\r\n..........\r\n.#..^...." +
                        ".\r\n........#.\r\n#.........\r\n......#..."), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 17
        await testRunner.WhenAsync("you load the grid", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 18
        await testRunner.ThenAsync("the location of the guard should be 4,6", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Part 1, counting the distinc positions the guard visits with the test data")]
        [Xunit.TraitAttribute("FeatureTitle", "Day 06: Guard Gallivant")]
        [Xunit.TraitAttribute("Description", "Part 1, counting the distinc positions the guard visits with the test data")]
        public async System.Threading.Tasks.Task Part1CountingTheDistincPositionsTheGuardVisitsWithTheTestData()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Part 1, counting the distinc positions the guard visits with the test data", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 20
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 21
        await testRunner.GivenAsync("a map of the guard situation", ("....#.....\r\n.........#\r\n..........\r\n..#.......\r\n.......#..\r\n..........\r\n.#..^...." +
                        ".\r\n........#.\r\n#.........\r\n......#..."), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 34
        await testRunner.WhenAsync("you load the grid", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 35
        await testRunner.AndAsync("you count the number of distinct positions the guard visits", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 36
        await testRunner.ThenAsync("the number of distinct locations the guard visits should be 41", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Part 1, loading the map and finding the guard")]
        [Xunit.TraitAttribute("FeatureTitle", "Day 06: Guard Gallivant")]
        [Xunit.TraitAttribute("Description", "Part 1, loading the map and finding the guard")]
        public async System.Threading.Tasks.Task Part1LoadingTheMapAndFindingTheGuard()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Part 1, loading the map and finding the guard", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 38
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 39
        await testRunner.GivenAsync("the map of the guard situation called \'Day06.txt\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 40
        await testRunner.WhenAsync("you load the grid", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 41
        await testRunner.ThenAsync("the location of the guard should be 60,70", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Part 1, counting the distinc positions the guard visits")]
        [Xunit.TraitAttribute("FeatureTitle", "Day 06: Guard Gallivant")]
        [Xunit.TraitAttribute("Description", "Part 1, counting the distinc positions the guard visits")]
        public async System.Threading.Tasks.Task Part1CountingTheDistincPositionsTheGuardVisits()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            global::Reqnroll.ScenarioInfo scenarioInfo = new global::Reqnroll.ScenarioInfo("Part 1, counting the distinc positions the guard visits", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 43
    this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((global::Reqnroll.TagHelper.ContainsIgnoreTag(scenarioInfo.CombinedTags) || global::Reqnroll.TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                await this.ScenarioStartAsync();
#line 44
        await testRunner.GivenAsync("the map of the guard situation called \'Day06.txt\'", ((string)(null)), ((global::Reqnroll.Table)(null)), "Given ");
#line hidden
#line 45
        await testRunner.WhenAsync("you load the grid", ((string)(null)), ((global::Reqnroll.Table)(null)), "When ");
#line hidden
#line 46
        await testRunner.AndAsync("you count the number of distinct positions the guard visits", ((string)(null)), ((global::Reqnroll.Table)(null)), "And ");
#line hidden
#line 47
        await testRunner.ThenAsync("the number of distinct locations the guard visits should be 4967", ((string)(null)), ((global::Reqnroll.Table)(null)), "Then ");
#line hidden
            }
            await this.ScenarioCleanupAsync();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("Reqnroll", "2.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : object, Xunit.IAsyncLifetime
        {
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.InitializeAsync()
            {
                await Day06GuardGallivantFeature.FeatureSetupAsync();
            }
            
            async System.Threading.Tasks.Task Xunit.IAsyncLifetime.DisposeAsync()
            {
                await Day06GuardGallivantFeature.FeatureTearDownAsync();
            }
        }
    }
}
#pragma warning restore
#endregion