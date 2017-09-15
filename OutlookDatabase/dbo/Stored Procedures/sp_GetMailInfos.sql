
CREATE PROCEDURE [dbo].[sp_GetMailInfos] 
(
	@mailDownloadLimit INT
)
AS
SELECT
	TOP (@mailDownloadLimit) 
	[INDEX] 
FROM dbo.MailInfo
WHERE [Index] NOT IN (SELECT [INDEX] FROM dbo.Mail)