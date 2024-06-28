StudentPortal/
│
├── Controllers/
│   ├── AccountController.cs
│   ├── AdminController.cs
│   └── StudentController.cs
│
├── Models/
│   ├── Course.cs
│   ├── SelectedCourse.cs
│   └── User.cs
│
├── Views/
│   ├── Account/
│   │   └── Login.cshtml
│   │
│   ├── Admin/
│   │   ├── CreateCourse.cshtml
│   │   └── Index.cshtml
│   │
│   ├── Student/
│   │   ├── Index.cshtml
│   │   ├── SelectCourses.cshtml
│   │   └── ViewSelectedCourses.cshtml
│   │
│   ├── Shared/
│   │   ├── _Layout.cshtml
│   │   ├── _LoginPartial.cshtml
│   │   └── _ValidationScriptsPartial.cshtml
│   │
│   └── Error/
│       └── (Error handling views)
│
├── BusinessLogicLayer/
│   ├── Interfaces/
│   │   └── ICourseService.cs
│   ├── Services/
│   │   └── CourseService.cs
│   └── AutoMapperProfiles/
│       └── CourseProfile.cs
│
├── DataAccessLayer/
│   ├── Context/
│   │   └── StudentPortalDbContext.cs
│   ├── Repositories/
│   │   ├── Interfaces/
│   │   │   ├── ICourseRepository.cs
│   │   │   └── IStudentRepository.cs
│   │   ├── CourseRepository.cs
│   │   └── StudentRepository.cs
│   └── Migrations/
│       └── (Entity Framework migrations)
│
├── Infrastructure/
│   └── (Helper classes, utilities)
│
├── App_Start/
│   ├── AutoMapperConfig.cs
│   └── RouteConfig.cs
│
├── Global.asax
├── Global.asax.cs
├── Packages.config
└── Web.config
