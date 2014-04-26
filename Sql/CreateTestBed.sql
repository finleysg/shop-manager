USE [master]
GO

EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'EnfieldMasterTestBed'
GO
USE [master]
GO
ALTER DATABASE [EnfieldMasterTestBed] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
GO
USE [master]
GO

DROP DATABASE [EnfieldMasterTestBed]
GO

CREATE DATABASE [EnfieldMasterTestBed]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EnfieldMasterTestBed', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\EnfieldMasterTestBed.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'EnfieldMasterTestBed_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\EnfieldMasterTestBed_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [EnfieldMasterTestBed] SET COMPATIBILITY_LEVEL = 110
GO

USE [EnfieldMasterTestBed]
GO

CREATE USER [shopuser] FOR LOGIN [shopuser] WITH DEFAULT_SCHEMA=[dbo]
GO

CREATE ROLE [WebUser]
GO

ALTER ROLE [db_owner] ADD MEMBER [shopuser]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + 'Z'
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )


GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create function [dbo].[GetVehicle] (
    @invoiceId int
)
returns varchar(100)
as
begin

	declare @vehicle varchar(100)
	select @vehicle = 
	(	case isnull([Year], 'null') 
    		when 'null' 
			then '' 
			else [Year] + ' ' 
		end 
		+ case isnull(Color, 'null') 
			when 'null' 
			then '' 
			else Color + ' ' 
		end 
		+ case isnull(Make, 'null') 
			when 'null' 
			then '' 
			else Make + ' ' 
		end 
		+ case isnull(Model, 'null') 
			when 'null' 
			then '' 
			else Model 
		end
	) 
	from Invoice
	where InvoiceId = @invoiceId

	return @vehicle
end




GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Transfer Accounts
CREATE FUNCTION [dbo].[PadString]

(@Seq varchar(16),
@PadWith char(1),
@PadLength int
)

RETURNS varchar(16) AS

BEGIN

declare @curSeq varchar(16)

SELECT @curSeq = ISNULL(REPLICATE(@PadWith, @PadLength - len(ISNULL(@Seq ,0))), '') + @Seq

RETURN @curSeq

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[AccountNumber] [varchar](10) NULL,
	[AccountName] [varchar](100) NOT NULL,
	[AddressLine1] [varchar](100) NULL,
	[AddressLine2] [varchar](100) NULL,
	[City] [varchar](30) NULL,
	[StateCode] [char](2) NULL,
	[PostalCode] [char](5) NULL,
	[Notes] [varchar](500) NULL,
	[ModifyUser] [varchar](30) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountType](
	[AccountTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](30) NOT NULL,
	[TaxRate] [decimal](6, 4) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ModifyUser] [varchar](30) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AccountType] PRIMARY KEY NONCLUSTERED 
