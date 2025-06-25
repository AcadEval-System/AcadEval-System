import { create } from "zustand";
import { User } from "@/shared/auth/types";

interface AuthStore {
  isAuthenticated: boolean;
  user: User | null;
  setAuth: (user: User) => void;
  clearAuth: () => void;
}

// Store ultra-simple con selectores fijos
export const authStore = create<AuthStore>((set) => ({
  isAuthenticated: false,
  user: null,
  setAuth: (user: User) => set({ isAuthenticated: true, user }),
  clearAuth: () => set({ isAuthenticated: false, user: null }),
}));

// Selectores fijos para evitar loops
const selectIsAuthenticated = (state: AuthStore) => state.isAuthenticated;
const selectUser = (state: AuthStore) => state.user;

// Hooks con selectores fijos
export const useIsAuthenticated = () => authStore(selectIsAuthenticated);
export const useUser = () => authStore(selectUser);
