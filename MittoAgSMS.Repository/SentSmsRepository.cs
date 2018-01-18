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

        public Sms[] FilterSentSms(SentSmsFilterRequest request)
        {
            return GetAll().AsQueryable().Where(x => x.Sent >= request.DateTimeFrom && x.Sent < request.DateTimeTo).OrderBy(x=>x.Sent).Skip(request.Skip).Take(request.Take).ToArray();
        }
    }
}
