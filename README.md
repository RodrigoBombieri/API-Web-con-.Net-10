# 🛠️ Sistema de Gestión de un Taller Mecánico

Este proyecto es una **Web API robusta** desarrollada con .NET 10, diseñada para la gestión integral de un taller mecánico. La aplicación permite el control de vehículos, clientes, órdenes de servicio y un inventario dinámico de repuestos con lógica de negocio aplicada.

---

## 🚀 Tecnologías y Herramientas

* **Framework:** .NET 10 (ASP.NET Core Web API)
* **Base de Datos:** SQL Server
* **ORM:** Entity Framework Core (Code First)
* **Mapeo de Objetos:** AutoMapper (Arquitectura basada en DTOs)
* **Seguridad:** ASP.NET Core Identity
* **Documentación Interactiva:** Scalar (OpenAPI v3)
* **Control de Versiones:** Git & GitHub

---

## 🧠 Características Principales

* **Arquitectura DTO:** Separación estricta entre las entidades de base de datos y los objetos de transferencia de datos para mayor seguridad, evitando el "Overposting" y protegiendo datos sensibles.
* **Lógica de Inventario:** El sistema cuenta con validaciones inteligentes que descuentan automáticamente el stock de repuestos al momento de asignarlos a una orden de servicio.
* **Manejo de Estados:** Gestión profesional de órdenes de servicio mediante Enums (Pendiente, En Proceso, Finalizado, Entregado).
* **Relaciones Complejas:** Implementación de relaciones 1:N (Usuario-Vehículo) y N:N mediante tablas intermedias (Orden-Repuesto) para mantener un historial de precios de venta.

---

## 📊 Modelo de Datos (ERD)

El sistema organiza la información en las siguientes entidades:

1.  **Usuarios:** Gestión de identidad y roles.
2.  **Vehículos:** Registro de marca, modelo, placa y vinculación con el dueño.
3.  **Órdenes de Servicio:** Registro de fallas, diagnósticos técnicos y seguimiento de estados.
4.  **Repuestos:** Control de catálogo, precios de lista y stock disponible.
5.  **Detalle de Repuestos:** Registro de consumo de materiales por cada orden, fijando el precio al momento de la transacción.

---
