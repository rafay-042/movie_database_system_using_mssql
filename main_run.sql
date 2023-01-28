create database FMDB1
use FMDB1
go


create table genre
(
id int IDENTITY(1,1) primary key,
genre varchar(50),
)


create table member 
(
id int IDENTITY(1,1) primary key,
name varchar(100),
password nvarchar(20),
age int ,
gender char check (gender ='M' OR gender ='F'),
critic char(2) check (critic ='Y' OR critic ='N'),
[fav_genre] varchar(50),
[fav_genre2] varchar(50)

);


create table movie 
(
id int IDENTITY(1,1) primary key,
age_rating varchar (50),
name varchar(100),
[description] varchar(500),
release_date date ,
rating int,
genre int FOREIGN KEY REFERENCES genre(id), 
duration varchar(50),
[Language] varchar(50),
budget varchar(100),
box_office varchar(50),
producer varchar(100),
);

create table actors 
(
id int IDENTITY(1,1)  primary key,
name varchar(100),
age int ,
gender varchar(50) check (gender ='male' OR gender ='female'),
about varchar(500), 
awards varchar(100),
);




create table directors 
(
id int  IDENTITY(1,1) primary key,
name varchar(100),
age int ,
gender varchar(50) check (gender ='male' OR gender ='female'),
about varchar(500), 
awards varchar(100),
);




create table review 
(
mov_id int FOREIGN KEY REFERENCES movie(id),
mem_id int FOREIGN KEY REFERENCES member(id),
rating float ,
comments varchar(500), 

);


create table Watched_List 
(
mov_id int FOREIGN KEY REFERENCES movie(id),
mem_id int FOREIGN KEY REFERENCES member(id),
[date] date,

);


create table CastandCrew
(
mov_id int FOREIGN KEY REFERENCES movie(id),
act_id int FOREIGN KEY REFERENCES actors(id),
direc_id int FOREIGN KEY REFERENCES directors(id),
);


create table Watch_Later 
(
mov_id int FOREIGN KEY REFERENCES movie(id),
mem_id int FOREIGN KEY REFERENCES member(id),


);


create table Suggestion 
(
mov_id int FOREIGN KEY REFERENCES movie(id),
mem_id int FOREIGN KEY REFERENCES member(id),


);




                            --SIGNUP
go
create procedure SIGNUP
@name varchar (50),
@password nvarchar(20),
@age int,
@gender char,
@iscritic char,
@firstchoice varchar(20),
@secondchoice varchar(20)

as 
begin

IF (@name = '' or len(@password) < 6 or @age  < 3 or @gender = '' or @iscritic = '' or @firstchoice = '' or @secondchoice ='')
begin
print ('data incorrect')
end
else

BEGIN 

insert into member values (@name,@password , @age ,@gender, @iscritic , @firstchoice , @secondchoice )
print ('MEMBER ADDED')

end
end


select * from member




                                --LOGIN

go
create procedure login
@name varchar (50),
@password nvarchar(20),
@status int output
as 
begin
IF exists(select * from member M where @name=M.name and @password= M.password )
begin
set @status=1
print ('login successfully')
end
else

BEGIN 
set @status =0
print ('MEMBER not Found')
end
end

declare @myoutput int


           --ADD MOVIE
	
go
create procedure addMovie
@name varchar(50),
@age_rating varchar (50),               
@desc varchar(200),
@release_date date,
@rating int,
@genre int,
@duration VARCHAR(50),
@lang varchar(50),
@budget varchar(50),
@box varchar(50),
@producer varchar(50)
as 

begin

IF ( @name = '' or @desc = '' or @genre = '' or  @lang='' )
begin
print ('data incorrect')
end

else
BEGIN 
insert into movie values ( @age_rating  , @name, @desc, @release_date , @rating , @genre ,@duration,@lang,@budget , @box ,@producer )
print ('New Movie ADDED')
end

end






            --WATCH LATER--

go 
create procedure AddToWatchLater
@member_id int,
@movie_id int

as
begin

if exists (select * from movie where movie.id = @movie_id ) 
begin
print ('Added To Watch Later')
insert into Watch_Later values (@movie_id,@member_id)
end
else

begin
print ('No Such Movie Found')
end
end




                    --Search BY Director

