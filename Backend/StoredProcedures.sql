
Create database CSE_23010101030


---------------------------------	MST_USER	-----------------------------------

---------- For Login ----------


CREATE or alter PROCEDURE [dbo].[PR_MST_User_Login]
	@UserName	VARCHAR(100),
	@Password	VARCHAR(100)
AS
BEGIN
    SELECT 
        [dbo].[MST_User].[UserID],
        [dbo].[MST_User].[UserName],
		[dbo].[MST_User].[Password]
    FROM [dbo].[MST_User]
    WHERE (UserName = @UserName or Email = @UserName or Mobile = @UserName)
			AND Password = @Password
END


---------- For Register ----------


CREATE PROCEDURE [dbo].[PR_MST_User_Register]
    @Username VARCHAR(50),
    @Password VARCHAR(500),
    @Email VARCHAR(100)
AS
BEGIN
    INSERT INTO [dbo].[MST_User] 
		(tas
			[Username], 
			[Password], 
			[Email]
		)
    VALUES 
		(
			@Username,
			@Password, 
			@Email
		);
END


----------	Insert ----------

select * from [dbo].[MST_User]

CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Insert]
	@UserName	NVARCHAR(100),
	@Password	NVARCHAR(100),
	@Email		NVARCHAR(100),
	@Mobile		NVARCHAR(100),
	@IsActive	BIT,
	@IsAdmin	bit
AS
BEGIN
	INSERT INTO [dbo].[MST_User] 
	(
		[dbo].[MST_User].[UserName],
		[dbo].[MST_User].[Password],
		[dbo].[MST_User].[Email],
		[dbo].[MST_User].[Mobile],
		[dbo].[MST_User].[IsActive],
		[dbo].[MST_User].[IsAdmin],
		[dbo].[MST_User].[Modified]
	)
	VALUES 
	(
		@UserName,
		@Password,
		@Email,
		@Mobile,
		@IsActive,
		@IsAdmin,
		getdate()
	)
END

insert into [dbo].[MST_User] (UserName,Password,Email,Mobile,IsActive,IsAdmin,Modified)
values ('abc','abcc','abc@gmail.com','1234567890',1,0,getdate())

insert into [dbo].[MST_User] (UserName,Password,Email,Mobile,IsActive,IsAdmin,Modified)
values ('xyz','xyzz','xyz@gmail.com','1234567899',1,0,getdate())

select * from [dbo].[MST_User]


----------	Update ----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Update]
	@UserID		INT,
	@UserName	NVARCHAR(100),
	@Password	NVARCHAR(100),
	@Email		NVARCHAR(100),
	@Mobile		NVARCHAR(100),
	@IsActive	BIT,
    @IsAdmin	bit
AS
BEGIN
	UPDATE [dbo].[MST_User]
	SET [dbo].[MST_User].[UserName]=@UserName,
		[dbo].[MST_User].[Password]=@Password,
		[dbo].[MST_User].[Email]=@Email,
		[dbo].[MST_User].[Mobile]=@Mobile,
		[dbo].[MST_User].[IsActive]=@IsActive,
		[dbo].[MST_User].[IsAdmin]=@IsAdmin,
		[dbo].[MST_User].[Modified]=getdate()
	WHERE [dbo].[MST_User].[UserID]=@UserID
END


----------	Delete ----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_Delete] 
	@UserId INT 
AS
BEGIN
	DELETE FROM [dbo].[MST_User] 
	WHERE [dbo].[MST_User].[UserID]=@UserId
END


----------	Select All ----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectAll] 
AS
BEGIN
	SELECT [dbo].[MST_User].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[IsActive],
		   [dbo].[MST_User].[IsAdmin],
		   [dbo].[MST_User].[Created],
		   [dbo].[MST_User].[Modified]
	FROM [dbo].[MST_User]
	ORDER BY [dbo].[MST_User].[UserName]
END


----------	Select By ID ----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_User_SelectByID] 
	@UserId INT 
