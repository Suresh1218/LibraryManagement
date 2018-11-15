using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement
{
    public class MapperClass : Profile
    {
        public MapperClass()
        {
            //CreateMap();
        }
    }

    public class MapRegister
    {
        public static void Configure()
        {
            Mapper.Initialize(mapper =>
            {
                mapper.AddProfile<MapperClass>();
            });
        }
    }
}