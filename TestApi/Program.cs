var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
    opt
    .AllowAnyOrigin()
    .AllowAnyMethod()
);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//using (TestApi.Data.SearchAndRangeContext dbContext = new TestApi.Data.SearchAndRangeContext())
//{
//    if (dbContext.Okpd2s.Count() == 0)
//    {
//        TestApi.Adapter.Okpd2ParserAdapter okpdAdapter = new TestApi.Adapter.Okpd2ParserAdapter();

//        okpdAdapter.AddToDb();

//        Console.WriteLine("Okpd2 added");
//    }
//}

app.Run();