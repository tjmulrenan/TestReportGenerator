using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Services
{
    public class ProcessService : IProcessService
    {
        public IProcess[] GetProcessesByName(string name)
        {
            return Process.GetProcessesByName(name).Select(o => new ProcessInfo(() => o.Kill())).ToArray();
        }
    }

    public class ProcessInfo : IProcess
    {
        Action _killMe;

        public ProcessInfo(Action killMe)
        {
            _killMe = killMe;
        }

        public void Kill()
        {
            _killMe?.Invoke();
        }
    }
}
