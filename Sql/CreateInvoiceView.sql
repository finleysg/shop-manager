/****** Object:  View [dbo].[InvoiceView]    Script Date: 01/22/2012 13:21:04 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceView]'))
DROP VIEW [dbo].[InvoiceView]
GO

create view [dbo].[InvoiceView] as

select
	 i.InvoiceId as InvoiceViewId
	,l.LocationId
	,l.LocationName
	,a.AccountId
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
	,SUM(ISNULL(s.Rate,0)) as Total
from Invoice i
join Location l on i.LocationId = l.LocationId
join Account a on i.AccountId = a.AccountId
left join Service s on i.InvoiceId = s.InvoiceId
group by
	 i.InvoiceId
	,l.LocationId
	,l.LocationName
	,a.AccountId
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
GO


