using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HtmlSerializer
{
    internal class HtmlSerializer
    {
        public string Url { get; set; }
        public List<string> Tags { get; set; }

        public HtmlSerializer()
        {
            Tags = new List<string>();
        }

        // פונקציה לטעינת ה-HTML מהאינטרנט
        public async Task<string> Load(string url)
        {
            this.Url = url;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var html = await response.Content.ReadAsStringAsync();
                return html;
            }
        }

        // פונקציה שמפרקת את המחרוזת וממלאת את רשימת ה-Tags
        public void Parse(string html)
        {
            // תבנית Regex שמוצאת תגיות ושומרת גם את הטקסט שביניהן
            string pattern = @"(<[^>]+>)";

            // פירוק המחרוזת למערך
            string[] parts = Regex.Split(html, pattern);

            // ניקוי רווחים, ירידות שורה ומחרוזות ריקות
            this.Tags = parts
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToList();
        }
    }
}