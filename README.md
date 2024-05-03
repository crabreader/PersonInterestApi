# PersonInterestAPI
ASP.NET WebAPI

## Endpoints

### Interest

* [GET] /api/Interest
* [POST] /api/Interest
* [POST] /api/Interest/{personId}/link-interest/{interestId}
* [POST] /api/Interest/{personId}/interest/{interestId}/link

### Person

* [GET] /api/Person
* [POST] /api/Person
* [POST] /api/Person/{id}/interests
* [POST] /api/Person/{id}/links

## Testing

### 1. Create people

[**POST**] http://localhost:5010/api/person

Body:
```json
{
    "name": "John Doe",
    "phoneNumber": "123-456-7890"
}
```

[**POST**] http://localhost:5010/api/person

Body:
```json
{
    "name": "Jane Doe",
    "phoneNumber": "098-765-4321"
}
```

### 2. Create interests

[**POST**] http://localhost:5010/api/interest

Body:
```json
{
    "title": "Programming",
    "description": "The art and science of coding."
}
```

[**POST**] http://localhost:5010/api/interest

Body:
```json
{
    "title": "Cooking",
    "description": "Everything about preparing food."
}
```

### 3. Assign interests


[**POST**] http://localhost:5010/api/interest/1/link-interest/1

[**POST**] http://localhost:5010/api/interest/1/link-interest/2

[**POST**] http://localhost:5010/api/interest/2/link-interest/2

### 4. Assign links

[**POST**] http://localhost:5010/api/interest/1/interest/1/link

Body:
```json
{
  "url": "https://example.com/some-interesting-article"
}
```

[**POST**] http://localhost:5010/api/interest/1/interest/2/link

Body:
```json
{
  "url": "https://example.com/some-other-interesting-article"
}
```

[**POST**] http://localhost:5010/api/interest/2/interest/2/link

Body:
```json
{
  "url": "https://example.com/a-third-interesting-article"
}
```

### 5. Get all people

[**GET**] http://localhost:5010/api/person

### 6. Get all interests for a person

[**GET**] http://localhost:5010/api/person/2/interests

### 7. Get all links for a person

[**GET**] http://localhost:5010/api/person/1/links

