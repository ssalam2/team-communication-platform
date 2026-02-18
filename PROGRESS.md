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
- [ ] Repo initialized and pushed to GitHub
- [ ] Solution scaffolded via Claude Code

---

## Day 1 — Foundation & Auth
- [ ] Solution builds cleanly
- [ ] Desktop app shell runs
- [ ] EF Core + PostgreSQL connected
- [ ] User entity and DbContext created
- [ ] First migration applied
- [ ] POST /api/auth/register working
- [ ] POST /api/auth/login returns JWT
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
_Add any issues or decisions made during the build here._
