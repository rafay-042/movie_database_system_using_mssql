use master
go
drop database connectivity
go
Create Database connectivity
go
use connectivity
go
Create Table Users
(
   userId nvarchar(50) primary key,
   [password] nvarchar(50) NOT NULL,
   dateOfBirth date	
)

go

Create Procedure UserSignupProc @userId nvarchar(50), @password nvarchar(50), @dateOfBirth date, @output int OUTPUT
As
Begin

	if exists (select * From users where userId=@userId)
	Begin
	set @output=0 --user id is not unique
	return
	End

    Insert into Users values (@userId, @password, @dateOfBirth)
	set @output=1 --signup successful

End

go
Create Procedure UserLoginProc @userId nvarchar(50), @password nvarchar(50), @output int OUTPUT
As
Begin

	if  not exists (select * From users where userId=@userId AND [password]=@password)
	Begin
	set @output=0 --user id and password combination incorrect
	return
	End

	set @output=1 --login successful
End

go

Create Procedure ViewUsers 
As
Begin
Select * From Users
End

go
declare @out int
exec UserSignupProc 'peter', '1234', '2-13-2001', @out OUT
Select @out

go
declare @out int
exec UserSignupProc 'john', 'abcd', '2-13-2001', @out OUT
Select @out

go
declare @out int
exec UserLoginProc 'peter', '1234', @out  out
Select @out

go
Create Procedure ViewUser @userId nvarchar(50)
As
Begin
Select * From Users where userId=@userId
End

go
select * From Users