using System.Net;
using CreateWebPage;
using HtmlAgilityPack;

class DownloadPartOfWebUtils
{
    public static async Task DownloadPartOfWeb()
    {
        List<LessonModel> lessons = [];

        for (int i = 1; i <= 40; i++)
        {
            LessonModel lesson = new()
            {
                Name = "3000-tu-vung-n1-bai-" + i,
                Index = i,
                Code = (i > 30) ? 2864 + i : 2635 + i
            };
            lessons.Add(lesson);
        }

        // Tải và phân tích HTML của trang web cho từng URL song song
        await Task.WhenAll(lessons.Select(l => DownloadWebById(l)));
    }

    static async Task DownloadWebById(LessonModel lesson)
    {
        try
        {
            // Đường dẫn của trang web bạn muốn lưu
            string name = lesson.Name;
            int index = lesson.Index;
            int code = lesson.Code;
            string fileName = $"bai-{index}.html";
            string url = "https://vietnamjp.com/" + name + "-" + code;

            // Lấy đường dẫn của thư mục chứa file Program.cs
            string srcFolder = Directory.GetCurrentDirectory();
            string savePathHtml = Path.Combine(srcFolder, fileName);
            string elementId = "post-" + code;

            // Tải và phân tích HTML của trang web
            HtmlWeb web = new();
            HtmlDocument document = web.Load(url);

            // Tải nội dung HTML từ trang web
            string htmlContent = DownloadHtml(url);

            // Phân tích nội dung HTML
            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(htmlContent);

            // Tìm phần tử theo ID
            HtmlNode targetElement = htmlDocument.GetElementbyId(elementId);

            if (targetElement != null)
            {
                if (!File.Exists(savePathHtml))
                {
                    // Lưu nội dung vào một tệp tin
                    SaveHtmlToFile(fileName, targetElement.OuterHtml);
                }
                else
                {
                    Console.WriteLine($"File already exists: {fileName}");
                }
            }
            else
            {
                Console.WriteLine("Element with ID '{0}' not found.", elementId);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing lesson {lesson.Name}: {ex.Message}");
        }
    }

    static string DownloadHtml(string url)
    {
        using (WebClient client = new WebClient())
        {
            try
            {
                // Tải nội dung HTML từ URL
                return client.DownloadString(url);
            }
            catch (WebException ex)
            {
                Console.WriteLine("Error downloading HTML: " + ex.Message);
                return null;
            }
        }
    }

    static void SaveHtmlToFile(string filePath, string htmlContent)
    {
        // Ghi nội dung HTML vào tệp tin
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            sw.Write(htmlContent);
        }
    }
}