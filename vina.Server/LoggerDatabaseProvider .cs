using System.Threading.Tasks;
using vina.Server.Models;
namespace vina.Server;

public class LoggerDatabaseProvider : ILoggerProvider
{
    private string _connectionString;

    public LoggerDatabaseProvider(string connectionString)
    {
        _connectionString = connectionString;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new Logger(categoryName, _connectionString);
    }

    public void Dispose()
    {
    }

    public class Logger : ILogger
    {
        private readonly string _categoryName;
        private readonly string _connectionString;

        public Logger(string categoryName, string connectionString)
        {
            _connectionString = connectionString;
            _categoryName = categoryName;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        private async Task RecordMsg<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            DBLog log = new DBLog
            {
                LogLevel = logLevel.ToString(),
                CategoryName = _categoryName,
                Message = state == null ? exception?.ToString() ?? "" : formatter(state, exception),
                Exception = exception == null ? "" : exception.ToString(),
                LogTimestamp = DateTime.UtcNow
            };
            DBcs.DBcs db = new DBcs.DBcs(_connectionString);

            var ret = db.RunScalarAsync(@"
                    select 
                        count(*)
                    from
                        information_schema.tables t  
                    where 
                        t.table_schema = 'public' 
                    and 
                        table_type='BASE TABLE'
                    and 
                        table_name='logs';")
                        .GetAwaiter().GetResult(); 
            if(ret == null)
                return;
            if((Int64)ret>0)
                await db.RunNonQueryAsync(DBLog.InsertText, log);
            
         
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            return new NoopDisposable();
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            RecordMsg(logLevel, eventId, state, exception, formatter).GetAwaiter().GetResult();

        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
