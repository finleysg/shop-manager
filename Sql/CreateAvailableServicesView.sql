USE [EnfieldMaster]
GO

/****** Object:  View [dbo].[AvailableServicesView]    Script Date: 02/25/2012 12:03:34 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[AvailableServicesView]'))
DROP VIEW [dbo].[AvailableServicesView]
GO

USE [EnfieldMaster]
GO

/****** Object:  View [dbo].[AvailableServicesView]    Script Date: 02/25/2012 12:03:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[AvailableServicesView]
as

select
	 s.AccountTypeServiceId as AvailableServicesViewId
	,a.AccountTypeId
	,a.Description as AccountTypeDescription
	,t.ServiceTypeId
	,t.Description as ServiceTypeDescription
	,s.IsActive
from AccountTypeService s 
join ServiceType t on s.ServiceTypeId = t.ServiceTypeId
join AccountType a on s.AccountTypeId = a.AccountTypeId


GO

