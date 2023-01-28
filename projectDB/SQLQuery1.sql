use master
go
drop database IMDB10
go

create database IMDB18
go
use IMDB18
go


create table genre
(
id int IDENTITY(1,1) primary key,
genre varchar(50)
)

insert into genre values('action')
insert into genre values('adventure')
insert into genre values('romance')
insert into genre values('drama')
insert into genre values('crime')
insert into genre values('thriller')
insert into genre values('history')
insert into genre values('sci-fi')

create table member 
(
pic text,
id  int  primary key,
name varchar(100),
password_ nvarchar(20),
age nvarchar(10),
gender varchar(50) check (gender ='male' OR gender ='female'),
iscritic char check (iscritic ='Y' OR iscritic ='N'),
firstchoice varchar(50),
secondchoice varchar(50)

);


create table movie 
(
id int IDENTITY(1,1) primary key,
age_rating varchar (50),
name varchar(100),
[description] varchar(500),
release_date varchar(50) ,
rating float,
genre int FOREIGN KEY REFERENCES genre(id), 
duration varchar(50),
[Language] varchar(50),
budget varchar(100),
box_office varchar(50),
producer varchar(100),
picture text
);

create table actors 
(
id int IDENTITY(1,1)  primary key,
name varchar(100),
age int ,
gender varchar(50) check (gender ='male' OR gender ='female'),
about varchar(max), 
awards varchar(100),
picture text
);




