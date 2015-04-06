School.Me
=================

Short Description: A sample web application for managing primary and secondary school educational and organizational activities.

<img src="https://raw.githubusercontent.com/encounter12/AspNet-Mvc-School/master/school-image.jpg" width="400" alt="School image" />

## Business requirements

The web application consists of 2 areas: public and private one (for logged-in users). 

---
### Public Area

The public area has 3 web pages - Home, About and Contact. 

On the top of the 'Home' page there is an image slider showing photos of the School premises and classroom activities. Below the image slider 3 sections appear: 

- News
- Upcoming Events
- Announcements

On the 'About' page there is detailed information regarding the school - its faculty, students, school history, academic focus and achievements (not fully completed yet).

On the 'Contact' page the school address (location), phone number and email address are specified.

Registration: After clicking on the Register link in the Navigation bar a new page opens, on this page there is an option for Students and Teachers registration. After registering in the website students / teachers are automatically logged in and directed to the Home page. 

---

### Private Area

Private area could be accessed only by registered users. The 'Dashboard' section in this area is designed for website administrators. It contains links to 4 web pages:

*Teachers* (displaying the school's teachers list) - Next to each teacher there are buttons for: 

* viewing teacher details
* editing teacher details
* deleting teacher


*Students* (displaying the school's student list) - Next to each student there are buttons for: 

* viewing student details
* editing student details
* deleting teacher

*Academic Years* (listing of all academic years since the school is operating). 

Administrator can create new academic year by clicking on the 'Add' button, on the new page specify the new year start and end dates and click on Save. During the creation of new academic year new grades are created and if there are previous academic years all students from specific classes are shifted into new classes one grade higher, e.g. students from 4A class are automatically transferred to 5A class. The only exception applies when respective classes from two neighboring grades (e.g. 7A, 8A) have different school themes (for ex: 7A has 'General' theme and 8A has 'Physics' theme). In this case new school class is not created automaticaly and students are not transferred into higher grade. So administrators should create the School class themselves (e.g. 8A) and manually add the students from the previous academic year's class (7A).     

On this page next to each academic year there is a 'Details' button which displays the academic year details: 

* Academic year start date
* Academic year end date
* 'Active' status
* Expandable grades list (Grade 1 - Grade 12) having links to each grade classes.

Clicking on the link of specific class opens a new page having the class details (Name, Academic Year, students number) and its students list. Next to each student there are buttons:  Edit, Details, Delete

Superadministrator can view a list of website administrators and create / delete an admin.

- Master Schedule - A web page where administrators fill in a calendar each class periods with their respective subjects (not completed yet).

---
## Technical requirements

Web technologies: <a href="http://www.asp.net/mvc/mvc5" target="_blank">ASP.NET MVC 5</a>, <a href="http://www.asp.net/identity" target="_blank">ASP.NET Identity 2</a>, <a href="https://msdn.microsoft.com/en-us/data/ee712907.aspx" target="_blank">Entity Framework 6</a>

### Areas

##### <a href="https://github.com/encounter12/School.Me/tree/master/School/School.Web/Areas/Administration" target="_blank">Administration</a>
##### <a href="https://github.com/encounter12/School.Me/tree/master/School/School.Web/Areas/Students" target="_blank">Students</a>
##### <a href="https://github.com/encounter12/School.Me/tree/master/School/School.Web/Areas/Teachers" target="_blank">Teachers</a>

### Controllers: 
 - Web Project (Main): Home, Account, Manage
 - Administration Area: Home, AcademicYears, Students, Teachers, Administrators, SchoolClasses, Account
 - Students Area: Account
 - Teachers Area: Account 

### Application Architecture (N-Tier) 

* Domain model layer
* Data layer (Repository pattern)
* Application (Services) layer
* UI Layer


### Architectural Design Patterns: 

- <a href="http://www.asp.net/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application" target="_blank">Repository</a>


- Dependency Injection (using <a href="http://www.ninject.org/" target="_blank">Ninject DI Container</a>)

Each layer should have a corresponding Visual Studio Project. 

Additional Projects: 

* School.Tests
* School.Common 

### Roles: Student, Teacher, Administrator, Superadministrator

### Database Design:

Entities: Student, Teacher, Administrator, ApplicationUser, AcademicYear, Grade, SchoolClass, Subject, Lesson, LessonAttachment, Attachment, Homework, HomeworkAttachment, TotalScore, SchoolTheme

Database Seed: create 3 academic years with grades, classes, students, teachers and subjects.

Custom Classes and Methods: 

- Custom Model Validation (see demo <a href="http://weblogs.asp.net/scottgu/class-level-model-validation-with-ef-code-first-and-asp-net-mvc-3" target="_blank">here</a>) by implementing <a href="https://msdn.microsoft.com/en-us/library/system.componentmodel.dataannotations.ivalidatableobject.aspx" target="_blank">IValidatableObject</a> interface, see: <a href="https://github.com/encounter12/School.Me/blob/master/School/School.Web/Areas/Administration/Models/AcademicYearCreateSubmitModel.cs" target="_blank">School.Web - Areas - Administration - Models - AcademicYearCreateSubmitModel.cs</a>)

- Custom Html Helper extension methods (demos: <a href="http://www.asp.net/mvc/overview/older-versions-1/views/using-the-tagbuilder-class-to-build-html-helpers-cs" target="_blank">here</a> and <a href="http://www.webdevelopmenthelp.net/2014/07/custom-html-helper-in-aspnet-mvc.html" target="_blank">here</a>) for setting up buttons with glyphicon icons (inserting Bootstrap glyphicons span element into an anchor tag (<a href="http://www.w3schools.com/bootstrap/bootstrap_ref_comp_glyphs.asp" target="_blank">W3Schools demos</a>), see: <a href="https://github.com/encounter12/School.Me/blob/master/School/School.Web/Extensions/ActionLinkExtensions.cs" target="_blank">School.Web - Extensions - ActionLinkExtensions.cs</a>

### ASP.NET MVC Extensions

Pager (<a href="https://www.nuget.org/packages/PagedList.Mvc" target="_blank">PagedList.MVC</a>) for displaying a paged list of students and teachers

### Web UI: 

* Bootstrap Accordion for displaying expandable grades list with each grade's classes. Use <a href="http://getbootstrap.com/components/#list-group-linked" target="_blank">Bootstrap Linked items</a> and <a href="http://getbootstrap.com/javascript/#collapse" target="_blank">Bootstrap collapse.js plug-in</a> (demos <a href="http://www.w3schools.com/bootstrap/bootstrap_ref_js_collapse.asp" target="_blank">here</a> and <a href="http://www.bootply.com/uBoT3zP1P2" target="_blank">here</a>).

* Search field with magnifier glass button using <a href="http://getbootstrap.com/components/#input-groups" target="_blank">Bootstrap input groups</a>.

* <a href="http://eonasdan.github.io/bootstrap-datetimepicker/#linked-pickers" target="_blank">Bootstrap Datetimepicker</a> for selecting dates when creating new academic year.

### Code Conventions / Code Standards: 

Comply with the following tools for code standards and analysis: 

<a href="https://stylecop.codeplex.com/" target="_blank">*StyleCop*</a>, excluding rules:
- <a href="http://www.stylecop.com/docs/SA1126.html" target="_blank">SA1126: PrefixCallsCorrectly</a> for School.Web Project

This rule has to be turned off for the web projects so that StyleCop would not require base controller methods like: View() and RedirectToAction() to be prefixed with 'this.'

- <a href="http://stylecop.soyuz5.com/SA1600.html" target="_blank">SA1600 ElementsMustBeDocumented</a> for all VS Projects in Solution (Classed and methods documentation to be completed later)

<a href="https://msdn.microsoft.com/en-us/library/dd264939.aspx" target="_blank">Visual Studio Code Analysis</a> (VS - Analyze - Run Code Analysis on Solution)

---

### Continuous Deployment 

Implement continuous deployment using Azure and AppHarbor cloud. 

* <a href="http://schoolme.azurewebsites.net/" target="_blank">School.Me on Azure</a>
* <a href="http://schoolme.apphb.com/" target="_blank">School.Me on AppHarbor</a>

---
### Web Application Statistics:

* Lines of Code: 2424 (Date: 06 April 2015)