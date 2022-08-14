# ApartmentsBilling



Merhabalar Sizlere Kısaca Projeyi Açıklamak İstiyorum;

Şuan Sadece APi tarafı çalışmakta,CLient Ksımı için hala uğraşıyorum!


Öncelikle Siteye üye oluyoruz. Üye olurken kenidimize bir apartman adı ve bir dairede oluşturyoruz ve Otomatik olarak
Oluşturduğumuz Apartmanın yöneticsi olyoruz

UYARI: Şifre sistem tarafından otomatik oluşup belirtmiş olduğunuz maile gelmektadir.
Deneme için temp-mail kullanılablir
Allta Örnek olarak verildiği Gibi
![Screenshot_1](https://user-images.githubusercontent.com/92210948/184557058-4155e7b4-c1c6-4e84-85a1-4b6be4357436.png)
Sonradan Şifrenizi değiştirebilirsiniz

Sisteme kayıt olduktan sonra Kullanıcı eklemeden önce Daire eklemelisiniz.İsterseniz sizin dışında başka yönetici de ekleyebilirsiniz




Fatura eklemeden Önce Sisteme Fatura tipi eklemelisiniz Örnek olarak Doğal gaz verilebilir.Fatura ekleme kısmında girilen Fatura tipi ile beraber
bir apartman Id si de çekilir ve girilen tutar apartmana bağlı dairelere eşit olarak bölünür.Daire Başına Fatura oluşturulur.

Mesaj kısmında şuan için Yönetici sadece mesajları görntüleyip silebilir.Mesaj Gönderme Yetkisi Bulunmamaktadır.

Kullanmış Olduğum Teknolojiler:

.Net Core 5.0,
Entity FrameWork 5.0.17
AutoFac
MimeKit
Redis
AutoMapper
FluentValidation
