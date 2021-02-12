namespace Core.Entities.OrderAggregate
{
    //Owned entity for Order, has no key, part of the owner/order and cannot exist without it
    public class Address
    {
        //This is just for the sake of EF, as it expects an empty constructor
        public Address()
        {
        }

        //To pass values
        public Address(string firstName, string lastName, string street, string city, string state, string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            City = city;
            State = state;
            Zipcode = zipcode;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

    }
}