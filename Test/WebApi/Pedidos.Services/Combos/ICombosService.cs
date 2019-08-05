using Pedidos.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.Combos
{
    public interface ICombosService
    {
        ICollection<Combo> GetCombos();
        Combo GetCombo(int idCombo);
        bool InsertarCombo(Combo combo);
        bool ActualizarCombo(Combo combo);
        bool EliminarCombo(int id);
    }
}
