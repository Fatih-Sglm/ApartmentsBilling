using System;

namespace ApartmentsBilling.Common.Dtos.UserDtos
{
    public class ChangePasswordDto
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
