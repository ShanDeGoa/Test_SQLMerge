CREATE PROCEDURE update_mirror
@playerdata playertype readonly  
AS BEGIN
MERGE players_mirror as TARGET
USING @playerdata AS SOURCE
ON (TARGET.ID =  SOURCE.ID)
WHEN MATCHED

THEN UPDATE SET TARGET.name = SOURCE.name

WHEN NOT MATCHED BY TARGET
THEN INSERT (id,name) VALUES (SOURCE.id, SOURCE.name)

WHEN NOT MATCHED BY SOURCE THEN 
DELETE;
END

GO


Declare @shanmon playertype
insert @shanmon(id,name) values(1,'ekta'),(2,'shanu'),(3,'shanos')


exec update_mirror @playerdata=@shanmon