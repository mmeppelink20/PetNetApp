/// <summary>
/// Alexis Oetken
/// Created: 2023/04/20
/// </summary>
///
/// <remarks>
/// Oleksiy Fedchuk
/// Updated: 2023/04/23
/// 
/// Final QA
/// </remarks>
using DataAccessLayerInterfaces;
using DataObjects;
using LogicLayerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class BookmarkManager : IBookmarkManager
    {
        private IBookmarkAccessor _bookmarkAccessor = null;

        public BookmarkManager()
        {
            _bookmarkAccessor = new DataAccessLayer.BookmarkAccessor();
        }

        public BookmarkManager(IBookmarkAccessor bookmarkAccessor)
        {
            _bookmarkAccessor = bookmarkAccessor;
        }

        public bool AddBookmark(int UserId, int AnimalId)
        {
            bool result = false;

            try
            {
                result = 1 == _bookmarkAccessor.InsertBookmark(UserId, AnimalId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool DeleteBookmark(int UserId, int AnimalId)
        {
            bool result = false;

            try
            {
                result = 1 == _bookmarkAccessor.RemoveBookmark(UserId, AnimalId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public List<Bookmark> RetrieveAllBookmarks(int UserId)
        {
            List<Bookmark> bookmarks = null;

            try
            {
                bookmarks = _bookmarkAccessor.RetrieveAllBookmarks(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bookmarks;
        }

    }
}


