using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Promociones
{
    public interface IPromocionesService
    {
        bool InsertarPromocion(Promocion promocion);
        bool ActualizarPromocion(Promocion promocion);
        bool BorrarPromocion(int idPromocion);
    }
}
