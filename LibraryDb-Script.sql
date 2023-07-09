
/****** Object:  Table [dbo].[Author]    Script Date: 11/7/2022 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Book]    Script Date: 11/7/2022 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Book](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[AuthorId] [int] NOT NULL,
	[PublisherId] [int] NOT NULL,
	[YearPublished] [smallint] NOT NULL,
	[ISBN] [nvarchar](256) NULL,
	[Picture] varbinary(max) null
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 11/7/2022 9:14:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Book_AuthorId]    Script Date: 11/7/2022 9:14:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Book_AuthorId] ON [dbo].[Book]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Book_PublisherId]    Script Date: 11/7/2022 9:14:28 PM ******/
CREATE NONCLUSTERED INDEX [IX_Book_PublisherId] ON [dbo].[Book]
(
	[PublisherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Author_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[Author] ([Id])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Author_AuthorId]
GO
ALTER TABLE [dbo].[Book]  WITH CHECK ADD  CONSTRAINT [FK_Book_Publisher_PublisherId] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[Publisher] ([Id])
GO
ALTER TABLE [dbo].[Book] CHECK CONSTRAINT [FK_Book_Publisher_PublisherId]
GO



--[Author]
CREATE PROC GetAuthors
AS
SELECT * FROM Author
GO

CREATE PROC GetAuthor
	@id int
AS
SELECT * FROM Author WHERE Id = @id
GO

CREATE PROC DeleteAuthor
	@id int
AS
DELETE FROM Book WHERE AuthorId = @id
DELETE FROM Author WHERE Id = @id
GO

CREATE PROC AddAuthor
	@name nvarchar(20),
	@id INT OUTPUT
AS 
BEGIN
INSERT INTO Author VALUES (@name)
	SET @id = SCOPE_IDENTITY()
END
GO

CREATE PROC UpdateAuthor
	@id int,
	@name nvarchar(20)
AS
UPDATE Author SET 
		[Name] = @name
	WHERE 
		Id = @id
GO


--[Publisher]
CREATE PROC GetPublishers
AS
SELECT * FROM Publisher
GO

CREATE PROC GetPublisher
	@id int
AS
SELECT * FROM Publisher WHERE Id = @id
GO

CREATE PROC DeletePublisher
	@id int
AS
DELETE FROM Book WHERE PublisherId = @id
DELETE FROM Publisher WHERE Id = @id
GO

CREATE PROC AddPublisher
	@name nvarchar(20),
	@id INT OUTPUT
AS 
BEGIN
INSERT INTO Publisher VALUES (@name)
	SET @id = SCOPE_IDENTITY()
END
GO

CREATE PROC UpdatePublisher
	@id int,
	@name nvarchar(20)
AS
UPDATE Publisher SET 
		[Name] = @name
	WHERE 
		Id = @id
GO

--[Book]
CREATE PROC GetBooks
AS
SELECT * FROM Book
GO

CREATE PROC GetBook
	@id int
AS
SELECT * FROM Book WHERE Id = @id
GO

CREATE PROC DeleteBook
	@id int
AS
DELETE FROM Book WHERE Id = @id
GO

CREATE PROC AddBook
	@title nvarchar(20),
	@author int,
	@publisher int,
	@yearPublished int,
	@isbn nvarchar(256),
	@picture varbinary(max),
	@id INT OUTPUT
AS 
BEGIN
INSERT INTO Book VALUES (@title, @author, @publisher, @yearPublished, @isbn, @picture)
	SET @id = SCOPE_IDENTITY()
END
GO

CREATE PROC UpdateBook
	@id int,
	@title nvarchar(20),
	@author int,
	@publisher int,
	@yearPublished int,
	@isbn nvarchar(256),
	@picture varbinary(max)
AS
UPDATE Book SET 
		Title = @title,
		AuthorId = @author,
		PublisherId = @publisher,
		YearPublished = @yearPublished,
		ISBN = @isbn,
		Picture = @picture
	WHERE 
		Id = @id
GO

