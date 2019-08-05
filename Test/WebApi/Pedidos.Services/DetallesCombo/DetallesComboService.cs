using Pedidos.Repository.DetallesCombo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pedidos.Services.DetallesCombo
{
    public class DetallesComboService: IDetallesComboService
    {
        private readonly IDetallesComboRepository detallesComboRepository;
        public DetallesComboService(IDetallesComboRepository detallesComboRepository)
        {
            this.detallesComboRepository = detallesComboRepository;
        }
    }
}
