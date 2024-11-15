# CrossPlatformLabs

Запуск:
```bash
dotnet build Build.proj -p:Solution=Lab<номер_лабораторної> -t:Run
```

Білд:
```bash
dotnet build Build.proj -p:Solution=Lab<номер_лабораторної> -t:Build
```

Тест:
```bash
dotnet build Build.proj -p:Solution=Lab<номер_лабораторної> -t:Test
```
---

Починаючи з 4 лабораторної, запуск здійснювати з папок проекту. 
У цих же папках знаходяться файли README з описом того, які команди необхідно ввести