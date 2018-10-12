SELECT dbo.UserConfiguration.*
FROM dbo.UserConfiguration
LEFT JOIN dbo.UserConfiguration_Interests ON dbo.UserConfiguration.Id = dbo.UserConfiguration_Interests.UserId
WHERE
InterestId = @InterestId AND
@MinLatitude < Latitude AND
@MaxLatitude > Latitude AND
@MinLongitude < Longitude AND
@MaxLongitude > Longitude