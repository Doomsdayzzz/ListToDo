using ListToDo.Models;
using MongoDB.Bson;
using MongoDB.Driver;
//DATA ACCESS LAYER. Слой работы с базой. Уровень данных.

namespace ListToDo.DAL;

public class DbContext: ICrud<TaskToDo_Model>{

    //private const string ConnectingString = "mongodb://root:1234@127.0.0.1:27017/db1?authSource=userDb";
    private const string ConnectingString = "mongodb://root:1234@127.0.0.1:27017";
    private readonly MongoClient _client;   //подключение к БД
    private const string nameDB = "test"; 
    private const string nameColDB = "test";
    
    public DbContext() {
        _client=new MongoClient(ConnectingString);  //инициализация подключения к БД
    }
    
    public IEnumerable<TaskToDo_Model> GetAll() {
        
        //var taskList=new List<TaskToDo_Model>();
        IMongoDatabase database = _client.GetDatabase(nameDB);
        var collection = database.GetCollection<TaskToDo_Model>(nameColDB);
        
        var cursorTaskList = collection.Find(new BsonDocument());
        return cursorTaskList.ToEnumerable();
        
    }

    public void Insert(IEnumerable<TaskToDo_Model> entity) {
        List<TaskToDo_Model> arr = entity.ToList();
        
        IMongoDatabase database = _client.GetDatabase(nameDB);
        var collection = database.GetCollection<TaskToDo_Model>(nameColDB);
        //добавить проверку на 0
        if (arr.Count!=0)
            collection.InsertMany(arr);
        else return;
    }

    public void Update(TaskToDo_Model entity) {
        throw new NotImplementedException();
    }

    public void Delete(TaskToDo_Model entity) {
        throw new NotImplementedException();
    }
}