DELETE StockNumberHistory
GO

INSERT StockNumberHistory (
	 StockNumber
	,InvoiceId
	,Note
	,ModifyUser
	,ModifyDate
)
SELECT
	 i.StockNumber
	,i.InvoiceId
	,h.Note
	,h.ModifyUser
	,h.ModifyDate
FROM Invoice i
JOIN InvoiceHistory h ON i.InvoiceId = h.InvoiceId
WHERE i.StockNumber IS NOT NULL
AND DATALENGTH(i.StockNumber) > 0
ORDER BY i.StockNumber