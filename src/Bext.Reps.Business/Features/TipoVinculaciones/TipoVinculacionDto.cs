﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Bext.Reps.Business.Features.TipoNaturalezas;
public class TipoVinculacionDto
{
    public string Id { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;

    [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
    private class Mapping : Profile
    {
        
        [SuppressMessage("Category", "S3260", Justification = "Clase privada de mapeo")]
        public Mapping()
        {
            CreateMap<TipoVinculacion, TipoVinculacionDto>();
        }
    }

}
