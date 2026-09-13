# Provenance

## Butchering and Fauna of the Stone Age Compatability Patch

**Mod ID:** `butcheringfotsacompatability`  
**Author and maintainer:** AzureTai  
**Project repository:** https://github.com/AzureTai/butcheringfotsacompatability  
**Vintage Story ModDB:** https://mods.vintagestory.at/butcheringfotsacompatability

This document records the technical provenance of the **Butchering and Fauna of the Stone Age Compatability Patch**: the upstream projects it depends on, historical compatibility work consulted during development, contributed work incorporated with permission, the rules used to decide what compatibility should be implemented, and the provenance of published releases.

This is a living project record. It is not a licence document and it is not the forensic comparison of the original 1.0.0 release. Licensing is handled separately. The detailed technical comparison of 1.0.0 against earlier compatibility projects is retained separately as `PROVENANCE-AUDIT-1.0.0.md`.

---

## 1. Project purpose

This project provides and maintains compatibility between the **Butchering** mod and the modular **Fauna of the Stone Age (FotSA)** animal packs for Vintage Story 1.22.x.

The project is a content-only compatibility layer. It does not replace or fork Butchering or FotSA. Compatibility is supplied through JSON content definitions and conditional patches which target the relevant upstream resources only when the corresponding mods are installed.

The project follows a current-state compatibility model:

1. Preserve native Butchering compatibility wherever it is complete and suitable.
2. Supplement native Butchering coverage where it is only partial.
3. Supply compatibility where no suitable native implementation exists.
4. Re-audit compatibility when current FotSA or Butchering releases change their data structures, identifiers or runtime requirements.
5. Remove or avoid obsolete compatibility definitions when upstream support has made them unnecessary.
6. Patch closely related upstream data defects only where they directly prevent clean Butchering/FotSA compatibility or cause failures in the supported processing workflow.

Historical compatibility projects are reference material, not an authority over current upstream data. Current Vintage Story, current FotSA and current Butchering data take precedence over historical compatibility implementations.

---

## 2. Authoritative upstream sources

### Vintage Story / Anego Studios

Vintage Story defines the asset system, domain and resource addressing model, JSON patch system, patch-operation semantics, content schemas and runtime behaviour within which this compatibility project operates.

The project must therefore conform to Vintage Story's required structures and semantics. Resource domains, target paths and patch operations are not treated as cosmetic text when evaluating or maintaining compatibility.

### Butchering

**Authors/maintainers:** G3rste and CaptainOats  
**ModDB:** https://mods.vintagestory.at/butchering

Butchering defines the carcass-processing system targeted by this project. Where Butchering already provides a complete and appropriate carcass implementation, that native implementation is preferred rather than duplicated.

Butchering is treated as authoritative for its own carcass identifiers, processing behaviours, workloads, storage behaviour, processing rewards and comparable carcass/meat profiles. When an older compatibility project makes a choice which conflicts with the current Butchering implementation, the current Butchering behaviour is preferred where it is technically appropriate for the FotSA animal being mapped.

### Fauna of the Stone Age

**Creator:** Tenth Architect

The FotSA packs define the animals being integrated: their entity identifiers, species and life-stage variants, source harvest data, shapes, textures, sounds and other animal-specific resources.

Current FotSA files are treated as authoritative for the identity and structure of the animals being patched. Historical paths or mappings are not retained when they no longer match the current FotSA pack.

---

## 3. Technical decision rules

Compatibility decisions are made against the current upstream files rather than by mechanically carrying forward an earlier compatibility patch.

The principal rules are:

