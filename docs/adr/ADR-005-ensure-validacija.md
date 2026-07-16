# ADR-005: Centralizovana validacija kroz Ensure klasu

**Datum:** Jun 2026  
**Status:** Prihvaćena

## Kontekst

Validacija unosa se ponavljala u svakom servisu sa istim if/throw kodom. Razmatrane su tri opcije:

- **Validacija u ViewModelima** - logika u UI sloju, ne prenosi se na web verziju
- **FluentValidation biblioteka** - moćna ali kompleksna za 5 entiteta
- **Centralizovana helper klasa** - jednostavno, bez eksternih zavisnosti

## Odluka

Ensure klasa u SharedKernel-u sa statičkim metodama (NotNullOrEmpty, ValidEmail, ValidPhone, NotNegativeOrZero). Servisi pozivaju Ensure metode, ViewModeli hvataju InvalidOperationException i prikazuju poruku korisniku.

## Razlog

Validacija u ViewModelu bi bila vezana za UI - ako se doda Web API, pravila se moraju duplirati. Validacija u servisu je nezavisna od UI-a i ponovo se koristi bez promene. FluentValidation bi dodao novu zavisnost i kompleksnost bez realne koristi za 5 entiteta sa jednostavnim pravilima.

## Posledice

✅ Nema dupliranja koda — pravila su na jednom mestu  
✅ Validacija živi u Application sloju, nezavisna od UI-a  
✅ Lako se testira bez pokretanja UI-a  
✅ Dodavanje novog pravila je jedna linija u Ensure klasi  
❌ Manje ekspresivna od FluentValidation za složena pravila
