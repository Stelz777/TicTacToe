using ASP.NETCoreTicTacToe.Infrastructure.MapperProfiles;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace XUnitTestTicTacToe
{
    public class AutomapperSingleton
    {
        private static IMapper mapper;
        public static IMapper Mapper
        {
            get
            {
                if (mapper == null)
                {
                    var mappingConfig = new MapperConfiguration(mapperConfiguration =>
                    {
                        mapperConfiguration.AddProfile(new GameProfile());
                    });
                    IMapper newMapper = mappingConfig.CreateMapper();
                    mapper = newMapper;
                }
                return mapper;
            }
        }


    }
}
