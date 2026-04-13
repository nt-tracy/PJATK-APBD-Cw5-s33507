W tym zadaniu budujesz aplikację ASP.NET Core Web API opartą na kontrolerach. Celem jest przećwiczenie routingu, metod HTTP, model bindingu z route, query i body oraz zwracania poprawnych kodów statusu. Dane przechowujesz wyłącznie w pamięci aplikacji, a działanie endpointów weryfikujesz później w Postmanie.

Kontekst biznesowy
Centrum szkoleniowe organizuje warsztaty i konsultacje w kilku salach dydaktycznych. Koordynator potrzebuje prostego backendu, który pozwoli zarządzać listą sal oraz ich rezerwacjami. Na tym etapie system nie korzysta jeszcze z prawdziwej bazy danych. Najważniejsze jest poprawne zaprojektowanie API i przećwiczenie pracy z żądaniami HTTP.

Aplikacja ma umożliwiać przeglądanie sal, filtrowanie ich po wybranych parametrach, dodawanie nowych pozycji, aktualizację danych, usuwanie sal oraz zarządzanie rezerwacjami. Dane startowe mają być inicjalizowane w momencie uruchomienia aplikacji i przechowywane w statycznych listach.

Ważne. Nie używamy tu jeszcze Entity Framework Core ani bazy SQL. Źródłem danych są statyczne listy w pamięci aplikacji, przygotowane przy jej starcie.

Co masz przygotować
Stwórz aplikację ASP.NET Core Web API w C#, która zawiera dwa kontrolery:

RoomsController do zarządzania salami,
ReservationsController do zarządzania rezerwacjami.
Oba kontrolery mają działać na danych przechowywanych w pamięci aplikacji. Dane powinny być dostępne po starcie programu bez konieczności ręcznego dodawania rekordów.

Proponowane modele danych
Room

Id
Name
BuildingCode
Floor
Capacity
HasProjector
IsActive
Reservation

Id
RoomId
OrganizerName
Topic
Date
StartTime
EndTime
Status np. planned, confirmed, cancelled
Zakres endpointów
Kontroler sal
Metoda Endpoint Opis GET /api/rooms Zwraca wszystkie sale. GET /api/rooms/{id} Zwraca pojedynczą salę po identyfikatorze. GET /api/rooms/building/{buildingCode} Zwraca sale z wybranego budynku. Parametr buildingCode ma być pobierany z trasy. GET /api/rooms?minCapacity=20&hasProjector=true&activeOnly=true Zwraca sale przefiltrowane po query stringu. POST /api/rooms Dodaje nową salę. PUT /api/rooms/{id} Aktualizuje pełne dane sali. DELETE /api/rooms/{id} Usuwa salę.
Kontroler rezerwacji
Metoda Endpoint Opis GET /api/reservations Zwraca wszystkie rezerwacje. GET /api/reservations/{id} Zwraca jedną rezerwację. GET /api/reservations?date=2026-05-10&status=confirmed&roomId=2 Zwraca rezerwacje przefiltrowane po query stringu. POST /api/reservations Tworzy nową rezerwację. PUT /api/reservations/{id} Aktualizuje istniejącą rezerwację. DELETE /api/reservations/{id} Usuwa rezerwację.
Najważniejsze. W zadaniu muszą pojawić się różne sposoby przekazywania danych: id i buildingCode z trasy, filtry z query stringa oraz dane obiektów z body żądania w formacie JSON.

