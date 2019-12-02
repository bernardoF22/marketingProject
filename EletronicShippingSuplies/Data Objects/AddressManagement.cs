using System.Collections.Generic;
using System.Linq;

namespace EletronicShippingSuplies.Data_Objects
{
    public class AddressManagement
    {

        public static List<Address> GetUserAddress(DB_OSSEntities oss, int iID)
        {
            List<Address> address = null;
            address = (from ad in oss.Address
                       where ad.OWNER == iID
                       select ad).ToList();
            return address;
        }

        public static Address GetAddressDetails(DB_OSSEntities oss, int iID)
        {
            var address = oss.Address.FirstOrDefault(s => s.ID == iID);
            return address;
        }

        public static Address GetOwnerAddress(DB_OSSEntities oss, int owner)
        {
            var addressOwner = from ao in oss.Address
                               where ao.OWNER.Equals(owner)
                               select ao;
            if (addressOwner.Count() == 0)
            {
                return null;
            }
            else
            {
                return addressOwner.First();
            }
        }

        public static Address DeleteAddress(DB_OSSEntities oss, int iID)
        {
            var address = oss.Address.First(s => s.ID == iID);
            if (address != null)
            {
                oss.Address.Remove(address);
                oss.SaveChanges();
                return address;
            }
            else
            {
                return null;
            }
        }
    }
}