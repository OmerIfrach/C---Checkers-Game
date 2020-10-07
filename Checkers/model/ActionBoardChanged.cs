using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers.model
{
    public class ActionBoardChanged
    {
        private event Action<string> ReportGameObjectDelegate;

        public ActionBoardChanged(Action<string> i_Listener)
        {
            ReportGameObjectDelegate += i_Listener;
        }

        public void TriggerChange(string i_ListenerNewSymbol)
        {
            if(this.ReportGameObjectDelegate != null)
            {
                this.ReportGameObjectDelegate.Invoke(i_ListenerNewSymbol);
            }
        }
    }
}
