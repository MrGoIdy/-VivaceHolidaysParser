using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VivaceHolidaysParser
{
    class Program
    {


        private static string GetConnectionString()
        {
            return (new MySqlConnectionStringBuilder
            {
                Server = "5.132.159.203",
                UserID = "alex",
                Password = "Minsk2017!",
                Database = "VIVACE"
            })
            .ConnectionString;
        }


        static string ConvertToGermany(string str)
        {
            return str.Replace(@"\u00f6", "ö").Replace(@"\u00d6", "Ö").Replace(@"\u00df", "ß").Trim();
        }

        static void Main(string[] args)
        {
            using (var web = new WebClient())
            {
                web.Headers.Add("user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Safari/537.36");
                try
                {
                    int year = 2015;
                    var doc = new HtmlAgilityPack.HtmlDocument();
                    //2015
                    for (year = 2015; year <= 2018; year++)
                    {


                        doc.LoadHtml(web.DownloadString("https://www.wallstreet-online.de/_rpc/json/news/calendar/getCalendarTable?formtype=holiday&range=" + year + "&country%5B%5D=22&country%5B%5D=25&country%5B%5D=2&country%5B%5D=6&country%5B%5D=93&country%5B%5D=32&country%5B%5D=33&country%5B%5D=1&country%5B%5D=3&country%5B%5D=20&offset="));
                        var dataContainer = doc.DocumentNode.SelectNodes("//td");
                        int j = 0;
                        for (int i = 0; i < doc.DocumentNode.SelectNodes("//tr").Count; i++)
                        {
                            Console.WriteLine(ConvertToGermany(dataContainer[j].InnerText.Split('<')[0]));
                            Console.WriteLine(ConvertToGermany(dataContainer[j + 1].InnerText.Split('<')[0]));
                            Console.WriteLine(ConvertToGermany(dataContainer[j + 2].InnerText.Split('>')[1].Split('<')[0]));
                            Console.WriteLine(ConvertToGermany(dataContainer[j + 3].InnerText.Split('<')[0]));
                            Console.WriteLine(ConvertToGermany(dataContainer[j + 4].InnerText.Split('<')[0]));
                            Console.WriteLine();
                            j += 5;

                        }
                        Console.ReadLine();
                    }
                }
                catch
                {
                    Console.ReadLine();
                }
            }
        }
    }
}