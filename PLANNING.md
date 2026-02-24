# CollabPlatform — Project Planning

## Resume Bullets
- Designing a desktop collaboration app for student engineering teams with .NET MAUI, ASP.NET Core Web API, and Blazor Hybrid with PostgreSQL database
- Implementing GitHub integrations as well as Redis caching with Entity Framework Core
- Developing unit tests with xUnit and Moq along with E2E tests with Playwright

---

## Stack
| Layer | Technology |
|---|---|
| Desktop App | .NET MAUI + Blazor Hybrid |
| Backend API | ASP.NET Core Web API |
| Real-Time | SignalR |
| Database | PostgreSQL + Entity Framework Core |
| Caching / Presence | Redis |
| Auth | JWT (JwtBearer) |
| Unit Testing | xUnit + Moq + FluentAssertions |
| E2E Testing | Playwright |

---

## MVP Scope (1 Week)

### Core (Non-Negotiable)
- User auth (register/login with JWT)
- Create/join a team workspace
- Channels (create, list, join)
- Real-time messaging in channels via SignalR
- Threaded replies on messages

### Standout Feature
- GitHub webhook integration — PR opened/merged posts an automatic notification into a designated channel

### Nice-to-Have (if time allows)
- Online presence indicators via Redis
- Direct messages between users

### Post-MVP (mention in README/interviews)
- Stand-up bot
- Embedded sprint boards
- Knowledge base / wiki
- Academic calendar integration
- Role rotation tracking
- Semester handoff tools
- Member portfolio pages

---

## Build Plan

| Day | Focus | Goal |
|---|---|---|
| 1 | Foundation & Auth | Solution scaffolded, JWT auth working, desktop app shell running |
| 2 | Workspaces & Channels | Data models, EF Core migrations, API endpoints, basic nav UI |
| 3 | Real-Time Messaging | SignalR hub connected, messages sent/received live, persisted to DB |
| 4 | Threads + Redis Presence | Threaded replies, online/offline status via Redis |
| 5 | GitHub Webhook | Webhook endpoint, parse PR events, post to designated channel |
| 6 | Testing | xUnit + Moq unit tests, Playwright E2E critical path |
| 7 | Polish & README | Bug fixes, UI tidy, strong README with architecture + planned features |

---

## Data Model

```
User
- Id (Guid)
- Username (string)
- Email (string)
- PasswordHash (string)
- CreatedAt (DateTime)

Workspace
- Id (Guid)
- Name (string)
- OwnerId (Guid → User)
- CreatedAt (DateTime)

WorkspaceMember
- WorkspaceId (Guid → Workspace)
- UserId (Guid → User)
- Role (enum: Owner, Member)
- JoinedAt (DateTime)

Channel
- Id (Guid)
- WorkspaceId (Guid → Workspace)
- Name (string)
- IsGitHubChannel (bool)
- CreatedAt (DateTime)

Message
- Id (Guid)
- ChannelId (Guid → Channel)
- SenderId (Guid → User)
- Content (string)
- ParentMessageId (Guid? → Message)   ← null = top-level, set = thread reply
- CreatedAt (DateTime)
```

---

## API Structure

```
Auth
  POST   /api/auth/register
  POST   /api/auth/login

Workspaces
  POST   /api/workspaces
  GET    /api/workspaces/{id}
  POST   /api/workspaces/{id}/members

Channels
  POST   /api/workspaces/{workspaceId}/channels
  GET    /api/workspaces/{workspaceId}/channels

Messages  (REST = read-only history, live messages go through SignalR)
  GET    /api/channels/{channelId}/messages
  GET    /api/channels/{channelId}/messages/{messageId}/replies

Webhooks
  POST   /api/webhooks/github
```

---

## SignalR Hub

**Hub:** `CollabHub`

```
Client → Server:
  JoinChannel(channelId)
  LeaveChannel(channelId)
  SendMessage(channelId, content, parentMessageId?)

Server → Client:
  ReceiveMessage(message)
  UserPresenceChanged(userId, isOnline)
```

