using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace ApartmentsBilling.BussinessLayer.Configuration.LogFilters
{
    public class MssqlLog
    {
        public ILogger _logger { get; set; }
        public MssqlLog(IConfiguration configuration)
        {
            var opt = new MSSqlServerSinkOptions()
            {
                TableName = "Logs",
                AutoCreateSqlTable = true,
            };

            var ColumnOpt = new ColumnOptions();
            ColumnOpt.Store.Remove(StandardColumn.Message);
            ColumnOpt.Store.Remove(StandardColumn.Properties);
            ColumnOpt.Store.Remove(StandardColumn.Exception);

            var SerilogConfig = new LoggerConfiguration().
                WriteTo.MSSqlServer(connectionString: configuration.
                GetConnectionString("MSS"), sinkOptions: opt, columnOptions: ColumnOpt);
            _logger = SerilogConfig.CreateLogger();
        }

    }
}
