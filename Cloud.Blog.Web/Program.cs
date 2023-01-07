using Cloud.Infra.Applicatoins.Extensions;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

//通用服务
builder.Services.AddCloudService(builder);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//var a = AssemblyHelper.AllAssemblies;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();