AS
BEGIN
	SELECT
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[IsActive],
		   [dbo].[MST_User].[IsAdmin],
		   [dbo].[MST_User].[Created],
		   [dbo].[MST_User].[Modified]
	FROM [dbo].[MST_User]
	WHERE [dbo].[MST_User].[UserID]=@UserId
END

select * from MST_User

----------	Select By UserName or Password ----------


----------	FOR LOGIN WHEN USER ENTERS THEIR USERNAME = USERNAME OR MOBILE OR EMAIL	----------
-----	EXEC [DBO].[PR_MST_USER_SELECTBYUSERNAMEPASSWORD] 'XYZ','XYZ'	-------

CREATE OR ALTER PROCEDURE PR_MST_User_SelectByUserNamePassword
	@USERNAME	VARCHAR(100),
	@PASSWORD	VARCHAR(100)
AS
BEGIN
	SELECT
		[DBO].[MST_User].[UserName],
		[DBO].[MST_User].[Password]
	FROM [DBO].[MST_User]
	WHERE (UserName = @USERNAME or Email = @USERNAME or Mobile = @USERNAME)
			AND Password = @PASSWORD
END


---------------------------------	MST_QUIZ	-----------------------------------

select * from MST_Quiz

----------	Insert	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Insert]
	@QuizName			NVARCHAR(100),
	@TotalQuestions		NVARCHAR(100),
	@QuizDate			datetime,
	@UserID				INT
AS 
BEGIN
	INSERT INTO [dbo].[MST_Quiz] 
	(
		[dbo].[MST_Quiz].[QuizName],
		[dbo].[MST_Quiz].[TotalQuestions],
		[dbo].[MST_Quiz].[QuizDate],
		[dbo].[MST_Quiz].[UserID],
		[dbo].[MST_Quiz].[Modified]
	) 
	VALUES (
		@QuizName,
		@TotalQuestions,
		@QuizDate,
		@UserID,
		getdate()
	)
END

insert into [dbo].[MST_Quiz] (QuizName,TotalQuestions,QuizDate,UserID,Modified)
values ('asp.net','30',getdate(),4,getdate())

insert into [dbo].[MST_Quiz] (QuizName,TotalQuestions,QuizDate,UserID,Modified)
values ('DBMS','30',getdate(),5,getdate())

insert into [dbo].[MST_Quiz] (QuizName,TotalQuestions,QuizDate,UserID,Modified)
values ('DBMS','30',getdate(),5,getdate())


----------	Update	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Update]
	@QuizID				INT,
	@QuizName			NVARCHAR(100),
	@TotalQuestions		NVARCHAR(100),
	@QuizDate			datetime,
	@UserID				INT
AS
BEGIN
	UPDATE [dbo].[MST_Quiz]
	SET	[dbo].[MST_Quiz].[QuizName]=@QuizName,
		[dbo].[MST_Quiz].[TotalQuestions]=@TotalQuestions,
		[dbo].[MST_Quiz].[QuizDate]=@QuizDate,
		[dbo].[MST_Quiz].[UserID]=@UserID
	WHERE [dbo].[MST_Quiz].[QuizID]=@QuizID
END


----------	Delete	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_Delete]
	@QuizID INT 
AS
BEGIN
	DELETE FROM [dbo].[MST_Quiz] 
	WHERE [dbo].[MST_Quiz].[QuizID]=@QuizID 
END


----------	Select All	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_SelectAll] 
AS
BEGIN
	SELECT [dbo].[MST_Quiz].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_Quiz].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_Quiz].[Created],
		   [dbo].[MST_Quiz].[Modified]
	FROM [dbo].[MST_Quiz] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_Quiz].[UserID]=[dbo].[MST_User].[UserID]
	ORDER BY [dbo].[MST_Quiz].[QuizName]
END

exec [dbo].[PR_MST_Quiz_SelectAll] 


----------	Select By ID	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Quiz_SelectByID] 
	@QuizID INT 
