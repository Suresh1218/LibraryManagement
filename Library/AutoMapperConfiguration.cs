using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library
{
    public class AutoMapperConfiguration
    {
        public static void configuration()
        {
            Mapper.Initialize(mapper =>
            {
                mapper.AddProfile<AutoMapping>();
            });
        }
    }
}