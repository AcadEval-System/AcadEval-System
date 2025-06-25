import { api } from "@/shared/config/axios";
import { LoginCredentials, User } from "../types";
import { authStore } from "../stores/auth-store";

const AUTH_API_URL = "/identity";

export const authService = {
  async login(credentials: LoginCredentials): Promise<void> {
    await api.post(`${AUTH_API_URL}/login?useCookies=true`, credentials);
  },

  async logout(): Promise<void> {
    await api.post(`${AUTH_API_URL}/logout`);
    authStore.getState().clearAuth();
  },

  async getCurrentUser(): Promise<User> {
    const response = await api.get<User>(`${AUTH_API_URL}/info`);
    return response.data;
  },
};
