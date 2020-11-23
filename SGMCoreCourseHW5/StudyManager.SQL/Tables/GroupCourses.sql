CREATE TABLE [dbo].[GroupCourses]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [GroupId] INT NOT NULL, 
    [CourseId] INT NOT NULL
	FOREIGN KEY ([GroupId]) REFERENCES Groups(Id)
	FOREIGN KEY ([CourseId]) REFERENCES Courses(Id)
)
