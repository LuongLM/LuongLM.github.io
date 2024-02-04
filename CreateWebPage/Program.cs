using HtmlAgilityPack;

class Program
{
    static async Task Main()
    {
        // Đường dẫn của trang web bạn muốn lưu
        int lessonIndex = 2638;
        string lesson = "3000-tu-vung-n1-bai-" + 3;
        string url = "https://vietnamjp.com/" + lesson + "-" + lessonIndex;

        // Lấy đường dẫn của thư mục chứa file Program.cs
        string srcFolder = Directory.GetCurrentDirectory();
        string savePathHtml = Path.Combine(srcFolder, lesson + ".html");
        string saveDirectory = Path.Combine(srcFolder, "common");

        // Tải và phân tích HTML của trang web
        HtmlWeb web = new();
        HtmlDocument document = web.Load(url);

        // Lưu trữ nội dung HTML vào file
        // Kiểm tra xem file đã tồn tại hay chưa
        if (!File.Exists(savePathHtml))
        {
            File.WriteAllText(Path.Combine(savePathHtml), document.DocumentNode.OuterHtml);
        }
        else
        {
            Console.WriteLine($"File already exists: {lesson + ".html"}");
        }

        // Tải và lưu trữ tất cả các tài nguyên liên quan
        await DownloadResourcesAsync(document, saveDirectory, url);
    }

    static async Task DownloadResourcesAsync(HtmlDocument document, string outputFolder, string baseUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            // Lấy tất cả các thẻ <script> và tải về file
            foreach (HtmlNode scriptNode in document.DocumentNode.SelectNodes("//script[@src]"))
            {
                string scriptUrl = scriptNode.GetAttributeValue("src", "");
                await DownloadFileAsync(client, scriptUrl, outputFolder, baseUrl);
            }

            // Lấy tất cả các thẻ <link> có attribute rel="stylesheet" và tải về file
            foreach (HtmlNode linkNode in document.DocumentNode.SelectNodes("//link[@rel='stylesheet']"))
            {
                string cssUrl = linkNode.GetAttributeValue("href", "");
                await DownloadFileAsync(client, cssUrl, outputFolder, baseUrl);
            }
        }
    }

    static async Task DownloadFileAsync(HttpClient client, string fileUrl, string outputFolder, string baseUrl)
    {
        try
        {
            Uri absoluteUri = new Uri(new Uri(baseUrl), fileUrl);
            string fileName = Path.GetFileName(absoluteUri.LocalPath);
            string filePath = Path.Combine(outputFolder, fileName);

            // Kiểm tra xem file đã tồn tại hay chưa
            if (File.Exists(filePath))
            {
                Console.WriteLine($"File already exists: {fileName}");
                return;
            }

            // Tải tài nguyên về và lưu vào thư mục đích
            byte[] fileBytes = await client.GetByteArrayAsync(absoluteUri);
            await File.WriteAllBytesAsync(filePath, fileBytes);

            Console.WriteLine($"Downloaded: {fileName}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error downloading file: {ex.Message}");
        }
    }
}