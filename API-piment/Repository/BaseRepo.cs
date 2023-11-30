using API_piment.Repository.Interfaces;

using Dapper;

using Microsoft.Data.SqlClient;

namespace API_piment.Repository
{
    public class BaseRepo<E,M> : IRepo<E, M>
        where E : class
        where M : class
    {

        //ajouter la connection a la DB

        private readonly SqlConnection? _conn = null;
        private readonly string str = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Db-piments;Integrated Security=True;";

        public BaseRepo()
        {
            _conn = new SqlConnection(str);
        }

        public bool Create(M model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(M model)
        {
            throw new NotImplementedException();
        }

        public bool Update(M model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<E> GetAll() //---------> Check pour levé un exception si le result est null
        {
            string sql = "SELECT * FROM [Piments]";

            IEnumerable<E>? items = _conn?.Query<E>(sql).ToList();

             return items;

        }

        public E GetById(int id)
        {
            throw new NotImplementedException();
        }

        public E GetByname(string name)
        {
            throw new NotImplementedException();
        }
    }
}
