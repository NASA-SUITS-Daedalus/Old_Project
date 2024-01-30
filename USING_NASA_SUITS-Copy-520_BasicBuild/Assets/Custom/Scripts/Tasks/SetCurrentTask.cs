using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TaskNS;
using TMPro;

public class SetCurrentTask : MonoBehaviour
{
    //private Task currentTask;
    public TextMeshPro currentTaskDesc;
    List<Task> allTasks = new List<Task>();

    // Start is called before the first frame update
    void Start()
    {
        buildFirstTasks();
        currentTaskDesc.text = "No current task set...";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Build all tasks that are known from the task
    // Will need to change this depending on how tasks are received
    // Just testing things out for now
    private void buildFirstTasks()
    {
        List<string> test = new List<string>
        {"Egress", "Navigation", "Data Collection" };
        foreach (var line in test)
            { Task newTask = buildTask(line);
            allTasks.Add(newTask);
            }
        //var lines = File.ReadLines("Assets/Custom/Scripts/Tasks/SampleTasks.txt");
        // foreach (var line in lines)
        //  {
        //      Task newTask = buildTask(line);
        //allTasks.Add(newTask);
      //  }

    }

    // Build an individual task
    private Task buildTask(string desc)
    {
        return new Task(desc);
    }

    public void setCurrentTask(int taskIndex)
    {
        currentTaskDesc.text = allTasks[taskIndex].getDesc();
    }
}
