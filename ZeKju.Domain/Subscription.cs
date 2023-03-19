using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeKju.Domain
{
    public class Subscription : BaseEntity<string>
    {
        public new string Id { get => string.Format("{0}-{1}-{2}", AgencyId, OriginCityId, DestinationCityId); }
        public int AgencyId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
    }
}
