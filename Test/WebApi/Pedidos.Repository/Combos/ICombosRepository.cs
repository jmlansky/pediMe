using System;
using System.Collections.Generic;
using System.Text;
using Pedidos.Core;

namespace Pedidos.Repository.Combos
{
    public interface ICombosRepository
    {
        ICollection<Combo> GetCombos();
        Combo GetCombo(int idCombo);
        bool InsertarCombo(Combo combo);
        bool ActualizarCombo(Combo combo);
        bool EliminarCombo(int id);
    }
}
