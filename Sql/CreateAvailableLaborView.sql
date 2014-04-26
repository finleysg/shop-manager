USE [EnfieldMaster]
GO

/****** Object:  View [dbo].[AvailableLaborView]    Script Date: 02/25/2012 12:02:44 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[AvailableLaborView]'))
DROP VIEW [dbo].[AvailableLaborView]
GO

USE [EnfieldMaster]
GO

/****** Object:  View [dbo].[AvailableLaborView]    Script Date: 02/25/2012 12:02:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[AvailableLaborView]
as

select
	 l.AccountTypeLaborId as AvailableLaborViewId
	,s.AccountTypeServiceId
	,st.ServiceTypeId
	,st.Description as ServiceTypeDescription
	,t.LaborTypeId
	,t.Description as LaborTypeDescription
from AccountTypeService s
join AccountTypeLabor l on s.AccountTypeServiceId = l.AccountTypeServiceId
join ServiceType st on s.ServiceTypeId = st.ServiceTypeId
join LaborType t on l.LaborTypeId = t.LaborTypeId


GO

