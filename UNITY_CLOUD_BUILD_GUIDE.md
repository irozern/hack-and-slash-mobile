# 🎮 UNITY CLOUD BUILD - KOMPLETNY PRZEWODNIK

**Unity Cloud Build** to oficjalne narzędzie Unity do automatycznego budowania aplikacji mobilnych. Jest **darmowe** i **bardzo proste**!

---

## ✅ WYMAGANIA

- ✅ Konto Unity (darmowe)
- ✅ Projekt na GitHub (już masz!)
- ✅ GitHub account (już masz!)
- ✅ 15 minut czasu

---

## 🚀 KROK PO KROKU (15 MINUT)

### KROK 1: Zaloguj się do Unity

1. Idź na: https://id.unity.com/en/signin
2. Zaloguj się (lub utwórz konto)
3. Kliknij "Sign in"

### KROK 2: Otwórz Unity Cloud Build

1. Idź na: https://cloud.unity.com/
2. Kliknij "Cloud Build"
3. Kliknij "Get Started"

### KROK 3: Utwórz Nowy Projekt

1. Kliknij "New Project"
2. Wpisz:
   - **Project Name**: `hack-and-slash-mobile`
   - **Description**: `Isometric Hack & Slash Mobile Game`
3. Kliknij "Create"

### KROK 4: Połącz GitHub

1. Kliknij "Link Repository"
2. Wybierz "GitHub"
3. Kliknij "Authorize"
4. Zaloguj się na GitHub (jeśli trzeba)
5. Kliknij "Authorize unity-cloud-build"

### KROK 5: Wybierz Repozytorium

1. Wyszukaj: `hack-and-slash-mobile`
2. Kliknij na swoje repozytorium
3. Kliknij "Next"

### KROK 6: Skonfiguruj Build

1. **Build Target**: Wybierz "Android"
2. **Branch**: Wybierz "main"
3. **Build Name**: `Android Release`
4. Kliknij "Next"

### KROK 7: Skonfiguruj Android Signing

1. **Signing**: Wybierz "Create New"
2. Wpisz:
   - **Keystore**: Wygeneruj nowy
   - **Alias**: `release-key`
   - **Password**: Wymyśl silne hasło
3. Kliknij "Create Keystore"

### KROK 8: Uruchom Pierwszy Build

1. Kliknij "Build"
2. Czekaj na build (15-20 minut)
3. Obserwuj progress na ekranie

### KROK 9: Pobierz APK

1. Gdy build się skończy, kliknij "Download"
2. Pobierz `HackSlash-Android.apk`
3. Rozmiar: ~120-150 MB

---

## 📱 INSTALACJA NA ANDROIDZIE

### Metoda 1: USB Cable (Najszybciej)

```bash
# Na komputerze
adb install HackSlash-Android.apk

# Czekaj ~30 sekund
# Gra powinna się zainstalować
```

### Metoda 2: Transfer Pliku

1. Skopiuj `HackSlash-Android.apk` na telefon
2. Na telefonie: Otwórz Files
3. Znajdź `HackSlash-Android.apk`
4. Kliknij → Zainstaluj
5. Potwierdź

### Metoda 3: Google Drive

1. Wrzuć `HackSlash-Android.apk` na Google Drive
2. Na telefonie: Otwórz Google Drive
3. Pobierz plik
4. Zainstaluj

---

## 🔄 AUTOMATYCZNE BUILDY

Po pierwszej konfiguracji, **każdy push do GitHub automatycznie buduje nowy APK**!

```bash
# Na Twoim komputerze
git add .
git commit -m "Update game"
git push origin main

# → Unity Cloud Build automatycznie buduje APK!
# → Możesz go pobrać za 15-20 minut
```

---

## 📊 CZAS BUILD'U

| Etap | Czas |
|------|------|
| Setup | 5 min |
| Konfiguracja | 5 min |
| Build | 15-20 min |
| Download | 2 min |
| **Razem** | **27-32 min** |

---

## ✅ CHECKLIST

- [ ] Unity account utworzony
- [ ] Cloud Build otworzony
- [ ] Projekt utworzony
- [ ] GitHub połączony
- [ ] Repozytorium wybrane
- [ ] Build target ustawiony
- [ ] Android signing skonfigurowany
- [ ] Pierwszy build uruchomiony
- [ ] Build się powiedzie
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

## 🔐 BEZPIECZEŃSTWO

⚠️ **Ważne**: Keystore jest prywatny!

- ✅ Nie udostępniaj keystore'a
- ✅ Zapamiętaj hasło
- ✅ Przechowuj w bezpiecznym miejscu

---

## 📞 TROUBLESHOOTING

### Problem: "Build failed"
```
Rozwiązanie:
1. Sprawdź błędy w Cloud Build
2. Przeczytaj logi
3. Napraw błędy w kodzie
4. Push do GitHub
5. Spróbuj ponownie
```

### Problem: "Android signing error"
```
Rozwiązanie:
1. Usuń stary keystore
2. Utwórz nowy keystore
3. Spróbuj ponownie
```

### Problem: "Build takes too long"
```
Rozwiązanie:
1. To normalne (15-20 minut)
2. Czekaj cierpliwie
3. Możesz zamknąć przeglądarkę
4. Build będzie kontynuowany
```

---

## 💡 TIPS & TRICKS

### Automatyczne Notyfikacje
1. Cloud Build → Settings
2. Email Notifications
3. Zaznacz "Build succeeded"
4. Zaznacz "Build failed"

### Wiele Build Targets
1. Możesz dodać iOS build
2. Możesz dodać WebGL build
3. Każdy buduje się niezależnie

### Wersjonowanie
1. Każdy build ma numer wersji
2. Możesz śledzić historię
3. Możesz wrócić do starszych wersji

---

## 🎉 GOTOWE!

Po 30 minutach będziesz mieć:
- ✅ APK na komputerze
- ✅ Grę zainstalowaną na telefonie
- ✅ Automatyczne buildy na każdy push

---

**Powodzenia z Unity Cloud Build!** 🚀🎮
