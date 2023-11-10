using System;
using System.Threading.Tasks;
using System.Windows.Documents;

public delegate void LogErrorHandler(Guid userKey, string message, string stackTrace, string conString);

/// <summary>
/// Сводное описание для MyTask
/// </summary>
public class MyTask:ITask
{
    readonly LogErrorHandler _method;
    object[] _args;
    public MyTask(LogErrorHandler method, params object[] args)
    {
        _method = method;
        _args = (object[])args[0];
    }

    bool _fineshed= false;
    public bool FinishedOk { get { return _fineshed; } }

    bool _isBusy= false;
    public bool IsBusy { get { return _isBusy; } }

    public Task StartTask()
    {
        try
        {
            _isBusy = true;

            _method.Invoke((Guid)_args[0], (string)_args[1], (string)_args[2], (string)_args[3]);

            _fineshed = true;
        }
        catch (Exception)
        {
            _fineshed = false;
        }
        finally
        {
            _isBusy = false;
        }

        return Task.CompletedTask;
    }
}

public interface ITask
{
    bool FinishedOk { get; }
    bool IsBusy { get; }
    Task StartTask(); 
}