# Shortify
# Description
Этот REST API предоставляет возможность сокращать длинные URL-адреса в более короткие. Вы можете использовать его для создания сервиса сокращения ссылок или интеграции в свое приложение.
# API Definition

## Create Short URL

### Short URL Request

```js
POST /api/short
```

```json
{
  "url": "string"
}
```

## Get Short URL

## Short URL Response


```js
GET /api/{{code}}
```

### Short URL Response

```js
200 Ok
```
```js
*Redirect* to your link with given code
```

