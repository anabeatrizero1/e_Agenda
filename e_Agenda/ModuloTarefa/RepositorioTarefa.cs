using e_Agenda.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ModuloTarefa
{
    public class RepositorioTarefa : RepositorioBase<Tarefa>
    {
        public List<Tarefa> SelecionarTarefasPendentes()
        {
            List<Tarefa> taredasPendentes = new List<Tarefa>();
            foreach( Tarefa registro in registros)
            {
                if(registro.StatusPendencia == true) 
                    taredasPendentes.Add(registro);

            }
            return taredasPendentes;
        }
        public List<Tarefa> SelecionarTarefasConcluidas()
        {
            List<Tarefa> taredasConcluidas = new List<Tarefa>();
            foreach (Tarefa registro in registros)
            {
                if (registro.StatusPendencia == false)
                    taredasConcluidas.Add(registro);

            }
            return taredasConcluidas; 
        }

        public Item SelecionarItem(int idItem, List<Item> checkList)
        {
            foreach (Item item in checkList)
            {
                if (item.id == idItem)
                {
                    return item;
                }
            }
            return null;

        }

    }

    
}
