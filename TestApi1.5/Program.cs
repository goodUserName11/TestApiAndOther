var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(opt =>
    opt
    //.AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()
) ;

app.MapControllers();

using (TestApi.Data.SearchAndRangeContext dbContext = new TestApi.Data.SearchAndRangeContext())
{
    if (dbContext.Okpd2s.Count() == 0)
    {
        await TestApi.Adapter.AdapterContainer.Okpd2Adapter.AddToDb();
    }

    //dbContext.SupplierInLists.RemoveRange(dbContext.SupplierInLists.Where(s => s.SupplierListId == null));

    //dbContext.SaveChanges(true);

    await dbContext.DisposeAsync();
}

app.Run();
