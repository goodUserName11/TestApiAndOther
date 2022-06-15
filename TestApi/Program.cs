using Microsoft.EntityFrameworkCore;
using TestApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<TestApi.AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<TestApi.Authentication.JwtMiddleware>();

app.MapControllers();

using (TestApi.Data.SearchAndRangeContext dbContext = new())
{
    if (dbContext.Okpd2s.Count() == 0)
    {
        await TestApi.Adapter.AdapterContainer.Okpd2Adapter.AddToDb();
    }

    dbContext.SupplierInLists.RemoveRange(dbContext.SupplierInLists.Where(s => s.SupplierListId == null));

    dbContext.SaveChanges(true);
}

app.Run();