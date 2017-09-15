using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Live;
using EAGetMail;
using System.IO;
using outlook_toolkit;



namespace outlook_toolkit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Email address: ");
            string emailAddress = Console.ReadLine();
            Console.Write("Password: ");
            string accountPassword = Console.ReadLine();
            Console.Write("Number of email to download: ");
            int mailToDwonloadLimit = Convert.ToInt32(Console.ReadLine());

            MailManipulator.GetAndSaveToDatabaseMailInfo(emailAddress, accountPassword);
            MailManipulator.GetMailInfoFromDatabaseAndGetMailFromOutlook(mailToDwonloadLimit, emailAddress, accountPassword);

            Console.WriteLine("Done");
            Console.ReadKey();

        }



    }
}
