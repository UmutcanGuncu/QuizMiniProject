using Quiz.API.Configurations.ColumnWriter;
using Quiz.API.Extensions;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
var logger = new LoggerConfiguration() // Serilogu implemente edecek
    .WriteTo.Console() // nereye loglama yapacağımızı belirtiyoruz
    .WriteTo.File("logs/log.txt") //logs.txt dosyasına kayıt yapılmasını sağlar
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("DefaultConnection"), //appsetting.json 
        //dosyası içindeki defaultconnection parametresinde veri tabanı ayarları bildirilmiştir
        "logs",
        needAutoCreateTable:true,
        columnOptions: new Dictionary<string, ColumnWriterBase> // tablodaki sütünların ayarlanması
        {
            {"message", new RenderedMessageColumnWriter()},
            {"level", new LevelColumnWriter()},
            {"message_template", new MessageTemplateColumnWriter()},
            {"exception", new ExceptionColumnWriter()},
            {"log_event", new LogEventSerializedColumnWriter()},
            {"user_name", new UsernameColumnWriter()},
            {"timestamp",  new TimestampColumnWriter()}
            
        })
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(logger);
var app = builder.Build();
await app.SeedDataAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseCors("Frontend");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.Use(async (context, next) =>
{
    // sisteme giriş yapan kullanıcının kullanıcı adının loglama için kullanılan veri tabanına kayıt edilmesi
    
    var username = context.User?.Identity?.IsAuthenticated !=null ? context.User.Identity?.Name : "Anonymous";
    LogContext.PushProperty("user_name", username); // alınan kullanıcı adı logs veri tabanındaki user_name 
    //alanına kayıt edilir
    await next();;
});
app.MapControllers();

app.Run();