using System;

namespace Common.Interfaces
{
    public interface IRibbonService
    {
        void RegisterRibbonCommand(string tabName, string groupName, string label, Action commandImplementation, Func<bool> active);
    }
}
