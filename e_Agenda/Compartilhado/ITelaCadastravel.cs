using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Compartilhado
{
    public interface ITelaCadastravel
    {
        void InserirRegistro();
        void EditarRegistro();
        void ExcluirRegistro();
        bool VisualizarRegistros(string tipoVisualizado);
    }
}
