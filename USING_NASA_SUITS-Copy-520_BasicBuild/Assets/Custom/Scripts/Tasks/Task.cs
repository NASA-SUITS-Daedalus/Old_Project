using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TaskNS 
{ 
    public class Task
    {

        private string desc;
        private bool isComplete;

        // Constructor for a Task object
        public Task(string desc)
        {
            this.desc = desc;
            this.isComplete = false;
        }

        // Change the string representation of a task
        public override string ToString()
        {
            return this.desc;
        }

        // Check the task's description
        public string getDesc()
        {
            return this.desc;
        }


        // Mark complete
        public void markComplete()
        {
            this.isComplete = true;
        }

        // Mark incomplete
        public void markIncomplete()
        {
            this.isComplete = false;
        }

        // Check completion status
        public bool hasBeenCompleted()
        {
            return this.isComplete;
        }
    }
}