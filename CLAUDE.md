# CLAUDE.md — CollabPlatform

> Do not modify this file. That is exclusively the developer's responsibility.

## Project Context
A cross-platform desktop collaboration tool for student engineering teams.
Built as a portfolio project targeting entry-level SWE interviews.

## Stack
| Layer | Technology |
|---|---|
| Desktop App | .NET MAUI + Blazor Hybrid |
| Backend API | ASP.NET Core Web API |
| Real-Time | SignalR (CollabHub) |
| Database | PostgreSQL + Entity Framework Core |
| Caching / Presence | Redis |
| Auth | JWT (JwtBearer) |
| Unit Testing | xUnit + Moq + FluentAssertions |
| E2E Testing | Playwright |

## Solution Structure
    CollabPlatform.sln
    ├── src/
    │   ├── CollabPlatform.API/         # Controllers, Hubs, Services, Data, Middleware
    │   ├── CollabPlatform.Desktop/     # MAUI Blazor Hybrid client
    │   └── CollabPlatform.Shared/      # DTOs (Auth, Channels, Messages, Workspaces), Enums
    └── tests/
        ├── CollabPlatform.UnitTests/
        └── CollabPlatform.E2ETests/

## Architecture Decisions
- REST endpoints are read-only for message history. All live messages go through SignalR.
- ParentMessageId self-reference on Message handles threads — no separate table.
- Redis is for ephemeral presence only (presence:{workspaceId}:{userId}). Never persisted to PostgreSQL.
- DTOs live in Shared so both API and Desktop reference them without duplication.
- Controllers handle req/res only. Business logic belongs in Services.

## API Shape
    Auth:        POST /api/auth/register, /api/auth/login
    Workspaces:  POST/GET /api/workspaces, POST /api/workspaces/{id}/members
    Channels:    POST/GET /api/workspaces/{workspaceId}/channels
    Messages:    GET /api/channels/{channelId}/messages (history only)
    Webhooks:    POST /api/webhooks/github

## SignalR Hub — CollabHub
    Client → Server:  JoinChannel, LeaveChannel, SendMessage(channelId, content, parentMessageId?)
    Server → Client:  ReceiveMessage, UserPresenceChanged

## Code Style
- Follow Microsoft's official C# coding conventions
- Use Guid for all entity IDs
- Async/await throughout — no blocking calls

## Known Friction Points
- MAUI Blazor Hybrid can be fussy with SignalR WebSocket config — check MAUI-specific
  WebSocket setup if connection issues arise on the Desktop client
- UI polish is post-MVP. Desktop is scaffolding only during the build week.

## How to Work With Me

### Challenge, Don't Just Comply
Push back on decisions, flag anti-patterns, and question the approach — including
instructions in this file. Friction is expected. We're refining.

### Give It to Me Straight
No fluff. No affirmations. No "great question." Lead with the answer, follow with
the reasoning. If something is wrong, say so directly. Brutal honesty is more useful
than a polished non-answer.

### Code Review (Every Merge dev → master)
Act as a Senior SWE. Ask: "What would I flag in this PR?"

### Debugging Loops
If the same problem persists across multiple attempts, stop and reframe instead of
iterating. Flag it early.

## GitHub Workflow
- Branch flow: master → dev → feature branches
- PR format: Summary / Changes / Verification
- Commit format: <type>(<scope>): <summary>