AS
BEGIN
	SELECT [dbo].[MST_Quiz].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_Quiz].[UserID],
		   [dbo].[MST_Quiz].[Created],
		   [dbo].[MST_Quiz].[Modified]
	FROM [dbo].[MST_Quiz] 
	WHERE [dbo].[MST_Quiz].[QuizID]=@QuizID
END


--------- Search Quiz ----------


CREATE or alter PROCEDURE [dbo].[PR_MST_Quiz_Search]
    @QuizName NVARCHAR(255) = NULL,
    @MinQuestions INT = NULL,
    @MaxQuestions INT = NULL,
    @FromQuizDate DATE = NULL,
    @ToQuizDate DATE = NULL	
AS
BEGIN
    SELECT [dbo].[MST_Quiz].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_Quiz].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_Quiz].[Created],
		   [dbo].[MST_Quiz].[Modified]
    FROM [dbo].[MST_Quiz]
	inner join [dbo].[MST_User]
	on [dbo].[MST_Quiz].[UserID] = [dbo].[MST_User].[UserID]
    WHERE 
        (@QuizName IS NULL OR QuizName LIKE '%' + @QuizName + '%')
        AND (@MinQuestions IS NULL OR TotalQuestions >= @MinQuestions)
        AND (@MaxQuestions IS NULL OR TotalQuestions <= @MaxQuestions)
        AND (@FromQuizDate IS NULL OR QuizDate >= @FromQuizDate)
        AND (@ToQuizDate IS NULL OR QuizDate <= @ToQuizDate)
    ORDER BY QuizDate DESC;
END;


---------------------------------	MST_QUESTION	-----------------------------------


----------	Insert	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Insert]
	@QuestionText		NVARCHAR(100),
	@QuestionLevelID	nvarchar(100),
	@OptionA			NVARCHAR(100),
	@OptionB			NVARCHAR(100),
	@OptionC			NVARCHAR(100),
	@OptionD			NVARCHAR(100),
	@CorrectOption		NVARCHAR(100),
	@QuestionMarks		int,
	@UserID				INT
AS
BEGIN
	INSERT INTO [dbo].[MST_Question]
	(
		[dbo].[MST_Question].[QuestionText],
		[dbo].[MST_Question].[QuestionLevelID],
		[dbo].[MST_Question].[OptionA],
		[dbo].[MST_Question].[OptionB],
		[dbo].[MST_Question].[OptionC],
		[dbo].[MST_Question].[OptionD],
		[dbo].[MST_Question].[CorrectOption],
		[dbo].[MST_Question].[QuestionMarks],
		[dbo].[MST_Question].[UserID],
		[dbo].[MST_Question].[Modified]
	)
	VALUES
	(
		@QuestionText,
		@QuestionLevelID,
		@OptionA,
		@OptionB,
		@OptionC,
		@OptionD,
		@CorrectOption,
		@QuestionMarks,
		@UserID,
		getdate()
	)
END

insert into [dbo].[MST_Question] (QuestionText,QuestionLevelID,OptionA,OptionB,OptionC,OptionD,CorrectOption,QuestionMarks,UserID,Modified)
values ('What is your name ?','1','abc','def','pqr','xyz','A',1,4,getdate())

insert into [dbo].[MST_Question] (QuestionText,QuestionLevelID,OptionA,OptionB,OptionC,OptionD,CorrectOption,QuestionMarks,UserID,Modified)
values ('What is your name ?','2','abc','def','pqr','xyz','D',1,5,getdate())


----------	Update	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Update]
	@QuestionID				INT,
	@QuestionText		NVARCHAR(100),
	@QuestionLevelID	nvarchar(100),
	@OptionA			NVARCHAR(100),
	@OptionB			NVARCHAR(100),
	@OptionC			NVARCHAR(100),
	@OptionD			NVARCHAR(100),
	@CorrectOption		NVARCHAR(100),
	@QuestionMarks		int,
	@UserID				INT
