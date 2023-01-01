using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_5_Miracle
{
    abstract class Owner
    {
        string ownerName, ownerSurename, ownerPhone, ownerEmail, ownerDatebirth, ownerAddress;
        public string OwnerName { get { return ownerName; } set { ownerName = value; } }
        public string OwnerSurename { get { return ownerSurename;  } set { ownerSurename = value; } }
        public string OwnerPhone { get { return ownerPhone; } set { ownerPhone = value; } }
        public string OwnerEmail { get { return ownerEmail; } set { ownerEmail = value; } }
        public string OwnerDatebirth { get { return ownerDatebirth; } set { ownerDatebirth = value; } }
        public string OwnerAddress { get { return ownerAddress; } set { ownerAddress = value; } }
        public Owner(string ownerName, string ownerSurename, string ownerPhone, 
            string ownerEmail, string ownerDatebirth, string ownerAddress)
        {
            this.ownerName = ownerName;
            this.ownerSurename = ownerSurename;
            this.ownerPhone = ownerPhone;
            this.ownerEmail = ownerEmail;
            this.ownerDatebirth = ownerDatebirth;
            this.ownerAddress = ownerAddress;
        }
    }
}
