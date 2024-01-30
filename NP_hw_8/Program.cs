using System.Diagnostics;
using System.Net;

internal class Program
{
    static async Task Main(string[] args) => await new Program().Run();

    private async Task Run()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://ftp.dlptest.com/input/archive_csv.zip");
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        request.Credentials = new NetworkCredential(
            "dlpuser", "rNrKYTX9g7z3RgJRmxWuGHbeu");

        using FtpWebResponse response = (FtpWebResponse)await request.GetResponseAsync();

        if ((int)response.StatusCode >= 400)

            throw new InvalidOperationException(response.StatusCode.ToString());

        using Stream stream = response.GetResponseStream();
        using FileStream file = File.Create("archive_csv.zip");


        await stream.CopyToAsync(file);

        stopwatch.Stop();

        long length = file.Length;

        TimeSpan time = stopwatch.Elapsed;

        Console.WriteLine($"Время получения: {length / time.TotalMilliseconds} секунд");

        

    }
}
