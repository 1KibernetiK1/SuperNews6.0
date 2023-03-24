using SuperNews.Models;
using System.Collections.Generic;
using System.Linq;

namespace SuperNews.BusinessLogic
{
    public class Bookmarks
    {
        public Bookmarks()
        {
            Records = new List<BookmarksRecord>();
        }

        public int NewsCount => Records.Sum(r => r.Quantity);


        public void RemoveNews(NewsViewModel model)
        {
            var record = Records
                .FirstOrDefault(r => r.News.NewsId == model.NewsId);

            if (record != null)
            {
                record.Quantity--;
                if (record.Quantity == 0)
                {
                    Records.Remove(record);
                }
            }
        }

        public void AddNews(NewsViewModel model)
        {
            var record = Records
                .FirstOrDefault(r => r.News.NewsId == model.NewsId);

            if (record == null)
            {
                Records.Add(new BookmarksRecord { News = model, Quantity = 1 });
            }
            else
            {
                record.Quantity++;
            }
        }

        public List<BookmarksRecord> Records { get; set; }
    }

    public class BookmarksRecord
    {
        public NewsViewModel News { get; set; }

        public int Quantity { get; set; }
    }
}
