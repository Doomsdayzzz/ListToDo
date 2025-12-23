using ListToDo.DAL;
using ListToDo.Models;
//СЛОЙ БИЗНЕС-ЛОГИКИ. здесь управление данными с базы данных
namespace ListToDo.BLL;

public class Service{
    
    private readonly ICrud<TaskToDo_Model>? _operations;

    public Service() {
        _operations = new DbContext();
    }

    public IEnumerable<TaskToDo_Model> GetAll() {
        return _operations.GetAll();
    }

    public void Add(TaskToDo_Model task) {
        _operations.Insert(task);
    }

    public void Update(TaskToDo_Model task) {
        _operations.Update(task);
    }

    public void Delete(TaskToDo_Model task) {
        _operations.Delete(task);
    }
}