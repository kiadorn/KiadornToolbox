# KiadornToolbox
A collection of reusable patterns and utilities for Unity projects, designed for personal use.

## Features

### State Machine
Finite State Machine pattern.

### Object Pool
Wrapper to Unity's Object Pool implementation.

### Entities
Components relevant for "actors", like locomotion and animations.

### Custom Attributes
E.g. RequireInterface attribute to validate Behaviour references with an implemented interface.

### Animation Events
Animation State Machine Behaviours used as alternative Animation Events.

### Animation Rigging Utilities
For controlling Unity Rigging constraints through scripting.

### Audio Manager
Simple global audio manager with FMOD support.

### Save System
Simple save system with Unity Serialization or Json Utility support.

### Behaviour Tree (WIP)
Not fully implemented.

### Custom Unity Objects (deprecated)
Custom Mono Behaviour and Scriptable Objects, first intended to be inherited by all components in order to apply global behaviours like batched Update() calls.

Will instead be replaced by interface implementations instead.

### And more... 

## Release

Run "npm run release" to bump up version and generate changelog.