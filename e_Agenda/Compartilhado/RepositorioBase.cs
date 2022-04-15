using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.Compartilhado
{
    public class RepositorioBase<T> : IRepositorio<T> where T : Entidade
    {
        protected readonly List<T> registros;
        protected int contadorId;

        public RepositorioBase()
        {
            registros = new List<T>();
        }
        public string Inserir(T entidade)
        {
            entidade.id = ++contadorId;

            registros.Add(entidade);    

            return "REGISTRO_VALIDO";

        }
        public bool Editar(int idSelecionado, T novaEntidade)
        {
            
            foreach (T entidade in registros)
            {
                if(idSelecionado == entidade.id)
                {
                    novaEntidade.id = entidade.id;

                    int posicaoParaEditar = registros.IndexOf(entidade);

                    registros[posicaoParaEditar] = entidade;
                    
                    return true;
                }
            }
            return false;
        }

        public bool Excluir(int idSelecionado)
        {
            foreach(T entidade in registros)
            {
                if(idSelecionado == entidade.id)
                {
                    registros.Remove(entidade);
                    return true;
                }
            }
            return false;

        }

        public bool ExisteRegistro(int idSelecionado)
        {
            foreach (T entidade in registros)
                if (idSelecionado == entidade.id)
                    return true;

            return false;
        }

        public T SelecionarRegistro(int idSelecionado)
        {
            foreach (T entidade in registros)
            {
                if (idSelecionado == entidade.id)
                    return entidade;
            }

            return null;
        }

        public List<T> SelecionarTodos()
        {
            return registros;
        }
    }
}