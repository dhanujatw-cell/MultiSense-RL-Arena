# MultiSense-RL-Arena

MultiSense-RL Arena is a reinforcement learning framework for multi-sensor edge-based surveillance, using Unity ML-Agents, Stable-Baselines3,Perception Package and custom RL policies.

[Screencast from 15-05-2025 19_49_37.webm](https://github.com/user-attachments/assets/f6cde4fb-2465-42fb-97db-af75cceaa267)

## Features

- 🧠 Multi-agent/Single-agent reinforcement learning
- 🎮 Unity simulation for edge-camera setups
- 📦 Built-in object detection support
- 🔀 Real-time manupulation of multi-camera sensors

## Pre Installation
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
├── requirements.txt
   ```
## 🛠️ Setup Unity Packages

### ✅ Unity Installation

Install **Unity 2021.3.9f1** using Unity Hub.

![Unity Installation](https://github.com/user-attachments/assets/75aeb102-3c15-4fbd-ada5-c4fa354ce1bf)

---

### 📦 Package Installation

#### 🔹 ML-Agents Package

Install `ml-agents-release_20` by adding it to your Unity project as shown below:

![Install Unity Packages](https://github.com/user-attachments/assets/d0f31edd-67fd-4a3e-9ded-0a9d64e658d1)

#### 🔹 Perception Package

Follow the same procedure to install `com.unity.perception-Release-0.9.0.preview.2`.

> ⚠️ **Important:**  
> Please use the exact package versions provided in this repository.  
> We have made minor modifications to the original packages to enable **real-time annotation generation**.  
> Using the original unmodified packages may result in unexpected behavior.

---
### ✅ Server Installation

To set up the server, it is **recommended** to install all the prerequisite packages using the **same versions** specified in the `requirements.txt` file.  
Please use **Python 3.10.8** for full compatibility. 
To enable GPU-powered acceleration, ensure that you install a **CUDA-compatible version of PyTorch**.  
In our case, we tested the setup using **PyTorch 1.11.0 with CUDA 11.5**.

Navigate to the `ml-agents-release_20` directory and run the following commands:

```bash
pip3 install -e ./ml-agents-envs
pip3 install -e ./ml-agents
```
## 🎯 Use Case: Multi-Camera Surveillance with Intelligent Steering

This framework is demonstrated through a **multi-camera surveillance system** using three fixed-location cameras.  
Each camera is attached to a **resource-constrained edge device**, capable of running lightweight 2D object detectors such as **YOLOv8-n**.

The objective is to **maximize person detection** in a large outdoor environment by steering each camera **only along the yaw (horizontal) axis**.  
The system **intelligently coordinates** the cameras to cover areas where people are present and **dynamically adapts** to their natural movement within the scene.

---

## 🚀 Training & Simulation Steps

We showcase the end-to-end training and simulation workflow based on the above use case.

### 🧠 Step 1: Start the RL Training Server

Start the centralized multi-agent training server by running:

```bash
python3 server_multiagent.py
```
This will launch the DQN-based training for a centralized N-camera policy.
The state, reward, and action spaces are defined in the code according to the surveillance application.
You may modify these definitions based on your own use case.
### 🎮 Step 2: Start Unity Simulation

After configuring the Unity environment (as described earlier), start the simulation by pressing **Play** in the Unity Editor.

This will begin the training process. You can observe the simulation either:

- Visually through the **Unity Game view**
- Or via an **OpenCV-based visualization window**

---

### 🎥 Demo Video

See the system in action:  
**Screencast from 15-05-2025 19:49:37**  
[Screencast from 15-05-2025 19_49_37.webm](https://github.com/user-attachments/assets/f6cde4fb-2465-42fb-97db-af75cceaa267)


##Citation
If you use this work, please cite:
@article{YourCitation2025,
  title={MultiSense-RL-Arena: Multi-Sensor Reinforcement Learning Framework},
  author={Your Name},
  year={2025},
  journal={GitHub Project}
}

