CREATE TABLE UserProfiles (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    EmailAddress NVARCHAR(200),
    Gender NVARCHAR(10) NOT NULL,
    Birthday DATE NOT NULL
);

CREATE OR ALTER  PROCEDURE GetAllUser
AS
BEGIN
    SELECT * FROM UserProfiles;
END;

CREATE OR ALTER  PROCEDURE GetUserProfileById
    @UserID INT
AS
BEGIN
    SELECT * FROM UserProfiles WHERE UserID = @UserID;
END;

CREATE OR ALTER PROCEDURE CreateUserProfile
    @Name NVARCHAR(255),
    @EmailAddress NVARCHAR(255),
    @Gender NVARCHAR(50),
    @Birthday DATE,
    @UserID INT OUTPUT
AS
BEGIN
    INSERT INTO UserProfiles (Name, EmailAddress, Gender, Birthday)
    VALUES (@Name, @EmailAddress, @Gender, @Birthday);

    SET @UserID = SCOPE_IDENTITY();
END;


CREATE OR ALTER  PROCEDURE UpdateUserProfile
    @UserID INT,
    @Name NVARCHAR(100),
    @EmailAddress NVARCHAR(200),
    @Gender NVARCHAR(10),
    @Birthday DATE
AS
BEGIN
    UPDATE UserProfiles
    SET Name = @Name, EmailAddress = @EmailAddress, Gender = @Gender, Birthday = @Birthday
    WHERE UserID = @UserID;
END;

CREATE OR ALTER  PROCEDURE DeleteUserProfile
    @UserID INT
AS
BEGIN
    DELETE FROM UserProfiles WHERE UserID = @UserID;
END;