AS
BEGIN
	UPDATE [dbo].[MST_Question]
	SET [dbo].[MST_Question].[QuestionText]=@QuestionText,
		[dbo].[MST_Question].[QuestionLevelID]=@QuestionLevelID,
		[dbo].[MST_Question].[OptionA]=@OptionA,
		[dbo].[MST_Question].[OptionB]=@OptionB,
		[dbo].[MST_Question].[OptionC]=@OptionC,
		[dbo].[MST_Question].[OptionD]=@OptionD,
		[dbo].[MST_Question].[CorrectOption]=@CorrectOption,
		[dbo].[MST_Question].[QuestionMarks]=@QuestionMarks,
		[dbo].[MST_Question].[UserID]=@UserID
	WHERE [dbo].[MST_Question].[QuestionID]=@QuestionID
END


----------	Delete	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_Delete]
	@QuestionID INT 
AS
BEGIN
	DELETE FROM [dbo].[MST_Question] 
	WHERE [dbo].[MST_Question].[QuestionID]=@QuestionID
END


----------	Select All	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_SelectAll] 
AS
BEGIN
	SELECT[dbo].[MST_Question].[QuestionID],
	[dbo].[MST_Question].[QuestionText],
	[dbo].[MST_Question].[QuestionLevelID],
	[dbo].[MST_QuestionLevel].[QuestionLevel],
	[dbo].[MST_Question].[OptionA],
	[dbo].[MST_Question].[OptionB],
	[dbo].[MST_Question].[OptionC],
	[dbo].[MST_Question].[OptionD],
	--[dbo].[MST_Question].[OptionA]+'<br/>'+[dbo].[MST_Question].[OptionB]+'<br/>'+[dbo].[MST_Question].[OptionC]+'<br/>'+[dbo].[MST_Question].[OptionD],
	[dbo].[MST_Question].[CorrectOption],
	[dbo].[MST_Question].[QuestionMarks],
	[dbo].[MST_Question].[UserID],
	[dbo].[MST_User].[UserName],
	[dbo].[MST_User].[IsActive],
	[dbo].[MST_Question].[Created],
	[dbo].[MST_Question].[Modified]
	FROM [dbo].[MST_Question] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_Question].[UserID]=[dbo].[MST_User].[UserID]
	INNER JOIN [DBO].[MST_QuestionLevel] 
	ON [dbo].[MST_Question].[QuestionLevelID]=[dbo].[MST_QuestionLevel].[QuestionLevelID]
	ORDER BY [dbo].[MST_Question].[QuestionText]
END


----------	Select By ID	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_Question_SelectByID]
	@QuestionID INT 
AS
BEGIN
	SELECT [dbo].[MST_Question].[QuestionID],
		   [dbo].[MST_Question].[QuestionText],
		   [dbo].[MST_Question].[QuestionLevelID],
		   [dbo].[MST_Question].[OptionA],
		   [dbo].[MST_Question].[OptionB],
		   [dbo].[MST_Question].[OptionC],
		   [dbo].[MST_Question].[OptionD],
		   [dbo].[MST_Question].[CorrectOption],
		   [dbo].[MST_Question].[QuestionMarks],
		   [dbo].[MST_Question].[UserID],
		   [dbo].[MST_Question].[Created],
		   [dbo].[MST_Question].[Modified] 
	FROM [dbo].[MST_Question]
	WHERE [dbo].[MST_Question].[QuestionID]=@QuestionID
END


---------------------------------	MST_QUESTIONLEVEL	-----------------------------------


----------	Insert	----------


CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuestionLevel_Insert]
	@QuestionLevel		nvarchar(100),
	@UserID				int
AS
BEGIN
	INSERT INTO [dbo].[MST_QuestionLevel] 
	(
		[dbo].[MST_QuestionLevel].[QuestionLevel],
		[dbo].[MST_QuestionLevel].[UserID],
		[dbo].[MST_QuestionLevel].[Modified]
	) 
	VALUES 
	(
		@QuestionLevel,
		@UserID,
		getdate()
	)
END

insert into [dbo].[MST_QuestionLevel] (QuestionLevel,UserID,Modified)
values ('Easy',4,GETDATE())

