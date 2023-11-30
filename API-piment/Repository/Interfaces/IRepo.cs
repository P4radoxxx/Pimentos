namespace API_piment.Repository.Interfaces
{
    public interface IRepo<E,M>
        where E : class
        where M : class
    {

        IEnumerable<E> GetAll(); // retourne toute les entité associé
        E GetById(int id);          // retourne une entité sur base de son id
        E GetByname(string name);    // retourne une entité sur base de son nom

        bool Create(M model);
        bool Update(M model);
        bool Delete(M model);
    }
}
