import { useEffect, useState } from "react";
import { authService } from "../services/auth-service";
import { authStore } from "../stores/auth-store";

/**
 * Hook simple: solo verifica cookies al cargar la app
 */
export const useSessionCheck = () => {
  const [isCheckingSession, setIsCheckingSession] = useState(true);

  useEffect(() => {
    const checkSession = async () => {
      console.log("🔍 Verificando sesión...");
      try {
        // Intentar obtener usuario con cookies actuales
        const userData = await authService.getCurrentUser();
        console.log("✅ Usuario válido encontrado:", userData.email);
        authStore.getState().setAuth(userData);
      } catch (error) {
        // Si falla, no hay sesión válida
        console.log("❌ No hay sesión válida, limpiando estado");
        authStore.getState().clearAuth();
      } finally {
        // IMPORTANTE: Siempre setear que terminó la verificación
        setIsCheckingSession(false);
        console.log("🏁 Verificación completada");
      }
    };

    checkSession();
  }, []);

  return { isCheckingSession };
};
