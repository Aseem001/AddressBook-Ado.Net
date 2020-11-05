create procedure dbo.spAddContactDetails
(	
	@firstname	varchar(100),
	@lastname varchar(100),
	@address varchar(500),
	@city varchar(50),
	@state varchar(50),
	@zip int,
	@phoneNo bigint,
	@email varchar(250),
	@addressbookname varchar(100),
	@contactType varchar(100)		
	)
	as begin
	Insert into address_book values(@firstname,@lastname,@address,@city,@state,@zip,@phoneNo,@email,@addressbookname,@contactType)
	End
