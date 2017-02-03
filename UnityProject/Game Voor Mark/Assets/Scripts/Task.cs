using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class Task : MonoBehaviour
    {
        #region "Fields"

        public string Message;
        public TaskState State;

        private bool destroy;
        private float totalTime;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public void Create(string message)
        {
            this.Message = message;
            this.State = TaskState.Todo;

            Text text = GetComponentInChildren<Text>();
            text.text = message;
        }

        public void NextState()
        {
            State = (TaskState)(((int)State) + 1);
        }

        public void Destroy()
        {
            destroy = true;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Update()
        {
            if (destroy)
            {
                totalTime += Time.deltaTime;
                if (totalTime >= 1f)
                {
                    Destroy(gameObject);
                }
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
