using System;
using System.Collections.Generic;
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
            t.Create("test");

            currentTasks.Add(t);
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Awake()
        {
            instance = this;

            currentTasks = new List<Task>();
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
