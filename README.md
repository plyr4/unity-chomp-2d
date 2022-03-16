# unity-chomp-2d
starter project for chomp proof of concepts in 2D

## summary

the arm is built using transform, colliders, and joint rotations using transform

## controls

use a gamepad. joint controls are:
- left stick horizontal: shoulder
- left stick vertical: elbow
- right stick horizontal: wrist
- right stick vertical: hand
- right shoulder button: jaw

## camera

default orthographic

## Transform-rotation

the arm uses [Transform-rotation](https://docs.unity3d.com/ScriptReference/Transform-rotation.html) to act as arm joints, with colliders as the body, NOT controlled by the physics engine.
