USE EnfieldMaster
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'DF_StockNumberHistory_ModifyUser') AND type = 'D')
BEGIN
ALTER TABLE dbo.StockNumberHistory DROP CONSTRAINT DF_StockNumberHistory_ModifyUser
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'DF_StockNumberHistory_ModifyDate') AND type = 'D')
BEGIN
ALTER TABLE dbo.StockNumberHistory DROP CONSTRAINT DF_StockNumberHistory_ModifyDate
END

GO

/****** Object:  Table dbo.StockNumberHistory    Script Date: 01/17/2012 14:31:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.StockNumberHistory') AND type in (N'U'))
DROP TABLE dbo.StockNumberHistory
GO

/****** Object:  Table dbo.StockNumberHistory    Script Date: 01/17/2012 14:31:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE dbo.StockNumberHistory(
	StockNumberHistoryId int IDENTITY(1,1) NOT NULL,
	StockNumber varchar(20) NOT NULL,
	InvoiceId int NOT NULL,
	Note varchar(250) NOT NULL,
	ModifyUser varchar(30) NOT NULL,
	ModifyDate datetime NOT NULL
)

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE dbo.StockNumberHistory ADD  CONSTRAINT DF_StockNumberHistory_ModifyUser  DEFAULT ('system') FOR ModifyUser
GO

ALTER TABLE dbo.StockNumberHistory ADD  CONSTRAINT DF_StockNumberHistory_ModifyDate  DEFAULT (getdate()) FOR ModifyDate
GO

/****** Object:  Index PK_StockNumberHistory    Script Date: 01/17/2012 14:34:26 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.StockNumberHistory') AND name = N'PK_StockNumberHistory')
DROP INDEX PK_StockNumberHistory ON dbo.StockNumberHistory WITH ( ONLINE = OFF )
GO

/****** Object:  Index PK_StockNumberHistory    Script Date: 01/17/2012 14:34:26 ******/
CREATE UNIQUE NONCLUSTERED INDEX PK_StockNumberHistory ON dbo.StockNumberHistory 
(
	StockNumberHistoryId ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

/****** Object:  Index IX_StockNumberHistory_StockNumber    Script Date: 01/17/2012 14:34:40 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.StockNumberHistory') AND name = N'IX_StockNumberHistory_StockNumber')
DROP INDEX IX_StockNumberHistory_StockNumber ON dbo.StockNumberHistory WITH ( ONLINE = OFF )
GO

/****** Object:  Index IX_StockNumberHistory_StockNumber    Script Date: 01/17/2012 14:34:40 ******/
CREATE CLUSTERED INDEX IX_StockNumberHistory_StockNumber ON dbo.StockNumberHistory 
(
	StockNumber ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

/****** Object:  Index IX_StockNumberHistory_InvoiceId    Script Date: 01/17/2012 14:34:52 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'dbo.StockNumberHistory') AND name = N'IX_StockNumberHistory_InvoiceId')
DROP INDEX IX_StockNumberHistory_InvoiceId ON dbo.StockNumberHistory WITH ( ONLINE = OFF )
GO

/****** Object:  Index IX_StockNumberHistory_InvoiceId    Script Date: 01/17/2012 14:34:52 ******/
CREATE NONCLUSTERED INDEX IX_StockNumberHistory_InvoiceId ON dbo.StockNumberHistory 
(
	InvoiceId ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO

