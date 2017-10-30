create procedure sp_movie_rating
(
	@userid varchar(max),
	@Movie_Id varchar(max),
	@r int=0
)
as
if exists(select 1 from Movie_Rating where UserId=@userid and Movie_Id=@Movie_Id)
begin
   update Movie_Rating
   set Rating=Rating+@r 
   where UserId=@userid and Movie_Id=@Movie_Id 
end
else
begin
  insert into Movie_Rating values(NEWID(),CURRENT_TIMESTAMP,@Movie_Id,null,@userid,1)

end