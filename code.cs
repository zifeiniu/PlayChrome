using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayChrome
{
    internal class Demo
    {

        public static async Task Start() 
        {

            using var playwright = await Playwright.CreateAsync();
            BrowserTypeLaunchOptions option = new BrowserTypeLaunchOptions();
            option.Headless = false;
            option.ExecutablePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
            option.Timeout = 60 * 1000;
             

            await using var browser = await playwright.Chromium.LaunchAsync(option);
            var page = await browser.NewPageAsync();

            for (int p = 35; p < 100; p++)
            {
                try
                {
                    StreamWriter sw = File.CreateText($"page{p}.txt");

                    await page.GotoAsync("https://sukebei.nyaa.si/?p=" + p);
                    Console.WriteLine(p);
                    var allTr = await page.QuerySelectorAllAsync("tr");
                    for (int i = 0; i < allTr.Count; i++)
                    {
                        var tr = allTr[i];
                        var allTd = await tr.QuerySelectorAllAsync("td");
                        if (allTd.Count > 3)
                        {
                            try
                            {
                                var name = await allTd[1].QuerySelectorAsync("a");
                                var tittle = await name.GetPropertyAsync("title");
                                if (tittle == null)
                                {
                                    continue;
                                }
                                //Console.WriteLine(tittle);
                                var downloadALink = await allTd[2].QuerySelectorAllAsync("a");


                                var size = await allTd[3].InnerTextAsync();

                                var torrent = await downloadALink[0].GetPropertyAsync("href");
                                //Console.WriteLine(torrent);
                                var Mage = await downloadALink[1].GetPropertyAsync("href");
                                //Console.WriteLine(Mage);

                                sw.WriteLine(tittle);
                                sw.WriteLine(torrent);
                                sw.WriteLine(Mage);
                                sw.WriteLine(size);
                                sw.Flush();
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
