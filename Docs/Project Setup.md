# Project Setup

The following is a document describing the development environment we'll be working in, links to any relevant docs or extensions, and an explanation of why.

## Unity

Version: 2022.3.43f1

> This is the version currently installed on all of the Grid Lab computers, and it throws an Admin prompt whenever you try to install another version. If the Grid Lab gets Unity 6 installed on everything AND Unity 6 is stable and easy, then I suppose we could change. I have no preference, Unity will catch itself on fire no matter what we do.

## Dependencies

I've gone ahead and added what I think will be the bare bones packages we'll need. This list is by no means decisive or well-informed, I honestly just added the things that seem relevant because Unity packages install themselves like the Kool-Aid Man kicking in your front door but do just fine uninstalling themselves. We can prune and adjust later FOR SOME ITEMS.

Also 'Features' appear to just be package bundles that are version locked together, we'll see if that's a timebomb waiting to happen.

### Features

- 3D Characters and Animation
- 3D World Building
- Engineering
- Gameplay and Storytelling
- VR

### Packages

- Animation Rigging 1.2.1
- Burst 1.8.17
- Cinemachine 2.10.1
- Code Coverage 1.2.6
- Collections 2.4.2
- Core RP Library 14.0.11
- Editor Coroutines 1.0.0
- FBX Exporter 4.2.1
- Input System 1.7.0
- JetBrains Rider Editor 3.0.31
- Mathematics 1.2.6
- Oculus XR Plugin 4.2.0
- OpenXR Plugin 1.12.1
- Polybrush 1.1.8
- ProBuilder 5.2.2
- Profile Analyzer 1.2.2
- Serialization 3.1.2
- Shader Graph 14.0.11
- Terrain Tools 5.0.5
- Test Framework 1.4.3
- TextMeshPro 3.0.9
- Timeline 1.7.6
- Tutorial Framework 3.1.3
- Unity UI 1.0.0
- Universal RP 14.0.11
- Version Control 2.6.0
- Visual Scripting 1.9.5
- Visual Studio Code Editor 1.2.5
- Visual Studio Editor 2.0.22
- WebGL Publisher 4.2.3
- XR Interaction Toolkit 2.6.3
    > So far this is the only package that the versioning actually mattered a lot. It looks like as of now (1/16/25) that XR Interaction Toolkit 3.0.XX and Unity 2022.3.XX are mortal enemies.
- XR Plugin Management 4.5.0



## Visual Studio Setup

The .editorconfig and .vsconfig files should take care of this automatically, but I am not sure and don't feel like uninstalling and reinstalling Visual Studio to find out.

### Extensions

[SonarLint / SonarQube](https://marketplace.visualstudio.com/items?itemName=SonarSource.sonarlint-vscode)

This is the best linter I've found so far across both Visual Studio and Visual Studio Code, it does the standard linting you would want but it also offers suggestions to improve your code with documentation that includes explanations of why XYZ would be better or preferred.

### Naming Rules

[.NET Runtime team's coding style](https://leasa6rn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names#naming-conventions)

> It avoids all the common naming issues and it's wells documented. But pretty much for the sheer fact its just standardizedyo, we will obey. SonarLint with the configuration settings should either do this automatically or give you a yellow squiggle warning to do better. You will live.