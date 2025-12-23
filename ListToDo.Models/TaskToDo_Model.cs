using MongoDB.Bson.Serialization.Attributes;

namespace ListToDo.Models;
using MongoDB.Bson;
public class TaskToDo_Model{
    public  ObjectId Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("description")]
    public string Description { get; set; }
    [BsonElement("priority")]
    public string Priority { get; set; }
    [BsonElement("dueDate")]
    public string DueDate { get; set; }
}