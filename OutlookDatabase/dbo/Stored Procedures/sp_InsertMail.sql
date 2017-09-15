
CREATE PROCEDURE dbo.sp_InsertMail
(
	@Index int,
	@FromName varchar(MAX),
	@FromAddress varchar(MAX),
	@HtmlBody varchar(MAX),
	@ReceivedDate DATETIME,
	@SentDate DATETIME,
	@Subject varchar(MAX),
	@TextBody varchar(MAX)
)
AS
IF NOT EXISTS (SELECT [Index] FROM dbo.Mail WHERE [Index] = @Index)

BEGIN
INSERT INTO dbo.Mail
(
	[Index]
	,[FromName]
	,[FromAddress]
	,[HtmlBody]
	,[ReceivedDate]
	,[SentDate]
	,[Subject]
	,[TextBody]
)
VALUES (
	@Index
	,@FromName
	,@FromAddress
	,@HtmlBody
	,@ReceivedDate
	,@SentDate
	,@Subject
	,@TextBody
	)
END