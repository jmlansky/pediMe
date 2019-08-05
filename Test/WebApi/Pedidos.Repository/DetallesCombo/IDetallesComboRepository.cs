using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Pedidos.Core;

namespace Pedidos.Repository.DetallesCombo
{
    public interface IDetallesComboRepository
    {
        ICollection<DetalleCombo> GetDetalles(int idCombo);
        bool InsertarDetalleCombo(DetalleCombo detC, long idCombo, SqlTransaction tran);
    }
}
