using e_Agenda.Compartilhado;
using e_Agenda.ModuloCompromisso;
using e_Agenda.ModuloContato;
using e_Agenda.ModuloTarefa;
using System;

namespace e_Agenda
{
    internal class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);   

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada == null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                    GerenciarTarefas(telaSelecionada, opcaoSelecionada);
                

                

            }

        }
        private static void GerenciarTarefas(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroTarefa telaCadastroTarefa = telaSelecionada as TelaCadastroTarefa;

            if (telaCadastroTarefa is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroTarefa.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroTarefa.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroTarefa.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroTarefa.VisualizarRegistros("Pendentes");

            else if (opcaoSelecionada == "5")
                telaCadastroTarefa.VisualizarRegistros("Concluidas");

            else if (opcaoSelecionada == "6")
                telaCadastroTarefa.AtualizarTarefasPendentes();

        }
        private static void GerenciarContatos(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroContato telaCadastroContato = telaSelecionada as TelaCadastroContato;

            if (telaCadastroContato is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroContato.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroContato.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroContato.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroContato.VisualizarRegistros("Tela");
        }
        private static void GerenciarCompromissos(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroCompromisso telaCadastroCompromisso = telaSelecionada as TelaCadastroCompromisso;

            if (telaCadastroCompromisso is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroCompromisso.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroCompromisso.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroCompromisso.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroCompromisso.VisualizarRegistros("Tela");
        }
    }
}
