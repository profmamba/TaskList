IF NOT EXISTS (SELECT TOP 1 * FROM dbo.TaskTypes)
BEGIN
	INSERT INTO dbo.TaskTypes 
		(TaskTypeId, TaskTypeName)
	VALUES
		(1, 'Work'),
		(2, 'Scheduled Meeting'),
		(3, 'Social')
END