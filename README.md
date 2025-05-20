# HomeHealth
# 🩺 VitalSigns Monitoring System

Este proyecto implementa un sistema distribuido para el monitoreo de signos vitales en un entorno de cuidado en casa. Utiliza procesamiento en el dispositivo móvil, mensajería MQTT para comunicación, y almacenamiento persistente en SQLite.

## 📦 Componentes

| Componente               | Función                                                  |
|--------------------------|-----------------------------------------------------------|
| `VitalSignsProcessor`    | Simula sensores en el dispositivo móvil y publica por MQTT |
| `VitalSignsStorage`      | Recibe datos desde MQTT y los almacena en SQLite          |
| `Mosquitto Broker`       | Middleware MQTT para comunicación desacoplada             |
| `Blackboard` (opcional)  | Motor de inferencia que accede al almacenamiento vía REST |
| `AdvisoryWebApp` (opcional) | Interfaz web para consultas del equipo asesor          |

## ✅ Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Mosquitto MQTT](https://mosquitto.org/download/)
- VS Code (opcional)
- Extensión SQLite Viewer (opcional)

---

## 🚀 Instalación y Ejecución

### 1. Clona este repositorio

```bash
git clone https://github.com/tu-usuario/nombre-repo.git
cd nombre-repo
