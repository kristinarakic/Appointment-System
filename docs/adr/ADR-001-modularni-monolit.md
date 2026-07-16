# ADR-001: Izbor modularnog monolita kao arhitektonskog stila

**Datum:** Jun 2026  
**Status:** Prihvaćena

## Kontekst

Sistem zahteva jasnu organizaciju koda sa više poslovnih domena (klijenti, usluge, zaposleni, termini). Razmatrane su tri opcije:

- **Klasični monolit** — sav kod u jednom projektu bez jasnih granica
- **Modularni monolit** — jedan izvršni proces sa logički odvojenim modulima
- **Mikroservisi** — odvojeni procesi koji komuniciraju preko mreže

## Odluka

Koristimo modularni monolit sa pet funkcionalnih modula i SharedKernel-om kao zajedničkom osnovom.

## Razlog

Mikroservisi su previše kompleksni za desktop aplikaciju — zahtevaju mrežnu komunikaciju, zasebne deploymente i distribuiranu konfiguraciju bez realne koristi.
Klasični monolit ne pruža jasne granice između domena. Modularni monolit daje najbolji balans — jasne granice, jednostavan deployment, mogućnost buduće ekstrakcije u mikroservise.

## Posledice

✅ Jednostavan deployment (jedan .exe)  
✅ Jasne granice između modula  
✅ Mogućnost buduće ekstrakcije u mikroservise  
❌ Svi moduli dele isti proces i ne mogu se nezavisno skalirati
