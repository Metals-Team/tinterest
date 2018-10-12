SELECT dbo.UserConfiguration.*
FROM dbo.UserConfiguration
LEFT JOIN dbo.UserConfiguration_Interests
WHERE InterestId = @InterestId AND 