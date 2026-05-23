# 🎮 LOCAL BUILD GUIDE - Build APK na Twoim Komputerze

Ponieważ Codemagic free tier ma ograniczenia, będziesz budować APK **lokalnie na swoim komputerze**.

---

## 📋 WYMAGANIA

### Windows/Mac/Linux
- **Unity 2022 LTS** lub nowszy (5-10 GB)
- **Android SDK** (5 GB)
- **Java Development Kit (JDK)** 11+ (500 MB)
- **Gradle** (automatycznie)
- **Minimum 8 GB RAM**

---

## 🚀 KROK PO KROKU

### KROK 1: Zainstaluj Unity

1. Pobierz: https://unity.com/download
2. Zainstaluj **Unity Hub**
3. Zainstaluj **Unity 2022 LTS** (LTS = Long Term Support)
4. Czas: ~30 minut

### KROK 2: Zainstaluj Android SDK

#### Windows/Mac
1. Pobierz: https://developer.android.com/studio
2. Zainstaluj **Android Studio**
3. Otwórz Android Studio
4. Tools → SDK Manager
5. Zainstaluj:
   - Android SDK Platform 33+
   - Android SDK Build Tools 33+
   - Android Emulator
6. Czas: ~20 minut

#### Linux
```bash
# Ubuntu/Debian
sudo apt-get install -y openjdk-11-jdk-headless
sudo apt-get install -y android-sdk
```

### KROK 3: Skonfiguruj Unity

1. Otwórz **Unity Hub**
2. Kliknij "Add project"
3. Wybierz folder `HackSlashGame`
4. Czekaj na import (2-3 minuty)

### KROK 4: Otwórz Projekt w Unity

1. Kliknij projekt w Unity Hub
2. Czekaj na load (1-2 minuty)
3. Powinieneś zobaczyć scenę gry

### KROK 5: Skonfiguruj Build Settings

1. **File → Build Settings**
2. **Platform**: Wybierz **Android**
3. Kliknij **"Switch Platform"** (czekaj 2-3 minuty)
4. **Scenes In Build**: Dodaj `Assets/Scenes/GameScene`

### KROK 6: Skonfiguruj Player Settings

1. **File → Build Settings → Player Settings**
2. **Company Name**: Twoja nazwa
3. **Product Name**: `HackSlash`
4. **Package Name**: `com.yourname.hackslash`
5. **Version**: `1.0.0`
6. **Minimum API Level**: 24
7. **Target API Level**: 33

### KROK 7: Skonfiguruj Android SDK Path

1. **Edit → Preferences** (Mac: **Unity → Preferences**)
2. **External Tools**
3. **Android SDK Path**: Ustaw ścieżkę do Android SDK
   - Windows: `C:\Users\YourName\AppData\Local\Android\Sdk`
   - Mac: `/Users/YourName/Library/Android/sdk`
   - Linux: `/home/yourname/Android/Sdk`
4. **JDK Path**: Ustaw ścieżkę do JDK
   - Windows: `C:\Program Files\Java\jdk-11`
   - Mac: `/Library/Java/JavaVirtualMachines/jdk-11.jdk/Contents/Home`
   - Linux: `/usr/lib/jvm/java-11-openjdk-amd64`
5. **Gradle Path**: Pozostaw domyślnie

### KROK 8: Buduj APK

1. **File → Build Settings**
2. Kliknij **"Build"**
3. Wybierz folder do zapisania (np. `Desktop/Build`)
4. Nazwa: `HackSlash.apk`
5. Kliknij **"Save"**
6. **Czekaj 20-30 minut** — Unity buduje APK

### KROK 9: Pobierz APK

Po zakończeniu build'u:
- Plik `HackSlash.apk` będzie w wybranym folderze
- Rozmiar: ~120-150 MB

---

## 📱 INSTALACJA NA ANDROIDZIE

### Metoda 1: USB Cable (Najszybciej)

```bash
# Na komputerze
adb install HackSlash.apk

# Czekaj ~30 sekund
# Gra powinna się zainstalować
```

### Metoda 2: Transfer Pliku

1. Skopiuj `HackSlash.apk` na telefon (USB/Bluetooth)
2. Na telefonie: Otwórz Files
3. Znajdź `HackSlash.apk`
4. Kliknij → Zainstaluj
5. Potwierdź

### Metoda 3: Google Drive

1. Wrzuć `HackSlash.apk` na Google Drive
2. Na telefonie: Otwórz Google Drive
3. Pobierz plik
4. Zainstaluj

---

## ⚙️ TROUBLESHOOTING

### Problem: "Android SDK not found"
```
Rozwiązanie:
1. Edit → Preferences → External Tools
2. Ustaw Android SDK Path ręcznie
3. Restart Unity
```

### Problem: "JDK not found"
```
Rozwiązanie:
1. Zainstaluj JDK 11+
2. Edit → Preferences → External Tools
3. Ustaw JDK Path
4. Restart Unity
```

### Problem: "Build failed"
```
Rozwiązanie:
1. Otwórz Console (Window → General → Console)
2. Przeczytaj błędy
3. Szukaj rozwiązania w Google
4. Spróbuj ponownie
```

### Problem: "Out of memory"
```
Rozwiązanie:
1. Zamknij inne aplikacje
2. Zwiększ RAM dla Unity:
   - Edit → Preferences → General
   - Ustaw wyżej "Max Sprite Atlas Cache Size"
3. Spróbuj ponownie
```

---

## 📊 CZAS BUILD'U

| Etap | Czas |
|------|------|
| Instalacja Unity | 30 min |
| Instalacja Android SDK | 20 min |
| Konfiguracja | 10 min |
| Build APK | 20-30 min |
| **Razem** | **80-90 min** |

---

## ✅ CHECKLIST

- [ ] Unity 2022 LTS zainstalowany
- [ ] Android Studio zainstalowany
- [ ] Android SDK zainstalowany
- [ ] JDK zainstalowany
- [ ] Projekt otwarty w Unity
- [ ] Build Settings skonfigurowany
- [ ] Player Settings skonfigurowany
- [ ] Android SDK Path ustawiony
- [ ] JDK Path ustawiony
- [ ] Build APK uruchomiony
- [ ] APK pobierany
- [ ] APK zainstalowany na telefonie
- [ ] Gra uruchamia się

---

## 🎮 TESTOWANIE GRY

Po instalacji:

1. Otwórz aplikację "HackSlash"
2. Testuj:
   - Poruszanie się joystickiem
   - Atak na wrogów
   - Zbieranie lootu
   - UI
   - Performance (FPS)

---

## 📞 POMOC

Jeśli masz problemy:

1. Przeczytaj błąd w Console
2. Szukaj rozwiązania w Google
3. Sprawdź oficjalną dokumentację Unity
4. Spróbuj ponownie

---

**Powodzenia w budowaniu APK!** 🎮🚀
