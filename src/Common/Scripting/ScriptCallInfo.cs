using System;
namespace FreeSCADA.Common.Scripting
{
    public class ScriptCallInfo
    {
        private string className;
        private string handlerName;
        public string ClassName
        {
            get
            {
                return this.className;
            }
            set
            {
                this.className = value;
            }
        }
        public string HandlerName
        {
            get
            {
                return this.handlerName;
            }
            set
            {
                this.handlerName = value;
            }
        }
    }
}
