# C# DotNet RabbitMQ

- RabbitSender sends one message every 1 seconds
- RabbitReceiver1 receives one message every 5 seconds
- RabbitReceiver2 receives one message every 3 seconds


# RabbitMQ

- message broker
- decouple applications for microservies, scale up, standalone server for better performance
- avoid tightly coupled and connect directly in monolithic architecture


# Docker

Running RabbitMQ on Docker

`docker run -d --hostname rmq --name rabbit-server -p 8080:15672 -p 5672:5672 rabbitmq:3-management`

> Container name = rabbit-server
> host port = 8080
> guest = 15672

[RabbitMQ Management](http://localhost:8080)



