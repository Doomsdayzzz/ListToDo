namespace ListToDo.DAL;

public interface ICrud<T>{
    
    public IEnumerable<T> GetAll();
    public void Insert(IEnumerable<T> entity);
    public void Update(T entity);
    public void Delete(T entity);
}