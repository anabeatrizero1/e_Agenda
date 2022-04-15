using e_Agenda.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ModuloTarefa
{
    public class Tarefa : Entidade
    {
        public List<Item> checklist;
      
        public Tarefa(String titulo, DateTime dataCriacao, List<Item> checklist)
        {
            Titulo = titulo;
            DataCriacao = dataCriacao;
            this.checklist = checklist;
        }
        public Tarefa(EnumPrioridade prioridade)
        {
            Prioridade = prioridade;

        }
        public double Percentual { get; private set; }

        public String Titulo { get;}

        public DateTime DataCriacao { get;}

        public EnumPrioridade Prioridade { get; set; }

        public bool StatusPendencia { get; set; }

        public void CalcularPercentualConcluido()
        {
            int itensFeitos = checklist.FindAll(x => x.pendencia.Equals("FEITO")).Count;

            Percentual = + (itensFeitos * 100) / checklist.Count;

            if(Percentual == 100)
                StatusPendencia = false;
           
        }
        public override string ToString()
        {
            return "Id " + id + Environment.NewLine +
                "Titulo: " + Titulo + Environment.NewLine +
                "Data de Criação: " + DataCriacao.ToShortDateString() + Environment.NewLine +
                "Percentual de Conclusão: " + Percentual + "%" +Environment.NewLine
                ;
        }
    }
}
