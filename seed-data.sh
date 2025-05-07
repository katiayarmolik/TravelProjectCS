#!/bin/bash

# Базовий URL API
BASE_URL="http://localhost:5000/api"

# Створення користувачів
echo "Creating users..."
curl -X POST "$BASE_URL/user" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "john_doe",
    "email": "john@example.com",
    "passwordHash": "password123",
    "firstName": "John",
    "lastName": "Doe"
  }'

curl -X POST "$BASE_URL/user" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "jane_smith",
    "email": "jane@example.com",
    "passwordHash": "password123",
    "firstName": "Jane",
    "lastName": "Smith"
  }'

# Створення місць призначення
echo "Creating destinations..."
curl -X POST "$BASE_URL/destination" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Paris",
    "country": "France",
    "description": "The City of Light, known for its iconic Eiffel Tower and rich cultural heritage."
  }'

curl -X POST "$BASE_URL/destination" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "London",
    "country": "UK",
    "description": "The capital of England, famous for Big Ben and the Royal Family."
  }'

curl -X POST "$BASE_URL/destination" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Rome",
    "country": "Italy",
    "description": "The Eternal City, home to the Colosseum and Vatican City."
  }'

curl -X POST "$BASE_URL/destination" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Barcelona",
    "country": "Spain",
    "description": "A vibrant city known for its unique architecture and beautiful beaches."
  }'

# Створення подорожей
echo "Creating trips..."
curl -X POST "$BASE_URL/trip" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Paris Adventure",
    "description": "A week-long exploration of Paris",
    "startDate": "2024-06-01T00:00:00",
    "endDate": "2024-06-08T00:00:00",
    "price": 1500.00,
    "popularityCount": 0,
    "destinationId": 1,
    "departureLocationId": 2,
    "userId": 1
  }'

curl -X POST "$BASE_URL/trip" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "London Weekend",
    "description": "A weekend getaway to London",
    "startDate": "2024-07-15T00:00:00",
    "endDate": "2024-07-17T00:00:00",
    "price": 800.00,
    "popularityCount": 0,
    "destinationId": 2,
    "departureLocationId": 1,
    "userId": 2
  }'

curl -X POST "$BASE_URL/trip" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Roman Holiday",
    "description": "Experience the ancient wonders of Rome",
    "startDate": "2024-08-01T00:00:00",
    "endDate": "2024-08-05T00:00:00",
    "price": 1200.00,
    "popularityCount": 0,
    "destinationId": 3,
    "departureLocationId": 4,
    "userId": 1
  }'

curl -X POST "$BASE_URL/trip" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Barcelona Beach Vacation",
    "description": "Relax on the beautiful beaches of Barcelona",
    "startDate": "2024-09-01T00:00:00",
    "endDate": "2024-09-08T00:00:00",
    "price": 1800.00,
    "popularityCount": 0,
    "destinationId": 4,
    "departureLocationId": 3,
    "userId": 2
  }'

echo "Data seeding completed!" 