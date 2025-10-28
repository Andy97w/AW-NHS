# AW-NHS

AW-NHS is a small ASP.NET Core (.NET 8) Web API created as a tech test for the NHS.

## Solution Overview

This solution contains two projects:

### API (`AW-NHS.API`)
A .NET 8 Web API that exposes an endpoint to retrieve a patient using their ID. The API uses a mocked/in-memory data store.

- GET /api/patient/{id}

### Tests (`AW-NHS.Tests`)
A test project containing:
- Unit tests for business logic
- Integration tests that exercise the patient API endpoint

API exploration:
- Use the included `AW-NHS.API/AW-NHS.http` file to run example requests.
- Also available using swagger `http://localhost:{port}/swagger` after starting the API.
