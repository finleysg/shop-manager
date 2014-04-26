USE [EnfieldMaster]
GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ServiceTotalsView]'))
DROP VIEW [dbo].[ServiceTotalsView]
GO

CREATE view ServiceTotalsView
as

select
	 ServiceTotalsViewId = row_number() over (order by a.AccountName, convert(varchar, i.CompleteDate, 101) desc)
	,a.AccountName
	,convert(varchar, i.CompleteDate, 101) as CompleteDate
	,i.LocationId
	,sum(s.Rate) as Total
	,count(*) as Cars
from Invoice i
join [Service] s on i.InvoiceId = s.InvoiceId
join Account a on i.AccountId = a.AccountId
where a.AccountTypeId = 1
and i.CompleteDate is not null
group by
	 a.AccountName
	,convert(varchar, i.CompleteDate, 101)
	,i.LocationId

union

select
	 ServiceTotalsViewId = row_number() over (order by convert(varchar, i.CompleteDate, 101) desc)
	,'PRIVATE ACCOUNT' as AccountName
	,convert(varchar, i.CompleteDate, 101) as CompleteDate
	,i.LocationId
	,sum(s.Rate) as Total
	,count(*) as Cars
from Invoice i
join [Service] s on i.InvoiceId = s.InvoiceId
join Account a on i.AccountId = a.AccountId
where a.AccountTypeId = 2
and i.CompleteDate is not null
group by
	 convert(varchar, i.CompleteDate, 101)
	,i.LocationId

GO


