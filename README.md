# 1P FMS Atom x KAI трек КТЗИ
## Сервис для разработки автопарка. 

Наш сервис представляет собой API по работе с автопарком по международным стандартам FMS.

Тестовый сайт: http://atomkai1p-001-site1.dtempurl.com/swagger/

## Преимущества

- Добавляйте автомобили в автопарк, редактируйте, удаляйте и смотрите акутальную информацию в реальном времени
- Лёгкость использования благодаря web api
- Возможность подключить другие сервисы

## Стек

Использованные технологии:

- [ASP NET WEB API] - АПИ для работы с сервисом
- [.NET CORE 6] - Сервис написан на LTS версии .net
- [Swagger] - Удобная документация и тестирование проекта
- [Automapper] - маппинг dto моделей
- [MS SQL] - microsoft sql server database
- [Serilog] - логгирование проекта

Наш исходный код общедоступный [public repository][dill] на гитхабе.
Для удобства проверки проекта, я подключил тестовую бд прямо в конфиге, 
конечно в продакшене мы не заливаем строку подключения в гитхаб, а храним лишь локально, используя User Secrets.
Если вам нужна своя бд, когда скачаете проект замените значение переменной ConnectionString на свою. Откройте окно PM (или используйте .net cli) и пропишите update-database, чтобы миграция применилась.


## Development

Хотите запустить проект в режиме разработки? Используйте visual studio community edition с скаченными пакетами .net core 6 и запустите из меню.
Если такой возможности нет, установите все пакеты через winget
```sh
winget install Microsoft.DotNet.SDK.Preview
winget install Microsoft.DotNet.DesktopRuntime.Preview
winget install Microsoft.DotNet.AspNetCore.Preview
winget install Microsoft.DotNet.Runtime.Preview
```

Более подробная инструкция без ошибок https://learn.microsoft.com/ru-ru/dotnet/core/install/windows?tabs=net70

После установки 

Откройте папку с проектом апи, путь ..\1p-atom-carmanager\1p-atom-carmanager.backend\1p-atom-carmanager.backend.api
Выполните
```sh
dotnet run
```
При удачном запуске выйдут следующие сообщения в консоли:
```sh
Now listening on: https://localhost:7084
Now listening on: http://localhost:5240
Application started. Press Ctrl+C to shut down.
Hosting environment: Development
```
Перейдите на https://localhost:7084/swagger

#### Запуск готового билда
Скачайте первый билд в репозитории проекта. 
В каталоге ..\Build Atom 1p fms\  
запустите команду в консоли:
```sh
dotnet
```
При удачном запуске выйдут следующие сообщения в консоли:
```sh
Now listening on: https://localhost:7084
Now listening on: http://localhost:5240
Application started. Press Ctrl+C to shut down.
Hosting environment: Development
```
Перейдите на https://localhost:7084/swagger

## Комментарии от разработчика
К сожалению не успел сделать Симулятор для апи. Надеюсь если у вас осстануться вопросы, вы напишите мне на ratmir@ishgulov.ru

## License

MIT

**Пользуйтесь кто хочет, это проект для хакатона!**
