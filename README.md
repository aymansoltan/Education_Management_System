🎓 Education Management System (EMS)
A robust, production-ready Learning Management & Examination System built with .NET 9. This system automates the educational process, from student enrollment to dynamic exam generation and automated grading.

🏛️ Architectural Integrity
The project is built following Clean Architecture and N-Tier principles to ensure the system is scalable, testable, and maintainable:

API Layer: RESTful endpoints with standardized responses and middleware for global error handling.

Application Layer: Contains business logic, MediatR (if used), DTOs, and Service interfaces.

Infrastructure Layer: Implementation of Entity Framework Core, Repository Pattern, and Unit of Work.

Domain Layer: Core entities, exceptions, and domain logic.

🚀 Core Features
Dynamic Exam Generation: Automated creation of exams based on question banks, difficulty levels, and subjects.

Automated Grading Engine: Instant evaluation of student submissions with detailed performance analytics.

Advanced User Roles: Granular permission system for Admins, Instructors, and Students.

Real-time Notifications: Leveraging SignalR for instant alerts and exam start/end updates.

Optimized Data Access: Use of SQL Server Stored Procedures and custom schemas for high-performance reporting.

🛠️ Tech Stack
Backend: .NET 9, C#, ASP.NET Core Web API.

Database: SQL Server (Advanced indexing, Triggers, and Stored Procedures).

Data Access: Entity Framework Core (Code First).

Frontend: Vanilla JS, HTML5, CSS3, and Bootstrap for a fast, responsive dashboard.

Communication: SignalR for real-time interactivity.

📂 Database & Schema Design
The system uses a highly normalized schema to handle:

Complex relationships between Courses, Instructors, and Students.

Question banks supporting multiple formats (MCQ, True/False).

Storage optimization using dedicated Filegroups and maintenance via SQL Server Agent Jobs.
