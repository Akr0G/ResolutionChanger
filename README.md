# ResolutionChanger (Windows-Based Auto Display Tuner)

A background automation tool for Windows that dynamically changes your display resolution based on which application is currently running. Designed for gamers and professionals who require different display settings across tasks.

## Overview
This utility enhances productivity and performance by ensuring your display resolution automatically adapts to your active applications. Whether you're launching a game that benefits from 1080p or switching to a workflow that needs full 1440p clarity — ResolutionChanger handles it for you with zero clicks.

**Note:** This repository is private and intended for demonstration purposes only.

## Features
- Automatically adjusts screen resolution based on the detected application  
- Maintains your refresh rate during changes  
- Fully configurable via `config.json`  
- Runs silently in the background  
- Optimized for low overhead and fast switching  

## Tech Stack
- **Language:** Python 3  
- **Platform:** Windows  
- **Tools & Dependencies:**  
  - `pygetwindow`, `win32api`, `psutil` – for app detection and system-level interaction  
  - Batch script (`app.bat`) for execution  
  - JSON-based config system for easy customization  
- **Development Tools:** Git, GitHub, VS Code  

## My Contributions
- Developed the full automation logic for detecting active applications  
- Implemented system-level resolution switching while preserving refresh rate  
- Designed a JSON-based configuration format for user-defined app-resolution pairs  
- Integrated fallback safety checks for unsupported resolutions or inactive monitors  
- Built a lightweight launcher (`app.bat`) for simple startup execution  

## Supported Applications (Examples)
Users can customize resolution rules for any application. Example entries include:
- `VALORANT-Win64-Shipping`  
- `FortniteClient-Win64-Shipping_EAC_EOS`  
- `r5apex`  
- `cod`  
- `cs2`  

These entries are defined in the `config.json` file and can be modified to match any executable.

## Demo
A short demo video or screenshots are available upon request. Please contact me if you'd like to see the system in action.

## Access
If you're a college admissions officer, recruiter, or reviewer and would like access to the code, feel free to reach out. I’ll provide collaborator access upon request.

## Contact
**Email:** [gajula.akhil13@gmail.com]  
**GitHub:** [https://github.com/Akr0G]