(
	[AccountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_AccountType_Description] UNIQUE CLUSTERED 
(
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AccountTypeLabor](
	[AccountTypeLaborId] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeServiceId] [int] NOT NULL,
	[LaborTypeId] [int] NOT NULL,
	[DefaultRate] [decimal](9, 4) NOT NULL,
	[DefaultRateType] [char](1) NOT NULL,
 CONSTRAINT [PK_AccountTypeLabor] PRIMARY KEY CLUSTERED 
(
	[AccountTypeLaborId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountTypeService](
	[AccountTypeServiceId] [int] IDENTITY(1,1) NOT NULL,
	[AccountTypeId] [int] NOT NULL,
	[ServiceTypeId] [int] NOT NULL,
	[DefaultRate] [decimal](12, 4) NOT NULL,
	[DefaultEstimatedTime] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AccountTypeService] PRIMARY KEY CLUSTERED 
(
	[AccountTypeServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[ContactTypeId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[LastName] [varchar](30) NULL,
	[FirstName] [varchar](20) NULL,
	[ContactDetail] [varchar](200) NOT NULL,
	[DoNotify] [bit] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactType](
	[ContactTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](20) NOT NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[ContactTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [ntext] NOT NULL,
 CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[LocationId] [int] NULL,
	[Name] [varchar](30) NOT NULL,
	[FirstName] [varchar](20) NULL,
	[LastName] [varchar](30) NULL,
	[StartDate] [datetime] NULL,
	[Rate] [decimal](4, 3) NOT NULL,
	[IsEmployed] [bit] NOT NULL,
	[RoleName] [varchar](20) NULL,
	[Password] [varbinary](200) NULL,
	[CanLogin] [bit] NULL,
	[Notes] [varchar](200) NULL,
	[ModifyUser] [varchar](30) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeLog](
	[EmployeeLogId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[SignInDate] [datetime] NOT NULL,
	[SignOutDate] [datetime] NULL,
	[LocationId] [int] NULL,
 CONSTRAINT [PK_EmployeeLog] PRIMARY KEY CLUSTERED 
(
	[EmployeeLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[ErrorLogId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NULL,
	[ExceptionType] [varchar](100) NULL,
	[Message] [varchar](800) NOT NULL,
	[StackTrace] [varchar](max) NULL,
	[ModifyUser] [varchar](20) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[ErrorLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[InvoiceTypeId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[ServiceOrderId] [int] NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[CompleteDate] [datetime] NULL,
	[Year] [varchar](4) NULL,
	[Make] [varchar](20) NULL,
	[Model] [varchar](30) NULL,
	[Color] [varchar](20) NULL,
	[VIN] [varchar](20) NULL,
	[StockNumber] [varchar](20) NULL,
	[PurchaseOrderNumber] [varchar](20) NULL,
	[WorkOrderNumber] [varchar](20) NULL,
	[IsComplete] [bit] NOT NULL,
	[IsPaid] [bit] NOT NULL,
	[TaxRate] [decimal](5, 4) NOT NULL,
	[ModifyUser] [varchar](100) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InvoiceType](
	[InvoiceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](20) NOT NULL,
 CONSTRAINT [PK_InvoiceType] PRIMARY KEY CLUSTERED 
(
	[InvoiceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Labor](
	[LaborId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[EmployeeId] [int] NULL,
	[LaborTypeId] [int] NOT NULL,
	[EstimatedRate] [money] NOT NULL,
	[ActualRate] [money] NOT NULL,
	[EstimatedTime] [int] NOT NULL,
	[ActualTime] [int] NOT NULL,
	[LaborDate] [datetime] NOT NULL,
	[ModifyUser] [varchar](30) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Labor] PRIMARY KEY CLUSTERED 
(
	[LaborId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LaborType](
	[LaborTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](30) NOT NULL,
 CONSTRAINT [PK_LaborType] PRIMARY KEY NONCLUSTERED 
(
	[LaborTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_LaborType_Description] UNIQUE CLUSTERED 
(
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [varchar](50) NOT NULL,
	[DefaultAccountId] [int] NULL,
	[StaticIpAddress] [varchar](20) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY NONCLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[LoginAttemptLog](
	[LoginAttemptId] [int] IDENTITY(1,1) NOT NULL,
	[LoginDate] [datetime] NOT NULL,
	[ResultFlag] [bit] NOT NULL,
	[LocationId] [int] NOT NULL,
	[IpAddress] [varchar](20) NULL,
	[UserName] [varchar](20) NULL,
	[Reason] [varchar](200) NULL,
 CONSTRAINT [PK_LoginAttemptLog] PRIMARY KEY CLUSTERED 
(
	[LoginAttemptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[ServiceTypeId] [int] NOT NULL,
	[Rate] [decimal](12, 4) NOT NULL,
	[EstimatedTime] [int] NOT NULL,
	[ServiceDate] [datetime] NOT NULL,
	[ModifyUser] [varchar](30) NOT NULL,
	[ModifyDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_Service_Unique] UNIQUE NONCLUSTERED 
(
	[InvoiceId] ASC,
	[ServiceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ServiceType](
	[ServiceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](30) NOT NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY NONCLUSTERED 
(
	[ServiceTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_ServiceType_Description] UNIQUE CLUSTERED 
(
	[Description] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[StockNumberHistory](
	[StockNumberHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[StockNumber] [varchar](20) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[Note] [varchar](250) NOT NULL,
	[ModifyUser] [varchar](30) NOT NULL,
	[ModifyDate] [datetime] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[EmployeeLogView] AS

SELECT
	 l.EmployeeLogId AS EmployeeLogViewId
	,e.EmployeeId
	,e.Name
	,e.FirstName + ' ' + e.LastName AS FullName
	,l.LocationId
	,l.SignInDate
	,l.SignOutDate
FROM dbo.Employee e
JOIN dbo.EmployeeLog l ON e.EmployeeId = l.EmployeeId
WHERE l.LocationId IS NOT NULL



GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[EmployeeView]
AS

SELECT e.EmployeeId AS EmployeeViewId
	  ,e.Name
	  ,e.FirstName
	  ,e.LastName
	  ,e.Rate
	  ,e.IsEmployed
	  ,e.CanLogin
	  ,ISNULL(e.RoleName, 'None') AS RoleName
FROM EnfieldMaster.dbo.Employee e
WHERE e.Name <> '-NONE-'

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[InvoiceView] as

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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create view [dbo].[OpenBalanceView]
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[ServiceTotalsView]
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
SET ANSI_PADDING ON

GO
CREATE CLUSTERED INDEX [IX_StockNumberHistory_StockNumber] ON [dbo].[StockNumberHistory]
(
	[StockNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_AccountTypeLabor_Service] ON [dbo].[AccountTypeLabor]
(
	[AccountTypeServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Contact_Account] ON [dbo].[Contact]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error]
(
	[Application] ASC,
	[TimeUtc] DESC,
	[Sequence] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Employee_Unique] ON [dbo].[Employee]
(
	[Name] ASC,
	[IsEmployed] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Invoice_Account] ON [dbo].[Invoice]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Invoice_ReceiveDate] ON [dbo].[Invoice]
(
	[ReceiveDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
CREATE NONCLUSTERED INDEX [IX_Invoice_StockNumber] ON [dbo].[Invoice]
(
	[StockNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_Labor_InvoiceId] ON [dbo].[Labor]
(
	[InvoiceId] ASC,
	[EmployeeId] ASC,
	[LaborTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_StockNumberHistory_InvoiceId] ON [dbo].[StockNumberHistory]
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE UNIQUE NONCLUSTERED INDEX [PK_StockNumberHistory] ON [dbo].[StockNumberHistory]
(
	[StockNumberHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_ModifyUser]  DEFAULT ('system') FOR [ModifyUser]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
ALTER TABLE [dbo].[AccountType] ADD  CONSTRAINT [DF_AccountType_TaxRate]  DEFAULT ((0.0)) FOR [TaxRate]
GO
ALTER TABLE [dbo].[AccountType] ADD  CONSTRAINT [DF_AccountType_Retired]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AccountType] ADD  CONSTRAINT [DF_AccountType_ModifyUser]  DEFAULT ('system') FOR [ModifyUser]
GO
ALTER TABLE [dbo].[AccountType] ADD  CONSTRAINT [DF_AccountType_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
ALTER TABLE [dbo].[AccountTypeLabor] ADD  CONSTRAINT [DF_AccountTypeLabor_DefaultRate]  DEFAULT ((0.0)) FOR [DefaultRate]
GO
ALTER TABLE [dbo].[AccountTypeLabor] ADD  CONSTRAINT [DF_AccountTypeLabor_DefaultRateType]  DEFAULT ('F') FOR [DefaultRateType]
GO
ALTER TABLE [dbo].[AccountTypeService] ADD  CONSTRAINT [DF_AccountTypeService_DefaultRate]  DEFAULT ((0.0)) FOR [DefaultRate]
GO
ALTER TABLE [dbo].[AccountTypeService] ADD  CONSTRAINT [DF_AccountTypeService_DefaultEstimatedTime]  DEFAULT ((0)) FOR [DefaultEstimatedTime]
GO
ALTER TABLE [dbo].[AccountTypeService] ADD  CONSTRAINT [DF_AccountTypeService_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_DoNotify]  DEFAULT ((0)) FOR [DoNotify]
GO
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_Rate]  DEFAULT ((1.0)) FOR [Rate]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_CurrentlyEmployed]  DEFAULT ((1)) FOR [IsEmployed]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_CanLogin]  DEFAULT ((0)) FOR [CanLogin]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_ModifyUser]  DEFAULT ('system') FOR [ModifyUser]
GO
ALTER TABLE [dbo].[Employee] ADD  CONSTRAINT [DF_Employee_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  CONSTRAINT [DF_ErrorLog_ModifyUser]  DEFAULT ('system') FOR [ModifyUser]
GO
ALTER TABLE [dbo].[ErrorLog] ADD  CONSTRAINT [DF_ErrorLog_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_IsComplete]  DEFAULT ((0)) FOR [IsComplete]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_IsPaid]  DEFAULT ((0)) FOR [IsPaid]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_TaxRate]  DEFAULT ((0.0)) FOR [TaxRate]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_ModifyUser]  DEFAULT ('system') FOR [ModifyUser]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
ALTER TABLE [dbo].[Labor] ADD  CONSTRAINT [DF_Labor_EstimatedRate]  DEFAULT ((0)) FOR [EstimatedRate]
GO
ALTER TABLE [dbo].[Labor] ADD  CONSTRAINT [DF_Labor_EstimatedTime]  DEFAULT ((0)) FOR [EstimatedTime]
GO
ALTER TABLE [dbo].[Labor] ADD  CONSTRAINT [DF_Labor_ActualTime]  DEFAULT ((0)) FOR [ActualTime]
GO
ALTER TABLE [dbo].[Labor] ADD  CONSTRAINT [DF_Labor_ModifyUser]  DEFAULT ('system') FOR [ModifyUser]
GO
ALTER TABLE [dbo].[Labor] ADD  CONSTRAINT [DF_Labor_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_EstimatedTime]  DEFAULT ((0)) FOR [EstimatedTime]
GO
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_ModifyUser]  DEFAULT ('system') FOR [ModifyUser]
GO
ALTER TABLE [dbo].[Service] ADD  CONSTRAINT [DF_Service_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
ALTER TABLE [dbo].[StockNumberHistory] ADD  CONSTRAINT [DF_StockNumberHistory_ModifyUser]  DEFAULT ('system') FOR [ModifyUser]
GO
ALTER TABLE [dbo].[StockNumberHistory] ADD  CONSTRAINT [DF_StockNumberHistory_ModifyDate]  DEFAULT (getdate()) FOR [ModifyDate]
GO
