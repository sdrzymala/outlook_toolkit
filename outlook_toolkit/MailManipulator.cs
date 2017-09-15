using EAGetMail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace outlook_toolkit
{
    public class MailManipulator
    {
        public static void GetAndSaveToDatabaseMailInfo(string emailAddress, string accountPassword)
        {
            MailServer oServer = new MailServer("imap-mail.outlook.com", emailAddress, accountPassword, ServerProtocol.Imap4);
            MailClient oClient = new MailClient("TryIt");


            oServer.SSLConnection = true;
            oServer.Port = 993;

            try
            {
                oClient.Connect(oServer);

                List<MailInfo> allMailInfos = oClient.GetMailInfos().ToList();

                foreach (MailInfo item in allMailInfos)
                {
                    Console.WriteLine(item.Index);

                    using (SqlConnection con = new SqlConnection("Data Source=.\\sql2016; Initial Catalog=Outlook; Integrated Security=SSPI;"))
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_insertMailInfo", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.Add("@Deleted", SqlDbType.Bit).Value = item.Deleted;
                            cmd.Parameters.Add("@Envelope", SqlDbType.VarChar).Value = item.Envelope;
                            cmd.Parameters.Add("@EWSChangeKey", SqlDbType.VarChar).Value = item.EWSChangeKey;
                            cmd.Parameters.Add("@EWSPublicFolder", SqlDbType.Bit).Value = item.EWSPublicFolder;
                            cmd.Parameters.Add("@IMAP4MailFlags", SqlDbType.VarChar).Value = item.IMAP4MailFlags;
                            cmd.Parameters.Add("@Index", SqlDbType.Int).Value = item.Index;
                            cmd.Parameters.Add("@PostItem", SqlDbType.Bit).Value = item.PostItem;
                            cmd.Parameters.Add("@Read", SqlDbType.Bit).Value = item.Read;
                            cmd.Parameters.Add("@Size", SqlDbType.Int).Value = item.Size;
                            cmd.Parameters.Add("@UIDL", SqlDbType.VarChar).Value = item.UIDL;

                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                oClient.Quit();
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }

        }

        public static void GetMailInfoFromDatabaseAndGetMailFromOutlook(int mailDownloadLimit, string emailAddress, string accountPassword)
        {
            List<int> allMailsToDownload = new List<int>();

            using (SqlConnection con = new SqlConnection("Data Source=.\\sql2016; Initial Catalog=Outlook; Integrated Security=SSPI;"))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_GetMailInfos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@mailDownloadLimit", SqlDbType.Int).Value = mailDownloadLimit;

                    con.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        allMailsToDownload.Add(int.Parse(reader["Index"].ToString()));
                    }

                }
            }


            MailServer oServer = new MailServer("imap-mail.outlook.com", emailAddress, accountPassword, ServerProtocol.Imap4);
            MailClient oClient = new MailClient("TryIt");


            oServer.SSLConnection = true;
            oServer.Port = 993;

            try
            {
                oClient.Connect(oServer);

                List<MailInfo> allMailInfos = oClient.GetMailInfos().ToList();

                foreach (MailInfo mailInfo in allMailInfos)
                {
                    if (allMailsToDownload.Contains(mailInfo.Index))
                    {
                        Mail mail = oClient.GetMail(mailInfo);
  
                        using (SqlConnection con = new SqlConnection("Data Source=.\\sql2016; Initial Catalog=Outlook; Integrated Security=SSPI;"))
                        {
                            using (SqlCommand cmd = new SqlCommand("sp_InsertMail", con))
                            {
                                Console.WriteLine(mailInfo.Index);

                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.Add("@Index", SqlDbType.Int).Value = mailInfo.Index;
                                cmd.Parameters.Add("@FromName", SqlDbType.VarChar).Value = mail.From.Name;
                                cmd.Parameters.Add("@FromAddress", SqlDbType.VarChar).Value = mail.From.Address;
                                cmd.Parameters.Add("@HtmlBody", SqlDbType.VarChar).Value = mail.HtmlBody;
                                cmd.Parameters.Add("@ReceivedDate", SqlDbType.DateTime).Value = mail.ReceivedDate;
                                cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = mail.SentDate;
                                cmd.Parameters.Add("@Subject", SqlDbType.VarChar).Value = mail.Subject;
                                cmd.Parameters.Add("@TextBody", SqlDbType.VarChar).Value = mail.TextBody;

                                con.Open();
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }
                }

                oClient.Quit();
            }
            catch (Exception ep)
            {
                Console.WriteLine(ep.Message);
            }

        }
    }
}
