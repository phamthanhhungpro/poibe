namespace Poi.Prj.Logic.Requests
{
    public class GetQuanLyCongViecRequest
    {
        public string TaskName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Guid> AssignedUserIds { get; set; }
        public List<string> Status { get; set; }
        public List<Guid> DuAnIds { get; set; }


        public int PageIndex { get; set; }
        public int PageSize { get; set; }

        public GetQuanLyCongViecRequest()
        {
            AssignedUserIds = [];
            Status = [];
            DuAnIds = [];
            PageIndex = 0;
            PageSize = 50;
        }
    }
}
