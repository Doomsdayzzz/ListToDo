using ListToDo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();
const string connectingString = "mongodb://root:1234@127.0.0.1:27017";
const string nameDb = "test";
const string nameColDb = "test";
MongoClient _client= new MongoClient(connectingString); //подключение к БД

app.MapPost("/add", Add);
app.MapGet("/getBD", GetAll);

app.Run();
Console.WriteLine("Сервер запущен");

void Add(TaskToDo_Model entity) {
    IMongoDatabase database = _client.GetDatabase(nameDb);
    var collection = database.GetCollection<TaskToDo_Model>(nameColDb);
    collection.InsertOneAsync(entity);
    Console.WriteLine("Добавлено: "+entity.Id);
}

IEnumerable<TaskToDo_Model> GetAll() {
        
    //var taskList=new List<TaskToDo_Model>();
    IMongoDatabase database = _client.GetDatabase(nameDb);
    var collection = database.GetCollection<TaskToDo_Model>(nameColDb);
    return collection.Find(new BsonDocument()).ToEnumerable();
    // var cursorTaskList = collection.FindAsync(new BsonDocument());
    // Console.WriteLine("Отправлено: "+cursorTaskList.Id);
    // return cursorTaskList.Result.ToJson();

}


// var builder = WebApplication.CreateBuilder(args);
//
// // Add services to the container.
// // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddOpenApi();
//
// var app = builder.Build();
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) {
//     app.MapOpenApi();
// }
//
// app.UseHttpsRedirection();
//
// var summaries = new[] {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };
//
// app.MapGet("/weatherforecast", () => {
//         var forecast = Enumerable.Range(1, 5).Select(index =>
//                 new WeatherForecast
//                 (
//                     DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                     Random.Shared.Next(-20, 55),
//                     summaries[Random.Shared.Next(summaries.Length)]
//                 ))
//             .ToArray();
//         return forecast;
//     })
//     .WithName("GetWeatherForecast");
//
// app.Run();
//
// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary){
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }