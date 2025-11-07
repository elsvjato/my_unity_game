# My Unity Game - 2D Platformer

A story-driven platformer prototype built as part of my university coursework. The project explores moment-to-moment traversal, collectible-driven progression, and an AI-driven ghost enemy that actively hunts the player across tilemap layouts—a feature I personally envisioned and implemented to push the gameplay beyond baseline requirements.


📱 About

My Unity Game is a side-scrolling adventure where platforming meets reactive enemy behaviour. You guide a pixel-art hero through handcrafted levels, collect coins, and manage health while a spectral pursuer uses pathfinding to cut off escape routes. Integrating the AI ghost chaser was my own initiative during development, meant to demonstrate applied pathfinding inside a 2D platformer.


✨ Features

Core Gameplay

Player Movement: Responsive 2D controller with jump, mid-air attack, ladder climbing, and grappling rope traversal
Collectible Loop: Coins, score tracking UI, and particle feedback reinforce exploration
Health System: Heart-based UI, temporary invulnerability frames, and damage zones
Enemy Variety: Patrolling enemies, slam traps, and a ghost that adapts to the player’s path
Checkpoints & Effects: Smoke VFX, audio cues, and scripted interactions to pace progression

AI & Systems

Ghost Hunter AI: Tilemap-aware chaser powered by an A* pathfinding implementation (`TilemapPathfinding` + `PriorityQueue`) that I added to elevate the project’s challenge and academic scope
Dynamic Path Refresh: Enemy recalculates routes at runtime, respecting walkable zones and player movement
Environmental Awareness: Physics-based triggers, edge detectors, and hazard scripts drive enemy reactions

World Building

Scenes: Playable `Game` level and `Main Menu` hub crafted with Unity Tilemap and Cinemachine framing
Art: Pixel-art sprites, UI elements, and animation clips authored in the Unity Animator
Audio: Music and SFX routed through a `MusicController` for in-game transitions


🛠️ Tech Stack

Engine & Languages

Unity 2022.3.11f1 (LTS) — Core engine and editor
C# — Gameplay scripting, AI, and systems
Unity Input System (Legacy axes) — Keyboard-driven controls

Packages & Libraries

com.unity.feature.2d — Tilemap, SpriteShape, and 2D physics tooling
com.unity.cinemachine — Camera framing for 2D scenes
com.unity.textmeshpro — UI text rendering
Custom Priority Queue — Lightweight generic priority queue supporting A* pathfinding

Design & Assets

Unity Animator Controllers — Player, enemy, and environment animation state machines
Line Renderer + DistanceJoint2D — Grappling rope mechanic
Audio Clips — `Assets/Resources` houses music and SFX for runtime loading


📋 Prerequisites

Before opening the project, install:

Unity Hub with Unity 2022.3.11f1 (or compatible LTS release)
Git (optional, for cloning/pulling updates)
A keyboard & mouse for default control scheme


🚀 Getting Started

Clone the repository

git clone https://github.com/elsvjato/my_unity_game.git
cd my_unity_game

Open in Unity

Launch Unity Hub
Click "Open" and select the cloned folder
Let Unity import assets (first import can take several minutes)

Play & Test

Open `Assets/Scenes/Main Menu.unity` or `Assets/Scenes/Game.unity`
Hit the Play button in the Unity Editor
Use A/D (or arrow keys) to move, Space to jump, E to mid-air attack, and Left Mouse Button to fire the grappling rope at `Ceiling`-tagged anchors

Build (Optional)

File → Build Settings…
Add the required scenes
Target platform: PC, Mac & Linux Standalone
Press "Build" and choose an output folder


📱 Available Scripts

EnemyBehaviourAI.cs — Ghost chaser with tilemap-aware A* navigation
TilemapPathfinding.cs — Pathfinding service caching neighbors and reconstructing optimal routes
hero.cs — Player controller, attack logic, and damage handling
GrapplingRope.cs — DistanceJoint2D-driven grapple mechanic with line renderer visuals
GameManager.cs — Scene-wide state management and audio transitions
ScoreCounter.cs — UI binding for coin collection


🏗️ Project Structure

my_unity_game/
├── Assets/
│   ├── Animation/           # Animator controllers & clips
│   ├── Resources/           # Music, SFX, runtime-loaded prefabs
│   ├── Scenes/              # Main Menu + Game scenes
│   ├── Scripts/             # Gameplay, AI, utilities (C#)
│   └── Sprites/             # Pixel art sprites, tilemaps, UI textures
├── Packages/
│   └── manifest.json        # Unity package dependencies
├── ProjectSettings/         # Unity project and build configuration
├── UserSettings/            # Editor layouts (local only, not required)
└── README.md                # Project documentation (this file)


🔐 Academic Context

This prototype was developed as part of my university coursework to explore intelligent enemy behaviour in 2D platformers. The ghost AI combines tilemap data with runtime pathfinding to deliver a persistent, but fair, challenge.


💾 Save & Data Handling

Unity persists editor-only settings under `UserSettings/`. Runtime data (score, health) is session-based; no persistent saves are written in the current prototype.


🌍 Localization & Accessibility

Language: English-only UI
Accessibility: Keyboard input and visual feedback (hearts, particle effects). Audio cues support certain events, but subtitles/captions are not yet implemented.


🎨 Visual & Audio Direction

Visual Style: Pixel art sprites combined with dynamic lighting and smoke VFX
Audio: Two layered background tracks with SFX for coin collection, bonuses, and environment events


📝 License

This project is shared for educational purposes related to my degree work. Assets (art, audio) remain © their respective creators and are not licensed for commercial reuse.


👤 Author

elsvjato

GitHub: @elsvjato
University coursework submission (2025)


🙏 Acknowledgments

Unity Technologies — Engine & tooling
Community pixel-art resources leveraged during prototyping
Fellow students and mentors who provided playtesting feedback
