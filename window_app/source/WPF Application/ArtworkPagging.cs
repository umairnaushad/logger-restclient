using REST_Client_API;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace WPFApplication
{
    class ArtworkPagging
    {
        public int PageIndex { get; set; }

        DataTable PagedList = new DataTable();

        public DataTable Next(IList<Artwork> ListToPage, int RecordsPerPage)
        {
            PageIndex++;
            if (PageIndex >= ListToPage.Count / RecordsPerPage)
            {
                PageIndex = ListToPage.Count / RecordsPerPage;
            }
            PagedList = SetPaging(ListToPage, RecordsPerPage);
            return PagedList;
        }

        public DataTable Previous(IList<Artwork> ListToPage, int RecordsPerPage)
        {
            PageIndex--;
            if (PageIndex <= 0)
            {
                PageIndex = 0;
            }
            PagedList = SetPaging(ListToPage, RecordsPerPage);
            return PagedList;
        }
                
        public DataTable First(IList<Artwork> ListToPage, int RecordsPerPage)
        {
            PageIndex = 0;
            PagedList = SetPaging(ListToPage, RecordsPerPage);
            return PagedList;
        }

        public DataTable Last(IList<Artwork> ListToPage, int RecordsPerPage)
        {
            PageIndex = ListToPage.Count / RecordsPerPage;
            PagedList = SetPaging(ListToPage, RecordsPerPage);
            return PagedList;
        }
		public DataTable SetPaging(IList<Artwork> ListToPage, int RecordsPerPage)
        {
            int PageGroup = PageIndex * RecordsPerPage;

            IList<Artwork> PagedList = new List<Artwork>();

            PagedList = ListToPage.Skip(PageGroup).Take(RecordsPerPage).ToList(); //This is where the Magic Happens. If you have a Specific sort or want to return ONLY a specific set of columns, add it to this LINQ Query.

            DataTable FinalPaging = PagedTable(PagedList);

            return FinalPaging;
        }

        //If youre paging say 30,000 rows and you know the processors of the users will be
        //slow you can ASync thread both of these to allow the UI to update after they finish and prevent a hang.

		private DataTable PagedTable<T>(IList<T> SourceList)
        {
            Type columnType = typeof(T);
            DataTable TableToReturn = new DataTable();

            foreach (var Column in columnType.GetProperties())
            {
                TableToReturn.Columns.Add(Column.Name, Column.PropertyType);
            }

            foreach (object item in SourceList)
            {
                DataRow ReturnTableRow = TableToReturn.NewRow();
                foreach (var Column in columnType.GetProperties())
                {
                    ReturnTableRow[Column.Name] = Column.GetValue(item);
                }
                TableToReturn.Rows.Add(ReturnTableRow);
            }
            return TableToReturn;
        }
    }
}
