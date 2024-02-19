using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace toDoList
{

    public class Todo
    {
        public int Id { get; set; }
        private string _task;


        public string Task
        {
            get
            {
                return _task;
            }
            set
            {
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentException("task description length must be 1-100 characters long");
                }
                _task = value;
            }
        }
        private int _difficulty;
        public int Difficulty { get {
                return _difficulty;
            }
            set {
                if(value < 1 || value > 5) {
                    throw new ArgumentException("difficulty must fall in 1-5 range");
                }
                _difficulty = value;
            }
        }


        public Todo(string task, int difficulty, DateTime dueDate, StatusEnum status)
        {
            this.Task = task;
            this.Difficulty = difficulty;
            this.DueDate = dueDate;
            this.Status = status;
        }

        public Todo()
        {
        }

        private DateTime _dueDate;
        public DateTime DueDate {
            get
            {
                return _dueDate;
            }
            set {
                if(value.Year <1900 || value.Year > 2099)
                {
                    throw new ArgumentException("invalid year, must be between 1900-2099");
                }
                _dueDate = value;
            }
        }
        public enum StatusEnum
        {
            pending=0,
            done=1,
            delegated=2
        }
        public StatusEnum Status { get; set; }  

    }

}
