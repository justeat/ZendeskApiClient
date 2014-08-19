﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.34209
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code

using TechTalk.SpecFlow;

#pragma warning disable
namespace JustEat.ZendeskApi.Acceptance
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Tickets")]
    public partial class TicketsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "Tickets.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Tickets", "In order to manage issues\r\nAs an api comsumer\r\nI want to be able to get, getall, " +
                    "put, post and delete tickets", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When I call Get by id, I get the given ticket by Id")]
        public virtual void WhenICallGetByIdIGetTheGivenTicketById()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I call Get by id, I get the given ticket by Id", ((string[])(null)));
#line 6
this.ScenarioSetup(scenarioInfo);
#line 7
 testRunner.Given("a ticket in Zendesk with the subject \'The coffee machiene is broken\' and descript" +
                    "ion \'I can\'t work in these conditions!\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
 testRunner.When("I call get by id on the ZendeskApiClient", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
 testRunner.Then("I get a ticket from Zendesk with the subject \'The coffee machiene is broken\' and " +
                    "description \'I can\'t work in these conditions!\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When I call GetAll, I am returned a list of tickets")]
        public virtual void WhenICallGetAllIAmReturnedAListOfTickets()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I call GetAll, I am returned a list of tickets", ((string[])(null)));
#line 11
this.ScenarioSetup(scenarioInfo);
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Subject",
                        "Description"});
            table1.AddRow(new string[] {
                        "I\'ve swallowed my mouse cable",
                        "It\'s a bit of a problem"});
            table1.AddRow(new string[] {
                        "The coffee machiene is broken",
                        "I can\'t work in these conditions!"});
#line 12
 testRunner.Given("the following tickets in Zendesk", ((string)(null)), table1, "Given ");
#line 16
 testRunner.When("I call getall by id on the ZendeskApiClient", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table2 = new TechTalk.SpecFlow.Table(new string[] {
                        "Subject",
                        "Description"});
            table2.AddRow(new string[] {
                        "I\'ve swallowed my mouse cable",
                        "It\'s a bit of a problem"});
            table2.AddRow(new string[] {
                        "The coffee machiene is broken",
                        "I can\'t work in these conditions!"});
#line 17
 testRunner.Then("I get a ticket from Zendesk with the following values", ((string)(null)), table2, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When I call Post I am able to add a ticket")]
        public virtual void WhenICallPostIAmAbleToAddATicket()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I call Post I am able to add a ticket", ((string[])(null)));
#line 22
this.ScenarioSetup(scenarioInfo);
#line 23
 testRunner.Given("a ticket in Zendesk with the subject \'I\'ve swallowed my mouse cable\' and descript" +
                    "ion \'It\'s a bit of a problem\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 24
 testRunner.When("I call get by id on the ZendeskApiClient", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 25
 testRunner.Then("I get a ticket from Zendesk with the subject \'I\'ve swallowed my mouse cable\' and " +
                    "description \'It\'s a bit of a problem\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When I call Post I am able to add a task")]
        public virtual void WhenICallPostIAmAbleToAddATask()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I call Post I am able to add a task", ((string[])(null)));
#line 27
this.ScenarioSetup(scenarioInfo);
#line 28
 testRunner.Given("a task in Zendesk with the subject \'I\'ve swallowed my mouse cable\' and descriptio" +
                    "n \'It\'s a bit of a problem\' and type \'Task\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 29
 testRunner.When("I call get by id on the ZendeskApiClient", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 30
 testRunner.Then("I get a task from Zendesk with the subject \'I\'ve swallowed my mouse cable\' and de" +
                    "scription \'It\'s a bit of a problem\' and type \'Task\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When I call Put I am able to update a ticket")]
        public virtual void WhenICallPutIAmAbleToUpdateATicket()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I call Put I am able to update a ticket", ((string[])(null)));
#line 32
this.ScenarioSetup(scenarioInfo);
#line 33
 testRunner.Given("a ticket in Zendesk with the subject \'I\'ve swallowed my mouse cable\' and descript" +
                    "ion \'It\'s a bit of a problem\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 34
 testRunner.When("I update the ticket with the status \'closed\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 35
 testRunner.And("I call get by id on the ZendeskApiClient", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
 testRunner.Then("I get a ticket from Zendesk with the status \'closed\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When I call Delete the ticket is deleted from zendesk")]
        public virtual void WhenICallDeleteTheTicketIsDeletedFromZendesk()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I call Delete the ticket is deleted from zendesk", ((string[])(null)));
#line 38
this.ScenarioSetup(scenarioInfo);
#line 39
 testRunner.Given("a ticket in Zendesk with the subject \'The coffee machiene is broken\' and descript" +
                    "ion \'I can\'t work in these conditions!\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
 testRunner.When("I call delete by id on the ZendeskApiClient", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 41
 testRunner.Then("the ticket is no longer in zendesk", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("When I call Get by id, I get told it was created via the api")]
        public virtual void WhenICallGetByIdIGetToldItWasCreatedViaTheApi()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("When I call Get by id, I get told it was created via the api", ((string[])(null)));
#line 43
this.ScenarioSetup(scenarioInfo);
#line 44
 testRunner.Given("a ticket in Zendesk with the subject \'The coffee machiene is broken\' and descript" +
                    "ion \'I can\'t work in these conditions!\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 45
 testRunner.When("I call get by id on the ZendeskApiClient", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then("I get a ticket from Zendesk which is via the api", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
