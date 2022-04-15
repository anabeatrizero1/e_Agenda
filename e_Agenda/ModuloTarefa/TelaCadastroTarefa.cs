using e_Agenda.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly RepositorioTarefa repositorioTarefa;

        public TelaCadastroTarefa(Notificador notificador, RepositorioTarefa repositorioTarefa) : base("Gerenciamento de Tarefas")
        {
            this.notificador = notificador;
            this.repositorioTarefa = repositorioTarefa ;
        }
        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar Tarefas Pendentes");
            Console.WriteLine("Digite 5 para Visualizar Tarefas Concluidas");
            Console.WriteLine("Digite 6 para atualizar percentuais de Taredas Pendentes");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();
            return opcao;

        }
        public void InserirRegistro()
        {
            MostrarTitulo("Inserir Tarefa");

            Tarefa novaTarefa = ObterTarefa();

            novaTarefa.StatusPendencia = true;

            repositorioTarefa.Inserir(novaTarefa);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Tarefa");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                notificador.ApresentarMensagem("Não há nenhuma Tarefa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }
          
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Tarefa");

            bool temContatoCadastrado = VisualizarRegistros("Pesquisando");

            if (temContatoCadastrado == false)
            {
                notificador.ApresentarMensagem("Não há nunhuma tarefa cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int idTarefa = ObterIdTarefa();

            bool conseguiuExcluir = repositorioTarefa.Excluir(idTarefa);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Tareda excluída com sucesso", TipoMensagem.Sucesso);

        }

        public bool VisualizarRegistros(string tipo)
        {
            List<Tarefa> tarefas = repositorioTarefa.SelecionarTodos();
            tarefas = repositorioTarefa.SelecionarTarefasPendentes();

            if (tarefas.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhuma no momento.", TipoMensagem.Atencao);
                Console.ReadKey();
                return false;
            }

            if (tipo == "Pendentes")
            {
                List<Tarefa> taredasPendentes = repositorioTarefa.SelecionarTarefasPendentes();
                foreach (Tarefa tarefa in taredasPendentes)
                    Console.WriteLine(tarefa.ToString());

                Console.ReadLine();
            }
            if(tipo == "Concluidas")
            {
                List<Tarefa> taredasConcluidas = repositorioTarefa.SelecionarTarefasConcluidas();
            }
            return true;

        }

        public void AtualizarTarefasPendentes()
        {
            bool temTarefasPendentes = VisualizarRegistros("Pendentes");
            if (temTarefasPendentes == false)
            {
                notificador.ApresentarMensagem("Não há nenhuma tarefa pendente.", TipoMensagem.Atencao);
                Console.ReadLine();
                return;
            }
            Console.Write("Digite o id da Tarefa que deseja atualizar pendencia: ");
            int idTarefa = Convert.ToInt32(Console.ReadLine());
            Tarefa tarefa = (Tarefa)repositorioTarefa.SelecionarRegistro(idTarefa);

            VisualizarCheckList(tarefa);
            Console.WriteLine("Digite o Id item que deseja atualizar: ");
            int idItem = Convert.ToInt32(Console.ReadLine());
            Item item = repositorioTarefa.SelecionarItem(idItem, tarefa.checklist);

            Console.WriteLine("Escolha o status desse item: [1 - Fazer] [2 - Fazendo] [3 - Feito] ");
            int statusItem = Convert.ToInt32(Console.ReadLine());


            if (statusItem == 1)
                item.pendencia = "FAZER";
            else if (statusItem == 2)
                item.pendencia = "FAZENDO";
            else if (statusItem == 3)
                item.pendencia = "FEITO";

            tarefa.CalcularPercentualConcluido();
           
        }

        public void VisualizarCheckList(Tarefa tarefa)
        {
            List<Item> list = tarefa.checklist;

            foreach(Item item in list)
            {
                item.ToString();
            }
        }
        private Tarefa ObterTarefa()
        {
            Tarefa novaTarefa;
            Console.Write("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.WriteLine(" 1 - prioridade Baixa");
            Console.WriteLine(" 2 - prioridade Normal");
            Console.WriteLine(" 3 - prioridade Alta");
            Console.WriteLine("Escolha a prioridade");
            string opcao = Console.ReadLine();

            if (opcao == "1")
                novaTarefa = new(EnumPrioridade.Baixa);
            else if (opcao == "2")
                novaTarefa = new(EnumPrioridade.Normal);
            else if (opcao == "3")
                novaTarefa = new(EnumPrioridade.Alta);

            List<Item> checklist = ObterChecklist();

            novaTarefa = new (titulo, DateTime.Now.Date, checklist);

            return novaTarefa;
        }

        private List<Item> ObterChecklist()
        {
            int id = 0;
            List<Item> checklist = new List<Item>();
            do
            {
                Console.WriteLine("Adicione um item ao CheckList da tarefa");

                Console.Write("Descrição: ");
                string descricao = Console.ReadLine();
                id++;

                checklist.Add(new Item(descricao, id));
                Console.Write("Deseja adicionar mais itens (S - sim | N - não)");
                char opcao = Convert.ToChar(Console.ReadLine().ToUpper());

                if (opcao == 'S')
                    continue;
                else
                    break;

            } while (true);

            return  checklist;
        }

        private int ObterIdTarefa()
        {
            int idTarefa;
            bool numeroCadastroEncontrado;

            do
            {
                Console.Write("Digite o id da tarefa que deseja selecionar: ");
                idTarefa = Convert.ToInt32(Console.ReadLine());

                numeroCadastroEncontrado = repositorioTarefa.ExisteRegistro(idTarefa);

                if (numeroCadastroEncontrado == false)
                    notificador.ApresentarMensagem("Id de contato não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroCadastroEncontrado == false);

            return idTarefa;
        }

    }
}
