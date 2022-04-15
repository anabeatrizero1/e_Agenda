using e_Agenda.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ModuloTarefa
{
    public class Item 
    {
        public string pendencia = "FAZER";
        string descricao;
        int id;
        public Item(string descricao, int id)
        {
            this.descricao = descricao;
            this.id = id;   
        }

           
        public override string ToString()
        {
            return
                "\nId: " + id +
                "\nDescricao: " + descricao +
                "\nStatus do Item: " + pendencia;
                    
        }
    }
    
}
