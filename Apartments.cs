using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_5_Miracle
{
    class Apartments : Owner
    {
        string id, size, rooms, bathrooms, floor, contractType, age, price,
            address, options;

        public string Id { get { return id; } set { id = value; } }
        public string Size { get { return size; } set { size = value; } }
        public string Rooms { get { return rooms; }set { rooms = value; } }
        public string Bathrooms { get { return bathrooms; } set { bathrooms = value; } }
        public string Floor { get { return floor; } set { floor = value; } }
        public string ContactType { get { return contractType; } set { contractType = value; } }
        public string Age { get { return age; } set { age = value; } }
        public string Price { get { return price; } set { price = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string Options { get { return options; } set { options = value; } }

        public Apartments(string id, string rooms, string size, string bathrooms, string floor, string contractType, string age,
            string price, string address, string options,string ownerName, string ownerPhone, string ownerSurename, 
            string ownerEmail, string ownerDatebirth, string ownerAddress)
            :base(ownerName,ownerSurename,ownerPhone,ownerEmail,ownerDatebirth,ownerAddress)
        {
            this.id = id;
            this.size = size;
            this.rooms = rooms;
            this.bathrooms = bathrooms;
            this.floor = floor;
            this.contractType = contractType;
            this.age = age;
            this.price = price;
            this.address = address;
            this.options = options;

        }
    }
}
