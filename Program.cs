using Microsoft.Playwright;
using PlayChrome;
using System.Threading.Tasks;

class Program
{
    public static async Task Main()
    {
        await Demo.Start();
        Console.Read(); 
        //await page.ScreenshotAsync(new PageScreenshotOptions { Path = "screenshot.png" });
        Console.WriteLine();
    }



}