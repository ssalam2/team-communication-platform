# CollabPlatform — Progress Tracker

> Paste this file into a new claude.ai or Claude Code session along with PLANNING.md to resume where you left off.

---

## Planning
- [x] Stack decided
- [x] MVP scope defined
- [x] Data model designed
- [x] API structure designed
- [x] SignalR hub designed
- [x] Redis pattern defined
- [x] Solution structure defined
- [x] Scaffold commands written
- [x] PLANNING.md created
- [x] PROGRESS.md created
- [x] Repo initialized and pushed to GitHub
- [x] Solution scaffolded via Claude Code

---

## Day 1 — Foundation & Auth
- [ ] Solution builds cleanly *(pending — first migration must be applied before a clean run)*
- [x] Desktop app shell scaffolded (MAUI Blazor Hybrid — stub UI only)
- [x] EF Core + PostgreSQL configured (AppDbContext, connection string, Npgsql)
- [x] User entity and DbContext created
- [x] Auth DTOs created (RegisterRequest, LoginRequest, AuthResponse in CollabPlatform.Shared)
- [x] WorkspaceRole enum created (CollabPlatform.Shared)
- [x] IAuthService interface + AuthService implemented (BCrypt hashing, JWT generation)
- [x] POST /api/auth/register implemented
- [x] POST /api/auth/login implemented
- [x] JWT auth middleware wired in Program.cs (issuer, audience, lifetime, signing key)
- [x] OpenAPI configured (.NET 10 built-in — serves `/openapi/v1.json` in dev)
- [x] Docker Compose configured (PostgreSQL 16 + Redis 7)
- [ ] First migration applied
- [ ] Auth endpoints verified end-to-end
- [ ] Postman collection started

---

## Day 2 — Workspaces & Channels
- [ ] Workspace + WorkspaceMember entities created
- [ ] Channel entity created
- [ ] Migration applied
- [ ] POST /api/workspaces
- [ ] GET /api/workspaces/{id}
- [ ] POST /api/workspaces/{id}/members
- [ ] POST /api/workspaces/{workspaceId}/channels
- [ ] GET /api/workspaces/{workspaceId}/channels
- [ ] Basic navigation UI in Blazor Hybrid
- [ ] Postman collection updated

---

## Day 3 — Real-Time Messaging
- [ ] Message entity created
- [ ] Migration applied
- [ ] CollabHub created
- [ ] JoinChannel working
- [ ] LeaveChannel working
- [ ] SendMessage working
- [ ] ReceiveMessage received client-side
- [ ] Messages persisted to PostgreSQL
- [ ] GET /api/channels/{channelId}/messages (history)
- [ ] Postman collection updated

---

## Day 4 — Threads + Redis Presence
- [ ] Threaded replies working (ParentMessageId)
- [ ] GET /api/channels/{channelId}/messages/{messageId}/replies
- [ ] Redis connected
- [ ] Presence set on SignalR connect
- [ ] Presence cleared on SignalR disconnect
- [ ] UserPresenceChanged broadcasting to clients
- [ ] Postman collection updated

---

## Day 5 — GitHub Webhook
- [ ] POST /api/webhooks/github endpoint created
- [ ] PR opened event parsed
- [ ] PR merged event parsed
- [ ] Notification posted to designated GitHub channel
- [ ] Tested with Postman (simulated payload)
- [ ] Postman collection updated

---

## Day 6 — Testing
- [ ] MessageService unit tests (xUnit + Moq)
- [ ] ChannelService unit tests
- [ ] GitHub event parser unit tests
- [ ] API endpoint integration tests
- [ ] Playwright E2E: login flow
- [ ] Playwright E2E: open channel + send message

---

## Day 7 — Polish & README
- [ ] Bug fixes
- [ ] UI tidy
- [ ] README written (architecture, features, how to run)
- [ ] Planned features section in README
- [ ] Final push to GitHub

---

## Blockers / Notes

- **OpenAPI:** Switched from Swashbuckle to `Microsoft.AspNetCore.OpenApi` (built-in .NET 10). Doc served at `/openapi/v1.json` in dev. No Swagger UI — use Postman or add Scalar (`Scalar.AspNetCore`) if a browser UI is needed.
- **First migration is the immediate blocker** — auth endpoints are fully coded but cannot run until `dotnet ef migrations add InitialCreate` and `dotnet ef database update` are run (requires Docker Compose up).
- Desktop app shell is scaffolded but has zero real UI — all Blazor pages are stubs. UI work begins Day 2.
