Лабораторна робота №6
===

Для запуску необхідно мати Virtual Box та Vagrant

Для створення ВМ потрібно запустити команду:
```bash
vagrant up --provision-with=init
```
Якщо, під час створення, ви отримаєте запит: "Which interface should the network bridge to?", то введіть: 1

Після цього буде створено та запущено ВМ, де будуть встановлені PostgreSQL, SQLServer, SQLite та .NET 6.0

Для запуску ввести таку команду:
* PostgreSQL:
  ```bash
  vagrant provision --provision-with=postgre
  ```
* SQLServer:
  ```bash
  vagrant provision --provision-with=sqlserver
  ```
* SQLite:
  ```bash
  vagrant provision --provision-with=sqlite
  ```
* InMemory:
  ```bash
  vagrant provision --provision-with=inmemory
  ```
Якщо ви вимкнули ВМ, то замість vagrant provision вводити vagrant up

Після цього буде запущено два проекти: Lab_5 та Lab_6, за портами: 3000 та 3001 відповідно

---

Якщо ви хочете запустити проекти вручну, то в Lab_6 потрібно передавати аргумент у такому виді:
* PostgreSQL:
  ```bash
  dotnet run --project Lab_6.csproj -- --DbType=PostgreSQL
  ```
* SQLServer:
  ```bash
  dotnet run --project Lab_6.csproj -- --DbType=SQLServer
  ```
* SQLite:
  ```bash
  dotnet run --project Lab_6.csproj -- --DbType=SQLite
  ```
* InMemory:
  ```bash
  dotnet run --project Lab_6.csproj -- --DbType=InMemory
  ```