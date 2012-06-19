USE [cuttagSKE]
GO

/****** Object:  Table [dbo].[PartsList]    Script Date: 05/28/2012 22:57:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PartsList](
	[PartID] [uniqueidentifier] NOT NULL,
	[SequenceNumber] [int] NOT NULL,
	[IsComputed] [bit] NOT NULL,
	[SourceID] [uniqueidentifier] NOT NULL,
	[PartNumber] [nvarchar](50) NOT NULL,
	[Qty] [decimal](18, 6) NOT NULL,
	[UOM] [nvarchar](3) NULL,
	[Description] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_PartsList_1] PRIMARY KEY CLUSTERED 
(
	[PartID] ASC,
	[SequenceNumber] ASC,
	[IsComputed] ASC,
	[SourceID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PartsList]  WITH CHECK ADD  CONSTRAINT [FK_PartsList_Part] FOREIGN KEY([PartID])
REFERENCES [dbo].[Part] ([PartID])
GO

ALTER TABLE [dbo].[PartsList] CHECK CONSTRAINT [FK_PartsList_Part]
GO

ALTER TABLE [dbo].[PartsList] ADD  CONSTRAINT [DF_PartsList_Qty]  DEFAULT ((0)) FOR [Qty]
GO


