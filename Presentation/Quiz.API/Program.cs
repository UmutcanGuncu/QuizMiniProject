using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quiz.Application.Abstractions;
using Quiz.Application.CQRS.Handlers.Questions;
using Quiz.Domain.Entities;
using Quiz.Persistence.Contexts;
using Quiz.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(
    typeof(Program).Assembly,
    typeof(AnswerQuestionCommandHandler).Assembly));
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseInMemoryDatabase("QuizDB"));
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    
}) .AddEntityFrameworkStores<QuizDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAuthService, AuthService>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QuizDbContext>();
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    if (!context.Questions.Any())
    {
        var questions = new List<Question>()
        {
            new Question()
            {
            Id =  Guid.NewGuid(),
            CorrectAnswer = 2,
            CreatedAt = DateTime.UtcNow,
            Options = new List<string>(){"Bursa","Adana","Ankara","Kırşehir"},
            QuestionText = "Türkiye'nin Başkenti Aşağıdakilerden Hangisidir?"
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 1,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Halk arasında kullanılan bir söze göre, bir uzlaşmazlığı arttıracak şekilde davrananların yangına neyle gittikleri söylenir?",
                Options = new List<string>(){"Oksijen tüpüyle", "Körükle","Odunla", "Benzinle"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 0,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Bilim adamları Utarit’e uzay aracı yollayacaksa hangisine keşif yapılacak demektir?",
                Options = new List<string>(){"Merkür", "Mars","Venüs", "Jüpiter"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 3,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Olimpiyat sporlarından hangisinde sadece erkekler yarışır?",
                Options = new List<string>(){"Kürek", "Güreş","Cirit Atma", "Kulplu Beygir"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 0,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Hangi ünlü bilimkurgu romanından sonra, hayvanlar üzerinde deneyler yapılmaması yönünde ciddi çalışmalar başlatılmıştır?",
                Options = new List<string>(){"Dr. Moreau'nun Adası", "Kuşlar","Dr. Jekyll ve Mr. Hyde", "Maymunlar Cehennemi"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 1,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Üzerinde sadece peynir, fesleğen, domates ve domates sosu konulan pizzanın adı nedir?",
                Options = new List<string>(){"Bianca", "Margherita","Peperonni", "Funghi"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 2,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "“Ayetel Kürsi” hangi surede yer alır?",
                Options = new List<string>(){"Fetih", "Rahman","Bakara", "Nur"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 1,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Büyüklüğüne göre sırasıyla defne yaprağı, çinekop, sarıkanat ve lüfer adını alan balığın daha büyüğüne ne ad verilir?",
                Options = new List<string>(){"Palamut", "Kofana","Torik", "Orkinos"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 2,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Suyun kaynama noktası vücut sıcaklığına düştüğünden, astronotların kıyafetsiz çıkabileceği, Dünya’dan 19.000 m yükseklikteki sınırın adı nedir?",
                Options = new List<string>(){"Kopernik limiti", "Gagarin limiti","Armstrong limiti", "Galilei limiti"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 3,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Hangi ulaşım aracında can yeleği bulunmaz?",
                Options = new List<string>(){"Vapur", "Uçak","Denizaltı", "Tren"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 3,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Jules Verne’in “Denizler Altında 20.000 Fersah” kitabındaki mesafeye inseydik, denizin kaç kilometre altında olurduk?",
                Options = new List<string>(){"10 kilometre", "100 kilometre","1.000 kilometre", "100 bin kilometre"}
                
            }, 
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 1,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Hangi iki ayın ilk günleri hep aynı güne denk gelir?",
                Options = new List<string>(){"Şubat-Eylül", "Mart-Kasım","Nisan-Ekim", "Mayıs-Aralık"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 1,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Guinness Dünya Rekorları'na göre hangi karakter sinema ve televizyonlarda toplan 254 kez canlandırılmıştır?",
                Options = new List<string>(){"James Bond", "Sherlock Holmes","Monte Kristo Kontu", "Superman"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 0,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Basketbol oyununun ilk dönemlerinde hangisini yapmak kural dışıydı?",
                Options = new List<string>(){"Top sürmek", "Tek elle pas vermek","Hakemle konuşmak", "Takım arkadaşıyla konuşmak"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 2,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Devletin görüşlerine aykırı bulunarak basımı reddedilen ve kaçırılarak İtalya’da basılan “Doktor Jivago” romanı kime aittir?",
                Options = new List<string>(){"Aleksandr Soljenitsin", "Maksim Gorki","Boris Pasternak", "Lev Tolstoy"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 1,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Sahil Güvenlik Komutanlığı” olağanüstü haller ve savaş hali dışında hangi kuruma bağlıdır?",
                Options = new List<string>(){"Jandarma Genel Komutanlığı", "İçişleri Bakanlığı","Deniz Kuvvetleri Komutanlığı", "Başbakanlık"}
                
            },
            new Question()
            {
                Id =  Guid.NewGuid(),
                CorrectAnswer = 3,
                CreatedAt = DateTime.UtcNow,
                QuestionText = "Geleneksel yöntemlerle kahveyi közde ağır ağır pişiren kişiler, lezzeti artırmak için özellikle nasıl cezve kullanırlar?",
                Options = new List<string>(){"Alüminyum", "Çelik","Gümüş", "Bakır"}
                
            },
            
        };
        await context.Questions.AddRangeAsync(questions);
        await context.SaveChangesAsync();
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();