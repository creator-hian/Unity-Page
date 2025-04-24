# Page Package - Tech Context

## Technology Stack

### Core Technologies

- **Unity Engine**: 6000.0 LTS (Primary development platform)
- **C# Language**: Version 9.0
- **UnityUI**: Unity's built-in UI system
- **Unity Animation**: For page transitions and effects

### Dependencies

- **com.famoz.future-ocean.logging**: v1.0.1 - Provides standardized logging utilities
- **UniTask**: v2.5.0 - For async/await support
- **VContainer (Optional)**: Integration provided for dependency injection

## Development Environment

- **Unity Editor**: Primary development tool
- **Visual Studio**: C# development
- **Git**: Version control system
- **Unity Package Manager**: Distribution mechanism

## Assembly Structure

The package is organized into multiple assemblies for clear separation of concerns:

1. **FAMOZ.FutureOcean.Page.asmdef**

   - Core package functionality
   - Platform-independent code
   - No Unity Editor dependencies

2. **FAMOZ.FutureOcean.Page.Runtime.asmdef**

   - Runtime-specific implementations
   - Unity runtime dependencies
   - No Editor dependencies

3. **FAMOZ.FutureOcean.Page.Editor.asmdef**

   - Editor tooling and utilities
   - Unity Editor dependencies

4. **FAMOZ.FutureOcean.Page.Runtime.\*.asmdef**
   - Extension modules for specific integration (VContainer, etc.)
   - Optional, modular components

## Code Organization

```
Core/
  |- Constants and core interfaces
  |- Logging utilities
Runtime/
  |- Default implementations
  |- Controllers
  |- Pages
  |- Transitions
  |- Events
Editor/
  |- Debug tools
  |- Editor utilities
Extensions/
  |- VContainer/
  |- [Other integration points]
```

## Technical Decisions

### Interface-Based Design

The decision to use an interface-first approach enables flexibility across different exhibits while maintaining a consistent API. All core functionality is defined through interfaces, with default implementations provided.

### Async Operations

Page transitions are handled asynchronously using UniTask to ensure smooth UI without blocking the main thread. This approach allows for complex loading operations during page changes.

### Testability Focus

The codebase is designed for testability, with clear interface boundaries that enable mocking and isolated testing of components.

### Performance Considerations

- Page pooling for frequently used pages
- Asynchronous resource loading
- Optimized animation playback
- Memory management for page assets

## Compilation and Build

The package uses conditional compilation symbols for debug features:

- `ENABLE_PAGE_DEBUG_LOG`: Enables detailed logging for page operations
- Controlled via Visual Studio EditorWindow for improved developer experience

## Technical Constraints

- Unity UI dependency: Currently tied to Unity's built-in UI system
- C# language version constraints based on Unity's .NET compatibility
- Editor tools require Unity Editor contexts
