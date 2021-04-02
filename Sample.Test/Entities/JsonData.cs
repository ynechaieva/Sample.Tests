using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace Sample.Test
{
    public class JsonData
    {
        [DataMember(Name = "page")]
        public int Page { get; set; }

        [DataMember(Name = "per_page")]
        public int PerPage { get; set; }

        [DataMember(Name = "total")]
        public int Total { get; set; }

        [DataMember(Name = "total_pages")]
        public int TotalPages { get; set; }

        [DataMember(Name = "data")]
        public List<User> Data { get; set; }

        [DataMember(Name = "support")]
        public Support Support { get; set; }


        public List<User> SelectUsersWithSpecificIds(int[] arr)
        {
            return Data.Where(user => arr.ToList().Contains(user.Id)).ToList();
        }

    }

}
