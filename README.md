 <div align="center">

# ⚔️ D&D Training Tool

### Learn the Rules. Experiment with Builds. Master the Game.

**An interactive desktop sandbox for Dungeons & Dragons players and Dungeon Masters.**

![Status](https://img.shields.io/badge/Status-Planning_%26_Design-DAA520?style=for-the-badge)
![Platform](https://img.shields.io/badge/Platform-Windows-0078D4?style=for-the-badge)
![Framework](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge)
![UI](https://img.shields.io/badge/UI-Avalonia-8B5CF6?style=for-the-badge)

*Built with C# · .NET · Avalonia UI*

---

</div>

## 📖 Overview

**D&D Training Tool** is a desktop application designed to help both new and experienced players explore and understand the mechanics of Dungeons & Dragons.

The project aims to provide an interactive environment where users can create characters, experiment with different builds, learn how game mechanics interact, and simulate gameplay scenarios without needing to participate in an actual D&D session.

Beyond character management, the long-term vision is to create a training environment for both **players and Dungeon Masters**, allowing users to practice combat, explore rules, and experiment with different scenarios.

The application is being designed around the **Dungeons & Dragons 5th Edition (2024 ruleset)**, with a modular architecture intended to support future extensions and custom content.

---

## 💡 Motivation

The idea for this project originated from a personal experience with Dungeons & Dragons.

While learning the game, I realized that understanding a character's abilities by reading their descriptions is very different from actually using them during gameplay.

Experimenting with a new class, testing a multiclass combination, or understanding the interactions between different abilities often requires an actual gaming session, other players, and a Dungeon Master.

This creates a significant barrier for beginners who want to become familiar with the game before playing, but also for experienced players who want to experiment with unfamiliar mechanics or character builds.

Dungeon Masters face similar challenges when preparing encounters, evaluating combat scenarios, or learning how to manage unfamiliar game mechanics.

**The main motivation behind this project is to make these experiences accessible in a standalone, interactive environment.**

Rather than replacing traditional D&D sessions, the application is intended to complement them by giving users a place to learn, practice, and experiment independently.

---

## 🎯 Project Goals

The primary goal is to develop a flexible training environment that combines character management, rules automation, and gameplay simulation.

The project focuses on four main objectives:

| | Pillar | Purpose |
|:---:|---|---|
| 🧙 | **Character Management** | Create, customize, and manage characters with automated calculations. |
| 📚 | **Interactive Learning** | Understand game mechanics through practical experimentation. |
| ⚔️ | **Gameplay Simulation** | Test builds, combat strategies, and abilities in controlled scenarios. |
| 🐉 | **Dungeon Master Training** | Practice encounter management and scenario preparation. |

### 🧙 01 — Character Creation & Management

Provide a complete character management system supporting different approaches to character creation:

- **Guided Creation:** A step-by-step process designed to introduce new players to character creation and explain their choices.
- **Free Creation:** A more flexible approach for users already familiar with the game, while respecting the selected ruleset.
- **Homebrew Creation:** Support for custom content and personalized rules.

The application aims to automatically handle derived statistics and interactions between character features, equipment, abilities, and other modifiers.

Users should be able to understand not only their character's current statistics, but also how those values are calculated.

### 📚 02 — Interactive Learning

Help beginners understand D&D mechanics through practical experimentation.

Instead of relying exclusively on rule descriptions, users will be able to observe how different mechanics behave in simulated situations.

The objective is to gradually introduce concepts such as:

- Ability checks and saving throws.
- Attack rolls, damage, and defenses.
- Actions, bonus actions, and reactions.
- Spellcasting and resource management.
- Conditions, effects, and interactions between abilities.

The application should support learning by doing, allowing users to develop confidence with game mechanics before encountering them during actual sessions.

### ⚔️ 03 — Gameplay Simulation

Develop a simulation environment where players can test their characters in controlled scenarios.

The long-term goal includes a turn-based combat simulator capable of managing characters, enemies, abilities, and environmental interactions.

This environment should allow users to compare different character builds, experiment with combat strategies, and understand the practical consequences of their decisions.

### 🐉 04 — Dungeon Master Training

Extend the simulation system to support users interested in learning or practicing the role of Dungeon Master.

Planned capabilities include encounter configuration, enemy management, and scenario simulation.

The objective is to provide an environment where aspiring Dungeon Masters can practice managing encounters and experienced Dungeon Masters can experiment with different configurations before introducing them into their campaigns.

---

## 🏗️ Development Approach

The application is planned as a progressively developed project, starting with a solid foundation for character representation and game mechanics.

Its development follows a modular approach, keeping character data, game rules, simulation logic, and the user interface as independent as possible.

This separation is intended to make the project easier to maintain, extend, and adapt as new features are introduced.

Development will initially focus on a Windows desktop application, with potential expansion to other desktop operating systems and mobile platforms in later stages.

The project also serves as an opportunity to explore software engineering concepts such as object-oriented design, domain modeling, modular architecture, and rule-driven systems.

---

## 🛠️ Technology Stack

| Technology | Purpose |
|---|---|
| **C#** | Primary programming language |
| **.NET 10** | Application framework and core logic |
| **Avalonia UI** | Cross-platform desktop user interface |
| **UML** | Object-oriented modeling and architectural design |

The architecture and implementation details will evolve as the project progresses.

---

## 🚧 Project Status

**Current phase: Planning & Architectural Design**

The project is in its early stages, with the current focus on defining the domain model, identifying the main components, and designing the relationships between game entities.

### 🗺️ Development Roadmap

- [ ] Domain modeling and UML architecture
- [ ] Core character and rules engine
- [ ] Character creation and management
- [ ] Automated statistics and modifiers
- [ ] Interactive learning tools
- [ ] Turn-based combat simulation
- [ ] Dungeon Master training tools
- [ ] Custom content and homebrew editor
- [ ] Cross-platform expansion

*The roadmap is indicative and will evolve throughout development. Features described in this README represent development goals and should not be considered implemented unless explicitly documented.*

---

## 🔭 Long-Term Vision

The ultimate vision is to create a comprehensive, extensible Dungeons & Dragons sandbox where players and Dungeon Masters can freely explore the game.

A place where beginners can learn without pressure, experienced players can experiment with unconventional builds, and aspiring Dungeon Masters can practice running encounters.

> **The goal is not to automate Dungeons & Dragons entirely, but to make its mechanics easier to understand, explore, and master.**

---

<details>
<summary><b>📜 Disclaimer & Legal Information</b></summary>

This is an independent, unofficial project and is not affiliated with, endorsed by, or sponsored by Wizards of the Coast.

Dungeons & Dragons is a trademark of Wizards of the Coast LLC.

Any third-party rules or game content included in future releases will be subject to the applicable licenses and distribution permissions.

</details>
