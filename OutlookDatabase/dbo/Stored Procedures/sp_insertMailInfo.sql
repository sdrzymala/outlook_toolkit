CREATE PROCEDURE dbo.sp_insertMailInfo
(
        @Deleted BIT,
        @Envelope varchar(MAX),
        @EWSChangeKey varchar(MAX)  ,
        @EWSPublicFolder BIT,
        @IMAP4MailFlags varchar(MAX),
        @Index int ,
        @PostItem BIT,
        @Read bit,
        @Size int ,
		@UIDL varchar(MAX) 
)
AS

IF NOT EXISTS ( SELECT [Index] FROM dbo.MailInfo WHERE [INDEX] = @Index )
BEGIN 
	INSERT INTO dbo.MailInfo 
	(
		[Deleted]
		,[Envelope]
		,[EWSChangeKey]
		,[EWSPublicFolder]
		,[IMAP4MailFlags]
		,[Index]
		,[PostItem]
		,[Read]
		,[Size]
		,[UIDL]
	)
	VALUES	
	(
		 @Deleted
		,@Envelope
		,@EWSChangeKey
		,@EWSPublicFolder
		,@IMAP4MailFlags
		,@Index
		,@PostItem
		,@Read
		,@Size
		,@UIDL
	)
END