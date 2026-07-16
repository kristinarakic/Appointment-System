# ADR-004: Event mehanizam za međumodulsku komunikaciju

**Datum:** Jun 2026  
**Status:** Prihvaćena

## Kontekst

Appointments i Notifications moduli moraju da komuniciraju - kad se kreira termin, treba da se kreira obaveštenje. Ali po pravilu modularnog monolita, moduli ne smeju da zavise jedan od drugog direktno.

## Odluka

Koristimo domenski event (AppointmentCreatedEvent) koji živi u SharedKernel-u. EventDispatcher u Infrastructure-u pronalazi i poziva registrovane handlere kroz DI kontejner.

## Razlog

Direktna referenca Appointments → Notifications bi stvorila zavisnost koja krši pravilo modularnog monolita. Event mehanizam omogućava potpunu labavu spregu - Appointments emituje event ne znajući ko ga sluša, Notifications reaguje ne znajući ko ga emituje.

## Posledice

✅ Potpuna labava sprega između modula  
✅ Appointments ne zna da Notifications postoji  
✅ Novi moduli mogu da reaguju na evente bez izmene postojećeg koda  
✅ Lako testiranje - mock EventDispatcher u testovima  
❌ Teže praćenje toka izvršavanja (treba znati ko sluša koji event)
