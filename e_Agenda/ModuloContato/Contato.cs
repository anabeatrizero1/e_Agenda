using e_Agenda.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ModuloContato
{
    public class Contato : Entidade
    {
        public Contato(String nome, string email, string telefone, string empresa, string cargo)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Empresa = empresa;
            Cargo = cargo;

        }
        public string Nome { get;}
        public string Email { get; }
        public string Telefone { get; }
        public string Empresa { get; }
        public string Cargo { get; }

        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "Email: " + Email + Environment.NewLine +
                "Telefone: " + Telefone + Environment.NewLine +
                "Empresa: " + Empresa + Environment.NewLine +
                "Corgo: " + Cargo + Environment.NewLine;
            ;
        }



    }
}
