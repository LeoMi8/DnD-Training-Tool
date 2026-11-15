# DnD Training Tool — UML v0.3 (bozza integrata)

Data: 14 novembre 2026. PlantUML sorgente; progettazione concettuale, NON API definitive.

## File
- DnD_TrainingTool_UML_v0_3_overview.puml — panoramica integrazione
- DnD_Characters_UML_v0_3_draft.puml — revisione precedente
- DnD_Content_UML_v0_3_draft.puml — revisione precedente
- DnD_Rules_UML_v0_3_draft.puml — revisione precedente
- DnD_Combat_UML_v0_3_draft.puml — revisione precedente
- DnD_World_UML_v0_3_draft.puml — mondo, mappe, geometrie, interazioni
- DnD_Simulation_UML_v0_3_draft.puml — sessioni, controller, commit, cronologia
- DnD_Training_UML_v0_3_draft.puml — lezioni, esercizi, tentativi e feedback
- DnD_Persistence_UML_v0_3_draft.puml — repository, adapter, versioni, migrazioni

## Decisioni consolidate
- PC/NPC distinti nella build; statistiche condivise e componenti piccoli.
- CharacterSheet persistente distinto da CreatureInstance/Snapshot/State runtime.
- Content dichiara; Rules valida e calcola; Simulation/Combat orchestrano commit atomico.
- Controllo umano/IA/script separato dalla creatura.
- Geometria Square/Hex/Gridless incapsulata in ISpatialRules.
- Training utilizza Simulation, che deve funzionare anche senza Training.
- Repository come porte applicative, adapter JSON/SQLite infrastrutturali.

## Decisioni ancora da risolvere
1. Regole D&D 2024 dettagliate: diagonali, copertura, line of sight, trigger reazioni.
2. Spellcasting multiclasse e incantesimi con effetti complessi.
3. Ordinamento/stacking delle modifiche, trigger ricorsivi e gestione undo/replay.
4. API asincrone dei controller e delle repository; cancellazione e salvataggi concorrenti.
5. Proprietà di `CreatureInstance`: `SimulationState` possiede le istanze, `SimulationParticipant` le referenzia; nel file Simulation è illustrata solo la relazione di riferimento per il participant.
6. Content Pack licenze e risorse distribuite: controllo in fase di packaging/rilascio.

## Verifica
File UTF-8 generati e controllati strutturalmente (tag @startuml/@enduml e parentesi).
Non è stata effettuata una compilazione/rendering PlantUML perché l'eseguibile non è presente in questo ambiente.
