namespace Nexora.WebUI.DTOs.DestinationDtos
{
    public class GetDestinationByIdDto
    {
        public bool status { get; set; }
        public List<DestinationItemDto> data { get; set; }

        public class DestinationItemDto
        {
            public string dest_id { get; set; }
            public string search_type { get; set; }
            public string country { get; set; }
            public string name { get; set; }
            public string label { get; set; }
        }
    }
}
