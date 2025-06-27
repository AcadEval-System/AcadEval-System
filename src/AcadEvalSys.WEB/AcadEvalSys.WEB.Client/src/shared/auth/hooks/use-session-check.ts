// ✅ VERSIÓN MEJORADA de use-session-check.ts
import { useEffect, useState } from "react";
import { authService } from "../services/auth-service";
import { authStore } from "../stores/auth-store";

/**
 * Hook para verificar sesión usando endpoint limpio que nunca devuelve 401
 * Sigue Clean Architecture y evita logs rojos en consola
 */
export const useSessionCheck = () => {
  const [isCheckingSession, setIsCheckingSession] = useState(true);

  useEffect(() => {
    const checkSession = async () => {
      try {
        const sessionStatus = await authService.checkSession();

        if (sessionStatus.isAuthenticated && sessionStatus.user) {
          authStore.getState().setAuth(sessionStatus.user);
        } else {
          authStore.getState().clearAuth();
        }
      } catch (error) {
        console.error("Error de conexión al verificar sesión:", error);
        authStore.getState().clearAuth();
      } finally {
        setIsCheckingSession(false);
      }
    };

    checkSession();
  }, []);

  return { isCheckingSession };
};
