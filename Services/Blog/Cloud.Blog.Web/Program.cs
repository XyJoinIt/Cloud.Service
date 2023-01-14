
var builder = WebApplication.CreateBuilder(args);
//��ʼ������
InitConfiguration(builder.Configuration);
//ע��ͨ�÷���
builder.Services.AddCloudService<BlogDbContext>(x =>
{
    x.builder = builder;
    x.dependencyContext = DependencyContext.Default!;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

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

//��ʼ������
void InitConfiguration(IConfiguration configuration)
{
    GlobalConfig.DbConnectionOptions = configuration.GetSection("ConnectionStrings").Get<DbConnectionOptions>()!;
    GlobalConfig.EventBusOptions = configuration.GetSection("EventBusOptions").Get<EventBusOptions>()!;
}