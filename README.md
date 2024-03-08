# ApartmentsBilling

Not: Bu Proje Bootcamp süresince geliştirilmiştir. Süre ile yarıştığımızdan Ötürü Projenin kendisinde Değişiklik yapmak istemedim. Refactoring işlemlerini farklı bir branch üzerinde geliştirmekteyim



Merhabalar Sizlere Kısaca Projeyi Açıklamak İstiyorum;

Şuan Sadece APi tarafı çalışmakta,CLient Ksımı için hala uğraşıyorum!


Öncelikle Siteye üye oluyoruz. Üye olurken kendimize bir apartman adı ve bir dairede oluşturyoruz ve Otomatik olarak
Oluşturduğumuz Apartmanın yöneticisi oluyoruz

UYARI: Şifre sistem tarafından otomatik oluşup belirtmiş olduğunuz maile gelmektadir.
Deneme için temp-mail kullanılablir
Sonradan Şifrenizi değiştirebilirsiniz

Sisteme kayıt olduktan sonra Kullanıcı eklemeden önce Daire eklemelisiniz.İsterseniz sizin dışında başka yönetici de ekleyebilirsiniz




Fatura eklemeden Önce Sisteme Fatura tipi eklemelisiniz Örnek olarak Doğal gaz verilebilir.Fatura ekleme kısmında girilen Fatura tipi ile beraber
bir apartman Id si de çekilir ve girilen tutar apartmana bağlı dairelere eşit olarak bölünür.Daire Başına Fatura oluşturulur.

Mesaj kısmında şuan için Yönetici sadece mesajları görntüleyip silebilir.Mesaj Gönderme Yetkisi Bulunmamaktadır.

Kullanmış Olduğum Teknolojiler:

  #### Technologies
- .NET Core 5.0
- Asp.NET for Restful Api
- MsSql
- Entity FrameWork Core 5.0.17
- Automapper
- AutoFac
- FluentValidation
- MimeKit

#### Techniques
- JWT (Json Web Tokens)
- IoC 
- Microsoft Built In Dependency Resolver
