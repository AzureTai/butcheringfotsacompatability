# Butchering – Fauna of the Stone Age Compatibility Patch

**Version 1.0.0 · Vintage Story 1.22.x · Universal content mod**

A focused compatibility add-on for Butchering and the modular Fauna of the Stone Age animal packs on Vintage Story 1.22.x.

This mod extends and repairs compatibility between [Butchering](https://mods.vintagestory.at/butchering) and the Fauna of the Stone Age animal packs.

It preserves Butchering’s native compatibility wherever that coverage is complete, adds only the missing or incomplete definitions, and provides dedicated carcass items where no suitable native Butchering implementation exists.

The patch can be used with one supported FotSA pack, several packs in any combination, or the complete supported collection. It does not require every FotSA pack to be installed.

## Contents

- [About This Patch](#about-this-patch)
- [What This Compatibility Patch Does](#what-this-compatibility-patch-does)
- [Conditional FotSA Support](#conditional-fotsa-support)
- [Supported FotSA Packs](#supported-fauna-of-the-stone-age-packs)
- [Caninae Compatibility Repairs](#caninae-compatibility-repairs)
- [Additional FotSA Data Repairs](#additional-fotsa-data-repairs)
- [Dedicated Carcass Coverage](#dedicated-carcass-coverage)
- [Installation](#installation)
- [Dependencies](#dependencies)
- [Multiplayer](#multiplayer)
- [Existing Worlds](#existing-worlds)
- [Release Notes](#version-100)
- [Credits](#credits)
- [Support](#support)

## About This Patch

This is a small, independent compatibility patch which must be installed alongside Butchering and the supported Fauna of the Stone Age packs.

It does not rewrite, replace, fork or redistribute either Butchering or Fauna of the Stone Age.

The original mods remain fully responsible for their own animals, assets, mechanics and gameplay systems. This patch only supplies additional JSON definitions and conditional patches where the two projects do not currently integrate correctly.

**Butchering:** Replaces immediate animal harvesting with a more detailed carcass-processing system.

Dead animals can be collected, transported, suspended from wall or ceiling hooks, bled, skinned and butchered using the appropriate equipment.

**Fauna of the Stone Age:** Is a modular collection of animal packs created by Tenth Architect. The individual packs introduce a wide range of living and extinct species using separate mod modules.

Because both projects are updated independently, some FotSA animals, life stages and domesticated variants are not currently recognised by Butchering or are handled by incomplete compatibility definitions.

This patch corrects those specific compatibility gaps for Vintage Story 1.22.x.

## What This Compatibility Patch Does

The compatibility requirements were determined through a complete review of:

- Butchering 1.14.2.
- The current Fauna of the Stone Age animal definitions.
- Butchering’s existing native FotSA compatibility.
- FotSA Butchering Compat by PepeCitron.
- FotSA Butchering Compat Plus by Tyeia.

This analysis was performed against the current mod files rather than relying only on the versions advertised as supported on ModDB.

No original mod was rebuilt. This package contains only the additional compatibility definitions and closely related data corrections required for the original mods to work together.

Butchering’s own definitions take priority wherever they provide complete compatibility.

Where native compatibility is only partial, this patch adds the missing animal families, species, life stages or domesticated variants.

Where no suitable Butchering implementation exists, the patch provides its own carcass definitions, processing attributes, shapes, textures and language entries.

## Conditional FotSA Support

The individual Fauna of the Stone Age packs are optional dependencies.

The patch is divided into independent compatibility sections for each supported FotSA pack. Each section is conditionally activated only when its corresponding pack is detected.

You can therefore use this patch with:

- A single supported FotSA pack
- Any group of supported FotSA packs
- All supported FotSA packs together

For example, if your installation contains only three supported FotSA packs, only the three matching compatibility sections will load. Every other FotSA compatibility section will be skipped.

Missing FotSA packs are not treated as missing dependencies. Their assets and entity files are never targeted unless the corresponding mod is present, preventing absent packs from producing missing-asset errors, failed patch warnings or unnecessary console output.

## Supported Fauna of the Stone Age Packs

Compatibility coverage was developed and reviewed against the following FotSA versions:

- [Fauna of the Stone Age: Bovinae](https://mods.vintagestory.at/bovinae) – 0.3.6
- [Fauna of the Stone Age: Caninae](https://mods.vintagestory.at/caninae) – 1.1.8
- [Fauna of the Stone Age: Capreolinae](https://mods.vintagestory.at/capreolinae) – 2.0.16
- [Fauna of the Stone Age: Casuariidae Plus](https://mods.vintagestory.at/casuariidae) – 1.1.11
- [Fauna of the Stone Age: Cervinae](https://mods.vintagestory.at/cervinae) – 0.1.14
- [Fauna of the Stone Age: Chelonioidea](https://mods.vintagestory.at/chelonioidea) – 1.0.8
- [Fauna of the Stone Age: Dinornithiformes Plus](https://mods.vintagestory.at/dinornithidae) – 1.0.23
- [Fauna of the Stone Age: Elephantidae](https://mods.vintagestory.at/elephantidae) – 1.0.18
- [Fauna of the Stone Age: Felinae](https://mods.vintagestory.at/felinae) – 0.2.14
- [Fauna of the Stone Age: Iniidae Plus](https://mods.vintagestory.at/iniidae) – 0.1.7
- [Fauna of the Stone Age: Manidae](https://mods.vintagestory.at/manidae) – 1.0.20
- [Fauna of the Stone Age: Meiolaniidae](https://mods.vintagestory.at/meiolaniidae) – 0.1.11
- [Fauna of the Stone Age: Rhinocerotidae](https://mods.vintagestory.at/rhinocerotidae) – 1.0.24
- [Fauna of the Stone Age: Spheniscidae](https://mods.vintagestory.at/spheniscidae) – 1.0.17
- [Fauna of the Stone Age: Thylacinidae Plus](https://mods.vintagestory.at/thylacinidae) – 0.1.7
- [Fauna of the Stone Age: Viverridae Plus](https://mods.vintagestory.at/viverridae) – 1.0.9
- [Fauna of the Stone Age: Vombatidae Plus](https://mods.vintagestory.at/vombatidae) – 0.4.6

These are the exact FotSA versions used during development and compatibility analysis for release 1.0.0. They identify the verified compatibility baseline and are not hard dependencies requiring every listed pack to be installed.

Additional FotSA packs may be added following compatibility analysis and testing.

## Caninae Compatibility Repairs

Butchering already includes substantial native compatibility for FotSA Caninae, but the current implementation contains several incomplete definitions.

This patch:

- Adds Butchering support for the semitamed Caninae variants omitted from Butchering’s native patches.
- Fixes all Caninae baby carcasses being impossible to skin after collection.
- Adds the missing baby carcass workload and blood type.
- Adds the missing workload to the unused female carcass definition as a defensive correction.
- Replaces the block-like generic Caninae carcass models with proper canine dead and hanging silhouettes.
- Preserves Butchering’s existing carcass-processing and original-animal-drop handling.

### Vanilla Fox Compatibility

Butchering normally excludes its vanilla fox butcherable behaviours whenever FotSA Caninae is installed.

Caninae suppresses the natural spawning of some vanilla fox variants, but it does not remove existing, manually spawned or otherwise present vanilla fox entities.

This resulted in vanilla red and Arctic foxes being impossible to collect or butcher whenever Caninae was enabled.

This patch restores Butchering support for:

- Adult red foxes
- Red fox babies
- Adult Arctic foxes
- Arctic fox babies

The repair is only applied when Caninae is installed, avoiding duplicate behaviours when Butchering’s normal vanilla fox patches are already active.

## Additional FotSA Data Repairs

A small number of FotSA data problems directly interfere with clean compatibility or entity loading.

### Felinae Lynx Sounds

Adds the missing idle-sound mappings for adults and babies belonging to:

- Canada lynx
- Eurasian lynx
- Iberian lynx

This prevents the affected variants from resolving incomplete sound definitions.

### Viverridae Plus

Adds the missing Nandiniidae sound mapping required by the African palm civet entity definition.

This addresses the null-reference entity-resolution errors caused by the incomplete adult Nandinia binotata sound data.

## Dedicated Carcass Coverage

Where Butchering does not provide a complete native carcass implementation, this compatibility patch supplies dedicated definitions for animals including:

- Cervini
- Sea turtles
- River dolphins
- Meiolaniid turtles
- Smaller Felinae
- Penguins
- Dasyurids
- African palm civets
- Viverrids
- Diprotodontids
- Marsupial lions
- Vombatids

The patch uses suitable FotSA assets where they are compatible with Butchering’s carcass system.

Where no safe matching carcass asset exists, an appropriate generic Butchering carcass or meat texture is used instead.

FotSA’s skeletal and decayed carcass models are not used as substitutes for fresh hanging carcasses.

## Installation

This is an add-on compatibility patch. It does not replace Butchering or any Fauna of the Stone Age pack.

1. Install Butchering 1.14.2.
2. Install one or more of whichever supported Fauna of the Stone Age packs you wish to use.
3. Install the Butchering – Fauna of the Stone Age Compatibility Patch.
4. Install the same mod versions on the server and all connecting clients.
5. Restart the game or server.

Do not remove Butchering or the relevant FotSA packs after installing this patch.

## Dependencies

### Required

- Vintage Story 1.22.x
- Butchering 1.14.2

### Optional

- One or more supported Fauna of the Stone Age animal packs

Every FotSA pack is an independent soft dependency. Compatibility content only loads for packs which are actually present; the patch does not require the complete FotSA collection.

## Multiplayer

This is a universal content mod and must be installed on:

- The server
- Every connecting client

All players should use the same version of the compatibility patch, Butchering and the installed FotSA packs.

## Existing Worlds

The patch is intended to be installable on existing worlds.

It does not replace the identifiers belonging to Butchering or the supported FotSA packs.

Existing Caninae baby carcass items should receive the corrected processing attributes after the updated definitions are loaded.

Creating a backup before installing, updating or removing any mod is still strongly recommended.

Do not remove this patch while custom carcasses supplied by it remain stored, placed or suspended in the world.

## Content-Only Package

This is a content-only mod consisting of JSON definitions and language assets.

It contains no C# code, DLL or executable components.

All compatibility is implemented using conditional JSON patches and content definitions.

## Release Status

Version 1.0.0 is the first public release of the compatibility patch.

The compatibility definitions have received detailed static review against Butchering 1.14.2 and the listed FotSA versions, together with targeted in-game testing across the supported packs. Due to the number of animal species, life stages and variants covered, additional edge cases may still be discovered after release.

Please report any affected animal which:

- Cannot be collected after death
- Cannot be placed on a wall or ceiling hook
- Cannot be bled, skinned or butchered
- Displays an incorrect or missing carcass model
- Produces missing textures
- Loses its expected FotSA drops
- Produces client or server errors

## Version 1.0.0

### Compatibility

- Added conditional Butchering compatibility across 17 FotSA animal packs.
- Added independent per-pack compatibility sections, allowing any supported FotSA pack or combination of packs to be used without requiring the complete collection.
- Preserved complete native Butchering implementations rather than duplicating them.
- Added missing carcass definitions and behaviours where native support was absent.
- Added semitamed and domesticated variants omitted from existing compatibility.

### Caninae

- Fixed all FotSA Caninae baby carcasses being impossible to skin.
- Added the missing baby butchering workload.
- Added the missing baby blood type.
- Added the missing female carcass workload as a defensive correction.
- Replaced generic block-like Caninae carcasses with canine dead and hanging silhouettes.
- Restored Butchering support for vanilla red and Arctic foxes while Caninae is installed.

### FotSA Data Repairs

- Added missing idle-sound mappings for the affected Felinae lynx adults and babies.
- Added the missing adult Nandiniidae sound mapping required by Viverridae Plus.

### Packaging

- Implemented entirely as a universal content mod.
- Added no C# code or DLL files.
- Added soft-dependent patches for each supported FotSA module.
- Ensured absent FotSA packs are skipped cleanly without generating missing-asset errors, failed patch warnings or unnecessary console output.

## Compatibility Status and Attribution

This is an independently maintained compatibility patch.

It is not a rewrite, fork or replacement release of either Butchering or Fauna of the Stone Age, and should not be mistaken for an official release of either project.

The patch must be installed alongside the original mods. It contains only the additional compatibility definitions and closely related data corrections required for them to work together.

All original animals, models, textures, sounds, behaviours, gameplay systems and other assets remain credited to their respective creators.

The earlier compatibility mods were used as technical reference material during the compatibility audit. Their definitions were checked against the current Butchering and FotSA files before any equivalent compatibility behaviour was implemented.

## Credits

- G3rste and CaptainOats – Creators and maintainers of Butchering.
- Tenth Architect – Creator of Fauna of the Stone Age and its animal packs.
- PepeCitron – Creator of FotSA Butchering Compat, used as reference material.
- Tyeia – Creator of FotSA Butchering Compat Plus, used as reference material.
- AzureTai – Compatibility analysis, patch implementation, testing, packaging and ongoing maintenance.
- Vintage Story community and server testers – Compatibility reports, testing and reproduction information.

## Support

Problems involving this compatibility patch should be reported through [GitHub Issues](../../issues) or on the Vintage Story ModDB page.

When reporting a problem, please include:

- Your Vintage Story version
- Your Butchering version
- The compatibility-patch version
- The name and version of every installed FotSA pack
- Your complete mod list
- The affected animal and life stage
- Whether the problem occurred while collecting, hanging, bleeding, skinning or butchering
- The type of hook or processing equipment used
- Relevant client and server logs
- Clear steps to reproduce the problem

This project is limited to Butchering and Fauna of the Stone Age compatibility, together with closely related data errors which directly prevent that compatibility from functioning correctly.
