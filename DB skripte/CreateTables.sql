USE [MittoSMS]
GO

/****** Object:  Table [dbo].[Country]    Script Date: 1/20/2018 8:59:25 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Country](
	[Name] [nvarchar](50) NULL,
	[MobileCountryCode] [nchar](10) NOT NULL,
	[CountryCode] [nchar](10) NULL,
	[PricePerSms] [decimal](18, 3) NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[MobileCountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_MobileCc] UNIQUE NONCLUSTERED 
(
	[MobileCountryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


USE [MittoSMS]
GO

/****** Object:  Table [dbo].[SentSms]    Script Date: 1/20/2018 8:59:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SentSms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From] [nvarchar](50) NULL,
	[To] [nvarchar](50) NULL,
	[Text] [nvarchar](160) NULL,
	[MobileCountryCode] [nchar](10) NOT NULL,
	[Sent] [datetime] NULL,
	[State] [bit] NULL,
 CONSTRAINT [PK_SentSms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SentSms]  WITH CHECK ADD  CONSTRAINT [FK_MobileCc] FOREIGN KEY([MobileCountryCode])
REFERENCES [dbo].[Country] ([MobileCountryCode])
GO

ALTER TABLE [dbo].[SentSms] CHECK CONSTRAINT [FK_MobileCc]
GO


----------------- inserts
INSERT INTO [dbo].[Country]
           ([Name]
           ,[MobileCountryCode]
           ,[CountryCode]
           ,[PricePerSms])
     VALUES
('Austria',232,43,0.053)

INSERT INTO [dbo].[Country]
           ([Name]
           ,[MobileCountryCode]
           ,[CountryCode]
           ,[PricePerSms])
     VALUES
('Poland',260,48,0.032)

INSERT INTO [dbo].[Country]
           ([Name]
           ,[MobileCountryCode]
           ,[CountryCode]
           ,[PricePerSms])
     VALUES