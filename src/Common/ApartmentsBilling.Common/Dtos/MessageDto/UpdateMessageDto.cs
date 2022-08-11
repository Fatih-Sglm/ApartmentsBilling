using System;

namespace ApartmentsBilling.Common.Dtos.MessageDto
{
    public class UpdateMessageDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
