IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeView]'))
DROP VIEW [dbo].[EmployeeView]
GO

CREATE VIEW EmployeeView
AS

SELECT e.EmployeeId AS EmployeeViewId
	  ,e.Name
	  ,e.FirstName
	  ,e.LastName
	  ,e.Rate
	  ,e.IsEmployed
	  ,e.CanLogin
	  ,ISNULL(e.RoleName, 'None') AS RoleName
FROM EnfieldMaster.dbo.Employee e
WHERE e.Name <> '-NONE-'