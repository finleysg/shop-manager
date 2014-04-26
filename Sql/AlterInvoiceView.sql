USE [EnfieldMaster]
GO

/****** Object:  View [dbo].[InvoiceView]    Script Date: 08/11/2012 16:33:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER view [dbo].[InvoiceView] as

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
	,SUM(s.Rate) as Total
	,SUM(s.Rate) * t.TaxRate as Tax
from Invoice i
join Location l on i.LocationId = l.LocationId
join Account a on i.AccountId = a.AccountId
join AccountType t on a.AccountTypeId = t.AccountTypeId
join Service s on i.InvoiceId = s.InvoiceId
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
	,t.TaxRate

GO


