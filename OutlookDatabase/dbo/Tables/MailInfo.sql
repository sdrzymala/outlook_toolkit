CREATE TABLE [dbo].[MailInfo] (
    [Deleted]         BIT           NULL,
    [Envelope]        VARCHAR (MAX) NULL,
    [EWSChangeKey]    VARCHAR (MAX) NULL,
    [EWSPublicFolder] BIT           NULL,
    [IMAP4MailFlags]  VARCHAR (MAX) NULL,
    [Index]           INT           NULL,
    [PostItem]        BIT           NULL,
    [Read]            BIT           NULL,
    [Size]            INT           NULL,
    [UIDL]            VARCHAR (MAX) NULL
);

