CREATE TABLE [dbo].[Students]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [GroupId] INT NOT  NULL, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [BirthYear] INT NULL, 
    [AverageMark] INT NULL
	FOREIGN KEY ([GroupId]) REFERENCES Groups(Id)
)
