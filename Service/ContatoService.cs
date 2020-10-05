using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniControl.Models;

namespace UniControl.Service
{
    public class ContatoService
    {
        public static IQueryable<Contato> Listar(AplicacaoDbContext db)
        {
            try
            {
                return db.Contato;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocorreu um erro ao listar os Contatos!", ex);
            }
        }

        public static Contato Listar(int id, AplicacaoDbContext db)
        {
            try
            {
                return db.Contato.Find(id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocorreu um erro ao listar o Contato!", ex);
            }
        }

        public static void Cadastrar(Contato cadastrar, AplicacaoDbContext db)
        {
            try
            {
                db.Contato.Add(cadastrar);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocorreu um erro ao cadastrar o Contato!", ex);
            }
        }

        public static void Atualizar(Contato atualizar, AplicacaoDbContext db)
        {
            try
            {
                var objeto = db.Contato.Find(atualizar.Id);
                if (objeto == null)
                {
                    throw new ArgumentException("Contato não encontrado!");
                }

                objeto.NomeFantasia = atualizar.NomeFantasia;
                objeto.Telefone = atualizar.Telefone;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocorreu um erro ao atualizar as informações do Contato!", ex);
            }
        }

        public static void Remover(int id, AplicacaoDbContext db)
        {
            try
            {
                var objeto = db.Contato.Find(id);
                if (objeto == null)
                {
                    throw new ArgumentException("Contato não encontrado!");
                }

                db.Contato.Remove(objeto);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocorreu um erro ao remover o Contato!", ex);
            }
        }

        public static void Remover(List<int> ids, AplicacaoDbContext db)
        {
            try
            {
                var objetos = db.Contato.Where(e => ids.Contains(e.Id));
                db.Contato.RemoveRange(objetos);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Ocorreu um erro ao remover os Contatos!", ex);
            }
        }
    }
}