create table directors 
(
id int  IDENTITY(1,1) primary key,
name varchar(100),
age int ,
gender varchar(50) check (gender ='male' OR gender ='female'),
about varchar(max), 
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


           --ADD MOVIE
	
	
go
create procedure addMovie
@name varchar(50),
@age_rating varchar (50),               
@desc varchar(200),
@release_date varchar(50),
@rating float,
@genre int,
@duration VARCHAR(50),
@lang varchar(50),
@budget varchar(50),
@box varchar(50),
@producer varchar(50),
@pic text 
as 

begin

IF ( @name = '' or @desc = '' or @genre = '' or  @lang='' )
begin
print ('data incorrect')
end

else
BEGIN 
insert into movie values ( @age_rating  , @name, @desc, @release_date , @rating , @genre ,@duration,@lang,@budget , @box ,@producer,@pic )
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








			--Cheking Search

--drop procedure CheckSearch
go
create procedure CheckSearch
@name varchar(50),
@status int output
as 
begin
IF exists(select * from directors d where d.name=@name)
begin
set @status=1
print ('Director Found!!!')
end

else IF exists(select * from actors a where a.name=@name)
begin
set @status=2
print ('Actor Found!!!')
end

else IF exists(select * from genre g where g.genre=@name)
begin
set @status=3
print ('Genre Found!!!')
end

else IF exists(select * from movie m where m.name=@name)
begin
set @status=4
print ('Movie Found!!!')
end

else
BEGIN 
set @status =0
print ('Not found!!!')
end
end










                    --Search BY Director

--drop procedure SearchByDirector

go
create procedure SearchByDirector
@input varchar(50)
as 
begin
    select * from movie where movie.name in( 
    select  movie.name
	from movie
	join CastandCrew on movie.id = CastandCrew.mov_id
	join directors on directors.id = CastandCrew.direc_id
	where directors.name=@input)
end





--Search by Movie

--drop procedure SearchByMovie

go
create procedure SearchByMovie
@input varchar(50)
as 
begin
    select * from movie where movie.name=@input
end






 --Search BY Actor


go
create procedure SearchByActor
@input varchar(50)
as 
begin
    select * from movie where movie.name in( 
    select  movie.name
	from movie
	join CastandCrew on movie.id = CastandCrew.mov_id
	join actors on actors.id = CastandCrew.act_id
	where actors.name=@input)
end








--Search BY Genre
			 


go
create procedure SearchByGenre
@input varchar(50)
as 
begin
    select * from movie where movie.name in( 
    select  movie.name
	from movie
	join genre on genre.id = movie.genre
	where genre.genre=@input)
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





            --ADD ACTOR
	
	
go
create procedure addActor
@name varchar (50),
@age int,
@gender varchar(50),
@about varchar(200),
@awards varchar(100),
@pic text
as 

begin

IF ( @name = '' or @age  < 3 or @gender = '' or @about = '' )
begin
print ('data incorrect')
end

else
BEGIN 
insert into actors values (@name, @age ,@gender, @about,@awards,@pic)
print ('New Actor ADDED')
end

end




          --SEARCH MOVIE by GENRE
go
create procedure BYGENRE
@input varchar(20)
as 
begin
    select *
	from movie
	where genre=@input
end


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




				   
				   --Show actor
go
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
go
Create Procedure SearchMoviebyActorandDirector
@Actorname varchar(100),
@Directorname varchar(100)
As 
Begin

if(@Actorname in(Select name from actors) and @Directorname in(Select name from directors))
Begin
  if exists (Select mov_id from CastandCrew join actors on CastandCrew.act_id=actors.id where actors.name=@Actorname)
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









                                   --SIGN UP PROCEADURE




go
create procedure SIGNUP
@pic text,
@id nvarchar(10) ,
@name varchar (50),
@password_ nvarchar(20),
@age int,
@gender varchar(50),
@iscritic char,
@firstchoice varchar(20),
@secondchoice varchar(20),
@status int output

as 
begin

IF (@name = '' or len(@password_) < 6 or @age  < 3 or @gender = '' or @iscritic = '' or @firstchoice = '' or @secondchoice ='')
begin
print ('data incorrect')
set @status=0 
end

else if exists (select * From member where member.id=@id)
Begin
set @status=0 
End

else

BEGIN 
insert into member values (@pic,@id,@name,@password_,@age ,@gender, @iscritic , @firstchoice , @secondchoice )
print ('MEMBER ADDED')
 set @status=1;
end
end


                            -- LOGIN PROCEADURE 

--drop procedure login

go
create procedure login
@id nvarchar(10),
@password_ nvarchar(20),
@status int output
as 
begin
IF exists(select * from member M where M.id=@id and @password_= M.password_ )
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



                      -- VIEW MEMBERS
go
Create Procedure ViewMember
As
Begin
Select * From member
End

go
Create Procedure ViewMember1
@id int
As
Begin
Select * From member where member.id=@id
End



                           
						         --Give Review--
								 
go
create procedure GiveReview
@review varchar (500),
@memid int,
@ratin float,
@movid int
as 
begin
IF exists(select id from  movie where @movid=movie.id)
         
begin

 insert into review values ( @movid,@memid,@ratin,@review) 
   
end
else
BEGIN 
print ('Movie not Found')
end
end


					         --Delete Review--
								 
go
create procedure DeleteReview
@memid int,
@movid int
as 
begin
IF exists(select id from  movie where @movid=movie.id)
         
begin

 delete from review where @movid =  review.mov_id and @memid=review.mem_id 
   
end
else
BEGIN 
print ('Movie not Found')
end
end




          --Get Review
		  
		  drop procedure GetReview
go
create procedure GetReview
@movid int
as
begin
Select AVG(review.rating) as rat
from review where review.mov_id=@movid
group by review.rating
end

       

          --Get Review2
		  
go
create procedure GetReview2
@movid int
as
begin
Select review.rating,review.comments,member.name
from review join member on member.id = review.mem_id 
where review.mov_id=@movid 
end
	   
	   
	   
	      --SUGGESTION
		  
		  
go
create procedure Sugesstions
@id int 
as 
begin
    select movie.name, movie.id ,movie.picture
	from member join genre on member.firstchoice = genre.genre join movie on genre.id = movie.genre
	where member.id = @id
end




       --WATCHED LIST
go 
create procedure AddToWatchedList
@member_id int,
@movie_id int

as
begin

if exists (select * from movie where movie.id = @movie_id ) 
begin
print ('Added To Watch Later')
insert into Watched_List values (@movie_id,@member_id, getdate())
end
else

begin
print ('No Such Movie Found')
end
end



------Show Movie
go
Create Procedure ViewMovie
@id int
As
Begin
Select * From movie where movie.id=@id
End 



select *from genre



insert into movie values('pg-13','The Pursuit of Happyness','Chris Gardner takes up an unpaid internship in a brokerage firm after he loses his lifes earnings selling a product he invested in. His wife leaves him and he is left with the custody of his son.','2006',8,4,'1h 57min','English','55,000,000',' $307,127,625','WB','https://www.gstatic.com/tv/thumb/v22vodart/162523/p162523_v_v8_at.jpg')
insert into movie values('R','Fight Club','An insomniac office worker and a devil-may-care soapmaker form an underground fight club that evolves into something much, much more.','1999',8,4,'2h 19min','English','$63,000,000','101,187,50','WB','https://m.media-amazon.com/images/M/MV5BMmEzNTkxYjQtZTc0MC00YTVjLTg5ZTEtZWMwOWVlYzY0NWIwXkEyXkFqcGdeQXVyNzkwMjQ5NzM@._V1_.jpg')
insert into movie values('R','Shutter Island','In 1954, a U.S. Marshal investigates the disappearance of a murderer who escaped from a hospital for the criminally insane.','2010',8,5,'2h 18min','English','55,000,000',' $307,127,625','WB','https://m.media-amazon.com/images/M/MV5BYzhiNDkyNzktNTZmYS00ZTBkLTk2MDAtM2U0YjU1MzgxZjgzXkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_.jpg')
insert into movie values('pg-13','Mission:Impossible-Fallout','Ethan Hunt and his IMF team, along with some familiar allies, race against time after a mission gone wrong.','2018',7,1,'2h 27min','English','55,000,000',' $307,127,625','WB','https://m.media-amazon.com/images/M/MV5BNjRlZmM0ODktY2RjNS00ZDdjLWJhZGYtNDljNWZkMGM5MTg0XkEyXkFqcGdeQXVyNjAwMjI5MDk@._V1_.jpg')
insert into movie values('pg-13','Avengers: Endgame','After the devastating events of Avengers: Infinity War (2018), the universe is in ruins. With the help of remaining allies, the Avengers assemble once more in order to reverse Thanos actions and restore balance to the universe','2019',8,1,'3h 1min','English','55,000,000',' $307,127,625','WB','https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_.jpg')




insert into actors values ('Edward Norton',50,'male','Edward Harrison Norton is an American actor and filmmaker. He has received multiple awards and nominations, including a Golden Globe Award and three Academy Award nominations. Born in Massachusetts and raised in Maryland, Norton was drawn to theatrical productions at local venues as a child.','Golden Globe Award for Best Supporting Actor – Motion Picture','https://www.biography.com/.image/t_share/MTIwNjA4NjM0MDI5OTAxMzI0/edward-norton-9542130-1-402.jpg')

insert into actors values ('Robert Downey, Jr.',55,'male','Robert John Downey Jr. is an American actor, producer, and singer. His career has been characterized by critical and popular success in his youth, followed by a period of substance abuse and legal troubles, before a resurgence of commercial success in middle age.','Golden Globe Award for Best Supporting Actor – Motion Picture','https://www.pinkvilla.com/files/iron-man-star-robert-downey-jr-best-film.jpg')

insert into actors values ('Leonardo DiCaprio',45,'male','Leonardo Wilhelm DiCaprio is an American actor, producer, and environmentalist. He has often played unconventional parts, particularly in biopics and period films. As of 2019, his films have earned US$7.2 billion worldwide, and he has placed eight times in annual rankings of the worlds highest-paid actors.','Golden Globe Award for Best Supporting Actor – Motion Picture','https://d17zbv0kd7tyek.cloudfront.net/wp-content/uploads/2015/06/leonardo-dicaprio-fb.jpg')


insert into actors values ('Will Smith',51,'male','Willard Carroll Smith Jr. is an American actor and rapper. In April 2007, Newsweek called him "the most powerful actor in Hollywood". Smith has been nominated for five Golden Globe Awards and two Academy Awards, and has won four Grammy Awards.','Golden Globe Award for Best Supporting Actor – Motion Picture','https://thumbor.forbes.com/thumbor/fit-in/416x416/filters%3Aformat%28jpg%29/https%3A%2F%2Fspecials-images.forbesimg.com%2Fimageserve%2F5b4501254bbe6f1becf1e1a5%2F0x0.jpg%3Fbackground%3D000000%26cropX1%3D0%26cropX2%3D2995%26cropY1%3D58%26cropY2%3D3055')


insert into actors values ('Tom Cruise',57,'male','DescriptionThomas Cruise Mapother IV is an American actor and producer. He has received several accolades for his work, including three Golden Globe Awards and three nominations for Academy Awards. With a net worth of $570 million as of 2020, he is one of the highest-paid actors in the world.','Golden Globe Award for Best Supporting Actor – Motion Picture','https://cdn.i-scmp.com/sites/default/files/styles/768x768/public/d8/images/methode/2020/05/06/51ecae48-8f18-11ea-a674-527cfdef49ee_image_hires_061441.JPG?itok=7duE1zu2&v=1588716889')


insert into actors values ('Scarlett Johansson',35,'female','Scarlett Ingrid Johansson is an American actress and singer. The worlds highest-paid actress since 2018, she has made multiple appearances in the Forbes Celebrity 100. Her films have grossed over $14.3 billion worldwide, making Johansson the ninth-highest-grossing box office star of all time.','Golden Globe Award for Best Supporting Actor – Motion Picture','https://www.biography.com/.image/t_share/MTE4MDAzNDEwNzkxOTI1MjYy/scarlett-johansson-13671719-1-402.jpg')


select * from actors


------------- Display Actor

go
Create Procedure DisplayActor
@id int
As
Begin
Select * From actors where actors.id=@id
End 



------------- Display Director

go
Create Procedure DisplayDirector
@id int
As
Begin
Select * From directors where directors.id=@id
End 




insert into directors values ('Christopher Nolan',49,'male','Christopher Edward Nolan CBE is a British-American filmmaker known for making personal, distinctive films within the Hollywood mainstream. His ten films have grossed over US$4.7 billion worldwide and garnered a total of 34 Oscar nominations and ten wins.','Golden Globe Award for Best Supporting Actor – Motion Picture')


insert into CastandCrew values (2,1,1)

insert into CastandCrew values (1,6,1)
insert into CastandCrew values (5,2,1)
insert into CastandCrew values (3,3,1)
insert into CastandCrew values (4,4,1)

insert into CastandCrew values (5,5,1)

select *from actors
select *from movie

select * from movie

select * from genre




--------------- Get Cast--------------

go
Create Procedure getCast
@id int
As
Begin
Select actors.id, directors.id as did , actors.name , actors.picture from CastandCrew
join actors on CastandCrew.act_id = actors.id join directors on directors.id=CastandCrew.direc_id join movie on movie.id=CastandCrew.mov_id
where CastandCrew.mov_id = @id
End  


--------------- Get Watched List--------------

go
Create Procedure getWatchedList
@id int
As
Begin
Select movie.name , movie.id , movie.picture from Watched_List join movie on movie.id=Watched_List.mov_id
where Watched_List.mem_id= @id
End  

--------------- Get WatchLater--------------

go
Create Procedure getWatchLater
@id int
As
Begin
Select movie.name , movie.id , movie.picture from Watch_Later join movie on movie.id=Watch_Later.mov_id
where Watch_Later.mem_id= @id
End  



insert into Watched_List values (1,1999,GETDATE())
insert into Watch_Later values (3,1999)




 --add to watch later
go
Create Procedure WatchLateradd
@id int,
@movid int
As
Begin
insert into Watch_Later values (@movid,@id)
End


 --add to watched list
go
Create Procedure Watchedadd
@id int,
@movid int
As
Begin
insert into Watched_List values (@movid,@id,GETDATE())
End




 --remove from watch later
go
Create Procedure WatchLaterremove
@id int,
@movid int
As
Begin
delete from Watch_Later where Watch_Later.mov_id=  @movid and Watch_Later.mem_id= @id
End


  --remove from watched
go
Create Procedure Watchedremove
@id int,
@movid int
As
Begin
delete from Watched_List where Watched_List.mov_id=  @movid and Watched_List.mem_id= @id
End

  