Wymagania techniczne
1. Kontrolery i routing
Użyj kontrolerów z atrybutami [ApiController] oraz [Route("api/[controller]")].
Każda operacja ma być obsługiwana przez poprawny atrybut, np. [HttpGet], [HttpPost], [HttpPut], [HttpDelete].
W Program.cs skonfiguruj kontrolery przez AddControllers() i mapowanie przez app.MapControllers().
2. Przechowywanie danych
Dane mają być inicjalizowane przy starcie aplikacji.
W pamięci aplikacji powinno znajdować się co najmniej 4-5 sal oraz 4-6 przykładowych rezerwacji.
Możesz trzymać te listy bezpośrednio w kontrolerach albo w jednej prostej klasie pomocniczej.
3. Modele danych
W tym zadaniu możesz przyjmować i zwracać bezpośrednio modele biznesowe.
Nie musisz przygotowywać osobnych klas pośrednich do wejścia i wyjścia.
4. Walidacja danych
Dodaj Data Annotations do klas używanych jako dane wejściowe.
Name, BuildingCode, OrganizerName i Topic nie powinny być puste.
Capacity musi być większe od zera.
EndTime musi być późniejsze niż StartTime.
Przy błędnych danych aplikacja ma zwracać 400 Bad Request.
5. Statusy HTTP
200 OK dla poprawnych odczytów i aktualizacji,
201 Created dla poprawnie utworzonego zasobu,
204 No Content dla poprawnego usunięcia,
404 Not Found gdy zasób nie istnieje,
409 Conflict gdy próbujesz dodać rezerwację kolidującą czasowo z istniejącą rezerwacją tej samej sali.
6. Proste reguły biznesowe
Nie wolno dodać rezerwacji dla sali, która nie istnieje.
Nie wolno dodać rezerwacji dla sali oznaczonej jako nieaktywna.
Dwie rezerwacje tej samej sali nie mogą nakładać się czasowo tego samego dnia.
Usunięcie sali może zwracać 409 Conflict, jeśli dla tej sali istnieją przyszłe rezerwacje, albo możesz przyjąć prostszą wersję i nie pozwalać usuwać sali z powiązanymi rezerwacjami.
Sugerowana organizacja projektu
folder Controllers z klasami RoomsController i ReservationsController,
folder Models na klasy domenowe,
opcjonalnie jedna prosta klasa pomocnicza na statyczne listy, jeśli chcesz mieć dane poza kontrolerami.
Nie musisz tworzyć rozbudowanej architektury. Kod ma być czytelny i prosty. Na tym etapie większość logiki może być utrzymywana wspólnie w kontrolerach.

Przykładowe dane wejściowe
Przykładowy POST do utworzenia sali
{
  "name": "Lab 204",
  "buildingCode": "B",
  "floor": 2,
  "capacity": 24,
  "hasProjector": true,
  "isActive": true
}
Kopiuj kod
Przykładowy POST do utworzenia rezerwacji
{
  "roomId": 2,
  "organizerName": "Anna Kowalska",
  "topic": "Warsztaty z HTTP i REST",
  "date": "2026-05-10",
  "startTime": "10:00:00",
  "endTime": "12:30:00",
  "status": "confirmed"
}
Kopiuj kod
Zakres testów w Postmanie
Po przygotowaniu aplikacji przetestuj jej działanie w Postmanie. Nie chodzi tylko o pojedyncze kliknięcie każdego endpointu, ale o sprawdzenie pełnego przepływu pracy i reakcji API na różne przypadki.

Minimalny zestaw scenariuszy testowych
Pobranie wszystkich sal przez GET /api/rooms.
Pobranie jednej sali po identyfikatorze.
Pobranie sal po kodzie budynku przekazanym w trasie.
Filtrowanie sal przez query string.
Dodanie nowej sali przez POST.
Aktualizacja istniejącej sali przez PUT.
Dodanie rezerwacji poprawnej biznesowo.
Próba dodania rezerwacji kolidującej czasowo i sprawdzenie 409 Conflict.
Usunięcie rezerwacji przez DELETE.
Sprawdzenie odpowiedzi 404 Not Found dla nieistniejącego zasobu.
Ważne. W Postmanie warto sprawdzić nie tylko poprawne przypadki, ale też błędne dane wejściowe. Dzięki temu zobaczysz, czy model binding, walidacja i statusy HTTP działają zgodnie z założeniami.

Na co zwrócić uwagę
Zadbaj o czytelne nazwy endpointów i klas.
Przy POST zwróć 201 Created, najlepiej z użyciem CreatedAtAction.
Przy PUT przyjmij pełną aktualizację obiektu, a nie częściową zmianę jednego pola.
Jeśli zasób nie istnieje, nie zwracaj 200 OK z pustą wartością, tylko sensowny kod błędu.
Co to zadanie ma przećwiczyć
projektowanie REST API w ASP.NET Core z użyciem kontrolerów,
użycie metod HTTP GET, POST, PUT i DELETE,
model binding z route, query string i body,
walidację danych wejściowych,
zwracanie poprawnych kodów statusu HTTP,
testowanie działania API w Postmanie.
