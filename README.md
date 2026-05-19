# TaskHub - проект для выполнения домашних заданий курса по ASP.NET

## Запуск
1) Развёртывание БД\
  Для развертывания базы данных необходимо установить **Docker Desktop**.\
  В корне репозитория выполнить команду в терминале: 
      ```bash
    docker compose up -d
      ```

2) Применение миграций
- Из папки решения установить **Microsoft.EntityFrameworkCore.Tools** (можно через Управление Пакетами NuGet):
  ```
  dotnet tool install --global dotnet-ef
  ```
- Генерируем миграции (Консоль диспетчера пакетов):
  ```
   dotnet ef migrations add CreateUsers --project Dal --startup-project Api --context UserDbContext --output-dir Migrations
   dotnet ef migrations add CreateUsers --project Dal --startup-project Api --context TaskDbContext # После создания TaskDbContext (задание на контроллеры)
  ```
  Если упали пробуем пересобрать проект:
  ```
   dotnet build Api/Api.csproj
  ```
  
- Применяем миграции:
  ```
  dotnet ef database update --project Dal --startup-project Api --context UserDbContext
  dotnet ef database update --project Dal --startup-project Api --context TaskDbContext # После создания TaskDbContext (задание на контроллеры)
  ```
## Немного про проект
Это обучающий курс,называется Разработка веб-приложений на ASP.NET Core, от компании Artsofte, что базируется в Екатеринбурге, на который я попал из института. Хоть на практику меня и не позвали, хотя я очень старался, темы, которые затронуты в процессе обучения:
1) Middleware
2) Атрибуты
3) Фильтры
4) Тесты(было как доп.задание)
5) API
6) ModelBinding\
Для каждой темы в проекте есть свой, самописный, пул-реквест. Если вдруг кто из будущих поколений, обучающихся на этом курсе, попал сюда - пользуйтесь на здоровье.\
Спасибо преподавательскому составу Artsofte.
