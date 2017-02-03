﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class TaskManager : MonoBehaviour
    {
        #region "Fields"

        private List<Task> currentTasks;
        private float pastTime;
        private List<MessageHolder> messages;
        private MessageHolder lastSpawned;
        private System.Random rand;

        public GameObject TaskPrefab = null;
        public float SpawnTimer = 3f;

        #endregion

        #region "Constructors"



        #endregion

        #region "Singleton"

        private static TaskManager instance;

        public static TaskManager INSTANCE
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private void SpawnNewCard()
        {
            GameObject go = Instantiate(TaskPrefab);
            Task t = go.GetComponent<Task>();

            if (lastSpawned == null)
            {
                lastSpawned = messages[rand.Next(0, messages.Count)];
            }

            t.Create(lastSpawned.Message);

            lastSpawned = lastSpawned.Child;

            currentTasks.Add(t);
        }

        public int GetCardCount(TaskState state)
        {
            return currentTasks.Count(x => x.State == state);
        }

        public void ParseFile()
        {
            const string filename = @"MarkTeksten.txt";

            string[] lines = File.ReadAllLines(filename);

            MessageHolder lastMessage = null;
            foreach (string line in lines)
            {
                if (line == "")
                    continue;
                if (line.StartsWith("-"))
                {
                    lastMessage = lastMessage.AddChild(line.Remove(0, 1));
                }
                else
                {
                    lastMessage = new MessageHolder(line);
                    messages.Add(lastMessage);
                }
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Awake()
        {
            instance = this;

            currentTasks = new List<Task>();
            messages = new List<MessageHolder>();
            rand = new System.Random();
        }

        public void Start()
        {
            ParseFile();
        }

        public void Update()
        {
            if (currentTasks.Count == 0 || pastTime >= SpawnTimer)
            {
                SpawnNewCard();

                pastTime = 0;
                return;
            }

            pastTime += Time.deltaTime;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}