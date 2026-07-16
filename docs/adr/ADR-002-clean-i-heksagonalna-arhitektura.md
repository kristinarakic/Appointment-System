# ADR-002: Primena Clean i heksagonalne arhitekture

**Datum:** Jun 2026  
**Status:** Prihvaćena

## Kontekst

Potrebna je arhitektura koja omogućava testiranje poslovne logike bez zavisnosti od baze podataka i UI-a. Bez jasne organizacije, logika bi se mešala sa infrastrukturnim kodom.

## Odluka

Svaki modul prati Clean arhitekturu (Domain i Application slojevi). Interfejsi (portovi) žive u SharedKernel-u, a implementacije (adapteri) u Infrastructure projektu.

## Razlog

Domain sloj koji ne zavisi ni od čega može se testirati bez pokretanja baze podataka ili WPF-a. Application sloj koji zavisi samo od interfejsa može se testirati sa mock objektima. Ovo je direktno dokazano kroz SchedulingServiceTests koji koriste Moq umesto pravog repozitorijuma.

## Posledice

✅ Visoka testabilnost — servisi se testiraju bez infrastrukture  
✅ Mogućnost zamene perzistencije bez promene poslovne logike  
✅ UI je zamenljiv (WPF → Web API) bez promene domenskog koda  
❌ Inicijalno više fajlova i projekata  
❌ Veća kriva učenja za nove članove tima
