; Script d'installation Inno Setup pour GameOn
; Application WPF C# .NET 8.0 avec configuration MySQL

#define MyAppName "GameOn"
#define MyAppVersion "1.0.0.0"
#define MyAppPublisher "Votre Société"
#define MyAppExeName "GameOn.exe"

[Setup]
; Informations de base de l'application
AppId={{A5B8C7D9-1234-5678-90AB-CDEF12345678}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=C:\Users\3very\Desktop\ProduitFinal
OutputBaseFilename=GameOn-Setup-{#MyAppVersion}
Compression=lzma
SolidCompression=yes
WizardStyle=modern
DisableProgramGroupPage=yes

; Langues
ShowLanguageDialog=no

; Privilèges
PrivilegesRequired=admin

[Languages]
Name: "french"; MessagesFile: "compiler:Languages\French.isl"

[CustomMessages]
french.DatabaseConfigPage=Configuration de la base de données
french.DatabaseConfigDescription=Veuillez entrer les informations de connexion à la base de données MySQL.
french.DatabaseServer=Serveur de base de données:
french.DatabasePort=Port:
french.DatabaseName=Nom de la base de données:
french.DatabaseUser=Utilisateur:
french.DatabasePassword=Mot de passe:

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
; Copier tous les fichiers depuis le dossier de compilation
Source: "C:\Users\3very\Desktop\Git Repo\Deploiement\Equipe5_Sudoku-main\GameOn\GameOn\bin\Release\net8.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
; Raccourci dans le menu Démarrer
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
var
  DatabaseConfigPage: TWizardPage;
  DBServerEdit: TEdit;
  DBPortEdit: TEdit;
  DBNameEdit: TEdit;
  DBUserEdit: TEdit;
  DBPasswordEdit: TPasswordEdit;

{ Initialiser la page personnalisée de configuration de la base de données }
procedure InitializeWizard;
var
  ServerLabel, PortLabel, NameLabel, UserLabel, PasswordLabel: TLabel;
begin
  { Créer la page de configuration AVANT la page de sélection du répertoire }
  DatabaseConfigPage := CreateCustomPage(
    wpLicense,
    ExpandConstant('{cm:DatabaseConfigPage}'),
    ExpandConstant('{cm:DatabaseConfigDescription}')
  );

  { Label et champ pour le serveur }
  ServerLabel := TLabel.Create(DatabaseConfigPage);
  ServerLabel.Parent := DatabaseConfigPage.Surface;
  ServerLabel.Caption := ExpandConstant('{cm:DatabaseServer}');
  ServerLabel.Left := 0;
  ServerLabel.Top := 0;

  DBServerEdit := TEdit.Create(DatabaseConfigPage);
  DBServerEdit.Parent := DatabaseConfigPage.Surface;
  DBServerEdit.Left := 0;
  DBServerEdit.Top := ServerLabel.Top + ServerLabel.Height + 5;
  DBServerEdit.Width := DatabaseConfigPage.SurfaceWidth;
  DBServerEdit.Text := 'localhost';

  { Label et champ pour le port }
  PortLabel := TLabel.Create(DatabaseConfigPage);
  PortLabel.Parent := DatabaseConfigPage.Surface;
  PortLabel.Caption := ExpandConstant('{cm:DatabasePort}');
  PortLabel.Left := 0;
  PortLabel.Top := DBServerEdit.Top + DBServerEdit.Height + 15;

  DBPortEdit := TEdit.Create(DatabaseConfigPage);
  DBPortEdit.Parent := DatabaseConfigPage.Surface;
  DBPortEdit.Left := 0;
  DBPortEdit.Top := PortLabel.Top + PortLabel.Height + 5;
  DBPortEdit.Width := DatabaseConfigPage.SurfaceWidth;
  DBPortEdit.Text := '33306';

  { Label et champ pour le nom de la base de données }
  NameLabel := TLabel.Create(DatabaseConfigPage);
  NameLabel.Parent := DatabaseConfigPage.Surface;
  NameLabel.Caption := ExpandConstant('{cm:DatabaseName}');
  NameLabel.Left := 0;
  NameLabel.Top := DBPortEdit.Top + DBPortEdit.Height + 15;

  DBNameEdit := TEdit.Create(DatabaseConfigPage);
  DBNameEdit.Parent := DatabaseConfigPage.Surface;
  DBNameEdit.Left := 0;
  DBNameEdit.Top := NameLabel.Top + NameLabel.Height + 5;
  DBNameEdit.Width := DatabaseConfigPage.SurfaceWidth;
  DBNameEdit.Text := 'gameon';

  { Label et champ pour l'utilisateur }
  UserLabel := TLabel.Create(DatabaseConfigPage);
  UserLabel.Parent := DatabaseConfigPage.Surface;
  UserLabel.Caption := ExpandConstant('{cm:DatabaseUser}');
  UserLabel.Left := 0;
  UserLabel.Top := DBNameEdit.Top + DBNameEdit.Height + 15;

  DBUserEdit := TEdit.Create(DatabaseConfigPage);
  DBUserEdit.Parent := DatabaseConfigPage.Surface;
  DBUserEdit.Left := 0;
  DBUserEdit.Top := UserLabel.Top + UserLabel.Height + 5;
  DBUserEdit.Width := DatabaseConfigPage.SurfaceWidth;
  DBUserEdit.Text := 'root';

  { Label et champ pour le mot de passe }
  PasswordLabel := TLabel.Create(DatabaseConfigPage);
  PasswordLabel.Parent := DatabaseConfigPage.Surface;
  PasswordLabel.Caption := ExpandConstant('{cm:DatabasePassword}');
  PasswordLabel.Left := 0;
  PasswordLabel.Top := DBUserEdit.Top + DBUserEdit.Height + 15;

  DBPasswordEdit := TPasswordEdit.Create(DatabaseConfigPage);
  DBPasswordEdit.Parent := DatabaseConfigPage.Surface;
  DBPasswordEdit.Left := 0;
  DBPasswordEdit.Top := PasswordLabel.Top + PasswordLabel.Height + 5;
  DBPasswordEdit.Width := DatabaseConfigPage.SurfaceWidth;
  DBPasswordEdit.Text := '';
end;

{ Validation de la page de configuration }
function NextButtonClick(CurPageID: Integer): Boolean;
begin
  Result := True;
  
  if CurPageID = DatabaseConfigPage.ID then
  begin
    { Vérifier que les champs obligatoires sont remplis }
    if Trim(DBServerEdit.Text) = '' then
    begin
      MsgBox('Veuillez entrer le serveur de base de données.', mbError, MB_OK);
      Result := False;
      Exit;
    end;
    
    if Trim(DBPortEdit.Text) = '' then
    begin
      MsgBox('Veuillez entrer le port de base de données.', mbError, MB_OK);
      Result := False;
      Exit;
    end;
    
    if Trim(DBNameEdit.Text) = '' then
    begin
      MsgBox('Veuillez entrer le nom de la base de données.', mbError, MB_OK);
      Result := False;
      Exit;
    end;
    
    if Trim(DBUserEdit.Text) = '' then
    begin
      MsgBox('Veuillez entrer l''utilisateur de base de données.', mbError, MB_OK);
      Result := False;
      Exit;
    end;
  end;
end;

{ Fonction pour remplacer une valeur dans le fichier app.config }
function ReplaceConfigValue(ConfigContent: String; KeyName: String; NewValue: String): String;
var
  StartPos, EndPos: Integer;
  SearchStr, OldValue: String;
begin
  Result := ConfigContent;
  
  { Rechercher la clé dans le XML: <add key="KeyName" value="..." /> }
  SearchStr := '<add key="' + KeyName + '" value="';
  StartPos := Pos(SearchStr, Result);
  
  if StartPos > 0 then
  begin
    { Trouver la position de départ de la valeur }
    StartPos := StartPos + Length(SearchStr);
    
    { Trouver la position de fin de la valeur (guillemet de fermeture) }
    EndPos := StartPos;
    while (EndPos <= Length(Result)) and (Result[EndPos] <> '"') do
      EndPos := EndPos + 1;
    
    if EndPos <= Length(Result) then
    begin
      { Extraire l'ancienne valeur }
      OldValue := Copy(Result, StartPos, EndPos - StartPos);
      
      { Remplacer l'ancienne valeur par la nouvelle }
      Delete(Result, StartPos, Length(OldValue));
      Insert(NewValue, Result, StartPos);
    end;
  end;
end;

{ Modifier le fichier app.config après l'installation }
procedure CurStepChanged(CurStep: TSetupStep);
var
  ConfigFile: String;
  ConfigContent: String;
  Lines: TArrayOfString;
  I: Integer;
begin
  if CurStep = ssPostInstall then
  begin
    ConfigFile := ExpandConstant('{app}\GameOn.dll.config');
    
    { Vérifier si le fichier existe }
    if not FileExists(ConfigFile) then
    begin
      { Essayer avec app.config }
      ConfigFile := ExpandConstant('{app}\app.config');
      if not FileExists(ConfigFile) then
      begin
        { Essayer avec GameOn.exe.config }
        ConfigFile := ExpandConstant('{app}\GameOn.exe.config');
      end;
    end;
    
    if FileExists(ConfigFile) then
    begin
      { Charger le contenu du fichier }
      if LoadStringsFromFile(ConfigFile, Lines) then
      begin
        { Reconstruire le contenu complet }
        ConfigContent := '';
        for I := 0 to GetArrayLength(Lines) - 1 do
        begin
          if I > 0 then
            ConfigContent := ConfigContent + #13#10;
          ConfigContent := ConfigContent + Lines[I];
        end;
        
        { Remplacer les valeurs }
        ConfigContent := ReplaceConfigValue(ConfigContent, 'DatabaseServer', DBServerEdit.Text);
        ConfigContent := ReplaceConfigValue(ConfigContent, 'DatabasePort', DBPortEdit.Text);
        ConfigContent := ReplaceConfigValue(ConfigContent, 'DatabaseName', DBNameEdit.Text);
        ConfigContent := ReplaceConfigValue(ConfigContent, 'DatabaseUser', DBUserEdit.Text);
        ConfigContent := ReplaceConfigValue(ConfigContent, 'DatabasePassword', DBPasswordEdit.Text);
        
        { Sauvegarder le fichier modifié }
        SaveStringToFile(ConfigFile, ConfigContent, False);
      end;
    end
    else
    begin
      { Si le fichier n'existe pas, créer un nouveau app.config }
      ConfigFile := ExpandConstant('{app}\app.config');
      ConfigContent := '<?xml version="1.0" encoding="utf-8" ?>' + #13#10 +
                      '<configuration>' + #13#10 +
                      '  <appSettings>' + #13#10 +
                      '    <add key="DatabaseServer" value="' + DBServerEdit.Text + '" />' + #13#10 +
                      '    <add key="DatabasePort" value="' + DBPortEdit.Text + '" />' + #13#10 +
                      '    <add key="DatabaseName" value="' + DBNameEdit.Text + '" />' + #13#10 +
                      '    <add key="DatabaseUser" value="' + DBUserEdit.Text + '" />' + #13#10 +
                      '    <add key="DatabasePassword" value="' + DBPasswordEdit.Text + '" />' + #13#10 +
                      '  </appSettings>' + #13#10 +
                      '</configuration>';
      
      SaveStringToFile(ConfigFile, ConfigContent, False);
    end;
  end;
end;
