using e_Agenda.ModuloCompromisso;
using e_Agenda.ModuloContato;
using e_Agenda.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private string opcaoSelecionada;

        //Declaração de tarefas 
        private RepositorioTarefa repositorioTarefa;
        private TelaCadastroTarefa telaCadastroTarefa;

        //Declaração de Contatos
        private IRepositorio<Compromisso> repositorioCompromisso;
        private TelaCadastroCompromisso telaCadastroCompromisso;

        //Declaracao de contatos
        private IRepositorio<Contato> repositorioContato;
        private TelaCadastroContato telaCadastroContato;

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioTarefa = new RepositorioTarefa();
            repositorioContato = new RepositorioContato();
            repositorioCompromisso = new RepositorioCompromisso();

            telaCadastroTarefa = new TelaCadastroTarefa(notificador, repositorioTarefa);//**
            telaCadastroContato = new TelaCadastroContato(notificador, repositorioContato);//***
            telaCadastroCompromisso = new TelaCadastroCompromisso(notificador, repositorioCompromisso, repositorioContato);
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("e_Agenda");

            Console.WriteLine("Digite 1 para Gerenciar Tarefas");
            Console.WriteLine("Digite 2 para Gerenciar Contatos ");
            Console.WriteLine("Digite 3 para Gerenciar Compromissos");

            Console.WriteLine("Digite s para sair");

            opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroTarefa;
            else if (opcao == "2")
                tela = telaCadastroContato;
            else if (opcao == "3")
                tela = telaCadastroCompromisso;

            return tela;
        }



    }
}


