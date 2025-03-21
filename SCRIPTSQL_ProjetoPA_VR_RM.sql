IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
DROP TABLE [dbo].[Products]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Seller]') AND type in (N'U'))
DROP TABLE [dbo].[Seller]
GO

CREATE TABLE [dbo].[Seller](
	[SellerID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Seller] PRIMARY KEY CLUSTERED 
(
	[SellerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO Seller VALUES (0, 'Vitor Rocha'), (1, 'Ricardo Moura'), (2, 'José Costa')
GO

CREATE TABLE [dbo].[Products](
	[ProductID] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](50) NULL,
	[Brand] [nvarchar](50) NULL,
	[Model] [nvarchar](50) NULL,
	[Year] [int] NULL,
	[BuyPrice] [decimal](10, 2) NULL,
	[SellPrice] [decimal](10, 2) NULL,
	[isSold] [bit] DEFAULT 0 NULL,
	[Date] [datetime] DEFAULT GETDATE() NULL,
	[SellerID] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Products] WITH CHECK ADD CONSTRAINT [FK_Products_Seller] FOREIGN KEY([SellerID])
REFERENCES [dbo].[Seller] ([SellerID])
GO

-- Procedimentos Armazenados para Seller

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteSeller]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteSeller]
GO

CREATE PROCEDURE [dbo].[DeleteSeller]
@SellerID int
AS
BEGIN
    DELETE FROM [Seller]
    WHERE SellerID = @SellerID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveSeller]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SaveSeller]
GO

CREATE PROCEDURE [dbo].[SaveSeller]
@SellerID int,
@Name nvarchar(100)
AS
BEGIN
    IF (SELECT COUNT(*) FROM [Seller] WHERE SellerID = @SellerID) = 0
    BEGIN
        INSERT INTO [Seller](Name)
        VALUES (@Name)
    END
    ELSE
    BEGIN
        UPDATE [Seller]
        SET Name = @Name
        WHERE SellerID = @SellerID
    END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSeller]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSeller]
GO

CREATE PROCEDURE [dbo].[GetSeller]
@SellerID int
AS
BEGIN
    SELECT * FROM [Seller] WHERE SellerID = @SellerID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ListSellers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ListSellers]
GO

CREATE PROCEDURE [dbo].[ListSellers]
AS
BEGIN
    SELECT * FROM [Seller]
END
GO

-- Procedimentos Armazenados para Products

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeleteProduct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[DeleteProduct]
GO

CREATE PROCEDURE [dbo].[DeleteProduct]
@ProductID int
AS
BEGIN
    DELETE FROM [Products]
    WHERE ProductID = @ProductID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaveProduct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SaveProduct]
GO

CREATE PROCEDURE [dbo].[SaveProduct]
@ProductID int,
@Name nvarchar(100),
@Category nvarchar(50),
@Brand nvarchar(50),
@Model nvarchar(50),
@Year int,
@BuyPrice decimal(10, 2),
@SellPrice decimal(10, 2),
@isSold bit,
@Date datetime,
@SellerID int
AS
BEGIN
    IF (SELECT COUNT(*) FROM [Products] WHERE ProductID = @ProductID) = 0
    BEGIN
        INSERT INTO [Products](Name, Category, Brand, Model, Year, BuyPrice, SellPrice, isSold, Date, SellerID)
        VALUES (@Name, @Category, @Brand, @Model, @Year, @BuyPrice, @SellPrice, @isSold, @Date, @SellerID)
    END
    ELSE
    BEGIN
        UPDATE [Products]
        SET Name = @Name,
            Category = @Category,
            Brand = @Brand,
            Model = @Model,
            Year = @Year,
            BuyPrice = @BuyPrice,
            SellPrice = @SellPrice,
            isSold = @isSold,
            Date = @Date,
            SellerID = @SellerID
        WHERE ProductID = @ProductID
    END
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProduct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProduct]
GO

CREATE PROCEDURE [dbo].[GetProduct]
@ProductID int
AS
BEGIN
    SELECT * FROM [Products] WHERE ProductID = @ProductID
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ListProduct]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ListProduct]
GO

CREATE PROCEDURE [dbo].[ListProduct]
AS
BEGIN
    SELECT * FROM [Products]
END
GO