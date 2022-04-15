using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Compartilhado
{
    public interface IRepositorio<T> where T : Entidade
    {
        string Inserir(T entidade);
        bool Editar(int idSelecionado, T novaEntidade);
        bool Excluir(int idSelecionado);
        bool ExisteRegistro(int idSelecionado);
        T SelecionarRegistro(int idSelecionado);
        List<T> SelecionarTodos();
    }
}
