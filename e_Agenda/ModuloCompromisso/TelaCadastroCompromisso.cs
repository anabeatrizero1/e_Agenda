using e_Agenda.Compartilhado;
using e_Agenda.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ModuloCompromisso
{
    public class TelaCadastroCompromisso : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Compromisso> repositorioCompromisso;
        private readonly IRepositorio<Contato > repositorioContato;
        public TelaCadastroCompromisso(Notificador notificador, IRepositorio<Compromisso> repositorioCompromisso, IRepositorio<Contato> repositorioContato) : base("Gerenciamento de Compromissos")
        {
            this.notificador = notificador;
            this.repositorioCompromisso = repositorioCompromisso;
            this.repositorioContato = repositorioContato;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Agendar Compromisso");

            Compromisso compromisso = ObterCompromisso();

            // fazer verificação 

            repositorioCompromisso.Inserir(compromisso);
            
        }
        public void EditarRegistro()
        {
            MostrarTitulo("Editando Compromisso");

            bool temCompromissoCadastrado = VisualizarRegistros("Pesquisando");

            if(temCompromissoCadastrado == false)
            {
                notificador.ApresentarMensagem("Não há nunhum compromisso agendado para editar.", TipoMensagem.Atencao);
                return;
            }

            int idCompromisso = ObterIdCompromisso();

            Compromisso compromissoAtualizado = ObterCompromisso();
            compromissoAtualizado.id = idCompromisso;

            bool conseguiuEditar = repositorioCompromisso.Editar(idCompromisso, compromissoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Compromisso editado com sucesso", TipoMensagem.Sucesso);


        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temCompromissoCadastrado = VisualizarRegistros("Pesquisando");

            if (temCompromissoCadastrado == false)
            {
                notificador.ApresentarMensagem("Não há nunhum compromisso agendado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int idCompromisso = ObterIdCompromisso();

            bool conseguiuExcluir = repositorioCompromisso.Excluir(idCompromisso);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Compromisso excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if(tipo == "Tela")
                MostrarTitulo("Visualização de Compromissos.");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum compromisso no momento.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;

        }

        #region Métodos Privados
        private Compromisso ObterCompromisso()
        {
            Console.Write("Digite o titulo do compromisso: ");
            string titulo = Console.ReadLine();

            Console.Write("Digite o local do compromisso: ");
            string local = Console.ReadLine();

            Console.Write("Digite a data do compromisso: ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Digite o horário de inicio do compromisso: (ex. 14:30)");
            string[] strHoraInicio = Console.ReadLine().Split(':');
            TimeSpan horaInicio = new TimeSpan(int.Parse(strHoraInicio[0]), int.Parse(strHoraInicio[1]), 0);

            Console.Write("Digite o horário de termino: (ex. 16:00)");
            string[] strHoraFinal = Console.ReadLine().Split(':');
            TimeSpan horaFim = new TimeSpan(int.Parse(strHoraFinal[0]), int.Parse (strHoraFinal[1]), 0);
        

            Console.Write("Deseja adicionar um contato? (S - sim/ N - não)");
            char adicionarContato = Convert.ToChar(Console.ReadLine().ToUpper());
            Contato contato = null;
            if (adicionarContato == 'S')
            {
                bool temContatoCadastrado = VisualizarRegistros("Pesquisando");
                if (temContatoCadastrado)
                {
                    Console.WriteLine("Digite o id do contato");
                    int idContato = Convert.ToInt32(Console.ReadLine());
                    contato = repositorioContato.SelecionarRegistro(idContato);
                }
            }

            Compromisso novoCompromisso = new Compromisso(titulo, local, data, horaInicio, horaFim, contato);
            return novoCompromisso;
        }
        private int ObterIdCompromisso()
        {
            int idCompromisso;
            bool numeroCadastroEncontrado;

            do
            {
                Console.Write("Digite o id do compromisso que deseja selecionar: ");
                idCompromisso = Convert.ToInt32(Console.ReadLine());

                numeroCadastroEncontrado = repositorioCompromisso.ExisteRegistro(idCompromisso);

                if (numeroCadastroEncontrado == false)
                    notificador.ApresentarMensagem("Número de cadastro não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroCadastroEncontrado == false);

            return idCompromisso;
        }

        #endregion

    }
}
