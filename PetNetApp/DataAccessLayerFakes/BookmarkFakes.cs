using DataAccessLayerInterfaces;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerFakes
{
    public class BookmarkFakes : IBookmarkAccessor
    {
        private List<Bookmark> bookmarks = new List<Bookmark>();
        private List<BookmarkVM> bookmarksVM = new List<BookmarkVM>();



        public BookmarkFakes()
        {
            bookmarksVM.Add(new BookmarkVM
            {
                AnimalId = 100000,
                AnimalName = "Chip",
                UserId = 100001
            });

            bookmarksVM.Add(new BookmarkVM
            {
                AnimalId = 100002,
                AnimalName = "Sally",
                UserId = 100031
            });

            bookmarksVM.Add(new BookmarkVM
            {
                AnimalId = 102000,
                AnimalName = "Rocko",
                UserId = 100011
            });
        }

        public int InsertBookmark(int UserId, int AnimalId)
        {
            int rowsAffected = 0;

            int existingRows = bookmarksVM.Count();
            bookmarksVM.Add(new BookmarkVM
            {
                UserId = 100000,
                AnimalId = 200000
            });

            rowsAffected = bookmarksVM.Count - existingRows;
            return rowsAffected;
        }

        public int RemoveBookmark(int UserId, int AnimalId)
        {
            int rowsAffected = 0;

            if (bookmarksVM.Remove(bookmarksVM.FirstOrDefault(v => v.UserId == UserId)))
            {
                rowsAffected++;
            }

            return rowsAffected;
        }

        public List<Bookmark> RetrieveAllBookmarks(int UserId)
        {
            List<Bookmark> bookmarks = new List<Bookmark>();

            foreach (BookmarkVM bookmark in bookmarksVM)
            {
                bookmarks.Add(bookmark);
            }

            return bookmarks;

        }
    }
}
