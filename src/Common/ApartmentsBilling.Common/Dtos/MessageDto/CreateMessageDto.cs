using System;

namespace ApartmentsBilling.Common.Dtos.MessageDto
{
    public class CreateMessageDto
    {

        public string Title { get; set; }
        public string Body { get; set; }
        public Guid UserId { get; set; }
    }
}
