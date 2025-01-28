# Procedure using For Insert
```
CREATE procedure insert_user  
@userName varchar(50),  
@userCity varchar(50),  
@userAge  int,  
@country int,  
@state int  
as  
begin  
insert into tbluser (userName,userCity, userAge, country, state)values (@userName,@userCity,@userAge,@country,@state)  
end
```

#-------------------------------------------------#
# Procedure Using For Delete
```
create procedure user_delete  
@userId int  
as  
begin  
  delete from tbluser where userId=@userId  
end
```
#------------------------------------------#
# Procedure Using For Edit
```
create procedure user_edit  
@userId int  
as  
begin  
  select * from tbluser where userId=@userId  
end
