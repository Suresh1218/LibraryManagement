using AutoMapper;
using DataModel;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Books, BookStatisticsViewModel>()
                .ForMember(dest => dest.BookName, vm => vm.MapFrom(src => src.Name))
                .ForMember(dest => dest.NoOfBookInUse, vm => vm.MapFrom(src => src.NoOfBooksIsInUse))
                .ForMember(dest => dest.BookCount, vm => vm.MapFrom(src => src.NoOfSoldBooks));
        }
    }
}