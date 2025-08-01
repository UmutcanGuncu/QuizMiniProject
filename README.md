# 🎯 Quiz Uygulaması

Bu proje, **Clean Architecture** prensiplerine uygun, JWT tabanlı kimlik doğrulama, **Admin ve User rollerine göre yetkilendirme**, Google OAuth desteği ve **SOLID prensiplerine uygun** olarak geliştirilmiş bir Quiz uygulamasıdır.

---

## ⚡ Özellikler

* 🏗️ Clean Architecture katmanlı yapı
* 🔑 JWT + Google OAuth kimlik doğrulama
* 👥 Rol bazlı yetkilendirme (Admin/User)
* 🎯 Rastgele soru ve süreye bağlı puanlama (60 saniye içinde +10 puan)
* 📊 Kullanıcı skor takibi
* 📝 Serilog + PostgreSQL loglama
* 🔄 AutoMapper kullanımı
* ✅ OpenAPI (Swagger) dokümantasyonu

---

## 🧠 SOLID Prensipleri

| Prensip | Uygulama Örneği                                                       |
| ------- | --------------------------------------------------------------------- |
| **SRP** | Her servis tek bir iş yapar (`AuthService`, `QuestionService`)        |
| **OCP** | Yeni login sağlayıcıları eklemek için `IAuthService` genişletilebilir |
| **ISP** | Küçük ve odaklı interface’ler (`IQuestionService`, `IUserService`)    |
| **DIP** | Controller ve Handler’lar interface üzerinden servis kullanır         |

---

## 🛠️ Kullanılan Teknolojiler

* **Backend:** ASP.NET Core 9, Entity Framework Core, MediatR
* **Veritabanı:** PostgreSQL (Loglama için), InMemoryDatabase (Hızlı test için)
* **Kimlik Doğrulama:** JWT, Google OAuth
* **Mimari:** CQRS + Clean Architecture
* **Mapleme:** AutoMapper
* **Loglama:** Serilog

---

## 📦 Kurulum

1️⃣ Depoyu klonla
2️⃣ `appsettings.json` dosyasını yapılandır
3️⃣ Migrasyonları çalıştır
4️⃣ Uygulamayı başlat

```bash
git clone https://github.com/UmutcanGuncu/QuizMiniProject
cd quiz-app
dotnet ef database update
dotnet run --project Presentation/Quiz.API
```

Swagger arayüzü: [https://localhost:7163/swagger](https://localhost:7163/swagger)

---

## 📜 API Endpointleri

| HTTP | Endpoint                 | Açıklama                         | Request Body                                      |
| ---- | ------------------------ | -------------------------------- | ------------------------------------------------- |
| POST | `/api/Auth/RegisterUser` | Yeni kullanıcı kaydı             | `nameSurname`, `email`, `phoneNumber`, `password` |
| POST | `/api/Auth/LoginUser`    | Kullanıcı girişi                 | `email`, `password`                               |
| POST | `/api/Auth/GoogleLogin`  | Google hesabıyla giriş yapma     | `idToken`, `provider`                             |
| GET  | `/api/questions`         | Sistemdeki tüm soruları listeler | -                                                 |
| GET  | `/api/question/random`   | Rastgele soru getirir            | -                                                 |
| POST | `/api/answer`            | Soruyu cevapla ve puan al        | `questionId`, `selectedOption`, `created`         |

> **Not:** JWT Authentication gerektiren endpointlerde `Authorization: Bearer <token>` başlığı kullanılmalıdır.

Github Linki : https://github.com/UmutcanGuncu/QuizMiniProject
