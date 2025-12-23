using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;
using System.Windows.Documents;
using ListToDo.BLL;

namespace ListToDo.Models;

public class TaskToDo_UI {
    [JsonPropertyName("name_task")]
    public required string? NameTask { get; set; }
    [JsonPropertyName("description_task")]
    public required string? DescriptionTask { get; set; }
    [JsonPropertyName("priority_task")]
    public required int? PriorityTask { get; set; }
    [JsonPropertyName("date_task")]
    public required DateTime DueDate { get; set; }
    
    // public static IEnumerable<TaskToDo_UI>? Load(string path = "tasks.json") {
    //     if (!File.Exists(path)) { return null; }
    //     var json= File.ReadAllText(path);
    //     if (json == null || json=="") return null;
    //     var tasks = JsonSerializer.Deserialize<IEnumerable<TaskToDo_UI>>(json);
    //     return tasks;
    // }
    public static IEnumerable<TaskToDo_UI>? Load(Service _serv) {
        var tasksFromDb =  _serv.GetAll();
        //List<TaskToDo_UI> taskUI = new List<TaskToDo_UI>();
        foreach (var task in tasksFromDb) {
            yield return new TaskToDo_UI() {
                NameTask = task.Name,
                DescriptionTask = task.Description,
                PriorityTask = int.Parse(task.Priority),
                DueDate = DateTime.Parse(task.DueDate.Replace('.', '/'))
            };
        }
    }

    public static void Save(IEnumerable<TaskToDo_UI> tasks, string path = "tasks.json") {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(path, json);
    }
}
