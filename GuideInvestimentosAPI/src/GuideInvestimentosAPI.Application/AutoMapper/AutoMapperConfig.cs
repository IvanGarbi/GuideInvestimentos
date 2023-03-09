using AutoMapper;
using GuideInvestimentosAPI.Business.Models;
using System.Net;
using GuideInvestimentosAPI.Application.ViewModels;

namespace GuideInvestimentosAPI.Application.AutoMapper;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<GetAssetViewModel, Asset>().ReverseMap();
    }
}