# Todo

## Menu

- [Period](#period)
- [Score](#score)
- [GameClock class](#gameclock-class)
- [JSON Serializer](#json-serilaizer)
- [Backup/Restore Filestorage](#backuprestore-filestorage)
- [UDP Transmitter](#udp-transmitter)
- [Start Up dialog - Set / Get settings](#start-up-dialog---set--get-settings)
- [Penalties](#Penalties)
- [Under Consideration](#under-consideration)


---


### Period
- ~~Handle negative values~~
- Reset?
---
### Score
- ~~Handle negative values~~
- Reset?
---
### GameClock class
- ~~Handle event unregister/register~~
---
### JSON Serilaizer
- ~~Serialize GameEventArgs~~
- ~~Event for when serialized~~
---
### Backup/Restore Filestorage
- ~~Write JSON data to file~~
- ~~Listen to JSON serialized event~~
---
### UDP Transmitter
- ~~Send JSON data via UDP Multicast~~
- ~~Listen to JSON serialized event~~
---
### Start Up dialog - Set / Get settings
- ~~Restore previous game~~
- ~~Add active penalties to restore~~
- Set/Get settings
---

**Penalties:** {Team team, TimeSpan time, bool active}
- ~~List to store penalties for each team~~
- ~~Add penalties~~
- ~~Remove penalties~~
- Adjust penalties
- Adjust penalty time on gameclock adjustment.
- Only two active per team  
---

### Under Consideration
- **Team Class:**
    - TeamId
    - TeamName
    - TeamAbbreviation
    - TeamPenalityList
    - TeamLogo
    - TeamScore  
   

   
- **Pre programmed settings for:**
    - Senior regular season games
    - Senior playoff game
    - Youth games
    - Tutbyten
- **SoundPlayer:**
    - TimerEnd sound


Current Todo:




---
~~Lägga till fler metoder för utvisningar (Ändra tid, ta bort, lägg till ny dialog)~~
~~Justera så utv-tid justeras med tidsjustering
~~~~Lägg till Utvisningar i återställning~~

Kontroller:
Se över vilka knappar som är mest logiska
Bygga dialogträd
~~Spacebar = toggle~~

Dialoger:
Nya settings
Förvald setting -> slutspel ja/nej -> långt powerbreak ja/nej
Justera tid
Sätt ny tid
Lägg till utvisning -> lag -> spelarnr -> tid ->

Ja/Nej dialoger

Konvertera:
till sekunder double -> timespan from seconds
sätt tid, sträng till -> timespan

--- 
Stäm av med SIF-manual + Icast + Annan manual för att se vilka funktioner som saknas.
Lägg till fler förprogrammerade inställningar