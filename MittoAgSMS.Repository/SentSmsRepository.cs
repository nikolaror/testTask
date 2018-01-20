using MittoAgSMS.DataAccessLayer.Abstraction;
using MittoAgSMS.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MittoAgSMS.DataAccessLayer
{
    public class SentSmsRepository : GenericRepository<DomainModel.Sms>, ISentSmsRepository
    {
        public SentSmsRepository(ModelEntities entities) : base(entities)
        {
        }

        public async Task<Sms[]> FilterSentSms(SentSmsFilterRequest request)
        {
            var res = await FindBy(x => x.Sent >= request.DateTimeFrom && x.Sent <= request.DateTimeTo);
            return res.AsQueryable().OrderBy(x => x.Sent).Skip(request.Skip).Take(request.Take).ToArray();
        }
    }
}
