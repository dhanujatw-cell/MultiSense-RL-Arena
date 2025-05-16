# MultiSense-RL-Arena

MultiSense-RL Arena is a reinforcement learning framework for multi-sensor edge-based surveillance, using Unity ML-Agents, Stable-Baselines3,Perception Package and custom RL policies.

[Screencast from 15-05-2025 19_49_37.webm](https://github.com/user-attachments/assets/f6cde4fb-2465-42fb-97db-af75cceaa267)

## Features

- 🧠 Multi-agent/Single-agent reinforcement learning
- 🎮 Unity simulation for edge-camera setups
- 📦 Built-in object detection support
- 🔀 Real-time manupulation of multi-camera sensors


### 📂 Code

Relevant code is available at:  
https://smu-my.sharepoint.com/:f:/r/personal/dhanujaw_2020_phdcs_smu_edu_sg/Documents/MultiSense-RL-Arena?csf=1&web=1&e=HoaoCl


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
### ✅ Unity Environment Configuration
![BigCity](https://github.com/user-attachments/assets/c887e46d-c71f-4677-b937-825cc56290a1)

A GUI-level control of the following configurations is available in the first Unity scene (see Figure&nbsp;1).  
In addition to the GUI, users can optionally control the same parameters via a YAML file for automated or scripted setups.

- **Environment to spawn** – Choose from: `Bank`, `City`, `Intersection`
- **Lighting level** – Adjust the global illumination/intensity for the scene
- **Number of humans to spawn** – Select how many characters appear, and optionally the type of activities they perform (from a predefined list)
- **Camera placements** – Choose from a set of predefined camera positions
- **Number of cameras to spawn** – Select how many cameras will be instantiated from the available positions

The prefabs populated in the scenes are designed in a reusable and modular way, making them easily extendable for related surveillance applications or research use cases.

#### Intro Scene

![Intro](https://github.com/user-attachments/assets/44e1c1ea-9d9b-4d2f-9fe1-578cde3eca0b)
Location: Assets/Scenes/Intro scene
This scene gives handle to congifure the environment as mentioned above. With the button press, Unity will spawn the selected environment with the requested settings.

#### Active Scene

Location: Assets/Scenes/Active scene
This scene will be spawned with the button clicked. This holds the spawners for people, vehicles, cameras and lightings.

There is an additional configuration to the number of people you spawn in the environment. You can configure the probabilies of each behavior they perform by setting the slider bar available in the PeopleSpawner GameObject inside Active Scene.
![people_with_probabilities](https://github.com/user-attachments/assets/ed6dff1a-5c01-49b9-8ee1-c61527b55d55)

#### Customise your scene

There are few adjustments you need to consider when you plugin your new environment to this framework. 
1. place the environment prefab file inside the Assets/Resources/Environment/YOUR SCENE
2. Go to Intro Scene. Add your environment name as a new dropdown option in the Canvas->Environment.
3. Add a few camera mount GameObjects to your scene at suitable positions to programmatically spawn your cameras. Refer to the CameraMounts GameObject attached to each Environment prefab.
4. Include the places you would like to spawn the people to an existing tag called Floor. In essence, people will be spawned on the gameobjects assigned to this tag.
5. Add vehicles or any moving objects to your environment refering to Cars GameObject. It is noteworthy to go through the CarWaypointFollower.cs script to assign a trajectory to any moving objects.


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


### 📚 Citation

If you use this work, please cite:

```bibtex
@article{YourCitation2025,
  title     = {MultiSense-RL-Arena: Multi-Sensor Reinforcement Learning Framework},
  author    = {Your Name},
  year      = {2025},
  journal   = {GitHub Project}
}

