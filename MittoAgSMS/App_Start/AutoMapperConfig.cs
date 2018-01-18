using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MittoAgSMS
{
        public class AutoMapperConfig
        {
            public static void Initialize()
            {
                Mapper.Initialize((config) =>
                {
                    config.CreateMap<DomainModel.Country, BusinessModel.Country>()
                    .ForMember(source => source.Cc, destination => destination.MapFrom(x => x.CountryCode.Trim()))
                    .ForMember(source => source.Mcc, destination => destination.MapFrom(x => x.MobileCountryCode.Trim()))
                    .ForMember(source => source.Name, destination => destination.MapFrom(x => x.Name.Trim()))
                    .ForMember(source => source.PricePerSms, destination => destination.MapFrom(x => x.PricePerSms));


                    config.CreateMap<BusinessModel.SentSmsFilterRequest, DomainModel.SentSmsFilterRequest>().ReverseMap();


                    config.CreateMap<MittoAgSMS.DomainModel.Sms, MittoAgSMS.BusinessModel.Sms>().ReverseMap();


                });
            }
        }
}