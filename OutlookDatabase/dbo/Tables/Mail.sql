CREATE TABLE [dbo].[Mail] (
    [Index]        INT           NULL,
    [FromName]     VARCHAR (MAX) NULL,
    [FromAddress]  VARCHAR (MAX) NULL,
    [HtmlBody]     VARCHAR (MAX) NULL,
    [ReceivedDate] DATETIME      NULL,
    [SentDate]     DATETIME      NULL,
    [Subject]      VARCHAR (MAX) NULL,
    [TextBody]     VARCHAR (MAX) NULL
);

