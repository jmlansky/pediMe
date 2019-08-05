using Pedidos.Core;
using Pedidos.Repository.Promociones;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Promociones
{
    public class PromocionesService: IPromocionesService
    {
        private readonly IPromocionesRepository promocionesRepository;
        public PromocionesService(IPromocionesRepository promocionesRepository)
        {
            this.promocionesRepository = promocionesRepository;
        }

        public bool ActualizarPromocion(Promocion promocion)
        {
            return promocionesRepository.ActualizarPromocion(promocion);
        }

        public bool BorrarPromocion(int idPromocion)
        {
            return promocionesRepository.BorrarPromocion(idPromocion);
        }

        public bool InsertarPromocion(Promocion promocion)
        {
            return promocionesRepository.InsertarPromocion(promocion);
        }
    }
}
