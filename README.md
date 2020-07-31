# job4everyone

## Description
An ASP.NET web project for Musala Soft Internship.
## Sample assginment
We want to have a job posting site that benefits both employers and candidates. An employer can publish as many advertisements as he wants, but only 10 are active at the same time. The ad is simple text (description), but there are categories for the type of profession: QA, Developer, Manager, DevOps, PM. The employer must have an account in the system. The applicant does not have an account, but if he / she decides to apply for a particular advertisement, a record must be created with the following characteristics: Names of the candidate and on which advertisement he / she applies. A candidate can apply on all the ads, but only once for each of them. Also make a reference (backend) that shows how many active listings we have by category and how many people have applied for each profession (QA, Developer, Manager, DevOps, PM).
## Installation
The installation can be done using the dotnet CLI.
### Requirements
- .NET Core SDK/Runtime 3.1
- MariaDB >10.4.13 (or MySQL)
- ASP.NET Runtime 3.1
- Entity Framework Core 5.0
### Getting the necessary packages
`dotnet restore` - will get all the required NuGet packages for the app. Do this for all of the projects in the solution.
### Running migrations
Update the database by running these commands while in **job4everyone.Data** project:
`dotnet ef database update Initial`
`dotnet ef database update CreateIdentitySchema`
*You may need to specify **job4everyone.Web** as a startup project
### Run
In the **job4everyone.Web** project run:
`dotnet run` - This will start up the app on localhost
