# STech_Assessment
Bu proje **NoSQL(MongoDB)** ile **PhoneDirectory** ve **Report** mikroservislerinin **RabbitMQ Event Driven Communication** üzerinden haberleştiği bir yapı ile hazırlanmıştır.

#### PhoneDirectory Mikroservisi aşağıdaki maddeleri içermektedir;
* ASP.Net Core Web API application
* Layered Architecture
* MongoDB veritabanı bağlantısı
* Generic Repository implementasyonu
* Swagger Open API implementasyonu
* MassTransit ve RabbitMQ kullanarak ReportQueue publishi

#### Report Mikroservisi aşağıdaki maddeleri içermektedir;
* ASP.Net Core Web API application
* Layered Architecture
* MongoDB veritabanı bağlantısı
* Generic Repository implementasyonu
* Swagger Open API implementasyonu

#### Projeyi çalıştırmak için ihtiyaç duyulan araçlar:
* Visual Studio 2019
* .Net Core 3.1 veya üstü

#### Mikro hizmetleri aşağıdaki URL'lerle başlatabilirsiniz:
* Report API -> https://localhost:44381/swagger/index.html
* PhoneDirectory API -> https://localhost:44311/swagger/index.html
* RabbitMQ Management Dashboard -> http://localhost:15672/ --guest/guest
* MongoDB Compass -> mongodb+srv://admin:admin@phonedirectorycluster.rtcqo.mongodb.net/test


