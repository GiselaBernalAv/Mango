using AutoMapper;
using Mango.Services.CouponAPI;
using Mango.Services.CouponAPI.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IMapper _mapper = MappingConfig.RegisterMaps().CreateMapper();


// Add services to the container.
builder.Services.AddDbContext<AppMangoDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DfltConn"));
});

builder.Services.AddSingleton(_mapper);

//builder.Services.AddSingleton(mapper).AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
ApplyMigration();

app.Run();

void ApplyMigration()
{
    using(var scope = app.Services.CreateAsyncScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppMangoDbContext>();
        if(_db.Database.GetPendingMigrations().Count() >0)
        {
            _db.Database.Migrate();
        }
    }
}
