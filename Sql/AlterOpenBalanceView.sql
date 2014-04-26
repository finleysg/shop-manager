USE [EnfieldMaster]
GO

/****** Object:  View [dbo].[OpenBalanceView]    Script Date: 08/07/2012 10:19:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


alter view [OpenBalanceView]
as

select top 100 percent
	 OpenBalanceViewId = row_number() over (order by a.AccountName)
	,a.AccountId
	,a.AccountName
	,t.Description as AccountType
	,sum(sub.Balance) as BalanceDue
from Invoice i
join Account a on i.AccountId = a.AccountId
join AccountType t on a.AccountTypeId = t.AccountTypeId
join (
	select InvoiceId, sum(Rate) as Balance
	from [Service]
	group by InvoiceId
	having sum(Rate) > 0
) sub on i.InvoiceId = sub.InvoiceId
where i.IsComplete = 1
and i.IsPaid = 0
group by
	 a.AccountId
	,a.AccountName
	,t.Description
order by a.AccountName




GO


