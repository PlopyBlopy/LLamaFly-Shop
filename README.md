# Интернет Магазин LlamaFly - Web API-приложение с микросервисной архитектурой.

## Ревью архитектуры:
https://github.com/user-attachments/assets/1e2f8c43-14c2-42b6-a2b5-aa7ee348788b

## Ревью сайта:
https://github.com/user-attachments/assets/9dba8928-0f27-4a8a-8cc3-45a575444fb4

### Микросервисы:
1. **Развернуты в Docker**:
   - docker-compose
   - docker-compose.override
   - env
2. **NGINX - LoadBalancer**:
   - default.conf
3. **Frontend**:
   - React
   - TypeScript
   - MUI
   - Axios
   - Mobx
3. **API на ASP.NET Core, .NET 8**:
   - Чистая архитектура
   - Разделение на слои: API, Application, Core, Infrastructure
   - Аунтификация/Авторизация: JwtBearer
   - Обработка ошибок: GlobalExceptionHandler, Result
   - ORM: Dapper, EFCore
   - Библиотеки: FluentValidation, FluentResults, Automapper, Scrutor, StackExchangeRedis, Npgsql
4. **База данных: PostgreSQL**
5. **Кэш: Redis**
6. **Брокер сообщений**: Kafka

## Планы развития:
1. Изменение архитектуры API на Чистую архитектуру + CQRS + своя реализация Mediator
2. Деструктуризация react компонентов
3. Добавление нового функционала:
   - Больше действий с продуктами
   - Интеграция покупателя
   - Платежная система
   - Эмуляция системы доставки
