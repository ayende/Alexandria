using System;

namespace Alexandria.Messages
{
    [Serializable]
    public class AddressDTO
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}