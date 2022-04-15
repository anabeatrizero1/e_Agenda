using e_Agenda.Compartilhado;
using e_Agenda.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ModuloCompromisso
{
    public class Compromisso : Entidade
    {
        public Compromisso(string assunto, string local, DateTime dataCompromisso, TimeSpan horaInicio, TimeSpan horaFim, Contato contato)
        {
            Assunto = assunto;
            Local = local;
            DataCompromisso = dataCompromisso;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
            Contato = contato;
        }
        public string Assunto { get;}
        public string Local { get;}
        public DateTime DataCompromisso { get;}
        public TimeSpan HoraInicio { get;}
        public TimeSpan HoraFim { get;}
        public Contato Contato { get;}

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Local: " + Local + Environment.NewLine +
                "Data do Compromisso: " + DataCompromisso.ToShortDateString() + Environment.NewLine +
                "Horário: " + HoraInicio.Hours + Environment.NewLine +
                "Termino: " + HoraFim.Hours + Environment.NewLine +
                "Contato: " + Contato.Nome + Environment.NewLine;
            ;
        }
    }
}
