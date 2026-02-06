# Encryption API CI/CD (C# .NET 8)

Detta repo innehåller ett enkelt C#-API med två endpoints för kryptering och avkryptering via ett Caesar-chiffer.  
Syftet är att visa en komplett CI/CD-kedja med Git Flow, GitHub Actions (CI) och deployment till AWS Elastic Beanstalk (Docker).

## Funktioner
- `POST /encrypt` – krypterar text med Caesar-shift
- `POST /decrypt` – avkrypterar text med samma shift
- Enhetstester med xUnit
- CI: build + test på Pull Requests
- CD: deploy till Elastic Beanstalk på push till `main`

## Projektstruktur
```text
.
├─ src/
│  └─ EncryptionApi/
│     ├─ EncryptionApi.csproj
│     └─ Program.cs
├─ tests/
│  └─ EncryptionApi.Tests/
│     ├─ EncryptionApi.Tests.csproj
│     └─ CaesarCipherTests.cs
├─ .github/workflows/
│  ├─ ci.yml
│  └─ deploy.yml
├─ Dockerfile
└─ README.md


````

## API-endpoints

### Health
```
curl -s http://localhost:5000/
````

Exempel-svar:

```json
{ "message": "Encryption API is running" }
```

### Encrypt

curl -s -X POST http://localhost:5000/encrypt \
  -H "Content-Type: application/json" \
  -d '{"text":"Hello, World!","shift":3}'


Exempel-svar:

{ "result": "Khoor, Zruog!" }


### Decrypt

curl -s -X POST http://localhost:5000/decrypt \
  -H "Content-Type: application/json" \
  -d '{"text":"Khoor, Zruog!","shift":3}'


Exempel-svar:

{ "result": "Hello, World!" }


## Lokal utveckling

### Restore + Run

### Restore + Run
```bash
dotnet restore src/EncryptionApi/EncryptionApi.csproj
dotnet run --project src/EncryptionApi/EncryptionApi.csproj
```

### Test

dotnet test tests/EncryptionApi.Tests/EncryptionApi.Tests.csproj -c Release


## Git Flow / Branch-strategi

* `main`: produktion (deploy triggas här)
* `develop`: integration
* `feature/*`: ny funktionalitet (PR till `develop`)

## CI (GitHub Actions)

Workflow: `.github/workflows/ci.yml`

* Körs på PR mot `develop` och `main`
* Steg: checkout → setup .NET → restore → build → test

## CD (Deploy till Elastic Beanstalk)

Workflow: `.github/workflows/deploy.yml`

* Körs på push till `main`
* Skapar zip-bundle → upload till S3 → create EB application version → update environment

### Secrets som krävs i GitHub

Gå till: Settings → Secrets and variables → Actions

* `AWS_ACCESS_KEY_ID`
* `AWS_SECRET_ACCESS_KEY`
* `EB_S3_BUCKET`

## AWS-miljö

* Region: `eu-north-1`
* Elastic Beanstalk Application: `crypto-cicd-api-csharp-prod`
* Elastic Beanstalk Environment: `Crypto-cicd-api-csharp-prod-env`

## FigJam – CI/CD Process (Fullstack)


- FigJam-skiss (branch-struktur, CI/CD-flöde, frontend + backend):
- Länk:
POST https://www.figma.com/board/1ikMbMOjL8GRUHpV7vqxf0/Untitled?node-id=0-1&p=f&t=574xEygKNPx845tv-0

**Innehåll i skissen:**
- Git Flow: `feature/*` → PR → `develop` → PR → `main`
- CI (GitHub Actions): build + test på Pull Requests
- CD (GitHub Actions):- CD: deploy till Elastic Beanstalk när PR mergas till `main`
- Fullstack: exempel på frontend build/test/deploy + backend build/test/deploy




