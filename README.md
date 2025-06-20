# FitnessCenter – Korisničko uputstvo

## Opis aplikacije

**FitnessCenter** je aplikacija za upravljanje korisnicima i terminima u fitnes centru. Omogućava rad sa tri vrste korisnika: članovi, treneri i grupe. Aplikacija je prilagođena za rad na srpskom i engleskom jeziku, sa podrškom za tamni i svijetli mod i tri različite teme.

---

## Jezik i tema

- Na početnom ekranu možeš izabrati **jezik**: srpski / engleski.
- U gornjem desnom uglu su **tri tačkice**:
  - **Uključi/isključi tamni mod** (Dark Mode)
  - **Izađi iz aplikacije**

---

## Prijava i registracija

### **Prijava:**
- Unosi se **korisničko ime** i **lozinka**.

### **Registracija:**
- Obavezni podaci: korisničko ime, lozinka, potvrda lozinke, email, broj telefona.
- Bira se tip naloga: **član**, **trener**, **grupa**

#### ➤ Ako je izabran ČLAN:
- Dodatno se unosi: ime, prezime, datum rođenja, datum upisa

#### ➤ Ako je izabran TRENER:
- Dodatno se unosi: ime, prezime, specijalizacija, radno vrijeme

#### ➤ Ako je izabrana GRUPA:
- Dodatno se unosi: ime grupe, opis, maksimalan broj članova
- Bira se trener iz padajuće liste

---

## Glavni meni (lijevo)

Svaki tip naloga ima meni sa lijeve strane:
- **Podešavanja** (tema, mod, font)
- **Gašenje aplikacije**

---

## ČLAN – funkcionalnosti

🔹 **Rezervacija termina**
- Pregled dostupnih termina
- Mogućnost rezervacije
- Pregled već rezervisanih termina

🔹 **Članstva**
- Pregled aktivnih članstava
- Dodavanje novog članstva (dugme + u donjem desnom uglu)

🔹 **Naplata članarine**
- Pregled neplaćenih članarina
- Klikom na članarinu → odabir načina plaćanja, cijene, dostupnih promocija
- Pregled istorije plaćenih članarina

---

## TRENER – funkcionalnosti

🔹 **Pregled termina**
- Svi njegovi termini
- Pretraga po nazivu i sali
- Izmjena termina (vrijeme/sala)
- Brisanje termina

🔹 **Dodavanje termina**
- Unos termina i izbor sale

🔹 **Pregled sala**
- Prikaz naziva, lokacije i kapaciteta svake sale

---

## GRUPA – funkcionalnosti

🔹 **Pregled članova**
- Lista svih članova grupe
- Informacije o treneru grupe
- Zauzeće grupe (trenutno vs maksimalno članova)
- Dodavanje novog člana (dugme + → lista dostupnih članova)

---

## Tehničke napomene

- Aplikacija koristi WPF za korisnički interfejs
- Podaci se čuvaju u bazi podataka
- Dark Mode i podešavanja jezika su sačuvana između sesija
