# 🎯 Quiz Uygulaması

Bu proje, **Clean Architecture** prensiplerine uygun, JWT tabanlı kimlik doğrulama, **Admin ve User rollerine göre yetkilendirme**, Google OAuth desteği ve **SOLID prensiplerine uygun** olarak geliştirilmiş bir Quiz uygulamasıdır.

---

##  Özellikler

-  Clean Architecture katmanlı yapı
-  JWT + Google OAuth kimlik doğrulama
-  Rol bazlı yetkilendirme (Admin/User)
-  Rastgele soru ve süreye bağlı puanlama (60 saniye içinde +10 puan)
-  Kullanıcı skor takibi
-  Serilog + PostgreSQL loglama
-  Mapleme

---

## 🧠 SOLID Prensipleri

| Prensip | Uygulama Örneği |
|---------|----------------|
| **SRP** | Her servis tek bir iş yapar (`AuthService`, `QuestionService`) |
| **OCP** | Yeni login sağlayıcıları eklemek için `IAuthService` genişletilebilir |
| **ISP** | Küçük ve odaklı interface’ler (`IQuestionService`, `IUserService`) |
| **DIP** | Controller ve Handler’lar interface üzerinden servis kullanır |

---

## 🛠️ Kullanılan Teknolojiler

- ASP.NET Core 9, Entity Framework Core, MediatR
- PostgreSQL(Loglama İçin), Serilog, InMemoryDatabase
- JWT Authentication, Google OAuth
- CQRS ve Clean Architecture
- AutoMapper

---

## 📦 Kurulum

1️⃣ Depoyu klonla  
2️⃣ `appsettings.json` yapılandır  
3️⃣ Migrasyon çalıştır  
4️⃣ Uygulamayı başlat

```bash
git clone https://github.com/kullaniciadi/quiz-app.git
cd quiz-app
dotnet ef database update
dotnet run --project Presentation/Quiz.API
