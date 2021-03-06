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
        private MessageHolder tutorial;
        private bool tutorialDone;
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

        private void SpawnNewCard(bool tutorial = false)
        {
            GameObject go = Instantiate(TaskPrefab);
            Task t = go.GetComponent<Task>();

            if (lastSpawned == null && !tutorial)
            {
                MessageHolder[] possible = messages.Where(x => !currentTasks.Any(a => a.Message == x.Message)).ToArray();
                lastSpawned = possible[rand.Next(0, possible.Length)];
            }
            else if (tutorial)
                lastSpawned = this.tutorial;

            t.Create(lastSpawned.Message);

            lastSpawned = lastSpawned.Child;

            currentTasks.Add(t);
            GUIManager.instance.PlaceCard(go, t.State, GetCardCount(t.State));
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
                if (line.StartsWith("+"))
                {
                    tutorial = new MessageHolder(line.Remove(0, 1));
                    lastMessage = tutorial;
                }
                else if (line.StartsWith("-"))
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

        private void RemoveArchivedCards()
        {
            if (GetCardCount(TaskState.Archive) > 0)
            {
                Task[] tasks = currentTasks.Where(x => x.State == TaskState.Archive).ToArray();
                foreach (Task t in tasks)
                {
                    currentTasks.Remove(t);
                    t.Destroy();
                    GameManager.INSTANCE.score++;
                }
            }
        }

        private void CheckTaskCount()
        {
            if (GetCardCount(TaskState.Todo) > 6 || GetCardCount(TaskState.InProgress) > 6 || GetCardCount(TaskState.Review) > 6)
            {
                GameManager.INSTANCE.Lose();
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
            tutorialDone = false;
        }

        public void Start()
        {
            ParseFile();
        }

        public void Update()
        {
            if (GameManager.INSTANCE.Lost)
                return;

            if (!tutorialDone)
            {
                SpawnNewCard(true);
                tutorialDone = true;
            }
            else if (currentTasks.Count == 0 || pastTime >= SpawnTimer)
            {
                SpawnNewCard();

                pastTime = 0;
                return;
            }

            SpawnTimer -= 0.01f * Time.deltaTime;

            if (SpawnTimer <= 1f)
                SpawnTimer = 1f;

            CheckTaskCount();
            RemoveArchivedCards();

            pastTime += Time.deltaTime;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
