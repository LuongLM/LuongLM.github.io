using CreateWebPage;
using HtmlAgilityPack;

class DownloadAllUtils
{
    public static async Task DownloadAll()
    {
        List<LessonModel> lessons = [];

        for (int i = 1; i <= 40; i++)
        {
            LessonModel lesson = new()
            {
                Name = "3000-tu-vung-n1-bai-" + i,
                Index = 2636 + i
            };
            lessons.Add(lesson);
        }

        // Tải và phân tích HTML của trang web cho từng URL song song
        await Task.WhenAll(lessons.Select(l => DownloadAndSaveWebsiteAsync(l)));
    }

    static async Task DownloadAndSaveWebsiteAsync(LessonModel lesson)
    {
        try
        {
            // Đường dẫn của trang web bạn muốn lưu
            int lessonIndex = 2636 + lesson.Index;
            string url = "https://vietnamjp.com/" + lesson.Name + "-" + lessonIndex;

            // Lấy đường dẫn của thư mục chứa file Program.cs
            string srcFolder = Directory.GetCurrentDirectory();
            string savePathHtml = Path.Combine(srcFolder, lesson.Name + ".html");
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
                Console.WriteLine($"File already exists: {lesson.Name + ".html"}");
            }

            // Tải và lưu trữ tất cả các tài nguyên liên quan
            await DownloadResourcesAsync(document, saveDirectory, url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing lesson {lesson.Name}: {ex.Message}");
        }
    }

    static async Task DownloadResourcesAsync(HtmlDocument document, string outputFolder, string baseUrl)
    {
        using HttpClient client = new();
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