insert into [dbo].[MST_QuestionLevel] (QuestionLevel,UserID,Modified)
values ('Medium',5,GETDATE())


----------	Update	----------


Create OR Alter Procedure [dbo].[PR_MST_QuestionLevel_Update]
	@QuestionLevelID	INT,
	@UserID				INT
As
Begin
	Update [dbo].[MST_QuestionLevel]
	set	[dbo].[MST_QuestionLevel].[UserID]				= @UserID	
	Where [dbo].[MST_QuestionLevel].[QuestionLevelID]   = @QuestionLevelID
End


----------	Delete	----------


Create OR Alter Procedure [dbo].[PR_MST_QuestionLevel_Delete]
	@QuestionLevelID INT
As
Begin
	Delete 
	From [dbo].[MST_QuestionLevel]	
	Where [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID
End


----------	Select All	----------


Create OR Alter Procedure [dbo].[PR_MST_QuestionLevel_SelectAll]
As
Begin
	Select [dbo].[MST_QuestionLevel].[QuestionLevelID],
		   [dbo].[MST_QuestionLevel].[QuestionLevel],
		   [dbo].[MST_QuestionLevel].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_QuestionLevel].[Created],	
		   [dbo].[MST_QuestionLevel].[Modified]
	From [dbo].[MST_QuestionLevel]
	Inner join [dbo].[MST_User]
	On [dbo].[MST_QuestionLevel].[UserID] = [dbo].[MST_User].[UserID]
	ORDER BY [dbo].[MST_QuestionLevel].[QuestionLevel]
End


----------	Select By ID	----------

 
Create OR Alter Procedure [dbo].[PR_MST_QuestionLevel_SelectByID]
	@QuestionLevelID INT
As
Begin
	Select [dbo].[MST_QuestionLevel].[QuestionLevelID],
		   [dbo].[MST_QuestionLevel].[QuestionLevel],
		   [dbo].[MST_QuestionLevel].[UserID],
		   [dbo].[MST_QuestionLevel].[Created],	
		   [dbo].[MST_QuestionLevel].[Modified]
	From [dbo].[MST_QuestionLevel]
	Where [dbo].[MST_QuestionLevel].[QuestionLevelID] = @QuestionLevelID
End


---------------------------------	MST_QUIZWISEQUESTION	-----------------------------------


----------	Insert	----------
 

Create OR Alter Procedure [dbo].[PR_MST_QuizWiseQuestions_Insert]
	@QuizID		INT,
	@QuestionID	INT,
	@UserID		INT
As
Begin
	Insert Into [dbo].[MST_QuizWiseQuestions]
	(
		[dbo].[MST_QuizWiseQuestions].[QuizID],		
		[dbo].[MST_QuizWiseQuestions].[QuestionID],	
		[dbo].[MST_QuizWiseQuestions].[UserID],
		[dbo].[MST_QuizWiseQuestions].[Modified]
	)
	Values
		(  
			@QuizID,		
		    @QuestionID,	
		    @UserID,
		    GetDate()
		)
End

insert into [dbo].[MST_QuizWiseQuestions] (QuizID,QuestionID,UserID,Modified)
values (3,1,4,GETDATE())

insert into [dbo].[MST_QuizWiseQuestions] (QuizID,QuestionID,UserID,Modified)
values (2,2,5,GETDATE())

insert into [dbo].[MST_QuizWiseQuestions] (QuizID,QuestionID,UserID,Modified)
values (2,1,4,GETDATE())

insert into [dbo].[MST_QuizWiseQuestions] (QuizID,QuestionID,UserID,Modified)
values (3,2,5,GETDATE())

insert into [dbo].[MST_QuizWiseQuestions] (QuizID,QuestionID,UserID,Modified)
values (3,2,5,GETDATE())

INSERT INTO MST_QuizWiseQuestions (QuizID,QuestionID,UserID,Modified)
VALUES (2, 2, 4, GETDATE())

