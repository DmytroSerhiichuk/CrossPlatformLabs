Лабораторна робота №7
===

Для запуску тестів виконати одну з команд:
* PostgreSQL:
  ```bash
  npm test -- --DbType=PostgreSQL
  ```
* SQLServer:
  ```bash
  npm test -- --DbType=SQLServer
  ```
* SQLite:
  ```bash
  npm test -- --DbType=SQLite
  ```
* InMemory:
  ```bash
  npm test -- --DbType=InMemory
  ```

# Для запуску на віртуальній машині необхідно мати: Vagrant та Virtual box

Для створення вм потрібно ввести таку команду:

```bash
vagrant up --provision-with=init
```

Після цього буде створено вм з усіма необхідними даними

Для запуску тестів виконанти таку команду:
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

Результати тестів будуть у консолі

# Результати:

На прикладі InMemory:

![image](https://github.com/user-attachments/assets/9a177e81-467b-4d33-8bcd-aac5705e7c1c)

На прикладі SQLite:

![image](https://github.com/user-attachments/assets/48996884-654f-426c-ba3e-f1a16a6d7d48)
