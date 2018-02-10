using System.Collections.Generic;

namespace WebApplication1.dto
{
    public class MyPage<T>
    {
        public int page = 0;
        public int size = 10;
        public int totalElements = 0;
        public int totalPages = 1;
        public List<T> content;

        public MyPage()
        {

        }

        public MyPage(int page, int size, int totalElements, int totalPages, List<T> content)
        {
            this.page = page;
            this.size = size;
            this.totalElements = totalElements;
            this.totalPages = totalPages;
            this.content = content;
        }

    }
}