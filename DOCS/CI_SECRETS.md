CI secrets — jak przygotować pliki i dodać je do GitHub Actions

Krótkie kroki:
- wygeneruj lub przygotuj pliki podpisów (Android `.jks`, iOS `.p12`, iOS `.mobileprovision`)
- zakoduj je do Base64 bez łamania linii
- dodaj zawartość jako `Secrets` w repo (Settings → Secrets and variables → Actions)

Przykłady poleceń (Linux):

- Android keystore (Base64, jednowierszowo):

  base64 -w 0 my-release-key.jks > keystore.jks.base64

- macOS (jeśli `-w` nie istnieje):

  base64 my-release-key.jks | tr -d '\n' > keystore.jks.base64

- iOS .p12 i .mobileprovision:

  base64 -w 0 cert.p12 > cert.p12.base64
  base64 -w 0 profile.mobileprovision > profile.mobileprovision.base64

Uwaga: jeśli używasz macOS z BSD `base64`, użyj `-b` lub `tr -d '\n'` jak wyżej.

Jak dodać do GitHub Secrets:

1. Otwórz repo → Settings → Secrets and variables → Actions
2. New repository secret
3. Nazwy sekretów dla workflow w tym repo:
   - `ANDROID_KEYSTORE` — zawartość Base64 pliku `.jks`
   - `ANDROID_KEYSTORE_PASSWORD`
   - `ANDROID_KEY_ALIAS`
   - `ANDROID_KEY_PASSWORD`
   - `IOS_SIGNING_CERT_BASE64` — Base64 pliku `.p12`
   - `IOS_SIGNING_CERT_PASSWORD`
   - `IOS_PROVISIONING_PROFILE_BASE64` — Base64 pliku `.mobileprovision`
   - `IOS_APPLE_TEAM_ID`
   - opcjonalnie: `UNITY_LICENSE` — Base64 pliku licencji Unity (jeśli używasz offline activation)
   - opcjonalnie dla powiadomień e-mail:
     - `SMTP_HOST`
     - `SMTP_PORT`
     - `SMTP_USERNAME`
     - `SMTP_PASSWORD`
     - `EMAIL_FROM`
     - `RELEASE_EMAIL` — adres, na który ma trafić powiadomienie o release (domyślnie `karatpol@gmail.com`)

Przykład użycia `gh` (GitHub CLI) do dodania sekretu z pliku Base64:

  gh secret set ANDROID_KEYSTORE --body "$(cat keystore.jks.base64)"
  gh secret set ANDROID_KEYSTORE_PASSWORD --body "your_store_password"
  gh secret set ANDROID_KEY_ALIAS --body "your_key_alias"
  gh secret set ANDROID_KEY_PASSWORD --body "your_key_password"

  gh secret set IOS_SIGNING_CERT_BASE64 --body "$(cat cert.p12.base64)"
  gh secret set IOS_SIGNING_CERT_PASSWORD --body "your_p12_password"
  gh secret set IOS_PROVISIONING_PROFILE_BASE64 --body "$(cat profile.mobileprovision.base64)"
  gh secret set IOS_APPLE_TEAM_ID --body "YOUR_TEAM_ID"

Uwaga: `gh` musi być zalogowane i mieć uprawnienia do repozytorium (gh auth login).

Szybka weryfikacja lokalna (po dekodowaniu):

- sprawdź keystore:

  base64 --decode keystore.jks.base64 > keystore.jks
  keytool -list -v -keystore keystore.jks -storepass YOUR_STORE_PASS

- sprawdź .p12:

  openssl pkcs12 -in cert.p12 -nodes -passin pass:YOUR_P12_PASS -info

Jak lokalnie uruchomić build Unity (przykład):

# wymaga zainstalowanego Unity i ścieżki do binarki `Unity`
/path/to/Unity -quit -batchmode -projectPath . -executeMethod BuildCommand.BuildAndroid -logFile build.log

Po dodaniu sekretów uruchom workflow (push/PR) — powinien zdekodować pliki, wykonać build i przesłać artefakty (APK).

Uwaga: jeśli nie dodasz `ANDROID_KEYSTORE`, workflow automatycznie wygeneruje tymczasowy debug keystore (alias `testkey`, hasło `android`) i użyje go do zbudowania testowego APK. Ten keystore nie nadaje się do publikacji — służy wyłącznie do szybkich testów.

Bezpieczeństwo:
- nie zapisuj surowych plików podpisów w repo
- używaj `base64` jednowierszowego formatu dla sekretów
- usuń niepotrzebne secrets, gdy ich nie używasz
