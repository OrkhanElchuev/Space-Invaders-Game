; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Space-Invaders-Game"
#define MyAppVersion "1.0"
#define MyAppPublisher "ElchuevOrkhan"
#define MyAppURL "https://github.com/OrkhanElchuev/Space-Invader-Game"
#define MyAppExeName "SpaceInvaders.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{F07DE088-C261-44E1-BF53-8AADAF486607}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=C:\Users\Orkhan\Desktop\Orkhan\GameDevelopment\unity\SpaceInvaders\Game\Installer
OutputBaseFilename=Space-Invaders Setup(x86)
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Orkhan\Desktop\Orkhan\GameDevelopment\unity\SpaceInvaders\Game\Windows (x86)\Space-Invaders-Game\SpaceInvaders.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Orkhan\Desktop\Orkhan\GameDevelopment\unity\SpaceInvaders\Game\Windows (x86)\Space-Invaders-Game\MonoBleedingEdge\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\Orkhan\Desktop\Orkhan\GameDevelopment\unity\SpaceInvaders\Game\Windows (x86)\Space-Invaders-Game\SpaceInvaders_Data\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\Orkhan\Desktop\Orkhan\GameDevelopment\unity\SpaceInvaders\Game\Windows (x86)\Space-Invaders-Game\UnityCrashHandler32\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
Source: "C:\Users\Orkhan\Desktop\Orkhan\GameDevelopment\unity\SpaceInvaders\Game\Windows (x86)\Space-Invaders-Game\UnityPlayer.dll\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
