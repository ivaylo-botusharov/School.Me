AspNet-Mvc-School
=================

Short Description: A sample 'School' web application implemented through ASP.NET MVC and designed to be starting scaffold for other projects.

<img src="https://raw.githubusercontent.com/encounter12/AspNet-Mvc-School/master/school-image.jpg" width="400" alt="School image" />


Details: The school should contain 'Students' and 'Courses'. Each student could be enrolled in several courses. There should be users and administration sections. 

---
### Administration section: 

*Views:*

* Before log-in administrators should see a list of all available courses.
* After log-in administrators should see a list of all students.

*Actions:*

* Administrators are able to **create**, **edit** and **delete** Students and Courses.
* They are also able to search for Students and Courses.

---
### Users (students) section: 

*Views:*

*  Before log-in students should see a list of all available courses.
*  After log-in students should see the courses that they are enrolled in.

*Actions:*

* Students should be able to enroll in any available course.

---

### Architecture:

The web application must be developed into layers: 

* Data layer (Repository, Unit of Work patterns)
* Application (Services) layer - using Unit of Work (from the Data layer) and Dependency Injection Container (Unity, Ninject, etc).
