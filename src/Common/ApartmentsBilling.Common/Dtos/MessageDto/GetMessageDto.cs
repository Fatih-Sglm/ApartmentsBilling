using System;

namespace ApartmentsBilling.Common.Dtos.MessageDto
{
    public class GetMessageDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Count { get; set; }
        public string UserName { get; set; }
        public bool IsRead { get; set; }
    }
}
