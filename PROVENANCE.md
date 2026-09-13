# Butchering – Fauna of the Stone Age Compatibility Patch 1.0.0

## Full technical and provenance audit

**Audit date:** 28 August 2026  
**Audited release:** the actual `butcheringfotsacompatability_1.0.0.zip` published on Vintage Story ModDB  
**Repository commit checked:** `db70122b3525d367ff34b651dcb400656df450fd`

## Conclusion

The published mod is a consolidation and extension of the same compatibility area covered by the two earlier mods, so substantial overlap in purpose, entity paths, carcass mappings and JSON structure is expected. The audit confirms that both earlier mods were used as references.

It does **not** support the claims that the release is a “99%” copy, that its files match the earlier projects line for line, or that `cervini.json` is exactly identical.

- No file in the published release is byte-for-byte identical to any file in either earlier compatibility mod.
- No shipped JSON file is semantically identical to either earlier mod after formatting differences are removed.
- No shipped JSON file becomes identical merely by replacing the compatibility namespace.
- Six simple mapping files become equivalent to Tyeia's files only after the audit also treats Vintage Story's meaningfully different `add` and `addmerge` operations as interchangeable.
- The release contains six functional patch files with no equivalent purpose in either earlier mod, plus material changes inside many overlapping files.

This is a technical provenance finding, not a legal opinion. Public source availability and attribution do not by themselves settle copyright or licensing questions.

## Scope

The published package contains exactly 50 files:

| Category | Files |
|---|---:|
| Custom carcass definitions | 12 |
| Compatibility patch files | 35 |
| Upstream data-fix patch files | 1 |
| Language files | 1 |
| `modinfo.json` | 1 |
| **Total** | **50** |

All 50 files are JSON. The package contains no copied textures, models, sounds, C# source, DLLs, PDBs or executable files.

Comparison sources:

- PepeCitron, FotSA Butchering Compat 0.2.1
- Tyeia, FotSA Butchering Compat Plus 0.1.1
- Butchering 1.14.2 and 1.14.3
- The exact FotSA versions identified on the release page
- The current published GitHub repository

## Whole-package comparison

| Test | PepeCitron | Tyeia | Butchering 1.14.2 |
|---|---:|---:|---:|
| Byte-identical files, including relocated files | 0 | 0 | 0 |
| Semantically identical JSON files | 0 | 0 | 0 |
| Identical after normalising only compatibility namespaces | 0 | 0 | 0 |
| Identical after also removing `dependsOn` | 0 | 0 | 0 |
| Identical after also treating `addmerge` as `add` | 0 | 6 | 0 |

The six mapping files in the final row are:

- Chelonioidea
- Iniidae
- Dasyuridae
- Diprotodontidae
- Thylacoleonidae
- Vombatidae

They target the same authoritative FotSA entity files and must produce the same Butchering carcass outputs. Their similarity is real, but it is limited and is not evidence that the whole package is identical.

## Operation-level comparison

The release contains 156 JSON patch operations:

- 104 `addmerge`
- 37 `replace`
- 15 `add`

After normalising the compatibility namespace:

- 10 operations correspond exactly to Tyeia operations. These are the simple operations which enable the ten retained custom item definitions.
- A further 24 correspond only if `addmerge` is deliberately treated as the same as `add`.
- The remaining 122 operations do not correspond to Tyeia operations under that comparison.
- No Pepe operation remains exactly equivalent under the same test.

Therefore, even at the smaller operation level, the evidence does not support a 99% figure.

## Why the custom carcass files have the same shape

All ten Tyeia-derived subject areas and the two carcass definitions retained from Pepe use the standard Butchering carcass schema. When the animal-specific item code is replaced with a placeholder, every one of the release's 12 carcass files has the same field layout as 12 native Butchering carcass definitions.

That layout includes the required variant groups, textures, blood data, workloads, shapes, storage flags and transition definitions. It is a Butchering data contract, not a structure unique to either earlier compatibility mod.

The release keeps that required structure while changing the values. Across the ten carcass definitions corresponding to Tyeia's mod, 178 leaf values differ. Across the two retained definitions corresponding to Pepe's mod, 21 leaf values differ.

## `cervini.json`

The claim that `itemtypes/cervini.json` is exactly identical is false.

The release version has 13 changed leaf values, including:

- adult mass: `0.20` to `0.08`
- adult blood amount: `600` to `200`
- adult workload: `large` to `medium`
- adult carcass model: generic huge to generic large
- baby workload: `medium` to `small`
- storage flags: `2049`/`2050` to `32769`/`32770`
- the compatibility namespace

The paired entity patch also changes `add` to `addmerge` and adds baby coverage.

## Diprotodontidae red-meat choice

The shared use of the red-meat texture is not a persuasive authorship fingerprint.

The authoritative FotSA Vombatidae 0.4.6 adult and baby Diprotodontidae entity definitions both explicitly declare `game:bushmeat-raw` and `game:redmeat-raw` drops. Red meat is therefore supported directly by the parent data.

The release follows a consistent decision rule:

1. Preserve Butchering's native meat treatment wherever Butchering already has an appropriate carcass.
2. Use poultry for poultry.
3. Use bushmeat for small non-poultry animals.
4. Use red meat for larger non-poultry animals.

The release does not simply retain every earlier texture choice. Examples include:

- Pepe's Felinae poultry texture changed to bushmeat.
- Tyeia's Iniidae bushmeat changed to red meat.
- Tyeia's Meiolaniidae red meat changed to bushmeat.
- Diprotodontidae remains red meat because it is the huge tier and the parent data explicitly includes red meat.

## Scientific filenames and `-dae`/`-nae`

The questioned scientific names are present in the authoritative FotSA asset names and entity paths, including `cervini`, `iniidae`, `dasyuridae`, `nandiniidae`, `diprotodontidae`, `thylacoleonidae` and `vombatidae`.

Thirty of the release's 36 patch filenames directly match at least one target asset's basename. Matching the source asset is the clearest maintainable naming convention and is not unique to either earlier mod.

There is also evidence against blind retention: Tyeia used the patch filename `chelonia.json`, while the release uses `chelonioidea.json`, matching the current FotSA module and entity filenames.

Following an earlier project's scientific label without independently checking it can still transmit a mistake. That would be a narrow review error, not evidence that the entire project was copied. No such inherited naming error was found among the current shipped targets.

## Consolidation and new work

The release intentionally covers the same functional territory as the earlier mods. Of its 49 functional files, 43 have a corresponding purpose in the combined earlier projects and six do not. This is 87.8% overlap in **purpose**, not file identity or copied content.

The six new functional patch files are:

- `canina-semitamed.json`
- `caninae-butchering-fixes.json`
- `vanilla-foxes.json`
- `alceini-domesticated.json`
- `odocoileini-domesticated.json`
- `lynx-sounds.json`

Additional work also exists inside files whose general purpose overlaps the earlier mods:

- Cervini baby coverage
- Meiolaniidae baby coverage
- Viverridae baby coverage
- Nandiniidae baby coverage and sound repair
- Bovinae tamed coverage
- Female Dinornithidae workload correction
- Female Manidae blood/workload correction
- Broader Felinae and Spheniscidae matching
- Correct native Rallidae and Anatidae outputs

The Pepe package defined 20 custom carcass groups. The release retains only two corresponding custom groups, Felinae and Spheniscidae. The other 18 are removed in favour of the current authoritative carcasses already supplied by Butchering.

## Functional and packaging validation

| Check | Result |
|---|---:|
| Strict JSON files parsed | 50/50 |
| Patch operations with conditional `dependsOn` | 156/156 |
| Independently gated FotSA modules | 17 |
| Current FotSA entity variants covered | 1,912/1,912 |
| Distinct carcass outputs resolved | 101/101 |
| Direct Butchering targets present in 1.14.2 | 20/20 |
| Direct Butchering targets present in 1.14.3 | 20/20 |
| Direct targets changed between 1.14.2 and 1.14.3 | 0 |
| Duplicate ZIP paths | 0 |
| Unsafe ZIP paths | 0 |

The local validator could not open four patch targets belonging to the unbundled base game: the adult and baby vanilla fox entity files, each targeted on both client and server. Those are not missing-mod errors. Butchering itself targets the same two base-game paths, and all six fox carcass outputs resolve in both audited Butchering versions.

The actual ModDB download contains version `1.0.0` internally and matches the current GitHub project content. The earlier local `-dev.4` archive differs only in `modinfo.json` and is not the published package.

## Attribution and licensing

The ModDB description and GitHub README both name PepeCitron and Tyeia and state that their mods were used as technical reference material. The use of the earlier projects was therefore disclosed, not concealed.

The audited GitHub roots for the two earlier mods did not contain a licence file, and the current AzureTai repository also contains no `LICENSE`, `NOTICE` or `PROVENANCE.md`. Absence of a licence is not permission to copy; equally, it does not turn constrained file paths and required patch mappings into a technical proof of copying.

This audit cannot give a legal conclusion. It can establish that the factual claims of exact file identity, line-for-line identity and 99% identity do not match the published files.

## Evidentiary weaknesses

The project has two avoidable weaknesses:

1. The public repository currently presents the entire release in a single initial commit. That makes the development sequence harder to demonstrate after the fact.
2. There is no dedicated provenance or licence document. The README credits are good, but they do not provide the same detail as a file-by-file decision record.

Neither weakness proves plagiarism, but both make a public dispute easier to start.

## Recommendations

1. Add a `PROVENANCE.md` recording the exact source versions, hashes, decision rules and the changes retained, removed or replaced in each compatibility area.
2. Add a clear licence for AzureTai's original contributions, with a notice that upstream names and assets remain the property of their respective authors and are referenced rather than redistributed.
3. Commit the reproducible comparison/validation scripts and retain the input archive hashes.
4. Use incremental commits for future compatibility updates.
5. Keep the existing credits and technical-reference wording.
6. Do not reorder or cosmetically rewrite constrained JSON merely to make it look different. Verifiable provenance and meaningful functional decisions are stronger evidence than superficial churn.

## Final finding

A fair description is:

> The project used both earlier compatibility mods as acknowledged references and necessarily retains much of the same functional mapping territory. It also removes obsolete duplicated content, redirects substantial coverage to current native Butchering definitions, changes data values throughout the retained custom carcasses, adds missing life stages and variants, fixes upstream compatibility defects, and introduces six new patch areas. The published package is not byte-identical, semantically identical, line-for-line identical or 99% identical to either earlier mod.

