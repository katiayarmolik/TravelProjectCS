# Базовий URL API
$baseUrl = "http://localhost:5282/api"

# Створення користувачів
Write-Host "Creating users..."
$johnDoe = @{
    username = "john_doe"
    email = "john@example.com"
    password = "password123"
    firstName = "John"
    lastName = "Doe"
} | ConvertTo-Json

$janeSmith = @{
    username = "jane_smith"
    email = "jane@example.com"
    password = "password123"
    firstName = "Jane"
    lastName = "Smith"
} | ConvertTo-Json

try {
    $johnResponse = Invoke-RestMethod -Uri "$baseUrl/User" -Method Post -Body $johnDoe -ContentType "application/json"
    Write-Host "Created user: $($johnResponse.username)"
} catch {
    Write-Host "Error creating John Doe: $_"
    Write-Host "Request body: $johnDoe"
    exit
}

try {
    $janeResponse = Invoke-RestMethod -Uri "$baseUrl/User" -Method Post -Body $janeSmith -ContentType "application/json"
    Write-Host "Created user: $($janeResponse.username)"
} catch {
    Write-Host "Error creating Jane Smith: $_"
    Write-Host "Request body: $janeSmith"
    exit
}

# Створення місць призначення
Write-Host "Creating destinations..."
$paris = @{
    name = "Paris"
    country = "France"
    description = "The City of Light"
} | ConvertTo-Json

$london = @{
    name = "London"
    country = "United Kingdom"
    description = "The Big Smoke"
} | ConvertTo-Json

$rome = @{
    name = "Rome"
    country = "Italy"
    description = "The Eternal City"
} | ConvertTo-Json

$barcelona = @{
    name = "Barcelona"
    country = "Spain"
    description = "The City of Counts"
} | ConvertTo-Json

try {
    $parisResponse = Invoke-RestMethod -Uri "$baseUrl/Destination" -Method Post -Body $paris -ContentType "application/json"
    Write-Host "Created destination: $($parisResponse.name)"
} catch {
    Write-Host "Error creating Paris: $_"
    Write-Host "Request body: $paris"
    exit
}

try {
    $londonResponse = Invoke-RestMethod -Uri "$baseUrl/Destination" -Method Post -Body $london -ContentType "application/json"
    Write-Host "Created destination: $($londonResponse.name)"
} catch {
    Write-Host "Error creating London: $_"
    Write-Host "Request body: $london"
    exit
}

try {
    $romeResponse = Invoke-RestMethod -Uri "$baseUrl/Destination" -Method Post -Body $rome -ContentType "application/json"
    Write-Host "Created destination: $($romeResponse.name)"
} catch {
    Write-Host "Error creating Rome: $_"
    Write-Host "Request body: $rome"
    exit
}

try {
    $barcelonaResponse = Invoke-RestMethod -Uri "$baseUrl/Destination" -Method Post -Body $barcelona -ContentType "application/json"
    Write-Host "Created destination: $($barcelonaResponse.name)"
} catch {
    Write-Host "Error creating Barcelona: $_"
    Write-Host "Request body: $barcelona"
    exit
}

# Створення подорожей
Write-Host "Creating trips..."
$parisTrip = @{
    title = "Paris Adventure"
    description = "A week in the City of Light"
    startDate = "2024-06-01T00:00:00"
    endDate = "2024-06-08T00:00:00"
    price = 1500.00
    destinationId = $parisResponse.id
    departureLocationId = $londonResponse.id
    userId = $johnResponse.id
} | ConvertTo-Json

$londonTrip = @{
    title = "London Weekend"
    description = "Quick getaway to London"
    startDate = "2024-07-15T00:00:00"
    endDate = "2024-07-17T00:00:00"
    price = 800.00
    destinationId = $londonResponse.id
    departureLocationId = $parisResponse.id
    userId = $janeResponse.id
} | ConvertTo-Json

$romeTrip = @{
    title = "Roman Holiday"
    description = "Experience ancient Rome"
    startDate = "2024-08-01T00:00:00"
    endDate = "2024-08-07T00:00:00"
    price = 1200.00
    destinationId = $romeResponse.id
    departureLocationId = $barcelonaResponse.id
    userId = $johnResponse.id
} | ConvertTo-Json

$barcelonaTrip = @{
    title = "Barcelona Beach"
    description = "Sun and sea in Barcelona"
    startDate = "2024-09-01T00:00:00"
    endDate = "2024-09-08T00:00:00"
    price = 1100.00
    destinationId = $barcelonaResponse.id
    departureLocationId = $romeResponse.id
    userId = $janeResponse.id
} | ConvertTo-Json

try {
    $parisTripResponse = Invoke-RestMethod -Uri "$baseUrl/Trip" -Method Post -Body $parisTrip -ContentType "application/json"
    Write-Host "Created trip: $($parisTripResponse.title)"
} catch {
    Write-Host "Error creating Paris trip: $_"
    Write-Host "Request body: $parisTrip"
}

try {
    $londonTripResponse = Invoke-RestMethod -Uri "$baseUrl/Trip" -Method Post -Body $londonTrip -ContentType "application/json"
    Write-Host "Created trip: $($londonTripResponse.title)"
} catch {
    Write-Host "Error creating London trip: $_"
    Write-Host "Request body: $londonTrip"
}

try {
    $romeTripResponse = Invoke-RestMethod -Uri "$baseUrl/Trip" -Method Post -Body $romeTrip -ContentType "application/json"
    Write-Host "Created trip: $($romeTripResponse.title)"
} catch {
    Write-Host "Error creating Rome trip: $_"
    Write-Host "Request body: $romeTrip"
}

try {
    $barcelonaTripResponse = Invoke-RestMethod -Uri "$baseUrl/Trip" -Method Post -Body $barcelonaTrip -ContentType "application/json"
    Write-Host "Created trip: $($barcelonaTripResponse.title)"
} catch {
    Write-Host "Error creating Barcelona trip: $_"
    Write-Host "Request body: $barcelonaTrip"
}

Write-Host "Data seeding completed!" 