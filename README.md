# MultiSense-RL-Arena

MultiSense-RL Arena is a reinforcement learning framework for multi-sensor edge-based surveillance, using Unity ML-Agents, Stable-Baselines3,Perception Package and custom RL policies.

![Unity Demo](path_to_demo_image_or_gif)

## Features

- 🧠 Multi-agent/Single-agent reinforcement learning
- 🎮 Unity simulation for edge-camera setups
- 📦 Built-in object detection support
- 🔀 Real-time manupulation of multi-camera sensors

## Installation
```bash
git clone https://github.com/dhanujatw-cell/MultiSense-RL-Arena.git
cd MultiSense-RL-Arena
pip install -r requirements.txt
```
## 📦 Usage

This project requires the installation of the Unity Perception package, ML-Agents, and Stable Baselines, as described below.
⚠️ **Important:** Use the **provided packages** and follow the installation instructions precisely. We have made minor modifications
 to the original packages; downloading them directly from the original repositories may lead to **unexpected errors**.

### 📁 Project Structure
```bash
MultiSense-RL-Arena/
├── Unity/
├── Server/
│ ├── ml-agents-release_20/
│ └── com.unity.perception-Release-0.9.0.preview.2/
├── server_multiagent.py
├── README.md
   ```

##Citation
If you use this work, please cite:
@article{YourCitation2025,
  title={MultiSense-RL-Arena: Multi-Sensor Reinforcement Learning Framework},
  author={Your Name},
  year={2025},
  journal={GitHub Project}
}

