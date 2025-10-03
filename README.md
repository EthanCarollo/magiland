# Magiland

## 🎯 Contexte du projet
Ce projet a été créé dans le cadre d'un **travail étudiants** visant à découvrir et expérimenter le moteur Unity.  
L'objectif était de travailler en équipe et de mettre en place un **pipeline de développement complet**, avec gestion du code via GitHub et structuration d'un projet Unity.

---

## 💡 Pitch du jeu
Le jeu est un **FPS arcade** où le joueur incarne un lutin malade qui a pris trop de médicaments et qui fait un énorme cauchemar.  
L'idée principale : **des combats nerveux**, un système d'armes interchangeables et une boucle de gameplay simple mais efficace.

---

## 🎮 Gameplay & Features

### Déplacements
- Déplacement fluide basé sur la physique.
- Support clavier/souris et manette.

### Caméra
- Système de **mouselook** avec pitch/yaw séparés.
- Compatible joystick droit.

### Système d’armes
- Chaque arme possède différents : 
  - sons
  - statistiques
  - animations 
  - façons de tirer
- Système de collision des balles pour ne pas traverser le bus.
- Switch entre les différentes armes.

### Ennemis
- Ennemis avec différents : 
  - sons
  - statistiques
- Déplacement vers le joueur automatique

### Spawn System
- Zone de spawn in game.
- Fréquence de spawn prédéfini

### UI
- Menu Pause.
- Game over
- Point de vies
- Indices de jeux

---

## ⚙️ Spécifications techniques

- Dessins de tous les assets.
- **Scriptable objects** pour la gestion des statistiques et des listes databases (comme celle des armes).
- **Singleton** pour les controllers et **DontDestroyOnLoad** pour garder les paramètres d'une scène à l'autre.
- Utilisations des **events** pour communiquer des informations d'un objet à plusieurs autres facilement.
- UI responsive.
- Compilation fonctionnelle et déployée.
- Inspiration du jeu **Mouse**

---

## 👨‍💻 Développeurs
- **Ethan**
- **Emmanuel**

---

## 🚀 Lancer le projet
1. Cloner le dépôt  
   ```bash
   git clone https://github.com/votre-org/votre-projet-fps.git
   ```
2. Ouvrir le projet dans **Unity 6.2.x** avec URP installé.
3. Lancer la scène `MainMenuScene.unity`.
4. Jouer !
