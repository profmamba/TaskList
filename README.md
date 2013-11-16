# TaskList #


An MVC 4 / WebApi / Knockout.js tech stack demo. 

This project is primarily a sample of my approach to building modern web apps on the Microsoft stack.


## Install ##

* If you want to use the database project to create the db, you have to have SQL Data Tools (Sep 2013 +) installed : http://msdn.microsoft.com/en-us/jj650015
 * Run Schema Compare and then Scripts/Seed
* Nuget packages are not included, use nuget package manager to update all missing dependencies

## Notes ##
* Service/Repository & Unit Of work pattern was employed. 
 * Context = Repository (DbSet) / Unit Of Work (DbContext)
 * Logic = Service (Business Logic)
*	Objects are almost anemic because of the nature of the project. This was not intended though.

## Todo ##
*	Tests have only been written for logic layer, typically a full suite would include
 * Object unit tests for business logic
 * WebApi and MVC controller unit tests
 * Selenium UI integration tests for end to end testing (defined in Specflow)
 * Jasmine (javascript) tests for the KO 'controllers'
* No WCF Service layer was introduced but typically I would do one of the following so a true n-tier arch can be achieved
 * Split out WebApi into its own project
 * Introduce WCF if WebApi is supposed to be just for front end web app (i.e. a proxy of of sorts).
* The WebApp project needs a lot of cleanup as only the standard templates were used, thus there is a lot of unnecessary bits floating around.

## Interesting Parts ##
* A lot of the more interesting parts are floating around in the Knockout parts (the rest seems pretty standard to me since I've been doing it like that for a while, whereas I have been playing a lot with KO and Angular lately)
 * The split of bindings into a ViewModel and independent Controller in WebApp\Scripts\taskList.ko.js (this would be scope and controllers in Angular)
 * The use of shared viewmodels and 'promises' (jqXHR) to reuse common functionality, where a viewmodel can be composed of different views (this would be factories in Angular)
 * The introduction of custom bindings  in KO (to use the timeago.js effectively) (this would be directives in Angular)
* For the rest the following might be interesting
 * The use of FakeItEasy mocking (A.CallTo().Returns/MustHaveHappened) in the test project.
 * An (almost) proper faked/mocked DBContext and interfaces that can be injected to work with EF dependent code. 
 * The use of the EF Fluent Api + mappings (in the context project) to have a truly independent POCO layer that gracefully maps to the DB schema (it's sometimes called 'code second').

