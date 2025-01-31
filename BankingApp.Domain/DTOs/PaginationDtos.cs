namespace BankingApp.Application.DTOs
{
    public  class PaginationDtos
    {
        
        public int page { get; set; }
        public int pageSize { get; set; }


        public PaginationDtos(int page , int pageSize) 
        {
            this.page = page;
            this.pageSize = pageSize;   
        }
    }
}
