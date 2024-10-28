# Todo

## Menu

- [Period](#period)
- [Score](#score)
- [GameClock class](#gameclock-class)
- [JSON Serializer](#json-serilaizer)
- [Backup/Restore Filestorage](#backuprestore-filestorage)
- [UDP Transmitter](#udp-transmitter)
- [Start Up dialog - Set / Get settings](#start-up-dialog---set--get-settings)
- [Under Consideration](#under-consideration)


---


### Period
- Handle negative values
- Reset?
---
### Score
- Handle negative values
- Reset?
---
### GameClock class
- Handle event unregister/register
---
### JSON Serilaizer
- Serialize GameEventArgs
- Event for when serialized
---
### Backup/Restore Filestorage
- Write JSON data to file
- Listen to JSON serialized event
---
### UDP Transmitter
- Send JSON data via UDP Multicast
- Listen to JSON serialized event
---
### Start Up dialog - Set / Get settings
- Restore previous game
- Set/Get settings
---
### Under Consideration
- **Team Class:**
    - TeamId
    - TeamName
    - TeamAbbreviation
    - TeamPenalityList
    - TeamLogo
    - TeamScore  
   
- **Penalties:** {Team team, TimeSpan time, bool active}
    - List to store penalties for each team
    - Add penalties
    - Remove penalties
    - Adjust penalties
    - Adjust time
    - Only two active per team  
   
- **Pre programmed settings for:**
    - Senior regular season games
    - Senior playoff game
    - Youth games
- **SoundPlayer:**
    - TimerEnd sound
