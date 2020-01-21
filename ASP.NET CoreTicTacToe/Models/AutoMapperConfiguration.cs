using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_CoreTicTacToe.Models
{
    public class AutoMapperConfiguration<Source, Destination>
    {
        public Destination GetDTO(Source source)
        {
            var config = Configure();
            IMapper mapper = config.CreateMapper();
            return mapper.Map<Source, Destination>(source);
        }

        public Source GetOrigin(Destination destination)
        {
            var config = ConfigureReverse();
            IMapper mapper = config.CreateMapper();
            return mapper.Map<Destination, Source>(destination);
        }

        MapperConfiguration ConfigureReverse()
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Source, Destination>().ReverseMap();
            });
            return config;
        }

        MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Source, Destination>();
            });
            return config;
        }
    }
}
