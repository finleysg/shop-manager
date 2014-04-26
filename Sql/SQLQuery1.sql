create view InvoiceView as

select
	 i.InvoiceId
	,l.LocationName
	,a.AccountName
	,i.ReceiveDate
	,i.CompleteDate
	,i.Year
	,i.Make
	,i.Model
	,i.Color
	,i.StockNumber
	,i.PurchaseOrderNumber
	,i.IsComplete
	,i.IsPaid
	,SUM(s.Rate) as Total
from Invoice i
join Location l on i.LocationId = l.LocationId
join Account a on i.AccountId = a.AccountId
join Service s on i.InvoiceId = s.InvoiceId
group by
	 i.InvoiceId
	,l.LocationName
	,a.AccountName
	,i.ReceiveDate
	,i.CompleteDate
	,i.Year
	,i.Make
	,i.Model
	,i.Color
	,i.StockNumber
	,i.PurchaseOrderNumber
	,i.IsComplete
	,i.IsPaid