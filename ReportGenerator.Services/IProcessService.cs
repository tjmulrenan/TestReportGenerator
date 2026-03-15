namespace ReportGenerator.Services
{
    public interface IProcessService
    {
        IProcess[] GetProcessesByName(string name);
    }

    public interface IProcess
    {
        void Kill();
    }
}
