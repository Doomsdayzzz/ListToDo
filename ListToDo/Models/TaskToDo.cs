using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace ListToDo.Models;

public class TaskToDo {
    [JsonPropertyName("name_task")]
    public required string? NameTask { get; set; }
    [JsonPropertyName("description_task")]
    public required string? DescriptionTask { get; set; }
    [JsonPropertyName("priority_task")]
    public required int? PriorityTask { get; set; }
    [JsonPropertyName("date_task")]
    public required DateTime DueDate { get; set; }

    public static IEnumerable<TaskToDo>? Load(string path = "tasks.json") {
        if (!File.Exists(path)) { return null; }
        var json= File.ReadAllText(path);
        if (json == null || json=="") return null;
        var tasks = JsonSerializer.Deserialize<IEnumerable<TaskToDo>>(json);
        return tasks;
    }

    public static void Save(IEnumerable<TaskToDo> tasks, string path = "tasks.json") {
        var json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(path, json);
    }
}