---

## Redis

```
Key pattern:   presence:{workspaceId}:{userId}
Value:         "online" / "offline"
Set:           on SignalR connect
Remove:        on SignalR disconnect
```

---

## Solution Structure

```
CollabPlatform.sln
│
├── src/
│   ├── CollabPlatform.API/
│   │   ├── Controllers/
│   │   ├── Hubs/
│   │   ├── Models/
│   │   ├── Services/
│   │   ├── Data/
│   │   ├── Middleware/
│   │   └── Program.cs
│   │
│   ├── CollabPlatform.Desktop/
│   │
│   └── CollabPlatform.Shared/
│       ├── DTOs/
│       │   ├── Auth/
│       │   ├── Channels/
│       │   ├── Messages/
│       │   └── Workspaces/
│       └── Enums/
│
└── tests/
    ├── CollabPlatform.UnitTests/
    └── CollabPlatform.E2ETests/
```

---

## Scaffold Commands

Run from the root folder where you want the project to live:

```bash
dotnet new sln -n CollabPlatform
mkdir src tests

# Projects
dotnet new webapi -n CollabPlatform.API -o src/CollabPlatform.API
dotnet new maui-blazor -n CollabPlatform.Desktop -o src/CollabPlatform.Desktop
dotnet new classlib -n CollabPlatform.Shared -o src/CollabPlatform.Shared

# Test projects
dotnet new xunit -n CollabPlatform.UnitTests -o tests/CollabPlatform.UnitTests
dotnet new xunit -n CollabPlatform.E2ETests -o tests/CollabPlatform.E2ETests

# Add all to solution
dotnet sln add src/CollabPlatform.API/CollabPlatform.API.csproj
dotnet sln add src/CollabPlatform.Desktop/CollabPlatform.Desktop.csproj
dotnet sln add src/CollabPlatform.Shared/CollabPlatform.Shared.csproj
dotnet sln add tests/CollabPlatform.UnitTests/CollabPlatform.UnitTests.csproj
dotnet sln add tests/CollabPlatform.E2ETests/CollabPlatform.E2ETests.csproj

# Project references
dotnet add src/CollabPlatform.API reference src/CollabPlatform.Shared
dotnet add src/CollabPlatform.Desktop reference src/CollabPlatform.Shared
dotnet add tests/CollabPlatform.UnitTests reference src/CollabPlatform.API

# API packages
cd src/CollabPlatform.API
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.AspNetCore.SignalR
dotnet add package StackExchange.Redis
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package BCrypt.Net-Next
cd ../..

# Unit test packages
cd tests/CollabPlatform.UnitTests
dotnet add package Moq
dotnet add package FluentAssertions
cd ../..

# E2E test packages
cd tests/CollabPlatform.E2ETests
dotnet add package Microsoft.Playwright
cd ../..
```

---

## Progress

- [x] Stack decided
- [x] MVP scope defined
- [x] Data model designed
- [x] API structure designed
- [x] Solution structure defined
- [x] Day 1 — Foundation & Auth *(auth endpoints implemented; first migration + Postman verification pending)*
- [ ] Day 2 — Workspaces & Channels
- [ ] Day 3 — Real-Time Messaging
- [ ] Day 4 — Threads + Redis Presence
- [ ] Day 5 — GitHub Webhook
- [ ] Day 6 — Testing
- [ ] Day 7 — Polish & README

---

## Notes
- Messages are sent over SignalR, not REST. REST endpoints are read-only for loading history.
- `ParentMessageId` self-reference on Message handles threads without a separate table.
- Redis is for ephemeral presence state only — keep it out of PostgreSQL.
- Build Postman collection endpoint-by-endpoint as you go. Use it for interviews.
- MAUI Blazor Hybrid can be fussy — if SignalR connection has issues on Day 3, check MAUI-specific WebSocket configuration.
- UI polish is post-MVP. Blazor Hybrid is scaffolding only during the build week.
- Future sessions: paste this file in as context to get back up to speed instantly.
