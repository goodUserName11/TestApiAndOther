var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//builder.WebHost
//    //.UseKestrel().ConfigureKestrel(opt => opt.ListenAnyIP(40215))
//    .UseIIS()
//    .UseUrls("http://l*:*", "https://*:*");

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(opt => 
    opt
    //.AllowCredentials()
    .AllowAnyOrigin()
    .AllowAnyMethod()
);

app.MapControllers();

using (TestApi.Data.SearchAndRangeContext dbContext = new TestApi.Data.SearchAndRangeContext())
{
    if (dbContext.Okpd2s.Count() == 0)
    {
        TestApi.Adapter.AdapterContainer.Okpd2Adapter.AddToDb();
    }

    //if(dbContext.Users.Count() == 0)
    //{
    //    await dbContext.Users.AddAsync(
    //        new TestApi.Entity.User());
    //}

    await dbContext.DisposeAsync();
}

app.Run();
