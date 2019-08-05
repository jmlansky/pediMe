using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Repository.Promociones
{
    public interface IPromocionesRepository
    {
        bool InsertarPromocion(Promocion promocion);
        bool ActualizarPromocion(Promocion promocion);
        bool BorrarPromocion(int idPromocion);
    }
}
