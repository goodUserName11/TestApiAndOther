using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("log.txt", 
                rollingInterval: RollingInterval.Day,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

try
{
    Log.Logger.Information("���������� �����������...");

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection();

    //app.user

    app.UseAuthorization();

    app.UseCors(opt =>
        opt
        //.AllowCredentials()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .Build()
    );

    app.MapControllers();

    using (TestApi.Data.SearchAndRangeContext dbContext = new TestApi.Data.SearchAndRangeContext())
    {
        if (dbContext.Okpd2s.Count() == 0)
        {
            Log.Logger.Information("��������� ����2...");
            await TestApi.Adapter.AdapterContainer.Okpd2Adapter.AddToDb();
            Log.Logger.Information("���� ��������");
        }

        //dbContext.SupplierInLists.RemoveRange(dbContext.SupplierInLists.Where(s => s.SupplierListId == null));

        //dbContext.SaveChanges(true);

        await dbContext.DisposeAsync();
    }

    Log.Logger.Information("���������� ��������");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "����������� ���������� ������");
}
finally
{
    Log.CloseAndFlush();
}
