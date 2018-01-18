using MittoAgSMS.DomainModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MittoAgSMS.DataAccessLayer
{
    public class SmsContext : DbContext
    {
        public SmsContext()
            : base("name=SMSEntities")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Sms> SentSms { get; set; }
    }
}
