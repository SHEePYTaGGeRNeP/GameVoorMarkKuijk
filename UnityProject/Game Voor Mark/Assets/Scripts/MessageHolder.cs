using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class MessageHolder
    {
        #region "Fields"

        private string message;
        private MessageHolder child;

        #endregion

        #region "Constructors"

        public MessageHolder(string message)
        {
            this.message = message;
        }

        #endregion

        #region "Properties"

        public string Message
        {
            get { return message; }
        }

        public MessageHolder Child
        {
            get { return child; }
        }

        #endregion

        #region "Methods"

        public MessageHolder AddChild(string message)
        {
            MessageHolder m = new MessageHolder(message);
            child = m;
            return m;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
