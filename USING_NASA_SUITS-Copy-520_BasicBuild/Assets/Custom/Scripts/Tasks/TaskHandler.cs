using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TaskNS;
using TMPro;

public class TaskHandler : MonoBehaviour
{

    private Task currentTask;
    public TextMeshPro currentTaskDesc;
    List<Task> allTasks = new List<Task>();
    List<Task> incompleteTasks = new List<Task>();

    // Start is called before the first frame update
    void Start()
    {
        // build all known tasks?
        buildFirstTasks();
    }

    // Update is called once per frame
    void Update()
    {
        updateTaskDisplay();
    }

    // Build all tasks that are known from the task
    // Will need to change this depending on how tasks are received
    // Just testing things out for now
    private void buildFirstTasks()
    {
        var lines = File.ReadLines("Assets/Custom/Scripts/Tasks/SampleTasks.txt");
        foreach (var line in lines)
        {
            Task newTask = buildTask(line);
            allTasks.Add(newTask);
            incompleteTasks.Add(newTask);
        }

        // change later? set the current task to the first member of the incomplete list
        currentTask = incompleteTasks[0];
        updateTaskDisplay();
    }

    // Build an individual task
    private Task buildTask(string desc)
    {
        return new Task(desc);
    }

    // Update the task description in the HUD
    private void updateTaskDisplay()
    {
        currentTaskDesc.text = currentTask.getDesc();
    }

    // Set the current task as complete and move onto the next one
    // This should be triggered when the complete button is clicked on the display
    // This is another method that can change, this is just for really basic testing
    public void completeCurrentTask()
    {
        // If there are no tasks left to complete...
        if (incompleteTasks.Count <= 1)
        {
            currentTaskDesc.text = "All tasks complete!";

            // If the current task is the last one...
            if (currentTask != null)
            {
                currentTask.markComplete();
                incompleteTasks.RemoveAt(0);    // This should be empty now
                currentTask = null;
            }

            return;
        }

        // Otherise, update the current task and move onto the next one
        currentTask.markComplete(); 
        incompleteTasks.RemoveAt(0);
        currentTask = incompleteTasks[0];
        updateTaskDisplay();

    }


}
