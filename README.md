# Football Teams MVC

**[Football Teams](https://development.cyberzenno.com/football-teams)** is an exercise with the aim to put together  
**As Simple As Possible (ASAP)**  examples in an AspNet MVC Application,for the following concepts and practices:

- **Bootstrap 4 Design** - **[View Design App](https://still-night-6460.bss.design/)**  
I started with a *design first* approach. Before going through the burden of building the actual application, I have designed it in pure html.  
It helped me to have a better understanding of what I wanted to build, and also I had most of the design job done.  

- **Agile Practices** - **[View Board](https://trello.com/b/l73YaarH/football-teams)**  
Because the simplicity of the project, I didn't apply a Fullstack Agile practice (epics, stories, task, releases, etc.), but I still wanted to have my tasks planned in advance.  
Despite its simplicity, there are dozens and dozens of tasks. To have them planned in advance is really helpful, always.  

- **Visual Driven Development ®** - **[View Map](https://development.cyberzenno.com/football-teams/vdd/map)**  
This one is definitely a special one. I will spent the next years of my life, trying to develop and explain this concept.  
For now, let's just have a look at some diagrams on draw.io.  

- **Clean Code** - **[View Code](https://github.com/cyberzenno/football-teams/tree/master/FootballTeams)**  
Or at least a reasonable attempt. Uncle Bob has plenty of theory about it and I really like most of them.  
However, extreme clean code is like compulsory cleaning:  
*cleaning is good, but obsession is not*.  
So where to draw the line? I think the only answer is **common sense**.  

----

- **Domain, Data** and **Generic Repository Pattern**  
By designing a *clear distinction* between Domain and Data, should keep the implementation and usage of the DataContext **loosely coupled**.   
In this way, if I want to migrate form Entity Framework to NHibernate or even MongoDB or so, in theory would be enough to reimplement just one interface.  
But of course, this is the theory... We'll see.

- **Dependency Injection**  
Using Castle Windsor for IoC - Inversion of Control.  
Instead of having to instantiate classes every time we need them, we use an IoC container.  
The Container will be responsible to instantiate or *inject* the concrete class when needed, based on a single (or so) source of settings.

- **Integration Tests**  
Integration Test, is when we test not only the business logic, but also the actual data connection and behaviour. There can be many scenarios where this is useful.  
In this specific case, I use them during the data design phase, to check the CRUD actions and relationships behaviour of Entity Framework Data Context.  
I can re-use the same tests if I want to use another ORM, let's say, NHibernate.

- **Unit Tests** and **Mocking**  
Unit Tests focus only on testing the business logic. In order to avoid unwanted calls to actual services and data, and to simulate specific scenarios we want to test, we use *Mocking*.  

----

- **Authentication and Authorization**  
What if you want to apply some really basic authentication to your app, without installing a full membership framework?  
It took me a while, but I managed to have something really simple, but working.

- **AutoMapping**  
The rule is to have a neat distiction between your DataModel (or DTO - Data Transfer Object) and your ViewModel. That's why is very handy to use an automatic mapper.  
From ViewModels to DataModels and viceversa using AutoMapper. With coming soon example of migration from older to newer version.

- **Cleaner and Simpler Model Data Binding**  
I wanted to use Razor as little as possible, in order to have a better control and understanding over the Html.  
By doing so, is much clear to see how data is sent and received by the controller.  
Also, with less dependency on Razor, the application is almost *Angular Ready*, for the future introduction of Angular JS.  

----

- **Introducing Angular JS**   
Introducing Angular JS to an existing MVC application.  
Then, migrating the whole front-end to a separate Angular JS app. By doing it in separate steps, is simpler to understand where the FE and BE meet and separate.

- **Introducing WebAPI**  
Introducing WebAPI to an existing MVC application and then migrate to a separate WebAPI project, is the same as above, but from the Back End perspective.