go
create procedure SearchByDireector
@input varchar(20)
as 
begin


    select  movie.name
	from movie
	join CastandCrew on movie.id = CastandCrew.mov_id
	join directors on directors.id = CastandCrew.direc_id
	where directors.name=@input
end


                     --Show By Director




go 
create procedure ShowDirector
@input varchar(50)
as 
begin
    
	if exists (select * from directors where directors.name = @input)
	begin 
	select *from directors
	end
else
begin
 print('No Such Director Exists')
 end
 end

 exec ShowDirector
 @input = 'frank darabont'




            --ADD ACTOR
	

go
create procedure addActor
@name varchar (50),
@age int,
@gender varchar(50),
@about varchar(200),
@awards varchar(100)
as 

begin

IF ( @name = '' or @age  < 3 or @gender = '' or @about = '' )
begin
print ('data incorrect')
end

else
BEGIN 
insert into actors values (@name, @age ,@gender, @about,@awards)
print ('New Actor ADDED')
end

end



          --SEARCH MOVIE by GENRE
go
create procedure BYGENRE
@input varchar(50)
as 
begin
    select movie.name
	from movie join genre on movie.genre = genre.id
	where genre.genre = @input
end

exec BYGENRE
@input='romance'


         --SHOW MOVIE 


go
create procedure showmovie
@name varchar (100)
as 
begin
IF exists(select * from movie where name=@name )
begin
select * from movie where name=@name
end
else
BEGIN 
print ('Movie not Found')
end
end

exec showmovie
@name = 'Race'



       --choosebyActors


go
create procedure byActors
@actor1 varchar (50),
@actor2 varchar (50)
as 
begin
IF exists(select mov_id from  CastandCrew where @actor1=act_id 
              intersect
          select mov_id from  CastandCrew where @actor2=act_id  )
begin
  select  M.name
  from movie M join ( select mov_id from  CastandCrew where @actor1=act_id 
              intersect
          select mov_id from  CastandCrew where @actor2=act_id ) T on M.id=T.mov_id   
end
else
BEGIN 
print ('Movie not Found')
end
end






							    --Add Director
create procedure AddDirector
@name varchar(100),
@age int ,
@gender varchar(50),
@about varchar(500), 
@awards varchar(100)

AS

begin

if(@name is null or @gender is null or @about is null)
begin 
print('Please enter name,gender,about')
end

else
begin
Insert into directors values(@name,@age,@gender,@about,@awards)
print('New Director Added')
end

end

					 
					 
					 
										 
					 
					 
					 --Search by actor
Create Procedure SearchMoviebyActor
@Actorname varchar(100)
As 
Begin

if(@Actorname in(Select name from actors))
Begin
--if(exist(Select mov_id from CastandCrew join actors on CastandCrew.act_id=actors.id where actors.name=@Actorname))
Select m.id,m.name,m.Language,m.age_rating,m.box_office,m.budget,m.description,m.duration,m.genre from movie as m 
join CastandCrew as c on m.id=c.mov_id
join actors as a on c.act_id=a.id
where a.name=@Actorname
End

else
Begin
print ('No Actor of this name')
end

end
                  
				   
				   
				   --Show actor
Create Procedure ActorData
@Actor varchar(100)
As

Begin

if(@Actor in (Select name from actors))
Begin
Select * from actors where name=@Actor
End

else
Begin
Print('Actor not Found')
End
End





                        --Director/Actor
Create Procedure SearchMoviebyActorandDirector
@Actorname varchar(100),
@Directorname varchar(100)
As 
Begin

if(@Actorname in(Select name from actors) and @Directorname in(Select name from directors))
Begin
--if(exist(Select mov_id from CastandCrew join actors on CastandCrew.act_id=actors.id where actors.name=@Actorname))
Select m.id,m.name,m.Language,m.age_rating,m.box_office,m.budget,m.description,m.duration,m.genre from movie as m 
join CastandCrew as c on m.id=c.mov_id
join actors as a on c.act_id=a.id
join directors as d on c.direc_id=d.id
where a.name=@Actorname and d.name=@Directorname

End

else
Begin
print ('No Actor,Director of this name')
end

end


                             -- ADD REVIEW

create procedure addreview
@movname varchar (100),
@review varchar(500),
@memberid int,
@rating float,
@movid int
as 
begin
IF exists(select * from movie where name=@movname )
begin
insert into review values (@movid , @memberid , @rating ,@review)
end
else
BEGIN 
print ('Movie not Found')
end
end
