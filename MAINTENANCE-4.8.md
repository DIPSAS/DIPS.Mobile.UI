# DUI maintenance for Arena Mobil 4.8

## Scope and provenance

- Repository: `DIPSAS/DIPS.Mobile.UI`; branch: `eisv/bugfix-provetaking-4.8`.
- Base: NuGet **60.2.18**, whose package metadata records source commit `e2926f0056d20f644bdb311b308c19e266311a86`.
- Candidate version: **60.6.0**. The minor version reflects additive public APIs, not a general upgrade to the previous 62.x pilot. The stable version was absent from NuGet.org when checked on 15 September 2026; existing `60.6.0-pre444136` and `60.6.0-pre444138` are different builds. Recheck availability before publishing; never overwrite an existing package.
- This is a deliberately limited maintenance line, **not a superset of every intervening 60.x/61.x/62.x release**. MAUI Controls **10.0.51**, the base design resources, global theme behavior and unrelated components are retained.

The changes cover StepFlow back navigation/dynamic Android sizing, bottom-sheet page navigation, scanner messages/hints and the ContentControl, Android Chip and LoadingOverlay fixes. Existing `LockWhenCompleted` and `DuplicateScanCooldown` APIs remain available. Scanner duplicate suppression remains enabled by default; repeated-label scanning is opt-in.

