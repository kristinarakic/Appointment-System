# Salon Appointment System

Sistem za zakazivanje termina u frizerskom salonu. WPF desktop aplikacija razvijena uz primenu modularnog monolita, Clean arhitekture i heksagonalnog pristupa.

## Struktura projekta

```
SalonAppointmentSystem.sln
├── src/
│   ├── SalonApp.SharedKernel/                 (bazne klase, interfejsi, eventi, Ensure)
│   ├── SalonApp.Infrastructure/               (JsonRepo, SqliteRepo, EventDispatcher, DI)
│   ├── SalonApp.WPF/                          (prezentacioni sloj, MVVM, bez poslovne logike)
│   └── Modules/
│       ├── Clients/      (.Domain / .Application)
│       ├── Services/     (.Domain / .Application)
│       ├── Staff/        (.Domain / .Application)
│       ├── Appointments/ (.Domain / .Application)
│       └── Notifications/(.Domain / .Application)
└── tests/
    └── SalonApp.Tests/                        (xUnit, Moq, mock repozitorijumi)
```

## Arhitektura

Projekat kombinuje tri arhitektonska pristupa koji se međusobno dopunjuju.

**Modularni monolit** definiše makro strukturu sistema — pet funkcionalnih modula sa jasnim granicama i enkapsulacijom. Moduli komuniciraju isključivo kroz SharedKernel, nikad direktno jedan sa drugim.

**Clean arhitektura** organizuje kod unutar svakog modula — Domain sloj sadrži entitete i poslovna pravila, Application sloj sadrži use case-ove i servise. Zavisnosti teku ka unutra, Domain ne zna za bazu ni UI.

**Heksagonalna arhitektura** definiše način povezivanja komponenti — interfejsi (portovi) žive u SharedKernel-u, konkretne implementacije (adapteri) u Infrastructure projektu. Zamena JSON perzistencije za SQLite je jedna linija u DI registraciji.

## Arhitektonske odluke (ADR)

| ADR | Odluka | Status |
|---|---|---|
| ADR-001 | Modularni monolit kao arhitektonski stil | Prihvaćena |
| ADR-002 | Primena Clean i heksagonalne arhitekture | Prihvaćena |
| ADR-003 | JSON kao primarna perzistencija, SQLite kao alternativa | Prihvaćena |
| ADR-004 | Event mehanizam za međumodulsku komunikaciju | Prihvaćena |
| ADR-005 | Centralizovana validacija kroz Ensure klasu | Prihvaćena |

Detaljni ADR-ovi su dostupni u dokumentaciji.

## Funkcionalni zahtevi

| # | Use Case | Opis |
|---|---|---|
| 1 | Upravljanje klijentima | Kreiranje, izmena i brisanje klijenata sa validacijom |
| 2 | Upravljanje uslugama | Definisanje usluga sa nazivom, trajanjem i cenom |
| 3 | Upravljanje zaposlenima | Registracija zaposlenih sa imenom i specijalnošću |
| 4 | Definisanje radnog vremena | Postavljanje rasporeda po danima u nedelji |
| 5 | Zakazivanje termina | Pronalaženje slobodnih slotova i kreiranje termina |
| 6 | Otkazivanje termina | Promena statusa uz emitovanje domenskog događaja |
| 7 | Pregled termina | Filtriranje po klijentu, zaposlenom i datumu |

## Napredni zahtevi

- **GitHub Actions CI** — automatski build i testovi pri svakom push-u na main
- **Više implementacija repozitorijuma** — JSON i SQLite, zamenjivo kroz DI
- **Mock testiranje** — Application sloj testiran bez infrastrukturnih zavisnosti

## Pokretanje

1. Kloniraj repozitorijum
2. Otvori `SalonAppointmentSystem.sln` u Visual Studio 2022
3. Postavi `SalonApp.WPF` kao startup projekat
4. Pokreni sa `F5`

Aplikacija podrazumevano koristi JSON fajlove. Za SQLite, u `App.xaml.cs` zakomentariši `AddSalonServices` i odkomentariši `AddSalonServicesWithSqlite`.

## Testiranje

Testovi pokrivaju Domain logiku (Appointment entity) i Application sloj (SchedulingService sa mock repozitorijumima).

## Tehnologije

| Kategorija           | Tehnologija                                                   |
|----------------------|---------------------------------------------------------------|
| UI                   | WPF (.NET 8), XAML, MVVM                                      |
| Arhitektura          | Modularni monolit, Clean Architecture, Hexagonal Architecture |
| Dependency Injection | Microsoft.Extensions.DependencyInjection                      |
| Perzistencija        | JSON (System.Text.Json), SQLite (EF Core)                     |
| Testiranje           | xUnit, Moq                                                    |
| CI/CD                | GitHub Actions                                                |
| Verzionisanje        | Git, GitHub                                                   |
