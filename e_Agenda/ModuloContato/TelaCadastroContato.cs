using e_Agenda.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ITelaCadastravel
    {
        private readonly Notificador notificador;
        private readonly IRepositorio<Contato> repositorioContato;

        public TelaCadastroContato(Notificador notificador, IRepositorio<Contato> repositorioContato) : base("Gerenciamento de Contatos")
        {
            this.notificador = notificador;
            this.repositorioContato = repositorioContato;
        }
        public void InserirRegistro()
        {
            MostrarTitulo("Inserir Contato");

            Contato novoContato = ObterContato();

            // validação do contato

            repositorioContato.Inserir(novoContato);

        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Contato");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if(temContatosCadastrados == false)
            {
                notificador.ApresentarMensagem("Não há nenhum contato cadastrado para editar.", TipoMensagem.Atencao);
                return;
            }

            int idContato = ObterIdContato();

            Contato contatoAtualizado = ObterContato();
            contatoAtualizado.id = idContato;

            bool conseguiuEditar = repositorioContato.Editar(idContato, contatoAtualizado);

            if (!conseguiuEditar)
                notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Contato editado com sucesso", TipoMensagem.Sucesso);

        }
        
        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Contato");

            bool temContatoCadastrado = VisualizarRegistros("Pesquisando");

            if (temContatoCadastrado == false)
            {
                notificador.ApresentarMensagem("Não há nunhum contato cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int idContato = ObterIdContato();

            bool conseguiuExcluir = repositorioContato.Excluir(idContato);

            if (!conseguiuExcluir)
                notificador.ApresentarMensagem("Não foi possível excluir", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem("Contato excluído com sucesso", TipoMensagem.Sucesso);

        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Compromissos.");

            List<Contato> contatos = repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                notificador.ApresentarMensagem("Não há nenhum contato no momento.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato contato in contatos)
                Console.WriteLine(contato.ToString());

            Console.ReadLine();

            return true;
        }

        #region Métodos Privados
        private int ObterIdContato()
        {
            int idContato;
            bool numeroCadastroEncontrado;

            do
            {
                Console.Write("Digite o id do contato que deseja selecionar: ");
                idContato = Convert.ToInt32(Console.ReadLine());

                numeroCadastroEncontrado = repositorioContato.ExisteRegistro(idContato);

                if (numeroCadastroEncontrado == false)
                    notificador.ApresentarMensagem("Id de contato não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroCadastroEncontrado == false);

            return idContato;
        }

        private Contato ObterContato()
        {
            Console.WriteLine("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o email: ");
            string email = Console.ReadLine();

            Console.WriteLine("Digite o telefone: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("Digite o nome da empresa em que trabalha: ");
            string empresa = Console.ReadLine();

            Console.WriteLine("Digite o cargo da pessoa: ");
            string cargo = Console.ReadLine();

            Contato novoContato = new Contato(nome, email, telefone, empresa, cargo);
            return novoContato;

        }
        #endregion
    }
}