1. **Native support first.** If Butchering already supplies complete support for a FotSA animal group, the project does not create a competing replacement merely because an older compatibility mod did so.
2. **Bridge partial support.** If native support exists but omits a life stage, gender, domesticated variant, placement behaviour, processing attribute or other required path, only the missing compatibility is added.
3. **Create support where absent.** Where no suitable native Butchering implementation exists, the project may supply dedicated carcass definitions and related content.
4. **Use current FotSA identities.** Entity families, species, genders, life stages and variants are resolved against the current FotSA data rather than historical naming assumptions.
5. **Use current Butchering profiles.** Butchering's current carcass and processing conventions are preferred over obsolete choices from historical compatibility projects.
6. **Preserve valid FotSA source data.** FotSA remains authoritative for its animal-specific source drops and assets. Compatibility-specific processing choices are reconciled with Butchering without unnecessarily replacing valid FotSA data.
7. **Treat runtime failures as compatibility defects.** Invalid harvest references, unresolved collectibles, broken carcass mappings, missing required behaviours and comparable defects discovered through testing are investigated as part of compatibility maintenance.
8. **Validate changes in game.** Published compatibility changes are personally reviewed and tested by AzureTai before release.

### Constrained Vintage Story patch data

Several elements in these JSON patches are technically constrained by the upstream framework and target data:

- mod/resource namespaces and domains;
- target asset paths;
- JSON property paths;
- upstream entity, item, block, shape, texture and sound identifiers;
- dependency conditions;
- Vintage Story patch-operation types such as `add`, `addmerge` and `replace`.

These values are retained according to their actual Vintage Story meaning. In particular, `add` and `addmerge` are distinct operations and are not treated as interchangeable merely to make implementations appear more or less similar. Likewise, namespaces are not normalised away when determining technical identity because they are part of the resource being addressed or created.

---

## 4. Historical compatibility work reviewed

Five earlier public compatibility projects were reviewed during the initial development process. They were treated as **historical technical reference material only**.

The purpose of reviewing them was to identify compatibility territory that earlier authors had encountered, then re-check that territory against the current FotSA packs and current Butchering implementation. Their mappings were not treated as authoritative and were not intended to be merged wholesale into this project.

### Principal current-branch references

#### FotSA Butchering Compat 0.2.1

**Author:** PepeCitron  
**ModDB:** https://mods.vintagestory.at/fotsabutcheringcompat#tab-description

Reference coverage included:

- Fauna of the Stone Age: Casuariidae Plus
- Fauna of the Stone Age: Dinornithiformes Plus
- Fauna of the Stone Age: Elephantidae
- Fauna of the Stone Age: Felinae
- Fauna of the Stone Age: Manidae
- Fauna of the Stone Age: Rhinocerotidae
- Fauna of the Stone Age: Spheniscidae
- Fauna of the Stone Age: Bovinae

#### FotSA Butchering Compat Plus 0.1.1

**Author:** Tyeia  
**ModDB:** https://mods.vintagestory.at/fotsabutcheringcompatplus

Reference coverage included:

- Fauna of the Stone Age: Cervinae
- Fauna of the Stone Age: Chelonioidea
- Fauna of the Stone Age: Iniidae Plus
- Fauna of the Stone Age: Meiolaniidae
- Fauna of the Stone Age: Thylacinidae Plus
- Fauna of the Stone Age: Viverridae Plus
- Fauna of the Stone Age: Vombatidae Plus

These two projects were the main historical compatibility references because their coverage was closest to the then-current Vintage Story 1.22/FotSA/Butchering environment. Their compatibility areas were nevertheless re-audited against the actual current upstream files before being retained, changed, replaced or omitted.

### Older compatibility projects reviewed and discounted

The following projects were also examined early in the analysis, but were quickly discounted as primary implementation references because they targeted substantially older Vintage Story, FotSA and/or Butchering releases. Much of their former coverage was obsolete, already handled upstream, or superseded by the newer compatibility projects above.

#### FoTSA Pack Butchering Compat 1.0.0

**Author:** Tarlin  
**ModDB:** https://mods.vintagestory.at/fotsapackbutchercompat

#### Butchering FotSA quick compat 1.0.1

**Author:** SirFell  
**ModDB:** https://mods.vintagestory.at/show/mod/32832

Historical scope included:

