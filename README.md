# Test Report Generator

A desktop application for generating fire resistance test reports as formatted Word documents. Built for fire testing labs, it takes doorset and test metadata through a form, assembles standardised wording from a library of text fragments, and outputs a ready-to-use `.docx` report.

---

## Table of Contents

- [Overview](#overview)
- [Project Structure](#project-structure)
- [Setup](#setup)
- [How to Use](#how-to-use)
  - [Filling in the Form](#filling-in-the-form)
  - [Creating a Report](#creating-a-report)
  - [Previewing the Report](#previewing-the-report)
  - [Keyboard Shortcuts](#keyboard-shortcuts)
- [Content Folder](#content-folder)
- [Output](#output)
- [Running the Tests](#running-the-tests)
- [Test Number Format](#test-number-format)

---

## Overview

| Feature | Detail |
|---|---|
| **UI** | WPF desktop app (Windows) |
| **Output** | `.docx` report via Word COM automation |
| **Standards** | EN and BS fire resistance standards |
| **Doorsets** | Single or non-identical LH/RH doorset configurations |
| **Previews** | Live text summary preview + rendered Word document preview |
| **Settings** | Form state is saved and restored between sessions |
| **Tests** | NUnit unit test suite covering validation, summary generation, and services |

The application merges form inputs — test number, sponsor, address, standard, and doorset specification — with a library of pre-approved standardised wording text files to produce a complete, formatted report document.

---

## Project Structure

```
TestReportGenerator/
│
├── ReportGenerator.WPF/              # Desktop GUI application (entry point)
│   ├── MainWindow.xaml               # Main window layout and bindings
│   ├── ReportGeneratorViewModel.cs   # Command handlers, report creation logic
│   ├── ReportGeneratorModel.cs       # UI state and INotifyPropertyChanged
│   ├── FormSettings.cs               # Settings persistence (LocalAppData)
│   ├── Command.cs                    # ICommand and AsyncCommand implementations
│   ├── InverseBooleanConverter.cs    # XAML bool→bool converter
│   └── StringToVisibilityConverter.cs
│
├── ReportGenerator.Services/         # Core business logic (no UI dependencies)
│   ├── InputValidation.cs            # Test number validation & date formatting
│   ├── SpecimenData.cs               # Doorset specimen record
│   ├── SpecimenSummary.cs            # Generates the specimen description sentence
│   ├── EnumConverter.cs              # Enum definitions + display name helpers
│   ├── CleanupService.cs             # Kills Word processes before generation
│   ├── ProcessService.cs             # Real System.Diagnostics process wrapper
│   ├── IProcessService.cs            # Process abstractions (for testability)
│   ├── ReportDocument.cs             # Word document creation via COM
│   └── TemplateTags.cs               # Template tag constants and file name constants
│
├── ReportGenerator.CLI/              # Command-line interface (legacy)
│   └── Program.cs
│
├── ReportGenerator.Tests/            # NUnit unit test suite
│   ├── InputValidatorTests.cs
│   ├── InputValidatorAdditionalTests.cs
│   ├── CleanUpServiceTests.cs
│   ├── CleanupServiceAdditionalTests.cs
│   ├── SpecimenSummaryTests.cs
│   ├── SpecimenSummaryAdditionalTests.cs
│   ├── GetFormattedDateTests.cs
│   └── EnumConverterTests.cs
│
└── Content/                          # Template files (not committed to repo)
    ├── Template.docx                 # Base Word document — all tags replaced at runtime
    └── Standardized Wording_DONOTMODIFY/
        ├── EN/                       # EN standard text fragment .txt files
        ├── BS/                       # BS standard text fragment .txt files
        └── Same/                     # Standard-agnostic text fragments
```

---

## Setup

### Prerequisites

- Windows 10 / 11
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Microsoft Word installed (required for `.docx` generation via COM)

### Build

```bash
dotnet build
```

### Run the WPF app

```bash
dotnet run --project ReportGenerator.WPF/ReportGenerator.WPF.csproj
```

Or open the solution in Visual Studio and run `ReportGenerator.WPF`.

### Content folder

The `Content/` folder is not committed to the repo. Place it at the following path relative to the project root:

```
Content/
├── Template.docx
└── Standardized Wording_DONOTMODIFY/
    ├── EN/
    ├── BS/
    └── Same/
```

The WPF project resolves the content folder relative to the build output at runtime (`../../../../Content`), so the folder must sit four levels above the build output directory — i.e. at the solution root.

---

## How to Use

### Filling in the Form

| Field | Description |
|---|---|
| **Test Number** | 7-digit number in `YYMMDDX` format (e.g. `2603151`). Turns red if invalid. |
| **Sponsor Name** | Name of the sponsoring organisation |
| **Address** | Sponsor address |
| **Standard** | `EN` or `BS` — determines which wording fragments are used |
| **Is there a sampling report?** | Checkbox — switches the 1.6 section text |
| **Are doorsets identical?** | Checked = both doorsets share the same LH specification. Unchecked = separate RH fields appear. |
| **Doorset fields** | Material, acting type, panels, glazing, insulation, latching, shootbolts, and heat condition direction |

Form state is automatically saved to `%LocalAppData%\TestReportGenerator\settings.json` as you type and is restored the next time you open the app.

### Creating a Report

1. Fill in all required fields (Test Number, Sponsor Name, Address, Standard, and doorset details)
2. Click **Create Document** or press **Ctrl+Enter**
3. If Word is open, you will be asked to close it before proceeding
4. The report is saved to `Documents\TestReports\` as `SponsorName_TestNumber_HHmmss.docx`
5. A dialog will ask if you want to open the report in Word immediately

### Previewing the Report

The right panel has two preview tabs:

| Tab | Description |
|---|---|
| **Text Preview** | Refreshes a plain-text summary of the report content. Updates as you fill in the form. Click **Refresh Text Preview** to regenerate from the content files. Use **Copy Summary** to copy the specimen description sentence to the clipboard. |
| **Word Document** | Click **Refresh Document Preview** to render a full HTML preview of the merged Word document in the built-in browser. |

### Keyboard Shortcuts

| Shortcut | Action |
|---|---|
| `Ctrl+Enter` | Create Document |
| `Tab` | Move to next field (all inputs have explicit tab order) |

### Clearing the Form

Click the red **Clear Form** button to reset all fields to their defaults.

---

## Content Folder

The `Standardized Wording_DONOTMODIFY` folder contains pre-approved `.txt` files for each section of the report. These are read at runtime and injected into the corresponding template tags in `Template.docx`.

**Do not edit these files** unless the wording has been formally approved — they are shared across all reports generated by the application.

Text files are organised by standard:

```
Standardized Wording_DONOTMODIFY/
├── EN/
│   ├── Standard.txt
│   ├── 1.2Title.txt
│   ├── MechPreTestTitle.txt
│   └── ... (one file per report section)
├── BS/
│   └── ... (same file names as EN)
└── Same/
    ├── 1.6Y.txt        # Used when sampling report = Yes
    ├── 1.6N.txt        # Used when sampling report = No
    └── ...
```

---

## Output

Reports are written to:

```
Documents\TestReports\{SponsorName}_{TestNumber}_{HHmmss}.docx
```

The folder is created automatically if it does not exist.

---

## Running the Tests

```bash
dotnet test ReportGenerator.Tests/ReportGenerator.Tests.csproj
```

The test suite covers:

| Test class | What it covers |
|---|---|
| `InputValidatorTests` | Core test number validation cases |
| `InputValidatorAdditionalTests` | Boundary values, null/empty input, `ValidateTestNum` |
| `GetFormattedDateTests` | Date formatting, ordinal suffixes (including 11th/12th/13th edge cases) |
| `CleanUpServiceTests` | Word process kill — basic happy path |
| `CleanupServiceAdditionalTests` | No processes, multiple processes, `HasActiveWordInstances`, non-Word processes untouched |
| `SpecimenSummaryTests` | Core specimen summary generation |
| `SpecimenSummaryAdditionalTests` | Panel variants, PVCu frames, shootbolt states, conjunction matrix |
| `EnumConverterTests` | `ToDisplayName` extensions, `GetXxxType` parse methods |

All services are designed for testability — `IProcessService` and `IProcess` allow the `CleanupService` to be tested without touching real system processes.

---

## Test Number Format

Test numbers follow the format `YYMMDDX` where:

| Part | Description | Example |
|---|---|---|
| `YY` | 2-digit year | `26` |
| `MM` | 2-digit month | `03` |
| `DD` | 2-digit day | `15` |
| `X` | Sequential test index for that day | `1` |

A test number is valid if it is a 7-digit integer, the date portion is a real past or current date, and the value is greater than `1700000`.

---

## Author

Created by TJ Mulrenan.
