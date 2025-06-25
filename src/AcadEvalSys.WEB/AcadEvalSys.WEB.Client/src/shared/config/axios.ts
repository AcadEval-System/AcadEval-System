/// <reference types="vite/client" />
import axios from "axios";

const api = axios.create({
  baseURL: "/api",
  withCredentials: true,
});

// Interceptor de respuesta para manejar errores de autenticación
api.interceptors.response.use(
  (response) => response,
  async (error) => {
    const originalRequest = error.config;

    // Si el error es 401 (Unauthorized) y no es un intento de renovación
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      // Si el request fallido es al endpoint de login, no intentar renovar
      if (originalRequest.url?.includes("/identity/login")) {
        return Promise.reject(error);
      }

      console.log("Sesión expirada, limpiando estado local");

      // Limpiar el estado de autenticación
      if (typeof window !== "undefined") {
        const { authStore } = await import("../auth/stores/auth-store");
        authStore.getState().clearAuth();
        // AppRouter se encargará automáticamente de la navegación
      }

      return Promise.reject(error);
    }

    return Promise.reject(error);
  }
);

export { api };
