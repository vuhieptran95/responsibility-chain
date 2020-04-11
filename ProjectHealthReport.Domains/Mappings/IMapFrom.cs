﻿﻿﻿using AutoMapper;

   namespace ProjectHealthReport.Domains.Mappings
{
    public interface IMapFrom<T>
    {   
        void MappingFrom(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
    
    public interface IMapTo<T>
    {   
        void MappingTo(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}
