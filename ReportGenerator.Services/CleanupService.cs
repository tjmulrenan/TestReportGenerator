using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Services
{
    public class CleanupService
    {
        public const string WordProcessName = "WINWORD";

        private readonly IProcessService _processService;
         
        public CleanupService(IProcessService processService)
        {
            _processService = processService;
        }

        private IProcess[] GetWordInstances()
        {
            return _processService.GetProcessesByName(WordProcessName);
        }

        public bool HasActiveWordInstances() => GetWordInstances().Any();

        public void KillWordInstances()
        {
            foreach (var process in GetWordInstances())
            {
                process.Kill();
            }
        }
    }
}