INSERT INTO MST_QuizWiseQuestions (QuizID,QuestionID,UserID,Modified)
VALUES (4, 2, 4, GETDATE())

INSERT INTO MST_QuizWiseQuestions (QuizID,QuestionID,UserID,Modified)
VALUES (5, 3, 5, GETDATE())

EXEC PR_MST_QuizWiseQuestions_SelectAll

EXEC sp_helptext 'PR_MST_QuizWiseQuestions_SelectAll';


----------	Update	----------


Create OR Alter Procedure [dbo].[PR_MST_QuizWiseQuestions_Update]
	@QuizWiseQuestionsID INT,
	@QuizID				INT,
	@QuestionID			INT,
	@UserID				INT
As
Begin
	Update [dbo].[MST_QuizWiseQuestions]
	set [dbo].[MST_QuizWiseQuestions].[QuizID]		= @QuizID,		
		[dbo].[MST_QuizWiseQuestions].[QuestionID]	= @QuestionID,	
		[dbo].[MST_QuizWiseQuestions].[UserID]		= @UserID,		
		[dbo].[MST_QuizWiseQuestions].[Modified]	= GetDate()
	Where [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
End

----------	Delete	----------


Create OR Alter Procedure [dbo].[PR_MST_QuizWiseQuestions_Delete]
	@QuizWiseQuestionsID INT
As
Begin
	Delete 
	From [dbo].[MST_QuizWiseQuestions]
	Where [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
End

[dbo].[PR_MST_QuizWiseQuestions_Delete] 3
[dbo].[PR_MST_QuizWiseQuestions_Delete] 4
[dbo].[PR_MST_QuizWiseQuestions_Delete] 5
[dbo].[PR_MST_QuizWiseQuestions_Delete] 6
[dbo].[PR_MST_QuizWiseQuestions_Delete] 8
[dbo].[PR_MST_QuizWiseQuestions_Delete] 9

select * from [dbo].[MST_QuizWiseQuestions]


----------	Select All	----------


Create OR Alter Procedure [dbo].[PR_MST_QuizWiseQuestions_SelectAll]
As
Begin
	--SET NOCOUNT ON;
	Select [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
		   [dbo].[MST_QuizWiseQuestions].[QuizID],
		   [dbo].[MST_QuizWiseQuestions].[QuestionID],	
		   [dbo].[MST_QuizWiseQuestions].[UserID],
		   [dbo].[MST_QuizWiseQuestions].[Created],	
		   [dbo].[MST_QuizWiseQuestions].[Modified],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_Question].[QuestionText]
	From [dbo].[MST_QuizWiseQuestions]
	left join [dbo].[MST_Quiz]
	On [dbo].[MST_QuizWiseQuestions].[QuizID] = [dbo].[MST_Quiz].[QuizID]
	left join [dbo].[MST_User]
	On [dbo].[MST_QuizWiseQuestions].[UserID] = [dbo].[MST_User].[UserID]
	left join [dbo].[MST_Question]
	On [dbo].[MST_QuizWiseQuestions].[QuestionID] = [dbo].[MST_Question].[QuestionID]
End

select * from [dbo].[MST_QuizWiseQuestions]


----------	Select By ID	----------


Create OR Alter Procedure [dbo].[PR_MST_QuizWiseQuestions_SelectByID]
	@QuizWiseQuestionsID INT
As
Begin
	Select [dbo].[MST_QuizWiseQuestions].[QuizID],
		   [dbo].[MST_QuizWiseQuestions].[QuestionID],	
		   [dbo].[MST_QuizWiseQuestions].[UserID],
		   [dbo].[MST_QuizWiseQuestions].[Created],	
		   [dbo].[MST_QuizWiseQuestions].[Modified]
	From [dbo].[MST_QuizWiseQuestions]
	Where [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID] = @QuizWiseQuestionsID
End


-------------- Search QuizWiseQuestions ---------------


-- Quiz
CREATE OR ALTER PROCEDURE [dbo].[PR_MST_QuizWiseQuestions_SelectByQuizID]
    @QuizID INT
AS
BEGIN
    SELECT [dbo].[MST_QuizWiseQuestions].[QuizWiseQuestionsID],
		   [dbo].[MST_QuizWiseQuestions].[QuizID],
		   [dbo].[MST_Quiz].[QuizName],
		   [dbo].[MST_Quiz].[TotalQuestions],
		   [dbo].[MST_Quiz].[QuizDate],
		   [dbo].[MST_QuizWiseQuestions].[QuestionID],
		   [dbo].[MST_Question].[QuestionId],
		   [dbo].[MST_Question].[QuestionText],
		   [dbo].[MST_Question].[OptionA],
		   [dbo].[MST_Question].[OptionB],
		   [dbo].[MST_Question].[OptionC],
		   [dbo].[MST_Question].[OptionD],
		   [dbo].[MST_Question].[CorrectOption],
		   [dbo].[MST_Question].[QuestionMarks],
		   [dbo].[MST_Question].[IsActive],
		   [dbo].[MST_QuizWiseQuestions].[UserID],
		   [dbo].[MST_User].[UserName],
		   [dbo].[MST_User].[Password],
		   [dbo].[MST_User].[Email],
		   [dbo].[MST_User].[Mobile],
		   [dbo].[MST_User].[isActive],
		   [dbo].[MST_User].[isAdmin],
		   [dbo].[MST_QuizWiseQuestions].[Created],
		   [dbo].[MST_QuizWiseQuestions].[Modified]
	FROM [dbo].[MST_QuizWiseQuestions] 
	INNER JOIN [dbo].[MST_User] 
	ON [dbo].[MST_QuizWiseQuestions].[UserID] = [dbo].[MST_User].[UserID]
	INNER JOIN [dbo].[MST_Question] 
	ON [dbo].[MST_QuizWiseQuestions].[QuestionID] = [dbo].[MST_Question].[QuestionID]
	INNER JOIN [dbo].[MST_Quiz] 
	ON [dbo].[MST_QuizWiseQuestions].[QuizID] = [dbo].[MST_Quiz].[QuizID]
    WHERE [dbo].[MST_QuizWiseQuestions].[QuizID] = @QuizID
END



-------------------	Dropdown SPs -------------------

--------- This is for filling all quizzes ---------

CREATE OR ALTER PROCEDURE PR_MST_Quiz_DropdownForQuiz
AS
BEGIN
	SELECT
		[DBO].[MST_Quiz].[QuizID],
		[DBO].[MST_Quiz].[QuizName]
	FROM [DBO].[MST_Quiz]
	ORDER BY [DBO].[MST_Quiz].[QuizName]
END


-------- This is for filling all questions ---------

CREATE OR ALTER PROCEDURE PR_MST_question_DropdownForQuestion
AS
BEGIN
	SELECT
		[DBO].[MST_Question].[QuestionID],
		[DBO].[MST_Question].[QuestionText]
	FROM [DBO].[MST_Question]
	ORDER BY [DBO].[MST_Question].[QuestionText]
END


-------- This is for filling all Users ---------

CREATE OR ALTER PROCEDURE PR_MST_User_DropdownForUser
AS
BEGIN
	SELECT
		[DBO].[MST_User].[UserID],
		[DBO].[MST_User].[UserName]
	FROM [DBO].[MST_User]
	ORDER BY [DBO].[MST_User].[UserName]
END


-------- This is for filling all QuestionLevel ---------

CREATE OR ALTER PROCEDURE PR_MST_QuestionLevel_DropdownForQuestionLevel
AS
BEGIN
	SELECT
		[DBO].[MST_QuestionLevel].[QuestionLevelID],
		[DBO].[MST_QuestionLevel].[QuestionLevel]
	FROM [DBO].[MST_QuestionLevel]
	ORDER BY [DBO].[MST_QuestionLevel].[QuestionLevel]
END

SELECT * FROM sys.foreign_keys WHERE referenced_object_id = OBJECT_ID('MST_QuizWiseQuestions');
