# Базовий URL API
$BASE_URL = "http://localhost:5282/api"

# Створення користувачів
Write-Host "Creating users..."
$user1 = @{
    username = "john_doe"
    email = "john@example.com"
    passwordHash = "password123"
    firstName = "John"
    lastName = "Doe"
} | ConvertTo-Json

$user2 = @{
    username = "jane_smith"
    email = "jane@example.com"
    passwordHash = "password123"
    firstName = "Jane"
    lastName = "Smith"
} | ConvertTo-Json

Invoke-RestMethod -Uri "$BASE_URL/user" -Method Post -Body $user1 -ContentType "application/json"
Invoke-RestMethod -Uri "$BASE_URL/user" -Method Post -Body $user2 -ContentType "application/json"

# Створення місць призначення
Write-Host "Creating destinations..."
$destinations = @(
    @{
        name = "Paris"
        country = "France"
        description = "The City of Light, known for its iconic Eiffel Tower and rich cultural heritage."
    },
    @{
        name = "London"
        country = "UK"
        description = "The capital of England, famous for Big Ben and the Royal Family."
    },
    @{
        name = "Rome"
        country = "Italy"
        description = "The Eternal City, home to the Colosseum and Vatican City."
    },
    @{
        name = "Barcelona"
        country = "Spain"
        description = "A vibrant city known for its unique architecture and beautiful beaches."
    }
)

foreach ($destination in $destinations) {
    $body = $destination | ConvertTo-Json
    Invoke-RestMethod -Uri "$BASE_URL/destination" -Method Post -Body $body -ContentType "application/json"
}

# Створення подорожей
Write-Host "Creating trips..."
$trips = @(
    @{
        title = "Paris Adventure"
        description = "A week-long exploration of Paris"
        startDate = "2024-06-01T00:00:00"
        endDate = "2024-06-08T00:00:00"
        price = 1500.00
        popularityCount = 0
        destinationId = 1
        departureLocationId = 2
        userId = 1
    },
    @{
        title = "London Weekend"
        description = "A weekend getaway to London"
        startDate = "2024-07-15T00:00:00"
        endDate = "2024-07-17T00:00:00"
        price = 800.00
        popularityCount = 0
        destinationId = 2
        departureLocationId = 1
        userId = 2
    },
    @{
        title = "Roman Holiday"
        description = "Experience the ancient wonders of Rome"
        startDate = "2024-08-01T00:00:00"
        endDate = "2024-08-05T00:00:00"
        price = 1200.00
        popularityCount = 0
        destinationId = 3
        departureLocationId = 4
        userId = 1
    },
    @{
        title = "Barcelona Beach Vacation"
        description = "Relax on the beautiful beaches of Barcelona"
        startDate = "2024-09-01T00:00:00"
        endDate = "2024-09-08T00:00:00"
        price = 1800.00
        popularityCount = 0
        destinationId = 4
        departureLocationId = 3
        userId = 2
    }
)

foreach ($trip in $trips) {
    $body = $trip | ConvertTo-Json
    try {
        $response = Invoke-RestMethod -Uri "$BASE_URL/trip" -Method Post -Body $body -ContentType "application/json"
        Write-Host "Created trip: $($response.title)"
    }
    catch {
        Write-Host "Error creating trip: $($_.Exception.Message)"
        Write-Host "Request body: $body"
    }
}

Write-Host "Data seeding completed!" 