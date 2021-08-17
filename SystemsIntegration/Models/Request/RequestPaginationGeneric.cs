namespace SystemsIntegration.Api.Models.Request
{
    public class RequestPaginationGeneric <T>
    {
        public T items { get; set; }
        public PaginationModel pageInfo { get; set; }
    }
}
