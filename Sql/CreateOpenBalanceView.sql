USE [EnfieldMaster]
GO

create view OpenBalanceView
as

select top 100 percent
	 OpenBalanceViewId = row_number() over (order by a.AccountName)
	,a.AccountId
	,a.AccountName
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
where t.Description = 'DEALER'
and i.IsComplete = 1
and i.IsPaid = 0
group by
	 a.AccountId
	,a.AccountName
order by a.AccountName



GO


