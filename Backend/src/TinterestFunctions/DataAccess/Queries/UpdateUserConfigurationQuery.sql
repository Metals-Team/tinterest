UPDATE dbo.UserConfiguration
SET
	Langitude = @Langitude,
	Longitude = @Longitude,
	Radius = @Radius,
	FacebookId = @FacebookId
WHERE Id = @Id
