using NUnit.Framework;
using ReportGenerator.Services;
using System.Collections.Generic;
using System.Linq;

namespace ReportGenerator.Tests
{
    public class CleanUpServiceTests
    {
        private FakeProcessService _fakeProcessService = new FakeProcessService();
        private CleanupService _cleanUpService;

        [SetUp]
        public void Setup()
        {
            _cleanUpService = new CleanupService(_fakeProcessService);            
        }

        [TestCase]
        public void check_running_process_is_killed()
        {
            // Arrange

            var wordProcess = new FakeProcess(CleanupService.WordProcessName);

            _fakeProcessService.AddFakeProcess(wordProcess);

            // Act

            _cleanUpService.KillWordInstances();

            // Assert

            Assert.That(wordProcess.IsKilled, Is.True);
        }
        
        // Fakes for testing

        private class FakeProcessService : IProcessService
        {
            private readonly List<FakeProcess> _fakeProcesses = new List<FakeProcess>();

            public void AddFakeProcess(FakeProcess fakeProcess)
            {
                _fakeProcesses.Add(fakeProcess);
            }

            public IProcess[] GetProcessesByName(string name)
            {
                return _fakeProcesses.Where(fakeProcess => fakeProcess.Name == name).ToArray();
            }
        }

        private class FakeProcess : IProcess
        {
            public FakeProcess(string name)
            {
                Name = name;
            }

            public string Name { get; }

            public bool IsKilled { get; private set; }

            public void Kill()
            {
                IsKilled = true;
            }
        }
    }
}