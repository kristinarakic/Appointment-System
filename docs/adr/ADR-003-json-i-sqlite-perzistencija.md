# ADR-003: JSON kao primarna perzistencija, SQLite kao alternativa

**Datum:** Jun 2026  
**Status:** Prihvaćena

## Kontekst

Potrebno je skladištenje podataka bez zahteva za eksternim serverom baze podataka. Projekat zahteva demonstraciju zamenjivosti implementacija kroz Dependency Injection.

## Odluka

JsonRepository kao podrazumevana implementacija (jednostavna, bez zavisnosti). SqliteRepository kao alternativa sa EF Core. Obe implementiraju isti `IRepository<T>` interfejs.

## Razlog

JSON fajlovi su čitljivi, lako se debuguju i ne zahtevaju nikakav setup. SQLite je prava relaciona baza koja demonstrira da sistem može da radi sa pravom bazom bez promene poslovne logike. Zamena se vrši promenom jedne linije u `ServiceRegistration.cs`.

## Posledice

✅ Zamena perzistencije jednom linijom u DI registraciji  
✅ JSON fajlovi su čitljivi i lako se debuguju  
✅ SQLite pruža relacione mogućnosti za složenije upite  
✅ Demonstrira Ports and Adapters obrazac u praksi  
❌ JSON nije pogodan za konkurentni pristup