Reference changes from the original DUI history include bottom-sheet navigation (#881), scanner feedback (#895), StepFlow back navigation/invalid-scan reset (#909), bottom-bar automation IDs (#913), dynamic StepFlow sizing (#914) and Android bottom-sheet height (#916). They were narrowed and adapted to preserve the 60.2.18 API, rather than cherry-picking the entire later release. ContentControl, Chip and LoadingOverlay fixes were carried from the previously prepared monorepo maintenance reference.

## Verification status

**Prepared for manual verification, not release-approved.** No tests, builds, device installation or pipelines have been run for this combination. Editor diagnostics and source/diff inspection do not establish that it compiles or behaves correctly on a device. The maintainer runs the checks below.

The original DUI `main`, Arena Mobil workspace and working 4.8 pilot have not been changed by this preparation. The earlier pilot used a different DUI package; its successful tests do not validate this one.

## Local verification (maintainer-run)

Run from this maintenance worktree, not the original DUI `main`. Use the repository SDK/workload configuration and existing NuGet credentials; do not commit credentials or signing files.

1. Run the library unit tests, including the added StepFlow, bottom-sheet, scanner-option and ContentControl regression tests:

   ```sh
   dotnet test src/tests/unittests/DIPS.Mobile.UI.UnitTests.csproj -c Test
   ```

2. Build all package target frameworks and create a distinct local prerelease, without changing the shared source configuration:

   ```sh
   dotnet pack src/library/DIPS.Mobile.UI/DIPS.Mobile.UI.csproj -c Release -p:PackageVersion=60.6.0-prepilot48.1 -o output/pilot48
   ```

   Use a new prerelease suffix for each changed test package to avoid NuGet-cache ambiguity. Do not reuse the earlier 62.1.9 pilot package name.

3. Inspect the package: `net10.0`, `net10.0-ios` and `net10.0-android` assemblies, localized resources, symbols, buildTransitive targets, icons, animations and splash resources must remain present. Check repository commit metadata and dependencies. Verify that no newer MAUI/theme dependency or unrelated resource update slipped in.

4. Test package consumption using a **separate copy/worktree of the complete 4.8 pilot**, including its uncommitted changes. A checkout of its current HEAD alone does not contain the backport. Do not overwrite or commit the existing working pilot just to make the test copy.

## Required Arena Mobil integration in that test copy

1. Change only the `DIPS.Mobile.UI` PackageReference in `src/Arena.Mobile/Arena.Mobile.csproj` to the exact test package version. Point its local NuGet source and package-source mapping to this worktree's output. Keep local app display/configuration version **1.0**, product version **4.8.0**, and other dependency versions unchanged.
2. In `SamplingLabelBarcodeResultStrategy.ConfigureScanner`, add:

   ```csharp
   startOptions.AllowRepeatedBarcodes = true;
   ```

   This setting belongs only to the label-scanning strategy, **not the shared scanner page or patient-armband strategies**. It removes the legacy library-level duplicate suppression for that session, but it does not bypass `ValidateBarcodeAsync`, required-label counts or patient validation. Simply setting `DuplicateScanCooldown` to zero does not enable this behavior.
3. Extend the strategy configuration test to assert this opt-in. Keep regression coverage that accepts the same label up to its required count and rejects further scans. Other strategies must retain the default duplicate suppression.
4. Run the app tests with `-c Test`, build Android and iOS and perform device verification. The date-range fix remains part of the pilot and must not be lost.

These app integration changes have **not** been applied to the working pilot as part of the DUI branch preparation.

## Device checklist

- Open ordinary bottom sheets and the Sampling filter sheet. Navigate into period, ward and sorting choices; verify title, back, close, Android system back, reset/apply actions and root binding context. Also exercise sheets with no internal navigation.
- On iOS, test complete **and cancelled** swipe-back gestures, closing from a pushed page and reopening the sheet. Check handler/subscription cleanup. Navigation inherited from the reference implementation is not a substitute for these checks.
- On Android, test medium/large sheet sizing, keyboard input and back navigation. Verify explicit consumer automation IDs are retained.
- In Sampling, go back to scan/time steps, verify downstream confirmations reset, and verify a finished flow cannot be reopened through `CanGoBack`. Check expansion after dynamic content changes.
- Verify legacy `LockWhenCompleted` behavior in existing StepFlow consumers that do not opt into `CanGoBack`.
- Scan repeated labels up to the expected count, then an extra label; check another patient's label, invalid text, consecutive failures, feedback/tooltips, automatic hints and pause/resume/background/close behavior. A resumed accepted scan must not interrupt the previous reset animation.
- Verify ContentControl null/true/false transitions, Android chip close tint, and repeated/rapid busy changes including handler reconnection in LoadingOverlay.
- Check non-Sampling regression areas: other scanners, patient menus, laboratory ordering, photo capture and ordinary bottom sheets. Use the earlier functioning pilot as a comparison, not as validation of this package.

## Pipeline 1365 (maintainer-run only)

Use [DIPS.Mobile.UI Nuget CD, definition 1365](https://dev.azure.com/dips/DIPS/_build?definitionId=1365), selecting `eisv/bugfix-provetaking-4.8` and checking the source commit. Do not use `main`, monorepo pipeline 1499 or the Components app distribution pipeline for this candidate.

- **Normal branch build:** `buildwindow.sh pack` reads the top changelog version and produces `60.6.0-pre<buildId>` on this branch. The default is deliberately unchanged.
- **Stable package name:** when ready, explicitly set pipeline variable `DUI_USE_STABLE_PACKAGE_VERSION=true`. The build script then uses `60.6.0` from the changelog. This controls naming only; it does not disable publication.
- **Dry run:** set `IsDryRun=true` before any initial pipeline verification. This classic pipeline uses the build script's environment-variable logic, not the YAML parameter of pipeline 1499. In the legacy script, **any nonempty `IsDryRun` or `DryRun` value, including `false`, means dry run**. For a live run, both must be absent/empty. Do not change secret variables.
- **Signing gate:** require the log to show successful package signing. The classic sign step is less strict than pipeline 1499, so this branch's `publish` task now runs `dotnet nuget verify --all` on every downloaded package before publishing any of them, including during a dry run. A verification failure stops publication; it is not bypassed to make the pipeline green. This guard has been prepared but not executed.
- **Known infrastructure issue:** the earlier 1499 attempt failed during NuGet signing with `An internal consistency check failed`. Using 1365 does not prove that issue is resolved. Stop and retain diagnostic evidence if signing fails; do not bypass verification or repeatedly retry without a new hypothesis.

After a signed package is successfully published and restore-tested without local feeds, update the actual 4.8 release branch's package reference and label-strategy opt-in, remove the machine-local source/mapping, then test and share that branch. Until then, colleagues cannot restore this candidate from NuGet.org automatically.