- Bovinae, removed by that project in 1.0.1 as no longer required from Butchering 1.10.7
- Cervinae, removed in 1.0.1 for the same reason
- Chelonioidea
- Felinae
- Iniidae
- Machairodontinae
- Meiolaniidae
- Sirenia, removed in 1.0.1 as no longer required from Butchering 1.10.7
- Spheniscidae
- Thylacinidae
- Viverridae
- Vombatidae

#### Butchering Compat Patch 1.1.0

**Author:** propaneko  
**ModDB:** https://mods.vintagestory.at/butcheringcompatpatch

The documented historical target set was:

- Bovinae 0.2.4
- Cervinae 0.1.6
- Chelonioidea 1.0.2
- Elephantidae 1.0.13
- Felinae 0.2.10
- Iniidae 0.1.3
- Machairodontinae 1.0.25
- Meiolaniidae 0.1.6
- Sirenia 1.0.22
- Spheniscidae 1.0.11
- Thylacinidae 0.1.3
- Viverridae 1.0.4
- Vombatidae 0.4.2

The three older projects above remain part of the historical provenance record because they were inspected, but they did not become the primary basis of the released implementation.

---

## 5. Preserved initial analysis snapshot

The initial 1.0.0 development analysis was preserved as a master snapshot containing the then-current upstream data and the two principal historical reference projects in extracted form.

**Archive:** `ButcheringFotSACompatability_v2_master_analysis.zip`  
**SHA-256:** `435f3019208e3719ef0770466ae4046fc4ce0c0980849463773945eab4a29225`

The snapshot contains, among the analysed material:

- extracted **FotSA Butchering Compat 0.2.1** by PepeCitron;
- extracted **FotSA Butchering Compat Plus 0.1.1** by Tyeia;
- **Butchering 1.14.2**;
- the FotSA pack versions used for the initial 1.0.0 compatibility analysis.

Because the earlier projects are stored in this snapshot as extracted project trees rather than their original downloaded ZIP containers, this checksum identifies the preserved **analysis corpus as a whole**. It is not represented as the original ModDB archive checksum for either earlier project.

For release 1.0.0, compatibility coverage was developed against 17 FotSA packs at their then-current analysed versions, with Butchering 1.14.2 as the declared minimum baseline. The individual FotSA packs remained optional and independently gated.

---

## 6. Contributed work: HeroSpoompls

HeroSpoompls' work is intentionally recorded separately from the historical public projects above because it was **directly offered for incorporation into this project** rather than merely consulted as reference material.

