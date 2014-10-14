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

Administrators are able to create, edit and delete students and courses.

---
### Users (students) section: 

*Views:*

*  Before log-in students should see a list of all available courses.
*  After log-in students should see the courses that they are enrolled in.

*Actions:*

* Students should be able to enroll in any available course.

---

### Architecture:

The web application should be developed into layers: 

* Data layer (Repository, Unit of Work patterns)
* Application layer - using Unit of Work and Dependency Injection (Unity, Ninject, etc)
