using ListToDo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
//DATA ACCESS LAYER. Слой работы с базой. Уровень данных.
namespace ListToDo.DAL;

public class DbContext: ICrud<TaskToDo_Model>{

    //private const string ConnectingString = "mongodb://root:1234@127.0.0.1:27017/db1?authSource=userDb";
    private const string ConnectingString = "mongodb://root:1234@127.0.0.1:27017";
    private readonly MongoClient _client;   //подключение к БД

    public DbContext() {
        _client=new MongoClient(ConnectingString);  //инициализация подключения к БД
    }
    
    public IEnumerable<TaskToDo_Model> GetAll() {
        //------BUG!!!------
        //var taskList=new List<TaskToDo_Model>();
        IMongoDatabase database = _client.GetDatabase("test");
        var collection = database.GetCollection<TaskToDo_Model>("test");
        //-----------???---------
        //var result=collection.Find("");
        return collection.Find(new BsonDocument()).ToList();
        
        //return(taskList);
    }

    public void Insert(TaskToDo_Model entity) {
        throw new NotImplementedException();
    }

    public void Update(TaskToDo_Model entity) {
        throw new NotImplementedException();
    }

    public void Delete(TaskToDo_Model entity) {
        throw new NotImplementedException();
    }
}