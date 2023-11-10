SELECT [aspnet_Users].UserName, [BodyMessage], [TypeMessage], [_Date] AS CreatedAt 
FROM [hs_Messages] 
Inner Join
[aspnet_Users] On [aspnet_Users].UserId = [hs_Messages].UserId