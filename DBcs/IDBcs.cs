using System.Data;

namespace DBcs
{
    public interface IDBcs
    {
        Task<string> GetClassCodeString(string[] classNames, string query, CommandType commandType = CommandType.Text);
        Task<string> GetClassCodeString(string className, string query);
        string GetDDLCodeString(Type type, string tableName);
        string GetDDLCodeString(Type[] types, string[] tableNames);
        Task<IList<T>?> RunQueryAsync<T>(string sqlQuery, object? parameterObject = null, CommandType commandType = CommandType.Text) where T : new();
        Task RunQueryWithCallBackAsync<T>(string sqlQuery, Action<T>? rowLoaded, object? parameterObject = null, CommandType commandType = CommandType.Text) where T : new();
        Task<object?> RunScalarAsync(string sqlQuery, object? parameterObject = null, CommandType commandType = CommandType.Text);
        Task<int> RunNonQueryAsync(string sqlQuery, object? parameterObject = null, CommandType commandType = CommandType.Text);
        Task<T?> RunQuerySingleOrDefaultAsync<T>(string sqlQuery, object? parameterObject = null, CommandType commandType = CommandType.Text) where T : new();
        Task RunAndFillReferenceTypesWithCallbackAsync<T>(string sqlQuery, Action<T> rowLoaded, object? parameterObject = null, CommandType commandType = CommandType.Text) where T : new();
        Task<List<T>> RunAndFillReferenceTypesAsync<T>(string sqlQuery, object? parameterObject = null, CommandType commandType = CommandType.Text) where T : new();
     
    }
}