**Contributor:** [HeroSpoompls](https://github.com/HeroSpoompls)  
**Public merge discussion:** https://github.com/AzureTai/butcheringfotsacompatability/issues/1  
**Contribution archive:** `butcheringcompatv2.zip`  
**Internal mod:** `Butchering Compatibility Patch (Fauna of the Stone Age) v2`  
**Internal version:** `2.0.0`  
**Mod ID:** `butcheringcompatv2`  
**SHA-256:** `54a6785be4e23dd020bd55fd11db395891e7acd0d548e9ccc78c9c1e47e48042`

HeroSpoompls approached AzureTai through GitHub Issue #1 and explicitly asked whether their private patch could be implemented into this compatibility project. Their supplied work included GroundStorable repairs, additional Casuariidae/Caninae compatibility coverage and Machairodontinae-related support.

The contribution was **reviewed and reconciled**, not merged wholesale:

- compatibility already present in this project, including overlapping Caninae gender handling, was not duplicated;
- HeroSpoompls' GroundStorable work identified a genuine missing behaviour and was incorporated;
- that discovery became the starting point for a broader AzureTai audit which extended GroundStorable coverage to additional supported FotSA carcasses;
- Machairodontinae coverage was re-audited against Butchering's existing native handling, with useful additional compatibility retained and validated;
- separate FotSA carcass-shape `Center` attachment repairs discovered during the wider 1.0.2 audit were additional project work, not represented as part of HeroSpoompls' original contribution.

HeroSpoompls has been credited as a project contributor from release 1.0.2 onward.

---

## 7. AI use and development responsibility

### Development philosophy

AI is used as a development aid for tasks such as log analysis, compatibility mapping, documentation support, and occasionally artwork. It does not replace AzureTai's development judgement or responsibility.

Every mod release is personally reviewed, integrated, tested, and verified by AzureTai before publication.

AI output is not published blindly or treated as finished work. Any code, data changes, compatibility patches, documentation, or other material that makes it into a release is examined, accepted, and taken responsibility for by AzureTai.

**AI is a tool in the workflow, not the author of the mod.**

---

## 8. Published release provenance

Only published stable releases are recorded in this section. Development and pre-release builds remain part of normal source-control history and are not added here unless they become a published stable release.

### 1.0.0

**SHA-256:** `e328a3a5de9b487045cefb32bf2dfc66be49e3aa08105a8fa4f7d7af6fe828d1`

Initial public release by AzureTai.

Provenance summary:

- built from a fresh review of current FotSA data, Butchering 1.14.2 and Butchering's existing native FotSA compatibility;
- used the earlier PepeCitron and Tyeia projects as acknowledged technical references, together with brief review of the older Tarlin, SirFell and propaneko projects;
- provided conditional compatibility across the initial 17-pack analysed FotSA set;
- preserved complete native Butchering implementations instead of recreating them;
- added missing carcass definitions, missing life-stage/variant coverage and compatibility repairs where current native support was absent or incomplete;
- included independent compatibility work such as additional Caninae, Capreolinae and vanilla-fox coverage, plus upstream data repairs identified during the audit.

A detailed retrospective comparison of this release is maintained separately as `PROVENANCE-AUDIT-1.0.0.md`.

### 1.0.1

**SHA-256:** `68eacbf7a58a2650c515e1f6fae9fca0f2a77def3b89790259fc6f18a41231e0`

Maintenance and compatibility-correction release by AzureTai.

Provenance summary:

- updated compatibility patches so server-only entity/item work was handled on the server side rather than unnecessarily processed by clients;
- corrected the semitamed female Caninae carcass mapping;
- added missing Butchering-style processing rewards across several existing custom carcass definitions using equivalent current Butchering animal profiles as the reference;
- added missing English localisation for Emeidae and Megalapterygidae carcasses;
- confirmed compatibility with Butchering 1.14.3 while retaining 1.14.2 as the minimum declared dependency.

### 1.0.2

**SHA-256:** `96c5ba0210926163d957bf9c7a3f5bf996b680a90e26612729c29628633638c7`

First published release containing acknowledged contributed work from HeroSpoompls.

Provenance summary:

- reviewed and incorporated the useful GroundStorable portion of HeroSpoompls' contributed patch with permission;
- expanded the GroundStorable audit beyond the contributed set to additional affected FotSA carcasses;
- re-audited and reconciled Machairodontinae coverage against Butchering's native support;
- completed broader gendered Caninae carcass corrections;
- independently identified and repaired missing `Center` attachment data on affected FotSA carcass shapes used with `FloatUpWhenStuck`;
- validated carcass identity, storage, ground placement, hooks and processing through dedicated-server/client testing.

The release metadata identifies AzureTai as author and HeroSpoompls as contributor.

### 1.0.3

**SHA-256:** `58d2cc176e02ad60451ecec709947a3da17131a58089e6dd7b51f8e3680861fb`

Compatibility-metadata release.

Provenance summary:

- lowered the declared Butchering minimum from 1.14.2 to 1.13.6 after targeted dedicated-server and connected-client backward-compatibility testing;
- verified carcass persistence and continued processing across a complete server restart under the older baseline;
- made no functional compatibility-definition or gameplay-content changes from 1.0.2.

### 1.0.4

**SHA-256:** `4dcf921a084fda2cd875567eb06154466e5821de92001513450b4f8496a0d8b7`

Current-upstream structural compatibility release.

Provenance summary:

- updated compatibility for **Manidae 1.0.21** following changes to its adult/baby entity layout;
- updated compatibility for **Rhinocerotidae 1.0.26** following corresponding structural changes;
- corrected malformed raw-pelt references which could break final Butchering processing;
- preserved the separate ambient/world-spawned Rhinocerotidae carcass rather than converting it into a Butchering carcass;
- raised the declared minimum Butchering version to **1.14.0** after review and runtime validation of the 1.14.0-1.14.3 branch;
- validated the affected workflows through collection, storage/placement, hooks, bleeding, skinning, final processing and persistence across server restart.

---

## 9. Published release hashes

The hashes below identify the exact stable release archives retained for this provenance record.

| Release | SHA-256 |
|---|---|
| 1.0.0 | `e328a3a5de9b487045cefb32bf2dfc66be49e3aa08105a8fa4f7d7af6fe828d1` |
| 1.0.1 | `68eacbf7a58a2650c515e1f6fae9fca0f2a77def3b89790259fc6f18a41231e0` |
| 1.0.2 | `96c5ba0210926163d957bf9c7a3f5bf996b680a90e26612729c29628633638c7` |
| 1.0.3 | `58d2cc176e02ad60451ecec709947a3da17131a58089e6dd7b51f8e3680861fb` |
| 1.0.4 | `4dcf921a084fda2cd875567eb06154466e5821de92001513450b4f8496a0d8b7` |

New stable releases should be added only after the exact published archive has been finalised. The release archive, its SHA-256, and any materially relevant upstream version changes should then be recorded here.

---

## 10. Authorship, attribution and third-party material

**AzureTai is the author and maintainer of this compatibility project** and is responsible for its compatibility analysis, integration, implementation, corrections, expanded coverage, testing, packaging, documentation and ongoing maintenance.

This project does not claim authorship of the underlying third-party projects or their original material. Vintage Story/Anego Studios, Butchering, FotSA and the historical compatibility projects remain attributed to their respective creators and are subject to their own applicable terms.

References to upstream namespaces, entity IDs, asset paths, carcass identifiers, patch targets and other technical identifiers are used because those identifiers are necessary to address the corresponding upstream resources.

HeroSpoompls' incorporated contribution is explicitly credited rather than represented as newly authored AzureTai work.

A separate `LICENSE` file may define the terms applied to AzureTai's own distributable contributions. This provenance document does not itself grant or alter any licence.

---

## 11. Relationship to the 1.0.0 provenance audit

`PROVENANCE-AUDIT-1.0.0.md` is a separate retrospective technical audit of the first public release. Its purpose is to document the detailed comparison between 1.0.0 and earlier compatibility projects, including file identity, semantic comparison, functional overlap and the evidence available at the time of that audit.

This `PROVENANCE.md` has a different scope. It is the ongoing project record and should remain neutral, factual and maintainable rather than becoming a dispute-response document.

When determining technical identity in either future provenance work or comparison tooling:

- namespaces should remain technically meaningful rather than being normalised away;
- `add`, `addmerge` and `replace` should retain their distinct Vintage Story semantics;
- dependency conditions should remain part of the compared operation;
- target paths and upstream identifiers should remain exact;
- constrained technical similarity should not be confused with file identity or independent authorship by itself.

---

## 12. Maintenance rules for this document

Update this document when a **stable public release** materially changes project provenance, including when:

- a new compatibility area is added;
- an upstream FotSA pack changes structure in a way requiring new mappings or repairs;
- Butchering gains, removes or materially changes native compatibility relied upon by this project;
- compatibility responsibility moves from a custom definition to an upstream-native implementation;
- third-party work is deliberately incorporated;
- a material upstream data repair is added because it directly affects Butchering/FotSA compatibility;
- project authorship or contribution attribution changes.

For each new stable release:

1. retain the exact published release archive;
2. calculate and record its SHA-256 hash;
3. record upstream version numbers where they materially explain the release change;
4. identify any new third-party contribution separately from ordinary reference material;
5. record a moderate, provenance-focused summary rather than duplicating the full changelog;
6. keep development/pre-release history in source control rather than expanding this file with every development build.

Historical evidence should be preserved rather than cosmetically rewritten to make constrained JSON appear artificially different. Accurate source records, hashes, test history and documented technical decisions are more useful provenance than superficial formatting churn.
